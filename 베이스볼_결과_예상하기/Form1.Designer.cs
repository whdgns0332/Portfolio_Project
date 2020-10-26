namespace 베이스볼_결과_예상하기
{
    partial class Form1
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
            this.AnswerRecordList = new System.Windows.Forms.ListBox();
            this.PossibleAnswerList = new System.Windows.Forms.ListBox();
            this.BtnStart = new System.Windows.Forms.Button();
            this.BtnRetry = new System.Windows.Forms.Button();
            this.BtnReset = new System.Windows.Forms.Button();
            this.LblMessage = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // AnswerRecordList
            // 
            this.AnswerRecordList.FormattingEnabled = true;
            this.AnswerRecordList.ItemHeight = 15;
            this.AnswerRecordList.Location = new System.Drawing.Point(38, 39);
            this.AnswerRecordList.Name = "AnswerRecordList";
            this.AnswerRecordList.Size = new System.Drawing.Size(144, 364);
            this.AnswerRecordList.TabIndex = 0;
            // 
            // PossibleAnswerList
            // 
            this.PossibleAnswerList.FormattingEnabled = true;
            this.PossibleAnswerList.ItemHeight = 15;
            this.PossibleAnswerList.Location = new System.Drawing.Point(610, 39);
            this.PossibleAnswerList.Name = "PossibleAnswerList";
            this.PossibleAnswerList.Size = new System.Drawing.Size(154, 364);
            this.PossibleAnswerList.TabIndex = 1;
            // 
            // BtnStart
            // 
            this.BtnStart.Location = new System.Drawing.Point(210, 321);
            this.BtnStart.Name = "BtnStart";
            this.BtnStart.Size = new System.Drawing.Size(98, 40);
            this.BtnStart.TabIndex = 4;
            this.BtnStart.Text = "Start";
            this.BtnStart.UseVisualStyleBackColor = true;
            this.BtnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // BtnRetry
            // 
            this.BtnRetry.Enabled = false;
            this.BtnRetry.Location = new System.Drawing.Point(350, 321);
            this.BtnRetry.Name = "BtnRetry";
            this.BtnRetry.Size = new System.Drawing.Size(98, 40);
            this.BtnRetry.TabIndex = 4;
            this.BtnRetry.Text = "Retry";
            this.BtnRetry.UseVisualStyleBackColor = true;
            this.BtnRetry.Click += new System.EventHandler(this.BtnRetry_Click);
            // 
            // BtnReset
            // 
            this.BtnReset.Location = new System.Drawing.Point(481, 321);
            this.BtnReset.Name = "BtnReset";
            this.BtnReset.Size = new System.Drawing.Size(98, 40);
            this.BtnReset.TabIndex = 4;
            this.BtnReset.Text = "Reset";
            this.BtnReset.UseVisualStyleBackColor = true;
            this.BtnReset.Click += new System.EventHandler(this.BtnReset_Click);
            // 
            // LblMessage
            // 
            this.LblMessage.Location = new System.Drawing.Point(249, 378);
            this.LblMessage.Name = "LblMessage";
            this.LblMessage.Size = new System.Drawing.Size(291, 58);
            this.LblMessage.TabIndex = 5;
            this.LblMessage.Text = "숫자 버튼 세 개를 클릭하세요.";
            this.LblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "시도한 번호 목록";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(649, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "정답 후보";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 453);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LblMessage);
            this.Controls.Add(this.BtnReset);
            this.Controls.Add(this.BtnRetry);
            this.Controls.Add(this.BtnStart);
            this.Controls.Add(this.PossibleAnswerList);
            this.Controls.Add(this.AnswerRecordList);
            this.Name = "Form1";
            this.Text = "베이스볼";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox AnswerRecordList;
        private System.Windows.Forms.ListBox PossibleAnswerList;
        private System.Windows.Forms.Button BtnStart;
        private System.Windows.Forms.Button BtnRetry;
        private System.Windows.Forms.Button BtnReset;
        private System.Windows.Forms.Label LblMessage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

