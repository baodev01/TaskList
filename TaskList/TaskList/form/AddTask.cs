using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TaskList.common;

namespace TaskList.form
{
    public partial class AddTask : Form
    {
        public AddTask()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            tblTasks task = new tblTasks();
            task.task_name = txtTaskName.Text;
            task.task_type = cboTaskType.SelectedValue;
            task.plan_date_start = dateStart.Value.Date;
            task.plan_date_end = dateEnd.Value.Date;
            task.plan_person = 1;
            task.re_plan_date_start = dateStart.Value.Date;
            task.re_plan_date_end = dateEnd.Value.Date;
            task.person = 1;
            task.must_date_finish = dateEnd.Value.Date;
            task.status = 0;
            task.note = txtNote.Text;
            if (copyFlag.Checked) { task.copy_f = 1; }
            else { task.copy_f = 0; }
            task.del_f = 0;

            Tasks.insert(task);
            // save data

            MessageBox.Show("Lưu dữ liệu thành công!");
            refreshForm();
        }

        private void refreshForm()
        {
            txtTaskName.Text = "";
            dateStart.Text = "";
            dateEnd.Text = "";
            copyFlag.Checked = true;
            txtNote.Text = "";
        }

        private void AddTask_Load(object sender, EventArgs e)
        {
            loadTaskType();
        }

        private void loadTaskType()
        {
            List<tblTaskType> listTaskType = TaskType.selectAll();

            BindingSource bs = new BindingSource();
            bs.DataSource = listTaskType;

            cboTaskType.DataSource = bs;
            cboTaskType.DisplayMember = "task_type_name";
            cboTaskType.ValueMember = "id";

        }
    }
}
