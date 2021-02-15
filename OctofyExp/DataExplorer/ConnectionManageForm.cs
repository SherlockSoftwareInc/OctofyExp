using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace OctofyExp
{
    /// <summary>
    /// The dialog is used to manage all database connections. 
    /// Add, edit or remove connection item
    /// </summary>
    public partial class ConnectionManageForm : Form
    {
        private int _selectedIndex = -1;    // index of current connect in the connection list
        //private string _selectedItemGUID = "";
        private string _serverName = "";
        private bool _populating = false;   // populating a connection item indicator
        private bool _changed = false;

        public ConnectionManageForm()
        {
            InitializeComponent();
        }

        private SQLServerConnections _connections = new SQLServerConnections();

        /// <summary>
        /// Gets or sets data connections object
        /// </summary>
        public SQLServerConnections DataConnections
        {
            get { return _connections; }
            set { _connections = value; }
        }

        /// <summary>
        /// Handle form load event:
        ///     Populate all connections and set current item to the first connection item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectionManageForm_Load(object sender, EventArgs e)
        {
            PopulateConnections();
            if (connectionsListBox.Items.Count > 0)
                connectionsListBox.SelectedIndex = 0;
        }

        /// <summary>
        /// Populate connection items to list box
        /// </summary>
        private void PopulateConnections()
        {
            connectionsListBox.Items.Clear();
            if (_connections != null)
            {
                foreach (var item in _connections.Connections)
                {
                    connectionsListBox.Items.Add(item);
                }
            }
            saveButton.Visible = false;
        }

        /// <summary>
        /// Handle connection list box selected index change event:
        ///     Populate connection setting details to the detail section for
        ///     selected item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectionsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_changed && _selectedIndex >= 0)
            {
                Save();
            }
            PopulateSelectedItem();
        }

        /// <summary>
        /// Populate the selected connection item
        /// </summary>
        private void PopulateSelectedItem()
        {
            var item = (SQLDatabaseConnectionItem)connectionsListBox.SelectedItem;
            if (item != null)
            {
                _populating = true;
                connectionGroupBox.Text = item.Name;
                connectionNameTextBox.Text = item.Name;
                serverNameTextBox.Text = item.ServerName;
                databaseTextBox.Text = item.Database;
                authenticationComboBox.SelectedIndex = item.AuthenticationType;
                userNameTextBox.Text = item.UserName;
                rememberPasswordCheckBox.Checked = item.RememberPassword;
                if (item.RememberPassword)
                {
                    passwordTextBox.Text = item.Password;
                }
                else
                {
                    passwordTextBox.Text = "";
                }
                _selectedIndex = connectionsListBox.SelectedIndex;
                //_selectedItemGUID = item.GUID;

                _populating = true;
                if (item.IsCustom)
                {
                    if (!useCustomConectionCheckBox.Checked)
                    {
                        useCustomConectionCheckBox.Checked = true;
                    }
                    connectionStringTextBox.Text = item.ConnectionString;
                    authenticationGroupBox.Enabled = false;
                    connectionStringTextBox.Focus();
                }
                else
                {
                    if (useCustomConectionCheckBox.Checked)
                    {
                        useCustomConectionCheckBox.Checked = false;
                    }
                    connectionStringTextBox.Text = item.ConnectionString;

                    if (_serverName != item.ServerName && item.ServerName.Length > 1)
                    {
                        _serverName = item.ServerName;

                        databaseTextBox.Items.Clear();

                        _serverName = item.ServerName;
                        Cursor = Cursors.WaitCursor;
                        var databases = GetDatabases(item.ServerName);
                        if (databases.Count > 0)
                        {
                            databaseTextBox.Items.AddRange(databases.ToArray());
                        }

                        Cursor = Cursors.Default;
                    }
                }

                connectionGroupBox.Enabled = true;
            }
            _populating = false;
        }

        /// <summary>
        /// Clear connection setting details section
        /// </summary>
        private void Clear()
        {
            _populating = true;
            _selectedIndex = -1;
            connectionGroupBox.Text = "";
            connectionNameTextBox.Text = "";
            serverNameTextBox.Text = "";
            databaseTextBox.Text = "";
            userNameTextBox.Text = "";
            passwordTextBox.Text = "";
            rememberPasswordCheckBox.Checked = false;
            connectionGroupBox.Enabled = false;
            useCustomConectionCheckBox.Checked = false;
            connectionStringTextBox.Text = "";
            _serverName = "";
            //testButton.Visible = false;
            _changed = false;
        }

        /// <summary>
        /// Build SQL server connection string based on current settings
        /// </summary>
        /// <returns></returns>
        private string BuildConnectionString()
        {
            string result = "";
            string serverName = serverNameTextBox.Text.Trim();
            string dbName = databaseTextBox.Text;
            if (serverName.Length > 0 & dbName.Length > 0)
            {
                bool integratedSecurity = (authenticationComboBox.SelectedIndex == 0);
                var builder = new System.Data.SqlClient.SqlConnectionStringBuilder()
                {
                    DataSource = serverName,
                    InitialCatalog = dbName,
                    IntegratedSecurity = integratedSecurity
                };
                if (integratedSecurity == false)
                {
                    builder.UserID = userNameTextBox.Text;
                    builder.Password = passwordTextBox.Text;
                }

                result = builder.ConnectionString;
            }
            return result;
        }

        /// <summary>
        /// Handle authentication combo box selected index change event:
        ///     Disable user name and password text box when selection is
        ///     Windows authentication
        ///     Otherwise, enable user name text box and password text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AuthenticationComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (authenticationComboBox.SelectedIndex == 0)
            {
                userNameTextBox.Enabled = false;
                passwordTextBox.Enabled = false;
                rememberPasswordCheckBox.Enabled = false;
            }
            else
            {
                userNameTextBox.Enabled = true;
                passwordTextBox.Enabled = true;
                rememberPasswordCheckBox.Enabled = true;
            }
            if (!_populating)
            {
                _changed = true;
            }
            connectionStringTextBox.Text = BuildConnectionString();
        }

        /// <summary>
        /// Save changes of the settings
        /// </summary>
        private void Save()
        {
            if (!_populating && _selectedIndex >= 0 && _changed)
            {
                bool canSave = (serverNameTextBox.Text.Trim().Length > 1 &&
                    databaseTextBox.Text.Trim().Length > 1);
                if (canSave && !useCustomConectionCheckBox.Checked)
                {
                    if (authenticationComboBox.SelectedIndex == 1)
                    {
                        canSave = (userNameTextBox.Text.Trim().Length > 1 && passwordTextBox.Text.Trim().Length > 1);
                    }
                }

                if (useCustomConectionCheckBox.Checked)
                {
                    if (canSave && connectionStringTextBox.Text.Length > 1 && connectionNameTextBox.Text.Trim().Length > 1)
                    {
                        var item = _connections.Connections[_selectedIndex];
                        item.Name = connectionNameTextBox.Text;
                        item.ServerName = serverNameTextBox.Text;
                        item.Database = databaseTextBox.Text;
                        item.AuthenticationType = (short)authenticationComboBox.SelectedIndex;
                        item.UserName = userNameTextBox.Text;
                        item.Password = passwordTextBox.Text;
                        item.RememberPassword = rememberPasswordCheckBox.Checked;
                        item.IsCustom = true;
                        item.ConnectionString = connectionStringTextBox.Text;
                        _connections.SaveTemp();
                        _changed = false;
                    }
                }
                else
                {
                    string connStr = BuildConnectionString();
                    if (canSave && connStr.Length > 1 && connectionNameTextBox.Text.Trim().Length > 1)
                    {
                        if (connectionStringTextBox.ReadOnly)
                        {
                            connectionStringTextBox.Text = connStr;
                        }
                        var item = _connections.Connections[_selectedIndex];
                        item.Name = connectionNameTextBox.Text;
                        item.ServerName = serverNameTextBox.Text;
                        item.Database = databaseTextBox.Text;
                        item.AuthenticationType = (short)authenticationComboBox.SelectedIndex;
                        item.UserName = userNameTextBox.Text;
                        item.Password = passwordTextBox.Text;
                        item.RememberPassword = rememberPasswordCheckBox.Checked;
                        item.ConnectionString = connStr;
                        item.IsCustom = false;
                        _connections.SaveTemp();
                        _changed = false;
                    }
                }
            }
        }

        /// <summary>
        /// Handle any change to the setting:
        ///     set change flag
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsChanged(object sender, EventArgs e)
        {
            if (!_populating)
            {
                _changed = true;
            }
        }

        private void FieldVerified(object sender, EventArgs e)
        {
            if (!_populating)
            {
                connectionStringTextBox.Text = BuildConnectionString();
            }
        }

        /// <summary>
        /// Handle Add tool bar button click event:
        ///     Start a new connection item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddToolStripButton_Click(object sender, EventArgs e)
        {
            Clear();
            connectionGroupBox.Text = Properties.Resources.A057;    //A057: New
            connectionGroupBox.Enabled = true;
            connectionsListBox.SelectedIndex = -1;
            connectionNameTextBox.Text = "";
            saveButton.Visible = true;
            _changed = false;
            //serverNameTextBox.Focus();
            connectionNameTextBox.Focus();
        }

        /// <summary>
        /// Handle tool bar delete button click event:
        ///     Delete selected connection item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteToolStripButton_Click(object sender, EventArgs e)
        {
            if (connectionsListBox.SelectedItem != null && _selectedIndex >= 0)
            {
                var item = (SQLDatabaseConnectionItem)(connectionsListBox.SelectedItem);
                string msg = string.Format(Properties.Resources.A058, item.Name);   //A058: Do you want to delete {0}?
                if (MessageBox.Show(msg, Properties.Resources.A059,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)  //A059: Delete Connection Item
                {
                    DataConnections.RemoveAt(_selectedIndex);
                    _selectedIndex = -1;
                    PopulateConnections();
                    if (connectionsListBox.Items.Count > 0)
                        connectionsListBox.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Handle tool bar close button click event:
        ///     Close the dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseToolStripButton_Click(object sender, EventArgs e)
        {
            if (_changed && _selectedIndex >= 0)
            {
                Save();
            }

            _connections.Save();
            Close();
        }

        /// <summary>
        /// Handle tool bar save button click event:
        ///     Save the changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            bool canAdd = connectionNameTextBox.Text.Trim().Length > 0 &&
                serverNameTextBox.Text.Trim().Length > 0 &&
                databaseTextBox.Text.Trim().Length > 0;

            if (canAdd)
            {
                if (authenticationComboBox.SelectedIndex == 1)
                {
                    canAdd = (userNameTextBox.Text.Trim().Length > 1 && passwordTextBox.Text.Trim().Length > 1);
                }
            }

            if (canAdd)
            {
                DataConnections.Add(connectionNameTextBox.Text,
                    serverNameTextBox.Text,
                    databaseTextBox.Text,
                    (short)authenticationComboBox.SelectedIndex,
                    userNameTextBox.Text,
                    passwordTextBox.Text,
                    BuildConnectionString(),
                    rememberPasswordCheckBox.Checked);

                PopulateConnections();

                if (connectionsListBox.Items.Count > 0)
                    connectionsListBox.SelectedIndex = connectionsListBox.Items.Count - 1;

            }
            else
            {
                MessageBox.Show(Properties.Resources.A060, Properties.Resources.A061, MessageBoxButtons.OK);
                //A060: Please complete the fields above.
                //A061: 
            }
        }

        /// <summary>
        /// Get database list of a sql server
        /// </summary>
        /// <param name="serverName"></param>
        /// <returns></returns>
        private List<string> GetDatabases(string serverName)
        {
            List<String> databases = new List<String>();
            try
            {
                SqlConnectionStringBuilder connection = new SqlConnectionStringBuilder
                {
                    DataSource = serverName,
                    IntegratedSecurity = true
                };

                String strConn = connection.ToString();
                SqlConnection sqlConn = new SqlConnection(strConn);
                sqlConn.Open();

                //get databases
                DataTable tblDatabases = sqlConn.GetSchema("Databases");

                sqlConn.Close();

                foreach (DataRow row in tblDatabases.Rows)
                {
                    String strDatabaseName = row["database_name"].ToString();

                    databases.Add(strDatabaseName);
                }
            }
            catch (SqlException)
            {
                //Ignore the error
            }
            return databases;
        }

        /// <summary>
        /// Handle server name validated event:
        ///     When server name changed, get the databases in the server
        ///     and list them in the database name combo box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ServerNameTextBox_Validated(object sender, EventArgs e)
        {
            string server = serverNameTextBox.Text.Trim();
            if (server != _serverName && server.Length > 1)
            {
                databaseTextBox.Items.Clear();

                _serverName = server;
                Cursor = Cursors.WaitCursor;
                var databases = GetDatabases(server);
                if (databases.Count > 0)
                {
                    databaseTextBox.Items.AddRange(databases.ToArray());
                }

                Cursor = Cursors.Default;
            }
            connectionStringTextBox.Text = BuildConnectionString();
        }

        /// <summary>
        /// Handle database name selected index change event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatabaseTextBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (databaseTextBox.SelectedIndex >= 0)
            {
                connectionStringTextBox.Text = BuildConnectionString();
                if (!_populating)
                {
                    _changed = true;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UseCustomConectionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (useCustomConectionCheckBox.Checked)
            {
                serverNameTextBox.Enabled = false;
                databaseTextBox.Enabled = false;
                authenticationGroupBox.Enabled = false;
                connectionStringTextBox.ReadOnly = false;
                //testButton.Visible = true;

                if (_selectedIndex >= 0)
                {
                    _connections.Connections[_selectedIndex].IsCustom = true;
                    //Save();
                }
                connectionStringTextBox.Focus();
            }
            else
            {
                if (_selectedIndex >= 0)
                {
                    _connections.Connections[_selectedIndex].IsCustom = false;
                    PopulateSelectedItem();
                    //Save();
                    connectionStringTextBox.Text = BuildConnectionString();
                }

                serverNameTextBox.Enabled = true;
                databaseTextBox.Enabled = true;
                authenticationGroupBox.Enabled = true;
                connectionStringTextBox.ReadOnly = true;
                connectionStringTextBox.Text = BuildConnectionString();
                //testButton.Visible = false;
            }

            if (!_populating)
            {
                _changed = true;
            }
        }

        /// <summary>
        /// Test button click event handle:
        /// Check if custom connection works or not
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestButton_Click(object sender, EventArgs e)
        {
            bool result = false;
            Cursor = Cursors.WaitCursor;
            string connectionString = connectionStringTextBox.Text.Trim();
            if (connectionString.Length > 1)
            {
                try
                {
                    SqlConnection sqlConn = new SqlConnection(connectionString);
                    sqlConn.Open();

                    //get databases
                    DataTable tblDatabases = sqlConn.GetSchema("Databases");

                    sqlConn.Close();

                    result = true;
                }
                catch (SqlException)
                {
                    //Ignore the error
                }
                catch (Exception)
                {
                }
                if (result)
                {
                    MessageBox.Show("Succeed!", "Connection testing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to connect to the database!", "Connection testing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter database connection string.", "Connection testing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Cursor = Cursors.Default;
        }

        private void ConnectionStringTextBox_Validated(object sender, EventArgs e)
        {
            //if (useCustomConectionCheckBox.Checked)
            //{
            //    Save();
            //}
        }

        /// <summary>
        /// Close the dialog without save the changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelToolStripButton1_Click(object sender, EventArgs e)
        {
            _connections.Load();
            Close();
        }

        /// <summary>
        /// Handle text change event of connection string text box 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectionStringTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!connectionStringTextBox.ReadOnly && !_populating)
            {
                _changed = true;
            }
        }
    }
}
