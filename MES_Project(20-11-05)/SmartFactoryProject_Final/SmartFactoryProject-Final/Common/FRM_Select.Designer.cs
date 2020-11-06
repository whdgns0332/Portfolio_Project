namespace SmartFactoryProject_Final.Common
{
    partial class FRM_Select
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
            this.Flp_Content = new System.Windows.Forms.FlowLayoutPanel();
            this.Btn_ScrUp = new System.Windows.Forms.Button();
            this.Btn_ScrDn = new System.Windows.Forms.Button();
            this.Lbl_Title = new System.Windows.Forms.Label();
            this.Btn_Exit = new System.Windows.Forms.Button();
            this.Pnl_BG = new System.Windows.Forms.Panel();
            this.Pnl_Drag = new System.Windows.Forms.Panel();
            this.Pnl_BG.SuspendLayout();
            this.Pnl_Drag.SuspendLayout();
            this.SuspendLayout();
            // 
            // Flp_Content
            // 
            this.Flp_Content.AutoScroll = true;
            this.Flp_Content.Location = new System.Drawing.Point(0, 0);
            this.Flp_Content.Name = "Flp_Content";
            this.Flp_Content.Padding = new System.Windows.Forms.Padding(15);
            this.Flp_Content.Size = new System.Drawing.Size(400, 455);
            this.Flp_Content.TabIndex = 0;
            // 
            // Btn_ScrUp
            // 
            this.Btn_ScrUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_ScrUp.Font = new System.Drawing.Font("Tahoma", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_ScrUp.Location = new System.Drawing.Point(456, 88);
            this.Btn_ScrUp.Name = "Btn_ScrUp";
            this.Btn_ScrUp.Size = new System.Drawing.Size(57, 252);
            this.Btn_ScrUp.TabIndex = 1;
            this.Btn_ScrUp.UseVisualStyleBackColor = true;
            this.Btn_ScrUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btn_ScrUp_MouseDown);
            this.Btn_ScrUp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Btn_ScrUp_MouseUp);
            // 
            // Btn_ScrDn
            // 
            this.Btn_ScrDn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_ScrDn.Font = new System.Drawing.Font("Tahoma", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_ScrDn.Location = new System.Drawing.Point(456, 396);
            this.Btn_ScrDn.Name = "Btn_ScrDn";
            this.Btn_ScrDn.Size = new System.Drawing.Size(57, 252);
            this.Btn_ScrDn.TabIndex = 2;
            this.Btn_ScrDn.UseVisualStyleBackColor = true;
            this.Btn_ScrDn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btn_ScrDn_MouseDown);
            this.Btn_ScrDn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Btn_ScrDn_MouseUp);
            // 
            // Lbl_Title
            // 
            this.Lbl_Title.Font = new System.Drawing.Font("굴림", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Lbl_Title.Location = new System.Drawing.Point(228, 21);
            this.Lbl_Title.Name = "Lbl_Title";
            this.Lbl_Title.Size = new System.Drawing.Size(205, 38);
            this.Lbl_Title.TabIndex = 3;
            this.Lbl_Title.Text = "선택해 주세요";
            this.Lbl_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Lbl_Title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Pnl_Drag_MouseDown);
            this.Lbl_Title.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Pnl_Drag_MouseMove);
            // 
            // Btn_Exit
            // 
            this.Btn_Exit.BackColor = System.Drawing.Color.Transparent;
            this.Btn_Exit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Exit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Btn_Exit.Location = new System.Drawing.Point(203, 615);
            this.Btn_Exit.Name = "Btn_Exit";
            this.Btn_Exit.Size = new System.Drawing.Size(153, 52);
            this.Btn_Exit.TabIndex = 4;
            this.Btn_Exit.UseVisualStyleBackColor = true;
            this.Btn_Exit.Click += new System.EventHandler(this.Btn_Exit_Click);
            this.Btn_Exit.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btn_Exit_MouseDown);
            this.Btn_Exit.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Btn_Exit_MouseUp);
            // 
            // Pnl_BG
            // 
            this.Pnl_BG.BackColor = System.Drawing.Color.Transparent;
            this.Pnl_BG.Controls.Add(this.Flp_Content);
            this.Pnl_BG.Location = new System.Drawing.Point(24, 134);
            this.Pnl_BG.Name = "Pnl_BG";
            this.Pnl_BG.Size = new System.Drawing.Size(426, 475);
            this.Pnl_BG.TabIndex = 0;
            // 
            // Pnl_Drag
            // 
            this.Pnl_Drag.BackColor = System.Drawing.Color.Transparent;
            this.Pnl_Drag.Controls.Add(this.Lbl_Title);
            this.Pnl_Drag.Location = new System.Drawing.Point(0, 0);
            this.Pnl_Drag.Name = "Pnl_Drag";
            this.Pnl_Drag.Size = new System.Drawing.Size(640, 80);
            this.Pnl_Drag.TabIndex = 5;
            this.Pnl_Drag.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Pnl_Drag_MouseDown);
            this.Pnl_Drag.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Pnl_Drag_MouseMove);
            // 
            // FRM_Select
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(640, 720);
            this.Controls.Add(this.Pnl_Drag);
            this.Controls.Add(this.Pnl_BG);
            this.Controls.Add(this.Btn_Exit);
            this.Controls.Add(this.Btn_ScrDn);
            this.Controls.Add(this.Btn_ScrUp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FRM_Select";
            this.Text = "FRM_Select";
            this.Load += new System.EventHandler(this.FRM_Select_Load);
            this.Pnl_BG.ResumeLayout(false);
            this.Pnl_Drag.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel Flp_Content;
        private System.Windows.Forms.Button Btn_ScrUp;
        private System.Windows.Forms.Button Btn_ScrDn;
        private System.Windows.Forms.Label Lbl_Title;
        private System.Windows.Forms.Button Btn_Exit;
        private System.Windows.Forms.Panel Pnl_BG;
        private System.Windows.Forms.Panel Pnl_Drag;
    }
}