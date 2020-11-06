using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Miniproject_Hamburger
{
    public partial class SearchIDForm : Form
    {
        public SearchIDForm()
        {
            CenterToScreen();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!CheckInfo())
            {
                MessageBox.Show("정보를 모두 입력해주세요!.", "[알림]", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            using(SqlConnection connection = new SqlConnection(Commons.CONSTRING))
            {
                connection.Open();
                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = @"SELECT LEFT(ID, 4) + '●●●' as ID
                                      FROM managerTbl
                                     WHERE Name = @Name
                                       AND Mobile = @Mobile"
                };
                SqlParameter paramName = new SqlParameter("@Name", SqlDbType.NVarChar, 10);
                paramName.Value = TextBox_Name.Text;
                SqlParameter paramMobile = new SqlParameter("@Mobile", SqlDbType.Char, 11);
                paramMobile.Value = TextBox_Mobile.Text;

                command.Parameters.Add(paramName);
                command.Parameters.Add(paramMobile);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    MessageBox.Show($"해당하는 계정의 ID는 {reader[0].ToString()}입니다");
                }
                else
                {
                    MessageBox.Show("해당하는 계정이 존재하지 않습니다");
                }
                reader.Close();
                connection.Close();
            }
        }

        private bool CheckInfo()
        {
            if (string.IsNullOrEmpty(TextBox_Mobile.Text) ||
                string.IsNullOrEmpty(TextBox_Name.Text))
            {
                return false;
            }
            else
                return true;
        }

        int mobileLog = 0;
        private void TextBox_Mobile_TextChanged(object sender, EventArgs e)
        {
            int mobile;
            if (int.TryParse(TextBox_Mobile.Text, out mobile))
            {
                if (mobile > 0)
                    mobileLog = mobile;
                else
                    TextBox_Mobile.Text = mobileLog.ToString();
            }
            else
            {
                if (!string.IsNullOrEmpty(TextBox_Mobile.Text))
                    TextBox_Mobile.Text = mobileLog.ToString();
                else
                    mobileLog = 0;
            }
        }

        private void TextBox_Name_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                button1_Click(sender, e);
        }
    }
}
