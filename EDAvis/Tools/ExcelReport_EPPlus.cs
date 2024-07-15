using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;


namespace EDAvis.Tools
{
    public class ExcelReport_EPPlus
    {
        public static UserNamesAndDataPoints GetData(string xls_file)
        {
            UserNamesAndDataPoints result = new UserNamesAndDataPoints();
            result.Data = new List<PowerMeter>();


            if (!File.Exists(xls_file))
                return null;


            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(new FileInfo(xls_file)))
            {
                ExcelWorksheet worksheetOverview = package.Workbook.Worksheets[0];
                ExcelWorksheet worksheetData = package.Workbook.Worksheets[1];

                Get_User_IDs(worksheetOverview, ref result);
                Get_Total_DataPoints(worksheetData, ref result);
                Get_PowerMeter_DataSeries(worksheetData, ref result);
            }



            return result;
        }

        private static PowerMeter CreateEmptyPowerMeter()
        {
            PowerMeter pm = new PowerMeter();
            pm.Series = new DataSeries();
            pm.User = new UserName();
            return pm;
        }

        private static void Get_User_IDs(ExcelWorksheet xlsSheet, ref UserNamesAndDataPoints usr)
        {
            if (xlsSheet == null)
                return;

            // at index 0 we add the TOTAL datapoints
            usr.Data.Add(CreateEmptyPowerMeter());
            usr.Data[0].User.Name = "Alle Teilnehmer";
            usr.Data[0].PM_ID = "";
            usr.Data[0].Type = "GESAMT";
            usr.Data[usr.Data.Count - 1].DataQuality = xlsSheet.Cells[3, 16].Value.ToString();

            int r = xlsSheet.Dimension.Rows;

            // now lets find the PowerMeter ID's
            for (int row = 8; row <= xlsSheet.Dimension.Rows; row++)
            {
                usr.Data.Add(CreateEmptyPowerMeter());
                usr.Data[usr.Data.Count - 1].Type = xlsSheet.Cells[row, 2].Value.ToString();
                usr.Data[usr.Data.Count - 1].PM_ID = xlsSheet.Cells[row, 1].Value.ToString();
                usr.Data[usr.Data.Count - 1].DataQuality = xlsSheet.Cells[row, 16].Value.ToString();
            }
        }

        private static void Get_Total_DataPoints(ExcelWorksheet xlsSheet, ref UserNamesAndDataPoints usr)
        {
            try
            {
                int last_col = xlsSheet.Dimension.Columns;
                int last_row = xlsSheet.Dimension.Rows;

                // get TimeStamp for all datapoints
                var rng_dt = xlsSheet.Cells[17,1,last_row,1];
                usr.Timestamps = RangeToDateTimeList(rng_dt);

                // get ConsumptionTotal_kWh
                var tot_con = xlsSheet.Cells[17, last_col - 8, last_row, last_col - 8];
                usr.Data[0].Series.Consumed_Total_kWh = RangeToDataPointClass(tot_con);

                // get ToEEG_kWh
                var to_eeg = xlsSheet.Cells[17, last_col - 5, last_row, last_col - 5];
                usr.Data[0].Series.FromEEG_Consumed_kWh = RangeToDataPointClass(to_eeg);

                // get ProducedTotal_kWh
                var tot_gen = xlsSheet.Cells[17, last_col - 3, last_row, last_col - 3];
                usr.Data[0].Series.Produced_Total_kWh = RangeToDataPointClass(tot_gen);

                // get ToGrid_KWh
                var to_grid = xlsSheet.Cells[17, last_col, last_row, last_col];
                usr.Data[0].Series.ToGrid_kWh = RangeToDataPointClass(to_grid);
            }
            catch (Exception ex) { MessageBox.Show("Get_Total_DataPoints: --> " + ex.ToString()); }
        }

        private static void Get_PowerMeter_DataSeries(ExcelWorksheet xlsSheet, ref UserNamesAndDataPoints usr)
        {
            try
            {
                for (int col = 2; col <= xlsSheet.Dimension.Columns; )
                {
                    // get correct index of already read userdata
                    var last_row = xlsSheet.Dimension.Rows;
                    int list_idx = usr.Data.FindIndex(r => r.PM_ID == xlsSheet.Cells[2, col].Value.ToString());
                    if (list_idx > -1)
                    {
                        if (usr.Data[list_idx].Type == "CONSUMPTION")
                        {
                            // get owner of power-meter
                            usr.Data[list_idx].User.Name = xlsSheet.Cells[3, col].Value.ToString();

                            // get UsedTotal_kWh data
                            var rng_total = xlsSheet.Cells[17, col, last_row, col];
                            usr.Data[list_idx].Series.Consumed_Total_kWh = RangeToDataPointClass(rng_total);

                            // get FromEEG_MaxAvaliable_kWh data
                            var rng_avaliable = xlsSheet.Cells[17, col + 4, last_row, col + 4];
                            usr.Data[list_idx].Series.FromEEG_MaxAvaliable_kWh = RangeToDataPointClass(rng_avaliable);

                            // get PowerFromEEG data
                            var rng_eeg = xlsSheet.Cells[17, col + 6, last_row, col + 6];
                            usr.Data[list_idx].Series.FromEEG_Consumed_kWh = RangeToDataPointClass(rng_eeg);

                            col += 10;
                        }
                        else if (usr.Data[list_idx].Type == "GENERATION")
                        {
                            // get owner of power-meter
                            usr.Data[list_idx].User.Name = xlsSheet.Cells[3, col].Value.ToString();

                            // get Produced_kWh data
                            var gen_total = xlsSheet.Cells[17, col, last_row, col];
                            usr.Data[list_idx].Series.Produced_Total_kWh = RangeToDataPointClass(gen_total);

                            // get ToGrid_kWh data
                            var gen_grid = xlsSheet.Cells[17, col + 6, last_row, col + 6];
                            usr.Data[list_idx].Series.ToGrid_kWh = RangeToDataPointClass(gen_grid);

                            // calc ToEEG_kWh and fill list
                            usr.Data[list_idx].Series.ToEEG_kWh = new DataPoints();
                            usr.Data[list_idx].Series.ToEEG_kWh.Points = new List<double>();
                            usr.Data[list_idx].Series.ToEEG_kWh.Visible = false;
                            for (int i = 0; i < usr.Data[list_idx].Series.Produced_Total_kWh.Points.Count; i++)
                                usr.Data[list_idx].Series.ToEEG_kWh.Points.Add(usr.Data[list_idx].Series.Produced_Total_kWh.Points[i] - usr.Data[list_idx].Series.ToGrid_kWh.Points[i]);

                            col += 8;
                        }
                    }
                    else
                        return;
                }
            }
            catch (Exception ex) { MessageBox.Show("Get_Consumer_DataPoints: --> " + ex.ToString()); }
        }

        private static List<DateTime> RangeToDateTimeList(ExcelRange range)
        {
            try
            {
                List<DateTime> dt = new List<DateTime>();
                foreach (var cell in range)
                {
                    if (cell.Value != null)
                        dt.Add(DateTime.FromOADate((double)cell.Value));
                    else
                        return null;
                }
                return dt;
            }
            catch { return null; }
        }

        private static DataPoints RangeToDataPointClass(ExcelRange range)
        {
            try
            {
                DataPoints ds = new DataPoints();
                ds.Points = new List<double>();

                foreach (var cell in range)
                {
                    if (cell.Value != null)
                        ds.Points.Add((double)cell.Value);
                    else
                        return null;
                }

                ds.Visible = false;
                return ds;
            }
            catch { return null; }
        }
    }
}