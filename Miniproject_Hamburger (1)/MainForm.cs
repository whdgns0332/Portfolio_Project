using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MiniProject_Hamburger;


/*
* 
*ID 1 빅맥       Bigmac        5000원   60초
*ID 2 불고기버거 Bulgogiburger 3500원   40초
*ID 3 치즈버거   Cheeseburger  4000원   50초
*ID 4 맥모닝     Macmorning    2500원   30초
*
*/

namespace Miniproject_Hamburger
{
    public partial class MainForm : Form
    {
        Dictionary<int, int> prdTime;           
        static List<HamOrder> remainOrder = new List<HamOrder>(); 
        static List<int> completeOrder = new List<int>();
        static bool[] MCheck = new bool[3] { false, false, false };
        static int[] Mprogressvalue = new int[3] { 0, 0, 0 };
        static int[] Imenuid = new int[3] { 0, 0, 0 };
        static int[] Iordernum = new int[3] { 0, 0, 0 };
        int[] Iprdtime = new int[3] { 0, 0, 0 };
        Queue<int> completeburgerID = new Queue<int>();      
        HamOrder[] currentOrder = new HamOrder[3];
        OrderForm orderform = new OrderForm();
        HamMakerForm makerform = new HamMakerForm();
        Form1 saleform = new Form1();

        public MainForm()
        {
            InitializeComponent();
        }

        // 햄버거메이커폼으로 던져줄 변수들
        public static bool OutMCheck(int MNumber)
        {
            return MCheck[MNumber];
        }
        public static List<HamOrder> OutRemainOrder()
        {
            return remainOrder;
        }
        public static List<int> OutCompleteOrder()
        {
            return completeOrder;
        }
        public static int[] OutIMenuID()
        {
            return Imenuid;
        }
        public static int OutIOrderNum(int Mnumber)
        {
            return Iordernum[Mnumber];
        }
        public static int[] OutMProgressValue()
        {
            return Mprogressvalue;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            if(login.ShowDialog() != DialogResult.OK)
            {
                Environment.Exit(0);
            }

            GetHamTime();   //   메뉴별 생산시간값 가져옴
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.WindowState = FormWindowState.Maximized;

            orderform.TopLevel = false;
            orderform.TopMost = true;
            orderform.Dock = DockStyle.Fill;
            OrderPage.Controls.Add(orderform);
            orderform.Show();

            HamMachine1.Interval = 100;
            HamMachine1.Start();
        }

        private void GetHamTime()   //   햄버거 조리시간을 prd.TIme(메뉴ID, 조리시간) 으로 저장
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Commons.CONSTRING))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT ID, ProductionTime " +
                                      "  FROM dbo.menuTbl ";
                    SqlDataReader reader = cmd.ExecuteReader();
                    this.prdTime = new Dictionary<int, int>();

                    while (reader.Read())
                    {
                        this.prdTime.Add(int.Parse(reader[0].ToString()), int.Parse(reader[1].ToString()));
                    }
                }
            }
            catch(Exception)
            {
                MessageBox.Show(this, "햄버거 조리시간을 가져오지 못했습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public static void newOrder(int orderNum)  // 다른폼에서 주문들어왔을때 주문명세서생성
        {
            HamOrder newHam = new HamOrder(orderNum);
            remainOrder.Add(newHam);
        }


        private void HamMachine1_Tick(object sender, EventArgs e)   //  햄버거기계 틱
        {
            if (remainOrder.Count == 0 && MCheck[0] == false && MCheck[1] == false && MCheck[2] == false)
            {
                Debug.WriteLine("");
            }
            else
            {
                //만들꺼 기계에 전달하는 부분
                for (int iNum = 0; iNum < 3; iNum++)
                {
                    if (MCheck[iNum] == false)
                    {

                        for (int t = 0; t < remainOrder.Count(); t++)   //  주문서에서 만들거 찾음
                        {
                            if (remainOrder[t].orderList.Count > 0)
                            {
                                currentOrder[iNum] = remainOrder.ElementAt(t);
                                Imenuid[iNum] = currentOrder[iNum].orderList.ElementAt(0);

                                currentOrder[iNum].orderList.RemoveAt(0);
                                currentOrder[iNum].TotalMinus(); // 명세서에 남은 버거갯수 -1
                                Iprdtime[iNum] = prdTime[Imenuid[iNum]];
                                Iprdtime[iNum] = (int)((10000 / Iprdtime[iNum] * HamMachine1.Interval) * (0.001));
                                currentOrder[iNum].MachineOn(iNum);
                                Iordernum[iNum] = currentOrder[iNum].OrderNumCheck();
                                MCheck[iNum] = true;
                                break;
                            }
                        }
                    }
                }

                //기계들 가동하는 부분
                for (int iNum = 0; iNum < 3; iNum++)
                {
                    if (10000 <= Mprogressvalue[iNum] + Iprdtime[iNum])
                    {
                        Mprogressvalue[iNum] = 10000;
                        currentOrder[iNum].MachineOff(iNum);    //   메뉴에서 만드는 기계 뻄
                        completeburgerID.Enqueue(Imenuid[iNum]);
                        Mprogressvalue[iNum] = 0;
                        Iordernum[iNum] = 0;
                        MCheck[iNum] = false;
                        if (currentOrder[iNum].MachineCheckingOn() == true && currentOrder[iNum].OrderEndCheck() == true)
                        {
                            for (int r = 0; r < remainOrder.Count; r++)
                            {
                                if (currentOrder[iNum].OrderNumCheck() == remainOrder[r].OrderNumCheck())
                                {
                                    completeOrder.Add(remainOrder[r].OrderNumCheck());
                                    remainOrder.RemoveAt(r);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (MCheck[iNum] == true)
                            Mprogressvalue[iNum] = Mprogressvalue[iNum] + Iprdtime[iNum];
                    }
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabControl1.SelectedTab == tabControl1.TabPages[1])  //  현황페이지 켜줌
            {
                makerform.TopLevel = false;
                makerform.TopMost = true;
                makerform.Dock = DockStyle.Fill;
                MakerPage.Controls.Add(makerform);
                makerform.Show();
            }
            if(tabControl1.SelectedTab == tabControl1.TabPages[2])  //  판매량페이지 켜줌
            {
                saleform.TopLevel = false;
                saleform.TopMost = true;
                saleform.Dock = DockStyle.Fill;
                SaleDataPage.Controls.Add(saleform);
                saleform.Show();
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void BtnMinimized_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
