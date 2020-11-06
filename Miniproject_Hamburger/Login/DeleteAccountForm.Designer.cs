namespace Miniproject_Hamburger
{
    partial class DeleteAccountForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeleteAccountForm));
            this.계정삭제확인버튼 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.계정삭제정보id = new System.Windows.Forms.TextBox();
            this.계정삭제정보pw = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // 계정삭제확인버튼
            // 
            this.계정삭제확인버튼.Location = new System.Drawing.Point(439, 63);
            this.계정삭제확인버튼.Name = "계정삭제확인버튼";
            this.계정삭제확인버튼.Size = new System.Drawing.Size(88, 65);
            this.계정삭제확인버튼.TabIndex = 0;
            this.계정삭제확인버튼.Text = "확인";
            this.계정삭제확인버튼.UseVisualStyleBackColor = true;
            this.계정삭제확인버튼.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 15F);
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(535, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "삭제를 원하시는 계정의 정보를 입력해 주세요.";
            // 
            // 계정삭제정보id
            // 
            this.계정삭제정보id.Location = new System.Drawing.Point(106, 63);
            this.계정삭제정보id.Name = "계정삭제정보id";
            this.계정삭제정보id.Size = new System.Drawing.Size(315, 25);
            this.계정삭제정보id.TabIndex = 2;
            // 
            // 계정삭제정보pw
            // 
            this.계정삭제정보pw.Location = new System.Drawing.Point(106, 103);
            this.계정삭제정보pw.Name = "계정삭제정보pw";
            this.계정삭제정보pw.PasswordChar = '●';
            this.계정삭제정보pw.Size = new System.Drawing.Size(315, 25);
            this.계정삭제정보pw.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "ID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Password";
            // 
            // DeleteAccountForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 143);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.계정삭제정보pw);
            this.Controls.Add(this.계정삭제정보id);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.계정삭제확인버튼);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DeleteAccountForm";
            this.Text = "계정삭제";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button 계정삭제확인버튼;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox 계정삭제정보id;
        private System.Windows.Forms.TextBox 계정삭제정보pw;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}