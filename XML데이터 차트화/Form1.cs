using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Windows.Forms.DataVisualization.Charting;

namespace XML데이터_차트화
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<string> TimeList = new List<string> { };
        List<string> TempList = new List<string> { };

        private void Form1_Load(object sender, EventArgs e)
        {
            XElement aXElement = XElement.Load("http://www.kma.go.kr/wid/queryDFSRSS.jsp?zone=2623052000");
            var XMLData = from Temp in aXElement.Descendants("data")
                          select new XML()
                          {
                              Hour = Temp.Element("hour").Value,
                              Temp = Temp.Element("temp").Value,
                              Day = Temp.Element("day").Value
                          };
            
            foreach (var Temp in XMLData)
            {
                TimeList.Add($"day{Temp.Day} {Temp.Hour}시");
                TempList.Add(Temp.Temp);
            }

            chart1.Series[0].Points.Clear();
            chart1.Series[0].ChartType = SeriesChartType.Spline;
            for (int i = 0; i < TimeList.Count-1; i++)
            {
                chart1.Series[0].Points.AddXY(TimeList[i], (int)double.Parse(TempList[i]));
            }
        }
    }
}
