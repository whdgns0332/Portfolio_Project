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
    public partial class Main : Form
    {
        private static int FocusedTextBox;

        public Main()
        {
            InitializeComponent();
            textBox1.Focus();
        }

        public void WriteTextBox(string s) // 키패드의 값을 텍스트 박스에 입력
        {
            if (FocusedTextBox == 1)
                textBox1.Text = s;
            else textBox2.Text = s;
        }

        private void textBox1_MouseUp(object sender, MouseEventArgs e)
        {
            textBox1.Text = "";
            FocusedTextBox = 1;
            NumPad k = new NumPad(this);
            k.ShowDialog();
        }

        private void textBox2_MouseUp(object sender, MouseEventArgs e)
        {
            textBox2.Text = "";
            FocusedTextBox = 2;
            NumPad k = new NumPad(this);
            k.ShowDialog();
        }
    }
}
