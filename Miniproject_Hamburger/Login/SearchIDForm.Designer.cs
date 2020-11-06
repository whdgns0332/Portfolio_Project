namespace Miniproject_Hamburger
{
    partial class SearchIDForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchIDForm));
            this.ID찾기이름 = new System.Windows.Forms.Label();
            this.TextBox_Mobile = new System.Windows.Forms.TextBox();
            this.TextBox_Name = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.ID찾기전화번호 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ID찾기이름
            // 
            this.ID찾기이름.AutoSize = true;
            this.ID찾기이름.Location = new System.Drawing.Point(38, 90);
            this.ID찾기이름.Name = "ID찾기이름";
            this.ID찾기이름.Size = new System.Drawing.Size(37, 15);
            this.ID찾기이름.TabIndex = 1;
            this.ID찾기이름.Text = "이름";
            // 
            // TextBox_Mobile
            // 
            this.TextBox_Mobile.Location = new System.Drawing.Point(85, 52);
            this.TextBox_Mobile.Name = "TextBox_Mobile";
            this.TextBox_Mobile.Size = new System.Drawing.Size(177, 25);
            this.TextBox_Mobile.TabIndex = 0;
            this.TextBox_Mobile.TextChanged += new System.EventHandler(this.TextBox_Mobile_TextChanged);
            // 
            // TextBox_Name
            // 
            this.TextBox_Name.Location = new System.Drawing.Point(85, 86);
            this.TextBox_Name.Name = "TextBox_Name";
            this.TextBox_Name.Size = new System.Drawing.Size(177, 25);
            this.TextBox_Name.TabIndex = 1;
            this.TextBox_Name.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_Name_KeyPress);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(282, 47);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(92, 98);
            this.button1.TabIndex = 2;
            this.button1.Text = "확인";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ID찾기전화번호
            // 
            this.ID찾기전화번호.AutoSize = true;
            this.ID찾기전화번호.Location = new System.Drawing.Point(12, 55);
            this.ID찾기전화번호.Name = "ID찾기전화번호";
            this.ID찾기전화번호.Size = new System.Drawing.Size(67, 15);
            this.ID찾기전화번호.TabIndex = 0;
            this.ID찾기전화번호.Text = "전화번호";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(362, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "찾으려는 계정의 패스워드와 명의자를 입력해주세요.";
            // 
            // SearchIDForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 161);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.TextBox_Name);
            this.Controls.Add(this.TextBox_Mobile);
            this.Controls.Add(this.ID찾기이름);
            this.Controls.Add(this.ID찾기전화번호);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SearchIDForm";
            this.Text = "아이디찾기";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label ID찾기이름;
        private System.Windows.Forms.TextBox TextBox_Mobile;
        private System.Windows.Forms.TextBox TextBox_Name;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label ID찾기전화번호;
        private System.Windows.Forms.Label label1;
    }
}