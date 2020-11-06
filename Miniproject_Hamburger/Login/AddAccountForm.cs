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

namespace Miniproject_Hamburger
{
    public partial class AddAccountForm : Form
    {
        SqlConnection connection = new SqlConnection(Commons.CONSTRING);

        public AddAccountForm()
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
            
            using (SqlConnection connection = new SqlConnection(Commons.CONSTRING))
            {
                connection.Open();
                SqlCommand command = new SqlCommand()
                {
                    Connection = connection,
                    CommandText = "INSERT INTO managerTbl Values (@ID, @Password, @Mobile, @Addr, @Name)"
                };
                SqlParameter paramID = new SqlParameter("@ID", SqlDbType.VarChar, 40);
                paramID.Value = TextBox_ID.Text;
                SqlParameter paramPW = new SqlParameter("@Password", SqlDbType.VarChar, 40);
                paramPW.Value = TextBox_Password.Text;
                SqlParameter paramMB = new SqlParameter("@Mobile", SqlDbType.Char, 11);
                paramMB.Value = TextBox_Mobile.Text;
                SqlParameter paramAddr = new SqlParameter("@Addr", SqlDbType.NVarChar, 70);
                paramAddr.Value = TextBox_Add.Text;
                SqlParameter paramName = new SqlParameter("@Name", SqlDbType.NVarChar, 10);
                paramName.Value = TextBox_Name.Text;

                command.Parameters.Add(paramID);
                command.Parameters.Add(paramPW);
                command.Parameters.Add(paramName);
                command.Parameters.Add(paramAddr);
                command.Parameters.Add(paramMB);

                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("가입이 완료되었습니다");
                this.Close();
            }
        }

        private bool CheckInfo()
        {
            if (string.IsNullOrEmpty(TextBox_Name.Text) ||
                string.IsNullOrEmpty(TextBox_Add.Text) ||
                string.IsNullOrEmpty(TextBox_Mobile.Text) ||
                string.IsNullOrEmpty(TextBox_Password.Text) ||
                string.IsNullOrEmpty(TextBox_ID.Text))
            {
                return false;
            }
            else
                return true;
        }

        private void Button_DeleteAcc_Click(object sender, EventArgs e)
        {
            DeleteAccountForm deleteForm1 = new DeleteAccountForm();
            deleteForm1.ShowDialog();

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

        private void TextBox_Mobile_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                button1_Click(sender, e);
        }
    }
}




