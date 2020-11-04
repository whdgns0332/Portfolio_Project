using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace 차트_기준선_구현
{
    public partial class Form1 : Form
    {
        int[] Values = new int[100];
        int pointCount = 0;
        int pointSum = 0;
        int defectCount = 0;
        const int maxMargin = 8;
        const int minMargin = 2;

        public Form1()
        {
            InitializeComponent();
            chart1.ChartAreas[0].AxisY.Maximum = 10;
            chart1.Series[0].ChartType = SeriesChartType.Line;
            timer1.Interval = 500;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StripLine strip1 = new StripLine();
            strip1.BackColor = Color.FromArgb(64, Color.Red);
            strip1.StripWidth = 2;
            strip1.IntervalOffset = maxMargin;
            chart1.ChartAreas[0].AxisY.StripLines.Add(strip1);

            StripLine strip2 = new StripLine();
            strip2.BackColor = Color.FromArgb(64, Color.Blue);
            strip2.StripWidth = 2;
            strip2.IntervalOffset = 0;
            chart1.ChartAreas[0].AxisY.StripLines.Add(strip2);

            chart1.Series[0].Points.DataBindY(Values);
            chart1.Series[0].Color = Color.Green;

            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Add(ValueUpdate());
            chart1.Series[0].Points.RemoveAt(0);
        }

        private float ValueUpdate() // 갱신값
        {
            Random random = new Random();
            int newValue = random.Next(0, 11);

            pointCount++;
            pointSum += newValue;
            if (newValue > maxMargin || newValue < minMargin)
                defectCount++;
            Txt_avg.Text = Math.Round(((double)pointSum / pointCount), 2).ToString();
            Txt_defectRatio.Text = Math.Round(((double)defectCount / pointCount), 2).ToString();

            return newValue;
        }
    }
}
