namespace Miniproject_Hamburger
{
    partial class HamMakerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HamMakerForm));
            this.M1progressBar = new System.Windows.Forms.ProgressBar();
            this.TxtM1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.FlpCompletePanel = new System.Windows.Forms.FlowLayoutPanel();
            this.HamImageList = new System.Windows.Forms.ImageList(this.components);
            this.TimOrderList = new System.Windows.Forms.Timer(this.components);
            this.M2progressBar = new System.Windows.Forms.ProgressBar();
            this.M3progressBar = new System.Windows.Forms.ProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.FlpOrderPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.TimAdmin = new System.Windows.Forms.Timer(this.components);
            this.TxtM2 = new System.Windows.Forms.Label();
            this.TxtM3 = new System.Windows.Forms.Label();
            this.TxtOrderNum1 = new System.Windows.Forms.Label();
            this.TxtOrderNum2 = new System.Windows.Forms.Label();
            this.TxtOrderNum3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.ImgHam1 = new System.Windows.Forms.PictureBox();
            this.TxtCompleteDelete = new System.Windows.Forms.Label();
            this.DeleteCompleteBtn = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.ImgHam2 = new System.Windows.Forms.PictureBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.ImgHam3 = new System.Windows.Forms.PictureBox();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImgHam1)).BeginInit();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImgHam2)).BeginInit();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImgHam3)).BeginInit();
            this.SuspendLayout();
            // 
            // M1progressBar
            // 
            this.M1progressBar.Location = new System.Drawing.Point(253, 186);
            this.M1progressBar.Name = "M1progressBar";
            this.M1progressBar.Size = new System.Drawing.Size(165, 25);
            this.M1progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.M1progressBar.TabIndex = 2;
            this.M1progressBar.Visible = false;
            // 
            // TxtM1
            // 
            this.TxtM1.Font = new System.Drawing.Font("맑은 고딕", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.TxtM1.Location = new System.Drawing.Point(3, 136);
            this.TxtM1.Name = "TxtM1";
            this.TxtM1.Size = new System.Drawing.Size(171, 37);
            this.TxtM1.TabIndex = 8;
            this.TxtM1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.BackColor = System.Drawing.SystemColors.Window;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.FlpCompletePanel);
            this.panel2.Location = new System.Drawing.Point(972, 50);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(273, 684);
            this.panel2.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.Location = new System.Drawing.Point(49, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(175, 33);
            this.label5.TabIndex = 16;
            this.label5.Text = "완료";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FlpCompletePanel
            // 
            this.FlpCompletePanel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.FlpCompletePanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.FlpCompletePanel.Location = new System.Drawing.Point(106, 36);
            this.FlpCompletePanel.Name = "FlpCompletePanel";
            this.FlpCompletePanel.Size = new System.Drawing.Size(162, 647);
            this.FlpCompletePanel.TabIndex = 14;
            // 
            // HamImageList
            // 
            this.HamImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("HamImageList.ImageStream")));
            this.HamImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.HamImageList.Images.SetKeyName(0, "BaseImage2.png");
            this.HamImageList.Images.SetKeyName(1, "BigMac.png");
            this.HamImageList.Images.SetKeyName(2, "Bulgogiburger.png");
            this.HamImageList.Images.SetKeyName(3, "Cheeseburger.png");
            this.HamImageList.Images.SetKeyName(4, "Macmorning.png");
            this.HamImageList.Images.SetKeyName(5, "shirimpburger.png");
            this.HamImageList.Images.SetKeyName(6, "begeburger.png");
            this.HamImageList.Images.SetKeyName(7, "potatoburger.png");
            this.HamImageList.Images.SetKeyName(8, "riceburger.png");
            this.HamImageList.Images.SetKeyName(9, "Newburger.png");
            // 
            // TimOrderList
            // 
            this.TimOrderList.Interval = 1000;
            // 
            // M2progressBar
            // 
            this.M2progressBar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.M2progressBar.Location = new System.Drawing.Point(253, 188);
            this.M2progressBar.Name = "M2progressBar";
            this.M2progressBar.Size = new System.Drawing.Size(165, 25);
            this.M2progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.M2progressBar.TabIndex = 16;
            this.M2progressBar.Visible = false;
            // 
            // M3progressBar
            // 
            this.M3progressBar.Location = new System.Drawing.Point(253, 181);
            this.M3progressBar.Name = "M3progressBar";
            this.M3progressBar.Size = new System.Drawing.Size(165, 25);
            this.M3progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.M3progressBar.TabIndex = 17;
            this.M3progressBar.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.FlpOrderPanel);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(687, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(279, 684);
            this.panel1.TabIndex = 15;
            // 
            // FlpOrderPanel
            // 
            this.FlpOrderPanel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.FlpOrderPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.FlpOrderPanel.Location = new System.Drawing.Point(110, 36);
            this.FlpOrderPanel.Name = "FlpOrderPanel";
            this.FlpOrderPanel.Size = new System.Drawing.Size(164, 647);
            this.FlpOrderPanel.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(51, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 33);
            this.label1.TabIndex = 15;
            this.label1.Text = "생산중";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TimAdmin
            // 
            this.TimAdmin.Interval = 300;
            this.TimAdmin.Tick += new System.EventHandler(this.AdminTick);
            // 
            // TxtM2
            // 
            this.TxtM2.Font = new System.Drawing.Font("맑은 고딕", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.TxtM2.Location = new System.Drawing.Point(3, 135);
            this.TxtM2.Name = "TxtM2";
            this.TxtM2.Size = new System.Drawing.Size(171, 37);
            this.TxtM2.TabIndex = 18;
            this.TxtM2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TxtM3
            // 
            this.TxtM3.Font = new System.Drawing.Font("맑은 고딕", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.TxtM3.Location = new System.Drawing.Point(3, 136);
            this.TxtM3.Name = "TxtM3";
            this.TxtM3.Size = new System.Drawing.Size(171, 37);
            this.TxtM3.TabIndex = 19;
            this.TxtM3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TxtOrderNum1
            // 
            this.TxtOrderNum1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.TxtOrderNum1.Font = new System.Drawing.Font("맑은 고딕", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.TxtOrderNum1.Location = new System.Drawing.Point(3, 37);
            this.TxtOrderNum1.Name = "TxtOrderNum1";
            this.TxtOrderNum1.Size = new System.Drawing.Size(167, 94);
            this.TxtOrderNum1.TabIndex = 20;
            this.TxtOrderNum1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TxtOrderNum2
            // 
            this.TxtOrderNum2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.TxtOrderNum2.Font = new System.Drawing.Font("맑은 고딕", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.TxtOrderNum2.Location = new System.Drawing.Point(3, 40);
            this.TxtOrderNum2.Name = "TxtOrderNum2";
            this.TxtOrderNum2.Size = new System.Drawing.Size(167, 97);
            this.TxtOrderNum2.TabIndex = 21;
            this.TxtOrderNum2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TxtOrderNum3
            // 
            this.TxtOrderNum3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.TxtOrderNum3.Font = new System.Drawing.Font("맑은 고딕", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.TxtOrderNum3.Location = new System.Drawing.Point(3, 38);
            this.TxtOrderNum3.Name = "TxtOrderNum3";
            this.TxtOrderNum3.Size = new System.Drawing.Size(167, 97);
            this.TxtOrderNum3.TabIndex = 22;
            this.TxtOrderNum3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Window;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.TxtM1);
            this.panel3.Controls.Add(this.TxtOrderNum1);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Location = new System.Drawing.Point(37, 29);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(175, 175);
            this.panel3.TabIndex = 24;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(-31, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(242, 33);
            this.label3.TabIndex = 26;
            this.label3.Text = "주문번호";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            this.panel4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel4.BackColor = System.Drawing.SystemColors.Window;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.TxtOrderNum2);
            this.panel4.Controls.Add(this.TxtM2);
            this.panel4.Location = new System.Drawing.Point(37, 23);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(175, 175);
            this.panel4.TabIndex = 25;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Font = new System.Drawing.Font("맑은 고딕", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.Location = new System.Drawing.Point(-31, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(242, 33);
            this.label4.TabIndex = 27;
            this.label4.Text = "주문번호";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.Window;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.TxtM3);
            this.panel5.Controls.Add(this.label6);
            this.panel5.Controls.Add(this.TxtOrderNum3);
            this.panel5.Location = new System.Drawing.Point(37, 24);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(175, 175);
            this.panel5.TabIndex = 25;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.Font = new System.Drawing.Font("맑은 고딕", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.Location = new System.Drawing.Point(-31, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(242, 33);
            this.label6.TabIndex = 28;
            this.label6.Text = "주문번호";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.Window;
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.panel3);
            this.panel6.Controls.Add(this.ImgHam1);
            this.panel6.Controls.Add(this.M1progressBar);
            this.panel6.Location = new System.Drawing.Point(217, 50);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(464, 224);
            this.panel6.TabIndex = 16;
            // 
            // ImgHam1
            // 
            this.ImgHam1.Location = new System.Drawing.Point(253, 21);
            this.ImgHam1.Name = "ImgHam1";
            this.ImgHam1.Size = new System.Drawing.Size(165, 165);
            this.ImgHam1.TabIndex = 13;
            this.ImgHam1.TabStop = false;
            // 
            // TxtCompleteDelete
            // 
            this.TxtCompleteDelete.Font = new System.Drawing.Font("맑은 고딕", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.TxtCompleteDelete.Location = new System.Drawing.Point(1280, 90);
            this.TxtCompleteDelete.Name = "TxtCompleteDelete";
            this.TxtCompleteDelete.Size = new System.Drawing.Size(149, 151);
            this.TxtCompleteDelete.TabIndex = 27;
            this.TxtCompleteDelete.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DeleteCompleteBtn
            // 
            this.DeleteCompleteBtn.BackColor = System.Drawing.SystemColors.Window;
            this.DeleteCompleteBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteCompleteBtn.Font = new System.Drawing.Font("맑은 고딕", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.DeleteCompleteBtn.Location = new System.Drawing.Point(1303, 244);
            this.DeleteCompleteBtn.Name = "DeleteCompleteBtn";
            this.DeleteCompleteBtn.Size = new System.Drawing.Size(113, 53);
            this.DeleteCompleteBtn.TabIndex = 28;
            this.DeleteCompleteBtn.Text = "삭제";
            this.DeleteCompleteBtn.UseVisualStyleBackColor = false;
            this.DeleteCompleteBtn.Click += new System.EventHandler(this.DeleteCompleteBtn_Click);
            // 
            // panel7
            // 
            this.panel7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.panel7.BackColor = System.Drawing.SystemColors.Window;
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.panel4);
            this.panel7.Controls.Add(this.M2progressBar);
            this.panel7.Controls.Add(this.ImgHam2);
            this.panel7.Location = new System.Drawing.Point(217, 280);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(464, 224);
            this.panel7.TabIndex = 29;
            // 
            // ImgHam2
            // 
            this.ImgHam2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ImgHam2.Location = new System.Drawing.Point(253, 23);
            this.ImgHam2.Name = "ImgHam2";
            this.ImgHam2.Size = new System.Drawing.Size(165, 165);
            this.ImgHam2.TabIndex = 14;
            this.ImgHam2.TabStop = false;
            // 
            // panel8
            // 
            this.panel8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel8.BackColor = System.Drawing.SystemColors.Window;
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel8.Controls.Add(this.panel5);
            this.panel8.Controls.Add(this.ImgHam3);
            this.panel8.Controls.Add(this.M3progressBar);
            this.panel8.Location = new System.Drawing.Point(217, 510);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(464, 224);
            this.panel8.TabIndex = 30;
            // 
            // ImgHam3
            // 
            this.ImgHam3.Location = new System.Drawing.Point(253, 16);
            this.ImgHam3.Name = "ImgHam3";
            this.ImgHam3.Size = new System.Drawing.Size(165, 165);
            this.ImgHam3.TabIndex = 15;
            this.ImgHam3.TabStop = false;
            // 
            // HamMakerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1479, 786);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.DeleteCompleteBtn);
            this.Controls.Add(this.TxtCompleteDelete);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "HamMakerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HamMakerForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.HamMakerForm_Load);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ImgHam1)).EndInit();
            this.panel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ImgHam2)).EndInit();
            this.panel8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ImgHam3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label TxtM1;
        public System.Windows.Forms.ProgressBar M1progressBar;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.FlowLayoutPanel FlpCompletePanel;
        private System.Windows.Forms.ImageList HamImageList;
        private System.Windows.Forms.PictureBox ImgHam1;
        private System.Windows.Forms.Timer TimOrderList;
        private System.Windows.Forms.PictureBox ImgHam2;
        private System.Windows.Forms.PictureBox ImgHam3;
        public System.Windows.Forms.ProgressBar M2progressBar;
        public System.Windows.Forms.ProgressBar M3progressBar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel FlpOrderPanel;
        private System.Windows.Forms.Timer TimAdmin;
        private System.Windows.Forms.Label TxtM2;
        private System.Windows.Forms.Label TxtM3;
        private System.Windows.Forms.Label TxtOrderNum1;
        private System.Windows.Forms.Label TxtOrderNum2;
        private System.Windows.Forms.Label TxtOrderNum3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label TxtCompleteDelete;
        private System.Windows.Forms.Button DeleteCompleteBtn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel8;
    }
}