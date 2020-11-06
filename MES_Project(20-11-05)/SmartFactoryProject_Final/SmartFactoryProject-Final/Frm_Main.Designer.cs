namespace SmartFactoryProject_Final
{
    partial class FRM_Main
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
            this.Btn_Logout = new System.Windows.Forms.Button();
            this.Btn_Process = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.Btn_Exit = new System.Windows.Forms.Button();
            this.Pnl_Drag = new System.Windows.Forms.Panel();
            this.Pnl_Drag.SuspendLayout();
            this.SuspendLayout();
            // 
            // Btn_Logout
            // 
            this.Btn_Logout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Logout.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Btn_Logout.Location = new System.Drawing.Point(954, 22);
            this.Btn_Logout.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_Logout.Name = "Btn_Logout";
            this.Btn_Logout.Size = new System.Drawing.Size(70, 32);
            this.Btn_Logout.TabIndex = 0;
            this.Btn_Logout.Text = "로그아웃";
            this.Btn_Logout.UseVisualStyleBackColor = true;
            this.Btn_Logout.Click += new System.EventHandler(this.Btn_Logout_Click);
            this.Btn_Logout.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btn_Logout_MouseDown);
            this.Btn_Logout.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Btn_Logout_MouseUp);
            // 
            // Btn_Process
            // 
            this.Btn_Process.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Process.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Btn_Process.Location = new System.Drawing.Point(164, 22);
            this.Btn_Process.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_Process.Name = "Btn_Process";
            this.Btn_Process.Size = new System.Drawing.Size(70, 32);
            this.Btn_Process.TabIndex = 2;
            this.Btn_Process.Text = "공정확인";
            this.Btn_Process.UseVisualStyleBackColor = true;
            this.Btn_Process.Click += new System.EventHandler(this.Btn_Process_Click);
            this.Btn_Process.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btn_Process_MouseDown);
            this.Btn_Process.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Btn_Process_MouseUp);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Location = new System.Drawing.Point(112, 77);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1008, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            // 
            // Btn_Exit
            // 
            this.Btn_Exit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Exit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Btn_Exit.Location = new System.Drawing.Point(1039, 22);
            this.Btn_Exit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_Exit.Name = "Btn_Exit";
            this.Btn_Exit.Size = new System.Drawing.Size(66, 32);
            this.Btn_Exit.TabIndex = 6;
            this.Btn_Exit.Text = "종료";
            this.Btn_Exit.UseVisualStyleBackColor = true;
            this.Btn_Exit.Click += new System.EventHandler(this.Btn_Exit_Click);
            this.Btn_Exit.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btn_Exit_MouseDown);
            this.Btn_Exit.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Btn_Exit_MouseUp);
            // 
            // Pnl_Drag
            // 
            this.Pnl_Drag.BackColor = System.Drawing.Color.Transparent;
            this.Pnl_Drag.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Pnl_Drag.Controls.Add(this.Btn_Process);
            this.Pnl_Drag.Controls.Add(this.Btn_Exit);
            this.Pnl_Drag.Controls.Add(this.Btn_Logout);
            this.Pnl_Drag.Location = new System.Drawing.Point(0, 0);
            this.Pnl_Drag.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Pnl_Drag.Name = "Pnl_Drag";
            this.Pnl_Drag.Size = new System.Drawing.Size(1120, 77);
            this.Pnl_Drag.TabIndex = 7;
            this.Pnl_Drag.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Pnl_Drag_MouseDown);
            this.Pnl_Drag.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Pnl_Drag_MouseMove);
            // 
            // FRM_Main
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1280, 1024);
            this.Controls.Add(this.Pnl_Drag);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FRM_Main";
            this.Padding = new System.Windows.Forms.Padding(0, 77, 0, 0);
            this.Load += new System.EventHandler(this.Frm_Main_Load);
            this.Pnl_Drag.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Btn_Logout;
        private System.Windows.Forms.Button Btn_Process;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Button Btn_Exit;
        private System.Windows.Forms.Panel Pnl_Drag;
    }
}