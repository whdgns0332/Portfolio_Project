using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Miniproject_Hamburger;

namespace MiniProject_Hamburger
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            this.CenterToScreen();   // 팝업 위치 조정
            InitializeComponent();
        }

        private void Login_text_Click(object sender, EventArgs e)
        {
            if (!CheckInfo())
            {
                MessageBox.Show("아이디 또는 패스워드를 모두 입력해주세요!.", "[알림]", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            SqlConnection con = new SqlConnection(Commons.CONSTRING);
            con.Open();
            SqlCommand command = new SqlCommand
            {
                Connection = con,
                CommandText = "Select * from managerTbl where ID = @ID and Password = @Password"
            };
            SqlParameter paramID = new SqlParameter("@ID", SqlDbType.VarChar, 40);
            paramID.Value = TextBox_ID.Text;
            SqlParameter paramPW = new SqlParameter("@Password", SqlDbType.VarChar, 40);
            paramPW.Value = TextBox_PW.Text;

            command.Parameters.Add(paramID);
            command.Parameters.Add(paramPW);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                MessageBox.Show("로그인에 성공했습니다");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("관리자 아이디와 비밀번호를 확인해 주세요!.");
            }
        }

        private bool CheckInfo()
        {
            if (string.IsNullOrEmpty(TextBox_ID.Text) ||                    //아이디 입력란 공백일경우 메시지출력
                string.IsNullOrEmpty(TextBox_PW.Text))                      //비밀번호 입력란 공백일경우 메시지출력
            {
                return false;
            }
            return true;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            TextBox_ID.Focus();
        }

        private void TextBox_PW_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                Login_text_Click(sender, new EventArgs());
            }
        }
    }
}


   
