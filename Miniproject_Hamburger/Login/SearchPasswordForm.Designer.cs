namespace Miniproject_Hamburger
{
    partial class SearchPasswordForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchPasswordForm));
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TextBox_ID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TextBox_Mobile = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(362, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "찾으려는 계정의 아이디와 전화번호를 입력해주세요.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "전화번호";
            // 
            // TextBox_ID
            // 
            this.TextBox_ID.Location = new System.Drawing.Point(70, 39);
            this.TextBox_ID.Name = "TextBox_ID";
            this.TextBox_ID.Size = new System.Drawing.Size(177, 25);
            this.TextBox_ID.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "아이디";
            // 
            // TextBox_Mobile
            // 
            this.TextBox_Mobile.Location = new System.Drawing.Point(70, 72);
            this.TextBox_Mobile.Name = "TextBox_Mobile";
            this.TextBox_Mobile.Size = new System.Drawing.Size(177, 25);
            this.TextBox_Mobile.TabIndex = 1;
            this.TextBox_Mobile.TextChanged += new System.EventHandler(this.TextBox_Mobile_TextChanged);
            this.TextBox_Mobile.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_Mobile_KeyPress);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(262, 39);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 58);
            this.button1.TabIndex = 2;
            this.button1.Text = "확인";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SearchPasswordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 109);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.TextBox_Mobile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TextBox_ID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SearchPasswordForm";
            this.Text = "패스워드찾기";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TextBox_ID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TextBox_Mobile;
        private System.Windows.Forms.Button button1;
    }
}