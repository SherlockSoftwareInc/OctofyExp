namespace OctofyLib
{
    public partial class FilterPanel : System.Windows.Forms.UserControl
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
            this.components = new System.ComponentModel.Container();
            this.applyButton = new System.Windows.Forms.Button();
            this.filterTree = new System.Windows.Forms.TreeView();
            this.addDateRangecontextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addDateRangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editDateRangecontextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editDateRangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addDateRangecontextMenuStrip.SuspendLayout();
            this.editDateRangecontextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // applyButton
            // 
            this.applyButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.applyButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.applyButton.Location = new System.Drawing.Point(0, 0);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(188, 23);
            this.applyButton.TabIndex = 1;
            this.applyButton.Text = "Apply data filter";
            this.applyButton.UseVisualStyleBackColor = false;
            this.applyButton.Click += new System.EventHandler(this.ApplyButton_Click);
            // 
            // filterTree
            // 
            this.filterTree.CheckBoxes = true;
            this.filterTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filterTree.Location = new System.Drawing.Point(0, 23);
            this.filterTree.Name = "filterTree";
            this.filterTree.Size = new System.Drawing.Size(188, 591);
            this.filterTree.TabIndex = 2;
            this.filterTree.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.OnTree_AfterCheck);
            this.filterTree.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.OnTreeView_BeforeExpand);
            this.filterTree.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FilterTree_MouseUp);
            // 
            // addDateRangecontextMenuStrip
            // 
            this.addDateRangecontextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addDateRangeToolStripMenuItem});
            this.addDateRangecontextMenuStrip.Name = "contextMenuStrip";
            this.addDateRangecontextMenuStrip.Size = new System.Drawing.Size(160, 26);
            // 
            // addDateRangeToolStripMenuItem
            // 
            this.addDateRangeToolStripMenuItem.Name = "addDateRangeToolStripMenuItem";
            this.addDateRangeToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.addDateRangeToolStripMenuItem.Text = "Add Date Range";
            this.addDateRangeToolStripMenuItem.Click += new System.EventHandler(this.AddDateRangeToolStripMenuItem_Click);
            // 
            // editDateRangecontextMenuStrip
            // 
            this.editDateRangecontextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editDateRangeToolStripMenuItem,
            this.toolStripSeparator1,
            this.removeToolStripMenuItem});
            this.editDateRangecontextMenuStrip.Name = "contextMenuStrip";
            this.editDateRangecontextMenuStrip.Size = new System.Drawing.Size(179, 54);
            // 
            // editDateRangeToolStripMenuItem
            // 
            this.editDateRangeToolStripMenuItem.Name = "editDateRangeToolStripMenuItem";
            this.editDateRangeToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.editDateRangeToolStripMenuItem.Text = "Change Date Range";
            this.editDateRangeToolStripMenuItem.Click += new System.EventHandler(this.EditDateRangeToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(175, 6);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.RemoveToolStripMenuItem_Click);
            // 
            // FilterPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.filterTree);
            this.Controls.Add(this.applyButton);
            this.Name = "FilterPanel";
            this.Size = new System.Drawing.Size(188, 614);
            this.addDateRangecontextMenuStrip.ResumeLayout(false);
            this.editDateRangecontextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.TreeView filterTree;
        private System.Windows.Forms.ContextMenuStrip addDateRangecontextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addDateRangeToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip editDateRangecontextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem editDateRangeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
    }
}