using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaskList.common;

namespace TaskList.form
{
    public partial class ChangePass : Form
    {
        public ChangePass()
        {
            InitializeComponent();
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            bool check = Users.login(txtUser.Text, txtOldPass.Text);
            if (check)
            {
                if(txtNewPass.Text.Equals(txtNewPassAgain.Text) && txtNewPass.Text.Length > 0)
                {
                    Users.changePass(txtUser.Text, txtNewPass.Text);
                    MessageBox.Show("Change pass is success!");
                    this.Close();
                } else
                {
                    MessageBox.Show("NewPass and NewPass(again) is not same!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } else
            {
                MessageBox.Show("UserName or Old Password is incorrect!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
