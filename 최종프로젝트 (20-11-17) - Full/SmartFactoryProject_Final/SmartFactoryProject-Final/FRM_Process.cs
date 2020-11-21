
using SmartFactoryProject_Final.Common;
using SmartFactoryProject_Final.CustomControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SmartFactoryProject_Final
{
    public partial class FRM_Process : Form
    {
        int selectedOrder { get; set; } = -1;           // 화면상에 정보를 표시중인 작업지시의 번호
        int currentOrder { get; set; } = 0;             // 현재 생산을 진행중인 작업지시의 번호
        OrderData currentOrderData;
        DateTime currentOrder_Start { get; set; }       // 생산을 개시한 시간
        int targetAmount { get; set; }
        int totalAmount { get; set; }
        int defectAmount { get; set; }
        int baseAmount { get; set; } = 0;
        string machCode;

        CountTimer Tim_Scroll = new CountTimer();

        public FRM_Process(Form parent)
        {
            InitializeComponent();
            this.MdiParent = parent;
        }

        #region ---- Initialize
        private void Frm_Process_Load(object sender, EventArgs e)
        {
            InitializeLayout();
            InitializeChartLayout();
            Initialize();
        }

        private void Initialize()
        {
            IniFile ini = new IniFile();
            ini.Load(IniData.SettingIniFile);
            IniSection settingSect = ini["Setting"];

            Tim_CheckTime.Enabled = true;
            Tim_CheckTime.Interval = 1000;
            Tim_DataRead.Enabled = true;
            Tim_DataRead.Interval = (int)(settingSect["ReadInterval"].ToDouble() * 1000);
            Lbl_Warning.Text = "";

            machCode = ini["Equipment"]["Mach_Code"].ToString();

            SetRealTime();
            ShowOrderList();
            InitializeChart();
            InitializeDGV();
        }
        #endregion
        #region ---- Reset
        public void Reset()
        {
            Stop();
            ResetOrderSelect();
        }

        private void Stop()
        {
            Tim_CheckTime.Enabled = false;
            Tim_DataRead.Enabled = false;
        }

        private void Restart()
        {
            Tim_CheckTime.Enabled = true;
            Tim_DataRead.Enabled = true;
        }

        private void ResetOrderList()
        {
            int amount = Flp_Order.Controls.Count;
            for (int i = 0; i < amount; i++)
            {
                Flp_Order.Controls.RemoveAt(0);
            }
        }
        
        private void Btn_RefreshOrderList_Click(object sender, EventArgs e)
        {
            ResetOrderList();
            ShowOrderList();
        }

        /// <summary>
        /// 선택한 작업지시 관련 정보들을 더이상 표시하지 않을 때 사용하는 함수
        /// </summary>
        private void ResetOrderSelect()
        {
            selectedOrder = 0;
            Txt_ProcName.Text = "";
            Txt_ProcName.Tag = null;
            Txt_Machine.Text = "";
            Txt_Machine.Tag = null;
            Txt_User.Text = "";
            Txt_User.Tag = null;
            Txt_Time.Text = "";
            Txt_DayNight.Text = "";
            Txt_DayNight.Tag = null;
            Txt_Mold.Text = "";
            Txt_Mold.Tag = null;
            if (Pic_Mold.Image != null)
            {
                Pic_Mold.Image.Dispose();
                Pic_Mold.Image = null;
            }
            Pic_Mold.Tag = null;
            Txt_ItemCode.Text = "";
            Txt_ItemName.Text = "";
            Txt_TargetAmount.Text = "";
            Txt_TotalAmount.Text = "";
            Txt_NormalAmount.Text = "";
            Txt_DefectAmount.Text = "";
        }
        #endregion

        #region ---- Layout
        private void InitializeLayout()
        {
            ControlLayout ctrlLayout = new ControlLayout();
            IniFile ini = new IniFile();
            ini.Load(IniData.SettingIniFile);
            IniSection resSect = ini["Resources"];
            string overallResPath = System.IO.Directory.GetCurrentDirectory() + $@"{resSect["ResourceFolder"]}{resSect["OverallFolder"]}";

            this.Dock = DockStyle.Fill;
            ctrlLayout.SetBackgroundImage(this, overallResPath + @"\bg.gif");

            ctrlLayout.Control_Sizing(Lbl_CurrentTime, this.Size, 0.05, 0.02);
            ctrlLayout.Control_Positioning(Lbl_CurrentTime, this.Size, 0.01, 0.0075);
            Lbl_CurrentTime.Font = ctrlLayout.GetProperFontSize("Tahoma", Lbl_CurrentTime.Height, 0.9, true);
            ctrlLayout.Control_Sizing(Lbl_RealTime, this.Size, 0.15, 0.03);
            ctrlLayout.Control_Positioning(Lbl_RealTime, this.Size, 0.015, 0.03);
            Lbl_RealTime.Font = ctrlLayout.GetProperFontSize("Tahoma", Lbl_RealTime.Height, 0.9);
            Lbl_RealTime.ForeColor = Color.White;

            ctrlLayout.Control_Sizing(Lbl_Title, this.Size, 0.6, 0.06);
            ctrlLayout.Control_Positioning(Lbl_Title, this.Size, 0.5, 0.01, ControlLayout.HorizontalSiding.Center);
            Lbl_Title.Text = ini["Title"]["ProcessTitle"].ToString();
            Lbl_Title.Font = ctrlLayout.GetProperFontSize("Tahoma", Lbl_Title.Height, 0.8, true);

            #region -------- Pnl_Order Contents
            ctrlLayout.Control_Sizing(Pnl_Order, this.Size, 0.2, 0.825);
            ctrlLayout.Control_Positioning(Pnl_Order, this.Size, 0.74, 0.15);

            Flp_Order.AutoScroll = false;
            Flp_Order.AutoSize = true;
            Flp_Order.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Flp_Order.WrapContents = false;
            Flp_Order.FlowDirection = FlowDirection.TopDown;

            ctrlLayout.Control_Sizing(Btn_RefreshOrderList, this.Size, 0.1, 0.05);
            ctrlLayout.Control_Positioning(Btn_RefreshOrderList, this.Size, 0.94, 0.145, ControlLayout.HorizontalSiding.Right, ControlLayout.VerticalSiding.Bottom);
            Btn_RefreshOrderList.DefaultSetting();
            ctrlLayout.SetBackgroundImage(Btn_RefreshOrderList, overallResPath + @"\btn_GreenBase.jpg");
            Btn_RefreshOrderList.Text = "새로고침";
            Btn_RefreshOrderList.Font = ctrlLayout.GetProperFontSize("Tahoma", Btn_RefreshOrderList.Height, 0.8, true);

            ctrlLayout.Control_Sizing(Btn_ScrUp, this.Size, 0.05, 0.4);
            ctrlLayout.Control_Positioning(Btn_ScrUp, this.Size, 0.95, 0.15);
            Btn_Scr_SetImage(Btn_ScrUp, "up", false);
            ctrlLayout.Control_Sizing(Btn_ScrDn, this.Size, 0.05, 0.4);
            ctrlLayout.Control_Positioning(Btn_ScrDn, this.Size, 0.95, 0.575);
            Btn_Scr_SetImage(Btn_ScrDn, "down", false);
            #endregion
            #region -------- Tpg_Order Contents
            ctrlLayout.Control_Sizing(Tab_Order, this.Size, 0.725, 0.425);
            ctrlLayout.Control_Positioning(Tab_Order, this.Size, 0.01, 0.15);
            ctrlLayout.SetBackgroundImage(Tpg_Order, overallResPath + @"\bg_box1.png");

            ctrlLayout.Control_Sizing(Btn_Tpg_Order, this.Size, 0.1, 0.05);
            ctrlLayout.Control_Positioning(Btn_Tpg_Order, this.Size, 0.01, 0.1);
            ctrlLayout.SetBackgroundImage(Btn_Tpg_Order, overallResPath + @"\tab_action.png");
            Btn_Tpg_Order.DefaultSetting();
            Btn_Tpg_Order.Text = "생산계획";
            Btn_Tpg_Order.Font = ctrlLayout.GetProperFontSize("Tahoma", Btn_Tpg_Order.Height, 0.8, true);
            Btn_Tpg_Order.ForeColor = Color.White;
            //--------------------------------------------------------------------------------------------
            ctrlLayout.Control_Sizing(Lbl_OrderTime, Tpg_Order.Size, 0.145, 0.125);
            ctrlLayout.Control_Positioning(Lbl_OrderTime, Tpg_Order.Size, 0.01, 0.03);
            ctrlLayout.SetBackgroundImage(Lbl_OrderTime, overallResPath + @"\label.gif");
            Lbl_OrderTime.Font = ctrlLayout.GetProperFontSize("Tahoma", Lbl_OrderTime.Height, 0.6, true);
            ctrlLayout.Control_Sizing(Txt_Time, Tpg_Order.Size, 0.3, 0.125);
            ctrlLayout.Control_Positioning(Txt_Time, Tpg_Order.Size, 0.16, 0.03);
            Txt_Time.DefaultSetting();
            Txt_Time.Font = ctrlLayout.GetProperFontSize("맑은 고딕", Txt_Time.Height, 0.7, true);

            ctrlLayout.Control_Sizing(Lbl_DayNight, Tpg_Order.Size, 0.145, 0.125);
            ctrlLayout.Control_Positioning(Lbl_DayNight, Tpg_Order.Size, 0.465, 0.03);
            ctrlLayout.SetBackgroundImage(Lbl_DayNight, overallResPath + @"\label.gif");
            Lbl_DayNight.Font = ctrlLayout.GetProperFontSize("Tahoma", Lbl_DayNight.Height, 0.6, true);
            ctrlLayout.Control_Sizing(Txt_DayNight, Tpg_Order.Size, 0.185, 0.125);
            ctrlLayout.Control_Positioning(Txt_DayNight, Tpg_Order.Size, 0.615, 0.03);
            Txt_DayNight.DefaultSetting();
            Txt_DayNight.Font = ctrlLayout.GetProperFontSize("맑은 고딕", Txt_DayNight.Height, 0.7, true);

            ctrlLayout.Control_Sizing(Lbl_User, Tpg_Order.Size, 0.145, 0.125);
            ctrlLayout.Control_Positioning(Lbl_User, Tpg_Order.Size, 0.01, 0.165);
            ctrlLayout.SetBackgroundImage(Lbl_User, overallResPath + @"\label.gif");
            Lbl_User.Font = ctrlLayout.GetProperFontSize("Tahoma", Lbl_User.Height, 0.6, true);
            ctrlLayout.Control_Sizing(Txt_User, Tpg_Order.Size, 0.64, 0.125);
            ctrlLayout.Control_Positioning(Txt_User, Tpg_Order.Size, 0.16, 0.165);
            Txt_User.DefaultSetting();
            Txt_User.Font = ctrlLayout.GetProperFontSize("맑은 고딕", Txt_User.Height, 0.7, true);

            ctrlLayout.Control_Sizing(Lbl_ItemCode, Tpg_Order.Size, 0.145, 0.125);
            ctrlLayout.Control_Positioning(Lbl_ItemCode, Tpg_Order.Size, 0.01, 0.3);
            ctrlLayout.SetBackgroundImage(Lbl_ItemCode, overallResPath + @"\label.gif");
            Lbl_ItemCode.Font = ctrlLayout.GetProperFontSize("Tahoma", Lbl_ItemCode.Height, 0.6, true);
            ctrlLayout.Control_Sizing(Txt_ItemCode, Tpg_Order.Size, 0.22, 0.125);
            ctrlLayout.Control_Positioning(Txt_ItemCode, Tpg_Order.Size, 0.16, 0.3);
            Txt_ItemCode.DefaultSetting();
            Txt_ItemCode.Font = ctrlLayout.GetProperFontSize("맑은 고딕", Txt_ItemCode.Height, 0.7, true);

            ctrlLayout.Control_Sizing(Lbl_ItemName, Tpg_Order.Size, 0.145, 0.125);
            ctrlLayout.Control_Positioning(Lbl_ItemName, Tpg_Order.Size, 0.385, 0.3);
            ctrlLayout.SetBackgroundImage(Lbl_ItemName, overallResPath + @"\label.gif");
            Lbl_ItemName.Font = ctrlLayout.GetProperFontSize("Tahoma", Lbl_ItemName.Height, 0.6, true);
            ctrlLayout.Control_Sizing(Txt_ItemName, Tpg_Order.Size, 0.265, 0.125);
            ctrlLayout.Control_Positioning(Txt_ItemName, Tpg_Order.Size, 0.535, 0.3);
            Txt_ItemName.DefaultSetting();
            Txt_ItemName.Font = ctrlLayout.GetProperFontSize("맑은 고딕", Txt_ItemName.Height, 0.7, true);

            ctrlLayout.Control_Sizing(Lbl_Proc, Tpg_Order.Size, 0.145, 0.125);
            ctrlLayout.Control_Positioning(Lbl_Proc, Tpg_Order.Size, 0.01, 0.435);
            ctrlLayout.SetBackgroundImage(Lbl_Proc, overallResPath + @"\label.gif");
            Lbl_Proc.Font = ctrlLayout.GetProperFontSize("Tahoma", Lbl_Proc.Height, 0.6, true);
            ctrlLayout.Control_Sizing(Txt_ProcName, Tpg_Order.Size, 0.22, 0.125);
            ctrlLayout.Control_Positioning(Txt_ProcName, Tpg_Order.Size, 0.16, 0.435);
            Txt_ProcName.DefaultSetting();
            Txt_ProcName.Font = ctrlLayout.GetProperFontSize("맑은 고딕", Txt_ProcName.Height, 0.7, true);
            ctrlLayout.Control_Sizing(Txt_Machine, Tpg_Order.Size, 0.31, 0.125);
            ctrlLayout.Control_Positioning(Txt_Machine, Tpg_Order.Size, 0.385, 0.435);
            Txt_Machine.DefaultSetting();
            Txt_Machine.Font = ctrlLayout.GetProperFontSize("맑은 고딕", Txt_Machine.Height, 0.7, true);

            ctrlLayout.Control_Sizing(Lbl_Mold, Tpg_Order.Size, 0.145, 0.125);
            ctrlLayout.Control_Positioning(Lbl_Mold, Tpg_Order.Size, 0.01, 0.57);
            ctrlLayout.SetBackgroundImage(Lbl_Mold, overallResPath + @"\label.gif");
            Lbl_Mold.Font = ctrlLayout.GetProperFontSize("Tahoma", Lbl_Mold.Height, 0.6, true);
            ctrlLayout.Control_Sizing(Txt_Mold, Tpg_Order.Size, 0.535, 0.125);
            ctrlLayout.Control_Positioning(Txt_Mold, Tpg_Order.Size, 0.16, 0.57);
            Txt_Mold.DefaultSetting();
            Txt_Mold.Font = ctrlLayout.GetProperFontSize("맑은 고딕", Txt_Mold.Height, 0.7, true);
            ctrlLayout.Control_Sizing(Pic_Mold, Tpg_Order.Size, 0.29, 0.53);
            ctrlLayout.Control_Positioning(Pic_Mold, Tpg_Order.Size, 0.7, 0.435);
            ctrlLayout.MakeCurvedBorder(Pic_Mold, 12, 12);

            ctrlLayout.Control_Sizing(Lbl_TargetAmount, Tpg_Order.Size, 0.145, 0.125);
            ctrlLayout.Control_Positioning(Lbl_TargetAmount, Tpg_Order.Size, 0.01, 0.705);
            ctrlLayout.SetBackgroundImage(Lbl_TargetAmount, overallResPath + @"\label.gif");
            Lbl_TargetAmount.Font = ctrlLayout.GetProperFontSize("Tahoma", Lbl_TargetAmount.Height, 0.6, true);
            ctrlLayout.Control_Sizing(Txt_TargetAmount, Tpg_Order.Size, 0.19, 0.125);
            ctrlLayout.Control_Positioning(Txt_TargetAmount, Tpg_Order.Size, 0.16, 0.705);
            Txt_TargetAmount.DefaultSetting();
            Txt_TargetAmount.Font = ctrlLayout.GetProperFontSize("맑은 고딕", Txt_TargetAmount.Height, 0.7, true);

            ctrlLayout.Control_Sizing(Lbl_TotalAmount, Tpg_Order.Size, 0.145, 0.125);
            ctrlLayout.Control_Positioning(Lbl_TotalAmount, Tpg_Order.Size, 0.01, 0.84);
            ctrlLayout.SetBackgroundImage(Lbl_TotalAmount, overallResPath + @"\label.gif");
            Lbl_TotalAmount.Font = ctrlLayout.GetProperFontSize("Tahoma", Lbl_TotalAmount.Height, 0.6, true);
            ctrlLayout.Control_Sizing(Txt_TotalAmount, Tpg_Order.Size, 0.19, 0.125);
            ctrlLayout.Control_Positioning(Txt_TotalAmount, Tpg_Order.Size, 0.16, 0.84);
            Txt_TotalAmount.DefaultSetting();
            Txt_TotalAmount.Font = ctrlLayout.GetProperFontSize("맑은 고딕", Txt_TotalAmount.Height, 0.7, true);

            ctrlLayout.Control_Sizing(Lbl_NormalAmount, Tpg_Order.Size, 0.145, 0.125);
            ctrlLayout.Control_Positioning(Lbl_NormalAmount, Tpg_Order.Size, 0.355, 0.705);
            ctrlLayout.SetBackgroundImage(Lbl_NormalAmount, overallResPath + @"\label.gif");
            Lbl_NormalAmount.Font = ctrlLayout.GetProperFontSize("Tahoma", Lbl_NormalAmount.Height, 0.6, true);
            ctrlLayout.Control_Sizing(Txt_NormalAmount, Tpg_Order.Size, 0.19, 0.125);
            ctrlLayout.Control_Positioning(Txt_NormalAmount, Tpg_Order.Size, 0.505, 0.705);
            Txt_NormalAmount.DefaultSetting();
            Txt_NormalAmount.Font = ctrlLayout.GetProperFontSize("맑은 고딕", Txt_NormalAmount.Height, 0.7, true);

            ctrlLayout.Control_Sizing(Lbl_DefectAmount, Tpg_Order.Size, 0.145, 0.125);
            ctrlLayout.Control_Positioning(Lbl_DefectAmount, Tpg_Order.Size, 0.3555, 0.84);
            ctrlLayout.SetBackgroundImage(Lbl_DefectAmount, overallResPath + @"\label.gif");
            Lbl_DefectAmount.Font = ctrlLayout.GetProperFontSize("Tahoma", Lbl_DefectAmount.Height, 0.6, true);
            ctrlLayout.Control_Sizing(Txt_DefectAmount, Tpg_Order.Size, 0.19, 0.125);
            ctrlLayout.Control_Positioning(Txt_DefectAmount, Tpg_Order.Size, 0.505, 0.84);
            Txt_DefectAmount.DefaultSetting();
            Txt_DefectAmount.Font = ctrlLayout.GetProperFontSize("맑은 고딕", Txt_DefectAmount.Height, 0.7, true);

            ctrlLayout.Control_Sizing(Btn_OrderStart, Tpg_Order.Size, 0.185, 0.395);
            ctrlLayout.Control_Positioning(Btn_OrderStart, Tpg_Order.Size, 0.805, 0.03);
            Btn_OrderStart.DefaultSetting();
            Btn_Order_SetImage(false, false);
            Btn_OrderStart.Font = ctrlLayout.GetProperFontSize("맑은 고딕", Btn_OrderStart.Height, 0.3, true);
            Btn_OrderStart.ForeColor = Color.White;
            #endregion
            #region -------- Tab_Chart Contents
            ctrlLayout.Control_Sizing(Tab_Chart, this.Size, 0.725, 0.325);
            ctrlLayout.Control_Positioning(Tab_Chart, this.Size, 0.01, 0.65);

            ctrlLayout.Control_Sizing(Btn_Tpg_Temp1, this.Size, 0.1, 0.05);
            ctrlLayout.Control_Positioning(Btn_Tpg_Temp1, this.Size, 0.01, 0.6);
            ctrlLayout.SetBackgroundImage(Btn_Tpg_Temp1, overallResPath + @"\tab_action.png");
            Btn_Tpg_Temp1.DefaultSetting();
            Btn_Tpg_Temp1.Text = "내경 온도";
            Btn_Tpg_Temp1.Font = ctrlLayout.GetProperFontSize("Tahoma", Btn_Tpg_Temp1.Height, 0.8, true);

            ctrlLayout.Control_Sizing(Btn_Tpg_Temp2, this.Size, 0.1, 0.05);
            ctrlLayout.Control_Positioning(Btn_Tpg_Temp2, this.Size, 0.11, 0.6);
            ctrlLayout.SetBackgroundImage(Btn_Tpg_Temp2, overallResPath + @"\tab_action.png");
            Btn_Tpg_Temp2.DefaultSetting();
            Btn_Tpg_Temp2.Text = "외경 온도";
            Btn_Tpg_Temp2.Font = ctrlLayout.GetProperFontSize("Tahoma", Btn_Tpg_Temp2.Height, 0.8, true);

            ctrlLayout.Control_Sizing(Btn_Tpg_Data, this.Size, 0.1, 0.05);
            ctrlLayout.Control_Positioning(Btn_Tpg_Data, this.Size, 0.21, 0.6);
            ctrlLayout.SetBackgroundImage(Btn_Tpg_Data, overallResPath + @"\tab_action.png");
            Btn_Tpg_Data.DefaultSetting();
            Btn_Tpg_Data.Text = "데이터";
            Btn_Tpg_Data.Font = ctrlLayout.GetProperFontSize("Tahoma", Btn_Tpg_Data.Height, 0.8, true);
            #endregion
        }

        private void InitializeChartLayout()
        {
            ControlLayout ctrlLayout = new ControlLayout();

            foreach (TabPage page in Tab_Chart.TabPages)
            {
                ctrlLayout.Control_Sizing(page, Tab_Chart.Size, 1, 1);      // Tab_Chart의 사이즈를 변경할 때 Tpg_Temp1의 사이즈만 그에 맞춰서 변경되므로 다른 TabPage의 크기도 변경함
            }

            ctrlLayout.Control_Sizing(Cht_Temp1, Tpg_Temp1.Size, 1, 1);
            ctrlLayout.Control_Sizing(Cht_Temp2, Tpg_Temp2.Size, 1, 1);

            ctrlLayout.Control_Sizing(Dgv_Temp1, Tpg_Data.Size, 0.26, 0.9);
            ctrlLayout.Control_Positioning(Dgv_Temp1, Tpg_Data.Size, 0.05, 0.05);
            ctrlLayout.Control_Sizing(Dgv_Temp2, Tpg_Data.Size, 0.26, 0.9);
            ctrlLayout.Control_Positioning(Dgv_Temp2, Tpg_Data.Size, 0.32, 0.05);
            ctrlLayout.Control_Sizing(Dgv_Amount, Tpg_Data.Size, 0.26, 0.9);
            ctrlLayout.Control_Positioning(Dgv_Amount, Tpg_Data.Size, 0.59, 0.05);

            ctrlLayout.Control_Sizing(Btn_DgvDat_ScrUp, Tpg_Data.Size, 0.06, 0.425);
            ctrlLayout.Control_Positioning(Btn_DgvDat_ScrUp, Tpg_Data.Size, 0.86, 0.05);
            Btn_DgvDat_SetImage(Btn_DgvDat_ScrUp, "up", false);
            ctrlLayout.Control_Sizing(Btn_DgvDat_ScrDn, Tpg_Data.Size, 0.06, 0.425);
            ctrlLayout.Control_Positioning(Btn_DgvDat_ScrDn, Tpg_Data.Size, 0.86, 0.525);
            Btn_DgvDat_SetImage(Btn_DgvDat_ScrDn, "down", false);
        }
        #endregion
        #region -------- Button - Mouse Up&Down
        /// <summary>
        /// Scroll 버튼에 적절한 이미지를 할당하는 함수
        /// </summary>
        /// <param name="direction">"up" 또는 "down"의 값을 입력</param>
        /// <param name="pressed"></param>
        private void Btn_Scr_SetImage(Button btn, string direction, bool pressed)
        {
            ControlLayout ctrlLayout = new ControlLayout();
            IniFile ini = new IniFile();
            ini.Load(IniData.SettingIniFile);
            IniSection resSect = ini["Resources"];
            string overallResPath = System.IO.Directory.GetCurrentDirectory() + $@"{resSect["ResourceFolder"]}{resSect["OverallFolder"]}";

            switch (direction)
            {
                case "up":
                    if (pressed)
                        ctrlLayout.SetBackgroundImage(btn, overallResPath + @"\btn_up_.gif");
                    else
                        ctrlLayout.SetBackgroundImage(btn, overallResPath + @"\btn_up.gif");
                    break;
                case "down":
                    if (pressed)
                        ctrlLayout.SetBackgroundImage(btn, overallResPath + @"\btn_down_.gif");
                    else
                        ctrlLayout.SetBackgroundImage(btn, overallResPath + @"\btn_down.gif");
                    break;
            }
        }

        private void Btn_DgvDat_SetImage(Button btn, string direction, bool pressed)
        {
            ControlLayout ctrlLayout = new ControlLayout();
            IniFile ini = new IniFile();
            ini.Load(IniData.SettingIniFile);
            IniSection resSect = ini["Resources"];
            string overallResPath = System.IO.Directory.GetCurrentDirectory() + $@"{resSect["ResourceFolder"]}{resSect["OverallFolder"]}";

            switch (direction)
            {
                case "up":
                    if (pressed)
                        ctrlLayout.SetBackgroundImage(btn, overallResPath + @"\pop_up_.gif");
                    else
                        ctrlLayout.SetBackgroundImage(btn, overallResPath + @"\pop_up.gif");
                    break;
                case "down":
                    if (pressed)
                        ctrlLayout.SetBackgroundImage(btn, overallResPath + @"\pop_down_.gif");
                    else
                        ctrlLayout.SetBackgroundImage(btn, overallResPath + @"\pop_down.gif");
                    break;
            }
        }

        private void Btn_Order_SetImage(bool running, bool pressed)
        {
            ControlLayout ctrlLayout = new ControlLayout();
            IniFile ini = new IniFile();
            ini.Load(IniData.SettingIniFile);
            IniSection resSect = ini["Resources"];
            string overallResPath = System.IO.Directory.GetCurrentDirectory() + $@"{resSect["ResourceFolder"]}{resSect["OverallFolder"]}";

            if (running)
            {
                if (pressed)
                    ctrlLayout.SetBackgroundImage(Btn_OrderStart, overallResPath + @"\btn_RedBase_.jpg");
                else
                    ctrlLayout.SetBackgroundImage(Btn_OrderStart, overallResPath + @"\btn_RedBase.jpg");

                Btn_OrderStart.Text = "작 업\n종 료";
            }
            else
            {
                if (pressed)
                    ctrlLayout.SetBackgroundImage(Btn_OrderStart, overallResPath + @"\btn_BlueBase_.jpg");
                else
                    ctrlLayout.SetBackgroundImage(Btn_OrderStart, overallResPath + @"\btn_BlueBase.jpg");

                Btn_OrderStart.Text = "작 업\n시 작";
            }
        }

        private void Btn_OrderStart_MouseDown(object sender, MouseEventArgs e)
        {
            Btn_Order_SetImage(IsRunning(), true);
        }

        private void Btn_OrderStart_MouseUp(object sender, MouseEventArgs e)
        {
            Btn_Order_SetImage(IsRunning(), false);
        }
        #endregion

        #region ---- Display Infomations (화면상에 정보들을 보여주기 위한 함수)
        private void Txt_Mold_Click(object sender, EventArgs e)
        {
            if (selectedOrder <= 0 || selectedOrder == currentOrder)
                return;

            string itemCode = GetItemCode();
            if (!string.IsNullOrEmpty(itemCode))
                SelectMold(itemCode);
        }

        private void Txt_User_Click(object sender, EventArgs e)
        {
            if (selectedOrder <= 0 || selectedOrder == currentOrder)
                return;

            SelectUser();
        }

        private void Txt_DayNight_Click(object sender, EventArgs e)
        {
            if (selectedOrder <= 0 || selectedOrder == currentOrder)
                return;

            SelectDayNight();
        }

        private void Txt_DefectAmount_Click(object sender, EventArgs e)
        {
            if (!IsRunning())   // 물품이 생산중인지 확인해 생산중이 아니라면 키패드가 나오지 않게 한다
            {
                FRM_MessageBox.Show("먼저 작업을 개시하십시오", "오류");
                Txt_DefectAmount.Text = "";
                return;
            }
            else if (currentOrder != GetSelectedOrder())
            {
                return;
            }

            SelectDefectAmount();
        }

        private void ShowOrderList()
        {
            DBConnect dbConnect = new DBConnect();
            DataSet orderSet = dbConnect.ExecuteProcedure("SPP_GET_STANDBYORDER_LIST", new SqlDbType[] { SqlDbType.VarChar },
                                                                                          new object[] { GetMachCode() });
            if (!dbConnect.HasContent(orderSet))
            {
                FRM_MessageBox.Show("주문이 존재하지 않습니다", "경고");
                return;
            }

            ControlLayout ctrlLayout = new ControlLayout();
            IniFile ini = new IniFile();
            ini.Load(IniData.SettingIniFile);
            IniSection resSect = ini["Resources"];
            string overallResPath = System.IO.Directory.GetCurrentDirectory() + $@"{resSect["ResourceFolder"]}{resSect["OverallFolder"]}";
            foreach (DataRow row in orderSet.Tables[0].Rows)
            {
                Label lbl = new Label();
                ctrlLayout.Control_Sizing(lbl, Pnl_Order.Size, 0.975, 0.12);
                ctrlLayout.SetBackgroundImage(lbl, overallResPath + @"\pop_itembox.gif");
                lbl.Margin = new Padding(4);            // FlowLayoutPanel 내에서 컨트롤간 간격을 조절하는 방법

                lbl.Text = $"{row[1].ToString().Substring(0, 10)}\n{row[2].ToString()}\n{row[3].ToString()}개";
                lbl.TextAlign = ContentAlignment.MiddleCenter;
                lbl.Font = ctrlLayout.GetProperFontSize("Tahoma", lbl.Height / 3, 0.9);   // 내용이 3줄이므로 Height 값을 3으로 나눔
                lbl.ForeColor = Color.RoyalBlue;
                lbl.Tag = row[0].ToString();
                lbl.Click += Btn_Order_Click;

                Flp_Order.Controls.Add(lbl);
            }
        }
        private void Btn_Order_Click(object sender, EventArgs e)
        {
            Label btn = sender as Label;

            ResetOrderSelect();
            selectedOrder = int.Parse(btn.Tag.ToString());
            GetOrderData(selectedOrder);

            Txt_Time.Text = Lbl_RealTime.Text.Substring(0, 10);
        }

        private void GetOrderData(int orderCode)
        {
            if (orderCode <= 0)
                return;

            DBConnect dbConnect = new DBConnect();

            DataSet machData = dbConnect.ExecuteProcedure("SPP_GET_MACH_DATA", new SqlDbType[] { SqlDbType.VarChar },
                                                                                  new object[] { GetMachCode() });
            if (dbConnect.HasContent(machData))
            {
                Txt_ProcName.Text = machData.Tables[0].Rows[0][2].ToString();
                Txt_ProcName.Tag = machData.Tables[0].Rows[0][1].ToString();
                Txt_Machine.Text = machData.Tables[0].Rows[0][0].ToString();
            }
            Txt_Machine.Tag = GetMachCode();
            SetDayNight();

            if (orderCode == currentOrder)
            {
                Txt_User.Text = currentOrderData.userName;
                Txt_User.Tag = currentOrderData.userNo;
                Txt_ItemCode.Text = currentOrderData.itemCode;
                Txt_ItemName.Text = currentOrderData.itemName;
                Txt_Mold.Text = currentOrderData.moldName;
                Txt_Mold.Tag = currentOrderData.moldCode;
                Txt_TargetAmount.Text = this.targetAmount.ToString();

                ShowAmount();
                Btn_Order_SetImage(true, false);
            }
            else
            {
                DataSet orderData = dbConnect.ExecuteProcedure("SPP_GET_ORDER_DATA", new SqlDbType[] { SqlDbType.Int },
                                                                                    new object[] { orderCode });
                if (!dbConnect.HasContent(orderData))
                {
                    FRM_MessageBox.Show("해당하는 주문의 정보를 가져오는데 실패했습니다", "경고");
                    return;
                }

                // 긴급정지된 이력이 있는 작업지시에 대해 목표생산량에서 이미 생산한 분량을 뺀다
                DataSet qtyData = dbConnect.ExecuteProcedure("SPP_GET_PRODAMOUNT_INCOMP", new SqlDbType[] { SqlDbType.Int },
                                                                                             new object[] { orderCode });
                int targetAmount = (int)orderData.Tables[0].Rows[0][3];
                if (!IsRunning() && dbConnect.HasContent(qtyData) &&
                    !string.IsNullOrEmpty(qtyData.Tables[0].Rows[0][0].ToString()))
                {
                    baseAmount = (int)qtyData.Tables[0].Rows[0][0];
                }
                else if (!IsRunning())
                {
                    baseAmount = 0;
                }


                // DB에서 받은 데이터를 가지고 선택한 작업지시에 대한 정보를 출력한다
                Txt_ItemCode.Text = orderData.Tables[0].Rows[0][1].ToString();
                Txt_ItemName.Text = orderData.Tables[0].Rows[0][2].ToString();
                Txt_TargetAmount.Text = targetAmount.ToString();

                Btn_Order_SetImage(false, false);
            }
        }

        private void SetDayNight()
        {
            DBConnect dbConnect = new DBConnect();
            DataSet dnData = dbConnect.ExecuteProcedure("SPP_GET_DN_LIST");
            if (!dbConnect.HasContent(dnData))
            {
                FRM_MessageBox.Show("주야간 코드 정보를 받아오는데 실패하였습니다", "경고");
                return;
            }

            DateTime currentDT = GetCurrentTime();
            int currentHour = currentDT.Hour;
            if (currentHour < 9 || currentHour >= 18)                       // 야간
            {
                Txt_DayNight.Text = dnData.Tables[0].Rows[1][1].ToString();
                Txt_DayNight.Tag = dnData.Tables[0].Rows[1][0].ToString();
            }
            else                                                            // 주간
            {
                Txt_DayNight.Text = dnData.Tables[0].Rows[0][1].ToString();
                Txt_DayNight.Tag = dnData.Tables[0].Rows[0][0].ToString();
            }
        }

        private void ShowAmount()
        {
            if (selectedOrder != currentOrder || selectedOrder == 0)
                return;

            Txt_TotalAmount.Text = (baseAmount + totalAmount).ToString();

            int normalAmount = totalAmount - defectAmount;
            Txt_NormalAmount.Text = (baseAmount + normalAmount).ToString();
            if (normalAmount >= targetAmount)
                Txt_NormalAmount.BackColor = Color.PaleGreen;
            else
                Txt_NormalAmount.BackColor = SystemColors.Window;
            Txt_DefectAmount.Text = defectAmount.ToString();
        }

        #region -------- Select Entry
        private void SelectMold(string itemCode)
        {
            DBConnect dbConnect = new DBConnect();
            DataSet moldData = dbConnect.ExecuteProcedure("SPP_GET_MOLD_LIST", new SqlDbType[] { SqlDbType.VarChar },
                                                                                  new object[] { itemCode });
            if (!dbConnect.HasContent(moldData))
            {
                FRM_MessageBox.Show("해당하는 금형 정보를 받아오는데 실패했습니다", "경고");
                return;
            }

            Dictionary<object, string> moldList = new Dictionary<object, string>();
            foreach (DataRow row in moldData.Tables[0].Rows)
            {
                moldList.Add(new object[] { row[0].ToString(), row[2].ToString() },
                             row[1].ToString());
            }

            FRM_Select select = new FRM_Select(moldList);
            if (select.ShowDialog() == DialogResult.OK)
            {
                ControlLayout ctrlLayout = new ControlLayout();
                IniFile ini = new IniFile();
                ini.Load(IniData.SettingIniFile);
                IniSection resSect = ini["Resources"];
                string moldResPath = System.IO.Directory.GetCurrentDirectory() + $@"{resSect["ResourceFolder"]}{resSect["MoldFolder"]}";
                KeyValuePair<object, string> selResult = select.GetSelection();
                object[] keys = selResult.Key as object[];

                Txt_Mold.Text = selResult.Value;
                Txt_Mold.Tag = keys[0].ToString();
                if (string.IsNullOrEmpty(keys[1].ToString()))
                    ctrlLayout.SetImage(Pic_Mold, moldResPath + $@"\{keys[0].ToString()}.png");
                else
                    ctrlLayout.SetImage(Pic_Mold, moldResPath + $@"\{keys[1].ToString()}");
            }
        }

        private void SelectUser()
        {
            DBConnect dbConnect = new DBConnect();
            DataSet userData = dbConnect.ExecuteProcedure("SPP_GET_USER_LIST");
            if (!dbConnect.HasContent(userData))
            {
                FRM_MessageBox.Show("사용자 정보를 받아오는데 실패하였습니다", "경고");
                return;
            }

            Dictionary<object, string> userList = new Dictionary<object, string>();
            foreach (DataRow row in userData.Tables[0].Rows)
            {
                userList.Add(row[0].ToString(), $"{row[1].ToString()} {row[2].ToString()} {row[3].ToString()}");
            }

            SetTextwithSelect(userList, Txt_User);
        }

        private void SelectDayNight()
        {
            DBConnect dbConnect = new DBConnect();
            DataSet dnData = dbConnect.ExecuteProcedure("SPP_GET_DN_LIST");
            if (!dbConnect.HasContent(dnData))
            {
                FRM_MessageBox.Show("주야간 코드 정보를 받아오는데 실패하였습니다", "경고");
                return;
            }

            Dictionary<object, string> dnList = new Dictionary<object, string>();
            foreach (DataRow row in dnData.Tables[0].Rows)
            {
                dnList.Add(row[0].ToString(), row[1].ToString());
            }

            SetTextwithSelect(dnList, Txt_DayNight);
        }

        private void SetTextwithSelect(Dictionary<object, string> list, TextBox textBox)
        {
            FRM_Select select = new FRM_Select(list);
            if (select.ShowDialog() == DialogResult.OK)
            {
                KeyValuePair<object, string> selResult = select.GetSelection();
                textBox.Text = selResult.Value;
                textBox.Tag = selResult.Key;
            }
        }

        private void SelectDefectAmount()
        {
            FRM_NumPadUI numpad = new FRM_NumPadUI();
            if (numpad.ShowDialog() == DialogResult.OK)
            {
                int defects;
                if (!int.TryParse(numpad.GetInputText(), out defects))
                    return;

                if (defects <= totalAmount)
                {
                    defectAmount = defects;
                    Txt_DefectAmount.Text = defects.ToString();
                    Txt_NormalAmount.Text = (baseAmount + totalAmount - defectAmount).ToString();
                }
                else
                {
                    FRM_MessageBox.Show($"입력한 불량품의 수가 너무 많습니다\n{totalAmount}개 이하로 입력해 주세요", "잘못된 입력 확인");
                    Txt_DefectAmount.Text = (defectAmount > 0) ? defectAmount.ToString() : "";
                }
            }
        }
        #endregion
        #endregion
        #region -------- TabPage View (TabControl에서 특정 TabPage를 보여주기 위한 버튼 관련 함수)
        private void Btn_Tpg_Order_Click(object sender, EventArgs e)
        {
            Tab_Order.SelectedIndex = 0;
        }

        private void Btn_Tpg_Temp1_Click(object sender, EventArgs e)
        {
            Tab_Chart.SelectedIndex = 0;
        }

        private void Btn_Tpg_Temp2_Click(object sender, EventArgs e)
        {
            Tab_Chart.SelectedIndex = 1;
        }

        private void Btn_Tpg_Data_Click(object sender, EventArgs e)
        {
            Tab_Chart.SelectedIndex = 2;
        }
        #endregion
        #region -------- Scroll OrderList
        private void Btn_ScrUp_MouseDown(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            Btn_Scr_SetImage(btn, "up", true);
            TickEvent_ScrUp(sender, e);
            Tim_Scroll.Start(TickEvent_ScrUp);
        }
        private void Btn_ScrUp_MouseUp(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            Btn_Scr_SetImage(btn, "up", false);
            Tim_Scroll.Stop();
        }

        private void Btn_ScrDn_MouseDown(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            Btn_Scr_SetImage(btn, "down", true);
            TickEvent_ScrDn(sender, e);
            Tim_Scroll.Start(TickEvent_ScrDn);
        }
        private void Btn_ScrDn_MouseUp(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            Btn_Scr_SetImage(btn, "down", false);
            Tim_Scroll.Stop();
        }

        private void TickEvent_ScrUp(object sender, EventArgs e)
        {
            ControlLayout ctrlLayout = new ControlLayout();
            Panel_VerticalScroll(Flp_Order, Pnl_Order, ctrlLayout.GetYPosByRatio(Pnl_Order.Size, 0.7f));
        }

        private void TickEvent_ScrDn(object sender, EventArgs e)
        {
            ControlLayout ctrlLayout = new ControlLayout();
            Panel_VerticalScroll(Flp_Order, Pnl_Order, -ctrlLayout.GetYPosByRatio(Pnl_Order.Size, 0.7f));
        }

        private void Panel_VerticalScroll(Panel panel, Panel bg, int scrollValue)
        {
            int min = -panel.Height + bg.Height;
            int max = 0;
            if (min > max)
                return;                                     // 목록의 길이가 짧아 한 화면을 가득 채우지 못하므로 스크롤의 필요성이 없음
            int newPos = panel.Location.Y + scrollValue;

            if (newPos <= max && newPos >= min)
                panel.Location = new Point(0, newPos);
            else if (newPos > max)
                panel.Location = new Point(0, max);
            else if (newPos < min)
                panel.Location = new Point(0, min);
        }
        #endregion

        #region ---- Get Informations (화면상의 정보를 가져오는 함수들?)
        private int GetSelectedOrder()
        {
            return selectedOrder;
        }

        private string GetProcessCode()
        {
            if (selectedOrder > 0)
                return Txt_ProcName.Tag.ToString();
            else
                return null;
        }

        private string GetItemCode()
        {
            if (selectedOrder > 0)
                return Txt_ItemCode.Text;
            else
                return null;
        }

        private string GetDNCode()
        {
            if (selectedOrder > 0)
                return Txt_DayNight.Tag.ToString();
            else
                return null;
        }

        private string GetMachCode()
        {
            return machCode;
        }

        private string GetMoldCode()
        {
            if (selectedOrder > 0)
                return Txt_Mold.Tag.ToString();
            else
                return null;
        }

        private string GetUserID()
        {
            FRM_Main parent = this.MdiParent as FRM_Main;
            return parent.loginuser.ID;
        }

        private int GetTotalQty()
        {
            if (IsRunning())
                return totalAmount;
            else
                return 0;
        }

        private int GetNormalQty()
        {
            if (IsRunning())
                return totalAmount - defectAmount;
            else
                return 0;
        }

        private int GetDefectQty()
        {
            if (IsRunning())
                return defectAmount;
            else
                return 0;
        }

        private DateTime GetCurrentTime()
        {
            DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();
            dtFormat.FullDateTimePattern = "yyyy-MM-dd HH:mm:ss";
            return Convert.ToDateTime(Lbl_RealTime.Text, dtFormat);
        }

        private bool IsRunning()
        {
            if (currentOrder > 0)
                return true;
            else
                return false;
        }
        #endregion

        #region ---- Periodic Operations (타이머 및 타이머를 통해 주기적으로 실행되는 함수)
        private void Tim_CheckTime_Tick(object sender, EventArgs e)
        {
            SetRealTime();
        }

        /// <summary>
        /// 서버의 시간을 받아 표시하기 위한 함수
        /// </summary>
        private void SetRealTime()
        {
            if (CanGetTimeFromServer())  /// 서버에서 시간을 가져올 수 있는지를 확인하는 곳
            {
                Lbl_RealTime.Text = "0000-00-00 00:00:00.0";
            }
            else
            {
                Lbl_RealTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.f");
            }
        }

        private bool CanGetTimeFromServer()
        {
            return false;
        }

        private void Tim_DataRead_Tick(object sender, EventArgs e)
        {
            RefreshChart(ChartRefreshType.RefreshOne);
            RefreshGridView(ChartRefreshType.RefreshOne);
            if (IsRunning())
            {
                GetProdData();
            }
        }

        /// <summary>
        /// DB로부터 생산현황에 대한 정보를 받아오는 함수
        /// </summary>
        private void GetProdData()
        {
            if (currentOrder <= 0)
                return;

            DBConnect dbConnect = new DBConnect();
            DataSet pointSet = dbConnect.ExecuteProcedure("SPP_CHT_GET_POINTS", new SqlDbType[] { SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Int },
                                                                                   new object[] { GetMachCode(), "PQ", 1 });
            if (!dbConnect.HasContent(pointSet))
            {
                FRM_MessageBox.Show("생산량 데이터를 가져오는데 실패하였습니다", "경고");
                return;
            }

            totalAmount = int.Parse(pointSet.Tables[0].Rows[0][1].ToString());
            ShowAmount();
        }
        #endregion

        #region ---- Order (작업지시의 처리 관련 함수)
        private void Btn_OrderStart_Click(object sender, EventArgs e)
        {
            int orderCode = GetSelectedOrder();
            if (currentOrder == orderCode && IsRunning())
            {
                OrderFinish();
            }
            else if (IsRunning())
            {
                FRM_MessageBox.Show("현재 다른 제품을 생산중입니다.", "주의");
                return;
            }
            else
            {
                OrderStart(orderCode);
            }
        }

        private void OrderStart(int orderCode)
        {
            if (!CheckOrderSettings())
                return;

            currentOrder = orderCode;
            currentOrderData = new OrderData(Txt_User.Text, Txt_User.Tag.ToString(),
                                             Txt_ItemName.Text, Txt_ItemCode.Text,
                                             Txt_Mold.Text, Txt_Mold.Tag.ToString());
            targetAmount = int.Parse(Txt_TargetAmount.Text);
            currentOrder_Start = GetCurrentTime();
            Btn_Order_SetImage(true, false);

            ChangePLCMode(PLCMode.Read);   // IO서버가 생산품 수 데이터를 읽어들이도록 처리
        }
        private bool CheckOrderSettings()
        {
            return GetSelectedOrder() >= 0 && Txt_User.Tag != null && Txt_Mold.Tag != null;
        }

        private void OrderFinish()
        {
            ChangePLCMode(PLCMode.Write);   // 더이상 IO서버가 생산품 수 데이터를 읽어들이지 않도록 처리

            DBConnect dbConnect = new DBConnect();
            bool incomplete;
            DialogResult dialogResult = FRM_MessageBox.ShowCustom("종료 방식을 선택해주세요", "종료 방식 선택", FRM_MessageBox.MessageBoxType.YesNoCancel,
                                                            new FRM_MessageBox.BGColor[] { FRM_MessageBox.BGColor.Red, FRM_MessageBox.BGColor.Green, FRM_MessageBox.BGColor.Gray },
                                                                            new string[] { "Holding", "작업 종료", "취소" });
            if (dialogResult == DialogResult.Yes)
            {
                incomplete = true;
            }
            else if (dialogResult == DialogResult.No)
            {
                incomplete = false;
                foreach (Control ctrl in Flp_Order.Controls)
                {
                    Label lbl = ctrl as Label;
                    if (lbl.Tag.ToString() == currentOrder.ToString())
                    {
                        Flp_Order.Controls.Remove(ctrl);
                        break;
                    }
                }
            }
            else
            {
                return;
            }

            DataSet orderSet = dbConnect.ExecuteProcedure("SPP_SET_OUTPUT",
                                         new SqlDbType[] { SqlDbType.Int, SqlDbType.Date, SqlDbType.Time, SqlDbType.Time,
                                                           SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar,
                                                           SqlDbType.Int, SqlDbType.Int,  SqlDbType.Int,  SqlDbType.Bit,
                                                           SqlDbType.NVarChar, SqlDbType.VarChar},
                                            new object[] { currentOrder, GetCurrentTime().ToString("yyyy-MM-dd"), currentOrder_Start.ToString("HH:mm:ss") ,GetCurrentTime().ToString("HH:mm:ss"),
                                                           GetDNCode(),       GetItemCode(),     GetMachCode(),     GetMoldCode(),
                                                           GetTotalQty(), GetNormalQty(), GetDefectQty(), incomplete,
                                                           null,              GetUserID() });

            if (dialogResult != DialogResult.Yes)
                ResetOrderSelect();
            currentOrder = 0;
            targetAmount = 0;
            totalAmount = 0;
            defectAmount = 0;
            Btn_Order_SetImage(false, false);
        }

        enum PLCMode { Read, Write }
        /// <summary>
        /// TB_PLC_Config의 값을 수정함으로써 IO서버와 간접적으로 통신하는 함수
        /// </summary>
        private void ChangePLCMode(PLCMode mode)
        {
            DBConnect dbConnect = new DBConnect();

            IniFile ini = new IniFile();
            ini.Load(IniData.SettingIniFile);
            IniSection equipSect = ini["Equipment"];
            string plcCode = equipSect["PLC_Code"].ToString();
            string modeChar = (mode == PLCMode.Read) ? "R" : "W";

            DataSet nullSet = dbConnect.ExecuteProcedure("SPP_SET_PLC_FLAG", new SqlDbType[] { SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Char },
                                                                                new object[] { plcCode, GetMachCode(), modeChar });
        }
        #endregion

        #region ---- Chart
        enum ChartRefreshType { RefreshAll, RefreshOne }
        const int MaxPointAmount = 300;
        Dictionary<string, Chart> chartLinkList = new Dictionary<string, Chart>();
        Dictionary<string, TabPage> tpgLinkList = new Dictionary<string, TabPage>();

        private void InitializeChart()
        {
            string seriesType = "TP1";
            chartLinkList[seriesType] = Cht_Temp1;
            chartLinkList[seriesType].Series[$"Ser_{seriesType}"].BorderColor = Color.LightSkyBlue;
            chartLinkList[seriesType].Series[$"Ser_{seriesType}"].MarkerColor = Color.Blue;
            tpgLinkList[seriesType] = Tpg_Temp1;
            InitializeSeries(seriesType);
            chartLinkList[seriesType].Series[$"Ser_{seriesType}"].IsVisibleInLegend = false;

            seriesType = "TP2";
            chartLinkList[seriesType] = Cht_Temp2;
            chartLinkList[seriesType].Series[$"Ser_{seriesType}"].BorderColor = Color.LightSkyBlue;
            chartLinkList[seriesType].Series[$"Ser_{seriesType}"].MarkerColor = Color.Blue;
            tpgLinkList[seriesType] = Tpg_Temp2;
            InitializeSeries(seriesType);
            chartLinkList[seriesType].Series[$"Ser_{seriesType}"].IsVisibleInLegend = false;

            RefreshChart(ChartRefreshType.RefreshAll);
        }

        private void InitializeSeries(string type)
        {
            DBConnect dbConnect = new DBConnect();
            DataSet qualSet = dbConnect.ExecuteProcedure("SPP_GET_QUALDATA", new SqlDbType[] { SqlDbType.VarChar },
                                                                                new object[] { type });
            if (!dbConnect.HasContent(qualSet))
            {
                FRM_MessageBox.Show("품질 코드 데이터를 가져오는데 실패하였습니다", "경고");
                return;
            }

            double[] minmax = new double[2];
            minmax[0] = double.Parse(qualSet.Tables[0].Rows[0][2].ToString());    // 최소 범위값
            minmax[1] = double.Parse(qualSet.Tables[0].Rows[0][1].ToString());    // 최대 범위값

            chartLinkList[type].Series[$"Ser_{type}"].Tag = minmax;
            chartLinkList[type].Series[$"Ser_{type}"].BorderWidth = 3;
            chartLinkList[type].Series[$"Ser_{type}"].XValueType = ChartValueType.DateTime;
            chartLinkList[type].ChartAreas[$"{type}Area"].AxisX.LabelStyle.Format = "HH:mm:ss";
            chartLinkList[type].ChartAreas[$"{type}Area"].AxisY.Minimum = minmax[0] * 1.25 - minmax[1] * 0.25;  //minmax[0] * 1.5 - minmax[1] * 0.5;
            chartLinkList[type].ChartAreas[$"{type}Area"].AxisY.Maximum = minmax[1] * 1.25 - minmax[0] * 0.25;  //minmax[1] * 1.5 - minmax[0] * 0.5;
            double width = (minmax[1] - minmax[0]) * 0.5 * 0.5;
            chartLinkList[type].ChartAreas[$"{type}Area"].AxisY.Interval = width;

            // 품질 경계를 표시하기 위한 코드
            StripLine lineMin = new StripLine();
            lineMin.Interval = 0;
            lineMin.StripWidth = width; // chartLinkList[type].ChartAreas[$"{type}Area"].AxisY.Interval;
            lineMin.BackColor = Color.FromArgb(255, 240, 236);
            lineMin.IntervalOffset = chartLinkList[type].ChartAreas[$"{type}Area"].AxisY.Minimum;

            StripLine lineMax = new StripLine();
            lineMax.Interval = 0;
            lineMax.StripWidth = width; // chartLinkList[type].ChartAreas[$"{type}Area"].AxisY.Interval;5
            lineMax.BackColor = Color.FromArgb(255, 240, 236);
            lineMax.IntervalOffset = minmax[1];

            chartLinkList[type].ChartAreas[$"{type}Area"].AxisY.StripLines.Add(lineMin);
            chartLinkList[type].ChartAreas[$"{type}Area"].AxisY.StripLines.Add(lineMax);

            tpgLinkList[type].Text = qualSet.Tables[0].Rows[0][0].ToString();
        }

        private void RefreshChart(ChartRefreshType type)
        {
            RefreshSeries(chartLinkList["TP1"].Series["Ser_TP1"], type);
            RefreshSeries(chartLinkList["TP2"].Series["Ser_TP2"], type);

            // 실시간 반영에 따른 X축 범위의 변화가 요구됨 - 하지 않을 경우 실시간으로 추가된 점들이 X축 범위 밖에 그려져 화면에 표시되지 않게 됨
            chartLinkList["TP1"].ChartAreas[$"TP1Area"].AxisX.Minimum = chartLinkList["TP1"].Series["Ser_TP1"].Points[0].XValue;
            chartLinkList["TP1"].ChartAreas[$"TP1Area"].AxisX.Maximum = chartLinkList["TP1"].Series["Ser_TP1"].Points[chartLinkList["TP1"].Series["Ser_TP1"].Points.Count - 1].XValue;
            chartLinkList["TP2"].ChartAreas[$"TP2Area"].AxisX.Minimum = chartLinkList["TP1"].Series["Ser_TP1"].Points[0].XValue;
            chartLinkList["TP2"].ChartAreas[$"TP2Area"].AxisX.Maximum = chartLinkList["TP1"].Series["Ser_TP1"].Points[chartLinkList["TP1"].Series["Ser_TP1"].Points.Count - 1].XValue;
        }

        private void RefreshSeries(Series ser, ChartRefreshType type)
        {
            string qmCode = ser.Name.Substring(4);
            int pointAmount = (type == ChartRefreshType.RefreshAll) ? MaxPointAmount : 1;
            DBConnect dbConnect = new DBConnect();
            DataSet pointSet = dbConnect.ExecuteProcedure("SPP_CHT_GET_POINTS", new SqlDbType[] { SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Int },
                                                                                   new object[] { GetMachCode(), qmCode, pointAmount });
            if (!dbConnect.HasContent(pointSet))
            {
                FRM_MessageBox.Show("금형 온도 데이터를 가져오는데 실패하였습니다", "경고");
                return;
            }

            DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();
            dtFormat.FullDateTimePattern = "yyyy-MM-dd HH:mm:ss";
            for (int i = pointSet.Tables[0].Rows.Count - 1; i >= 0; i--)
            {
                DataRow row = pointSet.Tables[0].Rows[i];   // "SPP_CHT_GET_POINTS" 저장프로시저로 가져오는 데이터는 날짜가 최근순으로 저장이 되어있으며
                                                            // 이를 역순으로 오래된 순서부터 점으로 만들어야 정상적으로 데이터가 기록된다
                                                            // 선은 마지막으로 찍은 점과 연결되며 역순으로 하지 않으면 마지막으로 찍은 점은 가장 오래된 데이터가 되어버린다
                DateTime pointTime = Convert.ToDateTime(row[0].ToString(), dtFormat);
                double pointVal = double.Parse(row[1].ToString());
                if (!HasDuplicate(ser, pointTime))
                {
                    if (ser.Points.Count >= MaxPointAmount)
                        ser.Points.RemoveAt(0);

                    AddPoint(ser, pointTime, pointVal);
                }
            }
        }

        private void AddPoint(Series ser, DateTime xDate, double yVal)
        {
            ser.Points.AddXY(xDate, yVal);

            int insertedIndex = ser.Points.Count - 1;
            ser.Points[insertedIndex].MarkerStyle = MarkerStyle.Circle;

            double[] minmax = ser.Tag as double[];
            double minimum = minmax[0];
            double maximum = minmax[1];

            if (yVal > maximum || yVal < minimum)                   // 범위를 벗어나는 값에 대해서는 붉은 점으로 표시한다
            {
                ser.Points[insertedIndex].MarkerColor = Color.OrangeRed;
                ser.Points[insertedIndex].BorderColor = Color.Coral;
                ser.Points[insertedIndex].MarkerSize = 10;
            }
            else
            {
                ser.Points[insertedIndex].MarkerSize = 7;
            }
        }

        private bool HasDuplicate(Series ser, DateTime xDate)
        {
            DataPointCollection points = ser.Points;
            for (int i = points.Count - 1; i >= 0; i--)                  // 오래된 데이터부터 차트에 추가하므로 가장 마지막 점이 가장 최근 데이터임
            {
                int comp = xDate.ToOADate().CompareTo(points[i].XValue); // time.ToOADate() - X축 값과 동일한 시간인지 비교하기 위한 Datetime->Double 변환함수
                if (comp == 0)
                    return true;
                else if (comp > 0)                                       // xDate 값이 Point 값보다 이후의 시점이라면
                    return false;                                        //     비교중인 Point보다 더 이전 시점의 Point들(이후에 비교하게 될 Point들)과는 일자가 중복될 수 없음
            }
            return false;
        }
        #endregion
        #region -------- DataGridView
        private void InitializeDGV()
        {
            Size textSize = TextRenderer.MeasureText("1234-56-78 12:34:56.7", Dgv_Temp1.DefaultCellStyle.Font);

            Dgv_Temp1.ColumnCount = 2;
            Dgv_Temp1.Columns[0].Name = "QualDate";
            Dgv_Temp1.Columns[0].HeaderText = "시간";
            Dgv_Temp1.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Dgv_Temp1.Columns[0].Width = textSize.Width + 8;
            Dgv_Temp1.Columns[1].Name = "Value";
            Dgv_Temp1.Columns[1].HeaderText = "내경온도";
            Dgv_Temp1.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Dgv_Temp1.Columns[1].Width = 80;
            Dgv_Temp1.RowTemplate.Height = textSize.Height + 6;
            Dgv_Temp1.RowHeadersWidth = 5;

            Dgv_Temp2.ColumnCount = 2;
            Dgv_Temp2.Columns[0].Name = "QualDate";
            Dgv_Temp2.Columns[0].HeaderText = "시간";
            Dgv_Temp2.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Dgv_Temp2.Columns[0].Width = textSize.Width + 8;
            Dgv_Temp2.Columns[1].Name = "Value";
            Dgv_Temp2.Columns[1].HeaderText = "외경온도";
            Dgv_Temp2.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Dgv_Temp2.Columns[1].Width = 80;
            Dgv_Temp2.RowTemplate.Height = textSize.Height + 6;
            Dgv_Temp2.RowHeadersWidth = 5;

            Dgv_Amount.ColumnCount = 2;
            Dgv_Amount.Columns[0].Name = "QualDate";
            Dgv_Amount.Columns[0].HeaderText = "시간";
            Dgv_Amount.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Dgv_Amount.Columns[0].Width = textSize.Width + 8;
            Dgv_Amount.Columns[1].Name = "Value";
            Dgv_Amount.Columns[1].HeaderText = "생산실적";
            Dgv_Amount.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Dgv_Amount.Columns[1].Width = 80;
            Dgv_Amount.RowTemplate.Height = textSize.Height + 6;
            Dgv_Amount.RowHeadersWidth = 5;

            RefreshGridView(ChartRefreshType.RefreshAll);
        }

        private void RefreshGridView(ChartRefreshType type)
        {
            int pointAmount = (type == ChartRefreshType.RefreshAll) ? MaxPointAmount : 1;
            DBConnect dbConnect = new DBConnect();
            DataSet pointSet = dbConnect.ExecuteProcedure("SPP_CHT_GET_TOPPOINTS", new SqlDbType[] { SqlDbType.VarChar, SqlDbType.Int },
                                                                                      new object[] { GetMachCode(), pointAmount });
            if (!dbConnect.HasContent(pointSet))
            {
                FRM_MessageBox.Show("품질 데이터를 가져오는데 실패하였습니다", "경고");
                return;
            }

            if (type == ChartRefreshType.RefreshAll)
            {
                Dgv_Temp1.Rows.Clear();
                Dgv_Temp2.Rows.Clear();
                Dgv_Amount.Rows.Clear();
            }

            foreach (DataRow row in pointSet.Tables[0].Rows)
            {
                // Dgv_Temp.DataSource에 DataTable을 할당한 후에는 Dgv_Temp.Rows.Insert를 통해 직접 GridView에 데이터를 넣을 수 없다
                // 따라서 해당 경우에는 할당된 DataTable에 데이터를 추가하는 등 간접적인 방법을 사용해야함
                switch (row[0].ToString())
                {
                    case "내경 온도":
                        if (HasDuplicate_Dgv(Dgv_Temp1.Rows, row))
                            continue;
                        GridView_AddRow(Dgv_Temp1, row[1].ToString(), $"{double.Parse(row[2].ToString()):N2}");
                        break;
                    case "외경 온도":
                        if (HasDuplicate_Dgv(Dgv_Temp2.Rows, row))
                            continue;
                        GridView_AddRow(Dgv_Temp2, row[1].ToString(), $"{double.Parse(row[2].ToString()):N2}");
                        break;
                    case "생산 실적":
                        if (HasDuplicate_Dgv(Dgv_Amount.Rows, row))
                            continue;
                        GridView_AddRow(Dgv_Amount, row[1].ToString(), row[2].ToString());
                        break;
                }
            }
        }

        private void GridView_AddRow(DataGridView view, string date, string value)
        {
            if (view.Rows.Count >= MaxPointAmount)
                view.Rows.RemoveAt(view.Rows.Count - 1);
            view.Rows.Insert(0, date, value);
        }

        private bool HasDuplicate_Dgv(DataGridViewRowCollection rows, DataRow row)
        {
            if (rows.Count <= 0)
                return false;

            foreach (DataGridViewRow compRow in rows)
            {
                if (string.Compare(row[1].ToString(), compRow.Cells[0].Value.ToString()) > 0)       // rows 내의 아직 비교하지 않은 데이터들이 row보다 이전 데이터들 뿐이라면
                    return false;                                                                   // 중복은 확실히 없다
                else if (row[1].ToString() == compRow.Cells[0].Value.ToString())
                    return true;
            }
            return false;
        }
        #region ---- DataGridView Scroll
        private void Btn_DgvDat_ScrUp_MouseDown(object sender, MouseEventArgs e)
        {
            Btn_DgvDat_SetImage(Btn_DgvDat_ScrUp, "up", true);
            TickEvent_DgvScrUp(sender, e);
            Tim_Scroll.Start(TickEvent_DgvScrUp);
        }

        private void Btn_DgvDat_ScrDn_MouseDown(object sender, MouseEventArgs e)
        {
            Btn_DgvDat_SetImage(Btn_DgvDat_ScrDn, "down", true);
            TickEvent_DgvScrDn(sender, e);
            Tim_Scroll.Start(TickEvent_DgvScrDn);
        }

        private void Btn_DgvDat_Scrup_MouseUp(object sender, MouseEventArgs e)
        {
            Btn_DgvDat_SetImage(Btn_DgvDat_ScrUp, "up", false);
            Tim_Scroll.Stop();
        }

        private void Btn_DgvDat_ScrDn_MouseUp(object sender, MouseEventArgs e)
        {
            Btn_DgvDat_SetImage(Btn_DgvDat_ScrDn, "down", false);
            Tim_Scroll.Stop();
        }

        private void TickEvent_DgvScrUp(object sender, EventArgs e)
        {
            int scrollAmount = (Dgv_Temp1.Height - Dgv_Temp1.ColumnHeadersHeight) / Dgv_Temp1.RowTemplate.Height;
            Dgv_Data_Scroll(Dgv_Temp1, Dgv_Temp1.FirstDisplayedScrollingRowIndex - scrollAmount);
            Dgv_Data_Scroll(Dgv_Temp2, Dgv_Temp2.FirstDisplayedScrollingRowIndex - scrollAmount);
            Dgv_Data_Scroll(Dgv_Amount, Dgv_Amount.FirstDisplayedScrollingRowIndex - scrollAmount);
        }

        private void TickEvent_DgvScrDn(object sender, EventArgs e)
        {
            int scrollAmount = (Dgv_Temp1.Height - Dgv_Temp1.ColumnHeadersHeight) / Dgv_Temp1.RowTemplate.Height;
            Dgv_Data_Scroll(Dgv_Temp1, Dgv_Temp1.FirstDisplayedScrollingRowIndex + scrollAmount);
            Dgv_Data_Scroll(Dgv_Temp2, Dgv_Temp2.FirstDisplayedScrollingRowIndex + scrollAmount);
            Dgv_Data_Scroll(Dgv_Amount, Dgv_Amount.FirstDisplayedScrollingRowIndex + scrollAmount);
        }

        private void Dgv_Data_Scroll(DataGridView view, int targetIndex)
        {
            if (targetIndex <= 0)
                targetIndex = 0;
            else if (targetIndex >= view.Rows.Count)        // targetIndex >= 1인 상황이므로 최소한 view.Rows.Count값은 1 이상이 될것
                targetIndex = view.Rows.Count - 1;

            if (targetIndex <= view.Rows.Count - 1 && view.Rows.Count > 0)
                view.FirstDisplayedScrollingRowIndex = targetIndex;
        }
        #endregion
        #endregion
    }

    class OrderData
    {
        public string userName;
        public string userNo;
        public string itemName;
        public string itemCode;
        public string moldName;
        public string moldCode;

        public OrderData(string userName, string userNo, string itemName, string itemCode, string moldName, string moldCode)
        {
            this.userName = userName;
            this.userNo = userNo;
            this.itemName = itemName;
            this.itemCode = itemCode;
            this.moldName = moldName;
            this.moldCode = moldCode;
        }
    }
}