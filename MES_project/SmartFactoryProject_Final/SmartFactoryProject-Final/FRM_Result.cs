using SmartFactoryProject_Final.Common;
using SmartFactoryProject_Final.CustomControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SmartFactoryProject_Final
{
    public partial class FRM_Result : Form
    {
        CountTimer Tim_Scroll = new CountTimer();

        public FRM_Result(Form parent)
        {
            InitializeComponent();
            this.MdiParent = parent;
        }

        #region ---- Initialize
        private void FRM_Result_Load(object sender, EventArgs e)
        {
            InitializeLayout();
            Initialize();
            InitializeGridView();
            InitializeChart();
        }

        private void Initialize()
        {
            DateTime now = DateTime.Now;
            Txt_Start_Year.Text = now.Year.ToString();
            Txt_Start_Month.Text = now.Month.ToString();
            Txt_Start_Day.Text = now.Day.ToString();

            Txt_End_Year.Text = now.Year.ToString();
            Txt_End_Month.Text = now.Month.ToString();
            Txt_End_Day.Text = now.Day.ToString();

            SetRealTime();
        }

        private void InitializeGridView()
        {
            Size textSize = TextRenderer.MeasureText("1234-56-78 12:34:56.7", Dgv_ItemGroup.DefaultCellStyle.Font);

            Dgv_ItemGroup.ColumnCount = 5; // item코드 총생산, 정상품, 불량품, 불량률
            Dgv_ItemGroup.Columns[0].Name = "ItemCode";
            Dgv_ItemGroup.Columns[0].HeaderText = "품목코드";
            Dgv_ItemGroup.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Dgv_ItemGroup.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Dgv_ItemGroup.Columns[0].Width = 120;
            Dgv_ItemGroup.Columns[1].Name = "Total";
            Dgv_ItemGroup.Columns[1].HeaderText = "총 생산량";
            Dgv_ItemGroup.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Dgv_ItemGroup.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Dgv_ItemGroup.Columns[1].Width = 70;
            Dgv_ItemGroup.Columns[2].Name = "Normal";
            Dgv_ItemGroup.Columns[2].HeaderText = "정상품 수";
            Dgv_ItemGroup.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Dgv_ItemGroup.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Dgv_ItemGroup.Columns[2].Width = 70;
            Dgv_ItemGroup.Columns[3].Name = "Defect";
            Dgv_ItemGroup.Columns[3].HeaderText = "불량품 수";
            Dgv_ItemGroup.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Dgv_ItemGroup.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Dgv_ItemGroup.Columns[3].Width = 70;
            Dgv_ItemGroup.Columns[4].Name = "DefectRate";
            Dgv_ItemGroup.Columns[4].HeaderText = "불량률";
            Dgv_ItemGroup.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Dgv_ItemGroup.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Dgv_ItemGroup.Columns[4].Width = 80;
            Dgv_ItemGroup.RowTemplate.Height = textSize.Height + 6;
            Dgv_ItemGroup.RowHeadersWidth = 5;
            Dgv_ItemGroup.ColumnHeadersHeight = 30;

            Dgv_MachGroup.ColumnCount = 5; // item코드 총생산, 정상품, 불량품, 불량률
            Dgv_MachGroup.Columns[0].Name = "MachCode";
            Dgv_MachGroup.Columns[0].HeaderText = "설비 코드";
            Dgv_MachGroup.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Dgv_MachGroup.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Dgv_MachGroup.Columns[0].Width = 120;
            Dgv_MachGroup.Columns[1].Name = "Total";
            Dgv_MachGroup.Columns[1].HeaderText = "총 생산량";
            Dgv_MachGroup.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Dgv_MachGroup.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Dgv_MachGroup.Columns[1].Width = 70;
            Dgv_MachGroup.Columns[2].Name = "Normal";
            Dgv_MachGroup.Columns[2].HeaderText = "정상품 수";
            Dgv_MachGroup.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Dgv_MachGroup.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Dgv_MachGroup.Columns[2].Width = 70;
            Dgv_MachGroup.Columns[3].Name = "Defect";
            Dgv_MachGroup.Columns[3].HeaderText = "불량품 수";
            Dgv_MachGroup.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Dgv_MachGroup.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Dgv_MachGroup.Columns[3].Width = 70;
            Dgv_MachGroup.Columns[4].Name = "DefectRate";
            Dgv_MachGroup.Columns[4].HeaderText = "불량률";
            Dgv_MachGroup.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Dgv_MachGroup.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Dgv_MachGroup.Columns[4].Width = 80;
            Dgv_MachGroup.RowTemplate.Height = textSize.Height + 6;
            Dgv_MachGroup.RowHeadersWidth = 5;
            Dgv_MachGroup.ColumnHeadersHeight = 30;
        }

        private void InitializeChart()
        {
            Cht_ItemGroup.Series["Normal"].Color = Color.LightGreen;
            Cht_ItemGroup.Series["Normal"].IsValueShownAsLabel = true;
            Cht_ItemGroup.Series["Normal"].IsVisibleInLegend = false;
            Cht_ItemGroup.Series["Defect"].Color = Color.Salmon;
            Cht_ItemGroup.Series["Defect"].IsValueShownAsLabel = true;
            Cht_ItemGroup.Series["Defect"].IsVisibleInLegend = false;


            Cht_MachGroup.Series["Normal"].Color = Color.LightGreen;
            Cht_MachGroup.Series["Normal"].IsValueShownAsLabel = true;
            Cht_MachGroup.Series["Normal"].IsVisibleInLegend = false;
            Cht_MachGroup.Series["Defect"].Color = Color.Salmon;
            Cht_MachGroup.Series["Defect"].IsValueShownAsLabel = true;
            Cht_MachGroup.Series["Defect"].IsVisibleInLegend = false;
        }
        #endregion
        #region ---- Reset
        private void ResetGridView(DataGridView gridView)
        {
            gridView.Rows.Clear();
        }

        private void ResetChart(Chart cht)
        {
            cht.Series["Normal"].Points.Clear();
            cht.Series["Defect"].Points.Clear();
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

            ctrlLayout.Control_Sizing(Lbl_Time, this.Size, 0.15, 0.03);
            ctrlLayout.Control_Positioning(Lbl_Time, this.Size, 0.015, 0.03);
            Lbl_Time.Font = ctrlLayout.GetProperFontSize("Tahoma", Lbl_Time.Height, 0.9);
            Lbl_Time.ForeColor = Color.White;

            ctrlLayout.Control_Sizing(Lbl_PeriodText, this.Size, 0.05, 0.05);
            ctrlLayout.Control_Positioning(Lbl_PeriodText, this.Size, 0.05, 0.12);
            Lbl_PeriodText.Font = ctrlLayout.GetProperFontSize("Tahoma", Lbl_PeriodText.Height, 0.8);

            Txt_Start_Year.DefaultSetting();
            ctrlLayout.Control_Sizing(Txt_Start_Year, this.Size, 0.075, 0.05);
            ctrlLayout.Control_Positioning(Txt_Start_Year, this.Size, 0.12, 0.12);
            Txt_Start_Year.Font = ctrlLayout.GetProperFontSize("Tahoma", Txt_Start_Year.Height, 0.8);
            Txt_Start_Month.DefaultSetting();
            ctrlLayout.Control_Sizing(Txt_Start_Month, this.Size, 0.075, 0.05);
            ctrlLayout.Control_Positioning(Txt_Start_Month, this.Size, 0.23, 0.12);
            Txt_Start_Month.Font = ctrlLayout.GetProperFontSize("Tahoma", Txt_Start_Month.Height, 0.8);
            Txt_Start_Day.DefaultSetting();
            ctrlLayout.Control_Sizing(Txt_Start_Day, this.Size, 0.075, 0.05);
            ctrlLayout.Control_Positioning(Txt_Start_Day, this.Size, 0.34, 0.12);
            Txt_Start_Day.Font = ctrlLayout.GetProperFontSize("Tahoma", Txt_Start_Day.Height, 0.8);

            ctrlLayout.Control_Sizing(Lbl_SYText, this.Size, 0.03, 0.05);
            ctrlLayout.Control_Positioning(Lbl_SYText, this.Size, 0.195, 0.12);
            Lbl_SYText.Font = ctrlLayout.GetProperFontSize("Tahoma", Lbl_SYText.Height, 0.8);
            ctrlLayout.Control_Sizing(Lbl_SMText, this.Size, 0.03, 0.05);
            ctrlLayout.Control_Positioning(Lbl_SMText, this.Size, 0.305, 0.12);
            Lbl_SMText.Font = ctrlLayout.GetProperFontSize("Tahoma", Lbl_SMText.Height, 0.8);
            ctrlLayout.Control_Sizing(Lbl_SDText, this.Size, 0.03, 0.05);
            ctrlLayout.Control_Positioning(Lbl_SDText, this.Size, 0.415, 0.12);
            Lbl_SDText.Font = ctrlLayout.GetProperFontSize("Tahoma", Lbl_SDText.Height, 0.8);
            
            ctrlLayout.Control_Sizing(Lbl_DateRange, this.Size, 0.03, 0.05);
            ctrlLayout.Control_Positioning(Lbl_DateRange, this.Size, 0.47, 0.12);
            Lbl_DateRange.Font = ctrlLayout.GetProperFontSize("Tahoma", Lbl_DateRange.Height, 0.8);

            Txt_End_Year.DefaultSetting();
            ctrlLayout.Control_Sizing(Txt_End_Year, this.Size, 0.075, 0.05);
            ctrlLayout.Control_Positioning(Txt_End_Year, this.Size, 0.52, 0.12);
            Txt_End_Year.Font = ctrlLayout.GetProperFontSize("Tahoma", Txt_End_Year.Height, 0.8);
            Txt_End_Month.DefaultSetting();
            ctrlLayout.Control_Sizing(Txt_End_Month, this.Size, 0.075, 0.05);
            ctrlLayout.Control_Positioning(Txt_End_Month, this.Size, 0.63, 0.12);
            Txt_End_Month.Font = ctrlLayout.GetProperFontSize("Tahoma", Txt_End_Month.Height, 0.8);
            Txt_End_Day.DefaultSetting();
            ctrlLayout.Control_Sizing(Txt_End_Day, this.Size, 0.075, 0.05);
            ctrlLayout.Control_Positioning(Txt_End_Day, this.Size, 0.74, 0.12);
            Txt_End_Day.Font = ctrlLayout.GetProperFontSize("Tahoma", Txt_End_Day.Height, 0.8);

            ctrlLayout.Control_Sizing(Lbl_EYText, this.Size, 0.03, 0.05);
            ctrlLayout.Control_Positioning(Lbl_EYText, this.Size, 0.595, 0.12);
            Lbl_EYText.Font = ctrlLayout.GetProperFontSize("Tahoma", Lbl_EYText.Height, 0.8);
            ctrlLayout.Control_Sizing(Lbl_EMText, this.Size, 0.03, 0.05);
            ctrlLayout.Control_Positioning(Lbl_EMText, this.Size, 0.705, 0.12);
            Lbl_EMText.Font = ctrlLayout.GetProperFontSize("Tahoma", Lbl_EMText.Height, 0.8);
            ctrlLayout.Control_Sizing(Lbl_EDText, this.Size, 0.03, 0.05);
            ctrlLayout.Control_Positioning(Lbl_EDText, this.Size, 0.815, 0.12);
            Lbl_EDText.Font = ctrlLayout.GetProperFontSize("Tahoma", Lbl_EDText.Height, 0.8);

            ctrlLayout.Control_Sizing(Dgv_ItemGroup, this.Size, 0.4, 0.35);
            ctrlLayout.Control_Positioning(Dgv_ItemGroup, this.Size, 0.025, 0.2);
            ctrlLayout.Control_Sizing(Btn_ItemGroup_ScrUp, this.Size, 0.045, 0.175);
            ctrlLayout.Control_Positioning(Btn_ItemGroup_ScrUp, this.Size, 0.425, 0.2);
            Btn_ItemGroup_ScrUp.DefaultSetting();
            Btn_Scroll_SetImage(Btn_ItemGroup_ScrUp, "up", false);
            ctrlLayout.Control_Sizing(Btn_ItemGroup_ScrDn, this.Size, 0.045, 0.175);
            ctrlLayout.Control_Positioning(Btn_ItemGroup_ScrDn, this.Size, 0.425, 0.375);
            Btn_ItemGroup_ScrDn.DefaultSetting();
            Btn_Scroll_SetImage(Btn_ItemGroup_ScrDn, "down", false);
            ctrlLayout.Control_Sizing(Cht_ItemGroup, this.Size, 0.5, 0.35);
            ctrlLayout.Control_Positioning(Cht_ItemGroup, this.Size, 0.475, 0.2);

            ctrlLayout.Control_Sizing(Btn_Search, this.Size, 0.1, 0.07);
            ctrlLayout.Control_Positioning(Btn_Search, this.Size, 0.87, 0.11);
            ctrlLayout.SetBackgroundImage(Btn_Search, overallResPath + @"\btn_GrayBase.jpg");
            Btn_Search.Font = ctrlLayout.GetProperFontSize("Tahoma", Btn_Search.Height, 0.7, true);

            ctrlLayout.Control_Sizing(Dgv_MachGroup, this.Size, 0.4, 0.35);
            ctrlLayout.Control_Positioning(Dgv_MachGroup, this.Size, 0.025, 0.6);
            ctrlLayout.Control_Sizing(Btn_MachGroup_ScrUp, this.Size, 0.045, 0.175);
            ctrlLayout.Control_Positioning(Btn_MachGroup_ScrUp, this.Size, 0.425, 0.6);
            Btn_MachGroup_ScrUp.DefaultSetting();
            Btn_Scroll_SetImage(Btn_MachGroup_ScrUp, "up", false);
            ctrlLayout.Control_Sizing(Btn_MachGroup_ScrDn, this.Size, 0.045, 0.175);
            ctrlLayout.Control_Positioning(Btn_MachGroup_ScrDn, this.Size, 0.425, 0.775);
            Btn_MachGroup_ScrDn.DefaultSetting();
            Btn_Scroll_SetImage(Btn_MachGroup_ScrDn, "down", false);
            ctrlLayout.Control_Sizing(Cht_MachGroup, this.Size, 0.5, 0.35);
            ctrlLayout.Control_Positioning(Cht_MachGroup, this.Size, 0.475, 0.6);
        }
        #endregion
        #region -------- Mouse Up & Down
        private void Btn_Scroll_SetImage(Button btn, string direction, bool pressed)
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

        private void Btn_ItemGroup_ScrUp_MouseDown(object sender, MouseEventArgs e)
        {
            Btn_Scroll_SetImage(sender as Button, "up", true);
            TickEvent_ItemScrUp(sender, e);
            Tim_Scroll.Start(TickEvent_ItemScrUp);
        }

        private void Btn_ItemGroup_ScrUp_MouseUp(object sender, MouseEventArgs e)
        {
            Btn_Scroll_SetImage(sender as Button, "up", false);
            Tim_Scroll.Stop();
        }

        private void Btn_ItemGroup_ScrDn_MouseDown(object sender, MouseEventArgs e)
        {
            Btn_Scroll_SetImage(sender as Button, "down", true);
            TickEvent_ItemScrDn(sender, e);
            Tim_Scroll.Start(TickEvent_ItemScrDn);
        }

        private void Btn_ItemGroup_ScrDn_MouseUp(object sender, MouseEventArgs e)
        {
            Btn_Scroll_SetImage(sender as Button, "down", false);
            Tim_Scroll.Stop();
        }

        private void Btn_MachGroup_ScrUp_MouseDown(object sender, MouseEventArgs e)
        {
            Btn_Scroll_SetImage(sender as Button, "up", true);
            TickEvent_MachScrUp(sender, e);
            Tim_Scroll.Start(TickEvent_MachScrUp);
        }

        private void Btn_MachGroup_ScrUp_MouseUp(object sender, MouseEventArgs e)
        {
            Btn_Scroll_SetImage(sender as Button, "up", false);
            Tim_Scroll.Stop();
        }

        private void Btn_MachGroup_ScrDn_MouseDown(object sender, MouseEventArgs e)
        {
            Btn_Scroll_SetImage(sender as Button, "down", true);
            TickEvent_MachScrDn(sender, e);
            Tim_Scroll.Start(TickEvent_MachScrDn);
        }

        private void Btn_MachGroup_ScrDn_MouseUp(object sender, MouseEventArgs e)
        {
            Btn_Scroll_SetImage(sender as Button, "down", false);
            Tim_Scroll.Stop();
        }
        #endregion
        #region -------- Scroll
        private void TickEvent_ItemScrUp(object sender, EventArgs e)
        {
            int scrollAmount = (Dgv_ItemGroup.Height - Dgv_ItemGroup.ColumnHeadersHeight) / Dgv_ItemGroup.RowTemplate.Height;
            Dgv_Data_Scroll(Dgv_ItemGroup, Dgv_ItemGroup.FirstDisplayedScrollingRowIndex - scrollAmount);
        }

        private void TickEvent_ItemScrDn(object sender, EventArgs e)
        {
            int scrollAmount = (Dgv_ItemGroup.Height - Dgv_ItemGroup.ColumnHeadersHeight) / Dgv_ItemGroup.RowTemplate.Height;
            Dgv_Data_Scroll(Dgv_ItemGroup, Dgv_ItemGroup.FirstDisplayedScrollingRowIndex + scrollAmount);
        }
        
        private void TickEvent_MachScrUp(object sender, EventArgs e)
        {
            int scrollAmount = (Dgv_MachGroup.Height - Dgv_MachGroup.ColumnHeadersHeight) / Dgv_MachGroup.RowTemplate.Height;
            Dgv_Data_Scroll(Dgv_MachGroup, Dgv_MachGroup.FirstDisplayedScrollingRowIndex - scrollAmount);
        }

        private void TickEvent_MachScrDn(object sender, EventArgs e)
        {
            int scrollAmount = (Dgv_MachGroup.Height - Dgv_MachGroup.ColumnHeadersHeight) / Dgv_MachGroup.RowTemplate.Height;
            Dgv_Data_Scroll(Dgv_MachGroup, Dgv_MachGroup.FirstDisplayedScrollingRowIndex + scrollAmount);
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

        private void Txt_Date_Click(object sender, EventArgs e)
        {
            TextBox txtBox = sender as TextBox;
            FRM_NumPadUI numpad = new FRM_NumPadUI("", txtBox.Text.Substring(0, txtBox.Text.Length / 2));
            if (numpad.ShowDialog() == DialogResult.OK)
            {
                int result;
                if (!int.TryParse(numpad.GetInputText(), out result))
                    return;

                if (result > 0)                         // 년 월 일 상관없이 모두 같은 함수를 쓰게 만들기 위해 일부러 최대치에 제한을 두지 않았음 (제한을 두면 완전히 통일시킬 수가 없음)
                    txtBox.Text = result.ToString();    //      검색 버튼을 누를 때 입력값이 올바른지 체크를 하므로 굳이 여기서 최대치에 대해 체크하지 않아도 됨
            }
        }
        #region ---- Search
        private void Btn_Search_Click(object sender, EventArgs e)
        {
            if (!IsValidDate(Txt_Start_Year.Text, Txt_Start_Month.Text, Txt_Start_Day.Text) ||
                !IsValidDate(Txt_End_Year.Text, Txt_End_Month.Text, Txt_End_Day.Text))
            {
                return;
            }

            DBConnect dbConnect = new DBConnect();
            string startDate = GetDateString(Txt_Start_Year.Text, Txt_Start_Month.Text, Txt_Start_Day.Text);
            string endDate = GetDateString(Txt_End_Year.Text, Txt_End_Month.Text, Txt_End_Day.Text);
            DataSet resultItem = dbConnect.ExecuteProcedure("SPR_GET_RESULT_BY_ITEM", new SqlDbType[] { SqlDbType.VarChar, SqlDbType.VarChar },
                                                                                         new object[] { startDate, endDate });
            if (!dbConnect.HasContent(resultItem))
                return;

            ResetGridView(Dgv_ItemGroup);
            SetGridView(Dgv_ItemGroup, resultItem);
            ResetChart(Cht_ItemGroup);
            SetChart(Cht_ItemGroup, resultItem);

            DataSet resultMach = dbConnect.ExecuteProcedure("SPR_GET_RESULT_BY_MACH", new SqlDbType[] { SqlDbType.VarChar, SqlDbType.VarChar },
                                                                                         new object[] { startDate, endDate });
            if (!dbConnect.HasContent(resultMach))
                return;

            ResetGridView(Dgv_MachGroup);
            SetGridView(Dgv_MachGroup, resultMach);
            ResetChart(Cht_MachGroup);
            SetChart(Cht_MachGroup, resultMach);
        }

        private string GetDateString(string year, string month, string day)
        {
            string result = year;
            result += (int.Parse(month) < 10) ? $"-0{month}" : $"-{month}";
            result += (int.Parse(day) < 10) ? $"-0{day}" : $"-{day}";

            return result;
        }

        private bool IsValidDate(string year, string month, string day)
        {
            int inputYear, inputMonth, inputDay;
            if (!int.TryParse(Txt_Start_Year.Text, out inputYear) ||
                !int.TryParse(Txt_Start_Month.Text, out inputMonth) ||
                !int.TryParse(Txt_Start_Day.Text, out inputDay))
            {
                return false;
            }

            if (inputYear < 2000 || inputYear > 10000)
                return false;
            else if (inputDay <= 0)
                return false;

            switch (inputMonth) // 0 이하나 13 이상의 월에 대해서는 default로 이동하므로 if문을 통해 inputMonth의 값을 확인할 필요가 없음
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    if (inputDay <= 31)
                        return true;

                    return false;
                case 4:
                case 6:
                case 9:
                case 11:
                    if (inputDay <= 30)
                        return true;

                    return false;
                case 2:
                    if (inputDay <= 28)
                        return true;
                    else if (inputDay == 29 && DateTime.IsLeapYear(inputYear))
                        return true;

                    return false;
                default:
                    return false;
            }
        }

        private void SetGridView(DataGridView gridView, DataSet set)
        {
            foreach (DataRow row in set.Tables[0].Rows)
            {
                string code = row[0].ToString();
                int total = int.Parse(row[1].ToString());
                int normal = int.Parse(row[2].ToString());
                int defect = total - normal;
                double defectRate = (defect / (double)total) * 100;

                gridView.Rows.Insert(gridView.Rows.Count, code, total, normal, defect, $"{defectRate:N2}");
            }
        }

        private void SetChart(Chart cht, DataSet set)
        {
            foreach (DataRow row in set.Tables[0].Rows)
            {
                string code = row[0].ToString();
                int total = int.Parse(row[1].ToString());
                int normal = int.Parse(row[2].ToString());
                int defect = total - normal;

                cht.Series["Normal"].Points.AddXY(code, normal);
                cht.Series["Defect"].Points.AddXY(code, defect);
            }
        }
        #endregion

        #region ---- Periodic Operations
        private void Tim_CheckTime_Tick(object sender, EventArgs e)
        {
            SetRealTime();
        }

        private void SetRealTime()
        {
            if (CanGetTimeFromServer())  /// 서버에서 시간을 가져올 수 있는지를 확인하는 곳
            {
                Lbl_Time.Text = "0000-00-00 00:00:00.0";
            }
            else
            {
                Lbl_Time.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.f");
            }
        }

        private bool CanGetTimeFromServer()
        {
            return false;
        }
        #endregion
    }
}
