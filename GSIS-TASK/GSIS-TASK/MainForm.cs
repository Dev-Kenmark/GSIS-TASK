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


        private void button1_Click(object sender, EventArgs e)
        {
            UC_Tab1 user1 = new UC_Tab1();
            AddUserControl(user1);
            button1.ForeColor = Color.Black;
            button1.BackColor = Color.FromArgb(255, 224, 192);
            button2.ForeColor = Color.White;
            button2.BackColor = Color.FromArgb(0, 64, 64);
            btnTab3.ForeColor = Color.White;
            btnTab3.BackColor = Color.FromArgb(0, 64, 64);
        } 

        private void button2_Click(object sender, EventArgs e)
        {
            UC_Tab2 user1 = new UC_Tab2();
            AddUserControl(user1);
            button2.ForeColor = Color.Black;
            button2.BackColor = Color.FromArgb(255, 224, 192);
            button1.ForeColor = Color.White;
            button1.BackColor = Color.FromArgb(0, 64, 64);
            btnTab3.ForeColor = Color.White;
            btnTab3.BackColor = Color.FromArgb(0, 64, 64);
        }
        private void btnTab3_Click(object sender, EventArgs e)
        {
            UC_Tab3 user1 = new UC_Tab3();
            AddUserControl(user1);

            button1.ForeColor = Color.White;
            button1.BackColor = Color.FromArgb(0, 64, 64);
            button2.ForeColor = Color.White;
            button2.BackColor = Color.FromArgb(0, 64, 64);
            btnTab3.ForeColor = Color.Black;
            btnTab3.BackColor = Color.FromArgb(255, 224, 192);
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("Do you want to exit the application?", "Exit?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if(rs == DialogResult.Yes)
            {
                this.Close();
            }
        }

    }
}

﻿