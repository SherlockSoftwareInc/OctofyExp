using OctofyLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OctofyExp
{
    /// <summary>
    /// Provide analysis for an entire data table
    /// </summary>
    public partial class DataAnalysisForm
    {
        public DataAnalysisForm()
        {
            InitializeComponent();
        }

        public enum Views
        {
            BarChart,
            CategoryBarChart
        }

        private readonly string BLANKS = Properties.Resources.A001;

        private Views _view = Views.BarChart;
        private readonly TableAnalysis _tableAnalysis = new TableAnalysis();
        private readonly CustomizedDataBuilder _customData = new CustomizedDataBuilder();
        private readonly Stopwatch _stopWatch = new Stopwatch();
        private int _numOfBlanks = 0;
        private int _currentSortColumn;
        private bool _sortAscending = false;
        private CancellationTokenSource _cancellation;

        #region "Properties for Table analysis. Used when AnalysisType is Table"
        /// <summary>
        /// Name of the table or view (include schema name)
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// Connection string to where data sotred
        /// </summary>
        public string ConnectionString { get; set; } = "";

        /// <summary>
        /// Fetch top number of rows is the value of this perperty > 0
        /// </summary>
        public int NumberOfTopRows { get; set; }

        /// <summary>
        /// SQL Query statement
        /// </summary>
        public string TableSelectSQL { get; set; }

        /// <summary>
        /// List columns that are excluded from the query statement
        /// (some data type is not able to display in the DataGridView, 
        /// ie. image. These columns are excluded. However, as the 
        /// information, use should acknowledged which columns have been
        /// excluded)
        /// </summary>
        public List<string> ExcludedColumns { get; set; }
        #endregion


        /// <summary>
        /// Close menu item click event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Get X-Axis variable
        /// </summary>
        /// <returns></returns>
        private string XAxisVariable()
        {
            return measurementVariables.XAxisVariable;
        }

        /// <summary>
        /// Get Y-axis variable
        /// </summary>
        /// <returns></returns>
        private string YAxisVariable()
        {
            return measurementVariables.YAxisVariable;
        }

        /// <summary>
        /// Form load event handler: populate related controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>
        /// </remarks>
        private void AnalysisFormEx_Load(object sender, EventArgs e)
        {
            _tableAnalysis.TableParseProgressChange += TableAnalysis_TableParseProgressChange;
            _tableAnalysis.DataBuildProgressChange += TableAnalysis_DataBuildProgressChange;

            var winLoc = Properties.Settings.Default.AnalysisFormLocation;
            if (winLoc.X >= Screen.PrimaryScreen.WorkingArea.Width || winLoc.X <= -Screen.PrimaryScreen.WorkingArea.Width)
            {
                winLoc.X = 0;
            }

            if (winLoc.Y >= Screen.PrimaryScreen.WorkingArea.Height || winLoc.Y <= -Screen.PrimaryScreen.WorkingArea.Height)
            {
                winLoc.Y = 0;
            }
            Point screenBounds = Properties.Settings.Default.AnalysisFormBounds;
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

            this.Size = Properties.Settings.Default.AnalysisFormSize;
            this.WindowState = (FormWindowState)Properties.Settings.Default.AnalysisFormState;
            if (!Module1.IsOnScreen(this))
                Location = new Point(0, 0);

            //pass app settings to the controls
            _tableAnalysis.MaxUniqueValues = Properties.Settings.Default.MaxYCategories;
            _tableAnalysis.CategoryNameMaxLen = Properties.Settings.Default.CategoryNameMaxLen;
            measurementVariables.MaxYMembers = Properties.Settings.Default.MaxYCategories;
            measurementVariables.MaxXMembers = Properties.Settings.Default.MaxXCategories;

            //ClearChart(Properties.Resources.A029); //A029 : Please wait while calculating...

            // show table name
            tableNameToolStripStatusLabel.Text = TableName;

            startTimer.Start();
            chart.Title = "";
            WindowState = FormWindowState.Maximized;
            splitContainer1.SplitterDistance = 200;
        }

        /// <summary>
        /// Table analysis progress handler: Show process progress of parse table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TableAnalysis_TableParseProgressChange(object sender, EventArgs e)
        {
            var analyzer = (TableAnalysis)sender;
            messageToolStripStatusLabel.Text = string.Format(Properties.Resources.A010, analyzer.ProgressPercent.ToString("N0"));
            //ClearChart(string.Format(Properties.Resources.A010, analyzer.ProgressPercent.ToString("N0")));
        }

        /// <summary>
        /// Table analysis progress handler: Show process progress of building data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TableAnalysis_DataBuildProgressChange(object sender, EventArgs e)
        {
            var analyzer = (TableAnalysis)sender;
            messageToolStripStatusLabel.Text = string.Format(Properties.Resources.A010, analyzer.ProgressPercent.ToString("N0"));
            //ClearChart(string.Format(Properties.Resources.A010, analyzer.ProgressPercent.ToString("N0")));
        }


        /// <summary>
        /// Output current list to an Excel sheet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void SummaryToExcelMenuItem_Click(object sender, EventArgs e)
        {
            if (summaryDataGridView.DataSource != null)
            {
                Cursor = Cursors.WaitCursor;
                Module1.DataGridViewToExcel(summaryDataGridView, 1, 1, false);
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Copy the chart graph to the clipboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyChartToolStripButton_Click(object sender, EventArgs e)
        {
            Bitmap image = chart.ToImage();
            if (image != null)
                Clipboard.SetImage(image);
        }


        /// <summary>
        /// Chart selected index change event handler: show selected case in the case list grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnChartView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chart.SelectedIndexX >= 0 || chart.SelectedIndexY >= 0)
            {
                if (_view == Views.BarChart || _view == Views.CategoryBarChart)
                {
                    GotoSummaryDataGridRow(chart.SelectedIndexY);
                }
            }
        }

        /// <summary>
        /// Move the current selected row in the data grid to a specified row
        /// </summary>
        /// <param name="index"></param>
        private void GotoSummaryDataGridRow(int index)
        {
            summaryDataGridView.ClearSelection();
            if (summaryDataGridView.CurrentRow != null)
                summaryDataGridView.CurrentRow.Selected = false;

            if (index < summaryDataGridView.Rows.Count)
            {
                summaryDataGridView.Rows[index].Selected = true;
                summaryDataGridView.CurrentCell = summaryDataGridView.Rows[index].Cells[0];
                summaryDataGridView.FirstDisplayedScrollingRowIndex = index;

                rowNumToolStripStatusLabel.Text = string.Format("Rn: {0}", (index + 1).ToString("N0"));
            }
        }

        /// <summary>
        /// Get y-axis selected value
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private string GetYAxisSelectedValue(int index)
        {
            if (index >= 0 && index < summaryDataGridView.Rows.Count)
            {
                return summaryDataGridView.Rows[index].Cells[0].Value.ToString();
            }
            return "";
        }

        /// <summary>
        /// Get x-axis selected value
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private string GetXAxisSelecedValue(int index)
        {
            if (index >= 0 && index < summaryDataGridView.Columns.Count)
            {
                return summaryDataGridView.Columns[index].HeaderText;
            }
            return "";
        }

        /// <summary>
        /// Clear aggregate data grid
        /// </summary>
        private void ClearDataGrid()
        {
            if (summaryDataGridView.DataSource != null)
            {
                DataTable table = (DataTable)summaryDataGridView.DataSource;
                table.Dispose();
                summaryDataGridView.DataSource = null;
                _currentSortColumn = -1;
            }
        }

        /// <summary>
        /// Handle start timer tick event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void StartTimer_Tick(object sender, EventArgs e)
        {
            startTimer.Stop();
            Cursor = Cursors.WaitCursor;
            SetMenuToolButtonState(false);

            messageToolStripStatusLabel.Text = Properties.Resources.A011;
            loadTimeToolStripStatusLabel.Text = "";

            ClearChart(Properties.Resources.A012);

            using (var cancellationTokenSource = new CancellationTokenSource())
            {
                _cancellation = cancellationTokenSource;

                try
                {
                    string loadResult = await OpenDatabaseTableAsync(cancellationTokenSource.Token);

                    // Get source data by type
                    if (loadResult.Length == 0)
                    {
                        messageToolStripStatusLabel.Text = "";
                        ClearChart(Properties.Resources.A019);
                        string analysisResult = await AnalysisDatabaseTableAsync(cancellationTokenSource.Token);

                        if (analysisResult.Length == 0)
                        {
                            // Populate variable picker
                            measurementVariables.DateColumnName = "";
                            measurementVariables.Open(_tableAnalysis, _view);

                            //PrepareData();
                            _ = await BuildChartDataAsync(cancellationTokenSource.Token);
                            PopulateChart();

                            ShowExcludedColumns();

                            SetMenuToolButtonState(true);
                            messageToolStripStatusLabel.Text = "";
                        }
                        else
                        {
                            chart.Clear(analysisResult, Color.Red);
                        }
                    }
                    else
                    {
                        chart.Clear(Properties.Resources.A013 + ":\r\n" + loadResult, Color.Red);
                    }
                }
                catch (OutOfMemoryException)
                {
                    chart.Clear("There is no enough memory to complete this operation.", Color.Red);
                }
                catch (TaskCanceledException)
                {
                    Debug.Print("Task cancelled");
                }
                catch (ObjectDisposedException)
                { }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
        }

        /// <summary>
        /// Show message to tell use that there are excluded columns
        /// </summary>
        private void ShowExcludedColumns()
        {
            if (ExcludedColumns.Count > 0)
            {
                string message = string.Format(Properties.Resources.A014, TableName);
                foreach (var column in ExcludedColumns)
                {
                    message += "\r\t" + column;
                }
                MessageBox.Show(message, Properties.Resources.A015, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Open specified data table
        /// </summary>
        /// <returns></returns>
        private async Task<string> OpenDatabaseTableAsync(CancellationToken cancellationToken)
        {
            string result;
            try
            {
                _stopWatch.Reset();
                _stopWatch.Start();
                result = await _customData.OpenTableAsync(ConnectionString, TableSelectSQL, cancellationToken,
                    Properties.Settings.Default.ConnectionTimeout);
                _stopWatch.Stop();

                loadTimeToolStripStatusLabel.Text = string.Format(Properties.Resources.A016,
                    ((double)_stopWatch.ElapsedMilliseconds / 1000F).ToString("N2"));

            }
            catch (OutOfMemoryException)
            {
                result = "There is no enough memory to complete this operation.";
            }
            catch (TaskCanceledException)
            {
                result = "Task cancelled";
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }

        private async Task<string> AnalysisDatabaseTableAsync(CancellationToken cancellationToken)
        {
            string result;
            try
            {
                _stopWatch.Restart();
                DataTable data = _customData.GetData("");

                numOfRowsToolStripStatusLabel.Text = string.Format(Properties.Resources.A017,
                    data.Rows.Count.ToString("N0"));

                messageToolStripStatusLabel.Text = Properties.Resources.A018;

                await _tableAnalysis.OpenAsync(data, cancellationToken);
                _stopWatch.Stop();
                analysisTimeToolStripStatusLabel.Text = string.Format(Properties.Resources.A020,
                    ((double)_stopWatch.ElapsedMilliseconds / 1000F).ToString("N2"));

                result = "";
            }
            catch (OutOfMemoryException)
            {
                result = "There is no enough memory to complete this operation.";
            }
            catch (TaskCanceledException)
            {
                result = "Task cancelled";
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// Build data for the chart
        /// </summary>
        /// <returns></returns>
        private bool BuildChartData()
        {
            bool result = false;
            _numOfBlanks = 0;
            //_inProcess = true;

            searchToolStripTextBox.Text = "";
            ClearSearch();

            Cursor = Cursors.WaitCursor;

            _stopWatch.Reset();
            _stopWatch.Start();
            CalculateTimeToolStripStatusLabel.Text = "";
            messageToolStripStatusLabel.Text = Properties.Resources.A021;   //Build data for the chart...

            SetMenuToolButtonState(false);

            ClearDataGrid();

            switch (_view)
            {
                case Views.BarChart:
                    result = BuildBarchartData();
                    if (result)
                    {
                        SetSummaryGridSortMode(true);
                        summaryDataGridView.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                    break;

                case Views.CategoryBarChart:
                    result = BuildStackedBarChartData();
                    SetSummaryGridSortMode(true);
                    break;

                default:
                    SetSummaryGridSortMode(false);
                    break;
            }

            _stopWatch.Stop();
            CalculateTimeToolStripStatusLabel.Text = string.Format(Properties.Resources.A022,
                ((double)_stopWatch.ElapsedMilliseconds / 1000F).ToString("N2"));   //A022: Build chart data: {0}s

            bool searchBoxVisible = (summaryDataGridView.Rows.Count > 10);
            this.searchToolStripTextBox.Visible = searchBoxVisible;
            this.searchForToolStripLabel.Visible = searchBoxVisible;

            excludeBlanksToolStripButton.Enabled = (_numOfBlanks > 0);

            return result;
        }

        /// <summary>
        /// Build data for the chart and data grid
        /// </summary>
        /// <returns></returns>
        private async Task<bool> BuildChartDataAsync(CancellationToken cancellationToken)
        {
            bool result = false;
            _numOfBlanks = 0;
            //_inProcess = true;

            searchToolStripTextBox.Text = "";
            ClearSearch();

            Cursor = Cursors.WaitCursor;

            _stopWatch.Reset();
            _stopWatch.Start();
            CalculateTimeToolStripStatusLabel.Text = "";
            messageToolStripStatusLabel.Text = Properties.Resources.A021;

            SetMenuToolButtonState(false);

            ClearDataGrid();

            switch (_view)
            {
                case Views.BarChart:
                    result = await BuildBarchartDataAsync(cancellationToken);
                    SetSummaryGridSortMode(true);
                    break;

                case Views.CategoryBarChart:
                    result = await BuildStackedBarChartDataAsync(cancellationToken);
                    SetSummaryGridSortMode(true);
                    break;

                default:
                    SetSummaryGridSortMode(false);
                    break;
            }

            _stopWatch.Stop();
            CalculateTimeToolStripStatusLabel.Text = string.Format(Properties.Resources.A022,
                ((double)_stopWatch.ElapsedMilliseconds / 1000F).ToString("N2"));

            bool searchBoxVisible = (summaryDataGridView.Rows.Count > 10);
            this.searchToolStripTextBox.Visible = searchBoxVisible;
            this.searchForToolStripLabel.Visible = searchBoxVisible;

            excludeBlanksToolStripButton.Enabled = (_numOfBlanks > 0);

            return result;
        }

        /// <summary>
        /// Populate the chart
        /// </summary>
        private void PopulateChart()
        {
            Cursor = Cursors.WaitCursor;
            SetMenuToolButtonState(false);
            //_stopWatch.Reset();
            //_stopWatch.Start();

            chart.Title = "";
            chart.Subtitle = "";
            rowNumToolStripStatusLabel.Text = "";

            switch (_view)
            {
                case Views.BarChart:
                    PopulateBarchart();
                    break;

                case Views.CategoryBarChart:
                    PopulateStackedBarChart();
                    break;

                default:
                    break;
            }
            SetMenuToolButtonState(true);
            Cursor = Cursors.Default;

            if (_numOfBlanks > 0 && excludeBlanksToolStripButton.Checked)
                if (_numOfBlanks == 1)
                    messageToolStripStatusLabel.Text = Properties.Resources.A023;
                else
                    messageToolStripStatusLabel.Text = string.Format(Properties.Resources.A024, _numOfBlanks.ToString("N0"));
            else
                messageToolStripStatusLabel.Text = "";

        }

        /// <summary>
        /// Get Y members from data grid
        /// </summary>
        /// <returns></returns>
        private List<string> GetYMembers()
        {
            var result = new List<string>();
            for (int i = 0; i < summaryDataGridView.Rows.Count; i++)
            {
                result.Add(summaryDataGridView.Rows[i].Cells[0].Value.ToString());
            }
            return result;
        }

        /// <summary>
        /// Get category members of x-axis
        /// </summary>
        /// <returns></returns>
        private List<string> GetXMembers()
        {
            var result = new List<string>();
            for (int i = 1; i < summaryDataGridView.Columns.Count - 1; i++)
            {
                result.Add(summaryDataGridView.Columns[i].HeaderText);
            }
            return result;
        }

        /// <summary>
        /// Build data for the bar chart and show it on the data grid
        /// </summary>
        /// <returns></returns>
        private bool BuildBarchartData()
        {
            string column = YAxisVariable();
            if (column.Length > 0)
            {
                return BuildFreqencyData();
            }
            return false;
        }

        private async Task<bool> BuildBarchartDataAsync(CancellationToken cancellationToken)
        {
            bool result = false;
            string column = YAxisVariable();
            if (column.Length > 0)
            {
                bool excludeBlanks = excludeBlanksToolStripButton.Checked;
                var categories = _tableAnalysis.GetSortedColumnUniqueValues(column, excludeBlanks);
                var values = await _tableAnalysis.BuildFrequencyTableAsync(column, excludeBlanks, cancellationToken);
                _numOfBlanks = _tableAnalysis.GetNumOfBlanks(column);
                PopulateAggregateDataGrid(column, categories, values);
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Populate category bar chart
        /// </summary>
        private void PopulateBarchart()
        {
            bool result = false;

            string column = YAxisVariable();
            if (column.Length > 0)
            {
                var categories = GetYMembers();
                int count = summaryDataGridView.Rows.Count;
                decimal?[] values = new decimal?[count];
                for (int i = 0; i < count; i++)
                {
                    string strValue = summaryDataGridView.Rows[i].Cells[1].Value.ToString();
                    if (strValue.Length > 0)
                        if (decimal.TryParse(strValue, out decimal value))
                        {
                            values[i] = value;
                        }
                }

                if (ValuesHasData(values))
                {
                    chart.ChartType = AnalysisChartPanel.ChartTypes.BarChart;
                    chart.Title = measurementVariables.Title;

                    chart.OpenBarChart(values, column, categories, "");
                    categoryToolStripStatusLabel.Text = string.Format(Properties.Resources.A025,
                        categories.Count.ToString("N0"));   // A025: Categories: {0}
                    result = true;
                }
            }

            if (!result)
                chart.Clear(Properties.Resources.A009, Color.Black);
        }

        /// <summary>
        /// Check if the specified array has data
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private bool ValuesHasData(decimal?[] values)
        {
            foreach (var value in values)
            {
                if (value.HasValue)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Check if the specified 2D-array has data
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private bool ValuesHasData(decimal?[,] values)
        {
            foreach (var value in values)
            {
                if (value.HasValue)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Build data for stacked bar chart
        /// </summary>
        /// <returns></returns>
        private bool BuildStackedBarChartData()
        {
            bool result = false;
            string xColumn = XAxisVariable();
            string yColumn = YAxisVariable();
            if (xColumn.Length > 0)
            {
                if (yColumn.Length > 0)
                {
                    bool excludeBlanks = excludeBlanksToolStripButton.Checked;
                    var xCategories = _tableAnalysis.GetSortedColumnUniqueValues(xColumn, false);
                    var yCategories = _tableAnalysis.GetSortedColumnUniqueValues(yColumn, excludeBlanks);
                    xCategories.Add(Properties.Resources.A026); // A026: (Subtotal)
                    var values = _tableAnalysis.BuildPivotTableValue(yColumn, xColumn, excludeBlanks);
                    _numOfBlanks = _tableAnalysis.GetNumOfBlanks(yColumn);

                    PopulateAggregateDataGrid(yColumn, yCategories, xColumn, xCategories, values);
                    result = true;
                }
            }
            else  //no x-axis variable
            {
                if (yColumn.Length > 0)
                {
                    bool excludeBlanks = excludeBlanksToolStripButton.Checked;
                    var categories = _tableAnalysis.GetSortedColumnUniqueValues(yColumn, excludeBlanks);
                    var values = _tableAnalysis.BuildFrequencyTable(yColumn, excludeBlanks);
                    _numOfBlanks = _tableAnalysis.GetNumOfBlanks(yColumn);
                    PopulateAggregateDataGrid(yColumn, categories, values);
                    result = true;
                }
            }
            return result;
        }

        private async Task<bool> BuildStackedBarChartDataAsync(CancellationToken cancellationToken)
        {
            bool result = false;
            string xColumn = XAxisVariable();
            string yColumn = YAxisVariable();
            if (xColumn.Length > 0)
            {
                if (yColumn.Length > 0)
                {
                    bool excludeBlanks = excludeBlanksToolStripButton.Checked;
                    var xCategories = _tableAnalysis.GetSortedColumnUniqueValues(xColumn, false);
                    var yCategories = _tableAnalysis.GetSortedColumnUniqueValues(yColumn, excludeBlanks);
                    xCategories.Add(Properties.Resources.A026); // A026: (Subtotal)
                    var values = await _tableAnalysis.BuildPivotTableValueAsync(yColumn, xColumn, excludeBlanks, cancellationToken);
                    _numOfBlanks = _tableAnalysis.GetNumOfBlanks(yColumn);

                    PopulateAggregateDataGrid(yColumn, yCategories, xColumn, xCategories, values);
                    result = true;
                }
            }
            else  //no x-axis variable
            {
                if (yColumn.Length > 0)
                {
                    return BuildFreqencyData();
                }
            }
            return result;
        }

        /// <summary>
        /// Populate stacked column chart
        /// </summary>
        private void PopulateStackedBarChart()
        {
            bool result = false;
            string xColumn = XAxisVariable();
            string yColumn = YAxisVariable();
            if (yColumn.Length > 0)
            {
                var categories = GetYMembers();
                var series = GetXMembers();
                int xCount = summaryDataGridView.Columns.Count - 1;
                int yCount = summaryDataGridView.Rows.Count;
                if (xCount > 0 && yCount >= 0)
                {
                    decimal?[,] values = new decimal?[yCount, xCount - 1];
                    for (int i = 0; i < yCount; i++)
                    {
                        for (int j = 1; j < xCount; j++)
                        {
                            string strValue = summaryDataGridView.Rows[i].Cells[j].Value.ToString();
                            if (strValue.Length > 0)
                                if (decimal.TryParse(strValue, out decimal value))
                                {
                                    values[i, j - 1] = value;
                                }
                        }
                    }

                    if (ValuesHasData(values))
                    {
                        chart.ChartType = AnalysisChartPanel.ChartTypes.StackedBarChart;
                        chart.Title = measurementVariables.Title;

                        chart.MaxSeries = Properties.Settings.Default.MaxXCategories;
                        chart.OpenStackedBarChart(values, yColumn, categories, xColumn, series);
                        categoryToolStripStatusLabel.Text = string.Format(Properties.Resources.A025,
                            categories.Count.ToString("N0"));
                        result = true;
                    }
                }
            }

            if (!result)
                chart.Clear(Properties.Resources.A009, Color.Black);
        }

        /// <summary>
        /// Populate aggregate datagrid with 1D data
        /// </summary>
        /// <param name="column"></param>
        /// <param name="categories"></param>
        /// <param name="values"></param>
        private void PopulateAggregateDataGrid(string column, List<string> categories, decimal?[] values)
        {
            summaryDataGridView.Columns.Clear();
            summaryDataGridView.DataSource = Build1DDataTable(column, categories, values);
            summaryDataGridView.ReadOnly = true;
            summaryDataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);

            excludeBlanksToolStripButton.Visible = true;
            sortToolStripButton.Visible = true;
            sortToolStripDropDownButton.Visible = false;

            if (sortToolStripButton.Checked)
            {
                SortSummaryData(1);
            }
        }

        /// <summary>
        /// Add a x-axis category member to the sort column drop down menu 
        /// so that it can be selected to sort by it
        /// </summary>
        /// <param name="name"></param>
        private void AddSortColumnButton(string name)
        {
            int index = sortToolStripDropDownButton.DropDownItems.Count;
            var button = new ToolStripMenuItem()
            {
                Name = string.Format("sortColumnToolStripMenuItem{0}", index + 1),
                Size = new System.Drawing.Size(180, 22),
                Text = name,
                Tag = index
            };
            button.Click += new System.EventHandler(this.OnSortColumnToolStripMenuItem_Click);
            sortToolStripDropDownButton.DropDownItems.Add(button);
        }

        /// <summary>
        /// Populate aggregate datagrid with 2D data
        /// </summary>
        /// <param name="yColumn">y-axis variable name</param>
        /// <param name="yMembers">y-axis unique values</param>
        /// <param name="xColumn">x-axis variable name</param>
        /// <param name="xMembers">x-axis uniquie values</param>
        /// <param name="values">Values of the chart</param>
        private void PopulateAggregateDataGrid(string yColumn, List<string> yMembers, string xColumn, List<string> xMembers, decimal?[,] values)
        {
            summaryDataGridView.Columns.Clear();
            summaryDataGridView.DataSource = Build2DDataTable(yColumn, yMembers, xColumn, xMembers, values);

            for (int i = 1; i < summaryDataGridView.ColumnCount; i++)
            {
                summaryDataGridView.Columns[i].HeaderText = xMembers[i - 1].TrimEnd();
            }
            summaryDataGridView.ReadOnly = true;
            summaryDataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);


            // clear tool buttons under Sort drop-down button
            for (int i = sortToolStripDropDownButton.DropDown.Items.Count - 1; i >= 0; i--)
            {
                ToolStripMenuItem submenuitem = (ToolStripMenuItem)sortToolStripDropDownButton.DropDownItems[i];
                submenuitem.Click -= OnSortColumnToolStripMenuItem_Click;
                sortToolStripDropDownButton.DropDownItems.RemoveAt(i);
            }
            AddSortColumnButton(yColumn);
            for (int i = 0; i < xMembers.Count; i++)
            {
                AddSortColumnButton(xMembers[i]);
            }

            sortToolStripButton.Visible = false;
            sortToolStripButton.Checked = false;
            excludeBlanksToolStripButton.Visible = true;
            sortToolStripDropDownButton.Visible = true;

            Application.DoEvents();

        }

        /// <summary>
        /// Build data table with 1-dimensional data
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="categories"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        private DataTable Build1DDataTable(string columnName, List<string> categories, decimal?[] values)
        {
            var result = new DataTable();
            result.Columns.Add(new DataColumn(columnName, Type.GetType("System.String")));
            result.Columns.Add(new DataColumn(Properties.Resources.A027, Type.GetType("System.Int32")));    //A027: Count
            result.Columns.Add(new DataColumn(Properties.Resources.A028, Type.GetType("System.String")));   //A028: Percent
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
        /// Build data table with 2-dimensional data
        /// </summary>
        /// <param name="columnNameX"></param>
        /// <param name="categoriesX"></param>
        /// <param name="columnNameY"></param>
        /// <param name="categoriesY"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        private DataTable Build2DDataTable(string columnNameY, List<string> categoriesY, string columnNameX, List<string> categoriesX, decimal?[,] values)
        {
            var result = new DataTable();

            result.Columns.Add(new DataColumn(columnNameY, Type.GetType("System.String")));
            for (int i = 0; i < categoriesX.Count; i++)
            {
                result.Columns.Add(new DataColumn(string.Format("Col{0}", i + 1), Type.GetType("System.Int32")));
            }
            for (int i = 0; i < categoriesY.Count; i++)
            {
                var dr = result.NewRow();
                dr[0] = categoriesY[i];
                for (int j = 0; j < categoriesX.Count; j++)
                {
                    if (values[i, j].HasValue)
                        dr[j + 1] = values[i, j];
                }
                result.Rows.Add(dr);
            }
            return result;
        }

        /// <summary>
        /// Copy menu click event handler: copy the aggregate data to the clipboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            summaryDataGridView.SelectAll();
            Clipboard.SetDataObject(summaryDataGridView.GetClipboardContent());
        }

        /// <summary>
        /// Export to Excel menu item click event handler:
        /// Output the aggregate data to an Excel sheet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SummaryToExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            Module1.DataGridViewToExcel(summaryDataGridView, 1, 1, false);
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Form closing event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnalysisFormEx_FormClosing(object sender, FormClosingEventArgs e)
        {
            CancelTask();

            Properties.Settings.Default.AnalysisFormState = (short)this.WindowState;
            if (WindowState != FormWindowState.Minimized)
            {
                if (WindowState == FormWindowState.Normal)
                {
                    Properties.Settings.Default.AnalysisFormLocation = this.Location;
                    Properties.Settings.Default.AnalysisFormSize = this.Size;
                }
                var s = Screen.FromControl(this);
                Properties.Settings.Default.AnalysisFormBounds = s.Bounds.Location;
            }

            Properties.Settings.Default.Save();
            _tableAnalysis?.Dispose();
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
        /// Clear the chart with display a given message
        /// </summary>
        /// <param name="message"></param>
        private void ClearChart(string message = "")
        {
            chart.Clear(message, Color.Black);
            Application.DoEvents();
        }

        /// <summary>
        /// Measure variable selection change event handler:
        /// Rebuild the chart based on the variable selections
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MeasurementVariables_SelectionChanged(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            BuildChartData();
            PopulateChart();

            measurementVariables.Focus();
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Bar char button/menu item click event handler:
        /// Switch to bar chart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarChartButton_Click(object sender, EventArgs e)
        {
            if (_view != Views.BarChart)
            {
                _view = Views.BarChart;
                measurementVariables.Open(_tableAnalysis, _view);
                CheckChartTypeButton(barChartToolStripButton);
                CheckChartTypeMenuItem(barChartToolStripMenuItem);
                BuildChartData();
                PopulateChart();
            }
        }

        /// <summary>
        /// Stacked column char button/menu item click event handler:
        /// Switch to stacked column chart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CategoryStackedBarChartMenuItem_Click(object sender, EventArgs e)
        {
            if (_view != Views.CategoryBarChart)
            {
                _view = Views.CategoryBarChart;
                measurementVariables.Open(_tableAnalysis, _view);
                CheckChartTypeButton(stackedBarChartToolStripButton);
                CheckChartTypeMenuItem(stackedBarChartToolStripMenuItem);
                BuildChartData();
                PopulateChart();
            }
        }

        /// <summary>
        /// Check specified chart type button and uncheck others
        /// </summary>
        /// <param name="activeButton"></param>
        private void CheckChartTypeButton(ToolStripButton activeButton)
        {
            barChartToolStripButton.Checked = false;
            stackedBarChartToolStripButton.Checked = false;

            activeButton.Checked = true;
        }

        /// <summary>
        /// Check specified chart type button and uncheck others
        /// </summary>
        /// <param name="activeButton"></param>
        private void CheckChartTypeMenuItem(ToolStripMenuItem activeMenuItem)
        {
            barChartToolStripMenuItem.Checked = false;
            stackedBarChartToolStripMenuItem.Checked = false;

            activeMenuItem.Checked = true;
        }

        /// <summary>
        /// Show cases when click on aggregate data grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SummaryDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (summaryDataGridView.CurrentCell == null)
                return;

            int rowIndex = summaryDataGridView.CurrentCell.RowIndex;
            int colIndex = summaryDataGridView.CurrentCell.ColumnIndex;

            if (rowIndex >= 0 && colIndex >= 0)
            {
                if (_view == Views.BarChart || _view == Views.CategoryBarChart)
                {
                    GoToChartRow(rowIndex);
                }
            }
        }

        /// <summary>
        /// Clear case list data grid after aggregate data grid sorted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SummaryDataGridView_Sorted(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            PopulateChart();
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Enable/disable toolbar buttons and menu items 
        /// </summary>
        /// <param name="state"></param>
        private void SetMenuToolButtonState(bool state)
        {
            barChartToolStripButton.Enabled = state;
            barChartToolStripMenuItem.Enabled = state;

            stackedBarChartToolStripButton.Enabled = state;
            copyChartToolStripMenuItem.Enabled = state;
            copyAggregateDataToolStripMenuItem.Enabled = state;
            allCasesToolStripMenuItem.Enabled = state;
            stackedBarChartToolStripMenuItem.Enabled = state;
            toolStripSeparator5.Visible = false;
            allCasesToolStripMenuItem.Visible = false;
        }

        /// <summary>
        /// Handle summary data grid column header click event:
        ///     Sort the data by clicked column.
        ///     If previous sort column is same column, then change
        ///     the sort order from ascending to descending or from 
        ///     descending to ascending
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SummaryDataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (_view == Views.BarChart || _view == Views.CategoryBarChart)
            {
                int colIndex = e.ColumnIndex;
                if (colIndex == _currentSortColumn)
                {
                    _sortAscending = !_sortAscending;
                }
                SortSummaryData(colIndex);
            }
        }

        /// <summary>
        /// Set the summary data grid column sort mode
        /// </summary>
        /// <param name="sortable"></param>
        private void SetSummaryGridSortMode(bool sortable)
        {
            DataGridViewColumnSortMode sortMode = sortable ? DataGridViewColumnSortMode.Programmatic : DataGridViewColumnSortMode.NotSortable;
            for (int i = 0; i < summaryDataGridView.Columns.Count; i++)
            {
                summaryDataGridView.Columns[i].SortMode = sortMode;
            }
        }

        /// <summary>
        /// Handle exclude/include blanks tool bar button click event:
        ///     Toggle exclude and include blanks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExcludeBlanksToolStripButton_Click(object sender, EventArgs e)
        {
            if (excludeBlanksToolStripButton.Checked)
            {
                excludeBlanksToolStripButton.Checked = false;
                this.excludeBlanksToolStripButton.Image = global::OctofyExp.Properties.Resources.no_blanks;
                this.excludeBlanksToolStripButton.Text = Properties.Resources.A030;  //A030: Exclude blanks of Y-axis
                this.excludeBlanksToolStripButton.ToolTipText = Properties.Resources.A030;
            }
            else
            {
                excludeBlanksToolStripButton.Checked = true;
                this.excludeBlanksToolStripButton.Image = global::OctofyExp.Properties.Resources.include_blanks;
                this.excludeBlanksToolStripButton.Text = Properties.Resources.A031; //A031: Include blanks of Y-axis
                this.excludeBlanksToolStripButton.ToolTipText = Properties.Resources.A031;
            }

            BuildChartData();
            PopulateChart();
        }

        /// <summary>
        /// Handle sort menu item click event:
        ///     Sort the data by clicked menu item (column)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSortColumnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_view == Views.CategoryBarChart)
            {
                ToolStripMenuItem menu = (ToolStripMenuItem)sender;
                int colIndex = (int)menu.Tag;
                bool status = menu.Checked;
                //uncheck all
                foreach (ToolStripMenuItem item in sortToolStripDropDownButton.DropDown.Items)
                {
                    if (item.Checked)
                        item.Checked = false;
                }

                status = !status;
                if (colIndex > 0)
                {
                    if (status)
                    {
                        menu.Checked = status;
                        _sortAscending = false;
                        _currentSortColumn = colIndex;
                    }
                    else
                    {
                        _currentSortColumn = 0;
                        _sortAscending = true;
                    }
                }
                else
                {
                    _currentSortColumn = 0;
                    _sortAscending = true;
                }

                SortSummaryData(_currentSortColumn);
            }
        }

        /// <summary>
        /// Perform data sorting on specified column
        /// </summary>
        /// <param name="columnIndex"></param>
        private void SortSummaryData(int columnIndex)
        {
            if (_view == Views.BarChart)
            {
                if (columnIndex > 1)
                    return;
            }

            if (columnIndex >= 0 && columnIndex < summaryDataGridView.Columns.Count)
            {
                _currentSortColumn = columnIndex;
                if (_sortAscending)
                {
                    summaryDataGridView.Sort(summaryDataGridView.Columns[columnIndex],
                        System.ComponentModel.ListSortDirection.Ascending);
                }
                else
                {
                    summaryDataGridView.Sort(summaryDataGridView.Columns[columnIndex],
                        System.ComponentModel.ListSortDirection.Descending);
                }
                ClearSearch();
            }
        }

        /// <summary>
        /// Set the chart selected category to a specified row
        /// </summary>
        /// <param name="rowIndex"></param>
        private void GoToChartRow(int rowIndex)
        {
            chart.SelectCategory(rowIndex);
            rowNumToolStripStatusLabel.Text = string.Format("Rn: {0}", (rowIndex + 1).ToString("N0"));
        }


        /// <summary>
        /// Sort button click event handle:
        /// This button is for bar chart. It toggle the checked status of the button.
        /// After toggle, if the status is checked, then sort the result by the count of category in descending order.
        /// Otherwise, sort by category in ascending order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortToolStripButton_Click(object sender, EventArgs e)
        {
            if (_view == Views.BarChart)
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

                //Sort1DDataGrid();
                SortSummaryData(_currentSortColumn);
            }
        }

        #region"Category Search"
        private readonly List<int> _searchResults = new List<int>();
        private string _searchFor = "";
        private int _currentSeachIndex = 0;

        /// <summary>
        /// Handle search text box key down event:
        ///     When enter key pressed: start search or search for next
        ///     When escape key pressed: earse the current search text
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
                        string columnName = YAxisVariable();
                        if (columnName.Length > 0)
                        {
                            for (int i = 0; i < summaryDataGridView.Rows.Count; i++)
                            {
                                var category = summaryDataGridView.Rows[i].Cells[0].Value.ToString();
                                if (category != BLANKS)
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
            }
            else if (e.KeyCode == Keys.Escape)
            {
                searchToolStripTextBox.Text = "";
                ClearSearch();
                searchToolStripTextBox.Focus();
            }
        }

        /// <summary>
        /// Go to a matched result
        /// </summary>
        /// <param name="index"></param>
        private void GotoSeachResult(int index)
        {
            if (index >= 0 && index < _searchResults.Count)
            {
                _currentSeachIndex = index;
                GoToChartRow(_searchResults[index]);
                GotoSummaryDataGridRow(_searchResults[index]);

                searchResultToolStripLabel.Text = string.Format("{0}/{1}", index + 1, _searchResults.Count);
                searchResultToolStripLabel.Visible = true;
            }
        }

        /// <summary>
        /// Handle next button click event:
        ///     Go to next matched item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextToolStripButton_Click(object sender, EventArgs e)
        {
            SearchNext();
        }

        /// <summary>
        /// Perform go to next action
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
        /// Previous button click event:
        ///     Go to previous matched search result
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
        private void DataAnalysisForm_KeyDown(object sender, KeyEventArgs e)
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
        /// Handle search text box text change event:
        ///     Clear current results
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
        }
        #endregion


        private bool BuildFreqencyData()
        {
            string yColumn = YAxisVariable();
            bool excludeBlanks = excludeBlanksToolStripButton.Checked;
            var categories = _tableAnalysis.GetSortedColumnUniqueValues(yColumn, excludeBlanks);
            var values = _tableAnalysis.BuildFrequencyTable(yColumn, excludeBlanks);
            _numOfBlanks = _tableAnalysis.GetNumOfBlanks(yColumn);
            PopulateAggregateDataGrid(yColumn, categories, values);
            return true;
        }

    }
}