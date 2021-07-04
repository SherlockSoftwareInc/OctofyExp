using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OctofyExp
{
    public partial class PreviewDataForm : Form
    {
        /// <summary>
        /// Maximum length of binary data to display (display is truncated after this length)
        /// </summary>
        private const int maxBinaryDisplayString = 8000;

        private int _activeColumn = -1;

        private DataTable _tableSchema;

        public PreviewDataForm()
        {
            InitializeComponent();
        }

        public string Columns { get; set; }
        public string ConnectionString { get; set; } = "";

        /// <summary>
        /// List columns that are excluded from the query statement
        /// (some data type is not able to display in the DataGridView,
        /// ie. image. These columns are excluded. However, as the
        /// information, use should acknowledged which columns have been
        /// excluded)
        /// </summary>
        public List<string> ExcludedColumns { get; set; }

        public string TableName { get; set; } = "";

        private List<string> BinaryColumns()
        {
            var result = new List<string>();
            for (int i = 0; i < _tableSchema.Rows.Count; i++)
            {
                DataRow dr = _tableSchema.Rows[i];
                int dataType = Convert.ToInt32(dr["ProviderType"]);
                switch (dataType)
                {
                    case 1:
                    case 7:
                    case 19:
                    case 21:
                        result.Add(dr["ColumnName"].ToString());
                        break;

                    default:
                        break;
                }
            }

            return result;
        }

        /// <summary>
        /// Converts binary data column to a string equivalent, including handling of null columns
        /// </summary>
        /// <param name="hexBuilder">String builder pre-allocated for maximum space needed</param>
        /// <param name="columnValue">Column value, expected to be of type byte []</param>
        /// <returns>String representation of column value</returns>
        private string BinaryDataColumnToString(StringBuilder hexBuilder, object columnValue)
        {
            const string hexChars = "0123456789ABCDEF";
            if (columnValue == DBNull.Value)
            {
                // Return special "(null)" value here for null column values
                return "(null)";
            }
            else
            {
                // Otherwise return hex representation
                byte[] byteArray = (byte[])columnValue;
                int displayLength = (byteArray.Length > maxBinaryDisplayString) ? maxBinaryDisplayString : byteArray.Length;
                hexBuilder.Length = 0;
                hexBuilder.Append("0x");
                for (int i = 0; i < displayLength; i++)
                {
                    hexBuilder.Append(hexChars[(int)byteArray[i] >> 4]);
                    hexBuilder.Append(hexChars[(int)byteArray[i] % 0x10]);
                }
                return hexBuilder.ToString();
            }
        }

        /// <summary>
        /// Build columns section of the SELECT statement
        /// </summary>
        private void BuildColumnsList()
        {
            if (_tableSchema != null)
            {
                var columns = new StringBuilder();
                for (int i = 0; i < _tableSchema.Rows.Count; i++)
                {
                    DataRow dr = _tableSchema.Rows[i];
                    string columnName = dr["ColumnName"].ToString();
                    int dataType = Convert.ToInt32(dr["ProviderType"]);
                    string quoteName;
                    switch (dataType)
                    {
                        case 29:
                            quoteName = string.Format("CAST({0} AS varchar(MAX)) AS [{0}]", columnName);
                            break;

                        case 19:    //timestamp
                            quoteName = string.Format("CAST({0} AS varbinary(10)) AS [{0}]", columnName);
                            break;

                        default:
                            quoteName = string.Format("[{0}]", columnName);
                            break;
                    }

                    if (i == 0)
                    {
                        columns.Append(quoteName);
                    }
                    else
                    {
                        columns.Append(", ").Append(quoteName);
                    }
                }

                Columns = columns.ToString();
                ExcludedColumns?.Clear();
            }
        }

        private void CloseToolStripButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            _activeColumn = e.ColumnIndex;
        }

        /// <summary>
        /// Read data and populating
        /// </summary>
        private void FetchAndPopulateData()
        {
            string sql;
            int numOfRows;
            switch (previewTypeToolStripComboBox.SelectedIndex)
            {
                case 0:
                    sql = String.Format("SELECT TOP 1000 {0} FROM {1} WITH (NOLOCK)", Columns, QuoteTableName());
                    numOfRows = 1000;
                    break;

                case 1:
                    sql = String.Format("SELECT TOP 10000 {0} FROM {1} WITH (NOLOCK)", Columns, QuoteTableName());
                    numOfRows = 10000;
                    break;

                default:
                    sql = String.Format("SELECT {0} FROM {1} WITH (NOLOCK)", Columns, QuoteTableName());
                    numOfRows = -1;
                    break;
            }

            if (sql.Length > 0)
            {
                using (var dlg = new DataLoaderForm()
                {
                    ConnectionString = ConnectionString,
                    DataSourceType = DataLoaderForm.DataSourceTypesEnum.SQLServerDataTable,
                    TableName = QuoteTableName(),
                    NumOfTopRows = numOfRows,
                    TableSelectSQL = sql
                })
                {
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        if (dlg.ErrorInfo.Length == 0)
                        {
                            var data = dlg.DataSource;
                            FixBinaryColumnsForDisplay(data);
                            dataGrid.DataSource = data;
                            toolStripStatusLabel1.Text = String.Format(Properties.Resources.A043, data.Rows.Count.ToString("N0")); //A043: Rows: {0}

                            ShowExcludedColumns();
                        }
                    }
                }
            }
            else
            {
                Console.Beep();
            }
        }

        /// <summary>
        /// Accepts datatable and converts all binary columns into textual representation of a binary column
        /// For use when display binary columns in a DataGridView
        /// </summary>
        /// <param name="t">Input data table</param>
        /// <returns>Updated data table, with binary columns replaced</returns>
        private DataTable FixBinaryColumnsForDisplay(DataTable t)
        {
            //List<string> binaryColumnNames = t.Columns.Cast<DataColumn>().Where(col => col.DataType.Equals(typeof(byte[]))).Select(col => col.ColumnName).ToList();
            foreach (string binaryColumnName in BinaryColumns())
            {
                // Create temporary column to copy over data
                string tempColumnName = "C" + Guid.NewGuid().ToString();
                t.Columns.Add(new DataColumn(tempColumnName, typeof(string)));
                t.Columns[tempColumnName].SetOrdinal(t.Columns[binaryColumnName].Ordinal);

                // Replace values in every row
                StringBuilder hexBuilder = new StringBuilder((maxBinaryDisplayString * 2) + 2);
                foreach (DataRow r in t.Rows)
                {
                    r[tempColumnName] = BinaryDataColumnToString(hexBuilder, r[binaryColumnName]);
                }

                t.Columns.Remove(binaryColumnName);
                t.Columns[tempColumnName].ColumnName = binaryColumnName;
            }
            return t;
        }

        /// <summary>
        /// Get schema of the table
        /// </summary>
        private void GetTableSchema()
        {
            string sql = String.Format("SELECT * FROM {0} WHERE 0=1", QuoteTableName());

            if (ConnectionString.Length > 0)
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        conn.Open();

                        using (var cmd = new SqlCommand(sql, conn)
                        {
                            CommandType = CommandType.Text
                        })
                        {
                            var reader = cmd.ExecuteReader();
                            _tableSchema = reader.GetSchemaTable();
                        }
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }
                    }
                }
            }
        }

        private void PreviewDataForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.PreviewDataFormState = (short)this.WindowState;
            if (WindowState != FormWindowState.Minimized)
            {
                if (WindowState == FormWindowState.Normal)
                {
                    Properties.Settings.Default.PreviewDataFormLocation = this.Location;
                    Properties.Settings.Default.PreviewDataFormSize = this.Size;
                }
                var s = Screen.FromControl(this);
                Properties.Settings.Default.PreviewDataFormBounds = s.Bounds.Location;
            }
        }

        private void PreviewDataForm_Load(object sender, EventArgs e)
        {
            var winLoc = Properties.Settings.Default.PreviewDataFormLocation;
            if (winLoc.X >= Screen.PrimaryScreen.WorkingArea.Width || winLoc.X <= -Screen.PrimaryScreen.WorkingArea.Width)
            {
                winLoc.X = 0;
            }

            if (winLoc.Y >= Screen.PrimaryScreen.WorkingArea.Height || winLoc.Y <= -Screen.PrimaryScreen.WorkingArea.Height)
            {
                winLoc.Y = 0;
            }
            Point screenBounds = Properties.Settings.Default.PreviewDataFormBounds;
            if (screenBounds.X != 0 || screenBounds.Y != 0)
            {
                foreach (var s in Screen.AllScreens)
                {
                    if (s.Bounds.X == screenBounds.X && s.Bounds.Y == screenBounds.Y)
                    {
                        winLoc = new Point(winLoc.X + screenBounds.X, winLoc.Y + screenBounds.Y);
                    }
                }
            }
            Location = winLoc;

            this.Size = Properties.Settings.Default.PreviewDataFormSize;
            this.WindowState = (FormWindowState)Properties.Settings.Default.PreviewDataFormState;
            if (!Module1.IsOnScreen(this))
            {
                Location = new Point(0, 0);
            }

            Text = String.Format(Properties.Resources.A042, TableName); //A042: Preview Data - {0}
            GetTableSchema();
            BuildColumnsList();
            previewTypeToolStripComboBox.SelectedIndex = 0;
        }

        private void PreviewTypeToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((TableName ?? "").Length == 0 && (Columns ?? "").Length == 0)
            {
                MessageBox.Show(Properties.Resources.A045, Properties.Resources.A046,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //A045: No table or view specified.
                //A046: No Object to view
                Close();
            }
            else
            {
                FetchAndPopulateData();
            }
        }

        private string QuoteTableName()
        {
            if (TableName.StartsWith("[") && TableName.EndsWith("]"))
            {
                return TableName;
            }
            else
            {
                string[] names = TableName.Split('.');
                string quotedName = "";
                for (int i = 0; i < names.Length; i++)
                {
                    if (i == 0)
                        quotedName = string.Format("[{0}]", names[i]);
                    else
                        quotedName += string.Format(".[{0}]", names[i]);
                }
                return quotedName;
            }
        }

        /// <summary>
        /// Show message to tell use that there are excluded columns
        /// </summary>
        private void ShowExcludedColumns()
        {
            if (ExcludedColumns != null)
            {
                if (ExcludedColumns.Count > 0)
                {
                    string message = string.Format(Properties.Resources.A044, TableName);   //A044: Following column(s) in {0} have been excluded:
                    foreach (var column in ExcludedColumns)
                    {
                        message += "\r\t" + column;
                    }
                    MessageBox.Show(message, Properties.Resources.A015, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        ///// <summary>
        ///// Handle cell formatting
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void DataGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        //{
        //    if (e.Value != null)
        //    {
        //        try
        //        {
        //            string columnName = dataGrid.Columns[e.ColumnIndex].Name;
        //            if (IsBinaryColumn(columnName, e.ColumnIndex))
        //            {
        //                e.Value = 0;
        //                e.FormattingApplied = true;
        //            }
        //        }
        //        catch (FormatException)
        //        {
        //            e.FormattingApplied = false;
        //            //throw;
        //        }
        //    }
        //}

        //private Byte[] hexStringToByteArray(String inString, DataGridViewCell cell)
        //{
        //    List<Byte> outArray = new List<Byte>();

        //    String[] list = inString.Split(new char[1] { ' ' }, 250, System.StringSplitOptions.RemoveEmptyEntries);

        //    for (int i = 0; i < list.Length; i++)
        //    {
        //        String bString = list[i];
        //        try
        //        {
        //            Byte bByte = Byte.Parse(bString, System.Globalization.NumberStyles.AllowHexSpecifier);

        //            Console.WriteLine("Parsing {0}: {1} -> {0}", i, bString, bByte);

        //            //txData.Add(bByte);
        //            outArray.Insert(i, bByte);
        //        }
        //        catch (OverflowException)
        //        {
        //            Console.WriteLine("Overflow parsing byte {0} -> {1}", i, bString);
        //            cell.ErrorText = string.Format("Invalid hex byte value '{0}' encountered while parsing.", bString);
        //            //ctl.Select();
        //            //ctl.Select(Text.IndexOf(bString, 0), bString.Length);

        //            break;
        //        }
        //        catch (FormatException)
        //        {
        //            Console.WriteLine("Format error parsing byte {0} -> {1}", i, bString);
        //            cell.ErrorText = string.Format("Invalid hex byte value '{0}' encountered while parsing.", bString);
        //            //ctl.Select();
        //            //ctl.Select(Text.IndexOf(bString, 0), bString.Length);

        //            break;
        //        }
        //    }
        //    return outArray.ToArray();
        //}

        //private string ByteArrayToString(byte[] ba)
        //{
        //    var hex = new System.Text.StringBuilder(ba.Length * 2);
        //    foreach (byte b in ba)
        //        hex.AppendFormat("{0:x2}", b);
        //    return hex.ToString();
        //}

        ///// <summary>
        ///// Checks if specified column is a binary data column
        ///// </summary>
        ///// <param name="columnIndex"></param>
        ///// <returns></returns>
        //private bool IsBinaryColumn(string columnName, int columnIndex)
        //{
        //    bool result = false;

        //    if (_tableSchema != null)
        //    {
        //        if (columnIndex >= 0 && columnIndex < _tableSchema.Rows.Count)
        //        {
        //            DataRow dr = _tableSchema.Rows[columnIndex];
        //            if (dr["columnName"].ToString() == columnName)
        //            {
        //                int dataType = Convert.ToInt32(dr["ProviderType"]);
        //                if (dataType == 21 || dataType == 29)
        //                {
        //                    result = true;
        //                }
        //            }
        //        }
        //    }

        //    return result;
        //}
    }
}
