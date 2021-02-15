namespace OctofyExp
{
    partial class DBObjectTree
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBObjectTree));
            this.ctlTree = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tableAnalysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analyzeEntireTableviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.previewDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctlTree
            // 
            resources.ApplyResources(this.ctlTree, "ctlTree");
            this.ctlTree.HideSelection = false;
            this.ctlTree.Name = "ctlTree";
            this.ctlTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.CtlTree_AfterSelect);
            this.ctlTree.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.CtlTree_NodeMouseDoubleClick);
            this.ctlTree.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CtlTree_MouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tableAnalysisToolStripMenuItem,
            this.analyzeEntireTableviewToolStripMenuItem,
            this.toolStripSeparator2,
            this.previewDataToolStripMenuItem,
            this.toolStripSeparator1,
            this.copyToolStripMenuItem,
            this.scriptToolStripMenuItem});
            this.contextMenuStrip1.Name = "ContextMenuStrip1";
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            // 
            // tableAnalysisToolStripMenuItem
            // 
            this.tableAnalysisToolStripMenuItem.Name = "tableAnalysisToolStripMenuItem";
            resources.ApplyResources(this.tableAnalysisToolStripMenuItem, "tableAnalysisToolStripMenuItem");
            this.tableAnalysisToolStripMenuItem.Click += new System.EventHandler(this.TableAnalysisToolStripMenuItem_Click);
            // 
            // analyzeEntireTableviewToolStripMenuItem
            // 
            this.analyzeEntireTableviewToolStripMenuItem.Name = "analyzeEntireTableviewToolStripMenuItem";
            resources.ApplyResources(this.analyzeEntireTableviewToolStripMenuItem, "analyzeEntireTableviewToolStripMenuItem");
            this.analyzeEntireTableviewToolStripMenuItem.Click += new System.EventHandler(this.AnalyzeEntireTableviewToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // previewDataToolStripMenuItem
            // 
            this.previewDataToolStripMenuItem.Name = "previewDataToolStripMenuItem";
            resources.ApplyResources(this.previewDataToolStripMenuItem, "previewDataToolStripMenuItem");
            this.previewDataToolStripMenuItem.Click += new System.EventHandler(this.PreviewDataToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            resources.ApplyResources(this.copyToolStripMenuItem, "copyToolStripMenuItem");
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItem_Click);
            // 
            // scriptToolStripMenuItem
            // 
            this.scriptToolStripMenuItem.Name = "scriptToolStripMenuItem";
            resources.ApplyResources(this.scriptToolStripMenuItem, "scriptToolStripMenuItem");
            this.scriptToolStripMenuItem.Click += new System.EventHandler(this.ScriptToolStripMenuItem_Click);
            // 
            // DBObjectTree
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ctlTree);
            this.Name = "DBObjectTree";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView ctlTree;
        internal System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        internal System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem scriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tableAnalysisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem analyzeEntireTableviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem previewDataToolStripMenuItem;
    }
}
