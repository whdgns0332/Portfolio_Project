namespace SmartFactoryProject_Final
{
    partial class FRM_LogIn
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
            this.Txt_ID = new System.Windows.Forms.TextBox();
            this.Txt_PW = new System.Windows.Forms.TextBox();
            this.Btn_LogIn = new System.Windows.Forms.Button();
            this.Btn_Exit = new System.Windows.Forms.Button();
            this.Pnl_Drag = new System.Windows.Forms.Panel();
            this.Pic_Title = new System.Windows.Forms.PictureBox();
            this.Lbl_Title = new System.Windows.Forms.Label();
            this.Lbl_ID = new System.Windows.Forms.Label();
            this.Lbl_PW = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_Title)).BeginInit();
            this.SuspendLayout();
            // 
            // Txt_ID
            // 
            this.Txt_ID.BackColor = System.Drawing.Color.Khaki;
            this.Txt_ID.Font = new System.Drawing.Font("굴림", 22F);
            this.Txt_ID.Location = new System.Drawing.Point(173, 139);
            this.Txt_ID.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Txt_ID.Name = "Txt_ID";
            this.Txt_ID.Size = new System.Drawing.Size(355, 50);
            this.Txt_ID.TabIndex = 4;
            this.Txt_ID.Click += new System.EventHandler(this.Txt_ID_Click);
            // 
            // Txt_PW
            // 
            this.Txt_PW.BackColor = System.Drawing.Color.Khaki;
            this.Txt_PW.Font = new System.Drawing.Font("굴림", 22F);
            this.Txt_PW.Location = new System.Drawing.Point(173, 214);
            this.Txt_PW.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Txt_PW.Name = "Txt_PW";
            this.Txt_PW.PasswordChar = '●';
            this.Txt_PW.Size = new System.Drawing.Size(355, 50);
            this.Txt_PW.TabIndex = 5;
            this.Txt_PW.Click += new System.EventHandler(this.Txt_PW_Click);
            // 
            // Btn_LogIn
            // 
            this.Btn_LogIn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_LogIn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Btn_LogIn.Location = new System.Drawing.Point(187, 272);
            this.Btn_LogIn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_LogIn.Name = "Btn_LogIn";
            this.Btn_LogIn.Size = new System.Drawing.Size(125, 75);
            this.Btn_LogIn.TabIndex = 6;
            this.Btn_LogIn.Text = "로그인";
            this.Btn_LogIn.UseVisualStyleBackColor = true;
            this.Btn_LogIn.Click += new System.EventHandler(this.Btn_LogIn_Click);
            this.Btn_LogIn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btn_LogIn_MouseDown);
            this.Btn_LogIn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Btn_LogIn_MouseUp);
            // 
            // Btn_Exit
            // 
            this.Btn_Exit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Exit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Btn_Exit.Location = new System.Drawing.Point(403, 279);
            this.Btn_Exit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_Exit.Name = "Btn_Exit";
            this.Btn_Exit.Size = new System.Drawing.Size(125, 60);
            this.Btn_Exit.TabIndex = 7;
            this.Btn_Exit.UseVisualStyleBackColor = true;
            this.Btn_Exit.Click += new System.EventHandler(this.Btn_Exit_Click);
            this.Btn_Exit.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btn_Exit_MouseDown);
            this.Btn_Exit.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Btn_Exit_MouseUp);
            // 
            // Pnl_Drag
            // 
            this.Pnl_Drag.BackColor = System.Drawing.Color.Transparent;
            this.Pnl_Drag.Location = new System.Drawing.Point(1, 2);
            this.Pnl_Drag.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Pnl_Drag.Name = "Pnl_Drag";
            this.Pnl_Drag.Size = new System.Drawing.Size(717, 60);
            this.Pnl_Drag.TabIndex = 8;
            this.Pnl_Drag.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Pnl_Drag_MouseDown);
            this.Pnl_Drag.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Pnl_Drag_MouseMove);
            // 
            // Pic_Title
            // 
            this.Pic_Title.BackColor = System.Drawing.Color.Transparent;
            this.Pic_Title.Location = new System.Drawing.Point(58, 72);
            this.Pic_Title.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Pic_Title.Name = "Pic_Title";
            this.Pic_Title.Size = new System.Drawing.Size(70, 50);
            this.Pic_Title.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Pic_Title.TabIndex = 9;
            this.Pic_Title.TabStop = false;
            this.Pic_Title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Pnl_Drag_MouseDown);
            this.Pic_Title.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Pnl_Drag_MouseMove);
            // 
            // Lbl_Title
            // 
            this.Lbl_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Title.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Title.Location = new System.Drawing.Point(162, 86);
            this.Lbl_Title.Name = "Lbl_Title";
            this.Lbl_Title.Size = new System.Drawing.Size(150, 48);
            this.Lbl_Title.TabIndex = 10;
            this.Lbl_Title.Text = "로그인";
            this.Lbl_Title.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.Lbl_Title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Pnl_Drag_MouseDown);
            this.Lbl_Title.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Pnl_Drag_MouseMove);
            // 
            // Lbl_ID
            // 
            this.Lbl_ID.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_ID.Location = new System.Drawing.Point(94, 161);
            this.Lbl_ID.Name = "Lbl_ID";
            this.Lbl_ID.Size = new System.Drawing.Size(20, 15);
            this.Lbl_ID.TabIndex = 13;
            this.Lbl_ID.Text = "ID";
            this.Lbl_ID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_PW
            // 
            this.Lbl_PW.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_PW.Location = new System.Drawing.Point(94, 236);
            this.Lbl_PW.Name = "Lbl_PW";
            this.Lbl_PW.Size = new System.Drawing.Size(31, 15);
            this.Lbl_PW.TabIndex = 14;
            this.Lbl_PW.Text = "PW";
            this.Lbl_PW.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FRM_LogIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(720, 360);
            this.Controls.Add(this.Btn_Exit);
            this.Controls.Add(this.Lbl_PW);
            this.Controls.Add(this.Lbl_ID);
            this.Controls.Add(this.Lbl_Title);
            this.Controls.Add(this.Pic_Title);
            this.Controls.Add(this.Pnl_Drag);
            this.Controls.Add(this.Btn_LogIn);
            this.Controls.Add(this.Txt_PW);
            this.Controls.Add(this.Txt_ID);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FRM_LogIn";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Log In";
            this.Load += new System.EventHandler(this.Frm_Login_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Pic_Title)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox Txt_ID;
        private System.Windows.Forms.TextBox Txt_PW;
        private System.Windows.Forms.Button Btn_LogIn;
        private System.Windows.Forms.Button Btn_Exit;
        private System.Windows.Forms.Panel Pnl_Drag;
        private System.Windows.Forms.PictureBox Pic_Title;
        private System.Windows.Forms.Label Lbl_Title;
        private System.Windows.Forms.Label Lbl_ID;
        private System.Windows.Forms.Label Lbl_PW;
    }
}

