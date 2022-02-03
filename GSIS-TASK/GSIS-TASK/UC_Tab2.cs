using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GSIS_TASK
{
    public partial class UC_Tab2 : UserControl
    {
        public UC_Tab2()
        {
            InitializeComponent();
        }

        private void UC_Tab2_Load(object sender, EventArgs e)
        {
            dtpFrom.Value = DateTime.Now;
            dtpTo.Value = DateTime.Now;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((dtpFrom.Value.CompareTo(dtpTo.Value) > 0))
            {
                MessageBox.Show("Invalid Range");
                dtpFrom.Value = DateTime.Now;
                dtpTo.Value = DateTime.Now;
            }
            else if ((dtpFrom.Value.CompareTo(dtpTo.Value) < 0 || dtpFrom.Value.CompareTo(dtpTo.Value) == 0))
            {
                //db things here
                //MessageBox.Show("Working");
            }
        }
    }
}
