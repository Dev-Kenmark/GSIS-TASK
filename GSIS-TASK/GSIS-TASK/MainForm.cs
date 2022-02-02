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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UC_Tab1 user1 = new UC_Tab1();
            addUserControl(user1);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void addUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(userControl);
            userControl.BringToFront();

        }

        private void panelContainer_Paint(object sender, PaintEventArgs e)
        {

        } 

        private void btnTab1_Click(object sender, EventArgs e)
        {
            UC_Tab1 user1 = new UC_Tab1();
            addUserControl(user1);
        }
        private void btnTab2_Click(object sender, EventArgs e)
        {
            UC_Tab2 user2 = new UC_Tab2();
            addUserControl(user2);
        }

    }
}
