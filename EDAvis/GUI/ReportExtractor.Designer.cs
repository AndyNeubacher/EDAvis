namespace EDAvis.GUI
{
    partial class ReportExtractor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbMonth = new System.Windows.Forms.TextBox();
            this.tbYear = new System.Windows.Forms.TextBox();
            this.btnSelectReport_Month = new System.Windows.Forms.Button();
            this.btnSelectReport_Year = new System.Windows.Forms.Button();
            this.btnAnalyzeAndMerge = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.tbExtractLog = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAnalyze = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Monatsreport XLS:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Jahresreport XLS:";
            // 
            // tbMonth
            // 
            this.tbMonth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMonth.Location = new System.Drawing.Point(129, 22);
            this.tbMonth.Name = "tbMonth";
            this.tbMonth.Size = new System.Drawing.Size(556, 20);
            this.tbMonth.TabIndex = 2;
            // 
            // tbYear
            // 
            this.tbYear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbYear.Location = new System.Drawing.Point(129, 57);
            this.tbYear.Name = "tbYear";
            this.tbYear.Size = new System.Drawing.Size(556, 20);
            this.tbYear.TabIndex = 3;
            // 
            // btnSelectReport_Month
            // 
            this.btnSelectReport_Month.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectReport_Month.Location = new System.Drawing.Point(691, 22);
            this.btnSelectReport_Month.Name = "btnSelectReport_Month";
            this.btnSelectReport_Month.Size = new System.Drawing.Size(31, 20);
            this.btnSelectReport_Month.TabIndex = 4;
            this.btnSelectReport_Month.Text = "...";
            this.btnSelectReport_Month.UseVisualStyleBackColor = true;
            this.btnSelectReport_Month.Click += new System.EventHandler(this.btnSelectReport_Month_Click);
            // 
            // btnSelectReport_Year
            // 
            this.btnSelectReport_Year.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectReport_Year.Location = new System.Drawing.Point(691, 57);
            this.btnSelectReport_Year.Name = "btnSelectReport_Year";
            this.btnSelectReport_Year.Size = new System.Drawing.Size(31, 20);
            this.btnSelectReport_Year.TabIndex = 5;
            this.btnSelectReport_Year.Text = "...";
            this.btnSelectReport_Year.UseVisualStyleBackColor = true;
            this.btnSelectReport_Year.Click += new System.EventHandler(this.btnSelectReport_Year_Click);
            // 
            // btnAnalyzeAndMerge
            // 
            this.btnAnalyzeAndMerge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAnalyzeAndMerge.Location = new System.Drawing.Point(287, 3);
            this.btnAnalyzeAndMerge.Name = "btnAnalyzeAndMerge";
            this.btnAnalyzeAndMerge.Size = new System.Drawing.Size(278, 27);
            this.btnAnalyzeAndMerge.TabIndex = 6;
            this.btnAnalyzeAndMerge.Text = "analyze + merge";
            this.btnAnalyzeAndMerge.UseVisualStyleBackColor = true;
            this.btnAnalyzeAndMerge.Click += new System.EventHandler(this.btnAnalyzeAndMerge_Click);
            // 
            // btnExit
            // 
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExit.Location = new System.Drawing.Point(571, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(136, 27);
            this.btnExit.TabIndex = 7;
            this.btnExit.Text = "exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // tbExtractLog
            // 
            this.tbExtractLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbExtractLog.Location = new System.Drawing.Point(12, 83);
            this.tbExtractLog.Margin = new System.Windows.Forms.Padding(2);
            this.tbExtractLog.Multiline = true;
            this.tbExtractLog.Name = "tbExtractLog";
            this.tbExtractLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbExtractLog.Size = new System.Drawing.Size(711, 269);
            this.tbExtractLog.TabIndex = 8;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.btnExit, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnAnalyzeAndMerge, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnAnalyze, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 357);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(710, 33);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // btnAnalyze
            // 
            this.btnAnalyze.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAnalyze.Location = new System.Drawing.Point(3, 3);
            this.btnAnalyze.Name = "btnAnalyze";
            this.btnAnalyze.Size = new System.Drawing.Size(278, 27);
            this.btnAnalyze.TabIndex = 8;
            this.btnAnalyze.Text = "analyze";
            this.btnAnalyze.UseVisualStyleBackColor = true;
            this.btnAnalyze.Click += new System.EventHandler(this.btnAnalyze_Click);
            // 
            // ReportExtractor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 402);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.tbExtractLog);
            this.Controls.Add(this.btnSelectReport_Year);
            this.Controls.Add(this.btnSelectReport_Month);
            this.Controls.Add(this.tbYear);
            this.Controls.Add(this.tbMonth);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(748, 164);
            this.Name = "ReportExtractor";
            this.Text = "ReportExtractor";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbMonth;
        private System.Windows.Forms.TextBox tbYear;
        private System.Windows.Forms.Button btnSelectReport_Month;
        private System.Windows.Forms.Button btnSelectReport_Year;
        private System.Windows.Forms.Button btnAnalyzeAndMerge;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox tbExtractLog;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnAnalyze;
    }
}