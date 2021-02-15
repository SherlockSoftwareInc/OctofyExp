using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace OctofyExp
{
    public partial class PreviewDataForm : Form
    {
        public PreviewDataForm()
        {
            InitializeComponent();
        }

        private int _activeColumn = -1;

        public string ConnectionString { get; set; } = "";

        public string TableName { get; set; } = "";

        public string Columns { get; set; }

        /// <summary>
        /// List columns that are excluded from the query statement
        /// (some data type is not able to display in the DataGridView, 
        /// ie. image. These columns are excluded. However, as the 
        /// information, use should acknowledged which columns have been
        /// excluded)
        /// </summary>
        public List<string> ExcludedColumns { get; set; }

        private void CloseToolStripButton_Click(object sender, EventArgs e)
        {
            Close();
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
                Location = new Point(0, 0);

            Text = String.Format(Properties.Resources.A042, TableName); //A042: Preview Data - {0}
            previewTypeToolStripComboBox.SelectedIndex = 0;
        }

        private void FetchAndPopulateData()
        {
            string sql = BuildSQL();

            if (sql.Length > 0)
            {
                Cursor = Cursors.WaitCursor;
                toolStripStatusLabel1.Text = Properties.Resources.A011; //A011: Loading data...
                if (dataGrid.DataSource != null)
                {
                    dataGrid.DataSource = null;
                }
                Application.DoEvents();

                using (var conn = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        using (var cmd = new SqlCommand(sql, conn) { CommandType = CommandType.Text })
                        {
                            conn.Open();
                            var dr = cmd.ExecuteReader();
                            var data = new DataTable();
                            data.Load(dr);
                            dr.Close();

                            if (data != null)
                            {
                                dataGrid.DataSource = data;
                                toolStripStatusLabel1.Text = String.Format(Properties.Resources.A043, data.Rows.Count); //A043: Rows: {0}

                                ShowExcludedColumns();
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message, Properties.Resources.A013, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }
                        Cursor = Cursors.Default;
                    }
                }
            }
            else
            {
                Console.Beep();
            }
        }

        private string BuildSQL()
        {
            switch (previewTypeToolStripComboBox.SelectedIndex)
            {
                case 0:
                    return String.Format("SELECT TOP 1000 {0} FROM {1}", Columns, QuoteTableName());

                case 1:
                    return String.Format("SELECT TOP 10000 {0} FROM {1}", Columns, QuoteTableName());

                default:
                    return String.Format("SELECT {0} FROM {1}", Columns, QuoteTableName());
            }
        }

        private string QuoteTableName()
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

        /// <summary>
        /// Show message to tell use that there are excluded columns
        /// </summary>
        private void ShowExcludedColumns()
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

        private void DataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            _activeColumn = e.ColumnIndex;
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
                FetchAndPopulateData();
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

        //private string GetActiveColumnName()
        //{
        //    string colName = "";
        //    try
        //    {
        //        if (_activeColumn >= 0)
        //        {
        //            colName = dataGrid.Columns[_activeColumn].DataPropertyName;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        //ignore ...
        //    }
        //    return colName;
        //}

        //private void CopyColumnNameToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    Clipboard.SetText(GetActiveColumnName());
        //}

        //private void FrequencyToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    string columnName = GetActiveColumnName();
        //    if (columnName.Length > 0 && TableName.Length > 0)
        //    {
        //        //show column frequency
        //    }
        //}


        //private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    var columnName = GetActiveColumnName();
        //    if (columnName.Length > 0 && e.RowIndex >= 0)
        //    {
        //        var value = DataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
        //        var strCondition = String.Format("({0} = '{1}')", columnName, value.Replace("'", "''"));

        //        if (tscboPreviewType.SelectedIndex < 2)
        //        {
        //            tscboPreviewType.SelectedIndex = 2;
        //        }

        //        if (strCondition.Length > 0)
        //        {
        //            if (tstxtCriteria.Text.Trim().Length == 0)
        //            {
        //                tstxtCriteria.Text = strCondition;
        //                FetchAndPopulateData();
        //            }
        //            else
        //            {
        //                if (tstxtCriteria.Text.IndexOf(strCondition) < 0)
        //                {
        //                    if (tstxtCriteria.Text.StartsWith("("))
        //                    {
        //                        tstxtCriteria.Text = String.Format("{0} AND {1}", tstxtCriteria.Text, strCondition);
        //                    }
        //                    else
        //                    {
        //                        tstxtCriteria.Text = String.Format("({0}) AND {1}", tstxtCriteria.Text, strCondition);
        //                    }
        //                    FetchAndPopulateData();
        //                }
        //            }
        //        }
        //    }
        //}
        //public string Script { get; set; } = "";

        //private void tscboPreviewType_Click(object sender, EventArgs e)
        //{

        //}

        //private void tscboPreviewType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (tscboPreviewType.SelectedIndex >= 2)
        //    {
        //        tslblCriteria.Visible = true;
        //        tstxtCriteria.Visible = true;
        //        tstxtCriteria.Focus();
        //    }
        //    else
        //    {
        //        tslblCriteria.Visible = false;
        //        tstxtCriteria.Visible = false;
        //    }
        //}

        //private void DataGridView1_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        //{
        //    _activeColumn = e.ColumnIndex;
        //    AddFilter();
        //}

        //private void AddFilter()
        //{
        //    var columnName = GetActiveColumnName();
        //    if (columnName.Length > 0)
        //    {
        //        if (tscboPreviewType.SelectedIndex < 2)
        //        {
        //            tscboPreviewType.SelectedIndex = 2;
        //        }

        //        string strValue = ""; //InputBox(String.Format("Please enter filter value for {0}", columnName)).Trim()
        //        string strCondition = "";
        //        if (strValue.Length > 0)
        //        {
        //            if (strValue.ToLower().StartsWith("is ") ||
        //                 strValue.StartsWith("<") ||
        //                 strValue.StartsWith(">") ||
        //                 strValue.StartsWith("="))
        //            {
        //                strCondition = String.Format("({0} {1})", columnName, strValue);
        //            }
        //            else
        //            {
        //                strCondition = String.Format("({0} = '{1}')", columnName, strValue.Replace("'", "''"));
        //            }

        //            if (strCondition.Length > 0)
        //            {
        //                if (tstxtCriteria.Text.Trim().Length == 0)
        //                {
        //                    tstxtCriteria.Text = strCondition;
        //                    FetchAndPopulateData();
        //                }
        //                else
        //                {
        //                    if (tstxtCriteria.Text.IndexOf(strCondition) < 0)
        //                    {
        //                        if (tstxtCriteria.Text.StartsWith("("))
        //                        {
        //                            tstxtCriteria.Text = String.Format("{0} AND {1}", tstxtCriteria.Text, strCondition);
        //                        }
        //                        else
        //                        {
        //                            tstxtCriteria.Text = String.Format("({0}) AND {1}", tstxtCriteria.Text, strCondition);
        //                        }
        //                        FetchAndPopulateData();
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        //private void tstxtCriteria_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == (char)13 && tstxtCriteria.Text.Trim().Length > 1)
        //    {
        //        FetchAndPopulateData();
        //    }
        //}

        //private void mnuFilter_Click(object sender, EventArgs e)
        //{
        //    AddFilter();
        //}

        //private void ToExcel()
        //{
        //}

        //private void tsbtnCopyScript_Click(object sender, EventArgs e)
        //{
        //    if (Script.Length > 1)
        //    {
        //        Clipboard.SetText(Script);
        //    }
        //}

        //private void btnPreview_Click(object sender, EventArgs e)
        //{
        //    FetchAndPopulateData();
        //}
    }
}
