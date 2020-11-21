namespace SmartFactoryProject_Final_IOServer
{
    partial class FRM_Main
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
            this.components = new System.ComponentModel.Container();
            this.Lbl_Time = new System.Windows.Forms.Label();
            this.Lbl_IPName = new System.Windows.Forms.Label();
            this.Lbl_PortName = new System.Windows.Forms.Label();
            this.Lbl_IPVal = new System.Windows.Forms.Label();
            this.Lbl_PortVal = new System.Windows.Forms.Label();
            this.Lbl_Connect = new System.Windows.Forms.Label();
            this.Btn_PLCMode = new System.Windows.Forms.Button();
            this.Btn_AutoMode = new System.Windows.Forms.Button();
            this.Btn_Start = new System.Windows.Forms.Button();
            this.Btn_Stop = new System.Windows.Forms.Button();
            this.Dgv_Data = new System.Windows.Forms.DataGridView();
            this.Tim_TimeCheck = new System.Windows.Forms.Timer(this.components);
            this.Excel_Data = new unvell.ReoGrid.ReoGridControl();
            this.Btn_ItvChange = new System.Windows.Forms.Button();
            this.Txt_TempItv = new System.Windows.Forms.TextBox();
            this.Txt_AmoItv = new System.Windows.Forms.TextBox();
            this.Lbl_Temp_Itv = new System.Windows.Forms.Label();
            this.Lbl_Amo_Itv = new System.Windows.Forms.Label();
            this.Lbl_Temp_Unit = new System.Windows.Forms.Label();
            this.Lbl_Amo_Unit = new System.Windows.Forms.Label();
            this.Tim_Temp = new System.Windows.Forms.Timer(this.components);
            this.Tim_Amount = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Data)).BeginInit();
            this.SuspendLayout();
            // 
            // Lbl_Time
            // 
            this.Lbl_Time.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Time.Location = new System.Drawing.Point(12, 9);
            this.Lbl_Time.Name = "Lbl_Time";
            this.Lbl_Time.Size = new System.Drawing.Size(37, 15);
            this.Lbl_Time.TabIndex = 0;
            this.Lbl_Time.Text = "Time";
            this.Lbl_Time.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_IPName
            // 
            this.Lbl_IPName.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_IPName.Location = new System.Drawing.Point(359, 13);
            this.Lbl_IPName.Name = "Lbl_IPName";
            this.Lbl_IPName.Size = new System.Drawing.Size(33, 15);
            this.Lbl_IPName.TabIndex = 1;
            this.Lbl_IPName.Text = "IP -";
            this.Lbl_IPName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_PortName
            // 
            this.Lbl_PortName.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_PortName.Location = new System.Drawing.Point(359, 44);
            this.Lbl_PortName.Name = "Lbl_PortName";
            this.Lbl_PortName.Size = new System.Drawing.Size(47, 15);
            this.Lbl_PortName.TabIndex = 2;
            this.Lbl_PortName.Text = "Port -";
            this.Lbl_PortName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_IPVal
            // 
            this.Lbl_IPVal.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_IPVal.Location = new System.Drawing.Point(431, 13);
            this.Lbl_IPVal.Name = "Lbl_IPVal";
            this.Lbl_IPVal.Size = new System.Drawing.Size(60, 15);
            this.Lbl_IPVal.TabIndex = 3;
            this.Lbl_IPVal.Text = "IP Value";
            this.Lbl_IPVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_PortVal
            // 
            this.Lbl_PortVal.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_PortVal.Location = new System.Drawing.Point(431, 44);
            this.Lbl_PortVal.Name = "Lbl_PortVal";
            this.Lbl_PortVal.Size = new System.Drawing.Size(74, 15);
            this.Lbl_PortVal.TabIndex = 4;
            this.Lbl_PortVal.Text = "Port Value";
            this.Lbl_PortVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_Connect
            // 
            this.Lbl_Connect.AutoSize = true;
            this.Lbl_Connect.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Lbl_Connect.Location = new System.Drawing.Point(410, 102);
            this.Lbl_Connect.Name = "Lbl_Connect";
            this.Lbl_Connect.Size = new System.Drawing.Size(69, 15);
            this.Lbl_Connect.TabIndex = 5;
            this.Lbl_Connect.Text = "Stopped";
            // 
            // Btn_PLCMode
            // 
            this.Btn_PLCMode.ForeColor = System.Drawing.Color.White;
            this.Btn_PLCMode.Location = new System.Drawing.Point(786, 32);
            this.Btn_PLCMode.Name = "Btn_PLCMode";
            this.Btn_PLCMode.Size = new System.Drawing.Size(75, 23);
            this.Btn_PLCMode.TabIndex = 6;
            this.Btn_PLCMode.Text = "PLC";
            this.Btn_PLCMode.UseVisualStyleBackColor = true;
            this.Btn_PLCMode.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btn_PLCMode_MouseDown);
            this.Btn_PLCMode.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Btn_PLCMode_MouseUp);
            // 
            // Btn_AutoMode
            // 
            this.Btn_AutoMode.ForeColor = System.Drawing.Color.White;
            this.Btn_AutoMode.Location = new System.Drawing.Point(889, 32);
            this.Btn_AutoMode.Name = "Btn_AutoMode";
            this.Btn_AutoMode.Size = new System.Drawing.Size(75, 23);
            this.Btn_AutoMode.TabIndex = 7;
            this.Btn_AutoMode.Text = "AUTO";
            this.Btn_AutoMode.UseVisualStyleBackColor = true;
            this.Btn_AutoMode.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btn_AutoMode_MouseDown);
            this.Btn_AutoMode.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Btn_AutoMode_MouseUp);
            // 
            // Btn_Start
            // 
            this.Btn_Start.ForeColor = System.Drawing.Color.White;
            this.Btn_Start.Location = new System.Drawing.Point(786, 81);
            this.Btn_Start.Name = "Btn_Start";
            this.Btn_Start.Size = new System.Drawing.Size(75, 23);
            this.Btn_Start.TabIndex = 8;
            this.Btn_Start.Text = "시작";
            this.Btn_Start.UseVisualStyleBackColor = true;
            this.Btn_Start.Click += new System.EventHandler(this.Btn_Start_Click);
            this.Btn_Start.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btn_Start_MouseDown);
            this.Btn_Start.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Btn_Start_MouseUp);
            // 
            // Btn_Stop
            // 
            this.Btn_Stop.ForeColor = System.Drawing.Color.White;
            this.Btn_Stop.Location = new System.Drawing.Point(889, 81);
            this.Btn_Stop.Name = "Btn_Stop";
            this.Btn_Stop.Size = new System.Drawing.Size(75, 23);
            this.Btn_Stop.TabIndex = 9;
            this.Btn_Stop.Text = "중지";
            this.Btn_Stop.UseVisualStyleBackColor = true;
            this.Btn_Stop.Click += new System.EventHandler(this.Btn_Stop_Click);
            this.Btn_Stop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btn_Stop_MouseDown);
            this.Btn_Stop.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Btn_Stop_MouseUp);
            // 
            // Dgv_Data
            // 
            this.Dgv_Data.AllowUserToAddRows = false;
            this.Dgv_Data.AllowUserToDeleteRows = false;
            this.Dgv_Data.AllowUserToResizeColumns = false;
            this.Dgv_Data.AllowUserToResizeRows = false;
            this.Dgv_Data.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Dgv_Data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_Data.Location = new System.Drawing.Point(15, 156);
            this.Dgv_Data.Name = "Dgv_Data";
            this.Dgv_Data.RowTemplate.Height = 27;
            this.Dgv_Data.Size = new System.Drawing.Size(288, 585);
            this.Dgv_Data.TabIndex = 10;
            // 
            // Tim_TimeCheck
            // 
            this.Tim_TimeCheck.Tick += new System.EventHandler(this.Tim_TimeCheck_Tick);
            // 
            // Excel_Data
            // 
            this.Excel_Data.BackColor = System.Drawing.Color.White;
            this.Excel_Data.ColumnHeaderContextMenuStrip = null;
            this.Excel_Data.LeadHeaderContextMenuStrip = null;
            this.Excel_Data.Location = new System.Drawing.Point(495, 156);
            this.Excel_Data.Name = "Excel_Data";
            this.Excel_Data.RowHeaderContextMenuStrip = null;
            this.Excel_Data.Script = null;
            this.Excel_Data.SheetTabContextMenuStrip = null;
            this.Excel_Data.SheetTabNewButtonVisible = true;
            this.Excel_Data.SheetTabVisible = true;
            this.Excel_Data.SheetTabWidth = 60;
            this.Excel_Data.ShowScrollEndSpacing = true;
            this.Excel_Data.Size = new System.Drawing.Size(469, 475);
            this.Excel_Data.TabIndex = 11;
            this.Excel_Data.Text = "reoGridControl1";
            // 
            // Btn_ItvChange
            // 
            this.Btn_ItvChange.Location = new System.Drawing.Point(688, 698);
            this.Btn_ItvChange.Name = "Btn_ItvChange";
            this.Btn_ItvChange.Size = new System.Drawing.Size(89, 23);
            this.Btn_ItvChange.TabIndex = 12;
            this.Btn_ItvChange.Text = "주기 변경";
            this.Btn_ItvChange.UseVisualStyleBackColor = true;
            this.Btn_ItvChange.Click += new System.EventHandler(this.Btn_ItvChange_Click);
            this.Btn_ItvChange.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btn_ItvChange_MouseDown);
            this.Btn_ItvChange.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Btn_ItvChange_MouseUp);
            // 
            // Txt_TempItv
            // 
            this.Txt_TempItv.Location = new System.Drawing.Point(553, 676);
            this.Txt_TempItv.Name = "Txt_TempItv";
            this.Txt_TempItv.Size = new System.Drawing.Size(100, 25);
            this.Txt_TempItv.TabIndex = 13;
            // 
            // Txt_AmoItv
            // 
            this.Txt_AmoItv.Location = new System.Drawing.Point(553, 716);
            this.Txt_AmoItv.Name = "Txt_AmoItv";
            this.Txt_AmoItv.Size = new System.Drawing.Size(100, 25);
            this.Txt_AmoItv.TabIndex = 14;
            // 
            // Lbl_Temp_Itv
            // 
            this.Lbl_Temp_Itv.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Temp_Itv.Location = new System.Drawing.Point(445, 679);
            this.Lbl_Temp_Itv.Name = "Lbl_Temp_Itv";
            this.Lbl_Temp_Itv.Size = new System.Drawing.Size(102, 22);
            this.Lbl_Temp_Itv.TabIndex = 15;
            this.Lbl_Temp_Itv.Text = "온도 확인주기";
            this.Lbl_Temp_Itv.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Lbl_Amo_Itv
            // 
            this.Lbl_Amo_Itv.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Amo_Itv.Location = new System.Drawing.Point(430, 719);
            this.Lbl_Amo_Itv.Name = "Lbl_Amo_Itv";
            this.Lbl_Amo_Itv.Size = new System.Drawing.Size(117, 21);
            this.Lbl_Amo_Itv.TabIndex = 16;
            this.Lbl_Amo_Itv.Text = "생산량 확인주기";
            this.Lbl_Amo_Itv.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Lbl_Temp_Unit
            // 
            this.Lbl_Temp_Unit.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Temp_Unit.Location = new System.Drawing.Point(659, 679);
            this.Lbl_Temp_Unit.Name = "Lbl_Temp_Unit";
            this.Lbl_Temp_Unit.Size = new System.Drawing.Size(23, 23);
            this.Lbl_Temp_Unit.TabIndex = 17;
            this.Lbl_Temp_Unit.Text = "초";
            this.Lbl_Temp_Unit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_Amo_Unit
            // 
            this.Lbl_Amo_Unit.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Amo_Unit.Location = new System.Drawing.Point(659, 725);
            this.Lbl_Amo_Unit.Name = "Lbl_Amo_Unit";
            this.Lbl_Amo_Unit.Size = new System.Drawing.Size(23, 15);
            this.Lbl_Amo_Unit.TabIndex = 18;
            this.Lbl_Amo_Unit.Text = "초";
            this.Lbl_Amo_Unit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Tim_Temp
            // 
            this.Tim_Temp.Interval = 1000;
            this.Tim_Temp.Tick += new System.EventHandler(this.Tim_Temp_Tick);
            // 
            // Tim_Amount
            // 
            this.Tim_Amount.Interval = 1000;
            this.Tim_Amount.Tick += new System.EventHandler(this.Tim_Amount_Tick);
            // 
            // FRM_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 753);
            this.Controls.Add(this.Lbl_Amo_Unit);
            this.Controls.Add(this.Lbl_Temp_Unit);
            this.Controls.Add(this.Lbl_Amo_Itv);
            this.Controls.Add(this.Lbl_Temp_Itv);
            this.Controls.Add(this.Txt_AmoItv);
            this.Controls.Add(this.Txt_TempItv);
            this.Controls.Add(this.Btn_ItvChange);
            this.Controls.Add(this.Excel_Data);
            this.Controls.Add(this.Dgv_Data);
            this.Controls.Add(this.Btn_Stop);
            this.Controls.Add(this.Btn_Start);
            this.Controls.Add(this.Btn_AutoMode);
            this.Controls.Add(this.Btn_PLCMode);
            this.Controls.Add(this.Lbl_Connect);
            this.Controls.Add(this.Lbl_PortVal);
            this.Controls.Add(this.Lbl_IPVal);
            this.Controls.Add(this.Lbl_PortName);
            this.Controls.Add(this.Lbl_IPName);
            this.Controls.Add(this.Lbl_Time);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FRM_Main";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FRM_Main_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Data)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Lbl_Time;
        private System.Windows.Forms.Label Lbl_IPName;
        private System.Windows.Forms.Label Lbl_PortName;
        private System.Windows.Forms.Label Lbl_IPVal;
        private System.Windows.Forms.Label Lbl_PortVal;
        private System.Windows.Forms.Label Lbl_Connect;
        private System.Windows.Forms.Button Btn_PLCMode;
        private System.Windows.Forms.Button Btn_AutoMode;
        private System.Windows.Forms.Button Btn_Start;
        private System.Windows.Forms.Button Btn_Stop;
        private System.Windows.Forms.DataGridView Dgv_Data;
        private System.Windows.Forms.Timer Tim_TimeCheck;
        private unvell.ReoGrid.ReoGridControl Excel_Data;
        private System.Windows.Forms.Button Btn_ItvChange;
        private System.Windows.Forms.TextBox Txt_TempItv;
        private System.Windows.Forms.TextBox Txt_AmoItv;
        private System.Windows.Forms.Label Lbl_Temp_Itv;
        private System.Windows.Forms.Label Lbl_Amo_Itv;
        private System.Windows.Forms.Label Lbl_Temp_Unit;
        private System.Windows.Forms.Label Lbl_Amo_Unit;
        private System.Windows.Forms.Timer Tim_Temp;
        private System.Windows.Forms.Timer Tim_Amount;
    }
}

