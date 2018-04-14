using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaskList.common;
using TaskList.form;

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
                AddTaskPlan addTask = new AddTaskPlan();
                addTask.modeForm = 1; // mode update
                addTask.id = id;
                addTask.ShowDialog();
                //btnSearch_Click(null, null);
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

        private void btnCopy_Click(object sender, EventArgs e)
        {
            CopyPlan copy = new CopyPlan();
            copy.ShowDialog();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel Documents (*.xlsx)|*.xlsx";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    readFileImport(ofd.FileName);
                    MessageBox.Show("Save data successful!");
                }
                catch
                {
                    MessageBox.Show("Please close file Excel: " + ofd.SafeFileName + " and try again!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void readFileImport(string file)
        {
            //Create a test file
            FileInfo fi = new FileInfo(file);
            List<tblTasks> tasks = new List<tblTasks>();

            // TODO
            using (var package = new ExcelPackage(fi))
            {
                var workbook = package.Workbook;
                var worksheet = workbook.Worksheets.First();

                int row = CommonConstants.TASK_LIST_DAILY_CELL_EXPORT_DATA_ROW + 1;
                tblTasks task = new tblTasks();
                task.id = worksheet.Cells[row, CommonConstants.TASK_LIST_DAILY_CELL_EXPORT_DATA_COL].Value;
                object dateCreate = DateTime.Now;// DateTime.Parse(worksheet.Cells[CommonConstants.TASK_LIST_DAILY_CELL_EXPORT_DATE_CREATE].Value.ToString());
                while (task.id != null && !String.IsNullOrWhiteSpace(task.id.ToString()))
                {
                    task.status = CommonUntil.converStatus(worksheet.Cells[row, CommonConstants.TASK_LIST_DAILY_CELL_EXPORT_DATA_COL_STATUS].Value);
                    task.note = worksheet.Cells[row, CommonConstants.TASK_LIST_DAILY_CELL_EXPORT_DATA_COL_NOTE].Value;
                    task.date_finish = dateCreate;
                    tasks.Add(task);

                    // next line
                    row++;
                    task = new tblTasks();
                    task.id = worksheet.Cells[row, CommonConstants.TASK_LIST_DAILY_CELL_EXPORT_DATA_COL].Value;
                }
                package.Save();
            }

            // update data to DB
            Tasks.updateStatus(tasks);
        }

    }
}