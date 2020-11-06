using System;

namespace Miniproject_Hamburger
{
    partial class AddAccountForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddAccountForm));
            this.new_managerinst = new System.Windows.Forms.Button();
            this.TextBox_ID = new System.Windows.Forms.TextBox();
            this.TextBox_Password = new System.Windows.Forms.TextBox();
            this.Nameread = new System.Windows.Forms.Label();
            this.ageread = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TextBox_Name = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TextBox_Add = new System.Windows.Forms.TextBox();
            this.TextBox_Mobile = new System.Windows.Forms.TextBox();
            this.Button_DeleteAcc = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // new_managerinst
            // 
            this.new_managerinst.Location = new System.Drawing.Point(17, 249);
            this.new_managerinst.Name = "new_managerinst";
            this.new_managerinst.Size = new System.Drawing.Size(318, 55);
            this.new_managerinst.TabIndex = 5;
            this.new_managerinst.Text = "신규 가입";
            this.new_managerinst.UseVisualStyleBackColor = true;
            this.new_managerinst.Click += new System.EventHandler(this.button1_Click);
            // 
            // TextBox_ID
            // 
            this.TextBox_ID.Location = new System.Drawing.Point(93, 88);
            this.TextBox_ID.Name = "TextBox_ID";
            this.TextBox_ID.Size = new System.Drawing.Size(242, 25);
            this.TextBox_ID.TabIndex = 0;
            // 
            // TextBox_Password
            // 
            this.TextBox_Password.Location = new System.Drawing.Point(93, 119);
            this.TextBox_Password.Name = "TextBox_Password";
            this.TextBox_Password.PasswordChar = '●';
            this.TextBox_Password.Size = new System.Drawing.Size(242, 25);
            this.TextBox_Password.TabIndex = 1;
            // 
            // Nameread
            // 
            this.Nameread.AutoSize = true;
            this.Nameread.Location = new System.Drawing.Point(58, 91);
            this.Nameread.Name = "Nameread";
            this.Nameread.Size = new System.Drawing.Size(32, 15);
            this.Nameread.TabIndex = 2;
            this.Nameread.Text = "(ID)";
            // 
            // ageread
            // 
            this.ageread.AutoSize = true;
            this.ageread.Location = new System.Drawing.Point(5, 122);
            this.ageread.Name = "ageread";
            this.ageread.Size = new System.Drawing.Size(84, 15);
            this.ageread.TabIndex = 2;
            this.ageread.Text = "(Password)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 25F);
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(368, 42);
            this.label1.TabIndex = 3;
            this.label1.Text = "[관리자계정 관리]";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 157);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "(이름)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 190);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "(주소지)";
            // 
            // TextBox_Name
            // 
            this.TextBox_Name.Location = new System.Drawing.Point(93, 155);
            this.TextBox_Name.Name = "TextBox_Name";
            this.TextBox_Name.Size = new System.Drawing.Size(242, 25);
            this.TextBox_Name.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0, 221);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 15);
            this.label4.TabIndex = 5;
            this.label4.Text = "(휴대폰번호)";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // TextBox_Add
            // 
            this.TextBox_Add.Location = new System.Drawing.Point(93, 186);
            this.TextBox_Add.Name = "TextBox_Add";
            this.TextBox_Add.Size = new System.Drawing.Size(242, 25);
            this.TextBox_Add.TabIndex = 3;
            // 
            // TextBox_Mobile
            // 
            this.TextBox_Mobile.Location = new System.Drawing.Point(94, 218);
            this.TextBox_Mobile.Name = "TextBox_Mobile";
            this.TextBox_Mobile.Size = new System.Drawing.Size(242, 25);
            this.TextBox_Mobile.TabIndex = 4;
            this.TextBox_Mobile.TextChanged += new System.EventHandler(this.TextBox_Mobile_TextChanged);
            this.TextBox_Mobile.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_Mobile_KeyPress);
            // 
            // Button_DeleteAcc
            // 
            this.Button_DeleteAcc.Location = new System.Drawing.Point(17, 344);
            this.Button_DeleteAcc.Name = "Button_DeleteAcc";
            this.Button_DeleteAcc.Size = new System.Drawing.Size(318, 50);
            this.Button_DeleteAcc.TabIndex = 6;
            this.Button_DeleteAcc.Text = " 계정삭제";
            this.Button_DeleteAcc.UseVisualStyleBackColor = true;
            this.Button_DeleteAcc.Click += new System.EventHandler(this.Button_DeleteAcc_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(61, 316);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(244, 15);
            this.label5.TabIndex = 8;
            this.label5.Text = "▼ 계정을 삭제하시려면 눌러주세요";
            // 
            // AddAccountForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 406);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Button_DeleteAcc);
            this.Controls.Add(this.TextBox_Mobile);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TextBox_Add);
            this.Controls.Add(this.TextBox_Name);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ageread);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Nameread);
            this.Controls.Add(this.TextBox_Password);
            this.Controls.Add(this.TextBox_ID);
            this.Controls.Add(this.new_managerinst);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddAccountForm";
            this.Text = "관리자계정관리";
            this.Load += new System.EventHandler(this.관리자신규가입_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void 관리자신규가입_Load(object sender, EventArgs e)
        {
           
        }

        private void label4_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.Button new_managerinst;
        private System.Windows.Forms.TextBox TextBox_ID;
        private System.Windows.Forms.TextBox TextBox_Password;
        private System.Windows.Forms.Label Nameread;
        private System.Windows.Forms.Label ageread;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TextBox_Name;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TextBox_Add;
        private System.Windows.Forms.TextBox TextBox_Mobile;
        private System.Windows.Forms.Button Button_DeleteAcc;
        private System.Windows.Forms.Label label5;
    }
}