namespace OctofyExp
{
    partial class TableSearchPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TableSearchPanel));
            this.searchButton = new System.Windows.Forms.Button();
            this.optionButton = new System.Windows.Forms.Button();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.rowsLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.searchForLabel = new System.Windows.Forms.Label();
            this.searchFromCombox = new System.Windows.Forms.ComboBox();
            this.searchTypeLabel = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tableAnalysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analyzeEntireTableviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.previewDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchResultsListBox = new System.Windows.Forms.ListBox();
            this.searchPatternLabel = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchButton
            // 
            resources.ApplyResources(this.searchButton, "searchButton");
            this.searchButton.Name = "searchButton";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // optionButton
            // 
            resources.ApplyResources(this.optionButton, "optionButton");
            this.optionButton.Name = "optionButton";
            this.optionButton.UseVisualStyleBackColor = true;
            this.optionButton.Click += new System.EventHandler(this.OptionButton_Click);
            // 
            // searchTextBox
            // 
            this.searchTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.searchTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            resources.ApplyResources(this.searchTextBox, "searchTextBox");
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSearchFor_KeyDown);
            // 
            // rowsLabel
            // 
            resources.ApplyResources(this.rowsLabel, "rowsLabel");
            this.rowsLabel.Name = "rowsLabel";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // searchForLabel
            // 
            resources.ApplyResources(this.searchForLabel, "searchForLabel");
            this.searchForLabel.Name = "searchForLabel";
            // 
            // searchFromCombox
            // 
            this.searchFromCombox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.searchFromCombox.FormattingEnabled = true;
            this.searchFromCombox.Items.AddRange(new object[] {
            resources.GetString("searchFromCombox.Items"),
            resources.GetString("searchFromCombox.Items1")});
            resources.ApplyResources(this.searchFromCombox, "searchFromCombox");
            this.searchFromCombox.Name = "searchFromCombox";
            this.searchFromCombox.SelectedIndexChanged += new System.EventHandler(this.SearchFromCombox_SelectedIndexChanged);
            // 
            // searchTypeLabel
            // 
            resources.ApplyResources(this.searchTypeLabel, "searchTypeLabel");
            this.searchTypeLabel.Name = "searchTypeLabel";
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
            // searchResultsListBox
            // 
            this.searchResultsListBox.ContextMenuStrip = this.contextMenuStrip1;
            this.searchResultsListBox.FormattingEnabled = true;
            resources.ApplyResources(this.searchResultsListBox, "searchResultsListBox");
            this.searchResultsListBox.Name = "searchResultsListBox";
            this.searchResultsListBox.SelectedIndexChanged += new System.EventHandler(this.SearchResultsListBox_SelectedIndexChanged);
            this.searchResultsListBox.DoubleClick += new System.EventHandler(this.SearchResultsListBox_DoubleClick);
            // 
            // searchPatternLabel
            // 
            resources.ApplyResources(this.searchPatternLabel, "searchPatternLabel");
            this.searchPatternLabel.Name = "searchPatternLabel";
            // 
            // TableSearchPanel
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.optionButton);
            this.Controls.Add(this.searchTextBox);
            this.Controls.Add(this.rowsLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.searchPatternLabel);
            this.Controls.Add(this.searchForLabel);
            this.Controls.Add(this.searchFromCombox);
            this.Controls.Add(this.searchTypeLabel);
            this.Controls.Add(this.searchResultsListBox);
            this.Name = "TableSearchPanel";
            this.Load += new System.EventHandler(this.TableSearchPanel_Load);
            this.Resize += new System.EventHandler(this.TableSearchPanel_Resize);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button searchButton;
        internal System.Windows.Forms.Button optionButton;
        internal System.Windows.Forms.TextBox searchTextBox;
        internal System.Windows.Forms.Label rowsLabel;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label searchForLabel;
        internal System.Windows.Forms.ComboBox searchFromCombox;
        internal System.Windows.Forms.Label searchTypeLabel;
        internal System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        internal System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem scriptToolStripMenuItem;
        internal System.Windows.Forms.ListBox searchResultsListBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tableAnalysisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem analyzeEntireTableviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem previewDataToolStripMenuItem;
        internal System.Windows.Forms.Label searchPatternLabel;
    }
}
