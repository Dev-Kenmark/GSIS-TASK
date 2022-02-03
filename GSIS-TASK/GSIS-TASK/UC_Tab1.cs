using System;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using System.IO;
using ExcelDataReader;
using System.Data;
using System.Text;

namespace GSIS_TASK
{
    public partial class UC_Tab1 : UserControl
    {
        DataTableCollection tableCollection;
        public UC_Tab1()
        {
            InitializeComponent();
        }
        private void btnUpload_Click(object sender, EventArgs e)
        {        
            try
            {               

                OpenFileDialog ofDialog = new OpenFileDialog();
              
                ofDialog.Title = "Select file"; 
                ofDialog.InitialDirectory = @"c:\";
                txtFileName.Text = ofDialog.FileName;
                ofDialog.Filter = "Excel Sheet(*.xlsx)|*.xlsx|All Files(*.*)|*.*";
                ofDialog.FilterIndex = 1;
                ofDialog.RestoreDirectory = true;

                if (ofDialog.ShowDialog() == DialogResult.OK)
                {
                    txtFileName.Text = ofDialog.FileName;
                    using (var stream = File.Open(ofDialog.FileName, FileMode.Open, FileAccess.Read))
                    {
                        using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                            });
                            tableCollection = result.Tables;
                            foreach(DataTable table in tableCollection)
                            {
                                DataTable dt = tableCollection[0];
                                dataGridView1.DataSource = dt;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }           
           
        }
 

        private void UC_Tab1_Load(object sender, EventArgs e)
        {

        }
    }
}
