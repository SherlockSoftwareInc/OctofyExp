

using OctofyLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OctofyExp
{
    /// <summary>
    /// Column value frequency form:
    /// Show column value frequency for the specified table and column
    /// 
    /// Todo: async/await and Cancel a Task and Its Children:
    /// https://docs.microsoft.com/en-us/dotnet/standard/parallel-programming/how-to-cancel-a-task-and-its-children
    /// https://www.codeproject.com/Questions/1243163/Csharp-how-to-cancle-task-run-by-click-on-another
    /// https://stackoverflow.com/questions/39166619/will-this-cancel-an-executereaderasync
    /// </summary>
    public partial class ColumnFrequencyForm : Form
    {
        private readonly string BLANKS = Properties.Resources.A001;

        private TableColumn _dataSource;
        private List<string> _categories;
        private List<int> _values;
        private int _blanksCount;
        private string _errorMessage = "";
        private int _currentSortColumn;
        private bool _sortAscending = false;
        //private bool _inProcess = true;
        private int _currentSelectedRow = 0;
        private CancellationTokenSource _cancellation;

        public ColumnFrequencyForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if ((components != null))
                {
                    components.Dispose();
                }
                if (_dataSource != null)
                    _dataSource.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Connection string of the data table
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Table name
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// Column name to check frequency
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// Form Load event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TableColumnFrequencyForm_Load(object sender, EventArgs e)
        {
            tableNameToolStripStatusLabel.Text = TableName;
            columnToolStripStatusLabel.Text = ColumnName;

            var winLoc = Properties.Settings.Default.FrequencyFormLocation;
            if (winLoc.X >= Screen.PrimaryScreen.WorkingArea.Width || winLoc.X <= -Screen.PrimaryScreen.WorkingArea.Width)
            {
                winLoc.X = 0;
            }

            if (winLoc.Y >= Screen.PrimaryScreen.WorkingArea.Height || winLoc.Y <= -Screen.PrimaryScreen.WorkingArea.Height)
            {
                winLoc.Y = 0;
            }
            Point screenBounds = Properties.Settings.Default.FrequencyFormBounds;
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

            this.Size = Properties.Settings.Default.FrequencyFormSize;
            this.WindowState = (FormWindowState)Properties.Settings.Default.FrequencyFormState;
            if (!Module1.IsOnScreen(this))
                Location = new Point(0, 0);

            ctlChart.Clear(Properties.Resources.A012, Color.Black);  //A012:Please wait while loading data. It may take a while..
            Cursor = Cursors.WaitCursor;

            openFormTimer.Start();
        }

        ///// <summary>
        ///// Get column frequency data
        ///// </summary>
        ///// <returns></returns>
        //private string GetData()
        //{
        //    if (ConnectionString.Length > 0)
        //    {
        //        _errorMessage = GetData1();
        //        if (_errorMessage.Length > 1)
        //            _errorMessage = GetData2();

        //        if (_errorMessage.Length == 0)
        //        {
        //            switch (_dataSource.Result)
        //            {
        //                case TableColumn.TableColumnProcessResults.CategoryOverflow:
        //                    _errorMessage = Properties.Resources.A033;  //A033: There are too many categories in this table column.
        //                    break;
        //                case TableColumn.TableColumnProcessResults.NameLengthOverflow:
        //                    _errorMessage = Properties.Resources.A034;   //A034: Category names are too long.
        //                    break;
        //                case TableColumn.TableColumnProcessResults.UnknowDataType:
        //                    _errorMessage = Properties.Resources.A035;  //A035: Data type of the column is not supported.
        //                    break;
        //                default:
        //                    break;
        //            }
        //        }
        //    }
        //    else
        //        _errorMessage = Properties.Resources.A036;  //A036: Database connection does not specified.

        //    return _errorMessage;
        //}

        ///// <summary>
        ///// Get column frequency from SQL server side by using COUNT(*) and GROUP BY statement
        ///// </summary>
        ///// <returns></returns>
        //private string GetData1()
        //{
        //    using (SqlConnection conn = new SqlConnection(ConnectionString))
        //    {
        //        try
        //        {
        //            using (SqlCommand cmd = new SqlCommand()
        //            {
        //                Connection = conn,
        //                CommandType = CommandType.Text,
        //                CommandTimeout = Properties.Settings.Default.ConnectionTimeout
        //            })
        //            {
        //                cmd.CommandText = string.Format("SELECT [{0}], COUNT(*) AS [Count] FROM {1} WITH(NOLOCK) GROUP BY [{0}] ORDER BY [{0}]", ColumnName, QuoteTableName());

        //                conn.Open();
        //                SqlDataReader dr = cmd.ExecuteReader();
        //                var dataTable = new DataTable();
        //                dataTable.Load(dr);

        //                _dataSource = new TableColumn(dataTable, Properties.Settings.Default.CategoryNameMaxLen);
        //            }
        //        }
        //        catch (SqlException ex)
        //        {
        //            _errorMessage = ex.Message;
        //        }
        //        finally
        //        {
        //            if (conn.State == ConnectionState.Open)
        //                conn.Close();
        //        }
        //    }
        //    return _errorMessage;
        //}

        ///// <summary>
        ///// Get all column data and count frequency in the app.
        ///// This is in case the previous GROUP BY query failed, especially for column that is extracted from xml.
        ///// </summary>
        ///// <returns></returns>
        //private string GetData2()
        //{
        //    _errorMessage = "";
        //    using (SqlConnection conn = new SqlConnection(ConnectionString))
        //    {
        //        try
        //        {
        //            using (SqlCommand cmd = new SqlCommand()
        //            {
        //                Connection = conn,
        //                CommandType = CommandType.Text,
        //                CommandTimeout = Properties.Settings.Default.ConnectionTimeout
        //            })
        //            {
        //                cmd.CommandText = string.Format("SELECT [{0}] FROM {1} WITH(NOLOCK)", ColumnName, QuoteTableName());
        //                conn.Open();
        //                SqlDataReader dr = cmd.ExecuteReader();
        //                var dataTable = new DataTable();
        //                dataTable.Load(dr);

        //                _dataSource = new TableColumn(dataTable, 0, long.MaxValue, Properties.Settings.Default.CategoryNameMaxLen);
        //            }
        //        }
        //        catch (SqlException ex)
        //        {
        //            _errorMessage = ex.Message;
        //        }
        //        finally
        //        {
        //            if (conn.State == ConnectionState.Open)
        //                conn.Close();
        //        }
        //    }

        //    return _errorMessage;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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
        /// Close button click event handler: close the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseToolStripButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Handle chart view toolbar button click event:
        /// switch to chart view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChartViewToolStripButton_Click(object sender, EventArgs e)
        {
            if (!chartViewToolStripButton.Checked)
            {
                if (frequencyDataGridView != null)
                {
                    frequencyDataGridView.Visible = false;
                    frequencyDataGridView.Dock = DockStyle.None;
                    frequencyDataGridView.SendToBack();
                }
                ctlChart.Dock = DockStyle.Fill;
                ctlChart.Visible = true;
                ctlChart.BringToFront();

                if (_currentSelectedRow >= 0)
                    ctlChart.SelectCategory(_currentSelectedRow);

                ctlChart.Focus();
                chartViewToolStripButton.Checked = true;
                gridViewToolStripButton.Checked = false;

                toolStripSeparator2.Visible = true;
            }
        }

        /// <summary>
        /// Handler data grid view toolbar button click event:
        /// switch to data grid view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridViewToolStripButton_Click(object sender, EventArgs e)
        {
            if (!gridViewToolStripButton.Checked)
            {
                //if (frequencyDataGridView == null)
                //    CreateDataGrid();

                ctlChart.Visible = false;
                ctlChart.Dock = DockStyle.None;
                ctlChart.SendToBack();
                frequencyDataGridView.Dock = DockStyle.Fill;
                frequencyDataGridView.Visible = true;
                frequencyDataGridView.BringToFront();
                frequencyDataGridView.Focus();
                chartViewToolStripButton.Checked = false;
                gridViewToolStripButton.Checked = true;

                toolStripSeparator2.Visible = false;
            }
        }

        /// <summary>
        /// Handle sort toolbar button click event:
        /// Toggle sort by categories count
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortToolStripButton_Click(object sender, EventArgs e)
        {
            bool status = !sortToolStripButton.Checked;
            sortToolStripButton.Checked = status;
            if (status)
            {
                _sortAscending = false;
                _currentSortColumn = 1;
            }
            else
            {
                _sortAscending = true;
                _currentSortColumn = 0;
            }

            SortData(_currentSortColumn);
        }

        /// <summary>
        /// Handle exclude blanks toolbar button click event:
        /// Toggle exclude/include blanks in the chart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExcludeBlanksToolStripButton_Click(object sender, EventArgs e)
        {
            if (excludeBlanksToolStripButton.Checked)
            {
                excludeBlanksToolStripButton.Checked = false;
                this.excludeBlanksToolStripButton.Image = global::OctofyExp.Properties.Resources.no_blanks;
                this.excludeBlanksToolStripButton.ToolTipText = Properties.Resources.A037;  //A037: Exclude blanks
            }
            else
            {
                excludeBlanksToolStripButton.Checked = true;
                this.excludeBlanksToolStripButton.Image = global::OctofyExp.Properties.Resources.include_blanks;
                this.excludeBlanksToolStripButton.ToolTipText = Properties.Resources.A038;  //A038: Include blanks
            }
            Populate();
        }

        /// <summary>
        /// Populate the chart
        /// </summary>
        private void PopulateChart()
        {
            if (ctlChart == null || frequencyDataGridView == null)
                return;

            if (frequencyDataGridView.Rows.Count > 0)
            {
                Cursor = Cursors.WaitCursor;

                int rowCount = frequencyDataGridView.Rows.Count;
                var values = new decimal?[rowCount];
                var categories = new List<string>();

                for (int i = 0; i < rowCount; i++)
                {
                    string category = frequencyDataGridView.Rows[i].Cells[0].Value.ToString();
                    if (category.Length == 0)
                        category = BLANKS;
                    categories.Add(category);
                    values[i] = Convert.ToDecimal(frequencyDataGridView.Rows[i].Cells[1].Value);
                }

                ctlChart.Title = ColumnName;
                messageToolStripStatusLabel.Text = Properties.Resources.A040;   //A040: Populating the chart...
                Application.DoEvents();
                ctlChart.Open(values, ColumnName, categories, Properties.Resources.A041);   //A041: Frequency

                bool searchBoxVisible = (categories.Count > 10 || ctlChart.CategoryLengthOverflow);
                this.searchToolStripTextBox.Visible = searchBoxVisible;
                this.searchForToolStripLabel.Visible = searchBoxVisible;

                if (excludeBlanksToolStripButton.Checked && _blanksCount > 0)
                    messageToolStripStatusLabel.Text = string.Format(Properties.Resources.A024, _blanksCount.ToString("N0"));
                else
                    messageToolStripStatusLabel.Text = "";

                Cursor = Cursors.Default;
            }
            else
            {
                ctlChart.Clear(Properties.Resources.A009, Color.Black);
                messageToolStripStatusLabel.Text = "";
            }
        }

        /// <summary>
        /// Find the index of the date in the period list
        /// </summary>
        /// <param name="periods"></param>
        /// <param name="eventDate"></param>
        /// <returns></returns>
        private int IndexOfTimePeriod(List<TimePeriod> periods, DateTime eventDate)
        {
            if (eventDate < periods[0].StartDate)
                return -1;  // before first period
            if (eventDate > periods[periods.Count - 1].EndDate)
                return -1;  // after last period

            return TimePeriodBinarySearch(periods, eventDate, 0, periods.Count - 1);
        }

        /// <summary>
        /// Do binary search to determine the index of a date in a period list
        /// </summary>
        /// <param name="periods">Time period list</param>
        /// <param name="eventDate">date to find the index</param>
        /// <param name="startIndex">Start index in the period list to search</param>
        /// <param name="endIndex">End index in the period list to search</param>
        /// <returns></returns>
        private int TimePeriodBinarySearch(List<TimePeriod> periods, DateTime eventDate, int startIndex, int endIndex)
        {
            if (startIndex > endIndex)
                return -1;

            if (eventDate >= periods[startIndex].StartDate && eventDate <= periods[startIndex].EndDate)
                return startIndex;

            if (startIndex == endIndex)
                return -1;

            if (eventDate >= periods[endIndex].StartDate && eventDate <= periods[endIndex].EndDate)
                return endIndex;

            if (startIndex + 2 == endIndex)
            {
                if (eventDate >= periods[startIndex + 1].StartDate && eventDate <= periods[startIndex + 1].EndDate)
                { return startIndex + 1; }
                else
                { return -1; }
            }

            int midIndex = startIndex + (endIndex - startIndex) / 2;
            if (eventDate >= periods[midIndex].StartDate)
                return TimePeriodBinarySearch(periods, eventDate, midIndex, endIndex);
            else
                return TimePeriodBinarySearch(periods, eventDate, startIndex, midIndex - 1);
        }


        /// <summary>
        /// Build data table with 1-dimensional data
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="categories"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        private DataTable BuildDataTable(string columnName, List<string> categories, decimal?[] values)
        {
            var result = new DataTable();
            result.Columns.Add(new DataColumn(columnName, Type.GetType("System.String")));
            result.Columns.Add(new DataColumn(Properties.Resources.A027, Type.GetType("System.Int32")));
            result.Columns.Add(new DataColumn(Properties.Resources.A028, Type.GetType("System.String")));
            decimal total = 0;
            foreach (var value in values)
            {
                if (value.HasValue)
                    total += (decimal)value;
            }

            for (int i = 0; i < categories.Count; i++)
            {
                var dr = result.NewRow();
                dr[0] = categories[i];
                if (values[i].HasValue)
                {
                    decimal value = (decimal)values[i];
                    dr[1] = value;
                    if (values[i] != 0)
                    {
                        if (total > 0)
                        {
                            dr[2] = ((value * 100) / total).ToString("N2") + "%";
                        }
                    }
                }
                else
                    dr[1] = 0;
                result.Rows.Add(dr);
            }
            return result;
        }

        /// <summary>
        /// Sort data by column index
        /// </summary>
        /// <param name="columnIndex"></param>
        private void SortData(int columnIndex)
        {
            rowNumToolStripStatusLabel.Text = "";
            messageToolStripStatusLabel.Text = Properties.Resources.A039;   //039: Calculating...
            ctlChart.Clear(Properties.Resources.A029, Color.Black);
            Application.DoEvents();

            if (columnIndex >= 0 && columnIndex < frequencyDataGridView.Columns.Count)
            {
                _currentSortColumn = columnIndex;
                if (_sortAscending)
                {
                    frequencyDataGridView.Sort(frequencyDataGridView.Columns[columnIndex], System.ComponentModel.ListSortDirection.Ascending);
                }
                else
                {
                    frequencyDataGridView.Sort(frequencyDataGridView.Columns[columnIndex], System.ComponentModel.ListSortDirection.Descending);
                }
                ClearSearch();

                frequencyDataGridView.ClearSelection();
                if (frequencyDataGridView.CurrentRow != null)
                    frequencyDataGridView.CurrentRow.Selected = false;

            }
            //if (excludeBlanksToolStripButton.Checked && _blanksCount > 0)
            //    messageToolStripStatusLabel.Text = string.Format(Properties.Resources.A024, _blanksCount.ToString("N0"));
            //else
            //    messageToolStripStatusLabel.Text = "";
        }

        #region"Read data async"
        /// <summary>
        /// Async version of GetData
        /// </summary>
        /// <returns></returns>
        private async Task<string> GetDataAsync(CancellationToken cancellationToken)
        {
            if (ConnectionString.Length > 0)
            {
                // try using SELECT COUNT(*) ... GROUP BY ...
                _errorMessage = await GetData1Async(cancellationToken);
                if (_errorMessage.Length > 1)
                {
                    // the group by does not work on xml data type column
                    // for those view column extract from xml column, 
                    // loading them into the app and count them in by program
                    _errorMessage = await GetData2Async(cancellationToken);
                }

                //_cancellation.Dispose();
            }
            else
                _errorMessage = Properties.Resources.A036;  //A036: Database connection does not specified.

            if (_errorMessage.Length == 0)
            {
                _blanksCount = _dataSource.BlanksCount;
                excludeBlanksToolStripButton.Enabled = (_blanksCount > 0);
            }

            return _errorMessage;
        }

        /// <summary>
        /// Async version of GetData1
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetData1Async(CancellationToken cancellationToken)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand()
                    {
                        Connection = conn,
                        CommandType = CommandType.Text,
                        CommandTimeout = Properties.Settings.Default.ConnectionTimeout
                    })
                    {
                        cmd.CommandText = string.Format("SELECT [{0}], COUNT(*) AS [Count] FROM {1} WITH(NOLOCK) GROUP BY [{0}] ORDER BY [{0}]", ColumnName, QuoteTableName());

                        await conn.OpenAsync();
                        using (CancellationTokenRegistration crt = cancellationToken.Register(() => cmd.Cancel()))
                        {
                            using (var dr = await cmd.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false))
                            {
                                var dataTable = new DataTable();
                                dataTable.Load(dr);

                                _dataSource = new TableColumn(dataTable, Properties.Settings.Default.CategoryNameMaxLen);
                                dr.Close();
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    _errorMessage = ex.Message;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
            return _errorMessage;
        }

        /// <summary>
        /// Async version of GetData2
        /// </summary>
        /// <returns></returns>
        private async Task<string> GetData2Async(CancellationToken cancellationToken)
        {
            _errorMessage = "";
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand()
                    {
                        Connection = conn,
                        CommandType = CommandType.Text,
                        CommandTimeout = Properties.Settings.Default.ConnectionTimeout
                    })
                    {
                        cmd.CommandText = string.Format("SELECT [{0}] FROM {1} WITH(NOLOCK)", ColumnName, QuoteTableName());

                        await conn.OpenAsync();
                        using (CancellationTokenRegistration crt = cancellationToken.Register(() => cmd.Cancel()))
                        {
                            using (var dr = await cmd.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false))
                            {
                                var dataTable = new DataTable();
                                dataTable.Load(dr);

                                _dataSource = new TableColumn(dataTable, 0, long.MaxValue, Properties.Settings.Default.CategoryNameMaxLen);
                                dr.Close();
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    _errorMessage = ex.Message;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }

            return _errorMessage;
        }
        #endregion

        #region"Category Search"
        private readonly List<int> _searchResults = new List<int>();    // hold the positions of matched categories
        private string _searchFor = "";     // this is what currently search for
        private int _currentSeachIndex = 0; // the current position in search results

        /// <summary>
        /// Handle search text box key press event:
        ///     Enter key -- start search or search for next
        ///     Escape key -- clear the search text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchToolStripTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var searchFor = searchToolStripTextBox.Text.TrimEnd();
                if (searchFor.Length > 0)
                {
                    if (string.Compare(_searchFor, searchFor, true) == 0)
                    {
                        SearchNext();
                    }
                    else
                    {
                        _searchResults.Clear();
                        _searchFor = searchFor;


                        //build search results
                        for (int i = 0; i < frequencyDataGridView.Rows.Count; i++)
                        {
                            var category = frequencyDataGridView.Rows[i].Cells[0].Value.ToString();
                            if (category.Length > 0 && category != BLANKS)
                            {
                                int index = category.IndexOf(searchFor, StringComparison.CurrentCultureIgnoreCase);
                                if (index >= 0)
                                    _searchResults.Add(i);
                            }
                        }

                        if (_searchResults.Count == 0)
                        {
                            MessageBox.Show(Properties.Resources.A032);
                        }
                        else if (_searchResults.Count == 1)
                        {
                            GoToChartRow(_searchResults[0]);
                            searchResultToolStripLabel.Visible = false;
                            nextToolStripButton.Visible = false;
                            previousToolStripButton.Visible = false;
                        }
                        else
                        {
                            _currentSeachIndex = 0;
                            GotoSeachResult(0);
                            nextToolStripButton.Visible = true;
                            previousToolStripButton.Visible = true;
                        }
                    }
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                searchToolStripTextBox.Text = "";
                ClearSearch();
                searchToolStripTextBox.Focus();
            }
        }

        /// <summary>
        /// Show matched category in the chart
        /// </summary>
        /// <param name="index"></param>
        private void GotoSeachResult(int index)
        {
            if (index >= 0 && index < _searchResults.Count)
            {
                _currentSeachIndex = index;
                GoToChartRow(_searchResults[index]);
                GotoDataGridRow(_searchResults[index]);

                searchResultToolStripLabel.Text = string.Format("{0}/{1}", index + 1, _searchResults.Count);
                searchResultToolStripLabel.Visible = true;
            }
        }

        /// <summary>
        /// Handle next search result button click event:
        /// Go to next mached result
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextToolStripButton_Click(object sender, EventArgs e)
        {
            SearchNext();
        }

        /// <summary>
        /// Go to next matched category
        /// </summary>
        private void SearchNext()
        {
            if (nextToolStripButton.Visible)
            {
                var index = _currentSeachIndex + 1;
                if (index >= _searchResults.Count)
                    index = 0;
                GotoSeachResult(index);
            }
        }

        /// <summary>
        /// Handle previous search result button click event:
        /// Go to previous mached result
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviousToolStripButton_Click(object sender, EventArgs e)
        {
            var index = _currentSeachIndex - 1;
            if (index < 0)
                index = _searchResults.Count - 1;
            GotoSeachResult(index);
        }

        /// <summary>
        /// Handle F3 and Escape key press on the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ColumnFrequencyForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.searchToolStripTextBox.Visible)
            {
                if (e.KeyCode == Keys.F3)
                {
                    if (_searchFor.Length > 0)
                        SearchNext();

                    searchToolStripTextBox.Focus();
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    searchToolStripTextBox.Text = "";
                    ClearSearch();
                    searchToolStripTextBox.Focus();
                }
            }
        }

        /// <summary>
        /// Clear search result when search text changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchToolStripTextBox_TextChanged(object sender, EventArgs e)
        {
            if (nextToolStripButton.Visible)
            {
                ClearSearch();
            }
        }

        /// <summary>
        /// Clear search, hide buttons and search results indicator
        /// </summary>
        private void ClearSearch()
        {
            searchResultToolStripLabel.Visible = false;
            nextToolStripButton.Visible = false;
            previousToolStripButton.Visible = false;
            _searchResults.Clear();
            _searchFor = "";
            _currentSelectedRow = -1;
        }

        /// <summary>
        /// Highlight selected category by index and show the position
        /// </summary>
        /// <param name="rowIndex"></param>
        private void GoToChartRow(int rowIndex)
        {
            _currentSelectedRow = rowIndex;
            ctlChart.SelectCategory(rowIndex);
            rowNumToolStripStatusLabel.Text = string.Format("Rn: {0}", (rowIndex + 1).ToString("N0"));
        }
        #endregion

        /// <summary>
        /// Handle chart selected index change event:
        /// Show selected category position
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnChart_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rowIndex = ctlChart.SelectedIndex.Y;
            if (rowIndex >= 0)
            {
                GotoDataGridRow(ctlChart.SelectedIndex.Y);
            }
            else
                rowNumToolStripStatusLabel.Text = "";
        }

        /// <summary>
        /// Move selected row in data grid to a specified row
        /// </summary>
        /// <param name="index"></param>
        private void GotoDataGridRow(int index)
        {
            frequencyDataGridView.ClearSelection();
            if (frequencyDataGridView.CurrentRow != null)
                frequencyDataGridView.CurrentRow.Selected = false;

            if (index < frequencyDataGridView.Rows.Count)
            {
                frequencyDataGridView.Rows[index].Selected = true;
                frequencyDataGridView.CurrentCell = frequencyDataGridView.Rows[index].Cells[0];
                frequencyDataGridView.FirstDisplayedScrollingRowIndex = index;

                rowNumToolStripStatusLabel.Text = string.Format("Rn: {0}", (index + 1).ToString("N0"));
            }
        }

        /// <summary>
        /// Handle form closing event:
        /// remember form size and state
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ColumnFrequencyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CancelTask();

            Properties.Settings.Default.FrequencyFormState = (short)this.WindowState;
            if (WindowState != FormWindowState.Minimized)
            {
                if (WindowState == FormWindowState.Normal)
                {
                    Properties.Settings.Default.FrequencyFormLocation = this.Location;
                    Properties.Settings.Default.FrequencyFormSize = this.Size;
                }
                var s = Screen.FromControl(this);
                Properties.Settings.Default.FrequencyFormBounds = s.Bounds.Location;
            }
        }

        /// <summary>
        /// frequency data grid sorted event handle:
        /// Populate the chart on sort result
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrequencyDataGridView_Sorted(object sender, EventArgs e)
        {
            PopulateChart();
        }

        /// <summary>
        /// Sync selected item on the chart with data grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrequencyDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (frequencyDataGridView.CurrentCell == null)
                return;

            int rowIndex = frequencyDataGridView.CurrentCell.RowIndex;
            if (rowIndex >= 0)
            {
                GoToChartRow(rowIndex);
            }
        }

        /// <summary>
        /// Populate chart by new date group selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GroupByToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Populate();
        }

        /// <summary>
        /// 
        /// </summary>
        private void CancelTask()
        {
            if (_cancellation != null)
            {
                try
                {
                    _cancellation.Cancel();
                }
                catch (ObjectDisposedException)
                {
                    // ignore
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OpenFormTimer_Tick(object sender, EventArgs e)
        {
            openFormTimer.Stop();

            bool fetchResult = false;

            using (var cancellationTokenSource = new CancellationTokenSource())
            {
                try
                {
                    _cancellation = cancellationTokenSource;
                    string msg = await GetDataAsync(cancellationTokenSource.Token);

                    if (msg.Length > 0)
                        ctlChart.Clear(msg, Color.Red);
                    else
                    {
                        if (_dataSource.DataType == TableColumn.DataSubtypes.Date ||
                            _dataSource.DataType == TableColumn.DataSubtypes.StringDate)
                        {
                            fetchResult = true;
                            //Populate();
                        }
                        else
                            fetchResult = true;
                        //Populate();
                    }

                    if (fetchResult)
                    {
                        Populate();
                    }
                }
                catch (TaskCanceledException)
                {
                    //toolStripStatusLabel1.Text = "Task was cancelled";
                }
            }

            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Populate the chart
        /// </summary>
        private void Populate()
        {
            if (ctlChart == null || frequencyDataGridView == null || _dataSource == null)
                return;

            //_categories.Clear();
            if (frequencyDataGridView.DataSource != null)
                frequencyDataGridView.DataSource = null;

            ClearSearch();
            rowNumToolStripStatusLabel.Text = "";

            if (_errorMessage.Length > 0)
            {
                ctlChart.Clear(_errorMessage, Color.Red);
                rowsToolStripStatusLabel.Text = Properties.Resources.A009;  //A009: No data available
            }
            else
            {
                messageToolStripStatusLabel.Text = Properties.Resources.A039;   //A039: Calculating...

                _categories = _dataSource.GetUniqueValues(true);
                _values = _dataSource.GetUniqueValueCounts(true);
                bool excludeBlanks = excludeBlanksToolStripButton.Checked;

                decimal?[] values;
                List<string> categories = new List<string>();
                DataTable data = new DataTable();

                int numDataPoint = _values.Count;
                if (!excludeBlanks && _blanksCount > 0)
                    numDataPoint++;

                values = new decimal?[numDataPoint];
                int dx = 0;
                if (!excludeBlanks && _blanksCount > 0)
                {
                    values[0] = _blanksCount;
                    categories.Add(BLANKS);
                    dx = 1;
                }

                for (int i = 0; i < _values.Count; i++)
                {
                    values[i + dx] = _values[i];
                }
                categories.AddRange(_categories);

                data = BuildDataTable(ColumnName, categories, values);

                if (data.Rows.Count > 0)
                {
                    frequencyDataGridView.DataSource = data;
                    frequencyDataGridView.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;

                    frequencyDataGridView.ReadOnly = true;
                    frequencyDataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);

                    int rowCount = data.Rows.Count;
                    rowsToolStripStatusLabel.Text = string.Format(Properties.Resources.A025, rowCount.ToString("N0"));

                    if (rowCount > 50000)
                    {
                        ctlChart.Clear(Properties.Resources.A029, Color.Black);
                        System.Windows.Forms.Application.DoEvents();
                    }

                    if (sortToolStripButton.Checked && _currentSortColumn == 1)
                    {
                        if (_sortAscending)
                        {
                            frequencyDataGridView.Sort(frequencyDataGridView.Columns[1], System.ComponentModel.ListSortDirection.Ascending);
                        }
                        else
                        {
                            frequencyDataGridView.Sort(frequencyDataGridView.Columns[1], System.ComponentModel.ListSortDirection.Descending);
                        }
                        ClearSearch();

                        frequencyDataGridView.ClearSelection();
                        if (frequencyDataGridView.CurrentRow != null)
                            frequencyDataGridView.CurrentRow.Selected = false;
                    }
                    else
                    {
                        PopulateChart();
                    }
                }
                else
                {
                    ctlChart.Clear(Properties.Resources.A009, Color.Black);
                    messageToolStripStatusLabel.Text = "";
                }
            }
        }
    }
}
