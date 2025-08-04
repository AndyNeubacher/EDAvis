using System;
using EDAvis.Tools;
using System.Windows.Forms;
using OfficeOpenXml;
using System.IO;

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

        private void btnExtractAndEdit_Click(object sender, EventArgs e)
        {
            MonthlyReport mon_rep = ExcelReport_EPPlus.GetMonthlyData(tbMonth.Text);
            if (mon_rep == null)
            {
                MessageBox.Show("error opening the monthly-report file!");
                return;
            }

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(new FileInfo(tbYear.Text)))
            {
                int row = 0;
                ExcelWorksheet ws = package.Workbook.Worksheets[0];

                // check if in cell[6,2] we can find the beginning of the list
                string check_zp = ws.Cells[6, 2].Value.ToString();
                if (check_zp != "Zählpunkt")
                    return;

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
                        if(usr_idx < 0)
                        {
                            MessageBox.Show("could not find " + year_pm + "in monthly report!");
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
                        MessageBox.Show("found new MeterPoint in montly report! -> add new line in yearly reportfile");
                        return;
                    }
                }

                // now fill the "TO-GRID-BLOCK"
                row_offset += mon_rep.User.Count + 1;
                for(row = row_offset; row < mon_rep.NumProducers + row_offset; row++)
                {
                    string year_dir = ws.Cells[row, 3].Value.ToString();
                    string year_pm = ws.Cells[row, 2].Value.ToString();
                    int usr_idx = mon_rep.User.FindIndex(md => md.PM_ID == year_pm);

                    if (year_pm != mon_rep.User[usr_idx].PM_ID)
                        continue;

                    ws.Cells[row, 5 + mon_rep.MonthOfYear].Value = mon_rep.User[usr_idx].ToGrid_kWh;
                }

                package.Save();

                MessageBox.Show("done!");
            }
        }
    }
}
