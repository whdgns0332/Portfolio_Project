using SmartFactoryProject_Final.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartFactoryProject_Final
{
    public partial class FRM_LogIn : Form
    {
        public enum LoginMode { ExitWhenFailed, Customize }
        LoginData loginUser = new LoginData();

        public FRM_LogIn()
        {
            InitializeComponent();
        }

        private void Frm_Login_Load(object sender, EventArgs e)
        {
            SetLayout();
            ResetTxt_ID();
            ResetTxt_PW();
        }

        private void SetLayout()
        {
            ControlLayout ctrlLayout = new ControlLayout();
            IniFile ini = new IniFile();
            ini.Load(IniData.SettingIniFile);
            IniSection resSect = ini["Resources"];
            string overallResPath = Directory.GetCurrentDirectory() + $@"{resSect["ResourceFolder"]}{resSect["OverallFolder"]}";

            ctrlLayout.MakeCurvedBorder(this, 18, 18);
            ctrlLayout.SetBackgroundImage(this, overallResPath + @"\loginpop_bg.gif");

            ctrlLayout.Control_Sizing(Pnl_Drag, this.Size, 1, 0.2f);

            ctrlLayout.Control_Sizing(Pic_Title, this.Size, 0.05, 0.1);
            ctrlLayout.Control_Positioning(Pic_Title, this.Size, 0.165, 0.05, ControlLayout.HorizontalSiding.Center);
            ctrlLayout.SetImage(Pic_Title, overallResPath + @"\In.png");

            ctrlLayout.Control_Sizing(Lbl_Title, this.Size, 0.15, 0.125);
            ctrlLayout.Control_Positioning(Lbl_Title, this.Size, 0.5, 0.03, ControlLayout.HorizontalSiding.Center);
            Lbl_Title.Font = ctrlLayout.GetProperFontSize("Tahoma", Lbl_Title.Height, 1, true);
            
            ctrlLayout.Control_Sizing(Lbl_ID, this.Size, 0.07, 0.12);
            ctrlLayout.Control_Positioning(Lbl_ID, this.Size, 0.17, 0.38, ControlLayout.HorizontalSiding.Center, ControlLayout.VerticalSiding.Center);
            Lbl_ID.Font = ctrlLayout.GetProperFontSize("Tahoma", Lbl_ID.Height, 0.9);
            ctrlLayout.Control_Sizing(Txt_ID, this.Size, 0.6, 0.1);
            ctrlLayout.Control_Positioning(Txt_ID, this.Size, 0.25, 0.31);
            Txt_ID.Font = ctrlLayout.GetProperFontSize("ArialBlack", Txt_ID.Height, 0.9);
            
            ctrlLayout.Control_Sizing(Lbl_PW, this.Size, 0.09, 0.12);
            ctrlLayout.Control_Positioning(Lbl_PW, this.Size, 0.17, 0.58, ControlLayout.HorizontalSiding.Center, ControlLayout.VerticalSiding.Center);
            Lbl_PW.Font = ctrlLayout.GetProperFontSize("Tahoma", Lbl_PW.Height, 0.9);
            ctrlLayout.Control_Sizing(Txt_PW, this.Size, 0.6, 0.1);
            ctrlLayout.Control_Positioning(Txt_PW, this.Size, 0.25, 0.51);
            Txt_PW.Font = ctrlLayout.GetProperFontSize("ArialBlack", Txt_PW.Height, 0.9);

            ctrlLayout.Control_Sizing(Btn_LogIn, this.Size, 0.2, 0.15);
            ctrlLayout.Control_Positioning(Btn_LogIn, this.Size, 0.35, 0.825, ControlLayout.HorizontalSiding.Center);
            Btn_Login_SetImage(false);
            Btn_LogIn.DefaultSetting();

            ctrlLayout.Control_Sizing(Btn_Exit, this.Size, 0.2, 0.15);
            ctrlLayout.Control_Positioning(Btn_Exit, this.Size, 0.65, 0.825, ControlLayout.HorizontalSiding.Center);
            Btn_Exit_SetImage(false);
            Btn_Exit.DefaultSetting();
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
        private void Btn_Login_SetImage(bool pressed)
        {
            ControlLayout ctrlLayout = new ControlLayout();
            IniFile ini = new IniFile();
            ini.Load(IniData.SettingIniFile);
            IniSection resSect = ini["Resources"];
            string overallResPath = Directory.GetCurrentDirectory() + $@"{resSect["ResourceFolder"]}{resSect["OverallFolder"]}";

            if (pressed)
                ctrlLayout.SetBackgroundImage(Btn_LogIn, overallResPath + @"\btn_login_.gif");
            else
                ctrlLayout.SetBackgroundImage(Btn_LogIn, overallResPath + @"\btn_login.gif");
        }

        private void Btn_LogIn_MouseDown(object sender, MouseEventArgs e)
        {
            Btn_Login_SetImage(true);
        }

        private void Btn_LogIn_MouseUp(object sender, MouseEventArgs e)
        {
            Btn_Login_SetImage(false);
        }

        private void Btn_Exit_SetImage(bool pressed)
        {
            ControlLayout ctrlLayout = new ControlLayout();
            IniFile ini = new IniFile();
            ini.Load(IniData.SettingIniFile);
            IniSection resSect = ini["Resources"];
            string overallResPath = Directory.GetCurrentDirectory() + $@"{resSect["ResourceFolder"]}{resSect["OverallFolder"]}";

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

        #region ---- SetText (ID & PW)
        private void ResetTxt_ID()
        {
            Txt_ID.Text = "User ID";
            Txt_ID.ForeColor = Color.LightGray;
        }

        private void ResetTxt_PW()
        {
            Txt_PW.Text = "Password";
            Txt_PW.ForeColor = Color.LightGray;
        }

        private void SetTxt_ID(string text)
        {
            Txt_ID.Text = text;
            Txt_ID.ForeColor = Color.Black;
        }

        private void SetTxt_PW(string text)
        {
            Txt_PW.Text = text;
            Txt_PW.ForeColor = Color.Black;
        }

        private void Txt_ID_Click(object sender, EventArgs e)
        {
            string text = (Txt_ID.ForeColor == Color.LightGray) ? "" : Txt_ID.Text;
            FRM_KeyBoardUI keyBoardUI = new FRM_KeyBoardUI("ID", text);
            if (keyBoardUI.ShowDialog() == DialogResult.OK)
            {
                string inputText = keyBoardUI.GetInputText();
                if (!string.IsNullOrEmpty(inputText))
                    SetTxt_ID(inputText);
                else
                    ResetTxt_ID();
            }
        }

        private void Txt_PW_Click(object sender, EventArgs e)
        {
            string text = (Txt_PW.ForeColor == Color.LightGray) ? "" : Txt_PW.Text;
            FRM_KeyBoardUI keyBoardUI = new FRM_KeyBoardUI("PW", text, true);
            if (keyBoardUI.ShowDialog() == DialogResult.OK)
            {
                string inputText = keyBoardUI.GetInputText();
                if (!string.IsNullOrEmpty(inputText))
                    SetTxt_PW(inputText);
                else
                    ResetTxt_PW();
            }
        }
        #endregion

        #region ---- Login
        private void Btn_LogIn_Click(object sender, EventArgs e)
        {
            if (!IsLoginInfoValid())
            {
                FRM_MessageBox.Show("ID와 PW를 모두 입력해 주세요", "로그인 실패");
                return;
            }

            LogIn();
        }

        private bool IsLoginInfoValid()
        {
            if (String.IsNullOrEmpty(Txt_ID.Text) || String.IsNullOrEmpty(Txt_PW.Text))
                return false;
            else
                return true;
        }

        private void LogIn()
        {
            DBConnect dbConnect = new DBConnect();
            DataSet loginData;
            loginData = dbConnect.ExecuteProcedure("SP_LOGIN", new SqlDbType[] { SqlDbType.VarChar, SqlDbType.VarChar },
                                                                  new string[] { Txt_ID.Text,       Txt_PW.Text       });

            if(loginData != null && loginData.Tables[0].Rows.Count > 0)
            {
                loginUser.ID = Txt_ID.Text;
                loginUser.dept = loginData.Tables[0].Rows[0][0].ToString();
                loginUser.rank = loginData.Tables[0].Rows[0][1].ToString();
                loginUser.name = loginData.Tables[0].Rows[0][2].ToString();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                if (loginData == null)
                    FRM_MessageBox.Show("로그인 DB의 접속에 실패했습니다", "로그인 실패");
                else if(loginData.Tables[0].Rows.Count == 0)
                    FRM_MessageBox.Show("입력하신 ID 또는 PW가 맞지 않습니다", "로그인 실패");
            }
        }

        public LoginData GetLoginData()
        {
            return loginUser;
        }
        #endregion

        private void Btn_Exit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }

    public struct LoginData
    {
        public string ID;
        public string name;
        public string dept;
        public string rank;
    }
}