using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Miniproject_Hamburger
{
    public partial class HamMakerForm : Form
    {
        static List<HamOrder> remainList = new List<HamOrder>();
        static List<int> completeList = new List<int>();
        static int[] Hmenuid = new int[3];    
        static int[] Hprogressvalue = new int[3];
        Dictionary<int, string> DHamName = new Dictionary<int, string>();

        string MsgOrderID = "";
        string MsgSaleDate = "";
        string MsgContents = "";


        int remainCount = 0;
        int CompleteCount = 0;

        public HamMakerForm()
        {
            InitializeComponent();
        }

        public void HamMakerForm_Load(object sender, EventArgs e)
        {
            remainList = MainForm.OutRemainOrder();      //  현재주문리스트
            completeList = MainForm.OutCompleteOrder();  //  완성주문리스트
            Hmenuid = MainForm.OutIMenuID();
            Hprogressvalue = MainForm.OutMProgressValue();
            
            GetHamName();
            M1progressBar.Maximum = 10000;
            M2progressBar.Maximum = 10000;
            M3progressBar.Maximum = 10000;
            TimAdmin.Start();
            DrawOrderCompleteList();
        }

        private void GetHamName()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Commons.CONSTRING))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT ID, Name " +
                                      "  FROM dbo.menuTbl ";
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        this.DHamName.Add(int.Parse(reader[0].ToString()), reader[1].ToString());
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show(this, "메이커폼에서 햄버거 이름을 가져오지 못했습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DeleteCompleteBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtCompleteDelete.Text))
                return;

            foreach(Button item in FlpCompletePanel.Controls)
            {
                if(item.Text == TxtCompleteDelete.Text.Replace("번",""))
                {
                    FlpCompletePanel.Controls.Remove(item);
                    break;
                }
            }
            foreach(int item in completeList)
            {
               if(item == int.Parse(TxtCompleteDelete.Text.Replace("번", "")))
                {
                    completeList.Remove(item);
                    break;
                }
            }
            TxtCompleteDelete.Text = "";
        }

        private void BtnCompleteClick(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            TxtCompleteDelete.Text = btn.Text + "번";
        }

        private void BtnOrderClick(object sender, EventArgs e)
        {
            Button btn = sender as Button;
                using (SqlConnection conn = new SqlConnection(Commons.CONSTRING))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT d.ID, m.Name, d.Amount, o.SaleDate " +
                                      "  FROM orderdetail AS d "+
                                      " INNER JOIN orderTbl AS o "+
                                      "    ON d.ID = o.ID "+
                                      " INNER JOIN menuTbl AS m " +
                                      "    ON m.ID = d.MenuID " +
                                      " WHERE d.ID = @ID ";
                    SqlParameter paramID = new SqlParameter("@ID", SqlDbType.Int);
                    paramID.Value = btn.Text;
                    cmd.Parameters.Add(paramID);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        MsgOrderID = reader[0].ToString();
                        MsgContents = MsgContents + reader[1].ToString() + " : " + reader[2].ToString() + "개\n";
                        MsgSaleDate = reader[3].ToString();
                    }
                    MsgContents = "주문번호 : " + MsgOrderID + "번\n\n"+ MsgContents + "\n";
                    MsgContents = MsgContents + MsgSaleDate.Replace("오전 12:00:00", "");
                    MessageBox.Show(this, MsgContents, "주문번호 : " + MsgOrderID, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MsgContents = "";
                }
        }
        private void DrawOrderCompleteList()
        {
            FlpOrderPanel.Controls.Clear();
            FlpCompletePanel.Controls.Clear();

            for (int i = 0; i < remainList.Count; i++)
            {
                if (remainList.Count == 0) break;
                Button TxtOrder = new Button();
                TxtOrder.Click += new EventHandler(BtnOrderClick);
                TxtOrder.Font = new Font("맑은고딕", 13, FontStyle.Bold);
                TxtOrder.Width = (int)(TxtOrder.Width * 0.6);
                TxtOrder.Height = (int)(TxtOrder.Height * 1.3);
                TxtOrder.BackColor = Color.White;
                TxtOrder.FlatStyle = FlatStyle.Flat;
                TxtOrder.Text = remainList[i].OrderNumCheck().ToString();

                FlpOrderPanel.Controls.Add(TxtOrder);
            }

            for (int i = 0; i < completeList.Count; i++)
            {
                if (completeList.Count == 0) break;
                Button TxtComplete = new Button();
                TxtComplete.Click += new EventHandler(BtnCompleteClick);
                TxtComplete.Font = new Font("맑은고딕", 13, FontStyle.Bold);
                TxtComplete.Width = (int)(TxtComplete.Width * 0.6);
                TxtComplete.Height = (int)(TxtComplete.Height * 1.3);
                TxtComplete.BackColor = Color.White;
                TxtComplete.FlatStyle = FlatStyle.Flat;
                TxtComplete.Text = completeList[i].ToString();
                FlpCompletePanel.Controls.Add(TxtComplete);
                TxtComplete.Anchor = AnchorStyles.None;
            }

        }
        private void AdminTick(object sender, EventArgs e)
        {
            if (remainCount != remainList.Count() || CompleteCount != completeList.Count())
            {
                DrawOrderCompleteList();
                remainCount = remainList.Count();
                CompleteCount = completeList.Count();
            }


            if (0 < MainForm.OutIOrderNum(0))
            {
                TxtOrderNum1.Text = MainForm.OutIOrderNum(0).ToString();
                ImgHam1.Image = HamImageList.Images[Hmenuid[0]];
                TxtM1.Text = DHamName[Hmenuid[0]];
                M1progressBar.Visible = true;
                M1progressBar.Value = Hprogressvalue[0];
            }
            else
            {
                TxtOrderNum1.Text = "";
                ImgHam1.Image = HamImageList.Images[0];
                TxtM1.Text = "";
                M1progressBar.Value = 0;
                M1progressBar.Visible = false;
            }

            if (0 < MainForm.OutIOrderNum(1))
            {
                TxtOrderNum2.Text = MainForm.OutIOrderNum(1).ToString();
                ImgHam2.Image = HamImageList.Images[Hmenuid[1]];
                TxtM2.Text = DHamName[Hmenuid[1]];
                M2progressBar.Visible = true;
                M2progressBar.Value = Hprogressvalue[1];
            }
            else
            {
                TxtOrderNum2.Text = "";
                ImgHam2.Image = HamImageList.Images[0];
                TxtM2.Text = "";
                M2progressBar.Value = 0;
                M2progressBar.Visible = false;
            }

            if (0 < MainForm.OutIOrderNum(2))
            {
                TxtOrderNum3.Text = MainForm.OutIOrderNum(2).ToString();
                ImgHam3.Image = HamImageList.Images[Hmenuid[2]];
                TxtM3.Text = DHamName[Hmenuid[2]];
                M3progressBar.Visible = true;
                M3progressBar.Value = Hprogressvalue[2];
            }
            else
            {
                TxtOrderNum3.Text = "";
                ImgHam3.Image = HamImageList.Images[0];
                TxtM3.Text = "";
                M3progressBar.Value = 0;
                M3progressBar.Visible = false;
            }
            if (MainForm.OutMCheck(0) == false) TxtM1.Text = "";
            if (MainForm.OutMCheck(1) == false) TxtM2.Text = "";
            if (MainForm.OutMCheck(2) == false) TxtM3.Text = "";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
