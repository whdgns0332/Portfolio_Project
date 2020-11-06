namespace Miniproject_Hamburger
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea10 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend10 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series10 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.BtnStock = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.BtnSaleData = new System.Windows.Forms.Button();
            this.BtnSaleMoney = new System.Windows.Forms.Button();
            this.BtnIngreConsume = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnStock
            // 
            this.BtnStock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnStock.Font = new System.Drawing.Font("맑은 고딕", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnStock.Location = new System.Drawing.Point(1402, 76);
            this.BtnStock.Name = "BtnStock";
            this.BtnStock.Size = new System.Drawing.Size(150, 49);
            this.BtnStock.TabIndex = 3;
            this.BtnStock.Text = "재료 재고량";
            this.BtnStock.UseVisualStyleBackColor = true;
            this.BtnStock.Click += new System.EventHandler(this.BtnStock_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(1178, 38);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(374, 23);
            this.comboBox1.TabIndex = 1;
            // 
            // chart1
            // 
            chartArea10.AxisX.Interval = 1D;
            chartArea10.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea10);
            legend10.Name = "Legend1";
            this.chart1.Legends.Add(legend10);
            this.chart1.Location = new System.Drawing.Point(12, 38);
            this.chart1.Name = "chart1";
            series10.ChartArea = "ChartArea1";
            series10.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;
            series10.IsXValueIndexed = true;
            series10.Legend = "Legend1";
            series10.Name = "월별 판매량";
            this.chart1.Series.Add(series10);
            this.chart1.Size = new System.Drawing.Size(1130, 747);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // BtnSaleData
            // 
            this.BtnSaleData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSaleData.Font = new System.Drawing.Font("맑은 고딕", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnSaleData.Location = new System.Drawing.Point(1247, 76);
            this.BtnSaleData.Name = "BtnSaleData";
            this.BtnSaleData.Size = new System.Drawing.Size(150, 49);
            this.BtnSaleData.TabIndex = 4;
            this.BtnSaleData.Text = "총 판매량";
            this.BtnSaleData.UseVisualStyleBackColor = true;
            this.BtnSaleData.Click += new System.EventHandler(this.BtnSaleData_Click);
            // 
            // BtnSaleMoney
            // 
            this.BtnSaleMoney.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSaleMoney.Font = new System.Drawing.Font("맑은 고딕", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnSaleMoney.Location = new System.Drawing.Point(1247, 131);
            this.BtnSaleMoney.Name = "BtnSaleMoney";
            this.BtnSaleMoney.Size = new System.Drawing.Size(150, 49);
            this.BtnSaleMoney.TabIndex = 5;
            this.BtnSaleMoney.Text = "월별 버거판매량";
            this.BtnSaleMoney.UseVisualStyleBackColor = true;
            this.BtnSaleMoney.Click += new System.EventHandler(this.BtnSaleMoney_Click);
            // 
            // BtnIngreConsume
            // 
            this.BtnIngreConsume.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnIngreConsume.Font = new System.Drawing.Font("맑은 고딕", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnIngreConsume.Location = new System.Drawing.Point(1403, 131);
            this.BtnIngreConsume.Name = "BtnIngreConsume";
            this.BtnIngreConsume.Size = new System.Drawing.Size(150, 49);
            this.BtnIngreConsume.TabIndex = 6;
            this.BtnIngreConsume.Text = "월별 재료소모량";
            this.BtnIngreConsume.UseVisualStyleBackColor = true;
            this.BtnIngreConsume.Click += new System.EventHandler(this.BtnIngreConsume_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1573, 829);
            this.Controls.Add(this.BtnIngreConsume);
            this.Controls.Add(this.BtnSaleMoney);
            this.Controls.Add(this.BtnSaleData);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.BtnStock);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.RightToLeftLayout = true;
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.VisibleChanged += new System.EventHandler(this.Form1_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button BtnStock;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button BtnSaleData;
        private System.Windows.Forms.Button BtnSaleMoney;
        private System.Windows.Forms.Button BtnIngreConsume;
    }
}

