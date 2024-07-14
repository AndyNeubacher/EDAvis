using OxyPlot.WindowsForms;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Legends;
using System;
using System.Collections.Generic;

namespace EDAvisu.Tools
{
    public class Plotter
    {
        private PlotView view = null;
        private PlotModel model = null;


        public Plotter(PlotView plotview)
        { 
            this.view = plotview;
            model = new PlotModel() { /*Title = "Diagnostic" */ };

            // add legend to graph
            model.Legends.Add(new Legend
            {
                LegendBackground = OxyColor.FromAColor(220, OxyColors.White),
                LegendBorder = OxyColors.Black,
                LegendBorderThickness = 1.0,
                LegendPlacement = LegendPlacement.Inside,
                LegendPosition = LegendPosition.TopCenter,
                LegendOrientation = LegendOrientation.Horizontal,
                LegendLineSpacing = 8,
                LegendMaxWidth = 2000,
                LegendFontSize = 10
            });

            // add custom mouse-controller (zoom/pan behaviour)
            PlotController myController = new PlotController();
            myController.BindMouseDown(OxyMouseButton.Left, PlotCommands.PanAt);
            myController.BindMouseDown(OxyMouseButton.Left, OxyModifierKeys.None, 2, PlotCommands.ResetAt);
            myController.BindMouseDown(OxyMouseButton.Left, OxyModifierKeys.Control, PlotCommands.ZoomRectangle);
            myController.BindMouseDown(OxyMouseButton.Right, PlotCommands.ZoomRectangle);
            myController.BindMouseEnter(PlotCommands.HoverPointsOnlyTrack);
            myController.UnbindMouseDown(OxyMouseButton.Middle);

            view.Controller = myController;
            view.Model = model;
        }

        private LinearAxis NewAxis(int idx, LineSeries line, AxisPosition pos)
        {
            LinearAxis linearAxis = new LinearAxis
            {
                Title = line.Title,
                Key = line.YAxisKey,
                Position = pos,

                AxislineColor = line.Color,
                TicklineColor = line.Color,
                TitleColor = line.Color,
                TextColor = line.Color,
                MajorGridlineColor = line.Color,

                MajorGridlineStyle = LineStyle.Dot,
                MinorGridlineStyle = LineStyle.None,
                MaximumPadding = 0,
                MinimumPadding = 0,

                PositionTier = idx,

                IsPanEnabled = true,
                IsAxisVisible = false,
                IsZoomEnabled = true
            };

            return linearAxis;
        }

        private bool AddLine(string legend, List<DateTime> time, DataPoints dp, DateTime from, DateTime to)
        {
            try
            {
                if (dp == null)
                    return false;

                if (!dp.Visible)
                    return false;

                if (time.Count != dp.Points.Count)
                    return false;

                //string legend_text = pm.Type.Substring(0, 3) + ": ";
                //legend_text += "AT**" + pm.PM_ID.Substring(pm.PM_ID.Length - 8) + ", " + pm.User.Name;

                LineSeries line = new LineSeries() { Title = legend, MarkerType = MarkerType.Circle, Color = OxyColors.Automatic };
                model.Series.Add(line);
                model.Axes.Add(NewAxis(model.Axes.Count, line, AxisPosition.Left));

                // now fill the datapoints
                for (int i = 0; i < dp.Points.Count; i++)
                {
                    if ((time[i] >= from) && (time[i] <= to))
                        line.Points.Add(new DataPoint(DateTimeAxis.ToDouble(time[i]), dp.Points[i]));
                }

                model.Axes[model.Axes.Count-1].Reset();

                return true;
            }
            catch { return false; };
        }

        public void ShowLineGraph(UserNamesAndDataPoints usr, DateTime from, DateTime to)
        {
            ClearData();
            model.Axes.Add(new DateTimeAxis() { Position = AxisPosition.Bottom });

            PowerMeter pm;

            // add each powermeter
            for (int meter = 0; meter < usr.Data.Count; meter++)
            {
                pm = usr.Data[meter];

                //legend_text = pm.Type.Substring(0, 3) + ": ";
                ////legend_text += "AT**" + pm.PM_ID.Substring(pm.PM_ID.Length - 8) + ", " + pm.User.Name;

                //LineSeries ls = new LineSeries() { Title = legend_text, MarkerType = MarkerType.Circle, Color = OxyColors.Automatic };
                //model.Series.Add(ls);
                //model.Axes.Add(NewAxis(meter + 1, ls, AxisPosition.Left));

                AddLine("CON-TOTAL", usr.Timestamps, pm.Series.Consumed_Total_kWh, from, to);
                AddLine("EEG-Max", usr.Timestamps, pm.Series.FromEEG_MaxAvaliable_kWh, from, to);
                AddLine("EEG-Verb", usr.Timestamps, pm.Series.FromEEG_Consumed_kWh, from, to);

                AddLine("PROD-TOTAL", usr.Timestamps, pm.Series.Produced_Total_kWh, from, to);
                AddLine("PROD-ToGRID, ", usr.Timestamps, pm.Series.ToGrid_kWh, from, to);
                AddLine("PROD-ToEEG", usr.Timestamps, pm.Series.ToEEG_kWh, from, to);

                //for (int dp = 0; dp < usr.Timestamps.Count; dp++)
                //{
                //    if ((usr.Timestamps[dp] >= from) && (usr.Timestamps[dp] <= to))
                //    {
                //        if((pm.Series.Consumed_Total_kWh != null) && (pm.Series.Consumed_Total_kWh.Visible))
                //            (model.Series[meter] as LineSeries).Points.Add(new DataPoint(DateTimeAxis.ToDouble(usr.Timestamps[dp]), pm.Points.FromEEG_Consumed_kWh[dp]));    // used from EEG
                //    }
                //}
                //model.Axes[meter].Reset();
            }

            // add TOTAL generation and consumption
            //int idx_con_tot = usr.PmData.Count;
            //int idx_gen_tot = usr.PmData.Count + 1;
            //int idx_to_grid = usr.PmData.Count + 2;
            //int idx_eeg = usr.PmData.Count + 3;
            //LineSeries con_tot = new LineSeries() { Title = "CON: TOTAL", MarkerType = MarkerType.Circle, Color = OxyColors.Automatic };
            //LineSeries gen_tot = new LineSeries() { Title = "GEN: TOTAL", MarkerType = MarkerType.Circle, Color = OxyColors.Automatic };
            //LineSeries gen_togrid = new LineSeries() { Title = "GEN: ToGrid", MarkerType = MarkerType.Circle, Color = OxyColors.Automatic };
            //LineSeries con_eeg = new LineSeries() { Title = "CON: FromEEG", MarkerType = MarkerType.Circle, Color = OxyColors.Automatic };
            //model.Series.Add(con_tot);
            //model.Series.Add(gen_tot);
            //model.Series.Add(gen_togrid);
            //model.Series.Add(con_eeg);
            //model.Axes.Add(NewAxis(idx_con_tot + 1, con_tot, AxisPosition.Left));
            //model.Axes.Add(NewAxis(idx_gen_tot + 2, gen_tot, AxisPosition.Left));
            //model.Axes.Add(NewAxis(idx_to_grid + 3, gen_togrid, AxisPosition.Left));
            //model.Axes.Add(NewAxis(idx_eeg + 4, con_eeg, AxisPosition.Left));


            //for (int dp = 0; dp < usr.Timestamps.Count; dp++)
            //{
            //    if ((usr.Timestamps[dp] >= from) && (usr.Timestamps[dp] <= to))
            //    {
            //        (model.Series[idx_con_tot] as LineSeries).Points.Add(new DataPoint(DateTimeAxis.ToDouble(usr.Timestamps[dp]), usr.ConsumptionTotal_kWh[dp]));
            //        (model.Series[idx_gen_tot] as LineSeries).Points.Add(new DataPoint(DateTimeAxis.ToDouble(usr.Timestamps[dp]), usr.ProducedTotal_kWh[dp]));
            //        (model.Series[idx_to_grid] as LineSeries).Points.Add(new DataPoint(DateTimeAxis.ToDouble(usr.Timestamps[dp]), usr.ToGrid_KWh[dp]));
            //        (model.Series[idx_eeg] as LineSeries).Points.Add(new DataPoint(DateTimeAxis.ToDouble(usr.Timestamps[dp]), usr.FromEEG_kWh[dp]));
            //    }
            //}

            //model.Axes[idx_con_tot].Reset();
            //model.Axes[idx_gen_tot].Reset();
            //model.Axes[idx_to_grid].Reset();
            //model.Axes[idx_eeg].Reset();
        }

        public void ShowBarGraph(UserNamesAndDataPoints data, DateTime from, DateTime to)
        {
            ClearData();

            

            var s1 = new BarSeries { Title = "Series 1", StrokeColor = OxyColors.Black, StrokeThickness = 1, IsStacked=true };
            s1.Items.Add(new BarItem { Value = 25 });
            s1.Items.Add(new BarItem { Value = 137 });
            s1.Items.Add(new BarItem { Value = 18 });
            s1.Items.Add(new BarItem { Value = 40 });

            var s2 = new BarSeries { Title = "Series 2", StrokeColor = OxyColors.Black, StrokeThickness = 1, IsStacked = true };
            s2.Items.Add(new BarItem { Value = 12 });
            s2.Items.Add(new BarItem { Value = 14 });
            s2.Items.Add(new BarItem { Value = 120 });
            s2.Items.Add(new BarItem { Value = 26 });

            var categoryAxis = new CategoryAxis { Position = AxisPosition.Bottom };
            categoryAxis.Labels.Add("Category A");
            categoryAxis.Labels.Add("Category B");
            categoryAxis.Labels.Add("Category C");
            categoryAxis.Labels.Add("Category D");
            //var valueAxis = new LinearAxis { Position = AxisPosition.Left, MinimumPadding = 0, MaximumPadding = 0.06, AbsoluteMinimum = 0 };

            model.Series.Add(s1);
            model.Series.Add(s2);
            //model.Axes.Add(valueAxis);
            model.Axes.Add(categoryAxis);

            return;

            /*


            PowerMeterData pm;
            string legend_text;

            for (int meter = 0; meter < data.PmData.Count; meter++)
            {
                pm = data.PmData[meter];

                legend_text = pm.isConsumer ? "CON: " : "GEN: ";
                legend_text += "AT**" + pm.PM_ID.Substring(pm.PM_ID.Length - 8) + ", " + pm.User.Name;

                BarSeries bs = new BarSeries();
                bs.IsStacked = true;
                bs.Items.Add(new BarItem(20));
                bs.Items.Add(new BarItem(60));

                model.Series.Add(bs);

                model.Axes[meter].Reset();
                return;

                //RectangleBarSeries rbs = new RectangleBarSeries() { Title = legend_text };
                //model.Series.Add(rbs);
                ////model.Axes.Add(NewAxis(meter + 1, rbs, AxisPosition.Left));

                //for (int dp = 0; dp < pm.Data.Timestamp.Count; dp++)
                //{
                //    if ((pm.Data.Timestamp[dp] >= from) && (pm.Data.Timestamp[dp] <= to))
                //    {
                //        RectangleBarItem item = new RectangleBarItem() { }

                //        if (pm.isConsumer)
                //            (model.Series[meter] as RectangleBarSeries).Items.Add( (new DataPoint(DateTimeAxis.ToDouble(pm.Data.Timestamp[dp]), pm.Data.PowerFromEEG[dp]));      // used from EEG
                //        else
                //            (model.Series[meter] as LineSeries).Points.Add(new DataPoint(DateTimeAxis.ToDouble(pm.Data.Timestamp[dp]), pm.Data.PowerUsedTotal[dp]));    // fed to EEG
                //    }
                //}
                //model.Axes[meter].Reset();
            }
            */
        }

        public void ClearData()
        {
            view.SuspendLayout();
            model.Series.Clear();
            model.Axes.Clear();
            model.InvalidatePlot(true);
            view.ResumeLayout();
            view.Invalidate(true);
        }
    }
}
