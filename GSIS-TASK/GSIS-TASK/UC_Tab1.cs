using System;
using System.Windows;
using System.Data;
using System.Globalization;
using Microsoft.Office.Interop.Excel;

using ExcelLibrary.SpreadSheet;
using Workbook = ExcelLibrary.SpreadSheet.Workbook;
using Worksheet = ExcelLibrary.SpreadSheet.Worksheet;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using ExcelDataReader;
using DataTable = System.Data.DataTable;
using ClosedXML.Excel;
using System.Collections.Generic;

namespace GSIS_TASK
{
    public partial class UC_Tab1 : UserControl
    {
        DataTableCollection tableCollection;
        private static string dbasefile = AppDomain.CurrentDomain.BaseDirectory + "\\dbFile";
        public static string connString;
        public static SqlConnection con = new SqlConnection(connString);
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

                            dataGridView1.Columns[9].HeaderText = "DONE";
                            dataGridView1.Columns[10].HeaderText = "CHECKING";



                        }
                        AddARow();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }           
           
        }
        private void AddARow()
        {
            DataTable dtable = (DataTable)dataGridView1.DataSource;
            dtable.Columns.RemoveAt(11);
            dtable.Columns.Add(new DataColumn("STATUS 1"));
            dtable.Columns.Add(new DataColumn("STATUS 2"));
           
            dataGridView1.DataSource = dtable;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Cells["STATUS 1"].Value = "KM";
                row.Cells["STATUS 2"].Value = "KM";

            }
            
            dtable.AcceptChanges();


        }

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

        private void UC_Tab1_Load(object sender, EventArgs e)
        {
            InitializeFile();
            connString = Read();
        }

        private void backgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {

        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Do you want to import an excel file?", "Import?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
               
                if (result == DialogResult.Yes)
                {
                    DataTable dt = new DataTable();

                    //Adding the Columns
                    foreach (DataGridViewColumn column in dataGridView1.Columns)
                    {
                        dt.Columns.Add(column.HeaderText, column.ValueType);
                    }

                    //Adding the Rows
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        dt.Rows.Add();
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = cell.Value.ToString();
                        }
                    }

                    //Exporting to Excel
                    string folderPath = "C:\\Excel\\";
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, "GSIS");
                        wb.SaveAs(folderPath + "GSIS_Data.xlsx");
                    }

                    MessageBox.Show("Succesfully ", "Import Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in import file: " + ex.Message);

            }
        }
    }
}
