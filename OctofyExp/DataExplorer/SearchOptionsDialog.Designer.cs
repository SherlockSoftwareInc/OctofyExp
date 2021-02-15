namespace OctofyExp
{
    partial class SearchOptionsDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchOptionsDialog));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.schemasCheckBox = new System.Windows.Forms.CheckBox();
            this.searchPatternGroupBox = new System.Windows.Forms.GroupBox();
            this.matchRadioButton = new System.Windows.Forms.RadioButton();
            this.containsRadioButton = new System.Windows.Forms.RadioButton();
            this.endWithRadioButton = new System.Windows.Forms.RadioButton();
            this.startWithRadioButton = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.oKButton = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.searchPatternGroupBox.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.schemasCheckBox);
            this.tabPage1.Controls.Add(this.searchPatternGroupBox);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // schemasCheckBox
            // 
            resources.ApplyResources(this.schemasCheckBox, "schemasCheckBox");
            this.schemasCheckBox.Name = "schemasCheckBox";
            this.schemasCheckBox.UseVisualStyleBackColor = true;
            // 
            // searchPatternGroupBox
            // 
            this.searchPatternGroupBox.Controls.Add(this.matchRadioButton);
            this.searchPatternGroupBox.Controls.Add(this.containsRadioButton);
            this.searchPatternGroupBox.Controls.Add(this.endWithRadioButton);
            this.searchPatternGroupBox.Controls.Add(this.startWithRadioButton);
            resources.ApplyResources(this.searchPatternGroupBox, "searchPatternGroupBox");
            this.searchPatternGroupBox.Name = "searchPatternGroupBox";
            this.searchPatternGroupBox.TabStop = false;
            // 
            // matchRadioButton
            // 
            resources.ApplyResources(this.matchRadioButton, "matchRadioButton");
            this.matchRadioButton.Name = "matchRadioButton";
            this.matchRadioButton.UseVisualStyleBackColor = true;
            // 
            // containsRadioButton
            // 
            resources.ApplyResources(this.containsRadioButton, "containsRadioButton");
            this.containsRadioButton.Checked = true;
            this.containsRadioButton.Name = "containsRadioButton";
            this.containsRadioButton.TabStop = true;
            this.containsRadioButton.UseVisualStyleBackColor = true;
            // 
            // endWithRadioButton
            // 
            resources.ApplyResources(this.endWithRadioButton, "endWithRadioButton");
            this.endWithRadioButton.Name = "endWithRadioButton";
            this.endWithRadioButton.UseVisualStyleBackColor = true;
            // 
            // startWithRadioButton
            // 
            resources.ApplyResources(this.startWithRadioButton, "startWithRadioButton");
            this.startWithRadioButton.Name = "startWithRadioButton";
            this.startWithRadioButton.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.cancelButton, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.oKButton, 1, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.cancelButton, "cancelButton");
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // oKButton
            // 
            resources.ApplyResources(this.oKButton, "oKButton");
            this.oKButton.Name = "oKButton";
            this.oKButton.UseVisualStyleBackColor = true;
            this.oKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // SearchOptionsDialog
            // 
            this.AcceptButton = this.oKButton;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SearchOptionsDialog";
            this.Load += new System.EventHandler(this.SearchOptionsDialog_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.searchPatternGroupBox.ResumeLayout(false);
            this.searchPatternGroupBox.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.CheckBox schemasCheckBox;
        private System.Windows.Forms.GroupBox searchPatternGroupBox;
        private System.Windows.Forms.RadioButton matchRadioButton;
        private System.Windows.Forms.RadioButton containsRadioButton;
        private System.Windows.Forms.RadioButton endWithRadioButton;
        private System.Windows.Forms.RadioButton startWithRadioButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button oKButton;
    }
}