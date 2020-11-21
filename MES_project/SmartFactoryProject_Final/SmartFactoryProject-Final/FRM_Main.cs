using SmartFactoryProject_Final.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartFactoryProject_Final
{
    public partial class FRM_Main : Form
    {
        const double LeftPadding = 0;
        const double TopPadding = 0;
        const double RightPadding = 0;
        const double BottomPadding = 0.072;

        Form mdiForm { get; set; }
        FRM_Process frm_process { get; set; }
        FRM_Result frm_result { get; set; }

        public LoginData loginuser;

        public FRM_Main()
        {
            InitializeComponent();
        }
        
        private void Frm_Main_Load(object sender, EventArgs e)
        {
            LogIn(FRM_LogIn.LoginMode.ExitWhenFailed);
            SetLayout();
        }
        
        private void SetLayout()
        {
            ControlLayout ctrlLayout = new ControlLayout();
            IniFile ini = new IniFile();
            ini.Load(IniData.SettingIniFile);
            IniSection resSect = ini["Resources"];
            IniSection equipSect = ini["Equipment"];
            string overallResPath = System.IO.Directory.GetCurrentDirectory() + $@"{resSect["ResourceFolder"]}{resSect["OverallFolder"]}";

            this.Location = new Point(this.Left, 0);
            this.Size = new Size(equipSect["Width"].ToInt(), equipSect["Height"].ToInt());
            this.Padding = new Padding(ctrlLayout.GetXPosByRatio(this.Size, LeftPadding), ctrlLayout.GetYPosByRatio(this.Size, TopPadding),
                                       ctrlLayout.GetXPosByRatio(this.Size, RightPadding), ctrlLayout.GetYPosByRatio(this.Size, BottomPadding));

            ctrlLayout.SetBackgroundImage(Pnl_Drag, overallResPath + @"\bg_bottomBox.png");
            ctrlLayout.Control_Sizing(Pnl_Drag, this.Size, 1, BottomPadding);
            ctrlLayout.Control_Positioning(Pnl_Drag, this.Size, 0, 1, ControlLayout.HorizontalSiding.Left, ControlLayout.VerticalSiding.Bottom);

            #region ---- Pnl_Drag Contents
            ctrlLayout.Control_Sizing(Btn_Process, Pnl_Drag.Size, 0.08, 0.8);
            ctrlLayout.Control_Positioning(Btn_Process, Pnl_Drag.Size, 0.06, 0.5, ControlLayout.HorizontalSiding.Center, ControlLayout.VerticalSiding.Center);
            Btn_Process.DefaultSetting();
            Btn_SetImage(Btn_Process, "Gray", false);
            Btn_Process.Text = "작업지시";
            Btn_Process.Font = ctrlLayout.GetProperFontSize("Tahoma", Btn_Process.Height, 0.6, true);

            ctrlLayout.Control_Sizing(Btn_Result, Pnl_Drag.Size, 0.08, 0.8);
            ctrlLayout.Control_Positioning(Btn_Result, Pnl_Drag.Size, 0.16, 0.5, ControlLayout.HorizontalSiding.Center, ControlLayout.VerticalSiding.Center);
            Btn_Result.DefaultSetting();
            Btn_SetImage(Btn_Result, "Gray", false);
            Btn_Result.Text = "작업결과";
            Btn_Result.Font = ctrlLayout.GetProperFontSize("Tahoma", Btn_Result.Height, 0.6, true);

            ctrlLayout.Control_Sizing(Btn_Logout, Pnl_Drag.Size, 0.08, 0.8);
            ctrlLayout.Control_Positioning(Btn_Logout, Pnl_Drag.Size, 0.86, 0.5, ControlLayout.HorizontalSiding.Center, ControlLayout.VerticalSiding.Center);
            Btn_Logout.DefaultSetting();
            Btn_SetImage(Btn_Logout, "Gray", false);
            Btn_Logout.Text = "로그아웃";
            Btn_Logout.Font = ctrlLayout.GetProperFontSize("Tahoma", Btn_Logout.Height, 0.6, true);
            
            ctrlLayout.Control_Sizing(Btn_Exit, Pnl_Drag.Size, 0.08, 0.8);
            ctrlLayout.Control_Positioning(Btn_Exit, Pnl_Drag.Size, 0.99, 0.5, ControlLayout.HorizontalSiding.Right, ControlLayout.VerticalSiding.Center);
            Btn_Exit.DefaultSetting();
            Btn_SetImage(Btn_Exit, "Red", false);
            Btn_Exit.Text = "종료";
            Btn_Exit.Font = ctrlLayout.GetProperFontSize("Tahoma", Btn_Exit.Height, 0.6, true);
            #endregion
        }

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
        #region ---- Button - Mouse Up&Down
        private void Btn_SetImage (Button btn, string color, bool pressed)
        {
            ControlLayout ctrlLayout = new ControlLayout();
            IniFile ini = new IniFile();
            ini.Load(IniData.SettingIniFile);
            IniSection resSect = ini["Resources"];
            string overallResPath = System.IO.Directory.GetCurrentDirectory() + $@"{resSect["ResourceFolder"]}{resSect["OverallFolder"]}";

            if (pressed)
                ctrlLayout.SetBackgroundImage(btn, overallResPath + $@"\btn_{color}Base_.jpg");
            else
                ctrlLayout.SetBackgroundImage(btn, overallResPath + $@"\btn_{color}Base.jpg");
        }

        private void Btn_Gray_MouseDown(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            Btn_SetImage(btn, "Gray", true);
        }

        private void Btn_Gray_MouseUp(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            Btn_SetImage(btn, "Gray", false);
        }

        private void Btn_Red_MouseDown(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            Btn_SetImage(btn, "Red", true);
        }

        private void Btn_Red_MouseUp(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            Btn_SetImage(btn, "Red", false);
        }


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
        #endregion

        #region ---- Login
        private bool LogIn(FRM_LogIn.LoginMode loginMode)
        {
            FRM_LogIn login = new FRM_LogIn();
            DialogResult result = login.ShowDialog();
            if(result != DialogResult.OK)
            {
                if (loginMode == FRM_LogIn.LoginMode.ExitWhenFailed)
                    this.Close();
                else
                    return false;
            }
            loginuser = login.GetLoginData();
            return true;
        }
        #endregion
        #region ---- Logout
        private void Btn_Logout_Click(object sender, EventArgs e)
        {
            if (FRM_MessageBox.Show("정말로 로그아웃하시겠습니까?", "로그아웃", FRM_MessageBox.MessageBoxType.YesNo) == DialogResult.Yes)
            {
                this.Opacity = 0;
                Reset();
                LogIn(FRM_LogIn.LoginMode.ExitWhenFailed);
                if (this.IsDisposed)    // Login 함수의 결과로 이 폼이 Close될 경우 DisPose되므로 해당 경우에는 이하의 코드를 실행하지 않도록 return한다
                    return;
                Restart();
                this.Opacity = 100;
            }
        }

        private void Reset()
        {
            if (mdiForm == null)
                Application.Restart();
            else
                mdiForm.Hide();
        }

        private void Restart()
        {
            if(mdiForm != null)
                mdiForm.Show();
        }
        #endregion

        private void Btn_Process_Click(object sender, EventArgs e)
        {
            if (mdiForm != null && mdiForm != frm_process)
                mdiForm.Hide();

            if (frm_process == null)
                frm_process = new FRM_Process(this);
            mdiForm = frm_process;
            frm_process.Show(); 
            frm_process.TopLevel = false;
        }


        private void Btn_Result_Click(object sender, EventArgs e)
        {
            if (mdiForm != null && mdiForm != frm_result)
                mdiForm.Hide();

            if (frm_result == null)
                frm_result = new FRM_Result(this);
            mdiForm = frm_result;
            frm_result.Show();
            frm_result.TopLevel = false;
        }

        private void Btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}