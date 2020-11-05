using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 프로젝트___선택지_패드
{
    public partial class SelectPad : Form
    {
        int buttonWidth = 200;
        int buttonHeight = 40;
        int buttonGap = 10;
        int marginX = 20;
        int marginY = 20;
        Dictionary<object, string> buttonList;
        public KeyValuePair<object, string> seletedPair { get; set; }

        public SelectPad(Dictionary<object, string> keyValuePairs)
        {
            InitializeComponent();
            buttonList = keyValuePairs;
            FormLayout();
            DialogResult = DialogResult.Cancel;
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void FormLayout()
        {
            int count = 0;
            foreach (KeyValuePair<object, string> item in buttonList)
            {
                Button button = new Button();
                button.Text = item.Value;
                button.Tag = item.Key;
                button.Width = buttonWidth;
                button.Height = buttonHeight;
                button.Location = new Point(marginX, marginY + (buttonHeight + buttonGap) * count);
                button.ClientSize = new Size(buttonWidth, buttonHeight);

                Controls.Add(button);
                button.Click += new EventHandler(this.buttonClick);

                count++;
            }

            this.ClientSize = new Size(2 * marginX + buttonWidth, 2 * marginY + count * (buttonHeight + buttonGap) - buttonGap);
            this.CenterToScreen();
        }

        private void buttonClick(object sender, EventArgs e)
        {
            Button button = sender as Button;
            seletedPair = new KeyValuePair<object, string>(button.Tag, button.Text);
            DialogResult = DialogResult.OK;

            Close();
        }
    }
}
