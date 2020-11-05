using NumberPad;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NumberPad
{
    public partial class NumPad : Form
    {
        StringBuilder sb = new StringBuilder();
        Button[] btn = new Button[10];
        Main form1;

        public NumPad(Main form)
        {
            InitializeComponent();
            form1 = form;
            ClientSize = new Size(340, 490);
            label1.Location = new Point(10, 450);
        }

        private void NumPad_Load(object sender, EventArgs e)
        {
            // 0~9까지 버튼 생성
            for (int i = 0; i < 10; i++)
            {
                btn[i] = new Button();
                btn[i].Location = new Point(10 + 110 * (i % 3), 10 + 110 * (i / 3));
                btn[i].Name = "button" + (i + 1).ToString();
                btn[i].Size = new Size(100, 100);
                btn[i].TabIndex = i;
                btn[i].Text = (i + 1).ToString();
                btn[i].UseVisualStyleBackColor = true;
                btn[i].Click += new EventHandler(button_Click);
                Controls.Add(btn[i]);
            }
            btn[9].Name = "button0";
            btn[9].Location = new Point(120, 340);
            btn[9].Text = "0";

            // backspace 버튼 생성
            Button Btn_bs = new Button();
            Btn_bs.Location = new Point(10, 340);
            Btn_bs.Name = "back_space";
            Btn_bs.Size = new Size(100, 100);
            Btn_bs.Text = "←";
            Btn_bs.UseVisualStyleBackColor = true;
            Btn_bs.Click += new EventHandler(button_Click_bs);
            Controls.Add(Btn_bs);

            // ok 버튼 생성
            Button Btn_ok = new Button();
            Btn_ok.Location = new Point(230, 340);
            Btn_ok.Name = "OK";
            Btn_ok.Size = new Size(100, 100);
            Btn_ok.Text = "OK";
            Btn_ok.UseVisualStyleBackColor = true;
            Btn_ok.Click += new EventHandler(button_Click_ok);
            Controls.Add(Btn_ok);
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            sb.Append(btn.Text);
            label1.Text = sb.ToString();
        }

        private void button_Click_bs(object sender, EventArgs e)
        {
            sb.Remove(sb.Length - 1, 1);
            label1.Text = sb.ToString();
        }

        private void button_Click_ok(object sender, EventArgs e)
        {
            form1.WriteTextBox(label1.Text);
            Close();
        }
    }
}
