using System;
using System.Data;
using System.Reflection;
using System.Windows.Forms;

namespace OctofyExp
{
    public partial class ExcelSheetsForm : Form
    {
        private string _connectionString = "";  // connection string to open the Excel file

        public ExcelSheetsForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The Excel file name to analysis
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Analysis button click event handle: start analysis selected sheet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnalysisButton_Click(object sender, EventArgs e)
        {
            try
            {
                string sheetName = sheetsListBox.SelectedItem.ToString();

                using (var dlg = new DataLoaderForm()
                {
                    ConnectionString = _connectionString,
                    DataSourceType = DataLoaderForm.DataSourceTypesEnum.ExcelFile,
                    TableName = sheetName,
                    TableSelectSQL = string.Format(String.Format("select * from [{0}]", sheetName))
                })
                {
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        if (dlg.ErrorInfo.Length == 0)
                        {
                            using (var frm = new DataAnalysisForm()
                            {
                                TableName = sheetName,
                                DataSource = dlg.DataSource
                            })
                            {
                                _ = frm.ShowDialog();
                            }
                        }
                        else
                        {
                            MessageBox.Show(dlg.ErrorInfo, Properties.Resources.A007,
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (ObjectDisposedException)
            {
                // ignore
            }
            catch (TargetInvocationException)
            { }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Properties.Resources.A008, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Close button click event handle: Close the dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Form load event handle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExcelSheetsForm_Load(object sender, EventArgs e)
        {
            string strPass = "";
            if (FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                _connectionString = String.Format("provider=Microsoft.ACE.OLEDB.12.0;data source={0};{1}Extended Properties=Excel 12.0;", FileName, strPass);
            }
            else
            {
                _connectionString = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};{1}Extended Properties=Excel 8.0;", FileName, strPass);
            }
            startTimer.Start();
        }

        /// <summary>
        /// start timer tick event handle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartTimer_Tick(object sender, EventArgs e)
        {
            startTimer.Stop();

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
                            sheetsListBox.Items.Add(dr["TABLE_NAME"].ToString());
                        }

                        if (sheetsListBox.Items.Count > 0)
                        {
                            analysisButton.Enabled = true;
                        }
                        infoToolStripStatusLabel.Text = System.IO.Path.GetFileName(FileName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Properties.Resources.A005, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    //throw;
                }
            }
        }
    }
}