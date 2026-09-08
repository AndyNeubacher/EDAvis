using EDAvis.Tools;
using OfficeOpenXml;
using System;
using System.IO;
using System.Windows.Forms;




namespace EDAvis.GUI
{
    public partial class ReportExtractor : Form
    {
        public ReportExtractor()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string getExcelFileName()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                    return openFileDialog.FileName;
            }
            return "";
        }

        private void btnSelectReport_Month_Click(object sender, EventArgs e)
        {
            tbMonth.Text = getExcelFileName();
        }

        private void btnSelectReport_Year_Click(object sender, EventArgs e)
        {
            tbYear.Text = getExcelFileName();
        }

        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            if (!File.Exists(tbMonth.Text))
            {
                tbExtractLog.AppendText("\r\nselect valid monthly EDA-Report!\r\n");
                return;
            }

            AnalyzeAndMerge(tbExtractLog, tbMonth.Text, null);
        }


        private void btnAnalyzeAndMerge_Click(object sender, EventArgs e)
        {
            if (!File.Exists(tbMonth.Text))
            {
                tbExtractLog.AppendText("\r\nselect valid monthly EDA-Report!\r\n");
                return;
            }
            if (!File.Exists(tbYear.Text))
            {
                tbExtractLog.AppendText("\r\nselect valid yearly report!\r\n");
                return;
            }

            AnalyzeAndMerge(tbExtractLog, tbMonth.Text, tbYear.Text);
        }



        private bool AnalyzeAndMerge(TextBox log, string f_month, string f_year)
        {
            if(f_month == null)
                return false;


            log.Clear();
            log.Refresh();
            log.AppendText("analyzing monthly-report: " + f_month + "\r\n");

            // get the monthly overview (one row per Zählpunkt, "Detailübersicht" sheet)
            MonthlyReport mon_rep = ExcelReport_EPPlus.GetMonthlyData(log, f_month);
            if (mon_rep == null)
            {
                log.AppendText("error getting monthly overview!\r\n");
                return false;
            }

            // now check if the report data is marked complete enough to be merged
            bool report_valid = IsMonthlyReportValid(mon_rep, log);

            if (f_year != null)
            {
                if (!report_valid)
                {
                    log.AppendText("monthly-report contains incomplete data ('Vollständig' fehlt) -> Übernahme in Jahresreport abgebrochen!\r\n");
                    return false;
                }

                log.AppendText("filling data to yearly-report\r\n");
                return FillDataToYearlyReport(log, mon_rep, f_year);
            }

            log.AppendText("finished!\r\n");
            return report_valid;
        }


        private bool IsMonthlyReportValid(MonthlyReport mon_rep, TextBox log)
        {
            bool report_valid = true;

            if ((mon_rep.NumConsumers + mon_rep.NumProducers) == 0)
            {
                log.AppendText("keine Zählpunkte im Monatsreport gefunden!\r\n");
                return false;
            }

            // copying into the yearly report is only allowed once every Zählpunkt is marked "Vollständig"
            foreach (var user in mon_rep.User)
            {
                if (!user.IsComplete)
                {
                    log.AppendText(user.PM_ID + ": Datenübermittlung ist nicht 'Vollständig'!\r\n");
                    report_valid = false;
                }
                else if (user.DataQuality != "L1")
                {
                    log.AppendText(user.PM_ID + ": Datenqualität ist '" + user.DataQuality + "' (nicht L1)!\r\n");
                }
            }

            return report_valid;
        }


        private bool FillDataToYearlyReport(TextBox log, MonthlyReport mon_rep, string f_year)
        {
            log.AppendText("filling data to yearly-report: " + f_year + "\r\n");

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(new FileInfo(f_year)))
            {
                int row = 0;
                ExcelWorksheet ws = package.Workbook.Worksheets[0];

                // check if in cell[6,2] we can find the beginning of the list
                string check_zp = ws.Cells[6, 2].Value.ToString();
                if (check_zp != "Zählpunkt")
                {
                    log.AppendText("could not find beginning of list in yearly-report!\r\n");
                    return false;
                }

                int row_offset = 7;     // where the PM-ID's start

                // Find the next row starting from row_offset where column=2 has a null/empty value
                int nextEmptyRow = -1;
                int maxRow = ws.Dimension?.End?.Row ?? (row_offset + mon_rep.User.Count + mon_rep.NumProducers + 50); // fallback
                for (int r = row_offset; r <= maxRow; r++)
                {
                    var cell = ws.Cells[r, 2];
                    // consider cell missing, Value == null or empty text as "empty"
                    if (cell == null || cell.Value == null || string.IsNullOrWhiteSpace(cell.Text))
                    {
                        nextEmptyRow = r;
                        break;
                    }
                }

                if (nextEmptyRow < 0)
                {
                    log.AppendText("could not find next empty PM-ID cell in yearly-report starting from row " + row_offset + "!\r\n");
                    return false;
                }

                // now lets find the correct entry
                for (row = row_offset; row < nextEmptyRow; row++)
                {
                    try
                    {
                        // get PM-ID and direction from yearly-report (to verify)
                        string year_dir = ws.Cells[row, 3].Value.ToString();
                        string year_pm = ws.Cells[row, 2].Value.ToString();

                        int usr_idx = mon_rep.User.FindIndex(md => md.PM_ID == year_pm);
                        if (usr_idx < 0)
                        {
                            log.AppendText("could not find " + year_pm + " in monthly report!\r\n");
                            ws.Cells[row, 5 + mon_rep.MonthOfYear].Value = 0;
                            continue;
                        }


                        if (year_dir != mon_rep.User[usr_idx].Type)
                        {
                            ws.Cells[row, 5 + mon_rep.MonthOfYear].Value = 0;
                            continue;
                        }

                        if (year_pm != mon_rep.User[usr_idx].PM_ID)
                        {
                            ws.Cells[row, 5 + mon_rep.MonthOfYear].Value = 0;
                            continue;
                        }

                        if (year_dir == "CONSUMPTION")
                            ws.Cells[row, 5 + mon_rep.MonthOfYear].Value = mon_rep.User[usr_idx].FromEEG_Consumed_kWh;
                        if (year_dir == "GENERATION")
                            ws.Cells[row, 5 + mon_rep.MonthOfYear].Value = mon_rep.User[usr_idx].ToEEG_kWh * -1;
                    }
                    catch (Exception ex)
                    {
                        log.AppendText(ex.ToString());
                        return false;
                    }
                }

                // now fill the "TO-GRID-BLOCK"
                for (row = nextEmptyRow + 1; row <= (nextEmptyRow + mon_rep.NumProducers); row++)
                {
                    string year_dir = ws.Cells[row, 3].Value.ToString();
                    string year_pm = ws.Cells[row, 2].Value.ToString();
                    int usr_idx = mon_rep.User.FindIndex(md => md.PM_ID == year_pm);

                    if (usr_idx < 0)
                    {
                        log.AppendText("could not find " + year_pm + "in monthly report!\r\n");
                        ws.Cells[row, 5 + mon_rep.MonthOfYear].Value = 0;
                        continue;
                    }

                    if (year_pm != mon_rep.User[usr_idx].PM_ID)
                    {
                        ws.Cells[row, 5 + mon_rep.MonthOfYear].Value = 0;
                        continue;
                    }

                    ws.Cells[row, 5 + mon_rep.MonthOfYear].Value = mon_rep.User[usr_idx].ToGrid_kWh;
                }

                package.Save();

                log.AppendText("written to yearly report!");
            }
            return true;
        }
    }
}
