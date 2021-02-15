using System;
using System.Drawing;
using System.Windows.Forms;

namespace OctofyExp
{
    public partial class DBExpoMain : Form
    {

        class ConnectionMenuItem : ToolStripMenuItem
        {
            public ConnectionMenuItem(SQLDatabaseConnectionItem connectionItem)
                : base(connectionItem.Name)
            {
                Connection = connectionItem;
            }

            public string ConnectionName
            {
                get { return Connection.Name; }
            }

            public SQLDatabaseConnectionItem Connection { get; set; }

            public override string ToString()
            {
                if (Connection != null)
                    return Connection.Name;
                else
                    return "";
            }
        }


        public DBExpoMain()
        {
            InitializeComponent();
        }

        private readonly SQLServerConnections _connections = new SQLServerConnections();
        private SQLDatabaseConnectionItem _selectedConnection;
        private string _currentTable = "";

        /// <summary>
        /// Form closing event handle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DBExpoMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //My.Settings.searchHistory = ctlSerachPanel.SearchHistory
            if (dataSourcesToolStripComboBox.SelectedItem != null)
            {
                var connection = (SQLDatabaseConnectionItem)dataSourcesToolStripComboBox.SelectedItem;
                Properties.Settings.Default.LastAccessConnection = connection.Name;
            }
            else
            {
                Properties.Settings.Default.LastAccessConnection = "";
            }
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Form loading event handle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DBExpoMain_Load(object sender, EventArgs e)
        {
            _connections.Load();

            PopulateConnections();

            if (dataSourcesToolStripComboBox.Items.Count > 0)
            {
                int index = 0;
                var lastConnection = Properties.Settings.Default.LastAccessConnection;
                if (lastConnection.Length > 0)
                {
                    for (int i = 0; i < dataSourcesToolStripComboBox.Items.Count; i++)
                    {
                        var item = (SQLDatabaseConnectionItem)dataSourcesToolStripComboBox.Items[i];
                        if (item.Name == lastConnection)
                        {
                            index = i;
                            break;
                        }
                    }
                }

                dataSourcesToolStripComboBox.SelectedIndex = index;
            }

            int numOfRows = Properties.Settings.Default.NumOfRowOnDoubleClick;
            dbObjectsTree.DefaultNumOfRows = numOfRows;
            serachPanel.DefaultNumOfRows = numOfRows;
            quickAnalysisToolStripMenuItem.Text = string.Format("Quick analysis on top {0} rows", numOfRows);

            //ctlSerachPanel.SearchHistory = My.Settings.searchHistory
        }

        /// <summary>
        /// Populate connections to menu and combobox
        /// </summary>
        private void PopulateConnections()
        {
            // clear menu items under Connect to... menu
            for (int i = connectToToolStripMenuItem.DropDown.Items.Count - 1; i >= 0; i--)
            {
                ConnectionMenuItem submenuitem = (ConnectionMenuItem)connectToToolStripMenuItem.DropDown.Items[i];
                submenuitem.Click -= OnConnectionToolStripMenuItem_Click;
                connectToToolStripMenuItem.DropDownItems.RemoveAt(i);
            }

            // clear connections combobox
            if (dataSourcesToolStripComboBox.Items.Count > 0)
                dataSourcesToolStripComboBox.Items.Clear();

            var connections = _connections.Connections;
            if (connections.Count == 0)
                AddConnection();
            else
            {
                for (int i = 0; i < connections.Count; i++)
                {
                    var item = connections[i];
                    dataSourcesToolStripComboBox.Items.Add(item);

                    var submenuitem = new ConnectionMenuItem(item)
                    {
                        Name = string.Format("ConnectionMenuItem{0}", i + 1),
                        Size = new Size(300, 26),
                    };
                    submenuitem.Click += OnConnectionToolStripMenuItem_Click;
                    connectToToolStripMenuItem.DropDown.Items.Add(submenuitem);
                }
            }
        }

        /// <summary>
        /// Search panel AfterSelect event handle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSerachPanel_AfterSelect(object sender, EventArgs e)
        {
            _currentTable = serachPanel.SelectedTable;
            columnView.Open(serachPanel.SelectedTable, serachPanel.SearchFor);
            tableToolStripMenuItem.Enabled = (_currentTable.Length > 0);
            messageToolStripStatusLabel.Text = _currentTable;
        }

        /// <summary>
        /// Handle DB connection combo box selected index changed event:
        ///     Change to the new selected connection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatabasewToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataSourcesToolStripComboBox.SelectedItem != null)
            {
                statusToolStripStatusLabe.Text = string.Format("Connect to {0}...", dataSourcesToolStripComboBox.SelectedItem.ToString());
                Cursor = Cursors.WaitCursor;
                Application.DoEvents();

                ChangeDBConnection((SQLDatabaseConnectionItem)dataSourcesToolStripComboBox.SelectedItem);

                Cursor = Cursors.Default;
                statusToolStripStatusLabe.Text = "";
            }
        }

        /// <summary>
        /// Change current database connection to a new connection
        /// </summary>
        /// <param name="connection">New db connection</param>
        private void ChangeDBConnection(SQLDatabaseConnectionItem connection)
        {
            if (connection != null)
            {
                bool connectionChanged = false;
                if (_selectedConnection == null)
                    connectionChanged = true;
                else
                {
                    if (!_selectedConnection.Equals(connection))
                        connectionChanged = true;
                }

                if (connectionChanged)
                {
                    _selectedConnection = connection;
                    serverToolStripStatusLabel.Text = "";
                    databaseToolStripStatusLabel.Text = "";

                    columnView.Clear();
                    serachPanel.ConnectionString = "";
                    dbObjectsTree.Clear();

                    string connectionString;
                    if (connection.ConnectionString.Length == 0)
                        connectionString = connection.Login();
                    else
                        connectionString = connection.ConnectionString;

                    //else
                    //{
                    //    connectionString = connection.ConnectionString;
                    //    if (connectionString.Length == 0)
                    //        connectionString = connection.Login();
                    //}

                    serachPanel.ConnectionString = connectionString;
                    columnView.ConnectionString = connectionString;

                    string errMessage;
                    if (connectionString.Length > 0)
                    {
                        errMessage = dbObjectsTree.Open(connectionString, connection.Name);

                        if (errMessage.Length > 0)
                        {
                            serachPanel.ConnectionString = "";
                            columnView.ConnectionString = "";
                            connection.ConnectionString = "";
                            connection.Password = "";
                        }
                        else
                        {
                            serverToolStripStatusLabel.Text = connection.ServerName;
                            databaseToolStripStatusLabel.Text = connection.Database;
                        }
                    }
                    else
                    {
                        errMessage = "Failed to connect to the data source.";
                    }

                    if (errMessage.Length > 0)
                        MessageBox.Show(errMessage, "Connection failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Handle connection menu item click event:
        ///     Open selected connection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender.GetType() == typeof(ConnectionMenuItem))
            {
                var menuItem = (ConnectionMenuItem)sender;
                for (int i = 0; i < dataSourcesToolStripComboBox.ComboBox.Items.Count; i++)
                {
                    if (menuItem.Connection.Equals((SQLDatabaseConnectionItem)dataSourcesToolStripComboBox.ComboBox.Items[i]))
                        dataSourcesToolStripComboBox.SelectedIndex = i;

                }
                //statusToolStripStatusLabe.Text = string.Format("Connect to {0}...", menuItem.Connection.ToString());
                //Cursor = Cursors.WaitCursor;
                //Application.DoEvents();

                //ChangeDBConnection(menuItem.Connection);

                //Cursor = Cursors.Default;
                //statusToolStripStatusLabe.Text = "";
            }
        }

        /// <summary>
        /// Handle database object tree AfterSelect event:
        ///     Open columns in column control.
        ///     Enable/disable Table menu 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DbObjects_AfterSelect(object sender, EventArgs e)
        {
            if (sender.GetType() == typeof(DBTableViewNode))
            {
                var node = (DBTableViewNode)sender;
                columnView.Open(string.Format("{0}.{1}", node.SchemaName, node.TableName), "");
            }
            else
            {
                columnView.Open("", "");
            }

            _currentTable = dbObjectsTree.SelectedTable;
            tableToolStripMenuItem.Enabled = (dbObjectsTree.SelectedTable.Length > 0);
            messageToolStripStatusLabel.Text = dbObjectsTree.SelectedTable;
        }

        /// <summary>
        /// Add button click event handle:
        ///   Add a new connection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddToolStripButton_Click(object sender, EventArgs e)
        {
            if (AddConnection())
                if (dataSourcesToolStripComboBox.Items.Count > 0)
                    dataSourcesToolStripComboBox.SelectedIndex = dataSourcesToolStripComboBox.Items.Count - 1;

        }

        /// <summary>
        /// Open add connection dialog and start to add a new database connection
        /// </summary>
        /// <returns></returns>
        private bool AddConnection()
        {
            using (var dlg = new NewSQLServerConnectionDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    var connection = new SQLDatabaseConnectionItem()
                    {
                        Name = dlg.ConnectionName,
                        ServerName = dlg.ServerName,
                        Database = dlg.DatabaseName,
                        AuthenticationType = dlg.Authentication,
                        UserName = dlg.UserName
                    };
                    if (dlg.Authentication == 0)
                        connection.BuildConnectionString();
                    _connections.Add(dlg.ConnectionName, dlg.ServerName, dlg.DatabaseName, dlg.Authentication, dlg.UserName, connection.ConnectionString, dlg.RememberPassword);
                    _connections.Save();

                    dataSourcesToolStripComboBox.Items.Add(connection);

                    var submenuitem = new ConnectionMenuItem(connection)
                    {
                        Name = string.Format("ConnectionMenuItem{0}", dataSourcesToolStripComboBox.Items.Count + 1),
                        Size = new Size(300, 26),
                    };
                    submenuitem.Click += OnConnectionToolStripMenuItem_Click;
                    connectToToolStripMenuItem.DropDown.Items.Add(submenuitem);

                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Handler OnAnylysisTable event from both DB object tree control and Search panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAnalysisTable(object sender, OnAnalysisTableEventArgs e)
        {
            AnalysisTable(e.TableName, e.NumOfRows, e.ConnectionString);
        }


        /// <summary>
        /// Open DataAnalysisForm for the specified table
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="numOfRows"></param>
        /// <param name="connectionString"></param>
        private void AnalysisTable(string tableName, int numOfRows, string connectionString)
        {
            try
            {
                //Ensure table object in column control is same as selected table
                if (columnView.ObjectName != tableName)
                {
                    columnView.Open(tableName, "");
                }

                //Get SELECT statement from column control
                var sql = columnView.SafeSQL(numOfRows);
                if (sql.Length > 0)
                {
                    using (var frm = new DataAnalysisForm()
                    {
                        TableName = tableName,
                        NumberOfTopRows = numOfRows,
                        ConnectionString = connectionString,
                        TableSelectSQL = sql
                    })
                    {
                        _ = frm.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("There is no column in the table take can used for analysis.", "No categorical column", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "An error occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// OnPreviewData event handler:
        ///     Show table data in PreviewDataForm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPreviewData(object sender, OnAnalysisTableEventArgs e)
        {
            PreviewTableData(e.TableName, e.ConnectionString);
        }

        /// <summary>
        /// Open PreviewDataForm for the specified table
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="connectionString"></param>
        private void PreviewTableData(string tableName, string connectionString)
        {
            try
            {
                if (columnView.ObjectName != tableName)
                {
                    columnView.Open(tableName, "");
                }
                var columns = columnView.CategoricalColumns(true);
                if (columns.Length > 0)
                {
                    using (var frm = new PreviewDataForm()
                    {
                        TableName = tableName,
                        ConnectionString = connectionString,
                        Columns = columns
                    })
                    {
                        frm.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "An error occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Manage Connections menu item click event handler:
        ///     Manage the data connections
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManageConnectionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var frm = new ConnectionManageForm() { DataConnections = _connections })
            {
                string currentSelection = "";
                if (dataSourcesToolStripComboBox.SelectedItem != null)
                {
                    var selectedItem = (SQLDatabaseConnectionItem)(dataSourcesToolStripComboBox.SelectedItem);
                    currentSelection = selectedItem.Name;
                }
                frm.ShowDialog();
                PopulateConnections();

                if (dataSourcesToolStripComboBox.Items.Count > 0)
                {
                    int index = 0;
                    for (int i = 0; i < dataSourcesToolStripComboBox.Items.Count; i++)
                    {
                        var item = (SQLDatabaseConnectionItem)(dataSourcesToolStripComboBox.Items[i]);
                        if (item.Name == currentSelection)
                        {
                            index = i;
                            break;
                        }
                    }
                    dataSourcesToolStripComboBox.SelectedIndex = index;
                }
            }
        }

        /// <summary>
        /// Handle About menu item click event:
        ///     Show About dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dlg = new OctofySplashScreen())
            {
                dlg.ShowDialog();
            }
        }

        /// <summary>
        /// Handle Exit menu item click event:
        ///     Close the app.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Handle tool bar menu item click event:
        ///     Toggle show/hide tool bar on the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool toolBarVisible = !toolBarToolStripMenuItem.Checked;
            toolBarToolStripMenuItem.Checked = toolBarVisible;
            toolStrip.Visible = toolBarVisible;
        }

        /// <summary>
        /// Handle status bar menu item click event:
        ///     Toggle show/hide status bar on the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool statusBarVisible = !statusBarToolStripMenuItem.Checked;
            statusBarToolStripMenuItem.Checked = statusBarVisible;
            statusStrip.Visible = statusBarVisible;
        }

        /// <summary>
        /// Handle options menu item click event:
        ///     Show Options dialog.
        ///     Update app options after setting.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dlg = new OptionsForm())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    int numOfRows = Properties.Settings.Default.NumOfRowOnDoubleClick;
                    dbObjectsTree.DefaultNumOfRows = numOfRows;
                    serachPanel.DefaultNumOfRows = numOfRows;
                    quickAnalysisToolStripMenuItem.Text = string.Format("Quick analysis on top {0} rows", numOfRows);
                }
            }
        }

        /// <summary>
        /// Handle quick analysis menu item click event:
        ///     Open analysis form with NumOfRowOnDoubleClick rows
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuickAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_currentTable.Length > 0 && _selectedConnection != null)
                if (_selectedConnection.ConnectionString.Length > 0)
                    AnalysisTable(_currentTable, Properties.Settings.Default.NumOfRowOnDoubleClick, _selectedConnection.ConnectionString);
        }

        /// <summary>
        /// Handle table analysis menu item click event:
        ///     Open analysis form on entire table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnalysisOnEntireTableviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_currentTable.Length > 0 && _selectedConnection != null)
                if (_selectedConnection.ConnectionString.Length > 0)
                    AnalysisTable(_currentTable, -1, _selectedConnection.ConnectionString);
        }

        /// <summary>
        /// Handle Preview table menu item click event:
        ///     Open current selected table in PreviewData Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviewDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_currentTable.Length > 0 && _selectedConnection != null)
                if (_selectedConnection.ConnectionString.Length > 0)
                    PreviewTableData(_currentTable, _selectedConnection.ConnectionString);
        }

        /// <summary>
        /// Handle copy table name menu item click event:
        ///     Copy selected table name to the clipboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyTableviewNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_currentTable.Length > 0)
                Clipboard.SetText(_currentTable);
        }

        /// <summary>
        /// Handle frequency menu item click event:
        ///     Show column frequency
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrequcneiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowColumnFrequency(columnView.ObjectName, columnView.SelectedColumn);
        }

        /// <summary>
        /// Handle copy column name menu item click event:
        ///     Copy the selected column name to the clipboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyColumnNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string columnName = columnView.SelectedColumn;
            if (columnName.Length > 0)
                Clipboard.SetText(columnName);
        }

        //private void OnSelectedTableChanged(object sender, EventArgs e)
        //{
        //    tableToolStripMenuItem.Enabled = (dbObjectsTree.SelectedTable.Length > 0);
        //    messageToolStripStatusLabel.Text = dbObjectsTree.SelectedTable;
        //}

        /// <summary>
        /// Handle column control OnColumnFrequency event:
        ///     Show column frequency
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnColumnFrequency(object sender, EventArgs e)
        {
            ShowColumnFrequency(columnView.ObjectName, columnView.SelectedColumn);
        }

        /// <summary>
        /// Open ColumnFrequencyForm and show column value frequencies for the
        /// specified column
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        private void ShowColumnFrequency(string tableName, string columnName)
        {
            try
            {
                if (tableName.Length > 0 && columnName.Length > 0)
                {
                    using (var frm = new ColumnFrequencyForm()
                    {
                        ConnectionString = _selectedConnection.ConnectionString,
                        TableName = tableName,
                        ColumnName = columnName
                    })
                        _ = frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "An error occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handle column control SelectedColumnChanged event:
        ///     Enable/disable column menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ColumnView_SelectedColumnChanged(object sender, EventArgs e)
        {
            columnToolStripMenuItem.Enabled = (columnView.SelectedColumn.Length > 0);
        }
    }
}
