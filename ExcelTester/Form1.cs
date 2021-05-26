using System;
using System.Data;
using System.Windows.Forms;

namespace ExcelTester
{
    public partial class Form1 : Form
    {
        private string _connectionString = "";

        public Form1()
        {
            InitializeComponent();
        }

        public string ErrorInfo { get; set; }

        private void Form1_Load(object sender, EventArgs e)
        {
        }


        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            using (var oFile = new OpenFileDialog() { Filter = "Excel Worksheets 2007 (*.xlsx)|*.xlsx|Excel Worksheets 2003 (*.xls)|*.xls" })
            {
                if (oFile.ShowDialog() == DialogResult.OK)
                {
                   var fileName = oFile.FileName;
                    if (fileName.Length > 0)
                    {
                        string strPass = "";
                        if (fileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
                        {
                            _connectionString = String.Format("provider=Microsoft.ACE.OLEDB.12.0;data source={0};{1}Extended Properties=Excel 12.0;", fileName, strPass);
                        }
                        else
                        {
                            _connectionString = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};{1}Extended Properties=Excel 8.0;", fileName, strPass);
                        }

                        OpenExcelDataSource();
                    }
                }
            }
        }


        private void OpenExcelDataSource()
        {
            sheetToolStripComboBox.Items.Clear();
            sheetToolStripComboBox.Text = "Reading data...";

            if (_connectionString.Length > 1)
            {
                try
                {
                    using (var conn = new System.Data.OleDb.OleDbConnection(_connectionString))
                    {
                        conn.Open();
                        DataTable dtSheets = conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, null);

                        foreach (DataRow dr in dtSheets.Rows)
                        {
                            sheetToolStripComboBox.Items.Add(dr["TABLE_NAME"].ToString());
                        }

                        if (sheetToolStripComboBox.Items.Count > 0)
                        {
                            sheetToolStripComboBox.SelectedIndex = 0;
                        }

                        //if (dtSheets != null)
                        //{
                        //    if (dtSheets.Rows.Count == 1)
                        //    {
                        //        DataRow dr0 = dtSheets.Rows[0];
                        //        sheetName = dr0["TABLE_NAME"].ToString();
                        //    }
                        //    else if (dtSheets.Rows.Count > 0)
                        //    {
                        //        sheetName = SelectASheet(dtSheets);
                        //    }
                        //}

                    }
                }
                catch (Exception ex)
                {
                    //ErrorInfo = ex.Message;
                    //exitTimer.Start();
                    throw;
                }
            }
        }

        private void PopulateSheetData()
        {
            string sheetName = sheetToolStripComboBox.Text;
            if (sheetName.Length > 0 && _connectionString.Length > 0)
            {
                try
                {
                    using (var conn = new System.Data.OleDb.OleDbConnection(_connectionString))
                    {
                        conn.Open();

                        var cmd = new System.Data.OleDb.OleDbDataAdapter(String.Format("select * from [{0}]", sheetName), conn);
                        cmd.TableMappings.Add("Table", sheetName);
                        DataSet dtSet = new System.Data.DataSet();
                        cmd.Fill(dtSet);
                        if (dtSet != null)
                        {
                            dataGridView1.DataSource = dtSet.Tables[0];
                        }
                        else
                        {
                            //ErrorInfo = "No data available";
                        }

                        //if (dtSheets != null)
                        //{
                        //    if (dtSheets.Rows.Count == 1)
                        //    {
                        //        DataRow dr0 = dtSheets.Rows[0];
                        //        sheetName = dr0["TABLE_NAME"].ToString();
                        //    }
                        //    else if (dtSheets.Rows.Count > 0)
                        //    {
                        //        sheetName = SelectASheet(dtSheets);
                        //    }
                        //}

                    }
                }
                catch (Exception ex)
                {
                    //ErrorInfo = ex.Message;
                    //exitTimer.Start();
                    throw;
                }
            }
        }

        private void sheetToolStripComboBox_Click(object sender, EventArgs e)
        {

        }

        private void sheetToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateSheetData();
        }
    }
}