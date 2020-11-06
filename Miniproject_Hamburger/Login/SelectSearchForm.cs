using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Miniproject_Hamburger
{
    public partial class SelectSearchForm : Form
    {
        public SelectSearchForm()
        {
            CenterToScreen();
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchIDForm mainform1 = new SearchIDForm();
            mainform1.ShowDialog();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchPasswordForm mainform1 = new SearchPasswordForm();
            mainform1.ShowDialog();
        }

        private void id찾기버튼_Click(object sender, EventArgs e)
        {

            SearchIDForm mainform1 = new SearchIDForm();
            mainform1.ShowDialog();

           
        }

        private void 패스워드찾기버튼_Click(object sender, EventArgs e)
        {

            SearchPasswordForm mainform1 = new SearchPasswordForm();
            mainform1.ShowDialog();
        }
    }
}
