namespace EDAvisu
{
    partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.MainMenuStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.fileOpenToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.graphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.typeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.barToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nachUpdatesSuchenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.überToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.objectListView = new BrightIdeasSoftware.ObjectListView();
            this.DataQuality = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.Type = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.UserName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.PowerMeterID = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.cbConsumed_Total = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.cbFrom_EEG_MaxAvaliable = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.cbFrom_EEG_Consumed = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.cbProduced_Total = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.cbToEEG = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.cbToGrid = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.SelectedDateFrom = new System.Windows.Forms.DateTimePicker();
            this.SelectedDateTo = new System.Windows.Forms.DateTimePicker();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.OxyPlotView = new OxyPlot.WindowsForms.PlotView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAll = new System.Windows.Forms.Button();
            this.btnNone = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenuStrip,
            this.graphToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(959, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "EGSmenuStrip";
            // 
            // MainMenuStrip
            // 
            this.MainMenuStrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileOpenToolStripMenuItem1,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.MainMenuStrip.Name = "MainMenuStrip";
            this.MainMenuStrip.Size = new System.Drawing.Size(50, 20);
            this.MainMenuStrip.Text = "Menü";
            // 
            // fileOpenToolStripMenuItem1
            // 
            this.fileOpenToolStripMenuItem1.Name = "fileOpenToolStripMenuItem1";
            this.fileOpenToolStripMenuItem1.Size = new System.Drawing.Size(172, 22);
            this.fileOpenToolStripMenuItem1.Text = "EDA Report öffnen";
            this.fileOpenToolStripMenuItem1.Click += new System.EventHandler(this.fileOpenToolStripMenuItem1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(169, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.exitToolStripMenuItem.Text = "Beenden";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // graphToolStripMenuItem
            // 
            this.graphToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.typeToolStripMenuItem});
            this.graphToolStripMenuItem.Name = "graphToolStripMenuItem";
            this.graphToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.graphToolStripMenuItem.Text = "Graphik";
            // 
            // typeToolStripMenuItem
            // 
            this.typeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lineToolStripMenuItem,
            this.barToolStripMenuItem});
            this.typeToolStripMenuItem.Name = "typeToolStripMenuItem";
            this.typeToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.typeToolStripMenuItem.Text = "Type";
            // 
            // lineToolStripMenuItem
            // 
            this.lineToolStripMenuItem.Checked = true;
            this.lineToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.lineToolStripMenuItem.Name = "lineToolStripMenuItem";
            this.lineToolStripMenuItem.Size = new System.Drawing.Size(96, 22);
            this.lineToolStripMenuItem.Text = "Line";
            this.lineToolStripMenuItem.Click += new System.EventHandler(this.lineToolStripMenuItem_Click);
            // 
            // barToolStripMenuItem
            // 
            this.barToolStripMenuItem.Name = "barToolStripMenuItem";
            this.barToolStripMenuItem.Size = new System.Drawing.Size(96, 22);
            this.barToolStripMenuItem.Text = "Bar";
            this.barToolStripMenuItem.Click += new System.EventHandler(this.barToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nachUpdatesSuchenToolStripMenuItem,
            this.toolStripSeparator2,
            this.überToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.aboutToolStripMenuItem.Text = "Hilfe";
            // 
            // nachUpdatesSuchenToolStripMenuItem
            // 
            this.nachUpdatesSuchenToolStripMenuItem.Name = "nachUpdatesSuchenToolStripMenuItem";
            this.nachUpdatesSuchenToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.nachUpdatesSuchenToolStripMenuItem.Text = "Nach Updates suchen";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(186, 6);
            // 
            // überToolStripMenuItem
            // 
            this.überToolStripMenuItem.Name = "überToolStripMenuItem";
            this.überToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.überToolStripMenuItem.Text = "Info";
            // 
            // objectListView
            // 
            this.objectListView.AllColumns.Add(this.DataQuality);
            this.objectListView.AllColumns.Add(this.Type);
            this.objectListView.AllColumns.Add(this.UserName);
            this.objectListView.AllColumns.Add(this.PowerMeterID);
            this.objectListView.AllColumns.Add(this.cbConsumed_Total);
            this.objectListView.AllColumns.Add(this.cbFrom_EEG_MaxAvaliable);
            this.objectListView.AllColumns.Add(this.cbFrom_EEG_Consumed);
            this.objectListView.AllColumns.Add(this.cbProduced_Total);
            this.objectListView.AllColumns.Add(this.cbToEEG);
            this.objectListView.AllColumns.Add(this.cbToGrid);
            this.objectListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.objectListView.CellEditUseWholeCell = false;
            this.objectListView.CheckedAspectName = "";
            this.objectListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.DataQuality,
            this.Type,
            this.UserName,
            this.PowerMeterID,
            this.cbConsumed_Total,
            this.cbFrom_EEG_MaxAvaliable,
            this.cbFrom_EEG_Consumed,
            this.cbProduced_Total,
            this.cbToEEG,
            this.cbToGrid});
            this.objectListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.objectListView.FullRowSelect = true;
            this.objectListView.GridLines = true;
            this.objectListView.HideSelection = false;
            this.objectListView.Location = new System.Drawing.Point(0, 35);
            this.objectListView.Name = "objectListView";
            this.objectListView.ShowImagesOnSubItems = true;
            this.objectListView.Size = new System.Drawing.Size(451, 447);
            this.objectListView.TabIndex = 1;
            this.objectListView.UseCompatibleStateImageBehavior = false;
            this.objectListView.UseSubItemCheckBoxes = true;
            this.objectListView.View = System.Windows.Forms.View.Details;
            // 
            // DataQuality
            // 
            this.DataQuality.AspectName = "DataQuality";
            this.DataQuality.IsEditable = false;
            this.DataQuality.IsHeaderVertical = true;
            this.DataQuality.Text = "DatenQuality";
            this.DataQuality.ToolTipText = "L1=gemessener Wert  |  L2=Wert kann sich noch ändern  |  L3=Ersatzwert, nicht ver" +
    "wenden!";
            this.DataQuality.Width = 59;
            // 
            // Type
            // 
            this.Type.AspectName = "Type";
            this.Type.IsHeaderVertical = true;
            this.Type.Text = "Type";
            this.Type.ToolTipText = "Erzeuger / Verbraucher";
            // 
            // UserName
            // 
            this.UserName.AspectName = "User.Name";
            this.UserName.IsHeaderVertical = true;
            this.UserName.Text = "Name";
            this.UserName.ToolTipText = "Zählpunkt Inhaber";
            // 
            // PowerMeterID
            // 
            this.PowerMeterID.AspectName = "PM_ID";
            this.PowerMeterID.IsHeaderVertical = true;
            this.PowerMeterID.Text = "ZählerNr.";
            this.PowerMeterID.ToolTipText = "eindeutige Zählpunktnummer";
            // 
            // cbConsumed_Total
            // 
            this.cbConsumed_Total.AspectName = "Series.Consumed_Total_kWh.Visible";
            this.cbConsumed_Total.CheckBoxes = true;
            this.cbConsumed_Total.IsHeaderVertical = true;
            this.cbConsumed_Total.MaximumWidth = 30;
            this.cbConsumed_Total.MinimumWidth = 10;
            this.cbConsumed_Total.Text = "Verbrauch Gesamt";
            this.cbConsumed_Total.ToolTipText = "gesamte verbrauchte Energie";
            this.cbConsumed_Total.Width = 20;
            // 
            // cbFrom_EEG_MaxAvaliable
            // 
            this.cbFrom_EEG_MaxAvaliable.AspectName = "Series.FromEEG_MaxAvaliable_kWh.Visible";
            this.cbFrom_EEG_MaxAvaliable.CheckBoxes = true;
            this.cbFrom_EEG_MaxAvaliable.IsHeaderVertical = true;
            this.cbFrom_EEG_MaxAvaliable.MaximumWidth = 30;
            this.cbFrom_EEG_MaxAvaliable.MinimumWidth = 10;
            this.cbFrom_EEG_MaxAvaliable.Text = "Verfügbar EEG";
            this.cbFrom_EEG_MaxAvaliable.ToolTipText = "aus der EEG theoretisch verfügbare Energie";
            this.cbFrom_EEG_MaxAvaliable.Width = 20;
            // 
            // cbFrom_EEG_Consumed
            // 
            this.cbFrom_EEG_Consumed.AspectName = "Series.FromEEG_Consumed_kWh.Visible";
            this.cbFrom_EEG_Consumed.CheckBoxes = true;
            this.cbFrom_EEG_Consumed.IsHeaderVertical = true;
            this.cbFrom_EEG_Consumed.MaximumWidth = 30;
            this.cbFrom_EEG_Consumed.MinimumWidth = 10;
            this.cbFrom_EEG_Consumed.Text = "Verbrauch EEG";
            this.cbFrom_EEG_Consumed.ToolTipText = "verbrauchte Energie aus der EEG";
            this.cbFrom_EEG_Consumed.Width = 20;
            // 
            // cbProduced_Total
            // 
            this.cbProduced_Total.AspectName = "Series.Produced_Total_kWh.Visible";
            this.cbProduced_Total.CheckBoxes = true;
            this.cbProduced_Total.IsHeaderVertical = true;
            this.cbProduced_Total.MaximumWidth = 30;
            this.cbProduced_Total.MinimumWidth = 10;
            this.cbProduced_Total.Text = "Erzeugung Gesamt";
            this.cbProduced_Total.ToolTipText = "gesamte erzeugte Energie";
            this.cbProduced_Total.Width = 20;
            // 
            // cbToEEG
            // 
            this.cbToEEG.AspectName = "Series.ToEEG_kWh.Visible";
            this.cbToEEG.CheckBoxes = true;
            this.cbToEEG.IsHeaderVertical = true;
            this.cbToEEG.MaximumWidth = 30;
            this.cbToEEG.MinimumWidth = 10;
            this.cbToEEG.Text = "Verbrauch EEG";
            this.cbToEEG.ToolTipText = "von der EEG verbrauchte Energie";
            this.cbToEEG.Width = 20;
            // 
            // cbToGrid
            // 
            this.cbToGrid.AspectName = "Series.ToGrid_kWh.Visible";
            this.cbToGrid.CheckBoxes = true;
            this.cbToGrid.IsHeaderVertical = true;
            this.cbToGrid.MaximumWidth = 30;
            this.cbToGrid.MinimumWidth = 10;
            this.cbToGrid.Text = "Überschuss ins Netz";
            this.cbToGrid.ToolTipText = "nicht in der EEG verbrauchte Energie (Restüberschuss )";
            this.cbToGrid.Width = 20;
            // 
            // SelectedDateFrom
            // 
            this.SelectedDateFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SelectedDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.SelectedDateFrom.Location = new System.Drawing.Point(3, 3);
            this.SelectedDateFrom.Name = "SelectedDateFrom";
            this.SelectedDateFrom.Size = new System.Drawing.Size(113, 20);
            this.SelectedDateFrom.TabIndex = 3;
            // 
            // SelectedDateTo
            // 
            this.SelectedDateTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SelectedDateTo.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.SelectedDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.SelectedDateTo.Location = new System.Drawing.Point(326, 3);
            this.SelectedDateTo.Name = "SelectedDateTo";
            this.SelectedDateTo.Size = new System.Drawing.Size(116, 20);
            this.SelectedDateTo.TabIndex = 5;
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.Location = new System.Drawing.Point(12, 27);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.OxyPlotView);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer.Panel2.Controls.Add(this.objectListView);
            this.splitContainer.Size = new System.Drawing.Size(935, 482);
            this.splitContainer.SplitterDistance = 480;
            this.splitContainer.TabIndex = 7;
            // 
            // OxyPlotView
            // 
            this.OxyPlotView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OxyPlotView.Location = new System.Drawing.Point(0, 0);
            this.OxyPlotView.Name = "OxyPlotView";
            this.OxyPlotView.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.OxyPlotView.Size = new System.Drawing.Size(480, 482);
            this.OxyPlotView.TabIndex = 1;
            this.OxyPlotView.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.OxyPlotView.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.OxyPlotView.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.89464F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.40357F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.40357F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.40357F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.89464F));
            this.tableLayoutPanel1.Controls.Add(this.btnAll, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnNone, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.SelectedDateFrom, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnUpdate, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.SelectedDateTo, 4, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(445, 26);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // btnAll
            // 
            this.btnAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAll.Location = new System.Drawing.Point(258, 3);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(62, 20);
            this.btnAll.TabIndex = 9;
            this.btnAll.Text = "Alle";
            this.btnAll.UseVisualStyleBackColor = true;
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // btnNone
            // 
            this.btnNone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNone.Location = new System.Drawing.Point(122, 3);
            this.btnNone.Name = "btnNone";
            this.btnNone.Size = new System.Drawing.Size(62, 20);
            this.btnNone.TabIndex = 8;
            this.btnNone.Text = "Nichts";
            this.btnNone.UseVisualStyleBackColor = true;
            this.btnNone.Click += new System.EventHandler(this.btnNone_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUpdate.Location = new System.Drawing.Point(190, 3);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(62, 20);
            this.btnUpdate.TabIndex = 7;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(959, 521);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.menuStrip);
            this.Name = "MainForm";
            this.Text = "EDAvisu";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectListView)).EndInit();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip;
        private new System.Windows.Forms.ToolStripMenuItem MainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileOpenToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.DateTimePicker SelectedDateFrom;
        private System.Windows.Forms.DateTimePicker SelectedDateTo;
        private System.Windows.Forms.ToolStripMenuItem graphToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem typeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem barToolStripMenuItem;
        private BrightIdeasSoftware.ObjectListView objectListView;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nachUpdatesSuchenToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem überToolStripMenuItem;
        private BrightIdeasSoftware.OLVColumn UserName;
        private BrightIdeasSoftware.OLVColumn PowerMeterID;
        private BrightIdeasSoftware.OLVColumn DataQuality;
        private BrightIdeasSoftware.OLVColumn Type;
        private BrightIdeasSoftware.OLVColumn cbConsumed_Total;
        private BrightIdeasSoftware.OLVColumn cbFrom_EEG_MaxAvaliable;
        private BrightIdeasSoftware.OLVColumn cbFrom_EEG_Consumed;
        private BrightIdeasSoftware.OLVColumn cbProduced_Total;
        private BrightIdeasSoftware.OLVColumn cbToGrid;
        private BrightIdeasSoftware.OLVColumn cbToEEG;
        private System.Windows.Forms.SplitContainer splitContainer;
        private OxyPlot.WindowsForms.PlotView OxyPlotView;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnAll;
        private System.Windows.Forms.Button btnNone;
    }
}

