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
    public partial class FRM_MessageBox : Form
    {
        public enum MessageBoxType { Normal,        // 단순히 메시지를 전달하기 위한 타입
                                     OkCancel,      // 메시지를 전달하고 거기에 대해 확인, 취소라는 답을 얻기 위한 타입
                                     YesNo,         // 한가지를 묻고 거기에 예, 아니오라는 답중 하나를 반드시 얻기 위한 타입
                                     YesNoCancel    // 한가지를 묻고 거기에 예, 아니오라는 답을 얻기 위한 타입이나 Cancel을 통해 취소도 가능
                                   }
        private MessageBoxType type;
        public FRM_MessageBox(MessageBoxType type = MessageBoxType.Normal)
        {
            InitializeComponent();
            this.type = type;
            this.StartPosition = FormStartPosition.CenterParent;
        }

        public static DialogResult Show(string content, string title = "Message", MessageBoxType type = MessageBoxType.Normal)
        {
            FRM_MessageBox message = new FRM_MessageBox(type);
            message.Lbl_Title.Text = title;
            message.Lbl_Content.Text = content;
            return message.ShowDialog();
        }

        private void FRM_MessageBox_Load(object sender, EventArgs e)
        {
            IniFile ini = new IniFile();
            ini.Load(IniData.SettingIniFile);
            IniSection resSect = ini["Resources"];
            string overallResPath = System.IO.Directory.GetCurrentDirectory() + $@"{resSect["ResourceFolder"]}{resSect["OverallFolder"]}";
            
            ControlLayout ctrlLayout = new ControlLayout();
            ctrlLayout.SetBackgroundImage(this, overallResPath + @"\pop2_bg.gif");
            ctrlLayout.Control_Sizing(Pnl_Drag, this.Size, 1, 0.2);
            ctrlLayout.Control_Positioning(Pnl_Drag, this.Size, 0, 0);
            ctrlLayout.Control_Sizing(Lbl_Title, Pnl_Drag.Size, 0.97, 0.75);
            ctrlLayout.Control_Positioning(Lbl_Title, Pnl_Drag.Size, 0.5, 0.5, ControlLayout.HorizontalSiding.Center, ControlLayout.VerticalSiding.Center);
            Lbl_Title.Font = ctrlLayout.GetProperFontSize("Tahoma", Lbl_Title.Height, 0.5);
            Lbl_Title.ForeColor = Color.Black;

            ctrlLayout.Control_Sizing(Lbl_Content, this.Size, 0.95, 0.55);
            ctrlLayout.Control_Positioning(Lbl_Content, this.Size, 0.5, 0.21, ControlLayout.HorizontalSiding.Center);
            Lbl_Content.Font = ctrlLayout.GetProperFontSize("Tahoma", Lbl_Content.Height, 0.125);

            switch (this.type)
            {
                case MessageBoxType.Normal:         // 첫번째 버튼을 종료 버튼으로서 사용하고 두번째 버튼은 사용하지 않음
                    SetButtonLayout(1);
                    AssignButton_Exit(Btn_First);
                    break;
                case MessageBoxType.OkCancel:       // 첫번째 버튼을 확인 버튼으로, 두번째 버튼을 취소 버튼으로 사용함
                    SetButtonLayout(2);
                    AssignButton_OK(Btn_First);
                    AssignButton_Cancel(Btn_Second);
                    break;
                case MessageBoxType.YesNo:
                    SetButtonLayout(2);
                    AssignButton_Yes(Btn_First);
                    AssignButton_No(Btn_Second);
                    break;
                case MessageBoxType.YesNoCancel:
                    SetButtonLayout(3);
                    AssignButton_Yes(Btn_First);
                    AssignButton_No(Btn_Second);
                    AssignButton_Cancel(Btn_Third);
                    break;
            }
        }

        /// <summary>
        /// 버튼의 크기, 위치를 조정하는 함수
        /// 사용할 버튼의 수에 따라 버튼의 크기와 위치가 달라진다
        /// </summary>
        /// <param name="buttonCount">사용할 버튼의 수</param>
        private void SetButtonLayout(int buttonCount)
        {
            ControlLayout ctrlLayout = new ControlLayout();
            switch (buttonCount){
                case 1:
                    ctrlLayout.Control_Sizing(Btn_First, this.Size, 0.3, 0.15);
                    ctrlLayout.Control_Positioning(Btn_First, this.Size, 0.5, 0.815, ControlLayout.HorizontalSiding.Center);

                    Btn_Second.Dispose();
                    Btn_Third.Dispose();
                    break;

                case 2:
                    ctrlLayout.Control_Sizing(Btn_First, this.Size, 0.3, 0.15);
                    ctrlLayout.Control_Positioning(Btn_First, this.Size, 0.3, 0.815, ControlLayout.HorizontalSiding.Center);

                    ctrlLayout.Control_Sizing(Btn_Second, this.Size, 0.3, 0.15);
                    ctrlLayout.Control_Positioning(Btn_Second, this.Size, 0.7, 0.815, ControlLayout.HorizontalSiding.Center);

                    Btn_Third.Dispose();
                    break;

                case 3:
                    ctrlLayout.Control_Sizing(Btn_First, this.Size, 0.25, 0.15);
                    ctrlLayout.Control_Positioning(Btn_First, this.Size, 0.2, 0.815, ControlLayout.HorizontalSiding.Center);

                    ctrlLayout.Control_Sizing(Btn_Second, this.Size, 0.25, 0.15);
                    ctrlLayout.Control_Positioning(Btn_Second, this.Size, 0.5, 0.815, ControlLayout.HorizontalSiding.Center);

                    ctrlLayout.Control_Sizing(Btn_Third, this.Size, 0.25, 0.15);
                    ctrlLayout.Control_Positioning(Btn_Third, this.Size, 0.8, 0.815, ControlLayout.HorizontalSiding.Center);
                    break;
            }
        }

        #region ---- Btn_Exit
        private void AssignButton_Exit(Button btn)
        {
            btn.MouseDown += Btn_Exit_MouseDown;
            btn.MouseUp += Btn_Exit_MouseUp;
            btn.Click += Btn_Exit_Click;
            SetBtn_Exit_Image(btn, false);
        }

        private void Btn_Exit_MouseDown(object sender, MouseEventArgs e)
        {
            SetBtn_Exit_Image(sender as Button, true);
        }

        private void Btn_Exit_MouseUp(object sender, MouseEventArgs e)
        {
            SetBtn_Exit_Image(sender as Button, false);
        }

        private void SetBtn_Exit_Image(Control ctrl, bool pressed)
        {
            ControlLayout ctrlLayout = new ControlLayout();
            IniFile ini = new IniFile();
            ini.Load(IniData.SettingIniFile);
            IniSection resSect = ini["Resources"];
            string overallResPath = System.IO.Directory.GetCurrentDirectory() + $@"{resSect["ResourceFolder"]}{resSect["OverallFolder"]}";

            if (pressed)
                ctrlLayout.SetBackgroundImage(ctrl, overallResPath + @"\btn_exit_.gif");
            else
                ctrlLayout.SetBackgroundImage(ctrl, overallResPath + @"\btn_exit.gif");
        }

        private void Btn_Exit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        #endregion

        #region ---- Btn_OK
        private void AssignButton_OK(Button btn)
        {
            btn.MouseDown += Btn_OK_MouseDown;
            btn.MouseUp += Btn_OK_MouseUp;
            btn.Click += Btn_OK_Click;
            SetBtn_OK_Image(btn, false);
        }

        private void Btn_OK_MouseDown(object sender, MouseEventArgs e)
        {
            SetBtn_OK_Image(sender as Button, true);
        }

        private void Btn_OK_MouseUp(object sender, MouseEventArgs e)
        {
            SetBtn_OK_Image(sender as Button, false);
        }

        private void SetBtn_OK_Image(Control ctrl, bool pressed)
        {
            ControlLayout ctrlLayout = new ControlLayout();
            IniFile ini = new IniFile();
            ini.Load(IniData.SettingIniFile);
            IniSection resSect = ini["Resources"];
            string overallResPath = System.IO.Directory.GetCurrentDirectory() + $@"{resSect["ResourceFolder"]}{resSect["OverallFolder"]}";

            if (pressed)
                ctrlLayout.SetBackgroundImage(ctrl, overallResPath + @"\btn_ok_.gif");
            else
                ctrlLayout.SetBackgroundImage(ctrl, overallResPath + @"\btn_ok.gif");
        }

        private void Btn_OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        #endregion
        #region ---- Btn_Cancel
        private void AssignButton_Cancel(Button btn)
        {
            btn.MouseDown += Btn_Cancel_MouseDown;
            btn.MouseUp += Btn_Cancel_MouseUp;
            btn.Click += Btn_Exit_Click;
            SetBtn_Cancel_Image(btn, false);
        }

        private void Btn_Cancel_MouseDown(object sender, MouseEventArgs e)
        {
            SetBtn_Cancel_Image(sender as Button, true);
        }

        private void Btn_Cancel_MouseUp(object sender, MouseEventArgs e)
        {
            SetBtn_Cancel_Image(sender as Button, false);
        }

        private void SetBtn_Cancel_Image(Control ctrl, bool pressed)
        {
            ControlLayout ctrlLayout = new ControlLayout();
            IniFile ini = new IniFile();
            ini.Load(IniData.SettingIniFile);
            IniSection resSect = ini["Resources"];
            string overallResPath = System.IO.Directory.GetCurrentDirectory() + $@"{resSect["ResourceFolder"]}{resSect["OverallFolder"]}";

            if (pressed)
                ctrlLayout.SetBackgroundImage(ctrl, overallResPath + @"\btn_cancel_.gif");
            else
                ctrlLayout.SetBackgroundImage(ctrl, overallResPath + @"\btn_cancel.gif");
        }

        // Click 이벤트 함수는 Btn_Exit_Click을 사용한다
        #endregion

        #region ---- Btn_Yes (할당할 이미지의 지정이 필요)
        private void AssignButton_Yes(Button btn)
        {
            btn.MouseDown += Btn_Yes_MouseDown;
            btn.MouseUp += Btn_Yes_MouseUp;
            btn.Click += Btn_Yes_Click;
            SetBtn_Yes_Image(btn, false);
        }

        private void Btn_Yes_MouseDown(object sender, MouseEventArgs e)
        {
            SetBtn_Yes_Image(sender as Button, true);
        }

        private void Btn_Yes_MouseUp(object sender, MouseEventArgs e)
        {
            SetBtn_Yes_Image(sender as Button, false);
        }

        private void SetBtn_Yes_Image(Control ctrl, bool pressed)
        {
            ControlLayout ctrlLayout = new ControlLayout();
            IniFile ini = new IniFile();
            ini.Load(IniData.SettingIniFile);
            IniSection resSect = ini["Resources"];
            string overallResPath = System.IO.Directory.GetCurrentDirectory() + $@"{resSect["ResourceFolder"]}{resSect["OverallFolder"]}";

            if (pressed)
                ctrlLayout.SetBackgroundImage(ctrl, overallResPath + @"\btn_ok_.gif");
            else
                ctrlLayout.SetBackgroundImage(ctrl, overallResPath + @"\btn_ok.gif");
        }

        private void Btn_Yes_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }
        #endregion
        #region ---- Btn_No (할당할 이미지의 지정이 필요)
        private void AssignButton_No(Button btn)
        {
            btn.MouseDown += Btn_No_MouseDown;
            btn.MouseUp += Btn_No_MouseUp;
            btn.Click += Btn_No_Click;
            SetBtn_No_Image(btn, false);
        }

        private void Btn_No_MouseDown(object sender, MouseEventArgs e)
        {
            SetBtn_No_Image(sender as Button, true);
        }

        private void Btn_No_MouseUp(object sender, MouseEventArgs e)
        {
            SetBtn_No_Image(sender as Button, false);
        }

        private void SetBtn_No_Image(Control ctrl, bool pressed)
        {
            ControlLayout ctrlLayout = new ControlLayout();
            IniFile ini = new IniFile();
            ini.Load(IniData.SettingIniFile);
            IniSection resSect = ini["Resources"];
            string overallResPath = System.IO.Directory.GetCurrentDirectory() + $@"{resSect["ResourceFolder"]}{resSect["OverallFolder"]}";

            if (pressed)
                ctrlLayout.SetBackgroundImage(ctrl, overallResPath + @"\btn_cancel_.gif");
            else
                ctrlLayout.SetBackgroundImage(ctrl, overallResPath + @"\btn_cancel.gif");
        }

        private void Btn_No_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
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
    }
}