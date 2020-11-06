namespace Miniproject_Hamburger
{
    partial class SelectSearchForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectSearchForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.id찾기버튼 = new System.Windows.Forms.Button();
            this.패스워드찾기버튼 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(246, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 15);
            this.label1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("굴림", 10F);
            this.label2.Location = new System.Drawing.Point(9, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(261, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "찾으시려는 항목을 선택해주세요.";
            // 
            // id찾기버튼
            // 
            this.id찾기버튼.Location = new System.Drawing.Point(12, 47);
            this.id찾기버튼.Name = "id찾기버튼";
            this.id찾기버튼.Size = new System.Drawing.Size(258, 31);
            this.id찾기버튼.TabIndex = 5;
            this.id찾기버튼.Text = "ID";
            this.id찾기버튼.UseVisualStyleBackColor = true;
            this.id찾기버튼.Click += new System.EventHandler(this.id찾기버튼_Click);
            // 
            // 패스워드찾기버튼
            // 
            this.패스워드찾기버튼.Location = new System.Drawing.Point(12, 95);
            this.패스워드찾기버튼.Name = "패스워드찾기버튼";
            this.패스워드찾기버튼.Size = new System.Drawing.Size(258, 28);
            this.패스워드찾기버튼.TabIndex = 5;
            this.패스워드찾기버튼.Text = "Password";
            this.패스워드찾기버튼.UseVisualStyleBackColor = true;
            this.패스워드찾기버튼.Click += new System.EventHandler(this.패스워드찾기버튼_Click);
            // 
            // SelectSearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(296, 149);
            this.Controls.Add(this.패스워드찾기버튼);
            this.Controls.Add(this.id찾기버튼);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SelectSearchForm";
            this.Text = "계정찾기";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button id찾기버튼;
        private System.Windows.Forms.Button 패스워드찾기버튼;
    }
}