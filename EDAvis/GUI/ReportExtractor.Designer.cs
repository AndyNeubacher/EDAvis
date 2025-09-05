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
            this.btnExtractAndEdit = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.tbExtractLog = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 38);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Monatsreport XLS:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 92);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Jahresreport XLS:";
            // 
            // tbMonth
            // 
            this.tbMonth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMonth.Location = new System.Drawing.Point(194, 34);
            this.tbMonth.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbMonth.Name = "tbMonth";
            this.tbMonth.Size = new System.Drawing.Size(832, 26);
            this.tbMonth.TabIndex = 2;
            // 
            // tbYear
            // 
            this.tbYear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbYear.Location = new System.Drawing.Point(194, 88);
            this.tbYear.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbYear.Name = "tbYear";
            this.tbYear.Size = new System.Drawing.Size(832, 26);
            this.tbYear.TabIndex = 3;
            // 
            // btnSelectReport_Month
            // 
            this.btnSelectReport_Month.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectReport_Month.Location = new System.Drawing.Point(1036, 34);
            this.btnSelectReport_Month.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSelectReport_Month.Name = "btnSelectReport_Month";
            this.btnSelectReport_Month.Size = new System.Drawing.Size(46, 31);
            this.btnSelectReport_Month.TabIndex = 4;
            this.btnSelectReport_Month.Text = "...";
            this.btnSelectReport_Month.UseVisualStyleBackColor = true;
            this.btnSelectReport_Month.Click += new System.EventHandler(this.btnSelectReport_Month_Click);
            // 
            // btnSelectReport_Year
            // 
            this.btnSelectReport_Year.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectReport_Year.Location = new System.Drawing.Point(1036, 88);
            this.btnSelectReport_Year.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSelectReport_Year.Name = "btnSelectReport_Year";
            this.btnSelectReport_Year.Size = new System.Drawing.Size(46, 31);
            this.btnSelectReport_Year.TabIndex = 5;
            this.btnSelectReport_Year.Text = "...";
            this.btnSelectReport_Year.UseVisualStyleBackColor = true;
            this.btnSelectReport_Year.Click += new System.EventHandler(this.btnSelectReport_Year_Click);
            // 
            // btnExtractAndEdit
            // 
            this.btnExtractAndEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExtractAndEdit.Location = new System.Drawing.Point(18, 565);
            this.btnExtractAndEdit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnExtractAndEdit.Name = "btnExtractAndEdit";
            this.btnExtractAndEdit.Size = new System.Drawing.Size(525, 35);
            this.btnExtractAndEdit.TabIndex = 6;
            this.btnExtractAndEdit.Text = "copy data Month -> Year";
            this.btnExtractAndEdit.UseVisualStyleBackColor = true;
            this.btnExtractAndEdit.Click += new System.EventHandler(this.btnCopyMonthlyReport_TO_YearlyReport_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(558, 565);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(525, 35);
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
            this.tbExtractLog.Location = new System.Drawing.Point(18, 127);
            this.tbExtractLog.Multiline = true;
            this.tbExtractLog.Name = "tbExtractLog";
            this.tbExtractLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbExtractLog.Size = new System.Drawing.Size(1064, 430);
            this.tbExtractLog.TabIndex = 8;
            // 
            // ReportExtractor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1101, 619);
            this.Controls.Add(this.tbExtractLog);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnExtractAndEdit);
            this.Controls.Add(this.btnSelectReport_Year);
            this.Controls.Add(this.btnSelectReport_Month);
            this.Controls.Add(this.tbYear);
            this.Controls.Add(this.tbMonth);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1114, 231);
            this.Name = "ReportExtractor";
            this.Text = "ReportExtractor";
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
        private System.Windows.Forms.Button btnExtractAndEdit;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox tbExtractLog;
    }
}