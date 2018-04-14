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

namespace TaskList.form
{
    public partial class CopyPlan : Form
    {
        public CopyPlan()
        {
            InitializeComponent();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            bool error_f = false;
            
            btnCopy.Enabled = false;
            int fromYear = dateFrom.Value.Year;
            int toYear = dateTo.Value.Year;
            if(fromYear >= toYear)
            {
                MessageBox.Show("To year need greater than From year.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                error_f = true;
            }
            // check data exist -> Error.
            Int32 count = Tasks.countTaskOfYear(toYear);
            if (count > 0 && !error_f)
            {
                MessageBox.Show("Data of "+ toYear + " year is already exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                error_f = true;
            }

            if(!error_f)
            {
                DialogResult result = MessageBox.Show("Do you want copy plan?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    // Set cursor as hourglass
                    Cursor.Current = Cursors.WaitCursor;
                    //List<tblTasks> tasks = Tasks.selectTaskListByYear(fromYear);
                    Tasks.copyPlan(fromYear, toYear);
                    // Set cursor as default arrow
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Copy plan successful!");
                }
            }
            btnCopy.Enabled = true;
        }

        private void dateTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.P)
            {
                int toYear = dateTo.Value.Year;
                DialogResult result = MessageBox.Show("Do you want delete data of " + toYear + " year?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    // Set cursor as hourglass
                    Cursor.Current = Cursors.WaitCursor;
                    //List<tblTasks> tasks = Tasks.selectTaskListByYear(fromYear);
                    Tasks.deletePlan(toYear);
                    // Set cursor as default arrow
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Delete plan successful!");
                }
            }
        }
    }
}
