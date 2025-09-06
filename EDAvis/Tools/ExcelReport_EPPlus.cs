using BrightIdeasSoftware;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public static MonthlyReport GetMonthlyData(TextBox log, string xls_file)
        {
            MonthlyReport report = new MonthlyReport();
            report.User = new List<MonthlyData>();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(new FileInfo(xls_file)))
            {
                ExcelWorksheet wsOverview = package.Workbook.Worksheets[0];
                ExcelWorksheet wsDataPoints = package.Workbook.Worksheets[1];

                Get_OverviewData(log, wsOverview, ref report);
            }

            return report;
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
            usr.Data[usr.Data.Count - 1].DataQuality = xlsSheet.Cells[3, 16].Value.ToString();              // TOTAL data quality

            int r = xlsSheet.DimensionByValue.Rows;
            int last_row = xlsSheet.DimensionByValue.Rows;
            int start_row = RowIdxOfKeyword("Zählpunkt", xlsSheet.Cells[1, 1, last_row, 1]) + 1;

            // now lets find the PowerMeter ID's
            for (int row = start_row; row <= xlsSheet.DimensionByValue.Rows; row++)
            {
                usr.Data.Add(CreateEmptyPowerMeter());
                usr.Data[usr.Data.Count - 1].Type = xlsSheet.Cells[row, 2].Value.ToString();                // CONSUMER / PRODUCER
                usr.Data[usr.Data.Count - 1].PM_ID = xlsSheet.Cells[row, 1].Value.ToString();               // PowerMeter ID AT00300000000xxxxx
                usr.Data[usr.Data.Count - 1].DataQuality = ParseDataQuality(xlsSheet.Cells[row, 16].Value); // data quality of each powermeter
            }
        }

        private static void Get_Total_DataPoints(ExcelWorksheet xlsSheet, ref UserNamesAndDataPoints usr)
        {
            try
            {
                int last_col = xlsSheet.DimensionByValue.Columns;
                int last_row = xlsSheet.DimensionByValue.Rows;
                int start_row = RowIdxOfKeyword("Data Completeness", xlsSheet.Cells[1, 1, last_row, 1]) + 2;

                // get TimeStamp for all datapoints
                var rng_dt = xlsSheet.Cells[start_row, 1,last_row,1];
                usr.Timestamps = RangeToDateTimeList(rng_dt);

                // get ConsumptionTotal_kWh
                var tot_con = xlsSheet.Cells[start_row, last_col - 8, last_row, last_col - 8];
                usr.Data[0].Series.Consumed_Total_kWh = RangeToDataPointClass(tot_con);

                // get ToEEG_kWh
                var to_eeg = xlsSheet.Cells[start_row, last_col - 5, last_row, last_col - 5];
                usr.Data[0].Series.FromEEG_Consumed_kWh = RangeToDataPointClass(to_eeg);

                // get ProducedTotal_kWh
                var tot_gen = xlsSheet.Cells[start_row, last_col - 3, last_row, last_col - 3];
                usr.Data[0].Series.Produced_Total_kWh = RangeToDataPointClass(tot_gen);

                // get ToGrid_KWh
                var to_grid = xlsSheet.Cells[start_row, last_col, last_row, last_col];
                usr.Data[0].Series.ToGrid_kWh = RangeToDataPointClass(to_grid);

                // consumed Total in EEG
                usr.Data[0].Series.ToEEG_kWh = new DataPoints();
                usr.Data[0].Series.ToEEG_kWh.Points = new List<double?>();
                usr.Data[0].Series.ToEEG_kWh.Visible = false;
                for (int i = 0; i < usr.Data[0].Series.Produced_Total_kWh.Points.Count; i++)
                    usr.Data[0].Series.ToEEG_kWh.Points.Add(usr.Data[0].Series.Produced_Total_kWh.Points[i] - usr.Data[0].Series.ToGrid_kWh.Points[i]);

            }
            catch (Exception ex) { MessageBox.Show("Get_Total_DataPoints: --> " + ex.ToString()); }
        }

        private static void Get_PowerMeter_DataSeries(ExcelWorksheet xlsSheet, ref UserNamesAndDataPoints usr)
        {
            try
            {
                int start_row = RowIdxOfKeyword("Data Completeness", xlsSheet.Cells[1, 1, xlsSheet.DimensionByValue.Rows, 1]) + 2;

                int dt_data_period_start_row = RowIdxOfKeyword("Data Period Start", xlsSheet.Cells[1, 1, 20, 1]);
                int dt_data_period_end_row = RowIdxOfKeyword("Data Period End", xlsSheet.Cells[1, 1, 20, 1]);
                int dt_mp_active_start_row = RowIdxOfKeyword("Metering Point Active Start", xlsSheet.Cells[1, 1, 20, 1]);
                int dt_mp_active_end_row = RowIdxOfKeyword("Metering Point Active End", xlsSheet.Cells[1, 1, 20, 1]);

                // if this value is 0 -> we have an old report version -> try to find with other keywords
                if (dt_data_period_start_row == 0)
                {
                    dt_data_period_start_row = RowIdxOfKeyword("Period start", xlsSheet.Cells[1, 1, 20, 1]);
                    dt_data_period_end_row = RowIdxOfKeyword("Period end", xlsSheet.Cells[1, 1, 20, 1]);
                    dt_mp_active_start_row = RowIdxOfKeyword("Metering Point active start", xlsSheet.Cells[1, 1, 20, 1]);
                    dt_mp_active_end_row = RowIdxOfKeyword("Metering Point active end", xlsSheet.Cells[1, 1, 20, 1]);
                }


                for (int col = 2; col <= xlsSheet.DimensionByValue.Columns; )
                {
                    // get correct index of already read userdata
                    var last_row = xlsSheet.DimensionByValue.Rows;

                    int pm_row = RowIdxOfKeyword("MeteringpointID", xlsSheet.Cells[1, 1, last_row, 1]);     // old-report
                    if(pm_row == 0)
                        pm_row = RowIdxOfKeyword("MeteringPointId", xlsSheet.Cells[1, 1, last_row, 1]);     // new-report
                    

                    int list_idx = usr.Data.FindIndex(r => r.PM_ID == xlsSheet.Cells[pm_row, col].Value.ToString());
                    if (list_idx > -1)
                    {
                        if (usr.Data[list_idx].Type == "CONSUMPTION")
                        {
                            // get owner of power-meter
                            int name_row = RowIdxOfKeyword("Name", xlsSheet.Cells[1, 1, last_row, 1]);
                            usr.Data[list_idx].User.Name = (xlsSheet.Cells[name_row, col].Value == null) ? "unknown" : xlsSheet.Cells[name_row, col].Value.ToString();

                            // get date where datapoints in the report should be present
                            usr.Data[list_idx].Series.PM_DataPeriod = new DateTimeStartEnd();
                            usr.Data[list_idx].Series.PM_DataPeriod.Start = ParseDateTime(xlsSheet.Cells[dt_data_period_start_row, col]);
                            usr.Data[list_idx].Series.PM_DataPeriod.End = ParseDateTime(xlsSheet.Cells[dt_data_period_end_row, col]);

                            // get date since when the powermeter is active in EEG
                            usr.Data[list_idx].Series.PM_Active = new DateTimeStartEnd();
                            usr.Data[list_idx].Series.PM_Active.Start = ParseDateTime(xlsSheet.Cells[dt_mp_active_start_row, col]);
                            usr.Data[list_idx].Series.PM_Active.End = ParseDateTime(xlsSheet.Cells[dt_mp_active_end_row, col]);

                            // get UsedTotal_kWh data
                            var rng_total = xlsSheet.Cells[start_row, col, last_row, col];
                            usr.Data[list_idx].Series.Consumed_Total_kWh = RangeToDataPointClass(rng_total);

                            // get FromEEG_MaxAvaliable_kWh data
                            var rng_avaliable = xlsSheet.Cells[start_row, col + 4, last_row, col + 4];
                            usr.Data[list_idx].Series.FromEEG_MaxAvaliable_kWh = RangeToDataPointClass(rng_avaliable);

                            // get PowerFromEEG data
                            var rng_eeg = xlsSheet.Cells[start_row, col + 6, last_row, col + 6];
                            usr.Data[list_idx].Series.FromEEG_Consumed_kWh = RangeToDataPointClass(rng_eeg);

                            col += 10;
                        }
                        else if (usr.Data[list_idx].Type == "GENERATION")
                        {
                            // get owner of power-meter
                            int name_row = RowIdxOfKeyword("Name", xlsSheet.Cells[1, 1, last_row, 1]);
                            usr.Data[list_idx].User.Name = (xlsSheet.Cells[name_row, col].Value == null) ? "unknown" : xlsSheet.Cells[name_row, col].Value.ToString();

                            // get date where datapoints in the report should be present
                            usr.Data[list_idx].Series.PM_DataPeriod = new DateTimeStartEnd();
                            usr.Data[list_idx].Series.PM_DataPeriod.Start = ParseDateTime(xlsSheet.Cells[dt_data_period_start_row, col]);
                            usr.Data[list_idx].Series.PM_DataPeriod.End = ParseDateTime(xlsSheet.Cells[dt_data_period_end_row, col]);

                            // get date since when the powermeter is active in EEG
                            usr.Data[list_idx].Series.PM_Active = new DateTimeStartEnd();
                            usr.Data[list_idx].Series.PM_Active.Start = ParseDateTime(xlsSheet.Cells[dt_mp_active_start_row, col]);
                            usr.Data[list_idx].Series.PM_Active.End = ParseDateTime(xlsSheet.Cells[dt_mp_active_end_row, col]);

                            // get Produced_kWh data
                            var gen_total = xlsSheet.Cells[start_row, col, last_row, col];
                            usr.Data[list_idx].Series.Produced_Total_kWh = RangeToDataPointClass(gen_total);

                            // get ToGrid_kWh data
                            var gen_grid = xlsSheet.Cells[start_row, col + 6, last_row, col + 6];
                            usr.Data[list_idx].Series.ToGrid_kWh = RangeToDataPointClass(gen_grid);

                            // calc ToEEG_kWh and fill list
                            usr.Data[list_idx].Series.ToEEG_kWh = new DataPoints();
                            usr.Data[list_idx].Series.ToEEG_kWh.Points = new List<double?>();
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

        private static void Get_OverviewData(TextBox log, ExcelWorksheet xlsSheet, ref MonthlyReport rep)
        {
            try
            {
                rep.ReportStartDate = ParseDateTime(xlsSheet.Cells[3, 4]);
                rep.ReportEndDate = ParseDateTime(xlsSheet.Cells[3, 5]);


                // check the timespan -> must be one month!
                if ((rep.ReportEndDate.Hour == 23) && (rep.ReportEndDate.Minute == 45))
                    rep.ReportEndDate = rep.ReportEndDate.AddMinutes(15);

                if (rep.ReportStartDate.AddMonths(1) != rep.ReportEndDate)
                    log.AppendText("Get_OverviewData: timespan of the EDA report is not one month!\r\n");


                // all datapoints needs to be "L1" quality -> otherwise it makes no sense!
                string total_data_quality = ParseDataQuality(xlsSheet.Cells[3, 16].Value);
                if (total_data_quality == null) {
                    rep = null;
                    return;
                }
                    
                if (total_data_quality != "L1") {
                    rep = null;
                    return;
                }


                int last_row = xlsSheet.DimensionByValue.Rows;
                int start_row = RowIdxOfKeyword("Zählpunkt", xlsSheet.Cells[1, 1, last_row, 1]) + 1;

                rep.NumConsumers = 0;
                rep.NumProducers = 0;
                rep.MonthOfYear = GetMonthFromDate(xlsSheet.Cells[start_row, 4]);


                for (int row=start_row; row<=last_row; row++)
                {
                    MonthlyData data = new MonthlyData();

                    data.PM_ID = xlsSheet.Cells[row, 1].Value.ToString();
                    data.Type = xlsSheet.Cells[row, 2].Value.ToString();
                    if (data.Type == "GENERATION")
                        rep.NumProducers++;

                    if (data.Type == "CONSUMPTION")
                        rep.NumConsumers++;

                    data.Consumed_Total_kWh = (double)xlsSheet.Cells[row, 6].Value;
                    data.FromEEG_Consumed_kWh = (double)xlsSheet.Cells[row, 9].Value;
                    data.Produced_Total_kWh = (double)xlsSheet.Cells[row, 11].Value;
                    data.ToGrid_kWh = (double)xlsSheet.Cells[row, 14].Value;
                    data.ToEEG_kWh = data.Produced_Total_kWh - data.ToGrid_kWh;

                    rep.User.Add(data);
                }
            }
            catch (Exception ex)
            {
                log.AppendText("Get_OverviewData: --> " + ex.ToString());
                rep = null;
                return;
            }
        }


        public static int GetMonthFromDate(ExcelRangeBase cell)
        {
            DateTime tmp = DateTime.MinValue;
            List<DateTime> dt = new List<DateTime>();
            if (cell.Value != null)
            {
                string format_old_short = "dd.MM.yyyy HH:mm";
                string format_old = "dd.MM.yyyy HH:mm:ss";
                string format_new = "yyyy-MM-dd HH:mm:ss";
                if (!DateTime.TryParseExact(cell.Value.ToString(), format_old_short, CultureInfo.InvariantCulture, DateTimeStyles.None, out tmp))
                {
                    if (!DateTime.TryParseExact(cell.Value.ToString(), format_old, CultureInfo.InvariantCulture, DateTimeStyles.None, out tmp))
                    {
                        if (!DateTime.TryParseExact(cell.Value.ToString(), format_new, CultureInfo.InvariantCulture, DateTimeStyles.None, out tmp))
                            return -1;
                    }
                }
                return tmp.Month;
            }
            return -1;
        }

        public static DateTime ParseDateTime(ExcelRangeBase cell)
        {
            if (cell.Value == null)
                return DateTime.MinValue;

            string date_str = cell.Value.ToString();
            DateTime tmp = DateTime.MinValue;
            string format_old_short = "dd.MM.yyyy HH:mm";
            string format_old = "dd.MM.yyyy HH:mm:ss";
            string format_new = "yyyy-MM-dd HH:mm:ss";
            if (!DateTime.TryParseExact(date_str, format_old_short, CultureInfo.InvariantCulture, DateTimeStyles.None, out tmp))
            {
                if (!DateTime.TryParseExact(date_str, format_old, CultureInfo.InvariantCulture, DateTimeStyles.None, out tmp))
                {
                    if (!DateTime.TryParseExact(date_str, format_new, CultureInfo.InvariantCulture, DateTimeStyles.None, out tmp))
                        return DateTime.MinValue;
                }
            }
            return tmp;
        }


        private static string ParseDataQuality(object val)
        {
            if (val == null)
                return "invalid";
            else if (val.ToString().Contains("L1"))
                return "L1";
            else if (val.ToString().Contains("L2"))
                return "L2";
            else if (val.ToString().Contains("L3"))
                return "L3";
            else
                return "unknown";
        }

        private static List<DateTime> RangeToDateTimeList(ExcelRange range)
        {
            try
            {
                DateTime tmp = DateTime.MinValue;
                List<DateTime> dt = new List<DateTime>();
                foreach (var cell in range)
                {
                    if (cell.Value != null)
                    {
                        string format_old_short = "dd.MM.yyyy HH:mm";
                        string format_old = "dd.MM.yyyy HH:mm:ss";
                        string format_new = "yyyy-MM-dd HH:mm:ss";
                        if (!DateTime.TryParseExact(cell.Value.ToString(), format_old_short, CultureInfo.InvariantCulture, DateTimeStyles.None, out tmp))
                        {
                            if (!DateTime.TryParseExact(cell.Value.ToString(), format_old, CultureInfo.InvariantCulture, DateTimeStyles.None, out tmp))
                            {
                                if (!DateTime.TryParseExact(cell.Value.ToString(), format_new, CultureInfo.InvariantCulture, DateTimeStyles.None, out tmp))
                                    return null;
                            }
                        }
                        dt.Add(tmp);
                    }
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
                ds.Points = new List<double?>();

                // Get the dimensions of the range
                int startRow = range.Start.Row;
                int endRow = range.End.Row;
                int startCol = range.Start.Column;

                // Loop from the start row to the end row
                for (int i = startRow; i <= endRow; i++)
                {
                    // Get the value of the cell and add it to the list as a nullable double.
                    double? cellValue = range.Worksheet.Cells[i, startCol].GetValue<double?>();
                    ds.Points.Add(cellValue);
                }

                ds.Visible = false;
                return ds;
            }
            catch { return null; }
        }

        private static int RowIdxOfKeyword(string keyword, ExcelRange range)
        {
            int num_rows = range.Rows;
            try
            {
                for(int row=1; row< num_rows; row++)
                {
                    if (range[row, 1].Value == null)
                        continue;

                    string x = range[row, 1].Value.ToString();
                    if (range[row, 1].Value.ToString() == keyword)
                        return row;
                }
            } catch { }
            return 0;
        }
    }
}