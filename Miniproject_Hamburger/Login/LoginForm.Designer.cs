using System;
using Miniproject_Hamburger;

namespace MiniProject_Hamburger
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.TextBox_ID = new System.Windows.Forms.TextBox();
            this.TextBox_PW = new System.Windows.Forms.TextBox();
            this.ID_text = new System.Windows.Forms.Label();
            this.PW_text = new System.Windows.Forms.Label();
            this.Login_text = new System.Windows.Forms.Button();
            this.managerlogin_text = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TextBox_ID
            // 
            this.TextBox_ID.Location = new System.Drawing.Point(97, 129);
            this.TextBox_ID.Name = "TextBox_ID";
            this.TextBox_ID.Size = new System.Drawing.Size(341, 25);
            this.TextBox_ID.TabIndex = 0;
            // 
            // TextBox_PW
            // 
            this.TextBox_PW.Location = new System.Drawing.Point(97, 169);
            this.TextBox_PW.Name = "TextBox_PW";
            this.TextBox_PW.PasswordChar = '●';
            this.TextBox_PW.Size = new System.Drawing.Size(341, 25);
            this.TextBox_PW.TabIndex = 1;
            this.TextBox_PW.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_PW_KeyPress);
            // 
            // ID_text
            // 
            this.ID_text.AutoSize = true;
            this.ID_text.Font = new System.Drawing.Font("굴림", 14F);
            this.ID_text.Location = new System.Drawing.Point(53, 130);
            this.ID_text.Name = "ID_text";
            this.ID_text.Size = new System.Drawing.Size(29, 24);
            this.ID_text.TabIndex = 1;
            this.ID_text.Text = "ID";
            // 
            // PW_text
            // 
            this.PW_text.AutoSize = true;
            this.PW_text.Font = new System.Drawing.Font("굴림", 14F);
            this.PW_text.Location = new System.Drawing.Point(29, 170);
            this.PW_text.Name = "PW_text";
            this.PW_text.Size = new System.Drawing.Size(53, 24);
            this.PW_text.TabIndex = 1;
            this.PW_text.Text = " PW";
            // 
            // Login_text
            // 
            this.Login_text.Font = new System.Drawing.Font("굴림", 14F);
            this.Login_text.Location = new System.Drawing.Point(453, 128);
            this.Login_text.Name = "Login_text";
            this.Login_text.Size = new System.Drawing.Size(130, 73);
            this.Login_text.TabIndex = 2;
            this.Login_text.Text = "로그인";
            this.Login_text.UseVisualStyleBackColor = true;
            this.Login_text.Click += new System.EventHandler(this.Login_text_Click);
            // 
            // managerlogin_text
            // 
            this.managerlogin_text.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.managerlogin_text.AutoSize = true;
            this.managerlogin_text.Font = new System.Drawing.Font("굴림", 35F);
            this.managerlogin_text.Location = new System.Drawing.Point(125, 35);
            this.managerlogin_text.Name = "managerlogin_text";
            this.managerlogin_text.Size = new System.Drawing.Size(399, 59);
            this.managerlogin_text.TabIndex = 0;
            this.managerlogin_text.Text = "관리자 로그인";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(97, 215);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(179, 48);
            this.button1.TabIndex = 3;
            this.button1.Text = "관리자 계정추가/삭제";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(300, 215);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(138, 48);
            this.button2.TabIndex = 4;
            this.button2.Text = "ID/PW 찾기";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 279);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.managerlogin_text);
            this.Controls.Add(this.Login_text);
            this.Controls.Add(this.PW_text);
            this.Controls.Add(this.ID_text);
            this.Controls.Add(this.TextBox_PW);
            this.Controls.Add(this.TextBox_ID);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "관리자 로그인";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void button2_Click(object sender, EventArgs e)    // ID/PW 찾기 버튼시 
        {
            SelectSearchForm searchForm = new SelectSearchForm();
            searchForm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)    //관리자 계정 추가/삭제 버튼시
        {
            AddAccountForm addForm = new AddAccountForm();
            addForm.ShowDialog();
        }


        #endregion

        private System.Windows.Forms.TextBox TextBox_ID;
        private System.Windows.Forms.TextBox TextBox_PW;
        private System.Windows.Forms.Label ID_text;
        private System.Windows.Forms.Label PW_text;
        private System.Windows.Forms.Button Login_text;
        private System.Windows.Forms.Label managerlogin_text;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

