using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartFactoryProject_Final.Common
{
    public partial class FRM_NumPadUI : Form
    {
        const int MaxTextLength = 16;

        public FRM_NumPadUI(string inputType = "", string initialText = "", bool inputPassword = false)
        {
            InitializeComponent();
            Lbl_Type.Text = inputType;
            Txt_Input.Text = initialText;
            if (inputPassword)
                Txt_Input.PasswordChar = '●';
        }
        
        private void Frm_NumPadUI_Load(object sender, EventArgs e)
        {
            GetKeyImages();

            List<Button> btnList = new List<Button>();
            btnList.Add(Btn_Key_1);
            btnList.Add(Btn_Key_2);
            btnList.Add(Btn_Key_3);
            btnList.Add(Btn_Key_4);
            btnList.Add(Btn_Key_5);
            btnList.Add(Btn_Key_6);
            btnList.Add(Btn_Key_7);
            btnList.Add(Btn_Key_8);
            btnList.Add(Btn_Key_9);
            btnList.Add(Btn_Key_0);
            btnList.Add(Btn_Key_BackSpace);
            btnList.Add(Btn_Key_Enter);

            ControlLayout ctrlLayout = new ControlLayout();
            foreach(Button btn in btnList)
            {
                btn.Text = "";
                ctrlLayout.MakeCurvedBorder(btn, 18, 18);
            }
            Btn_Exit.Text = "";
            ctrlLayout.MakeCurvedBorder(Btn_Exit, 8, 8);
        }

        private void GetKeyImages()
        {
            ControlLayout ctrlLayout = new ControlLayout();
            IniFile ini = new IniFile();
            ini.Load(IniData.SettingIniFile);
            IniSection resSect = ini["Resources"];
            string keyPadResPath = System.IO.Directory.GetCurrentDirectory() + $@"{resSect["ResourceFolder"]}{resSect["KeyPadFolder"]}";

            ctrlLayout.SetBackgroundImage(Btn_Key_1, keyPadResPath + @"\n1.png");
            ctrlLayout.SetBackgroundImage(Btn_Key_2, keyPadResPath + @"\n2.png");
            ctrlLayout.SetBackgroundImage(Btn_Key_3, keyPadResPath + @"\n3.png");
            ctrlLayout.SetBackgroundImage(Btn_Key_4, keyPadResPath + @"\n4.png");
            ctrlLayout.SetBackgroundImage(Btn_Key_5, keyPadResPath + @"\n5.png");
            ctrlLayout.SetBackgroundImage(Btn_Key_6, keyPadResPath + @"\n6.png");
            ctrlLayout.SetBackgroundImage(Btn_Key_7, keyPadResPath + @"\n7.png");
            ctrlLayout.SetBackgroundImage(Btn_Key_8, keyPadResPath + @"\n8.png");
            ctrlLayout.SetBackgroundImage(Btn_Key_9, keyPadResPath + @"\n9.png");
            ctrlLayout.SetBackgroundImage(Btn_Key_0, keyPadResPath + @"\n0.png");

            ctrlLayout.SetBackgroundImage(this, keyPadResPath + @"\bg_num.png");
            ctrlLayout.SetBackgroundImage(Btn_Key_BackSpace, keyPadResPath + @"\n_arrow.png");
            ctrlLayout.SetBackgroundImage(Btn_Key_Enter, keyPadResPath + @"\n_enter.png");
            ctrlLayout.SetBackgroundImage(Btn_Exit, keyPadResPath + @"\n_exit.png");
        }

        private void Btn_Key_Click(object sender, EventArgs e)
        {
            if (Txt_Input.Text.Length < MaxTextLength)
            {
                Button btnClicked = sender as Button;
                Txt_Input.Text += (string)btnClicked.Tag;
            }
        }

        private void Btn_Key_BackSpace_Click(object sender, EventArgs e)
        {
            if (Txt_Input.Text.Length > 0)
                Txt_Input.Text = Txt_Input.Text.Substring(0, Txt_Input.Text.Length - 1);
        }

        private void Btn_Key_Enter_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Btn_Key_EXIT_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public string GetInputText()
        {
            return Txt_Input.Text;
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
    }
}