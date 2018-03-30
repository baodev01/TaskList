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
    public partial class TaskList : Form
    {
        public TaskList()
        {
            InitializeComponent();
        }

        private void TaskList_Load(object sender, EventArgs e)
        {
            btnSearch_Click(null, null);
        }

        private void TaskList_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void taskListPlanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TaskListPlan plan = new TaskListPlan();
            plan.ShowDialog();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show("double click");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            bool finish_f = chkFinish.Checked;
            List<tblTasks> list = Tasks.selectTaskList(dateFrom.Value.Date, dateTo.Value.Date, finish_f);

            BindingSource bs = new BindingSource();
            bs.DataSource = list;

            dataGridView1.DataSource = bs;

            // setting header
            if (dataGridView1.Columns["task_name"] != null)
                dataGridView1.Columns["task_name"].HeaderText = "Task Name";
            if (dataGridView1.Columns["task_type_name"] != null)
                dataGridView1.Columns["task_type_name"].HeaderText = "Task Type";
            if (dataGridView1.Columns["plan_person"] != null)
                dataGridView1.Columns["plan_person"].HeaderText = "Person Plan";
            if (dataGridView1.Columns["re_plan_date_start"] != null)
                dataGridView1.Columns["re_plan_date_start"].HeaderText = "Start Date Plan";
            if (dataGridView1.Columns["re_plan_date_end"] != null)
                dataGridView1.Columns["re_plan_date_end"].HeaderText = "Finish Date Plan";
            if (dataGridView1.Columns["person"] != null)
                dataGridView1.Columns["person"].HeaderText = "Person";
            if (dataGridView1.Columns["must_date_finish"] != null)
                dataGridView1.Columns["must_date_finish"].HeaderText = "Date Must Finish";
            if (dataGridView1.Columns["status"] != null)
                dataGridView1.Columns["status"].HeaderText = "Status";
            if (dataGridView1.Columns["note"] != null)
                dataGridView1.Columns["note"].HeaderText = "Note";

            // setting column not display
            if (dataGridView1.Columns["task_type"] != null)
                dataGridView1.Columns["task_type"].Visible = false;
            if (dataGridView1.Columns["plan_date_start"] != null)
                dataGridView1.Columns["plan_date_start"].Visible = false;
            if (dataGridView1.Columns["plan_date_end"] != null)
                dataGridView1.Columns["plan_date_end"].Visible = false;
            //if (dataGridView1.Columns["person"] != null)
            //    dataGridView1.Columns["person"].Visible = false;
            //if (dataGridView1.Columns["must_date_finish"] != null)
            //    dataGridView1.Columns["must_date_finish"].Visible = false;
            //if (dataGridView1.Columns["status"] != null)
            //    dataGridView1.Columns["status"].Visible = false;
            if (dataGridView1.Columns["date_finish"] != null)
                dataGridView1.Columns["date_finish"].Visible = false;
            if (dataGridView1.Columns["del_f"] != null)
                dataGridView1.Columns["del_f"].Visible = false;
            if (dataGridView1.Columns["copy_f"] != null)
                dataGridView1.Columns["copy_f"].Visible = false;

            lblCountJob.Text = dataGridView1.RowCount.ToString();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
