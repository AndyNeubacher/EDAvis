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

        private void btnCopyMonthlyReport_TO_YearlyReport_Click(object sender, EventArgs e)
        {
            if (tbMonth.Text == "")
            {
                tbExtractLog.AppendText("\r\nselect monthly EDA-Report!\r\n");
                return;
            }
            if (tbYear.Text == "")
            {
                tbExtractLog.AppendText("\r\nselect yearly report!\r\n");
                return;
            }

            tbExtractLog.Clear();

            // first get all detailed PM-data from monthly-report
            UserNamesAndDataPoints pm_data = ExcelReport_EPPlus.GetData(tbMonth.Text);
            if (pm_data == null)
            {
                tbExtractLog.AppendText("error getting all PM-data from monthly-report!\r\n");
                return;
            }

            // now get the monthly overview
            MonthlyReport mon_rep = ExcelReport_EPPlus.GetMonthlyData(tbExtractLog, tbMonth.Text);
            if (mon_rep == null)
            {
                tbExtractLog.AppendText("error getting monthly overview!\r\n");
                return;
            }

            // now check if we are missing data in the monthly-report
            bool ok = CheckForMissingData(pm_data, mon_rep, tbExtractLog);

            if (ok == true)
            {
                DialogResult result = MessageBox.Show("missing data in EDA-Report! ... want to fill data to yearly-report anyway?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (result == DialogResult.Yes)
                    FillDataToYearlyReport(tbExtractLog, mon_rep);
            }
        }


        private void FillDataToYearlyReport(TextBox log, MonthlyReport mon_rep)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(new FileInfo(tbYear.Text)))
            {
                int row = 0;
                ExcelWorksheet ws = package.Workbook.Worksheets[0];

                // check if in cell[6,2] we can find the beginning of the list
                string check_zp = ws.Cells[6, 2].Value.ToString();
                if (check_zp != "Zählpunkt")
                {
                    log.AppendText("could not find beginning of list in yearly-report!\r\n");
                    return;
                }

                int row_offset = 7;     // where the PM-ID's start

                // now lets find the correct entry
                for (row = row_offset; row < mon_rep.User.Count + row_offset; row++)
                {
                    try
                    {
                        // get PM-ID and direction from yearly-report (to verify)
                        string year_dir = ws.Cells[row, 3].Value.ToString();
                        string year_pm = ws.Cells[row, 2].Value.ToString();

                        int usr_idx = mon_rep.User.FindIndex(md => md.PM_ID == year_pm);
                        if (usr_idx < 0)
                        {
                            log.AppendText("could not find " + year_pm + "in monthly report!\r\n");
                            continue;
                        }


                        if (year_dir != mon_rep.User[usr_idx].Type)
                            continue;

                        if (year_pm != mon_rep.User[usr_idx].PM_ID)
                            continue;

                        if (year_dir == "CONSUMPTION")
                            ws.Cells[row, 5 + mon_rep.MonthOfYear].Value = mon_rep.User[usr_idx].FromEEG_Consumed_kWh;
                        if (year_dir == "GENERATION")
                            ws.Cells[row, 5 + mon_rep.MonthOfYear].Value = mon_rep.User[usr_idx].ToEEG_kWh * -1;
                    }
                    catch (Exception ex)
                    {
                        log.AppendText(ex.ToString());
                        return;
                    }
                }

                // now fill the "TO-GRID-BLOCK"
                row_offset += mon_rep.User.Count + 1;
                for (row = row_offset; row < mon_rep.NumProducers + row_offset; row++)
                {
                    string year_dir = ws.Cells[row, 3].Value.ToString();
                    string year_pm = ws.Cells[row, 2].Value.ToString();
                    int usr_idx = mon_rep.User.FindIndex(md => md.PM_ID == year_pm);

                    if (year_pm != mon_rep.User[usr_idx].PM_ID)
                        continue;

                    ws.Cells[row, 5 + mon_rep.MonthOfYear].Value = mon_rep.User[usr_idx].ToGrid_kWh;
                }

                package.Save();

                log.AppendText("written to yearly report!");
            }
        }



        private bool CheckForMissingData(UserNamesAndDataPoints pm_data, MonthlyReport mon_rep, TextBox log)
        {
            bool missing_data = false;

            // first check if we have the same number of PM-ID's  (idx0 = TOTAL and not a real/valid PM)
            if (pm_data.Data.Count != (mon_rep.User.Count + 1))
            {
                log.AppendText("number of PM-ID's in monthly-report does not match!\r\n");
                missing_data = true;
            }


            // now check if all PM-ID's are present
            foreach (var pm in pm_data.Data)
            {
                if (pm.Type == "GESAMT")
                    continue;

                // find the index of this PM-ID in the monthly-report
                int idx = mon_rep.User.FindIndex(md => md.PM_ID == pm.PM_ID);
                if (idx < 0)
                {
                    log.AppendText(pm.PM_ID + " (" + pm.User.Name + ") " + " is missing in monthly-report!\r\n");
                    missing_data = true;
                    continue;
                }

                // check if the powermeter is/was active in the reported month
                if ((mon_rep.ReportStartDate >= pm.Series.PM_Active.End) || (mon_rep.ReportEndDate <= pm.Series.PM_Active.Start))
                {
                    log.AppendText(pm.PM_ID + " (" + pm.User.Name + ") " + " was not active in the monthly report!\r\n");
                    continue;
                }

                // check if the powermeter has missing datapoints in the reported month
                int data_row_start = pm_data.Timestamps.FindIndex(ts => ts >= pm.Series.PM_DataPeriod.Start);
                int data_row_end = pm_data.Timestamps.FindIndex(ts => ts >= pm.Series.PM_DataPeriod.End);
                int points_missing = 0;

                // check if we miss some data-points
                for (int i = data_row_start; i < data_row_end; i++)
                {
                    if ((pm.Type == "CONSUMPTION") && (pm.Series.FromEEG_Consumed_kWh.Points[i] == null))
                        points_missing++;
                    if ((pm.Type == "GENERATION") && (pm.Series.ToEEG_kWh.Points[i] == null))
                        points_missing++;
                }

                // make log -entry if we miss some data-points
                if (points_missing > 0)
                {
                    missing_data = true;
                    log.AppendText(pm.PM_ID + " (" + pm.User.Name + ") " + ": missing " + points_missing + " datapoints!\r\n");
                }
            }

            return missing_data;
        }
    }
}
