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
            this.columnDefDataGridView.AllowUserToAddRows = false;
            this.columnDefDataGridView.AllowUserToDeleteRows = false;
            this.columnDefDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.columnDefDataGridView.ContextMenuStrip = this.contextMenuStrip;
            this.columnDefDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.columnDefDataGridView.Location = new System.Drawing.Point(0, 37);
            this.columnDefDataGridView.Margin = new System.Windows.Forms.Padding(4);
            this.columnDefDataGridView.Name = "columnDefDataGridView";
            this.columnDefDataGridView.ReadOnly = true;
            this.columnDefDataGridView.RowHeadersWidth = 51;
            this.columnDefDataGridView.Size = new System.Drawing.Size(501, 464);
            this.columnDefDataGridView.TabIndex = 2;
            this.columnDefDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ColumnDefDataGridView_CellClick);
            this.columnDefDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ColumnDefDataGridView_CellDoubleClick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.frequcneyToolStripMenuItem,
            this.toolStripSeparator1,
            this.copyToolStripMenuItem,
            this.copySelectionToolStripMenuItem});
            this.contextMenuStrip.Name = "ContextMenuStrip1";
            this.contextMenuStrip.Size = new System.Drawing.Size(211, 110);
            // 
            // frequcneyToolStripMenuItem
            // 
            this.frequcneyToolStripMenuItem.Name = "frequcneyToolStripMenuItem";
            this.frequcneyToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.frequcneyToolStripMenuItem.Text = "Frequencies";
            this.frequcneyToolStripMenuItem.Click += new System.EventHandler(this.FrequcneyToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(207, 6);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.copyToolStripMenuItem.Text = "Copy column name";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItem_Click);
            // 
            // copySelectionToolStripMenuItem
            // 
            this.copySelectionToolStripMenuItem.Name = "copySelectionToolStripMenuItem";
            this.copySelectionToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.copySelectionToolStripMenuItem.Text = "Copy selection";
            this.copySelectionToolStripMenuItem.Click += new System.EventHandler(this.CopySelectionToolStripMenuItem_Click);
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel.Controls.Add(this.tablenameLabel);
            this.panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Margin = new System.Windows.Forms.Padding(4);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(501, 37);
            this.panel.TabIndex = 3;
            // 
            // tablenameLabel
            // 
            this.tablenameLabel.AutoSize = true;
            this.tablenameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tablenameLabel.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.tablenameLabel.Location = new System.Drawing.Point(8, 7);
            this.tablenameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.tablenameLabel.Name = "tablenameLabel";
            this.tablenameLabel.Size = new System.Drawing.Size(0, 24);
            this.tablenameLabel.TabIndex = 0;
            // 
            // ColumnDefView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.columnDefDataGridView);
            this.Controls.Add(this.panel);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ColumnDefView";
            this.Size = new System.Drawing.Size(501, 501);
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
