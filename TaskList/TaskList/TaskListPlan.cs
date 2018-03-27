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
    public partial class TaskListPlan : Form
    {
        public TaskListPlan()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<tblTasks> list = Tasks.selectTaskListPlan(dateFrom.Value.Date, dateTo.Value.Date);
            
            BindingSource bs = new BindingSource();
            bs.DataSource = list;

            dataGridView1.DataSource = bs;

            // setting header
            if (dataGridView1.Columns["task_name"] != null)
                dataGridView1.Columns["task_name"].HeaderText = "Task Name";
            if (dataGridView1.Columns["task_type_name"] != null)
                dataGridView1.Columns["task_type_name"].HeaderText = "Task Type";
            if (dataGridView1.Columns["plan_date_start"] != null)
                dataGridView1.Columns["plan_date_start"].HeaderText = "Date Start";
            if (dataGridView1.Columns["plan_date_end"] != null)
                dataGridView1.Columns["plan_date_end"].HeaderText = "Date End";
            if (dataGridView1.Columns["plan_person"] != null)
                dataGridView1.Columns["plan_person"].HeaderText = "Person";
            if (dataGridView1.Columns["note"] != null)
                dataGridView1.Columns["note"].HeaderText = "Note";
            if (dataGridView1.Columns["copy_f"] != null)
                dataGridView1.Columns["copy_f"].HeaderText = "Repeat";
            // setting column not display

            if (dataGridView1.Columns["task_type"] != null)
                dataGridView1.Columns["task_type"].Visible = false;
            if (dataGridView1.Columns["re_plan_date_start"] != null)
                dataGridView1.Columns["re_plan_date_start"].Visible = false;
            if (dataGridView1.Columns["re_plan_date_end"] != null)
                dataGridView1.Columns["re_plan_date_end"].Visible = false;
            if (dataGridView1.Columns["person"] != null)
                dataGridView1.Columns["person"].Visible = false;
            if (dataGridView1.Columns["must_date_finish"] != null)
                dataGridView1.Columns["must_date_finish"].Visible = false;
            if (dataGridView1.Columns["status"] != null)
                dataGridView1.Columns["status"].Visible = false;
            if (dataGridView1.Columns["date_finish"] != null)
                dataGridView1.Columns["date_finish"].Visible = false;
            if (dataGridView1.Columns["del_f"] != null)
                dataGridView1.Columns["del_f"].Visible = false;
        }
    }
}
