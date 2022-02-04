using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ExcelLibrary.SpreadSheet;
using ExcelLibrary.CompoundDocumentFormat;
using System.Data.SqlClient;
using System.IO;
using ExcelDataReader;
using DataTable = System.Data.DataTable;


namespace GSIS_TASK
{
    public partial class UC_Tab2 : UserControl
    {
        public UC_Tab2()
        {
            InitializeComponent();
        }
        private static string dbasefile = AppDomain.CurrentDomain.BaseDirectory + "\\dbFile";
        public static string connString;
        public static SqlConnection con = new SqlConnection(connString);
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
        private void UC_Tab2_Load(object sender, EventArgs e)
        {
            dtpFrom.Value = DateTime.Now;
            dtpTo.Value = DateTime.Now;
            connString = Read();
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
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    var command = conn.CreateCommand();
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "[UbpTbl].[dbo].[spUBTable_Select]";
                        command.Parameters.AddWithValue("@DTPFROM",DateTime.Now);
                        command.Parameters.AddWithValue("@DTPTO",DateTime.Now );
                        command.ExecuteNonQuery();
                        conn.Close();
                        try
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
                            worksheet.Cells[0, 11] = new Cell("DATE FROM");
                            worksheet.Cells[0, 12] = new Cell("DATE TO");
                            int j = 0;
                            int i = 1;
                            DataTable dt = new DataTable();
                            foreach (DataRowView row in dt.Rows)
                            {
                                worksheet.Cells[i, j] = new Cell(row.Row.ItemArray[j].ToString());
                                worksheet.Cells[i, j + 1] = new Cell(row.Row.ItemArray[j + 1].ToString());
                                worksheet.Cells[i, j + 2] = new Cell(row.Row.ItemArray[j + 2].ToString());
                                worksheet.Cells[i, j + 3] = new Cell(row.Row.ItemArray[j + 3].ToString());
                                worksheet.Cells[i, j + 4] = new Cell(row.Row.ItemArray[j + 4].ToString());
                                worksheet.Cells[i, j + 5] = new Cell(row.Row.ItemArray[j + 5].ToString());
                                worksheet.Cells[i, j + 6] = new Cell(row.Row.ItemArray[j + 6].ToString());
                                worksheet.Cells[i, j + 7] = new Cell(row.Row.ItemArray[j + 7].ToString());
                                worksheet.Cells[i, j + 8] = new Cell(row.Row.ItemArray[j + 8].ToString());
                                worksheet.Cells[i, j + 9] = new Cell(row.Row.ItemArray[j + 9].ToString());
                                worksheet.Cells[i, j + 10] = new Cell(row.Row.ItemArray[j + 10].ToString());
                                worksheet.Cells[i, j + 11] = new Cell(row.Row.ItemArray[j + 11].ToString());
                                worksheet.Cells[i, j + 12] = new Cell(row.Row.ItemArray[j + 12].ToString());
                                worksheet.Cells[i, j + 13] = new Cell(row.Row.ItemArray[j + 13].ToString());
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
                            MessageBox.Show("Exported");
                        }
                        catch
                        {
                            MessageBox.Show("Export Failed");
                        }
                    }
                }
            }
        }
    }
}
