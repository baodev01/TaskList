﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskList
{
    public partial class TaskList : Form
    {
        public TaskList()
        {
            InitializeComponent();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void TaskList_Load(object sender, EventArgs e)
        {
            
        }

        private void TaskList_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}