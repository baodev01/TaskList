using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
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
            
            //MessageBox.Show("double click");
            int row = dataGridView1.SelectedCells[0].RowIndex;
            int col = dataGridView1.SelectedCells[0].ColumnIndex;

            if (col != 12)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[row];
                string id = selectedRow.Cells["id"].Value.ToString();
                if (!String.IsNullOrEmpty(id))
                {
                    form.EditTask editTask = new form.EditTask();
                    editTask.id = id;
                    editTask.ShowDialog();
                    //btnSearch_Click(null, null);
                }
            }
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
            string delay = dataGridView1.Rows[e.RowIndex].Cells["Delay"].Value.ToString();
            if ("delay".Equals(delay))
            {
                dataGridView1.Rows[e.RowIndex].Cells["status"].Style.BackColor = Color.MistyRose;
            } else
            {
                dataGridView1.Rows[e.RowIndex].Cells["status"].Style.BackColor = Color.White;
            }
            dataGridView1.Rows[e.RowIndex].Cells["person"].Style.BackColor = Color.AliceBlue;
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int person = Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells["person"].Value.ToString());
                DateTime dateStart = DateTime.ParseExact(dataGridView1.Rows[e.RowIndex].Cells["re_plan_date_start"].Value.ToString(),"dd-MM-yyyy", CultureInfo.InvariantCulture);
                DateTime dateEnd = DateTime.ParseExact(dataGridView1.Rows[e.RowIndex].Cells["re_plan_date_end"].Value.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                object plan_person = dataGridView1.Rows[e.RowIndex].Cells["plan_person"].Value;

                DateTime day = CommonUntil.mustFinish(dateStart, dateEnd, plan_person, person);
                dataGridView1.Rows[e.RowIndex].Cells["must_date_finish"].Value = day.ToString("dd-MM-yyyy"); ;
            } catch
            {
                //
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            xuatFile();
        }

        private void xuatFile()
        {
            String dateCreate = DateTime.Now.ToString("dd-MM-yyyy");
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xlsx)|*.xlsx";
            sfd.FileName = "TaskList_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".xlsx";

            String templateFilePath = "TaskList_template.xlsx";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                // Copy DataGridView results to clipboard
                //copyAlltoClipboard();

                FileInfo newFile = new FileInfo(sfd.FileName);
                FileInfo template = null;
                if (!string.IsNullOrEmpty(templateFilePath))
                {
                    template = new FileInfo(templateFilePath);
                }

                using (ExcelPackage xlPackage = new ExcelPackage(newFile, template))
                {

                    ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets.FirstOrDefault(ws => ws.Name == "TaskList");

                    // Create a new table.
                    DataTable taskTable = new DataTable("TaskList");

                    // Create the columns.
                    taskTable.Columns.Add("Id", typeof(int));
                    taskTable.Columns.Add("Task Name", typeof(string));
                    taskTable.Columns.Add("Task Type", typeof(string));
                    taskTable.Columns.Add("Areas", typeof(string));
                    taskTable.Columns.Add("Location", typeof(string));
                    taskTable.Columns.Add("Person Plan", typeof(int));
                    taskTable.Columns.Add("Start Date Plan", typeof(string));
                    taskTable.Columns.Add("Finish Date Plan", typeof(string));
                    taskTable.Columns.Add("Person", typeof(int));
                    taskTable.Columns.Add("Date Must Finish", typeof(string));
                    taskTable.Columns.Add("Status", typeof(string));
                    taskTable.Columns.Add("Delay", typeof(string));
                    taskTable.Columns.Add("Note", typeof(string));

                    //Add data to the new table.
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        DataRow tableRow = taskTable.NewRow();
                        tableRow["Id"] = row.Cells["id"].Value;
                        if(chkEnglish.Checked)
                        {
                            tableRow["Task Name"] = row.Cells["task_name_en"].Value;
                        } else
                        {
                            tableRow["Task Name"] = row.Cells["task_name"].Value;
                        }
                        tableRow["Task Type"] = row.Cells["task_type_name"].Value;
                        tableRow["Areas"] = row.Cells["areas"].Value;
                        tableRow["Location"] = row.Cells["location"].Value;
                        tableRow["Person Plan"] = row.Cells["plan_person"].Value;
                        tableRow["Start Date Plan"] = row.Cells["re_plan_date_start"].Value;
                        tableRow["Finish Date Plan"] = row.Cells["re_plan_date_end"].Value;
                        tableRow["Person"] = row.Cells["person"].Value;
                        tableRow["Date Must Finish"] = row.Cells["must_date_finish"].Value;
                        tableRow["Status"] = row.Cells["status"].Value;
                        tableRow["Delay"] = row.Cells["delay"].Value;
                        tableRow["Note"] = row.Cells["note"].Value;

                        taskTable.Rows.Add(tableRow);
                    }
                    int startRow = CommonConstants.TASK_LIST_DAILY_CELL_EXPORT_ROW_STYLE;
                    int rowCount = taskTable.Rows.Count - startRow;

                    if(rowCount > 0)
                    {
                        // format sheet
                        int copyRow = startRow - 1;
                        worksheet.InsertRow(startRow, rowCount);
                        for (var i = 0; i < rowCount; i++)
                        {
                            var row = startRow + i;
                            worksheet.Cells[String.Format("{0}:{0}", copyRow)].Copy(worksheet.Cells[String.Format("{0}:{0}", row)]);
                            worksheet.Row(row).StyleID = worksheet.Row(copyRow).StyleID;
                        }
                    }
                    // add data to sheet
                    worksheet.Cells[CommonConstants.TASK_LIST_DAILY_CELL_EXPORT_DATA_ROW, CommonConstants.TASK_LIST_DAILY_CELL_EXPORT_DATA_COL].LoadFromDataTable( taskTable, true);
                    worksheet.Cells[CommonConstants.TASK_LIST_DAILY_CELL_EXPORT_DATE_CREATE].Value = dateCreate;

                    xlPackage.Save();
                }
                MessageBox.Show("Export success!");
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel Documents (*.xlsx)|*.xlsx";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show(ofd.FileName);
                readFileImport(ofd.FileName);
            }
        }

        public void readFileImport(string file)
        {
            //Create a test file
            FileInfo fi = new FileInfo(file);

            using (var package = new ExcelPackage(fi))
            {
                var workbook = package.Workbook;
                var worksheet = workbook.Worksheets.First();
                MessageBox.Show(worksheet.Cells[CommonConstants.TASK_LIST_DAILY_CELL_EXPORT_DATA_ROW,CommonConstants.TASK_LIST_DAILY_CELL_EXPORT_DATA_COL].Value.ToString());
                //var ThatList = worksheet.Tables.First().ConvertTableToObjects<ExcelData>();
                //foreach (var data in ThatList)
                //{
                //    Console.WriteLine(data.Id + data.Name + data.Gender);
                //}

                package.Save();
            }
        }
    }
}
