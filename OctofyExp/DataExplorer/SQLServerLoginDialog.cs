using System;
using System.Windows.Forms;

namespace OctofyExp
{
    public partial class SQLServerLoginDialog : Form
    {
        public SQLServerLoginDialog()
        {
            InitializeComponent();
        }

        public string ConnectionString { get; set; }
        public string ServerName { get; set; }
        public string DatabaseName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public short Authentication { get; set; }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (BuildConnectionString() == true)
            {
                ServerName = serverNameTextBox.Text;
                DatabaseName = databaseTextBox.Text;
                UserName = userNameTextBox.Text;
                Password = passwordTextBox.Text;
                Authentication = (short)authenticationComboBox.SelectedIndex;
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
                bool integratedSecurity = authenticationComboBox.SelectedIndex == 0;
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
            }
            else
            {
                userNameTextBox.Enabled = true;
                passwordTextBox.Enabled = true;
            }

            BuildConnectionString();
            EnableOKButton();
        }

        private void ServerNameTextBox_TextChanged(object sender, EventArgs e)
        {
            BuildConnectionString();
            EnableOKButton();
        }

        private void EnableOKButton()
        {
            bool enabled = (serverNameTextBox.Text.Trim().Length > 1 && databaseTextBox.Text.Trim().Length > 1);
            if (enabled)
            {
                if (authenticationComboBox.SelectedIndex == 1)
                {
                    enabled = (userNameTextBox.Text.Trim().Length > 1 && passwordTextBox.Text.Trim().Length > 1);
                }
            }

            okButton.Enabled = enabled;
        }
    }
}