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

namespace SmartFactoryProject_Final.Common
{
    public partial class FRM_Select : Form
    {
        Dictionary<object, string> lists;   // lists의 항목들을 버튼으로 만들어 object를 선택시 리턴하는 값으로, string을 버튼의 텍스트로 출력한다
        KeyValuePair<object, string> selected;

        CountTimer Tim_Scroll = new CountTimer();

        public FRM_Select()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
        }

        public FRM_Select(Dictionary<object, string> dict) : this()
        {
            lists = dict;
        }

        #region ---- Initialize
        private void FRM_Select_Load(object sender, EventArgs e)
        {
            SetLayout();
            SetButtons();
        }

        private void SetLayout()
        {
            ControlLayout ctrlLayout = new ControlLayout();
            IniFile ini = new IniFile();
            ini.Load(IniData.SettingIniFile);
            IniSection resSect = ini["Resources"];
            string overallResPath = System.IO.Directory.GetCurrentDirectory() + $@"{resSect["ResourceFolder"]}{resSect["OverallFolder"]}";

            ctrlLayout.SetBackgroundImage(this, overallResPath + @"\pop1_bg.gif");

            ctrlLayout.Control_Sizing(Pnl_Drag, this.Size, 0.96, 0.08);
            ctrlLayout.Control_Positioning(Pnl_Drag, this.Size, 0.5, 0.015, ControlLayout.HorizontalSiding.Center);

            ctrlLayout.Control_Sizing(Lbl_Title, Pnl_Drag.Size, 0.4, 1);
            ctrlLayout.Control_Positioning(Lbl_Title, Pnl_Drag.Size, 0.5, 0, ControlLayout.HorizontalSiding.Center);
            Lbl_Title.Font = ctrlLayout.GetProperFontSize("Tahoma", Lbl_Title.Height, 0.8, true);
            Lbl_Title.ForeColor = Color.DimGray;

            ctrlLayout.Control_Sizing(Pnl_BG, this.Size, 0.725, 0.7);
            ctrlLayout.Control_Positioning(Pnl_BG, this.Size, 0.05, 0.15);

            ctrlLayout.Control_Sizing(Flp_Content, Pnl_BG.Size, 1, 1);
            Flp_Content.AutoScroll = false;
            Flp_Content.AutoSize = true;
            Flp_Content.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Flp_Content.WrapContents = false;
            Flp_Content.FlowDirection = FlowDirection.TopDown;
            
            ctrlLayout.Control_Sizing(Btn_ScrUp, this.Size, 0.12, 0.35);
            ctrlLayout.Control_Positioning(Btn_ScrUp, this.Size, 0.8, 0.15);
            Btn_ScrUp_SetImage(false);

            ctrlLayout.Control_Sizing(Btn_ScrDn, this.Size, 0.12, 0.35);
            ctrlLayout.Control_Positioning(Btn_ScrDn, this.Size, 0.8, 0.5);
            Btn_ScrDn_SetImage(false);

            ctrlLayout.Control_Sizing(Btn_Exit, this.Size, 0.3, 0.1);
            ctrlLayout.Control_Positioning(Btn_Exit, this.Size, 0.5, 0.895, ControlLayout.HorizontalSiding.Center);
            Btn_Exit_SetImage(false);
            Btn_Exit.DefaultSetting();
        }

        private void SetButtons()
        {
            ControlLayout ctrlLayout = new ControlLayout();
            IniFile ini = new IniFile();
            ini.Load(IniData.SettingIniFile);
            IniSection resSect = ini["Resources"];
            string overallResPath = System.IO.Directory.GetCurrentDirectory() + $@"{resSect["ResourceFolder"]}{resSect["OverallFolder"]}";
            
            foreach (KeyValuePair<object, string> pair in lists)
            {
                Button btn = new Button();
                ctrlLayout.Control_Sizing(btn, Pnl_BG.Size, 0.9, 0.125);
                ctrlLayout.SetBackgroundImage(btn, overallResPath + @"\pop_itembox.gif");
                btn.BackColor = Color.Transparent;
                btn.Click += Btn_Content_Click;

                btn.Text = pair.Value;
                btn.Tag = pair.Key;
                btn.Font = ctrlLayout.GetProperFontSize("Tahoma", btn.Height, 0.5);
                btn.ForeColor = Color.RoyalBlue;
                Flp_Content.Controls.Add(btn);
            }
        }
        #endregion

        #region ---- Button - Mouse Up&Down
        private void Btn_Exit_SetImage(bool pressed)
        {
            ControlLayout ctrlLayout = new ControlLayout();
            IniFile ini = new IniFile();
            ini.Load(IniData.SettingIniFile);
            IniSection resSect = ini["Resources"];
            string overallResPath = System.IO.Directory.GetCurrentDirectory() + $@"{resSect["ResourceFolder"]}{resSect["OverallFolder"]}";

            if (pressed)
                ctrlLayout.SetBackgroundImage(Btn_Exit, overallResPath + @"\btn_exit_.gif");
            else
                ctrlLayout.SetBackgroundImage(Btn_Exit, overallResPath + @"\btn_exit.gif");
        }

        private void Btn_Exit_MouseDown(object sender, MouseEventArgs e)
        {
            Btn_Exit_SetImage(true);
        }

        private void Btn_Exit_MouseUp(object sender, MouseEventArgs e)
        {
            Btn_Exit_SetImage(false);
        }

        private void Btn_ScrUp_SetImage(bool pressed)
        {
            ControlLayout ctrlLayout = new ControlLayout();
            IniFile ini = new IniFile();
            ini.Load(IniData.SettingIniFile);
            IniSection resSect = ini["Resources"];
            string overallResPath = System.IO.Directory.GetCurrentDirectory() + $@"{resSect["ResourceFolder"]}{resSect["OverallFolder"]}";

            if (pressed)
                ctrlLayout.SetBackgroundImage(Btn_ScrUp, overallResPath + @"\btn_up_.gif");
            else
                ctrlLayout.SetBackgroundImage(Btn_ScrUp, overallResPath + @"\btn_up.gif");
        }

        private void Btn_ScrDn_SetImage(bool pressed)
        {
            ControlLayout ctrlLayout = new ControlLayout();
            IniFile ini = new IniFile();
            ini.Load(IniData.SettingIniFile);
            IniSection resSect = ini["Resources"];
            string overallResPath = System.IO.Directory.GetCurrentDirectory() + $@"{resSect["ResourceFolder"]}{resSect["OverallFolder"]}";

            if (pressed)
                ctrlLayout.SetBackgroundImage(Btn_ScrDn, overallResPath + @"\btn_down_.gif");
            else
                ctrlLayout.SetBackgroundImage(Btn_ScrDn, overallResPath + @"\btn_down.gif");
        }
        #endregion
        #region ---- Scroll Screen
        private void Btn_ScrUp_MouseDown(object sender, MouseEventArgs e)
        {
            Btn_ScrUp_SetImage(true);
            TickEvent_ScrUp(sender, e);
            Tim_Scroll.Start(TickEvent_ScrUp);
        }

        private void Btn_ScrUp_MouseUp(object sender, MouseEventArgs e)
        {
            Btn_ScrUp_SetImage(false);
            Tim_Scroll.Stop();
        }

        private void Btn_ScrDn_MouseDown(object sender, MouseEventArgs e)
        {
            Btn_ScrDn_SetImage(true);
            TickEvent_ScrDn(sender, e);
            Tim_Scroll.Start(TickEvent_ScrDn);
        }

        private void Btn_ScrDn_MouseUp(object sender, MouseEventArgs e)
        {
            Btn_ScrDn_SetImage(false);
            Tim_Scroll.Stop();
        }

        private void TickEvent_ScrUp(object sender, EventArgs e)
        {
            ControlLayout ctrlLayout = new ControlLayout();
            Panel_VerticalScroll(Flp_Content, Pnl_BG, ctrlLayout.GetYPosByRatio(this.Size, 0.5));
        }

        private void TickEvent_ScrDn(object sender, EventArgs e)
        {
            ControlLayout ctrlLayout = new ControlLayout();
            Panel_VerticalScroll(Flp_Content, Pnl_BG, -ctrlLayout.GetYPosByRatio(this.Size, 0.5));
        }

        private void Panel_VerticalScroll(Panel panel, Panel bg, int scrollValue)
        {
            int min = -panel.Height + bg.Height;
            int max = 0;
            if (min > max)                              // 목록의 길이가 짧아 한 화면을 가득 채우지 못하므로 스크롤의 필요성이 없음
                return;
            int newPos = panel.Location.Y + scrollValue;

            if (newPos <= max && newPos >= min)
                panel.Location = new Point(0, newPos);
            else if (newPos > max)
                panel.Location = new Point(0, max);
            else if (newPos < min)
                panel.Location = new Point(0, min);
        }
        #endregion
        #region ---- Drag Screen
        int xPos { get; set; }
        int yPos { get; set; }
        private void Pnl_Drag_MouseDown(object sender, MouseEventArgs e)
        {
            xPos = e.X;
            yPos = e.Y;
        }
        private void Pnl_Drag_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Location = new Point(this.Location.X + e.X - xPos,
                                          this.Location.Y + e.Y - yPos);
            }
        }
        #endregion

        private void Btn_Content_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            selected = new KeyValuePair<object, string>(btn.Tag, btn.Text);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        public KeyValuePair<object, string> GetSelection()
        {
            return selected;
        }

        private void Btn_Exit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}