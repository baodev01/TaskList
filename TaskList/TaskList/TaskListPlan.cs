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
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            if (dataGridView1.Columns["task_name"] != null)
            {
                dataGridView1.Columns["task_name"].HeaderText = "Task Name";
                dataGridView1.Columns["task_name"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            }
            if (dataGridView1.Columns["task_name_en"] != null)
            {
                dataGridView1.Columns["task_name_en"].HeaderText = "Task Name (English)";
                dataGridView1.Columns["task_name_en"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            }
            if (dataGridView1.Columns["task_type_name"] != null)
                dataGridView1.Columns["task_type_name"].HeaderText = "Task Type";
            if (dataGridView1.Columns["areas"] != null)
                dataGridView1.Columns["areas"].HeaderText = "Areas";
            if (dataGridView1.Columns["location"] != null)
                dataGridView1.Columns["location"].HeaderText = "Location";
            if (dataGridView1.Columns["plan_date_start"] != null)
                dataGridView1.Columns["plan_date_start"].HeaderText = "Start Date";
            if (dataGridView1.Columns["plan_date_end"] != null)
                dataGridView1.Columns["plan_date_end"].HeaderText = "Finish Date";
            if (dataGridView1.Columns["plan_person"] != null)
                dataGridView1.Columns["plan_person"].HeaderText = "Person";
            if (dataGridView1.Columns["note"] != null)
            {
                dataGridView1.Columns["note"].HeaderText = "Note";
                dataGridView1.Columns["note"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            }
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
            if (dataGridView1.Columns["delay"] != null)
                dataGridView1.Columns["delay"].Visible = false;

            lblCountJob.Text = dataGridView1.RowCount.ToString();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            int row = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[row];
            string id = selectedRow.Cells["id"].Value.ToString();
            if (!String.IsNullOrEmpty(id))
            {
                form.AddTaskPlan addTask = new form.AddTaskPlan();
                addTask.modeForm = 1; // mode update
                addTask.id = id;
                addTask.ShowDialog();
                btnSearch_Click(null, null);
            }
        }

        private void TaskListPlan_Load(object sender, EventArgs e)
        {
            int year = DateTime.Now.Year;
            DateTime firstDayYear = new DateTime(year, 1, 1);
            DateTime lastDayYear = new DateTime(year, 12, 31);

            dateFrom.Value = firstDayYear;
            dateTo.Value = lastDayYear;

            btnSearch_Click(null, null);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            form.AddTaskPlan addTask = new form.AddTaskPlan();
            addTask.ShowDialog();

            btnSearch_Click(null, null);
        }
    }
}