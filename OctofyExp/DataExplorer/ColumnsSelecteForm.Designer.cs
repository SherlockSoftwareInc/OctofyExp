
namespace OctofyExp
{
    partial class ColumnsSelecteForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColumnsSelecteForm));
            this.label1 = new System.Windows.Forms.Label();
            this.columnsCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.okToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.cancelToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.clearToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // columnsCheckedListBox
            // 
            resources.ApplyResources(this.columnsCheckedListBox, "columnsCheckedListBox");
            this.columnsCheckedListBox.FormattingEnabled = true;
            this.columnsCheckedListBox.Name = "columnsCheckedListBox";
            this.columnsCheckedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ColumnsCheckedListBox_ItemCheck);
            // 
            // toolStrip1
            // 
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.okToolStripButton,
            this.cancelToolStripButton,
            this.toolStripSeparator1,
            this.selectAllToolStripButton,
            this.clearToolStripButton});
            this.toolStrip1.Name = "toolStrip1";
            // 
            // okToolStripButton
            // 
            resources.ApplyResources(this.okToolStripButton, "okToolStripButton");
            this.okToolStripButton.Image = global::OctofyExp.Properties.Resources.checkmark;
            this.okToolStripButton.Name = "okToolStripButton";
            this.okToolStripButton.Click += new System.EventHandler(this.OKToolStripButton_Click);
            // 
            // cancelToolStripButton
            // 
            resources.ApplyResources(this.cancelToolStripButton, "cancelToolStripButton");
            this.cancelToolStripButton.Image = global::OctofyExp.Properties.Resources.delete_icon;
            this.cancelToolStripButton.Name = "cancelToolStripButton";
            this.cancelToolStripButton.Click += new System.EventHandler(this.CancelToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // selectAllToolStripButton
            // 
            resources.ApplyResources(this.selectAllToolStripButton, "selectAllToolStripButton");
            this.selectAllToolStripButton.Image = global::OctofyExp.Properties.Resources.select_all_16;
            this.selectAllToolStripButton.Name = "selectAllToolStripButton";
            this.selectAllToolStripButton.Click += new System.EventHandler(this.SelectAllToolStripButton_Click);
            // 
            // clearToolStripButton
            // 
            resources.ApplyResources(this.clearToolStripButton, "clearToolStripButton");
            this.clearToolStripButton.Image = global::OctofyExp.Properties.Resources.clear_all_16;
            this.clearToolStripButton.Name = "clearToolStripButton";
            this.clearToolStripButton.Click += new System.EventHandler(this.ClearToolStripButton_Click);
            // 
            // ColumnsSelecteForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.columnsCheckedListBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ColumnsSelecteForm";
            this.Load += new System.EventHandler(this.ColumnsSelecteForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox columnsCheckedListBox;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton selectAllToolStripButton;
        private System.Windows.Forms.ToolStripButton clearToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton okToolStripButton;
        private System.Windows.Forms.ToolStripButton cancelToolStripButton;
    }
}