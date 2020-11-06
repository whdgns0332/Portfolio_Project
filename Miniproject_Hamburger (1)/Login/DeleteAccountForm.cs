using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace Miniproject_Hamburger
{
    public partial class DeleteAccountForm : Form
    {
        public DeleteAccountForm()
        {
            CenterToScreen();
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (!CheckInfo())
            {
                MessageBox.Show("삭제계정의 아이디와 패스워드를 모두 입력해주세요!.", "[알림]", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
           

            using (SqlConnection con = new SqlConnection(Commons.CONSTRING))
            {
                con.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = con;

                command.CommandText = "Select * from managerTbl WHERE ID = @ID and Password = @Password";
                SqlParameter paID = new SqlParameter("@ID", SqlDbType.VarChar, 40);
                paID.Value = 계정삭제정보id.Text;
                SqlParameter paPW = new SqlParameter("@Password", SqlDbType.VarChar, 40);
                paPW.Value = 계정삭제정보pw.Text;
                command.Parameters.Add(paID);
                command.Parameters.Add(paPW);
                SqlDataReader reader = command.ExecuteReader();
                command.Parameters.Remove(paPW);

                if (reader.Read())
                {
                    reader.Close();
                    if (MessageBox.Show("정말로 삭제하시겠습니까?", "계정 삭제 확인", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        command.CommandText = "DELETE FROM managerTbl WHERE ID = @ID";
                        command.ExecuteNonQuery();
                        MessageBox.Show("계정이 삭제되었습니다");
                        this.Close();
                    }
                }
            }
        }

        private bool CheckInfo()
        {
            if (string.IsNullOrEmpty(계정삭제정보id.Text) ||
                string.IsNullOrEmpty(계정삭제정보pw.Text))
            {
                return false;
            }
            else
                return true;
        }
    }
}
