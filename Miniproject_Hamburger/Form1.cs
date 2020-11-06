using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows.Forms.DataVisualization;
using System.Data.SqlTypes;
using System.ComponentModel.Design.Serialization;
using System.Security.Cryptography.X509Certificates;

namespace Miniproject_Hamburger
{
    public partial class Form1 : Form
    {
        private const bool V = true;
        //Dictionary<int, int> saledata = new Dictionary<int, int>();
        //List<int> month = new List<int>();

        public Form1()
        {
            InitializeComponent();

            comboBox1.Items.Add("1월");
            comboBox1.Items.Add("2월");
            comboBox1.Items.Add("3월");
            comboBox1.Items.Add("4월");
            comboBox1.Items.Add("5월");
            comboBox1.Items.Add("6월");
            comboBox1.Items.Add("7월");
            comboBox1.Items.Add("8월");
            comboBox1.Items.Add("9월");
            comboBox1.Items.Add("10월");
            comboBox1.Items.Add("11월");
            comboBox1.Items.Add("12월");
        }
        private void Form1_VisibleChanged(object sender, EventArgs e)
        {
            BtnSaleData_Click(sender, e);

            comboBox1.SelectedIndex = DateTime.Now.Month - 1;
        }

        private void BtnStock_Click(object sender, EventArgs e)
        {
            string sql = "SELECT Name, Amount " +
                         "  FROM ingredientsTbl ";    

            SqlConnection con = new SqlConnection(Commons.CONSTRING);
            SqlDataAdapter sda = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            chart1.DataSource = dt;
            chart1.Series[0].XValueMember = "Name";
            chart1.Series[0].YValueMembers = "Amount";
            chart1.Series[0].Name = "재고량";
            chart1.Series[0].Color = Color.Black;

            chart1.DataBind();
        }

        private void BtnSaleData_Click(object sender, EventArgs e)
        {
            using (SqlConnection Conn = new SqlConnection(Commons.CONSTRING))
            {
                Conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Conn;

                string strQuery = "SELECT DATEPART(mm, ORDERS.SaleDate) AS date , SUM(DETAIL.Amount) AS SUM " +
                                      "  FROM orderTbl ORDERS " +
                                      " INNER JOIN orderdetail DETAIL " +
                                      "    ON ORDERS.ID = DETAIL.ID " +
                                      " GROUP BY DATEPART(mm, ORDERS.SaleDate)";

                SqlConnection conn = new SqlConnection(Commons.CONSTRING);
                SqlDataAdapter ads = new SqlDataAdapter(strQuery, conn);
                DataTable dt = new DataTable();
                ads.Fill(dt);
                chart1.DataSource = dt;
                chart1.Series[0].XValueMember = "date";
                chart1.Series[0].YValueMembers = "SUM";
                chart1.Series[0].Name = "총 판매량";
                chart1.Series[0].Color = Color.Aquamarine;

                chart1.DataBind();
                comboBox1.Visible = true;
            }
        }

        private void BtnSaleMoney_Click(object sender, EventArgs e)
        {
            string sql = "SELECT SUM(DETAIL.Amount) AS sum, menuTbl.Name AS name " +
                         "  FROM orderTbl ORDERS " +
                         " INNER JOIN orderdetail DETAIL " +
                         "    ON ORDERS.ID = DETAIL.ID " +
                         " RIGHT OUTER JOIN menuTbl " +
                         "    ON DETAIL.MenuID = menuTbl.ID " +
                         " WHERE DATEPART(mm, ORDERS.SaleDate) = @콤보상자 " +
                         " GROUP BY menuTbl.Name";
            if (comboBox1.Text == "")
            {
                MessageBox.Show("표시할 날짜를 먼저 선택해주세요.");
                return;
            }

            SqlConnection con = new SqlConnection(Commons.CONSTRING);
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);

            SqlParameter parmCbo = new SqlParameter("@콤보상자", SqlDbType.Int);
            parmCbo.Value = comboBox1.Text.Replace("월", "");
            cmd.Parameters.Add(parmCbo);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            chart1.DataSource = dt;
            chart1.Series[0].XValueMember = "name";
            chart1.Series[0].YValueMembers = "sum";
            chart1.Series[0].Name = "월별 버거판매량";

            chart1.Series[0].Color = Color.Gold;
            chart1.DataBind();
        }

        private void BtnIngreConsume_Click(object sender, EventArgs e)
        {
            string sql = "SELECT sum(recipeTbl.Consume * DETAIL.Amount) AS sum, ingredientsTbl.Name AS name" +
                         "  FROM orderTbl ORDERS " +
                         " INNER JOIN orderdetail DETAIL " +
                         "    ON ORDERS.ID = DETAIL.ID " +
                         " RIGHT OUTER JOIN recipeTbl " +
                         "    ON DETAIL.MenuID = recipeTbl.MenuID " +
                         "   LEFT OUTER JOIN ingredientsTbl " +
                         "     ON recipeTbl.IngredientsID = ingredientsTbl.ID " +
                         " WHERE DATEPART(mm, ORDERS.SaleDate) = @콤보상자 " +
                         " GROUP BY ingredientsTbl.Name";   

            if (comboBox1.Text == "")
            {
                MessageBox.Show("표시할 날짜를 먼저 선택해주세요.");
                return;
            }

            SqlConnection con = new SqlConnection(Commons.CONSTRING);
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);

            SqlParameter parmCbo = new SqlParameter("@콤보상자", SqlDbType.Int);
            parmCbo.Value = comboBox1.Text.Replace("월", "");
            cmd.Parameters.Add(parmCbo);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            chart1.DataSource = dt;
            chart1.Series[0].XValueMember = "name";
            chart1.Series[0].YValueMembers = "sum";
            chart1.Series[0].Name = "월별 재료소모량";
            chart1.Series[0].Color = Color.Orange;

            chart1.DataBind();
        }
    }
}



























         