using BrightIdeasSoftware;
using EDAvis.GUI;
using EDAvis.Tools;
using System;
using System.Reflection;
using System.Windows.Forms;



namespace EDAvis
{
    public partial class MainForm : Form
    {
        private Plotter EGS_Plotter = null;
        private UserNamesAndDataPoints User_Data = null;


        public MainForm()
        {
            InitializeComponent();

            #if (!DEBUG)
            // only add the eventhandler in the releasebuild -> ugly bugfix of the .NET framework
            this.SelectedDateTo.ValueChanged += new System.EventHandler(this.SelectedDateTo_ValueChanged);
            this.SelectedDateFrom.ValueChanged += new System.EventHandler(this.SelectedDateFrom_ValueChanged);
            #endif

            Version ver = Assembly.GetExecutingAssembly().GetName().Version;
            this.Text = "EDAvis V" + ver.ToString();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fileOpenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //User_Data = ExcelReport_Interop.GetData(openFileDialog.FileName);
                    User_Data = ExcelReport_EPPlus.GetData(openFileDialog.FileName);
                    if (User_Data.Timestamps.Count > 0)
                    {
                        SelectedDateFrom.Value = User_Data.Timestamps[0];
                        SelectedDateTo.Value = User_Data.Timestamps[User_Data.Timestamps.Count - 1];
                    }

                    objectListView.SetObjects(User_Data.Data);
                    objectListView.AutoResizeColumns();                    
                    UpdateGraph();
                }
            }
        }

        private void UpdateGraph()
        {
            if(User_Data != null)
            {
                if (EGS_Plotter == null)
                    EGS_Plotter = new Plotter(OxyPlotView);

                if(lineToolStripMenuItem.Checked)
                    EGS_Plotter.ShowLineGraph(User_Data, SelectedDateFrom.Value, SelectedDateTo.Value);
                if(barToolStripMenuItem.Checked)
                    EGS_Plotter.ShowBarGraph(User_Data, SelectedDateFrom.Value, SelectedDateTo.Value);
            }
        }

        private void SelectedDateFrom_ValueChanged(object sender, EventArgs e)
        {
            UpdateGraph();
        }

        private void SelectedDateTo_ValueChanged(object sender, EventArgs e)
        {
            UpdateGraph();
        }

        private void lineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            barToolStripMenuItem.Checked = lineToolStripMenuItem.Checked;
            lineToolStripMenuItem.Checked = !lineToolStripMenuItem.Checked;
            UpdateGraph();
        }

        private void barToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lineToolStripMenuItem.Checked = barToolStripMenuItem.Checked;
            barToolStripMenuItem.Checked = !barToolStripMenuItem.Checked;
            UpdateGraph();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateGraph();
        }

        private void SetAllSubItemCheckboxes(bool val)
        {
            foreach (OLVListItem item in objectListView.Items)
            {
                PowerMeter pm = item.RowObject as PowerMeter;

                if(pm.Series.Consumed_Total_kWh != null) pm.Series.Consumed_Total_kWh.Visible = val;
                if(pm.Series.FromEEG_MaxAvaliable_kWh != null) pm.Series.FromEEG_MaxAvaliable_kWh.Visible = val;
                if(pm.Series.FromEEG_Consumed_kWh != null) pm.Series.FromEEG_Consumed_kWh.Visible = val;
                if(pm.Series.Produced_Total_kWh != null) pm.Series.Produced_Total_kWh.Visible = val;
                if(pm.Series.ToGrid_kWh != null) pm.Series.ToGrid_kWh.Visible = val;
                if(pm.Series.ToEEG_kWh != null) pm.Series.ToEEG_kWh.Visible = val;
            }

            UpdateGraph();
        }

        private void btnNone_Click(object sender, EventArgs e)
        {
            SetAllSubItemCheckboxes(false);
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            SetAllSubItemCheckboxes(true);
        }

        private void überToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm about = new AboutForm();
            about.ShowDialog();
        }
    }
}
