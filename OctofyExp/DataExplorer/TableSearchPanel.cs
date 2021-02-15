using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace OctofyExp
{
    public partial class TableSearchPanel : UserControl
    {
        public enum SearchTypeNums
        {
            TableName = 0,
            ColumnName
        }

        public event EventHandler AfterSelect;
        public event EventHandler<OnAnalysisTableEventArgs> OnAnalysisTable;
        public event EventHandler<OnAnalysisTableEventArgs> OnPreviewData;

        private readonly SearchHistories _searchHistories = new SearchHistories();

        public TableSearchPanel()
        {
            InitializeComponent();
        }

        private string _connectionString = "";

        private int _defaultNumOfRows;

        public int DefaultNumOfRows
        {
            get { return _defaultNumOfRows; }
            set
            {
                _defaultNumOfRows = value;
                tableAnalysisToolStripMenuItem.Text = string.Format(Properties.Resources.A002, value);
            }
        }

        public string ConnectionString
        {
            get { return _connectionString; }
            set
            {
                _connectionString = value;
                searchResultsListBox.Items.Clear();
                if (value.Length == 0)
                {
                    searchTextBox.Enabled = false;
                    searchFromCombox.Enabled = false;
                    optionButton.Enabled = false;
                    searchButton.Enabled = false;
                }
                else
                {
                    searchTextBox.Enabled = true;
                    searchFromCombox.Enabled = true;
                    optionButton.Enabled = true;
                    searchButton.Enabled = true;
                }
            }
        }


        public string SelectedTable { get; set; } = "";

        public string TableType { get; set; } = "";

        public string SearchFor { get; set; } = "";

        public SearchTypeNums SearchType { get; set; } = SearchTypeNums.TableName;

        public string SearchHistory
        {
            get { return _searchHistories.ToString(); }
            set
            {
                if (value != null)
                    _searchHistories.Value = value;
                else
                    _searchHistories.Value = "";
                searchTextBox.AutoCompleteCustomSource = _searchHistories.DataSource;
            }
        }

        private void TableSearchPanel_Load(object sender, EventArgs e)
        {
            if (searchFromCombox.Items.Count > 0)
                searchFromCombox.SelectedIndex = 0;
            PopulateSearchPatternLabel();
        }

        private void PopulateSearchPatternLabel()
        {
            switch (Properties.Settings.Default.SearchPattern)
            {
                case 0:
                    searchPatternLabel.Text = Properties.Resources.A063;    //A063: (Starts with)
                    break;
                case 1:
                    searchPatternLabel.Text = Properties.Resources.A064;    //A064: (Ends with)
                    break;
                case 3:
                    searchPatternLabel.Text = Properties.Resources.A065;    //A065: (Equals)
                    break;
                default:
                    searchPatternLabel.Text = Properties.Resources.A067;    //A067: (Contains)
                    break;
            }
            searchPatternLabel.Left = Width - searchPatternLabel.Width - 12;
        }

        private void TableSearchPanel_Resize(object sender, EventArgs e)
        {
            int w = Width - 12;
            if (w > 0)
            {
                searchTextBox.Width = w;
                searchFromCombox.Width = w;
                searchResultsListBox.Width = w + 6;
                searchPatternLabel.Left = Width - searchPatternLabel.Width - 12;
            }
            int h = Height - searchResultsListBox.Top - 3;
            if (h > 0)
                searchResultsListBox.Height = h;
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            DOSearch();
        }

        private void DOSearch()
        {
            if (ConnectionString.Length > 0)
            {
                rowsLabel.Text = "";
                if (searchResultsListBox.Items.Count > 0)
                    searchResultsListBox.Items.Clear();

                string sqlSearchFor = searchTextBox.Text.Trim();
                SearchType = (SearchTypeNums)searchFromCombox.SelectedIndex;
                SearchFor = sqlSearchFor.Replace("%", "");

                if (sqlSearchFor.Length > 1)
                {
                    if (!sqlSearchFor.StartsWith("%"))
                        sqlSearchFor = "%" + sqlSearchFor;

                    if (!sqlSearchFor.EndsWith("%"))
                        sqlSearchFor += "%";

                    using (SqlConnection conn = new SqlConnection(ConnectionString))
                    {
                        try
                        {
                            using (SqlCommand cmd = new SqlCommand() { Connection = conn })
                            {
                                cmd.Parameters.Add(new SqlParameter("@searchFor", sqlSearchFor));
                                if (SearchType == SearchTypeNums.TableName)
                                {
                                    cmd.CommandText = "SELECT TABLE_SCHEMA, TABLE_NAME FROM information_schema.TABLES WHERE TABLE_NAME like @searchFor ORDER BY TABLE_NAME";
                                }
                                else
                                {
                                    cmd.CommandText = "SELECT DISTINCT TABLE_SCHEMA, TABLE_NAME, COLUMN_NAME FROM information_schema.columns WHERE column_name like @SearchFor ORDER BY TABLE_NAME";
                                }
                                conn.Open();
                                var dr = cmd.ExecuteReader();
                                while (dr.Read())
                                {
                                    string itemName;
                                    if (SearchType == SearchTypeNums.TableName)
                                    {
                                        itemName = dr["TABLE_NAME"].ToString();
                                    }
                                    else
                                    {
                                        itemName = dr["COLUMN_NAME"].ToString();
                                    }
                                    if (IsMatch(itemName))
                                    {
                                        AddSearchResult(string.Format("{0}.{1}", dr["TABLE_SCHEMA"].ToString(), dr["TABLE_NAME"].ToString()));
                                    }
                                }
                                dr.Close();
                            }
                            if (searchResultsListBox.Items.Count == 0)
                            {
                                rowsLabel.Text = Properties.Resources.A068; //A068: No match found
                            }
                            else if (searchResultsListBox.Items.Count == 1)
                            {
                                rowsLabel.Text = Properties.Resources.A069; //A069: One item found
                            }
                            else
                                rowsLabel.Text = string.Format(Properties.Resources.A070, searchResultsListBox.Items.Count);    //A070: {0} items found

                            if (searchResultsListBox.Items.Count > 0)
                                _searchHistories.AddSearchItem(searchTextBox.Text.Trim());
                        }
                        catch (SqlException)
                        {
                            throw;
                        }
                        finally
                        {
                            if (conn.State == ConnectionState.Open)
                                conn.Close();
                        }
                    }

                    if (searchResultsListBox.Items.Count > 0)
                        searchResultsListBox.SelectedIndex = 0;
                    else
                    {
                        SelectedTable = "";
                        AfterSelect?.Invoke(this, new EventArgs());
                    }
                }
                else
                    Console.Beep();
            }
        }

        /// <summary>
        /// Add a matched table into search result if it's not listed
        /// </summary>
        /// <param name="tableName"></param>
        private void AddSearchResult(string tableName)
        {
            bool tableExist = false;
            for (int i = 0; i < searchResultsListBox.Items.Count; i++)
            {
                if (searchResultsListBox.Items[i].ToString() == tableName)
                {
                    tableExist = true;
                    break;
                }
            }
            if (!tableExist)
            {
                searchResultsListBox.Items.Add(tableName);
            }
        }

        private bool IsMatch(string itemName)
        {
            if (itemName.Length > 0 && SearchFor.Length <= itemName.Length)
            {
                switch (Properties.Settings.Default.SearchPattern)
                {
                    case 0:
                        return itemName.StartsWith(SearchFor, StringComparison.CurrentCultureIgnoreCase);
                    case 1:
                        return itemName.EndsWith(SearchFor, StringComparison.CurrentCultureIgnoreCase);
                    case 3:
                        return string.Compare(itemName, SearchFor, true) == 0;
                    default:
                        return itemName.IndexOf(SearchFor, StringComparison.CurrentCultureIgnoreCase) >= 0;
                }
            }
            return false;
        }

        private void SearchResultsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (searchResultsListBox.SelectedItem != null)
            {
                SelectedTable = searchResultsListBox.SelectedItem.ToString();
                //switch (GetObjectType(SelectedTable))
                //{
                //    case "BASE TABLE":
                //        TableType = "T";
                //        break;

                //    case "VIEW":
                //        TableType = "V";
                //        break;

                //    default:
                //        break;
                //}
                AfterSelect?.Invoke(this, new EventArgs());
            }
            else
                SelectedTable = "";
            searchTextBox.Focus();
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(SelectedTable);
        }

        private void ScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedTable.Length > 0 && ConnectionString.Length > 0)
            {
                var sb = new StringBuilder();

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand() { Connection = conn, CommandType = System.Data.CommandType.Text })
                        {
                            cmd.CommandText = string.Format("SELECT * FROM {0} WHERE 0=1", SelectedTable);
                            conn.Open();

                            var dat = new SqlDataAdapter(cmd);
                            var ds = new DataSet();
                            dat.Fill(ds);

                            if (ds.Tables.Count > 0)
                            {
                                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                                {
                                    if (i == 0)
                                        sb.AppendLine("SELECT " + ds.Tables[0].Columns[i].ColumnName.Trim());
                                    else
                                        sb.AppendLine("      ," + ds.Tables[0].Columns[i].ColumnName.Trim());
                                }
                            }
                            sb.AppendLine(String.Format(" FROM {0}", SelectedTable));

                            Clipboard.SetText(sb.ToString());

                            MessageBox.Show(Properties.Resources.A062);
                        }
                    }
                    catch (SqlException)
                    {
                        throw;
                    }
                    finally
                    {
                        if (conn.State == ConnectionState.Open)
                            conn.Close();
                    }
                }
            }
        }

        //private void PropertyToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    MessageBox.Show("The feature is under development.");
        //}

        private void TxtSearchFor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                DOSearch();
            else if (e.KeyCode == Keys.C)
            {
                if (ModifierKeys == Keys.Control)
                    searchTextBox.Text = Clipboard.GetText();
            }
        }

        private void SearchResultsListBox_DoubleClick(object sender, EventArgs e)
        {
            OpenTableAnalysis(DefaultNumOfRows);
        }

        private void TableAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenTableAnalysis(DefaultNumOfRows);
        }

        private void OpenTableAnalysis(int numOfRows)
        {
            if (searchResultsListBox.SelectedItem != null)
            {
                string tableName = searchResultsListBox.SelectedItem.ToString();
                if (tableName.Length > 0)
                {
                    var args = new OnAnalysisTableEventArgs()
                    {
                        TableName = tableName,
                        NumOfRows = numOfRows,
                        ConnectionString = _connectionString
                    };

                    OnAnalysisTable?.Invoke(this, args);
                }
            }
        }

        private void AnalyzeEntireTableviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenTableAnalysis(-1);
        }

        private void SearchFromCombox_SelectedIndexChanged(object sender, EventArgs e)
        {
            searchTextBox.Focus();
        }

        private void PreviewDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (searchResultsListBox.SelectedItem != null)
            {
                string tableName = searchResultsListBox.SelectedItem.ToString();
                if (tableName.Length > 0)
                {
                    var args = new OnAnalysisTableEventArgs()
                    {
                        TableName = tableName,
                        NumOfRows = -1,
                        ConnectionString = _connectionString
                    };

                    OnPreviewData?.Invoke(this, args);
                }
            }
        }

        private void OptionButton_Click(object sender, EventArgs e)
        {
            using (SearchOptionsDialog dlg = new SearchOptionsDialog())
            {
                dlg.ShowDialog();
                PopulateSearchPatternLabel();
                searchTextBox.Focus();
            }
        }
    }
}
