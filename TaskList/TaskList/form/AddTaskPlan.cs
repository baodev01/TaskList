using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TaskList.common;

namespace TaskList.form
{
    public partial class AddTaskPlan : Form
    {
        public int modeForm = 0; // 0: is mode create , 1: is mode update, 2: is mode delete
        public string id { set; get; }

        public AddTaskPlan()
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
                    MessageBox.Show("Data date incorrect. Please edit and again!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            } catch
            {
                MessageBox.Show("Data person incorrect. Please edit and again!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            tblTasks task = new tblTasks();
            task.task_name = txtTaskName.Text;
            task.task_type = cboTaskType.SelectedValue;
            task.plan_date_start = dateStart.Value.Date;
            task.plan_date_end = dateEnd.Value.Date;
            task.plan_person = person;
            task.re_plan_date_start = dateStart.Value.Date;
            task.re_plan_date_end = dateEnd.Value.Date;
            task.person = person;
            task.must_date_finish = dateEnd.Value.Date;
            task.status = 0;
            task.note = txtNote.Text;
            if (copyFlag.Checked) { task.copy_f = 1; }
            else { task.copy_f = 0; }
            task.del_f = 0;

            if (modeForm == 0)
            {
                // add new 
                Tasks.insert(task);
                MessageBox.Show("Save success!");
                refreshForm();
            } else if(modeForm == 2)
            {
                // delete

            } else
            {
                // update
                task.id = id;
                Tasks.updateById(task);
                MessageBox.Show("Save success!");
                this.Close();
            }
            
        }

        private void refreshForm()
        {
            txtTaskName.Text = "";
            dateStart.Text = "";
            dateEnd.Text = "";
            copyFlag.Checked = true;
            txtNote.Text = "";
            txtPerson.Text = "1";
        }

        private void AddTask_Load(object sender, EventArgs e)
        {
            loadTaskType();

            if (modeForm == 0)
            {
                // create
                lblTitle.Text = "Create a task plan";
            }
            else
            {
                // update
                lblTitle.Text = "Update a task plan";
                tblTasks task = new tblTasks();
                task = Tasks.selectTaskById(id);

                txtTaskName.Text = task.task_name.ToString();
                cboTaskType.SelectedValue = task.task_type;
                dateStart.Value = DateTime.Parse(task.plan_date_start.ToString());
                dateEnd.Value = DateTime.Parse(task.plan_date_end.ToString());
                txtPerson.Text = task.plan_person.ToString();
                txtNote.Text = task.note.ToString();
                copyFlag.Checked = Boolean.Parse(task.copy_f.ToString());
            }


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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtTaskName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
