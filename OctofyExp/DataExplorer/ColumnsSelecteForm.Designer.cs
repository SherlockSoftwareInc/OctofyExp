
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
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select columns for analysis:";
            // 
            // columnsCheckedListBox
            // 
            this.columnsCheckedListBox.FormattingEnabled = true;
            this.columnsCheckedListBox.Location = new System.Drawing.Point(12, 41);
            this.columnsCheckedListBox.Name = "columnsCheckedListBox";
            this.columnsCheckedListBox.Size = new System.Drawing.Size(406, 274);
            this.columnsCheckedListBox.TabIndex = 1;
            this.columnsCheckedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ColumnsCheckedListBox_ItemCheck);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.okToolStripButton,
            this.cancelToolStripButton,
            this.toolStripSeparator1,
            this.selectAllToolStripButton,
            this.clearToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(430, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // okToolStripButton
            // 
            this.okToolStripButton.Enabled = false;
            this.okToolStripButton.Image = global::OctofyExp.Properties.Resources.checkmark;
            this.okToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.okToolStripButton.Name = "okToolStripButton";
            this.okToolStripButton.Size = new System.Drawing.Size(43, 22);
            this.okToolStripButton.Text = "OK";
            this.okToolStripButton.Click += new System.EventHandler(this.OKToolStripButton_Click);
            // 
            // cancelToolStripButton
            // 
            this.cancelToolStripButton.Image = global::OctofyExp.Properties.Resources.delete_icon;
            this.cancelToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cancelToolStripButton.Name = "cancelToolStripButton";
            this.cancelToolStripButton.Size = new System.Drawing.Size(63, 22);
            this.cancelToolStripButton.Text = "Cancel";
            this.cancelToolStripButton.Click += new System.EventHandler(this.CancelToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // selectAllToolStripButton
            // 
            this.selectAllToolStripButton.Image = global::OctofyExp.Properties.Resources.select_all_16;
            this.selectAllToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.selectAllToolStripButton.Name = "selectAllToolStripButton";
            this.selectAllToolStripButton.Size = new System.Drawing.Size(75, 22);
            this.selectAllToolStripButton.Text = "Select All";
            this.selectAllToolStripButton.Click += new System.EventHandler(this.SelectAllToolStripButton_Click);
            // 
            // clearToolStripButton
            // 
            this.clearToolStripButton.Image = global::OctofyExp.Properties.Resources.clear_all_16;
            this.clearToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clearToolStripButton.Name = "clearToolStripButton";
            this.clearToolStripButton.Size = new System.Drawing.Size(54, 22);
            this.clearToolStripButton.Text = "Clear";
            this.clearToolStripButton.Click += new System.EventHandler(this.ClearToolStripButton_Click);
            // 
            // ColumnsSelecteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 326);
            this.Controls.Add(this.columnsCheckedListBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ColumnsSelecteForm";
            this.Text = "Columns Picker Dialog";
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