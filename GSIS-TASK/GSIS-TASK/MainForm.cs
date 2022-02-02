using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GSIS_TASK
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
       
        private void AddUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            pnlMain.Controls.Clear();
            pnlMain.Controls.Add(userControl);
            userControl.BringToFront();

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            UC_Tab1 user1 = new UC_Tab1();
            AddUserControl(user1);
        }

        private void pnlMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            UC_Tab1 user1 = new UC_Tab1();
            AddUserControl(user1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UC_Tab2 user1 = new UC_Tab2();
            AddUserControl(user1);
        }

        private void lblHead_Click(object sender, EventArgs e)
        {

        }
    }
}

﻿