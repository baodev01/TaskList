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

        private void CopyPlan_Load(object sender, EventArgs e)
        {
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            btnCopy.Enabled = false;
            int fromYear = dateFrom.Value.Year;
            int toYear = dateTo.Value.Year;
            if(fromYear >= toYear)
            {
                MessageBox.Show("Error! To year need greater than From year");
                btnCopy.Enabled = true;
                return;
            }
            // check data exist -> Error.
            Int32 count = Tasks.countTaskOfYear(toYear);
            if (count > 0)
            {
                MessageBox.Show("Error! Data of "+ toYear + " year is already exist.");
                btnCopy.Enabled = true;
                return;
            }

            DialogResult result = MessageBox.Show("Do you want copy plan?", "Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                //...
            }
            btnCopy.Enabled = true;
        }
    }
}
