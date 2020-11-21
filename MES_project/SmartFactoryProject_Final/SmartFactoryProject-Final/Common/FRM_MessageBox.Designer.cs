namespace SmartFactoryProject_Final.Common
{
    partial class FRM_MessageBox
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
            this.Lbl_Content = new System.Windows.Forms.Label();
            this.Lbl_Title = new System.Windows.Forms.Label();
            this.Btn_First = new System.Windows.Forms.Button();
            this.Btn_Second = new System.Windows.Forms.Button();
            this.Pnl_Drag = new System.Windows.Forms.Panel();
            this.Btn_Third = new System.Windows.Forms.Button();
            this.Pnl_Drag.SuspendLayout();
            this.SuspendLayout();
            // 
            // Lbl_Content
            // 
            this.Lbl_Content.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Content.Location = new System.Drawing.Point(12, 85);
            this.Lbl_Content.Name = "Lbl_Content";
            this.Lbl_Content.Size = new System.Drawing.Size(616, 215);
            this.Lbl_Content.TabIndex = 0;
            this.Lbl_Content.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_Title
            // 
            this.Lbl_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Title.ForeColor = System.Drawing.SystemColors.Desktop;
            this.Lbl_Title.Location = new System.Drawing.Point(12, 11);
            this.Lbl_Title.Name = "Lbl_Title";
            this.Lbl_Title.Size = new System.Drawing.Size(616, 50);
            this.Lbl_Title.TabIndex = 1;
            this.Lbl_Title.Text = "Title";
            this.Lbl_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Lbl_Title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Pnl_Drag_MouseDown);
            this.Lbl_Title.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Pnl_Drag_MouseMove);
            // 
            // Btn_First
            // 
            this.Btn_First.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_First.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_First.Location = new System.Drawing.Point(65, 307);
            this.Btn_First.Name = "Btn_First";
            this.Btn_First.Size = new System.Drawing.Size(140, 40);
            this.Btn_First.TabIndex = 2;
            this.Btn_First.UseVisualStyleBackColor = true;
            // 
            // Btn_Second
            // 
            this.Btn_Second.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Second.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Second.Location = new System.Drawing.Point(261, 308);
            this.Btn_Second.Name = "Btn_Second";
            this.Btn_Second.Size = new System.Drawing.Size(140, 40);
            this.Btn_Second.TabIndex = 3;
            this.Btn_Second.UseVisualStyleBackColor = true;
            // 
            // Pnl_Drag
            // 
            this.Pnl_Drag.BackColor = System.Drawing.Color.Transparent;
            this.Pnl_Drag.Controls.Add(this.Lbl_Title);
            this.Pnl_Drag.Location = new System.Drawing.Point(0, 0);
            this.Pnl_Drag.Name = "Pnl_Drag";
            this.Pnl_Drag.Size = new System.Drawing.Size(640, 82);
            this.Pnl_Drag.TabIndex = 4;
            this.Pnl_Drag.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Pnl_Drag_MouseDown);
            this.Pnl_Drag.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Pnl_Drag_MouseMove);
            // 
            // Btn_Third
            // 
            this.Btn_Third.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Third.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Third.Location = new System.Drawing.Point(462, 308);
            this.Btn_Third.Name = "Btn_Third";
            this.Btn_Third.Size = new System.Drawing.Size(140, 40);
            this.Btn_Third.TabIndex = 5;
            this.Btn_Third.UseVisualStyleBackColor = true;
            // 
            // FRM_MessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(640, 360);
            this.Controls.Add(this.Btn_Third);
            this.Controls.Add(this.Pnl_Drag);
            this.Controls.Add(this.Btn_Second);
            this.Controls.Add(this.Btn_First);
            this.Controls.Add(this.Lbl_Content);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FRM_MessageBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FRM_MessageBox";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FRM_MessageBox_FormClosed);
            this.Load += new System.EventHandler(this.FRM_MessageBox_Load);
            this.Pnl_Drag.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label Lbl_Content;
        private System.Windows.Forms.Label Lbl_Title;
        private System.Windows.Forms.Button Btn_First;
        private System.Windows.Forms.Button Btn_Second;
        private System.Windows.Forms.Panel Pnl_Drag;
        private System.Windows.Forms.Button Btn_Third;
    }
}