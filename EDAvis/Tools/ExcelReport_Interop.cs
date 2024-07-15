using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;



namespace EDAvis.Tools
{
    public class ExcelReport_Interop
    {
        public static UserNamesAndDataPoints GetData(string xls_file)
        {
            UserNamesAndDataPoints result = new UserNamesAndDataPoints();
            result.Data = new List<PowerMeter>();


            if (!File.Exists(xls_file))
                return null;

            // create an instance of an excel-application and open given file
            Microsoft.Office.Interop.Excel.Application xlApp = null;
            Microsoft.Office.Interop.Excel.Workbooks xlWorkBooks = null;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook = null;

            try
            {
                UserNamesAndDataPoints data = new UserNamesAndDataPoints();
                xlApp = new Microsoft.Office.Interop.Excel.Application();
                xlApp.ScreenUpdating = false;

                xlWorkBooks = xlApp.Workbooks;
                xlWorkBook = xlWorkBooks.Open(xls_file);

                // get all raw-data from xls
                Get_User_IDs(xlWorkBook.Worksheets[1], ref result);
                Get_Total_DataPoints(xlWorkBook.Worksheets[2], ref result);
                Get_PowerMeter_DataSeries(xlWorkBook.Worksheets[2], ref result);

                xlWorkBook.Close();
                xlApp.Quit();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                result = null;
            }
            finally
            {
                if (xlWorkBook != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkBook);
                if (xlWorkBooks != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkBooks);
                if (xlApp != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);

                xlWorkBook = null;
                xlWorkBooks = null;
                xlApp = null;
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

        private static void Get_User_IDs(Microsoft.Office.Interop.Excel.Worksheet xlsSheet, ref UserNamesAndDataPoints usr)
        {
            if (xlsSheet == null)
                return;

            // at index 0 we add the TOTAL datapoints
            usr.Data.Add(CreateEmptyPowerMeter());
            usr.Data[0].User.Name = "Alle Teilnehmer";
            usr.Data[0].PM_ID = "";
            usr.Data[0].Type = "GESAMT";
            usr.Data[usr.Data.Count - 1].DataQuality = xlsSheet.Cells[3, 16].Value;

            // now lets find the PowerMeter ID's
            for (int row = 8; row <= xlsSheet.UsedRange.Rows.Count; row++)
            {
                usr.Data.Add(CreateEmptyPowerMeter());
                usr.Data[usr.Data.Count - 1].Type = xlsSheet.Cells[row, 2].Value;
                usr.Data[usr.Data.Count - 1].PM_ID = xlsSheet.Cells[row, 1].Value;
                usr.Data[usr.Data.Count - 1].DataQuality = xlsSheet.Cells[row, 16].Value;
            }

            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlsSheet);
        }

        private static void Get_PowerMeter_DataSeries(Microsoft.Office.Interop.Excel.Worksheet xlsSheet, ref UserNamesAndDataPoints usr)
        {
            try
            {
                for (int col = 2; col <= xlsSheet.UsedRange.Columns.Count; col += 10)
                {
                    // get correct index of already read userdata
                    var last_row = xlsSheet.UsedRange.Rows.Count;
                    int list_idx = usr.Data.FindIndex(r => r.PM_ID == xlsSheet.Cells[2, col].Value);
                    if (list_idx > -1)
                    {
                        if (usr.Data[list_idx].Type == "CONSUMPTION")
                        {
                            // get owner of power-meter
                            usr.Data[list_idx].User.Name = xlsSheet.Cells[3, col].Value;

                            // get UsedTotal_kWh data
                            object[,] rng_total = xlsSheet.Range[xlsSheet.Cells[17, col], xlsSheet.Cells[last_row, col]].Cells.Value2;
                            usr.Data[list_idx].Series.Consumed_Total_kWh = RangeToDataPointClass(rng_total);

                            // get FromEEG_MaxAvaliable_kWh data
                            object[,] rng_avaliable = xlsSheet.Range[xlsSheet.Cells[17, col + 4], xlsSheet.Cells[last_row, col + 4]].Cells.Value2;
                            usr.Data[list_idx].Series.FromEEG_MaxAvaliable_kWh = RangeToDataPointClass(rng_avaliable);

                            // get PowerFromEEG data
                            object[,] rng_eeg = xlsSheet.Range[xlsSheet.Cells[17, col + 6], xlsSheet.Cells[last_row, col + 6]].Cells.Value2;
                            usr.Data[list_idx].Series.FromEEG_Consumed_kWh = RangeToDataPointClass(rng_eeg);
                        }
                        else
                        {
                            // get owner of power-meter
                            usr.Data[list_idx].User.Name = xlsSheet.Cells[3, col].Value;

                            // get Produced_kWh data
                            object[,] gen_total = xlsSheet.Range[xlsSheet.Cells[17, col], xlsSheet.Cells[last_row, col]].Cells.Value2;
                            usr.Data[list_idx].Series.Produced_Total_kWh = RangeToDataPointClass(gen_total);

                            // get ToGrid_kWh data
                            object[,] gen_grid = xlsSheet.Range[xlsSheet.Cells[17, col + 6], xlsSheet.Cells[last_row, col + 6]].Cells.Value2;
                            usr.Data[list_idx].Series.ToGrid_kWh = RangeToDataPointClass(gen_grid);

                            // calc ToEEG_kWh and fill list
                            usr.Data[list_idx].Series.ToEEG_kWh = new DataPoints();
                            usr.Data[list_idx].Series.ToEEG_kWh.Points = new List<double>();
                            usr.Data[list_idx].Series.ToEEG_kWh.Visible = false;
                            for (int i = 0; i < usr.Data[list_idx].Series.Produced_Total_kWh.Points.Count; i++)
                                usr.Data[list_idx].Series.ToEEG_kWh.Points.Add(usr.Data[list_idx].Series.Produced_Total_kWh.Points[i] - usr.Data[list_idx].Series.ToGrid_kWh.Points[i]);
                        }
                    }
                }
            }
            catch(Exception ex) { MessageBox.Show("Get_Consumer_DataPoints: --> " + ex.ToString()); }
        }

        private static void Get_Total_DataPoints(Microsoft.Office.Interop.Excel.Worksheet xlsSheet, ref UserNamesAndDataPoints usr)
        {
            try
            {
                int last_col = xlsSheet.UsedRange.Columns.Count;
                int last_row = xlsSheet.UsedRange.Rows.Count;

                // get TimeStamp for all datapoints
                object[,] rng_dt = xlsSheet.Range[xlsSheet.Cells[17, 1], xlsSheet.Cells[last_row, 1]].Cells.Value;
                usr.Timestamps = RangeToDateTimeList(rng_dt);

                // get ConsumptionTotal_kWh
                object[,] tot_con = xlsSheet.Range[xlsSheet.Cells[17, last_col - 8], xlsSheet.Cells[last_row, last_col - 8]].Cells.Value2;
                usr.Data[0].Series.Consumed_Total_kWh = RangeToDataPointClass(tot_con);

                // get ToEEG_kWh
                object[,] to_eeg = xlsSheet.Range[xlsSheet.Cells[17, last_col - 5], xlsSheet.Cells[last_row, last_col - 5]].Cells.Value2;
                usr.Data[0].Series.FromEEG_Consumed_kWh = RangeToDataPointClass(to_eeg);

                // get ProducedTotal_kWh
                object[,] tot_gen = xlsSheet.Range[xlsSheet.Cells[17, last_col - 3], xlsSheet.Cells[last_row, last_col - 3]].Cells.Value2;
                usr.Data[0].Series.Produced_Total_kWh = RangeToDataPointClass(tot_gen);

                // get ToGrid_KWh
                object[,] to_grid = xlsSheet.Range[xlsSheet.Cells[17, last_col], xlsSheet.Cells[last_row, last_col]].Cells.Value2;
                usr.Data[0].Series.ToGrid_kWh = RangeToDataPointClass(to_grid);
            }
            catch (Exception ex) { MessageBox.Show("Get_Total_DataPoints: --> " + ex.ToString()); }
        }


        private static DataPoints RangeToDataPointClass(object[,] range)
        {
            try
            {
                DataPoints ds = new DataPoints();
                ds.Points = new List<double>();
                ds.Points = range.Cast<object>().ToList().ConvertAll(x => Convert.ToDouble(x));
                ds.Visible = false;
                return ds;
            }
            catch { return null; }
        }

        private static List<DateTime> RangeToDateTimeList(object[,] range)
        {
            try
            {
                List<DateTime> dt = new List<DateTime>();
                dt = range.Cast<object>().ToList().ConvertAll(x => Convert.ToDateTime(x));
                return dt;
            }
            catch { return null; }
        }
    }
}
