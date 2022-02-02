using System;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data;

using System.Windows.Forms;
using ADOX;

namespace GSIS_TASK
{
    public partial class UC_Tab1 : UserControl
    {
        public UC_Tab1()
        {
            InitializeComponent();
        }


        private void UC_Tab1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofDialog = new OpenFileDialog();
            ofDialog.Title = "Select file";
            ofDialog.InitialDirectory = @"c:\";
            txtFileName.Text = ofDialog.FileName;
            ofDialog.Filter = "Excel Sheet(*.xls)|*.xls|All Files(*.*)|*.*";
            ofDialog.FilterIndex = 1;
            ofDialog.RestoreDirectory = true;
            if (ofDialog.ShowDialog() == DialogResult.OK)
            {
                txtFileName.Text = ofDialog.FileName;
               
                Application.DoEvents();
            }
        }

      
       
    }
}
