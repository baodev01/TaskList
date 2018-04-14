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

namespace TaskList
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            bool check = Users.login(txtUser.Text, txtPass.Text);

            if(check)
            {
                TaskList taskList = new TaskList();
                taskList.Show();
                this.Hide();
            } else
            {
                MessageBox.Show("UserName or Password is incorrect!","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            form.AddTaskPlan addTask = new form.AddTaskPlan();
            addTask.ShowDialog();

        }

        private void login_Load(object sender, EventArgs e)
        {

        }

        private void txtPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(null, null);
            }
        }
    }
}
