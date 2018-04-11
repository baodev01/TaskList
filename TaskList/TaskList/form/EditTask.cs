using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TaskList.common;

namespace TaskList.form
{
    public partial class EditTask : Form
    {
        public string id { set; get; }
        public object plan_person { set; get; }

        public EditTask()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int person = 1;
            // validate
            try
            {
                person = Int32.Parse(txtPerson.Text);
                if (person < 1) { person = 1; }
                if (dateStart.Value.Date > dateEnd.Value.Date)
                {
                    MessageBox.Show("Data is date incorrect. Please edit and again!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            } catch
            {
                MessageBox.Show("Data is person incorrect. Please edit and again!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            tblTasks task = new tblTasks();
            task.re_plan_date_start = dateStart.Value.Date;
            task.re_plan_date_end = dateEnd.Value.Date;
            task.person = person;
            task.must_date_finish = dateMustFinish.Value.Date;
            task.status = cboStatus.SelectedValue;
            task.note = txtNote.Text;

            // update
            task.id = id;
            Tasks.updateRePlanById(task);
            MessageBox.Show("Save success!");
            this.Close();
        }

        private void refreshForm()
        {
            //txtTaskName.Text = "";
            //dateStart.Text = "";
            //dateEnd.Text = "";
            //copyFlag.Checked = true;
            //txtNote.Text = "";
            //txtPerson.Text = "1";
        }

        private void AddTask_Load(object sender, EventArgs e)
        {
            loadTaskType();
            loadAreas();
            loadStatus();
            // update
            //lblTitle.Text = "Update a Task";
            tblTasks task = new tblTasks();
            task = Tasks.selectTaskStatusById(id);

            txtTaskName.Text = task.task_name.ToString();
            txtTaskNameEn.Text = task.task_name_en.ToString();
            cboTaskType.SelectedValue = task.task_type;
            cboAreas.SelectedValue = task.areas;
            txtLocations.Text = task.location.ToString();
            dateStart.Value = DateTime.Parse(task.re_plan_date_start.ToString());
            dateEnd.Value = DateTime.Parse(task.re_plan_date_end.ToString());
            txtPerson.Text = task.person.ToString();
            dateMustFinish.Value = DateTime.Parse(task.must_date_finish.ToString());
            cboStatus.SelectedValue = task.status;
            txtNote.Text = task.note.ToString();
            this.plan_person = task.plan_person;
            //copyFlag.Checked = Boolean.Parse(task.copy_f.ToString());

            txtTaskName.Enabled = false;
            txtTaskNameEn.Enabled = false;
            cboTaskType.Enabled = false;
            cboAreas.Enabled = false;
            txtLocations.Enabled = false;
            dateMustFinish.Enabled = false;
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

        private void loadAreas()
        {
            List<tblAreas> areas = Areas.selectAll();

            BindingSource bs = new BindingSource();
            bs.DataSource = areas;

            cboAreas.DataSource = bs;
            cboAreas.DisplayMember = "areas";
            cboAreas.ValueMember = "id";

        }

        private void loadStatus()
        {
            List<tblStatus> statusList = new List<tblStatus>();
            tblStatus status = new tblStatus();
            status.id = 0;
            status.status = "";
            statusList.Add(status);
            status = new tblStatus();
            status.id = 1;
            status.status = "doing";
            statusList.Add(status);
            status = new tblStatus();
            status.id = 9;
            status.status = "done";
            statusList.Add(status);

            BindingSource bs = new BindingSource();
            bs.DataSource = statusList;

            cboStatus.DataSource = bs;
            cboStatus.DisplayMember = "status";
            cboStatus.ValueMember = "id";

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtTaskName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPerson_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime day = CommonUntil.mustFinish(dateStart.Value.Date, dateEnd.Value.Date, this.plan_person, txtPerson.Text);
                dateMustFinish.Value = day;
            } catch
            {
                //
            }
        }

        private void dateStart_ValueChanged(object sender, EventArgs e)
        {
            txtPerson_TextChanged(null, null);
        }

        private void dateEnd_ValueChanged(object sender, EventArgs e)
        {
            txtPerson_TextChanged(null, null);
        }
    }
}
