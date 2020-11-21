namespace SmartFactoryProject_Final
{
    partial class FRM_Result
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea15 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend15 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series29 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series30 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea16 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend16 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series31 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series32 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.Btn_Search = new System.Windows.Forms.Button();
            this.Txt_Start_Year = new System.Windows.Forms.TextBox();
            this.Txt_Start_Month = new System.Windows.Forms.TextBox();
            this.Txt_Start_Day = new System.Windows.Forms.TextBox();
            this.Lbl_PeriodText = new System.Windows.Forms.Label();
            this.Lbl_SYText = new System.Windows.Forms.Label();
            this.Lbl_SMText = new System.Windows.Forms.Label();
            this.Lbl_SDText = new System.Windows.Forms.Label();
            this.Lbl_EDText = new System.Windows.Forms.Label();
            this.Lbl_EMText = new System.Windows.Forms.Label();
            this.Lbl_EYText = new System.Windows.Forms.Label();
            this.Txt_End_Day = new System.Windows.Forms.TextBox();
            this.Txt_End_Month = new System.Windows.Forms.TextBox();
            this.Txt_End_Year = new System.Windows.Forms.TextBox();
            this.Dgv_ItemGroup = new System.Windows.Forms.DataGridView();
            this.Cht_ItemGroup = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Dgv_MachGroup = new System.Windows.Forms.DataGridView();
            this.Cht_MachGroup = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Tim_CheckTime = new System.Windows.Forms.Timer(this.components);
            this.Lbl_Time = new System.Windows.Forms.Label();
            this.Btn_ItemGroup_ScrUp = new System.Windows.Forms.Button();
            this.Btn_ItemGroup_ScrDn = new System.Windows.Forms.Button();
            this.Btn_MachGroup_ScrUp = new System.Windows.Forms.Button();
            this.Btn_MachGroup_ScrDn = new System.Windows.Forms.Button();
            this.Lbl_DateRange = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_ItemGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cht_ItemGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_MachGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cht_MachGroup)).BeginInit();
            this.SuspendLayout();
            // 
            // Btn_Search
            // 
            this.Btn_Search.ForeColor = System.Drawing.Color.White;
            this.Btn_Search.Location = new System.Drawing.Point(1112, 93);
            this.Btn_Search.Name = "Btn_Search";
            this.Btn_Search.Size = new System.Drawing.Size(75, 23);
            this.Btn_Search.TabIndex = 0;
            this.Btn_Search.Text = "검색";
            this.Btn_Search.UseVisualStyleBackColor = true;
            this.Btn_Search.Click += new System.EventHandler(this.Btn_Search_Click);
            // 
            // Txt_Start_Year
            // 
            this.Txt_Start_Year.Location = new System.Drawing.Point(143, 93);
            this.Txt_Start_Year.Name = "Txt_Start_Year";
            this.Txt_Start_Year.Size = new System.Drawing.Size(100, 25);
            this.Txt_Start_Year.TabIndex = 1;
            this.Txt_Start_Year.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Txt_Start_Year.Click += new System.EventHandler(this.Txt_Date_Click);
            // 
            // Txt_Start_Month
            // 
            this.Txt_Start_Month.Location = new System.Drawing.Point(298, 91);
            this.Txt_Start_Month.Name = "Txt_Start_Month";
            this.Txt_Start_Month.Size = new System.Drawing.Size(100, 25);
            this.Txt_Start_Month.TabIndex = 2;
            this.Txt_Start_Month.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Txt_Start_Month.Click += new System.EventHandler(this.Txt_Date_Click);
            // 
            // Txt_Start_Day
            // 
            this.Txt_Start_Day.Location = new System.Drawing.Point(432, 91);
            this.Txt_Start_Day.Name = "Txt_Start_Day";
            this.Txt_Start_Day.Size = new System.Drawing.Size(100, 25);
            this.Txt_Start_Day.TabIndex = 3;
            this.Txt_Start_Day.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Txt_Start_Day.Click += new System.EventHandler(this.Txt_Date_Click);
            // 
            // Lbl_PeriodText
            // 
            this.Lbl_PeriodText.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_PeriodText.Location = new System.Drawing.Point(82, 93);
            this.Lbl_PeriodText.Name = "Lbl_PeriodText";
            this.Lbl_PeriodText.Size = new System.Drawing.Size(37, 15);
            this.Lbl_PeriodText.TabIndex = 4;
            this.Lbl_PeriodText.Text = "기간";
            // 
            // Lbl_SYText
            // 
            this.Lbl_SYText.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_SYText.Location = new System.Drawing.Point(258, 97);
            this.Lbl_SYText.Name = "Lbl_SYText";
            this.Lbl_SYText.Size = new System.Drawing.Size(22, 15);
            this.Lbl_SYText.TabIndex = 5;
            this.Lbl_SYText.Text = "년";
            // 
            // Lbl_SMText
            // 
            this.Lbl_SMText.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_SMText.Location = new System.Drawing.Point(404, 96);
            this.Lbl_SMText.Name = "Lbl_SMText";
            this.Lbl_SMText.Size = new System.Drawing.Size(22, 15);
            this.Lbl_SMText.TabIndex = 6;
            this.Lbl_SMText.Text = "월";
            // 
            // Lbl_SDText
            // 
            this.Lbl_SDText.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_SDText.Location = new System.Drawing.Point(538, 97);
            this.Lbl_SDText.Name = "Lbl_SDText";
            this.Lbl_SDText.Size = new System.Drawing.Size(53, 15);
            this.Lbl_SDText.TabIndex = 7;
            this.Lbl_SDText.Text = "일";
            // 
            // Lbl_EDText
            // 
            this.Lbl_EDText.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_EDText.Location = new System.Drawing.Point(1019, 97);
            this.Lbl_EDText.Name = "Lbl_EDText";
            this.Lbl_EDText.Size = new System.Drawing.Size(22, 15);
            this.Lbl_EDText.TabIndex = 13;
            this.Lbl_EDText.Text = "일";
            // 
            // Lbl_EMText
            // 
            this.Lbl_EMText.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_EMText.Location = new System.Drawing.Point(885, 96);
            this.Lbl_EMText.Name = "Lbl_EMText";
            this.Lbl_EMText.Size = new System.Drawing.Size(22, 15);
            this.Lbl_EMText.TabIndex = 12;
            this.Lbl_EMText.Text = "월";
            // 
            // Lbl_EYText
            // 
            this.Lbl_EYText.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_EYText.Location = new System.Drawing.Point(739, 97);
            this.Lbl_EYText.Name = "Lbl_EYText";
            this.Lbl_EYText.Size = new System.Drawing.Size(22, 15);
            this.Lbl_EYText.TabIndex = 11;
            this.Lbl_EYText.Text = "년";
            // 
            // Txt_End_Day
            // 
            this.Txt_End_Day.Location = new System.Drawing.Point(913, 91);
            this.Txt_End_Day.Name = "Txt_End_Day";
            this.Txt_End_Day.Size = new System.Drawing.Size(100, 25);
            this.Txt_End_Day.TabIndex = 10;
            this.Txt_End_Day.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Txt_End_Day.Click += new System.EventHandler(this.Txt_Date_Click);
            // 
            // Txt_End_Month
            // 
            this.Txt_End_Month.Location = new System.Drawing.Point(779, 91);
            this.Txt_End_Month.Name = "Txt_End_Month";
            this.Txt_End_Month.Size = new System.Drawing.Size(100, 25);
            this.Txt_End_Month.TabIndex = 9;
            this.Txt_End_Month.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Txt_End_Month.Click += new System.EventHandler(this.Txt_Date_Click);
            // 
            // Txt_End_Year
            // 
            this.Txt_End_Year.Location = new System.Drawing.Point(624, 93);
            this.Txt_End_Year.Name = "Txt_End_Year";
            this.Txt_End_Year.Size = new System.Drawing.Size(100, 25);
            this.Txt_End_Year.TabIndex = 8;
            this.Txt_End_Year.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Txt_End_Year.Click += new System.EventHandler(this.Txt_Date_Click);
            // 
            // Dgv_ItemGroup
            // 
            this.Dgv_ItemGroup.AllowUserToAddRows = false;
            this.Dgv_ItemGroup.AllowUserToDeleteRows = false;
            this.Dgv_ItemGroup.AllowUserToResizeColumns = false;
            this.Dgv_ItemGroup.AllowUserToResizeRows = false;
            this.Dgv_ItemGroup.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Dgv_ItemGroup.Location = new System.Drawing.Point(51, 158);
            this.Dgv_ItemGroup.Name = "Dgv_ItemGroup";
            this.Dgv_ItemGroup.RowTemplate.Height = 27;
            this.Dgv_ItemGroup.Size = new System.Drawing.Size(487, 264);
            this.Dgv_ItemGroup.TabIndex = 14;
            // 
            // Cht_ItemGroup
            // 
            chartArea15.Name = "ChartArea1";
            this.Cht_ItemGroup.ChartAreas.Add(chartArea15);
            legend15.Name = "Legend1";
            this.Cht_ItemGroup.Legends.Add(legend15);
            this.Cht_ItemGroup.Location = new System.Drawing.Point(696, 170);
            this.Cht_ItemGroup.Name = "Cht_ItemGroup";
            series29.ChartArea = "ChartArea1";
            series29.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
            series29.Legend = "Legend1";
            series29.LegendText = "정상품";
            series29.Name = "Normal";
            series30.ChartArea = "ChartArea1";
            series30.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
            series30.Legend = "Legend1";
            series30.LegendText = "불량품";
            series30.Name = "Defect";
            this.Cht_ItemGroup.Series.Add(series29);
            this.Cht_ItemGroup.Series.Add(series30);
            this.Cht_ItemGroup.Size = new System.Drawing.Size(491, 252);
            this.Cht_ItemGroup.TabIndex = 15;
            this.Cht_ItemGroup.Text = "chart1";
            // 
            // Dgv_MachGroup
            // 
            this.Dgv_MachGroup.AllowUserToAddRows = false;
            this.Dgv_MachGroup.AllowUserToDeleteRows = false;
            this.Dgv_MachGroup.AllowUserToResizeColumns = false;
            this.Dgv_MachGroup.AllowUserToResizeRows = false;
            this.Dgv_MachGroup.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Dgv_MachGroup.Location = new System.Drawing.Point(51, 428);
            this.Dgv_MachGroup.Name = "Dgv_MachGroup";
            this.Dgv_MachGroup.RowTemplate.Height = 27;
            this.Dgv_MachGroup.Size = new System.Drawing.Size(487, 272);
            this.Dgv_MachGroup.TabIndex = 16;
            // 
            // Cht_MachGroup
            // 
            chartArea16.Name = "ChartArea1";
            this.Cht_MachGroup.ChartAreas.Add(chartArea16);
            legend16.Name = "Legend1";
            this.Cht_MachGroup.Legends.Add(legend16);
            this.Cht_MachGroup.Location = new System.Drawing.Point(696, 428);
            this.Cht_MachGroup.Name = "Cht_MachGroup";
            series31.ChartArea = "ChartArea1";
            series31.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
            series31.Legend = "Legend1";
            series31.LegendText = "정상품";
            series31.Name = "Normal";
            series32.ChartArea = "ChartArea1";
            series32.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
            series32.Legend = "Legend1";
            series32.LegendText = "불량품";
            series32.Name = "Defect";
            this.Cht_MachGroup.Series.Add(series31);
            this.Cht_MachGroup.Series.Add(series32);
            this.Cht_MachGroup.Size = new System.Drawing.Size(491, 252);
            this.Cht_MachGroup.TabIndex = 17;
            this.Cht_MachGroup.Text = "chart1";
            // 
            // Tim_CheckTime
            // 
            this.Tim_CheckTime.Enabled = true;
            this.Tim_CheckTime.Interval = 1000;
            this.Tim_CheckTime.Tick += new System.EventHandler(this.Tim_CheckTime_Tick);
            // 
            // Lbl_Time
            // 
            this.Lbl_Time.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Time.Location = new System.Drawing.Point(48, 38);
            this.Lbl_Time.Name = "Lbl_Time";
            this.Lbl_Time.Size = new System.Drawing.Size(100, 23);
            this.Lbl_Time.TabIndex = 18;
            this.Lbl_Time.Text = "label1";
            this.Lbl_Time.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Btn_ItemGroup_ScrUp
            // 
            this.Btn_ItemGroup_ScrUp.Location = new System.Drawing.Point(544, 158);
            this.Btn_ItemGroup_ScrUp.Name = "Btn_ItemGroup_ScrUp";
            this.Btn_ItemGroup_ScrUp.Size = new System.Drawing.Size(57, 119);
            this.Btn_ItemGroup_ScrUp.TabIndex = 19;
            this.Btn_ItemGroup_ScrUp.UseVisualStyleBackColor = true;
            this.Btn_ItemGroup_ScrUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btn_ItemGroup_ScrUp_MouseDown);
            this.Btn_ItemGroup_ScrUp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Btn_ItemGroup_ScrUp_MouseUp);
            // 
            // Btn_ItemGroup_ScrDn
            // 
            this.Btn_ItemGroup_ScrDn.Location = new System.Drawing.Point(544, 303);
            this.Btn_ItemGroup_ScrDn.Name = "Btn_ItemGroup_ScrDn";
            this.Btn_ItemGroup_ScrDn.Size = new System.Drawing.Size(57, 119);
            this.Btn_ItemGroup_ScrDn.TabIndex = 20;
            this.Btn_ItemGroup_ScrDn.UseVisualStyleBackColor = true;
            this.Btn_ItemGroup_ScrDn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btn_ItemGroup_ScrDn_MouseDown);
            this.Btn_ItemGroup_ScrDn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Btn_ItemGroup_ScrDn_MouseUp);
            // 
            // Btn_MachGroup_ScrUp
            // 
            this.Btn_MachGroup_ScrUp.Location = new System.Drawing.Point(544, 428);
            this.Btn_MachGroup_ScrUp.Name = "Btn_MachGroup_ScrUp";
            this.Btn_MachGroup_ScrUp.Size = new System.Drawing.Size(57, 119);
            this.Btn_MachGroup_ScrUp.TabIndex = 21;
            this.Btn_MachGroup_ScrUp.UseVisualStyleBackColor = true;
            this.Btn_MachGroup_ScrUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btn_MachGroup_ScrUp_MouseDown);
            this.Btn_MachGroup_ScrUp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Btn_MachGroup_ScrUp_MouseUp);
            // 
            // Btn_MachGroup_ScrDn
            // 
            this.Btn_MachGroup_ScrDn.Location = new System.Drawing.Point(544, 581);
            this.Btn_MachGroup_ScrDn.Name = "Btn_MachGroup_ScrDn";
            this.Btn_MachGroup_ScrDn.Size = new System.Drawing.Size(57, 119);
            this.Btn_MachGroup_ScrDn.TabIndex = 22;
            this.Btn_MachGroup_ScrDn.UseVisualStyleBackColor = true;
            this.Btn_MachGroup_ScrDn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btn_MachGroup_ScrDn_MouseDown);
            this.Btn_MachGroup_ScrDn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Btn_MachGroup_ScrDn_MouseUp);
            // 
            // Lbl_DateRange
            // 
            this.Lbl_DateRange.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_DateRange.Location = new System.Drawing.Point(579, 97);
            this.Lbl_DateRange.Name = "Lbl_DateRange";
            this.Lbl_DateRange.Size = new System.Drawing.Size(22, 15);
            this.Lbl_DateRange.TabIndex = 23;
            this.Lbl_DateRange.Text = "~";
            // 
            // FRM_Result
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1262, 731);
            this.Controls.Add(this.Lbl_DateRange);
            this.Controls.Add(this.Btn_MachGroup_ScrDn);
            this.Controls.Add(this.Btn_MachGroup_ScrUp);
            this.Controls.Add(this.Btn_ItemGroup_ScrDn);
            this.Controls.Add(this.Btn_ItemGroup_ScrUp);
            this.Controls.Add(this.Lbl_Time);
            this.Controls.Add(this.Cht_MachGroup);
            this.Controls.Add(this.Dgv_MachGroup);
            this.Controls.Add(this.Cht_ItemGroup);
            this.Controls.Add(this.Dgv_ItemGroup);
            this.Controls.Add(this.Lbl_EDText);
            this.Controls.Add(this.Lbl_EMText);
            this.Controls.Add(this.Lbl_EYText);
            this.Controls.Add(this.Txt_End_Day);
            this.Controls.Add(this.Txt_End_Month);
            this.Controls.Add(this.Txt_End_Year);
            this.Controls.Add(this.Lbl_SDText);
            this.Controls.Add(this.Lbl_SMText);
            this.Controls.Add(this.Lbl_SYText);
            this.Controls.Add(this.Lbl_PeriodText);
            this.Controls.Add(this.Txt_Start_Day);
            this.Controls.Add(this.Txt_Start_Month);
            this.Controls.Add(this.Txt_Start_Year);
            this.Controls.Add(this.Btn_Search);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FRM_Result";
            this.Text = "FRM_Result";
            this.Load += new System.EventHandler(this.FRM_Result_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_ItemGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cht_ItemGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_MachGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cht_MachGroup)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Btn_Search;
        private System.Windows.Forms.TextBox Txt_Start_Year;
        private System.Windows.Forms.TextBox Txt_Start_Month;
        private System.Windows.Forms.TextBox Txt_Start_Day;
        private System.Windows.Forms.Label Lbl_PeriodText;
        private System.Windows.Forms.Label Lbl_SYText;
        private System.Windows.Forms.Label Lbl_SMText;
        private System.Windows.Forms.Label Lbl_SDText;
        private System.Windows.Forms.Label Lbl_EDText;
        private System.Windows.Forms.Label Lbl_EMText;
        private System.Windows.Forms.Label Lbl_EYText;
        private System.Windows.Forms.TextBox Txt_End_Day;
        private System.Windows.Forms.TextBox Txt_End_Month;
        private System.Windows.Forms.TextBox Txt_End_Year;
        private System.Windows.Forms.DataGridView Dgv_ItemGroup;
        private System.Windows.Forms.DataVisualization.Charting.Chart Cht_ItemGroup;
        private System.Windows.Forms.DataGridView Dgv_MachGroup;
        private System.Windows.Forms.DataVisualization.Charting.Chart Cht_MachGroup;
        private System.Windows.Forms.Timer Tim_CheckTime;
        private System.Windows.Forms.Label Lbl_Time;
        private System.Windows.Forms.Button Btn_ItemGroup_ScrUp;
        private System.Windows.Forms.Button Btn_ItemGroup_ScrDn;
        private System.Windows.Forms.Button Btn_MachGroup_ScrUp;
        private System.Windows.Forms.Button Btn_MachGroup_ScrDn;
        private System.Windows.Forms.Label Lbl_DateRange;
    }
}