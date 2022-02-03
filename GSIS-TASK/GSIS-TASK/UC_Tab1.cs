using System;
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
               

                   
                        string file = AppDomain.CurrentDomain.BaseDirectory + "Exported_with_Date_Range_" + DateTime.Now.ToString("MM-dd-yyy") + ".xls";
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
                        worksheet.Cells[0, 9] = new Cell("STATUS 1");
                        worksheet.Cells[0, 10] = new Cell("STATUS 2");                        
                  
                        int j = 0;
                        int i = 1;

                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            //MessageBox.Show(row.Cells[j].Value.ToString());
                            worksheet.Cells[i, j] = new Cell(row.Cells[j]?.Value?.ToString());
                            worksheet.Cells[i, j + 1] = new Cell(row.Cells[j + 1]?.Value?.ToString());
                            worksheet.Cells[i, j + 2] = new Cell(row.Cells[j + 2]?.Value?.ToString()); 
                            worksheet.Cells[i, j + 3] = new Cell(row.Cells[j + 3]?.Value?.ToString());
                            worksheet.Cells[i, j + 4] = new Cell(row.Cells[j + 4]?.Value?.ToString());
                            worksheet.Cells[i, j + 5] = new Cell(row.Cells[j + 5]?.Value?.ToString()); 
                            worksheet.Cells[i, j + 6] = new Cell(row.Cells[j + 6]?.Value?.ToString());
                            worksheet.Cells[i, j + 7] = new Cell(row.Cells[j + 7]?.Value?.ToString());
                            worksheet.Cells[i, j + 8] = new Cell(row.Cells[j + 8]?.Value?.ToString());
                            worksheet.Cells[i, j + 9] = new Cell(row.Cells[j + 11]?.Value?.ToString());
                            worksheet.Cells[i, j + 10] = new Cell(row.Cells[j + 12]?.Value?.ToString());

                            worksheet.Cells.ColumnWidth[0, 1] = 3000; 
                            i += 1;
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
