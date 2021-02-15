namespace OctofyExp
{
    partial class VariableSelector
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VariableSelector));
            this.measurementColumnLabel = new System.Windows.Forms.Label();
            this.infoButton = new System.Windows.Forms.Button();
            this.titlePanel = new System.Windows.Forms.Panel();
            this.columnListBox = new System.Windows.Forms.ListBox();
            this.titlePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // measurementColumnLabel
            // 
            resources.ApplyResources(this.measurementColumnLabel, "measurementColumnLabel");
            this.measurementColumnLabel.Name = "measurementColumnLabel";
            // 
            // infoButton
            // 
            resources.ApplyResources(this.infoButton, "infoButton");
            this.infoButton.Image = global::OctofyExp.Properties.Resources.Excla_mark;
            this.infoButton.Name = "infoButton";
            this.infoButton.UseVisualStyleBackColor = true;
            this.infoButton.Click += new System.EventHandler(this.InfoButton_Click);
            // 
            // titlePanel
            // 
            this.titlePanel.Controls.Add(this.infoButton);
            this.titlePanel.Controls.Add(this.measurementColumnLabel);
            resources.ApplyResources(this.titlePanel, "titlePanel");
            this.titlePanel.Name = "titlePanel";
            this.titlePanel.Resize += new System.EventHandler(this.TitlePanel_Resize);
            // 
            // columnListBox
            // 
            this.columnListBox.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.columnListBox, "columnListBox");
            this.columnListBox.FormattingEnabled = true;
            this.columnListBox.Name = "columnListBox";
            this.columnListBox.SelectedIndexChanged += new System.EventHandler(this.Variable_SelectedIndexChanged);
            // 
            // VariableSelector
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.columnListBox);
            this.Controls.Add(this.titlePanel);
            this.Name = "VariableSelector";
            this.titlePanel.ResumeLayout(false);
            this.titlePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button infoButton;
        private System.Windows.Forms.Label measurementColumnLabel;
        private System.Windows.Forms.Panel titlePanel;
        private System.Windows.Forms.ListBox columnListBox;
    }
}
