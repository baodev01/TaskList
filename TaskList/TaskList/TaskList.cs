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
            List<tblTasks> list = Tasks.selectTaskList(dateTo.Value.Date, finish_f);

            BindingSource bs = new BindingSource();
            bs.DataSource = list;

            dataGridView1.DataSource = bs;

            // setting header
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            if (dataGridView1.Columns["id"] != null)
            {
                dataGridView1.Columns["id"].HeaderText = "Id";
                dataGridView1.Columns["id"].ReadOnly = true;
            }

            if(chkEnglish.Checked)
            {
                if (dataGridView1.Columns["task_name_en"] != null)
                {
                    dataGridView1.Columns["task_name_en"].HeaderText = "Task Name (English)";
                    dataGridView1.Columns["task_name_en"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    dataGridView1.Columns["task_name_en"].ReadOnly = true;
                    dataGridView1.Columns["task_name_en"].Visible = true;
                    if (dataGridView1.Columns["task_name"] != null)
                    {
                        dataGridView1.Columns["task_name"].Visible = false;
                    }
                }
            } else
            {
                if (dataGridView1.Columns["task_name"] != null)
                {
                    dataGridView1.Columns["task_name"].HeaderText = "Task Name";
                    dataGridView1.Columns["task_name"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    dataGridView1.Columns["task_name"].ReadOnly = true;
                    dataGridView1.Columns["task_name"].Visible = true;
                    if (dataGridView1.Columns["task_name_en"] != null)
                    {
                        dataGridView1.Columns["task_name_en"].Visible = false;
                    }
                }
            }
            
            if (dataGridView1.Columns["task_type_name"] != null)
            {
                dataGridView1.Columns["task_type_name"].HeaderText = "Task Type";
                dataGridView1.Columns["task_type_name"].ReadOnly = true;
            }
            if (dataGridView1.Columns["areas"] != null)
            {
                dataGridView1.Columns["areas"].HeaderText = "Areas";
                dataGridView1.Columns["areas"].ReadOnly = true;
            }
            if (dataGridView1.Columns["location"] != null)
            {
                dataGridView1.Columns["location"].HeaderText = "Location";
                dataGridView1.Columns["location"].ReadOnly = true;
            }
            if (dataGridView1.Columns["plan_person"] != null)
            {
                dataGridView1.Columns["plan_person"].HeaderText = "Person Plan";
                dataGridView1.Columns["plan_person"].ReadOnly = true;
            }
            if (dataGridView1.Columns["re_plan_date_start"] != null)
            {
                dataGridView1.Columns["re_plan_date_start"].HeaderText = "Start Date Plan";
                dataGridView1.Columns["re_plan_date_start"].ReadOnly = true;
            }
            if (dataGridView1.Columns["re_plan_date_end"] != null)
            {
                dataGridView1.Columns["re_plan_date_end"].HeaderText = "Finish Date Plan";
                dataGridView1.Columns["re_plan_date_end"].ReadOnly = true;
            }
            if (dataGridView1.Columns["person"] != null)
            {
                dataGridView1.Columns["person"].HeaderText = "Person";
                dataGridView1.Columns["person"].ReadOnly = false;
            }
            if (dataGridView1.Columns["must_date_finish"] != null)
            {
                dataGridView1.Columns["must_date_finish"].HeaderText = "Date Must Finish";
                dataGridView1.Columns["must_date_finish"].ReadOnly = true;
            }
            if (dataGridView1.Columns["status"] != null)
            {
                dataGridView1.Columns["status"].HeaderText = "Status";
                dataGridView1.Columns["status"].ReadOnly = true;
            }
            if (dataGridView1.Columns["delay"] != null)
            {
                dataGridView1.Columns["delay"].HeaderText = "Delay";
                dataGridView1.Columns["delay"].ReadOnly = true;
            }
            if (dataGridView1.Columns["note"] != null)
            {
                dataGridView1.Columns["note"].HeaderText = "Note";
                dataGridView1.Columns["note"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dataGridView1.Columns["note"].ReadOnly = true;
                //dataGridView1.Columns["note"].Width = 200;
            }

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
            if (dataGridView1.Columns["delay"] != null)
                dataGridView1.Columns["delay"].Visible = false;
            //if (dataGridView1.Columns["note"] != null)
            //    dataGridView1.Columns["note"].Visible = false;

            //dataGridView1.ScrollBars = 
            lblCountJob.Text = dataGridView1.RowCount.ToString();
            int countDone = 0;
            int countDelay = 0;

            foreach (tblTasks el in list)
            {
                if("done".Equals(el.status.ToString()))
                {
                    countDone++;
                }
                if ("delay".Equals(el.delay.ToString()))
                {
                    countDelay++;
                }
            }
            lblCountDone.Text = countDone.ToString();
            lblCountDelay.Text = countDelay.ToString();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string delay = row.Cells["Delay"].Value.ToString();

                if ("delay".Equals(delay))
                {
                    row.Cells["status"].Style.BackColor = Color.MistyRose;
                } else
                {
                    row.Cells["status"].Style.BackColor = Color.White;
                }
                row.Cells["person"].Style.BackColor = Color.AliceBlue;
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // TODO change person
        }
    }
}
