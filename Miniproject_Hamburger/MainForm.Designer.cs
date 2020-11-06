namespace Miniproject_Hamburger
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.HamMachine1 = new System.Windows.Forms.Timer(this.components);
            this.TimOrderCheck = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnExit = new System.Windows.Forms.Button();
            this.BtnMinimized = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.OrderPage = new System.Windows.Forms.TabPage();
            this.MakerPage = new System.Windows.Forms.TabPage();
            this.SaleDataPage = new System.Windows.Forms.TabPage();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // HamMachine1
            // 
            this.HamMachine1.Tick += new System.EventHandler(this.HamMachine1_Tick);
            // 
            // TimOrderCheck
            // 
            this.TimOrderCheck.Interval = 500;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.BtnExit);
            this.panel1.Controls.Add(this.BtnMinimized);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(781, 485);
            this.panel1.TabIndex = 6;
            // 
            // BtnExit
            // 
            this.BtnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnExit.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BtnExit.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.ForeColor = System.Drawing.SystemColors.WindowText;
            this.BtnExit.Location = new System.Drawing.Point(742, 3);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(30, 30);
            this.BtnExit.TabIndex = 1;
            this.BtnExit.Text = "X";
            this.BtnExit.UseVisualStyleBackColor = false;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnMinimized
            // 
            this.BtnMinimized.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnMinimized.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BtnMinimized.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnMinimized.ForeColor = System.Drawing.SystemColors.MenuText;
            this.BtnMinimized.Location = new System.Drawing.Point(706, 3);
            this.BtnMinimized.Name = "BtnMinimized";
            this.BtnMinimized.Size = new System.Drawing.Size(30, 30);
            this.BtnMinimized.TabIndex = 2;
            this.BtnMinimized.Text = "_";
            this.BtnMinimized.UseVisualStyleBackColor = false;
            this.BtnMinimized.Click += new System.EventHandler(this.BtnMinimized_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("궁서체", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(1, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(204, 41);
            this.label1.TabIndex = 0;
            this.label1.Text = "버거관리자";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.OrderPage);
            this.tabControl1.Controls.Add(this.MakerPage);
            this.tabControl1.Controls.Add(this.SaleDataPage);
            this.tabControl1.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.tabControl1.Location = new System.Drawing.Point(7, 53);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(769, 418);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // OrderPage
            // 
            this.OrderPage.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.OrderPage.Location = new System.Drawing.Point(4, 37);
            this.OrderPage.Name = "OrderPage";
            this.OrderPage.Padding = new System.Windows.Forms.Padding(3);
            this.OrderPage.Size = new System.Drawing.Size(761, 377);
            this.OrderPage.TabIndex = 0;
            this.OrderPage.Text = "주문";
            this.OrderPage.UseVisualStyleBackColor = true;
            // 
            // MakerPage
            // 
            this.MakerPage.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.MakerPage.Location = new System.Drawing.Point(4, 37);
            this.MakerPage.Name = "MakerPage";
            this.MakerPage.Padding = new System.Windows.Forms.Padding(3);
            this.MakerPage.Size = new System.Drawing.Size(761, 377);
            this.MakerPage.TabIndex = 1;
            this.MakerPage.Text = "현황";
            this.MakerPage.UseVisualStyleBackColor = true;
            // 
            // SaleDataPage
            // 
            this.SaleDataPage.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.SaleDataPage.Location = new System.Drawing.Point(4, 37);
            this.SaleDataPage.Name = "SaleDataPage";
            this.SaleDataPage.Padding = new System.Windows.Forms.Padding(3);
            this.SaleDataPage.Size = new System.Drawing.Size(761, 377);
            this.SaleDataPage.TabIndex = 2;
            this.SaleDataPage.Text = "판매량 & 재고 ";
            this.SaleDataPage.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(784, 483);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "MainForm";
            this.Text = "버거 관리자";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.Timer HamMachine1;
        private System.Windows.Forms.Timer TimOrderCheck;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage OrderPage;
        private System.Windows.Forms.TabPage MakerPage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnMinimized;
        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.TabPage SaleDataPage;
    }
}

