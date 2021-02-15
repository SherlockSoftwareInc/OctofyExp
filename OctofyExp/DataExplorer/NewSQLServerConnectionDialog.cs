using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace OctofyExp
{

    /// <summary>
    /// The dialog is used to add a sql server database connection
    public partial class NewSQLServerConnectionDialog : Form
    {
        string _serverName = "";

        public NewSQLServerConnectionDialog()
        {
            InitializeComponent();
        }
        public string ConnectionName { get; set; }
        public string ConnectionString { get; set; }
        public string ServerName { get; set; }
        public string DatabaseName { get; set; }
        public string UserName { get; set; }
        public short Authentication { get; set; }
        public bool RememberPassword { get; set; }
        public string Password { get; set; }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (BuildConnectionString() == true)
            {
                ServerName = serverNameTextBox.Text;
                DatabaseName = databaseTextBox.Text;
                ConnectionName = string.Format("{0} @ {1}", DatabaseName, ServerName);
                UserName = userNameTextBox.Text;
                Authentication = (short)authenticationComboBox.SelectedIndex;
                Password = passwordTextBox.Text;
                RememberPassword = rememberPasswordCheckBox.Checked;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                Console.Beep();
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            ConnectionString = string.Empty;
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ConnectionStringDialog_Load(object sender, EventArgs e)
        {
            serverNameTextBox.Text = ServerName;
            databaseTextBox.Text = DatabaseName;
            userNameTextBox.Text = UserName;
            authenticationComboBox.SelectedIndex = Authentication;
            EnableOKButton();
            if (Authentication == 1)
                passwordTextBox.Focus();
        }

        private bool BuildConnectionString()
        {
            var result = default(bool);
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

                ConnectionString = builder.ConnectionString;
                result = true;
            }

            return result;
        }

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

            BuildConnectionString();
            EnableOKButton();
        }

        private void ServerNameTextBox_TextChanged(object sender, EventArgs e)
        {
            string server = serverNameTextBox.Text;
            if (server != _serverName)
            {
                if (databaseTextBox.SelectedIndex >= 0)
                {
                    databaseTextBox.SelectedIndex = -1;
                    databaseTextBox.Items.Clear();
                    okButton.Enabled = false;
                }
            }
        }

        private void OnSettingsChanged(object sender, EventArgs e)
        {
            BuildConnectionString();
            EnableOKButton();
        }

        /// <summary>
        /// Enable OK button:
        /// connection name, server name, database name is required.
        /// If use sql server login, user name is required
        /// </summary>
        private void EnableOKButton()
        {
            bool enabled = (serverNameTextBox.Text.Trim().Length > 1 &&
                databaseTextBox.Text.Trim().Length > 1);
            if (enabled)
            {
                if (authenticationComboBox.SelectedIndex == 1)
                {
                    enabled = (userNameTextBox.Text.Trim().Length > 1 && passwordTextBox.Text.Trim().Length > 1);
                }
            }

            okButton.Enabled = enabled;
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
                BuildConnectionString();
                EnableOKButton();
            }
        }
    }
}