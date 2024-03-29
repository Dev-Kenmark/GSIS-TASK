﻿using System;
using System.Data;

using ExcelLibrary.SpreadSheet;
using Workbook = ExcelLibrary.SpreadSheet.Workbook;
using Worksheet = ExcelLibrary.SpreadSheet.Worksheet;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using ExcelDataReader;
using DataTable = System.Data.DataTable;

using ExcelLibrary.SpreadSheet;
using ExcelLibrary.CompoundDocumentFormat;
using System.Diagnostics;

using MySql.Data;
using MySql.Data.MySqlClient;
namespace GSIS_TASK
{
    public partial class UC_Tab1 : UserControl
    {
        DataTableCollection tableCollection;
        private static string dbasefile = AppDomain.CurrentDomain.BaseDirectory + "\\dbFile";
        private static string mysqldbasefile = AppDomain.CurrentDomain.BaseDirectory + "\\mysqldbFile";
        public static string connString, mysqlconnString;
        public static SqlConnection con = new SqlConnection(connString);
        public static MySqlConnection mcon; 
        int a, b;
        public string directory = System.AppDomain.CurrentDomain.BaseDirectory;

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

                            /*dataGridView1.Columns[9].HeaderText = "DONE";
                            dataGridView1.Columns[10].HeaderText = "CHECKING";*/



                        }
                        //AddARow();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }           
           
        }
        //private void AddARow()
        //{
        //    DataTable dtable = (DataTable)dataGridView1.DataSource;
        //    dtable.Columns.RemoveAt(11);
        //    dtable.Columns.Add(new DataColumn("STATUS 1"));
        //    dtable.Columns.Add(new DataColumn("STATUS 2"));
           
        //    dataGridView1.DataSource = dtable;
           
        //    /*foreach (DataGridViewRow row in dataGridView1.Rows)
        //    {
        //        row.Cells["STATUS 1"].Value = "KM";
        //        row.Cells["STATUS 2"].Value = "KM";

        //    }*/
            
        //    dtable.AcceptChanges();


        //}

        public static void InitializeFile()
        {
            if (!File.Exists(dbasefile))
            {
                StreamWriter sw = new StreamWriter(dbasefile);
                sw.WriteLine("");
                sw.Dispose();
                sw.Close();
            }
        }

        public static void MInitializeFile()
        {
            if (!File.Exists(mysqldbasefile))
            {
                StreamWriter sw = new StreamWriter(mysqldbasefile);
                sw.WriteLine("");
                sw.Dispose();
                sw.Close();
            }
        }

        public static void Write(string strData)
        {
            StreamWriter sw = new StreamWriter(dbasefile);
            sw.WriteLine(strData);
            sw.Dispose();
            sw.Close();
        }

        public static string Read()
        {
            if (!File.Exists(dbasefile))
            {
                return "";
            }
            StreamReader sr = new StreamReader(dbasefile);
            string str = sr.ReadToEnd();
            sr.Dispose();
            sr.Close();

            return str.Trim();
        }

        public static void MWrite(string strData)
        {
            StreamWriter sw = new StreamWriter(mysqldbasefile);
            sw.WriteLine(strData);
            sw.Dispose();
            sw.Close();
        }

        public static string MRead()
        {
            if (!File.Exists(mysqldbasefile))
            {
                return "";
            }
            StreamReader sr = new StreamReader(mysqldbasefile);
            string str = sr.ReadToEnd();
            sr.Dispose();
            sr.Close();

            return str.Trim();
        }

        private void UC_Tab1_Load(object sender, EventArgs e)
        {
            InitializeFile();
            MInitializeFile();
            connString = Read();
            mysqlconnString = MRead();

            mcon = new MySqlConnection(mysqlconnString);

            mcon.Open();
            mcon.Close();
            //mysqlconnString = "server=DESKTOP-39L40B6;uid=root;pwd=1234;database=ubp_sss";
            btnImport.Enabled = false;
        }

        private void backgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {

        }

        private void txtFileName_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connString))

            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    conn.Open();
                    dataGridView1.AllowUserToAddRows = false;
                    var command = conn.CreateCommand();
                    { 
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "[dbo].[spUBTable_Insert]";

                        command.Parameters.AddWithValue("@CRN_NUMBER", dataGridView1.Rows[i].Cells[0].Value.ToString());
                        command.Parameters.AddWithValue("@FIRSTNAME", dataGridView1.Rows[i].Cells[1].Value.ToString());
                        command.Parameters.AddWithValue("@MIDDLENAME", dataGridView1.Rows[i].Cells[2].Value.ToString());
                        command.Parameters.AddWithValue("@LASTNAME", dataGridView1.Rows[i].Cells[3].Value.ToString());
                        command.Parameters.AddWithValue("@CARD_ID#", dataGridView1.Rows[i].Cells[4].Value.ToString());
                        command.Parameters.AddWithValue("@GSIS_ID#", dataGridView1.Rows[i].Cells[5].Value.ToString());
                        command.Parameters.AddWithValue("@BRANCH_OFFICENAME", dataGridView1.Rows[i].Cells[6].Value.ToString());
                        command.Parameters.AddWithValue("@UMID_REFERENCE", dataGridView1.Rows[i].Cells[7].Value.ToString());
                        command.Parameters.AddWithValue("@EMBOSSING_FILE", dataGridView1.Rows[i].Cells[8].Value.ToString());
                        //command.Parameters.AddWithValue("@STATUS_1", dataGridView1.Rows[i].Cells[11].Value.ToString());
                        //command.Parameters.AddWithValue("@STATUS_2", dataGridView1.Rows[i].Cells[12].Value.ToString());

                        command.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            try
            {
                DialogResult result = MessageBox.Show("Do you want to process an excel file?", "Import?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    SaveFileDialog ofDialog = new SaveFileDialog();
                    ofDialog.Title = "Select file";
                    ofDialog.InitialDirectory = @"c:\";
                    txtFileName.Text = ofDialog.FileName;
                    ofDialog.Filter = "Excel Sheet(.xls)|.xls";
                    ofDialog.FilterIndex = 1;
                    ofDialog.RestoreDirectory = true;
                    if (ofDialog.ShowDialog() == DialogResult.OK)
                    {
                        string file = ofDialog.FileName;//AppDomain.CurrentDomain.BaseDirectory + "Exported_with_Columns_" + DateTime.Now.ToString("MM-dd-yyy") + ".xls";
                        Workbook workbook = new Workbook();
                        Worksheet worksheet = new Worksheet("First Sheet");
                        worksheet.Cells[0, 0] = new Cell("CRN NUMBER");
                        worksheet.Cells[0, 1] = new Cell("FIRSTNAME");
                        worksheet.Cells[0, 2] = new Cell("MIDDLENAME");
                        worksheet.Cells[0, 3] = new Cell("LASTNAME");
                        worksheet.Cells[0, 4] = new Cell("CARD ID #");
                        worksheet.Cells[0, 5] = new Cell("GSIS ID #");
                        worksheet.Cells[0, 6] = new Cell("BRANCH/OFFICENAME");
                        worksheet.Cells[0, 7] = new Cell("UMID REFERENCE");
                        worksheet.Cells[0, 8] = new Cell("EMBOSSING FILE");
                        //worksheet.Cells[0, 9] = new Cell("STATUS 1");
                        //worksheet.Cells[0, 10] = new Cell("STATUS 2");                        

                        int j = 0;
                        int i = 1;
                        using (SqlConnection conn = new SqlConnection(connString))

                        {
                            var command = conn.CreateCommand();
                            {
                                conn.Open();
                                command.CommandType = CommandType.StoredProcedure;
                                command.CommandText = "[dbo].[spUBTable_Select]";
                                command.ExecuteNonQuery();
                                conn.Close();
                                SqlDataAdapter data = new SqlDataAdapter(command);
                                DataTable dt = new DataTable();
                                data.Fill(dt);



                                using (MySqlConnection mconn = new MySqlConnection(mysqlconnString))
                                {
                                    mconn.Open();

                                    foreach (DataRow row in dt.Rows)
                                    {
                                        //DataRow row = drv.Row;
                                        //MessageBox.Show(row.Cells[j].Value.ToString());
                                        worksheet.Cells[i, j] = new Cell(row.ItemArray[j].ToString());
                                        worksheet.Cells[i, j + 1] = new Cell(row.ItemArray[j + 1].ToString());
                                        worksheet.Cells[i, j + 2] = new Cell(row.ItemArray[j + 2].ToString());
                                        worksheet.Cells[i, j + 3] = new Cell(row.ItemArray[j + 3].ToString());
                                        worksheet.Cells[i, j + 4] = new Cell(row.ItemArray[j + 4].ToString());
                                        worksheet.Cells[i, j + 5] = new Cell(row.ItemArray[j + 5].ToString());
                                        worksheet.Cells[i, j + 6] = new Cell(row.ItemArray[j + 6].ToString());
                                        worksheet.Cells[i, j + 7] = new Cell(row.ItemArray[j + 7].ToString());
                                        worksheet.Cells[i, j + 8] = new Cell(row.ItemArray[j + 8].ToString());
                                        worksheet.Cells[i, j + 9] = new Cell(row.ItemArray[j + 9].ToString());
                                        worksheet.Cells[i, j + 10] = new Cell(row.ItemArray[j + 10].ToString());

                                        try
                                        {
                                            //dito ilalagay ung sa mysql
                                            var cmd = mconn.CreateCommand();
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.CommandText = "sp_ExtractDR";
                                            string xEbossFIle = row.ItemArray[j + 8].ToString();
                                            cmd.Parameters.AddWithValue("_EmbossFile", xEbossFIle);
                                            cmd.ExecuteNonQuery();

                                            MySqlDataAdapter mdata = new MySqlDataAdapter(cmd);
                                            DataTable mdt = new DataTable();
                                            mdata.Fill(mdt);

                                            worksheet.Cells[i, j + 11] = new Cell(mdt.Rows[0].ItemArray[0].ToString());
                                            worksheet.Cells[i, j + 12] = new Cell(mdt.Rows[0].ItemArray[1].ToString());
                                        }
                                        catch (Exception ee)
                                        {
                                            MessageBox.Show(ee.Message);
                                        }

                                        /*worksheet.Cells[i, j + 1] = new Cell(row.Cells[j + 1]?.Value?.ToString());
                                        worksheet.Cells[i, j + 2] = new Cell(row.Cells[j + 2]?.Value?.ToString()); 
                                        worksheet.Cells[i, j + 3] = new Cell(row.Cells[j + 3]?.Value?.ToString());
                                        worksheet.Cells[i, j + 4] = new Cell(row.Cells[j + 4]?.Value?.ToString());
                                        worksheet.Cells[i, j + 5] = new Cell(row.Cells[j + 5]?.Value?.ToString()); 
                                        worksheet.Cells[i, j + 6] = new Cell(row.Cells[j + 6]?.Value?.ToString());
                                        worksheet.Cells[i, j + 7] = new Cell(row.Cells[j + 7]?.Value?.ToString());
                                        worksheet.Cells[i, j + 8] = new Cell(row.Cells[j + 8]?.Value?.ToString());*/
                                        //worksheet.Cells[i, j + 9] = new Cell(row.Cells[j + 11]?.Value?.ToString());
                                        //worksheet.Cells[i, j + 10] = new Cell(row.Cells[j + 12]?.Value?.ToString());

                                        worksheet.Cells.ColumnWidth[0, 1] = 3000;
                                        i += 1;
                                    }
                                    mconn.Close();
                                }

                                workbook.Worksheets.Add(worksheet);
                                workbook.Save(file);
                                Workbook book = Workbook.Load(file);
                                Worksheet sheet = book.Worksheets[0];

                                for (int rowIndex = sheet.Cells.FirstRowIndex; rowIndex <= sheet.Cells.LastRowIndex; rowIndex++)
                                {
                                    Row row = sheet.Cells.GetRow(rowIndex);
                                    for (int colIndex = row.FirstColIndex; colIndex <= row.LastColIndex; colIndex++)
                                    {
                                        Cell cell = row.GetCell(colIndex);
                                    }
                                }
                                MessageBox.Show("File successfully processed", "Sucessful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            conn.Open();
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "[dbo].[spUBTable_truncate]";
                            command.ExecuteNonQuery();
                            conn.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("File failed to processed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
                    
    }
}

