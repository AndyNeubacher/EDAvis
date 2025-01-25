using System;
using System.Collections.Generic;
using System.ComponentModel;



namespace EDAvis
{
    public class UserName
    {
        public string Name;                            // tab2, row3
        public string Address;
    }

    public class DataPoints : INotifyPropertyChanged
    {
        private bool _visible;
        public bool Visible
        {
            get { return _visible; }
            set {
                if (_visible != value)
                {
                    _visible = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Visible"));
                }
            }
        }
        public List<double> Points;

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class DataSeries
    {
        public DataPoints Consumed_Total_kWh;        // tab2, col 2,12,22 [B]
        public DataPoints FromEEG_MaxAvaliable_kWh;  // tab2, col 6
        public DataPoints FromEEG_Consumed_kWh;      // tab2, col 8,18,28 [H]

        public DataPoints Produced_Total_kWh;        // tab2, col 1(generation)
        public DataPoints ToGrid_kWh;                // tab2, col 7(generation)
        public DataPoints ToEEG_kWh;                 // Produced_kWh - ToGrid_kWh
    }

    public class PowerMeter
    {
        public string PM_ID;                         // tab1, A8-Ax(1)
        public UserName User;                        // tab2, row3(3)
        public string DataQuality;                   // tab1, P8(16)
        public string Type;                          // GENERATION or CONSUMPTION

        public DataSeries Series;
    }

    public class UserNamesAndDataPoints
    {
        public List<DateTime> Timestamps;
        public List<PowerMeter> Data;
    }
}
