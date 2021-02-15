using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace OctofyExp
{
    public partial class ColumnDefView : UserControl
    {
        public event EventHandler OnColumnFrequency;
        public event EventHandler SelectedColumnChanged;

        public ColumnDefView()
        {
            InitializeComponent();
        }

        private class TableColumnItem
        {
            public TableColumnItem(SqlDataReader dr)
            {
                this.No = (int)dr["ORDINAL_POSITION"];
                this.ColumnName = dr["COLUMN_NAME"].ToString();
                string dtType = dr["DATA_TYPE"].ToString();
                this.DataType = dtType;
                string strMaxLength = dr["CHARACTER_MAXIMUM_LENGTH"].ToString();
                if (strMaxLength.Length > 0)
                    if (string.Compare(dtType, "text", true) == 0 ||
                        string.Compare(dtType, "ntext", true) == 0 ||
                        string.Compare(dtType, "image", true) == 0 ||
                        string.Compare(dtType, "xml", true) == 0)
                    {
                        this.DataType = dtType;
                    }
                    else
                    {
                        if (strMaxLength.IsNumeric())
                        {
                            int maxLength = Int32.Parse(strMaxLength);
                            if (maxLength > 0)
                                this.DataType = string.Format("{0}({1})", dtType, strMaxLength);
                        }
                        else
                            this.DataType = dtType;
                    }
                else
                    this.DataType = dtType;

                this.Nullable = (dr["IS_NULLABLE"].ToString() == "YES");
            }
            public int No { get; set; }
            public string ColumnName { get; set; }
            public string DataType { get; set; }
            public bool Nullable { get; set; }
        }

        private string _tableSchema = "";
        private string _tableName = "";
        private readonly List<TableColumnItem> _columns = new List<TableColumnItem>();

        public string ConnectionString { get; set; } = "";

        public string ObjectName { get; set; } = "";

        public string SelectedColumn { get; private set; }

        public void Clear()
        {
            ConnectionString = "";
            tablenameLabel.Text = "";
            if (columnDefDataGridView.DataSource != null)
                columnDefDataGridView.DataSource = null;
        }

        /// <summary>
        /// Open database object and populate its columns' info
        /// </summary>
        /// <param name="objectName"></param>
        /// <param name="objectType"></param>
        /// <param name="searchFor"></param>
        public void Open(string objectName, string searchFor, bool columnSearch = false)
        {
            if (ObjectName == objectName)
                return;

            SelectedColumn = "";
            SelectedColumnChanged?.Invoke(this, new EventArgs());

            ObjectName = objectName;
            tablenameLabel.Text = objectName;
            if (objectName.Length == 0)
            {
                if (columnDefDataGridView.DataSource != null)
                    columnDefDataGridView.DataSource = null;
                return;
            }


            if (objectName.IndexOf(".") > 0)
            {
                var values = objectName.Split('.');
                _tableSchema = values[0];
                _tableName = values[1];
            }
            else
            {
                _tableSchema = "dbo";
                _tableName = objectName;
            }

            columnDefDataGridView.DataSource = null;
            columnDefDataGridView.Visible = false;
            columnDefDataGridView.Columns.Clear();

            if (ConnectionString.Length > 0 && _tableName.Length > 0)
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand() { Connection = conn, CommandType = System.Data.CommandType.Text })
                        {
                            cmd.Parameters.Add(new SqlParameter("@Schema", _tableSchema));
                            cmd.Parameters.Add(new SqlParameter("@TableName", _tableName));
                            cmd.CommandText = "SELECT ORDINAL_POSITION,COLUMN_NAME,DATA_TYPE,CHARACTER_MAXIMUM_LENGTH,NUMERIC_PRECISION,IS_NULLABLE,COLUMN_DEFAULT FROM information_schema.columns WHERE TABLE_SCHEMA = @Schema AND TABLE_NAME = @TableName ORDER BY ORDINAL_POSITION";
                            conn.Open();

                            _columns.Clear();
                            var dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                _columns.Add(new TableColumnItem(dr));
                            }
                            dr.Close();

                            columnDefDataGridView.DataSource = _columns;
                            columnDefDataGridView.AutoResizeColumns();

                            //if (columnDefDataGridView.RowCount > 0)
                            //    HighlightMatches(searchFor);

                            columnDefDataGridView.Visible = true;

                            if (columnSearch)
                            {
                                HighlightMatches(searchFor);
                            }
                        }
                    }
                    catch (SqlException)
                    {
                        return;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            else
            {
                if (columnDefDataGridView.DataSource != null)
                    columnDefDataGridView.DataSource = null;
            }
        }

        /// <summary>
        /// Handle copy menu item click event:
        ///     Copy column name to the clipboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (columnDefDataGridView.RowCount > 0 && columnDefDataGridView.CurrentRow != null)
            {
                int rowIndex = columnDefDataGridView.CurrentRow.Index;
                if (rowIndex >= 0)
                {
                    string columnName = columnDefDataGridView.Rows[rowIndex].Cells["ColumnName"].Value.ToString();
                    if (columnName.Length > 0)
                        Clipboard.SetText(columnName);
                }
            }
        }


        private void FrequcneyToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            ShowColumnFrequence();
        }


        /// <summary>
        /// 
        /// </summary>
        private void ShowColumnFrequence()
        {
            if (columnDefDataGridView.CurrentRow != null)
            {
                int rowIndex = columnDefDataGridView.CurrentRow.Index;
                if (rowIndex >= 0)
                {
                    string dataType = columnDefDataGridView.Rows[rowIndex].Cells["DataType"].Value.ToString().ToLower();
                    if (dataType.IndexOf("image") >= 0 || dataType.IndexOf("binary") >= 0)
                    {
                        MessageBox.Show(Properties.Resources.A054, Properties.Resources.A055,
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //A054: The system cannot calculate the frequencies on a binary column.
                        //A055: Data type not support
                    }
                    else if (dataType == "text" || dataType == "ntext")
                    {
                        MessageBox.Show(Properties.Resources.A056, Properties.Resources.A055,
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //A056: The system cannot calculate the frequencies on a text column.
                    }
                    else if (!IsCategoricalColumn(dataType))
                    {
                        MessageBox.Show(Properties.Resources.A077, Properties.Resources.A055,
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //A077: The system cannot calculate the frequencies for this column.
                        //A055: Data type not support
                    }
                    //else if (dataType == "xml")
                    //{
                    //    MessageBox.Show("The system cannot calculate the frequencies on a xml column.", "Data type not support", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //}
                    else
                    {
                        SelectedColumn = columnDefDataGridView.Rows[rowIndex].Cells["ColumnName"].Value.ToString();

                        if (SelectedColumn.Length > 0)
                        {

                            OnColumnFrequency?.Invoke(this, new EventArgs());
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Build a safe SQL statement with only selecte categorical columns
        /// </summary>
        /// <param name="numOfTop"></param>
        /// <param name="includeTextColumn"></param>
        /// <returns></returns>
        public string SafeSQL(int numOfTop, bool includeTextColumn = false)
        {
            string columns = CategoricalColumns(includeTextColumn);
            if (columns.Length > 0)
            {
                string table = string.Format("[{0}].[{1}]", _tableSchema, _tableName);
                if (numOfTop > 0)
                    return string.Format("SELECT TOP {0} {1} FROM {2} WITH (NOLOCK)", numOfTop, columns, table);
                else
                    return string.Format("SELECT {0} FROM {1} WITH (NOLOCK)", columns, table);
            }

            return "";
        }

        /// <summary>
        /// Result SELECT script with selected columns
        /// </summary>
        /// <returns></returns>
        public string SQLWithSelectedColumns()
        {
            string result = "";
            List<string> selectableColumns = new List<string>();
            for (int i = 0; i < columnDefDataGridView.Rows.Count; i++)
            {
                if (IsCategoricalColumn(i, true))
                {
                    selectableColumns.Add(columnDefDataGridView.Rows[i].Cells["ColumnName"].Value.ToString());
                }
            }

            using (ColumnsSelecteForm dlg = new ColumnsSelecteForm() { Columns = selectableColumns })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    string table = string.Format("[{0}].[{1}]", _tableSchema, _tableName);
                    string columns = "";
                    foreach (var column in dlg.SelectedColumns)
                    {
                        if (columns.Length == 0)
                        {
                            columns = string.Format("[{0}]", column);
                        }
                        else
                        {
                            columns += string.Format(", [{0}]", column);
                        }
                    }
                    result = string.Format("SELECT {0} FROM {1} WITH (NOLOCK)", columns, table);
                }
            }

            return result;
        }

        public List<string> ExcludedColumns { get; set; } = new List<string>();

        /// <summary>
        /// Get categorical columns of the table
        /// </summary>
        /// <param name="includeTextColumn"></param>
        /// <returns></returns>
        public string CategoricalColumns(bool includeTextColumn = false)
        {
            bool firstColumn = true;
            string result = "";
            ExcludedColumns.Clear();

            for (int i = 0; i < columnDefDataGridView.Rows.Count; i++)
            {
                if (IsCategoricalColumn(i, includeTextColumn))
                {
                    string columnName = columnDefDataGridView.Rows[i].Cells["ColumnName"].Value.ToString();
                    if (firstColumn)
                    {
                        result += "[" + columnName + "]";
                        firstColumn = false;
                    }
                    else
                    {
                        result += ", [" + columnName + "]";
                    }
                }
                else
                {
                    ExcludedColumns.Add(columnDefDataGridView.Rows[i].Cells["ColumnName"].Value.ToString());
                }
            }
            return result;
        }

        /// <summary>
        /// Checks if the specified column a categorical column
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <param name="includeTextColumn"></param>
        /// <returns></returns>
        private bool IsCategoricalColumn(int columnIndex, bool includeTextColumn = false)
        {
            string dataType = columnDefDataGridView.Rows[columnIndex].Cells["DataType"].Value.ToString().ToLower();
            if (!includeTextColumn)
            {
                if (dataType == "text" || dataType == "ntext")
                {
                    return false;
                }
                else
                    return IsCategoricalColumn(dataType);
            }
            return IsCategoricalColumn(dataType);

        }

        /// <summary>
        /// Checks if the specified data type is a categorical column
        /// </summary>
        /// <param name="dataType"></param>
        /// <returns></returns>
        private bool IsCategoricalColumn(string dataType)
        {
            if (dataType.IndexOf("image") >= 0 || dataType.IndexOf("binary") >= 0)
            {
                return false;
            }
            else if (dataType.StartsWith("geo"))
            {
                return false;
            }
            else if (dataType == "timestamp" || dataType == "cursor" || dataType == "rowversion" || dataType == "hierarchyid" || dataType == "sql_variant" || dataType == "uniqueidentifier" || dataType == "table")
            {
                return false;
            }
            //else if (dataType == "xml")
            //{
            //    return false;
            //}
            //else
            //{
            //    //List<string> excludeDataTypes = new List<string>(new string[] { "cursor", "rowversion", "hierarchyid", "sql_variant", "uniqueidentifier", "table", "geography" });
            //    //if (excludeDataTypes.Contains(dataType.ToLower()))
            //    //{
            //    //    return false;
            //    //}
            //    //else
            //    //{
            //    if (!includeTextColumn)
            //    {
            //        if (dataType == "text" || dataType == "ntext")
            //        {
            //            return false;
            //        }
            //    }
            //    //}
            //}
            return true;
        }

        /// <summary>
        /// Handle copy selections menu item click event:
        ///     Copy the selections of data grid to the clipboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopySelectionToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            CopySelections();
        }

        public void CopySelections()
        {
            if (columnDefDataGridView.RowCount > 0)
            {
                Clipboard.SetDataObject(columnDefDataGridView.GetClipboardContent());
            }
        }

        /// <summary>
        /// Handle data grid cell double click event:
        ///     Show column frequency
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ColumnDefDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ShowColumnFrequence();
        }

        /// <summary>
        /// Handle data grid cell click event:
        ///     Tell host that selected column changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ColumnDefDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (columnDefDataGridView.SelectedRows != null)
            {
                int rowIndex = columnDefDataGridView.CurrentCell.RowIndex;
                string columnName = columnDefDataGridView.Rows[rowIndex].Cells["ColumnName"].Value.ToString();
                if (columnName != SelectedColumn)
                {
                    SelectedColumn = columnName;
                    SelectedColumnChanged?.Invoke(this, new EventArgs());
                }
            }
        }

        /// <summary>
        /// Hight matched column
        /// </summary>
        /// <param name="searchfor"></param>
        private void HighlightMatches(string searchfor)
        {
            if (searchfor.Length > 0)
            {
                for (int i = 0; i < columnDefDataGridView.Rows.Count; i++)
                {
                    string strvalue = columnDefDataGridView.Rows[i].Cells["ColumnName"].Value.ToString();
                    if (strvalue.IndexOf(searchfor, 0, System.StringComparison.CurrentCultureIgnoreCase) > -1)
                    {
                        columnDefDataGridView.Rows[i].Cells["ColumnName"].Style.BackColor = System.Drawing.Color.LightSkyBlue;
                    }
                }
            }
        }

    }
}
