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
    public partial class Form1 : Form
    {
        Dictionary<object, string> keyValuePairs = new Dictionary<object, string>();

        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;

            keyValuePairs.Add(0, "빨강");
            keyValuePairs.Add(1, "노랑");
            keyValuePairs.Add(2, "초록");
            keyValuePairs.Add(3, "파랑");

            textBox1.Text = "선택지 패드 호출";
            textBox2.Text = "숫자 패드 호출";
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            SelectPad select = new SelectPad(keyValuePairs);
            select.ShowDialog();
            if (select.DialogResult == DialogResult.OK)
            {
                textBox1.Text = select.seletedPair.Value;
                textBox1.Tag = select.seletedPair.Key;
            }
            else
            {
                textBox1.Text = "선택지 패드 호출";
                textBox1.Tag = null;
            }
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            NumPad num = new NumPad();
            num.ShowDialog();
            if (num.DialogResult == DialogResult.OK)
            {
                if (num.inputNum.ToString() == "")
                {
                    textBox2.Text = "숫자 패드 호출";
                    return;
                }

                textBox2.Text = num.inputNum.ToString();
            }
            else
            {
                textBox2.Text = "숫자 패드 호출";
            }
        }
    }
}
