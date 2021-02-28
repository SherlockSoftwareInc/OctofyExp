namespace OctofyExp
{
    partial class ColumnDefView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColumnDefView));
            this.columnDefDataGridView = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.frequcneyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copySelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel = new System.Windows.Forms.Panel();
            this.tablenameLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.columnDefDataGridView)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // columnDefDataGridView
            // 
            resources.ApplyResources(this.columnDefDataGridView, "columnDefDataGridView");
            this.columnDefDataGridView.AllowUserToAddRows = false;
            this.columnDefDataGridView.AllowUserToDeleteRows = false;
            this.columnDefDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.columnDefDataGridView.ContextMenuStrip = this.contextMenuStrip;
            this.columnDefDataGridView.Name = "columnDefDataGridView";
            this.columnDefDataGridView.ReadOnly = true;
            this.columnDefDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ColumnDefDataGridView_CellClick);
            this.columnDefDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ColumnDefDataGridView_CellDoubleClick);
            // 
            // contextMenuStrip
            // 
            resources.ApplyResources(this.contextMenuStrip, "contextMenuStrip");
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.frequcneyToolStripMenuItem,
            this.toolStripSeparator1,
            this.copyToolStripMenuItem,
            this.copySelectionToolStripMenuItem});
            this.contextMenuStrip.Name = "ContextMenuStrip1";
            // 
            // frequcneyToolStripMenuItem
            // 
            resources.ApplyResources(this.frequcneyToolStripMenuItem, "frequcneyToolStripMenuItem");
            this.frequcneyToolStripMenuItem.Name = "frequcneyToolStripMenuItem";
            this.frequcneyToolStripMenuItem.Click += new System.EventHandler(this.FrequcneyToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // copyToolStripMenuItem
            // 
            resources.ApplyResources(this.copyToolStripMenuItem, "copyToolStripMenuItem");
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItem_Click);
            // 
            // copySelectionToolStripMenuItem
            // 
            resources.ApplyResources(this.copySelectionToolStripMenuItem, "copySelectionToolStripMenuItem");
            this.copySelectionToolStripMenuItem.Name = "copySelectionToolStripMenuItem";
            this.copySelectionToolStripMenuItem.Click += new System.EventHandler(this.CopySelectionToolStripMenuItem_Click);
            // 
            // panel
            // 
            resources.ApplyResources(this.panel, "panel");
            this.panel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel.Controls.Add(this.tablenameLabel);
            this.panel.Name = "panel";
            // 
            // tablenameLabel
            // 
            resources.ApplyResources(this.tablenameLabel, "tablenameLabel");
            this.tablenameLabel.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.tablenameLabel.Name = "tablenameLabel";
            // 
            // ColumnDefView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.columnDefDataGridView);
            this.Controls.Add(this.panel);
            this.Name = "ColumnDefView";
            ((System.ComponentModel.ISupportInitialize)(this.columnDefDataGridView)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.DataGridView columnDefDataGridView;
        internal System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        internal System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem copySelectionToolStripMenuItem;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        internal System.Windows.Forms.ToolStripMenuItem frequcneyToolStripMenuItem;
        internal System.Windows.Forms.Panel panel;
        internal System.Windows.Forms.Label tablenameLabel;
    }
}
