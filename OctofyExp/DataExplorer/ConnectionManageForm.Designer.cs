namespace OctofyExp
{
    partial class ConnectionManageForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectionManageForm));
            this.connectionsLabel = new System.Windows.Forms.Label();
            this.connectionsListBox = new System.Windows.Forms.ListBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.addToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.deleteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.TestToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.closeToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.cancelToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.connectionGroupBox = new System.Windows.Forms.GroupBox();
            this.useCustomConectionCheckBox = new System.Windows.Forms.CheckBox();
            this.connectionStringTextBox = new System.Windows.Forms.TextBox();
            this.connectionStringLabel = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.databaseTextBox = new System.Windows.Forms.ComboBox();
            this.databaseNameLabel = new System.Windows.Forms.Label();
            this.connectionNameTextBox = new System.Windows.Forms.TextBox();
            this.connectionNameLabel = new System.Windows.Forms.Label();
            this.serverNameTextBox = new System.Windows.Forms.TextBox();
            this.serverNameLabel = new System.Windows.Forms.Label();
            this.authenticationGroupBox = new System.Windows.Forms.GroupBox();
            this.rememberPasswordCheckBox = new System.Windows.Forms.CheckBox();
            this.authenticationLabel = new System.Windows.Forms.Label();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.authenticationComboBox = new System.Windows.Forms.ComboBox();
            this.userNameLabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.userNameTextBox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1.SuspendLayout();
            this.connectionGroupBox.SuspendLayout();
            this.authenticationGroupBox.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // connectionsLabel
            // 
            resources.ApplyResources(this.connectionsLabel, "connectionsLabel");
            this.connectionsLabel.Name = "connectionsLabel";
            // 
            // connectionsListBox
            // 
            this.connectionsListBox.FormattingEnabled = true;
            resources.ApplyResources(this.connectionsListBox, "connectionsListBox");
            this.connectionsListBox.Name = "connectionsListBox";
            this.connectionsListBox.SelectedIndexChanged += new System.EventHandler(this.ConnectionsListBox_SelectedIndexChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripButton,
            this.deleteToolStripButton,
            this.toolStripSeparator1,
            this.TestToolStripButton,
            this.toolStripSeparator2,
            this.closeToolStripButton,
            this.cancelToolStripButton});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // addToolStripButton
            // 
            this.addToolStripButton.Image = global::OctofyExp.Properties.Resources.add;
            resources.ApplyResources(this.addToolStripButton, "addToolStripButton");
            this.addToolStripButton.Name = "addToolStripButton";
            this.addToolStripButton.Click += new System.EventHandler(this.AddToolStripButton_Click);
            // 
            // deleteToolStripButton
            // 
            this.deleteToolStripButton.Image = global::OctofyExp.Properties.Resources.delete_icon;
            resources.ApplyResources(this.deleteToolStripButton, "deleteToolStripButton");
            this.deleteToolStripButton.Name = "deleteToolStripButton";
            this.deleteToolStripButton.Click += new System.EventHandler(this.DeleteToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // TestToolStripButton
            // 
            this.TestToolStripButton.Image = global::OctofyExp.Properties.Resources.checkmark;
            resources.ApplyResources(this.TestToolStripButton, "TestToolStripButton");
            this.TestToolStripButton.Name = "TestToolStripButton";
            this.TestToolStripButton.Click += new System.EventHandler(this.TestButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // closeToolStripButton
            // 
            this.closeToolStripButton.Image = global::OctofyExp.Properties.Resources.save;
            resources.ApplyResources(this.closeToolStripButton, "closeToolStripButton");
            this.closeToolStripButton.Name = "closeToolStripButton";
            this.closeToolStripButton.Click += new System.EventHandler(this.CloseToolStripButton_Click);
            // 
            // cancelToolStripButton
            // 
            this.cancelToolStripButton.Image = global::OctofyExp.Properties.Resources.cancel_icon_16;
            resources.ApplyResources(this.cancelToolStripButton, "cancelToolStripButton");
            this.cancelToolStripButton.Name = "cancelToolStripButton";
            this.cancelToolStripButton.Click += new System.EventHandler(this.CancelToolStripButton1_Click);
            // 
            // connectionGroupBox
            // 
            this.connectionGroupBox.Controls.Add(this.panel1);
            this.connectionGroupBox.Controls.Add(this.saveButton);
            this.connectionGroupBox.Controls.Add(this.databaseTextBox);
            this.connectionGroupBox.Controls.Add(this.databaseNameLabel);
            this.connectionGroupBox.Controls.Add(this.connectionNameTextBox);
            this.connectionGroupBox.Controls.Add(this.connectionNameLabel);
            this.connectionGroupBox.Controls.Add(this.serverNameTextBox);
            this.connectionGroupBox.Controls.Add(this.serverNameLabel);
            this.connectionGroupBox.Controls.Add(this.authenticationGroupBox);
            resources.ApplyResources(this.connectionGroupBox, "connectionGroupBox");
            this.connectionGroupBox.Name = "connectionGroupBox";
            this.connectionGroupBox.TabStop = false;
            // 
            // useCustomConectionCheckBox
            // 
            resources.ApplyResources(this.useCustomConectionCheckBox, "useCustomConectionCheckBox");
            this.useCustomConectionCheckBox.Name = "useCustomConectionCheckBox";
            this.useCustomConectionCheckBox.UseVisualStyleBackColor = true;
            this.useCustomConectionCheckBox.CheckedChanged += new System.EventHandler(this.UseCustomConectionCheckBox_CheckedChanged);
            // 
            // connectionStringTextBox
            // 
            resources.ApplyResources(this.connectionStringTextBox, "connectionStringTextBox");
            this.connectionStringTextBox.Name = "connectionStringTextBox";
            this.connectionStringTextBox.ReadOnly = true;
            this.connectionStringTextBox.TextChanged += new System.EventHandler(this.ConnectionStringTextBox_TextChanged);
            this.connectionStringTextBox.Validated += new System.EventHandler(this.ConnectionStringTextBox_Validated);
            // 
            // connectionStringLabel
            // 
            resources.ApplyResources(this.connectionStringLabel, "connectionStringLabel");
            this.connectionStringLabel.Name = "connectionStringLabel";
            // 
            // saveButton
            // 
            resources.ApplyResources(this.saveButton, "saveButton");
            this.saveButton.Name = "saveButton";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // databaseTextBox
            // 
            resources.ApplyResources(this.databaseTextBox, "databaseTextBox");
            this.databaseTextBox.Name = "databaseTextBox";
            this.databaseTextBox.SelectedIndexChanged += new System.EventHandler(this.DatabaseTextBox_SelectedIndexChanged);
            this.databaseTextBox.TextChanged += new System.EventHandler(this.SettingsChanged);
            this.databaseTextBox.Validated += new System.EventHandler(this.FieldVerified);
            // 
            // databaseNameLabel
            // 
            resources.ApplyResources(this.databaseNameLabel, "databaseNameLabel");
            this.databaseNameLabel.Name = "databaseNameLabel";
            // 
            // connectionNameTextBox
            // 
            resources.ApplyResources(this.connectionNameTextBox, "connectionNameTextBox");
            this.connectionNameTextBox.Name = "connectionNameTextBox";
            this.connectionNameTextBox.TextChanged += new System.EventHandler(this.SettingsChanged);
            // 
            // connectionNameLabel
            // 
            resources.ApplyResources(this.connectionNameLabel, "connectionNameLabel");
            this.connectionNameLabel.Name = "connectionNameLabel";
            // 
            // serverNameTextBox
            // 
            resources.ApplyResources(this.serverNameTextBox, "serverNameTextBox");
            this.serverNameTextBox.Name = "serverNameTextBox";
            this.serverNameTextBox.TextChanged += new System.EventHandler(this.SettingsChanged);
            this.serverNameTextBox.Validated += new System.EventHandler(this.ServerNameTextBox_Validated);
            // 
            // serverNameLabel
            // 
            resources.ApplyResources(this.serverNameLabel, "serverNameLabel");
            this.serverNameLabel.Name = "serverNameLabel";
            // 
            // authenticationGroupBox
            // 
            this.authenticationGroupBox.Controls.Add(this.rememberPasswordCheckBox);
            this.authenticationGroupBox.Controls.Add(this.authenticationLabel);
            this.authenticationGroupBox.Controls.Add(this.passwordTextBox);
            this.authenticationGroupBox.Controls.Add(this.authenticationComboBox);
            this.authenticationGroupBox.Controls.Add(this.userNameLabel);
            this.authenticationGroupBox.Controls.Add(this.passwordLabel);
            this.authenticationGroupBox.Controls.Add(this.userNameTextBox);
            resources.ApplyResources(this.authenticationGroupBox, "authenticationGroupBox");
            this.authenticationGroupBox.Name = "authenticationGroupBox";
            this.authenticationGroupBox.TabStop = false;
            // 
            // rememberPasswordCheckBox
            // 
            resources.ApplyResources(this.rememberPasswordCheckBox, "rememberPasswordCheckBox");
            this.rememberPasswordCheckBox.Name = "rememberPasswordCheckBox";
            this.rememberPasswordCheckBox.UseVisualStyleBackColor = true;
            this.rememberPasswordCheckBox.CheckedChanged += new System.EventHandler(this.SettingsChanged);
            // 
            // authenticationLabel
            // 
            resources.ApplyResources(this.authenticationLabel, "authenticationLabel");
            this.authenticationLabel.Name = "authenticationLabel";
            // 
            // passwordTextBox
            // 
            resources.ApplyResources(this.passwordTextBox, "passwordTextBox");
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.TextChanged += new System.EventHandler(this.SettingsChanged);
            this.passwordTextBox.Validated += new System.EventHandler(this.FieldVerified);
            // 
            // authenticationComboBox
            // 
            this.authenticationComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.authenticationComboBox.FormattingEnabled = true;
            this.authenticationComboBox.Items.AddRange(new object[] {
            resources.GetString("authenticationComboBox.Items"),
            resources.GetString("authenticationComboBox.Items1")});
            resources.ApplyResources(this.authenticationComboBox, "authenticationComboBox");
            this.authenticationComboBox.Name = "authenticationComboBox";
            this.authenticationComboBox.SelectedIndexChanged += new System.EventHandler(this.AuthenticationComboBox_SelectedIndexChanged);
            // 
            // userNameLabel
            // 
            resources.ApplyResources(this.userNameLabel, "userNameLabel");
            this.userNameLabel.Name = "userNameLabel";
            // 
            // passwordLabel
            // 
            resources.ApplyResources(this.passwordLabel, "passwordLabel");
            this.passwordLabel.Name = "passwordLabel";
            // 
            // userNameTextBox
            // 
            resources.ApplyResources(this.userNameTextBox, "userNameTextBox");
            this.userNameTextBox.Name = "userNameTextBox";
            this.userNameTextBox.TextChanged += new System.EventHandler(this.SettingsChanged);
            this.userNameTextBox.Validated += new System.EventHandler(this.FieldVerified);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.connectionStringLabel);
            this.panel1.Controls.Add(this.useCustomConectionCheckBox);
            this.panel1.Controls.Add(this.connectionStringTextBox);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // ConnectionManageForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.connectionGroupBox);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.connectionsListBox);
            this.Controls.Add(this.connectionsLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConnectionManageForm";
            this.Load += new System.EventHandler(this.ConnectionManageForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.connectionGroupBox.ResumeLayout(false);
            this.connectionGroupBox.PerformLayout();
            this.authenticationGroupBox.ResumeLayout(false);
            this.authenticationGroupBox.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label connectionsLabel;
        private System.Windows.Forms.ListBox connectionsListBox;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton addToolStripButton;
        private System.Windows.Forms.GroupBox connectionGroupBox;
        private System.Windows.Forms.ComboBox databaseTextBox;
        private System.Windows.Forms.Label databaseNameLabel;
        private System.Windows.Forms.TextBox connectionNameTextBox;
        private System.Windows.Forms.Label connectionNameLabel;
        private System.Windows.Forms.TextBox serverNameTextBox;
        private System.Windows.Forms.Label serverNameLabel;
        private System.Windows.Forms.GroupBox authenticationGroupBox;
        private System.Windows.Forms.CheckBox rememberPasswordCheckBox;
        private System.Windows.Forms.Label authenticationLabel;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.ComboBox authenticationComboBox;
        private System.Windows.Forms.Label userNameLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox userNameTextBox;
        private System.Windows.Forms.ToolStripButton deleteToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton closeToolStripButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.CheckBox useCustomConectionCheckBox;
        private System.Windows.Forms.TextBox connectionStringTextBox;
        private System.Windows.Forms.Label connectionStringLabel;
        private System.Windows.Forms.ToolStripButton cancelToolStripButton;
        private System.Windows.Forms.ToolStripButton TestToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Panel panel1;
    }
}