using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 프로젝트___선택지_패드
{
    public partial class NumPad : Form
    {
        int keyGap = 10;
        int buttonSize = 40;
        int Xmargin = 10;
        int Ymargin = 10;
        public StringBuilder inputNum = new StringBuilder();
        Label aLabel;

        public NumPad()
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;
            ClientSize = new Size(keyGap * 2 + buttonSize * 3 + Xmargin * 2, keyGap * 3 + buttonSize * 4 + Ymargin * 2 + 30);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void NumPad_Load(object sender, EventArgs e)
        {
            Button[] btn = new Button[10];

            // 0~9까지 버튼 생성
            for (int i = 0; i < 10; i++)
            {
                btn[i] = new Button();
                btn[i].Location = new Point(10 + 50 * (i % 3), 10 + 50 * (i / 3));
                btn[i].Name = "button" + (i + 1).ToString();
                btn[i].Size = new Size(40, 40);
                btn[i].TabIndex = i;
                btn[i].Text = (i + 1).ToString();
                btn[i].UseVisualStyleBackColor = true;
                btn[i].Click += new EventHandler(button_Click);
                Controls.Add(btn[i]);
            }
            btn[9].Name = "button0";
            btn[9].Location = new Point(60, 160);
            btn[9].Text = "0";

            // backspace 버튼 생성
            Button Btn_bs = new Button();
            Btn_bs.Location = new Point(10, 160);
            Btn_bs.Name = "back_space";
            Btn_bs.Size = new Size(40, 40);
            Btn_bs.Text = "←";
            Btn_bs.UseVisualStyleBackColor = true;
            Btn_bs.Click += new EventHandler(button_Click_bs);
            Controls.Add(Btn_bs);

            // ok 버튼 생성
            Button Btn_ok = new Button();
            Btn_ok.Location = new Point(110, 160);
            Btn_ok.Name = "OK";
            Btn_ok.Size = new Size(40, 40);
            Btn_ok.Text = "OK";
            Btn_ok.UseVisualStyleBackColor = true;
            Btn_ok.Click += new EventHandler(button_Click_ok);
            Controls.Add(Btn_ok);

            // 입력 숫자 라벨 생성
            aLabel = new Label();
            aLabel.Location = new Point(10, 210);
            aLabel.Font = new Font("굴림", 15);
            Controls.Add(aLabel);
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            inputNum.Append(btn.Text);
            aLabel.Text = inputNum.ToString();
        }

        private void button_Click_bs(object sender, EventArgs e)
        {
            inputNum.Remove(inputNum.Length - 1, 1);
            aLabel.Text = inputNum.ToString();
        }

        private void button_Click_ok(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
            Dispose();
        }
    }
}
