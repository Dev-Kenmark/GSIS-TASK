using System;
using ExcelDataReader;
using DataTable = System.Data.DataTable;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.IO;
using System.Data;
using ExcelLibrary.SpreadSheet;

namespace GSIS_TASK
{
    public partial class UC_Tab3 : UserControl
    {
        DataTableCollection tableCollection;
        private static string mysqlogdbasefile = AppDomain.CurrentDomain.BaseDirectory + "\\mysqlogdbFile";
        public static string mysql2connString;
        public static MySqlConnection mcon;
        int a, b;
        public string directory = System.AppDomain.CurrentDomain.BaseDirectory;
        public UC_Tab3()
        {
            InitializeComponent();
        }
        private void btnUpload_Click_1(object sender, EventArgs e)
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
                    using var stream = File.Open(ofDialog.FileName, FileMode.Open, FileAccess.Read);
                    using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                            ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                        });
                        tableCollection = result.Tables;
                        foreach (DataTable table in tableCollection)
                        {
                            DataTable dt = tableCollection[0];
                            dataGridView1.DataSource = dt;
                            btnImport.Enabled = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public static void MInitializeFile()
        {
            if (!File.Exists(mysqlogdbasefile))
            {
                StreamWriter sw = new StreamWriter(mysqlogdbasefile);
                sw.WriteLine("");
                sw.Dispose();
                sw.Close();
            }
        }
        public static void MWrite(string strData)
        {
            StreamWriter sw = new StreamWriter(mysqlogdbasefile);
            sw.WriteLine(strData);
            sw.Dispose();
            sw.Close();
        }

        public static string MRead()
        {
            if (!File.Exists(mysqlogdbasefile))
            {
                return "";
            }
            StreamReader sr = new StreamReader(mysqlogdbasefile);
            string str = sr.ReadToEnd();
            sr.Dispose();
            sr.Close();

            return str.Trim();
        }

        private void UC_Tab3_Load(object sender, EventArgs e)
        {
            btnImport.Enabled = false;
            mysql2connString = MRead();

            mcon = new MySqlConnection(mysql2connString);

            mcon.Open();
            mcon.Close();
        }
       


        private void btnImport_Click_1(object sender, EventArgs e)
        {
            try
            {                
                using (MySqlConnection conn = new MySqlConnection(mysql2connString))
                    
                {
                    for (int i = 1; i < dataGridView1.Rows.Count; i++)
                    {

                        conn.Open();
                        dataGridView1.AllowUserToAddRows = false;
                        var command = conn.CreateCommand();

                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "sp_UBPSSS_ImportDR";

                        command.Parameters.AddWithValue("_DR", dataGridView1.Rows[i].Cells[1].Value.ToString());
                        command.Parameters.AddWithValue("_Date", dataGridView1.Rows[i].Cells[0].Value.ToString());
                        command.Parameters.AddWithValue("_Batch", dataGridView1.Rows[i].Cells[4].Value.ToString());
                        command.Parameters.AddWithValue("_Quantity", dataGridView1.Rows[i].Cells[5].Value);
                        command.ExecuteNonQuery();
                        conn.Close();
                    }
                    
                }
                MessageBox.Show("File successfully processed", "Sucessful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch 
            {
             MessageBox.Show("File failed to processed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
        }
    }
}


