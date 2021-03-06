﻿namespace OctofyExp
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusToolStripStatusLabe = new System.Windows.Forms.ToolStripStatusLabel();
            this.messageToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.serverToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.databaseToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dbObjectsTree = new OctofyExp.DBObjectTree();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.serachPanel = new OctofyExp.TableSearchPanel();
            this.columnView = new OctofyExp.ColumnDefView();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewDBConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageConnectionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quickAnalysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analysisOnEntireTableviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.anaysisOnSelectedColumnsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.previewDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.copyTableviewNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.columnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FrequenciesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.copyColumnNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.dataSourcesToolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.excelFileAnalysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripContainer.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer.ContentPanel.SuspendLayout();
            this.toolStripContainer.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer
            // 
            // 
            // toolStripContainer.BottomToolStripPanel
            // 
            this.toolStripContainer.BottomToolStripPanel.Controls.Add(this.statusStrip);
            // 
            // toolStripContainer.ContentPanel
            // 
            this.toolStripContainer.ContentPanel.Controls.Add(this.splitContainer);
            resources.ApplyResources(this.toolStripContainer.ContentPanel, "toolStripContainer.ContentPanel");
            resources.ApplyResources(this.toolStripContainer, "toolStripContainer");
            this.toolStripContainer.Name = "toolStripContainer";
            // 
            // toolStripContainer.TopToolStripPanel
            // 
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.menuStrip);
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.toolStrip);
            // 
            // statusStrip
            // 
            resources.ApplyResources(this.statusStrip, "statusStrip");
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusToolStripStatusLabe,
            this.messageToolStripStatusLabel,
            this.serverToolStripStatusLabel,
            this.databaseToolStripStatusLabel});
            this.statusStrip.Name = "statusStrip";
            // 
            // statusToolStripStatusLabe
            // 
            this.statusToolStripStatusLabe.Name = "statusToolStripStatusLabe";
            resources.ApplyResources(this.statusToolStripStatusLabe, "statusToolStripStatusLabe");
            // 
            // messageToolStripStatusLabel
            // 
            this.messageToolStripStatusLabel.Name = "messageToolStripStatusLabel";
            resources.ApplyResources(this.messageToolStripStatusLabel, "messageToolStripStatusLabel");
            this.messageToolStripStatusLabel.Spring = true;
            // 
            // serverToolStripStatusLabel
            // 
            this.serverToolStripStatusLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.serverToolStripStatusLabel.Image = global::OctofyExp.Properties.Resources.server;
            this.serverToolStripStatusLabel.Name = "serverToolStripStatusLabel";
            resources.ApplyResources(this.serverToolStripStatusLabel, "serverToolStripStatusLabel");
            // 
            // databaseToolStripStatusLabel
            // 
            this.databaseToolStripStatusLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.databaseToolStripStatusLabel.Image = global::OctofyExp.Properties.Resources.database;
            this.databaseToolStripStatusLabel.Name = "databaseToolStripStatusLabel";
            resources.ApplyResources(this.databaseToolStripStatusLabel, "databaseToolStripStatusLabel");
            // 
            // splitContainer
            // 
            resources.ApplyResources(this.splitContainer, "splitContainer");
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.tabControl);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.columnView);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dbObjectsTree);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dbObjectsTree
            // 
            this.dbObjectsTree.DefaultNumOfRows = 10000;
            resources.ApplyResources(this.dbObjectsTree, "dbObjectsTree");
            this.dbObjectsTree.Name = "dbObjectsTree";
            this.dbObjectsTree.AfterSelect += new System.EventHandler(this.DbObjects_AfterSelect);
            this.dbObjectsTree.OnAnalysisTable += new System.EventHandler<OnAnalysisTableEventArgs>(this.OnAnalysisTable);
            this.dbObjectsTree.OnPreviewData += new System.EventHandler<OnAnalysisTableEventArgs>(this.OnPreviewData);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.serachPanel);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // serachPanel
            // 
            this.serachPanel.ConnectionString = "";
            this.serachPanel.DefaultNumOfRows = 10000;
            resources.ApplyResources(this.serachPanel, "serachPanel");
            this.serachPanel.Name = "serachPanel";
            this.serachPanel.SearchFor = "";
            this.serachPanel.SearchHistory = "";
            this.serachPanel.SearchType = OctofyExp.TableSearchPanel.SearchTypeNums.TableName;
            this.serachPanel.SelectedTable = "";
            this.serachPanel.TableType = "";
            this.serachPanel.AfterSelect += new System.EventHandler(this.OnSerachPanel_AfterSelect);
            this.serachPanel.OnAnalysisTable += new System.EventHandler<OnAnalysisTableEventArgs>(this.OnAnalysisTable);
            this.serachPanel.OnPreviewData += new System.EventHandler<OnAnalysisTableEventArgs>(this.OnPreviewData);
            // 
            // columnView
            // 
            this.columnView.ConnectionString = "";
            resources.ApplyResources(this.columnView, "columnView");
            this.columnView.ExcludedColumns = ((System.Collections.Generic.List<string>)(resources.GetObject("columnView.ExcludedColumns")));
            this.columnView.Name = "columnView";
            this.columnView.ObjectName = "";
            this.columnView.OnColumnFrequency += new System.EventHandler(this.OnColumnFrequency);
            this.columnView.SelectedColumnChanged += new System.EventHandler(this.ColumnView_SelectedColumnChanged);
            // 
            // menuStrip
            // 
            resources.ApplyResources(this.menuStrip, "menuStrip");
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.tableToolStripMenuItem,
            this.columnToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Name = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToToolStripMenuItem,
            this.addNewDBConnectionToolStripMenuItem,
            this.manageConnectionsToolStripMenuItem,
            this.toolStripSeparator3,
            this.excelFileAnalysisToolStripMenuItem,
            this.toolStripSeparator5,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // connectToToolStripMenuItem
            // 
            resources.ApplyResources(this.connectToToolStripMenuItem, "connectToToolStripMenuItem");
            this.connectToToolStripMenuItem.Name = "connectToToolStripMenuItem";
            // 
            // addNewDBConnectionToolStripMenuItem
            // 
            this.addNewDBConnectionToolStripMenuItem.Image = global::OctofyExp.Properties.Resources.add;
            this.addNewDBConnectionToolStripMenuItem.Name = "addNewDBConnectionToolStripMenuItem";
            resources.ApplyResources(this.addNewDBConnectionToolStripMenuItem, "addNewDBConnectionToolStripMenuItem");
            this.addNewDBConnectionToolStripMenuItem.Click += new System.EventHandler(this.AddToolStripButton_Click);
            // 
            // manageConnectionsToolStripMenuItem
            // 
            this.manageConnectionsToolStripMenuItem.Name = "manageConnectionsToolStripMenuItem";
            resources.ApplyResources(this.manageConnectionsToolStripMenuItem, "manageConnectionsToolStripMenuItem");
            this.manageConnectionsToolStripMenuItem.Click += new System.EventHandler(this.ManageConnectionsToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // tableToolStripMenuItem
            // 
            this.tableToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quickAnalysisToolStripMenuItem,
            this.analysisOnEntireTableviewToolStripMenuItem,
            this.anaysisOnSelectedColumnsToolStripMenuItem,
            this.toolStripSeparator1,
            this.previewDataToolStripMenuItem,
            this.toolStripSeparator2,
            this.copyTableviewNameToolStripMenuItem});
            resources.ApplyResources(this.tableToolStripMenuItem, "tableToolStripMenuItem");
            this.tableToolStripMenuItem.Name = "tableToolStripMenuItem";
            // 
            // quickAnalysisToolStripMenuItem
            // 
            this.quickAnalysisToolStripMenuItem.Name = "quickAnalysisToolStripMenuItem";
            resources.ApplyResources(this.quickAnalysisToolStripMenuItem, "quickAnalysisToolStripMenuItem");
            this.quickAnalysisToolStripMenuItem.Click += new System.EventHandler(this.QuickAnalysisToolStripMenuItem_Click);
            // 
            // analysisOnEntireTableviewToolStripMenuItem
            // 
            this.analysisOnEntireTableviewToolStripMenuItem.Name = "analysisOnEntireTableviewToolStripMenuItem";
            resources.ApplyResources(this.analysisOnEntireTableviewToolStripMenuItem, "analysisOnEntireTableviewToolStripMenuItem");
            this.analysisOnEntireTableviewToolStripMenuItem.Click += new System.EventHandler(this.AnalysisOnEntireTableviewToolStripMenuItem_Click);
            // 
            // anaysisOnSelectedColumnsToolStripMenuItem
            // 
            this.anaysisOnSelectedColumnsToolStripMenuItem.Name = "anaysisOnSelectedColumnsToolStripMenuItem";
            resources.ApplyResources(this.anaysisOnSelectedColumnsToolStripMenuItem, "anaysisOnSelectedColumnsToolStripMenuItem");
            this.anaysisOnSelectedColumnsToolStripMenuItem.Click += new System.EventHandler(this.AnaysisOnSelectedColumnsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // previewDataToolStripMenuItem
            // 
            this.previewDataToolStripMenuItem.Name = "previewDataToolStripMenuItem";
            resources.ApplyResources(this.previewDataToolStripMenuItem, "previewDataToolStripMenuItem");
            this.previewDataToolStripMenuItem.Click += new System.EventHandler(this.PreviewDataToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // copyTableviewNameToolStripMenuItem
            // 
            this.copyTableviewNameToolStripMenuItem.Name = "copyTableviewNameToolStripMenuItem";
            resources.ApplyResources(this.copyTableviewNameToolStripMenuItem, "copyTableviewNameToolStripMenuItem");
            this.copyTableviewNameToolStripMenuItem.Click += new System.EventHandler(this.CopyTableviewNameToolStripMenuItem_Click);
            // 
            // columnToolStripMenuItem
            // 
            this.columnToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FrequenciesToolStripMenuItem,
            this.toolStripSeparator4,
            this.copyColumnNameToolStripMenuItem});
            resources.ApplyResources(this.columnToolStripMenuItem, "columnToolStripMenuItem");
            this.columnToolStripMenuItem.Name = "columnToolStripMenuItem";
            // 
            // FrequenciesToolStripMenuItem
            // 
            this.FrequenciesToolStripMenuItem.Name = "FrequenciesToolStripMenuItem";
            resources.ApplyResources(this.FrequenciesToolStripMenuItem, "FrequenciesToolStripMenuItem");
            this.FrequenciesToolStripMenuItem.Click += new System.EventHandler(this.FrequenciesToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // copyColumnNameToolStripMenuItem
            // 
            this.copyColumnNameToolStripMenuItem.Name = "copyColumnNameToolStripMenuItem";
            resources.ApplyResources(this.copyColumnNameToolStripMenuItem, "copyColumnNameToolStripMenuItem");
            this.copyColumnNameToolStripMenuItem.Click += new System.EventHandler(this.CopyColumnNameToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            resources.ApplyResources(this.editToolStripMenuItem, "editToolStripMenuItem");
            // 
            // copyToolStripMenuItem
            // 
            resources.ApplyResources(this.copyToolStripMenuItem, "copyToolStripMenuItem");
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBarToolStripMenuItem,
            this.statusBarToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            resources.ApplyResources(this.viewToolStripMenuItem, "viewToolStripMenuItem");
            // 
            // toolBarToolStripMenuItem
            // 
            this.toolBarToolStripMenuItem.Checked = true;
            this.toolBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolBarToolStripMenuItem.Name = "toolBarToolStripMenuItem";
            resources.ApplyResources(this.toolBarToolStripMenuItem, "toolBarToolStripMenuItem");
            this.toolBarToolStripMenuItem.Click += new System.EventHandler(this.ToolBarToolStripMenuItem_Click);
            // 
            // statusBarToolStripMenuItem
            // 
            this.statusBarToolStripMenuItem.Checked = true;
            this.statusBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.statusBarToolStripMenuItem.Name = "statusBarToolStripMenuItem";
            resources.ApplyResources(this.statusBarToolStripMenuItem, "statusBarToolStripMenuItem");
            this.statusBarToolStripMenuItem.Click += new System.EventHandler(this.StatusBarToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            resources.ApplyResources(this.toolsToolStripMenuItem, "toolsToolStripMenuItem");
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            resources.ApplyResources(this.optionsToolStripMenuItem, "optionsToolStripMenuItem");
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.OptionsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            resources.ApplyResources(this.aboutToolStripMenuItem, "aboutToolStripMenuItem");
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // toolStrip
            // 
            resources.ApplyResources(this.toolStrip, "toolStrip");
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.dataSourcesToolStripComboBox,
            this.toolStripButton});
            this.toolStrip.Name = "toolStrip";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            resources.ApplyResources(this.toolStripLabel1, "toolStripLabel1");
            // 
            // dataSourcesToolStripComboBox
            // 
            this.dataSourcesToolStripComboBox.Name = "dataSourcesToolStripComboBox";
            resources.ApplyResources(this.dataSourcesToolStripComboBox, "dataSourcesToolStripComboBox");
            this.dataSourcesToolStripComboBox.SelectedIndexChanged += new System.EventHandler(this.DataSourcesToolStripComboBox_SelectedIndexChanged);
            // 
            // toolStripButton
            // 
            this.toolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton.Image = global::OctofyExp.Properties.Resources.add;
            resources.ApplyResources(this.toolStripButton, "toolStripButton");
            this.toolStripButton.Name = "toolStripButton";
            this.toolStripButton.Click += new System.EventHandler(this.AddToolStripButton_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            // 
            // excelFileAnalysisToolStripMenuItem
            // 
            this.excelFileAnalysisToolStripMenuItem.Name = "excelFileAnalysisToolStripMenuItem";
            resources.ApplyResources(this.excelFileAnalysisToolStripMenuItem, "excelFileAnalysisToolStripMenuItem");
            this.excelFileAnalysisToolStripMenuItem.Click += new System.EventHandler(this.ExcelFileAnalysisToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStripContainer);
            this.Name = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DBExpoMain_FormClosing);
            this.Load += new System.EventHandler(this.DBExpoMain_Load);
            this.toolStripContainer.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer.ContentPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.PerformLayout();
            this.toolStripContainer.ResumeLayout(false);
            this.toolStripContainer.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem toolBarToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem statusBarToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        internal System.Windows.Forms.ToolTip toolTip;
        internal System.Windows.Forms.ToolStripContainer toolStripContainer;
        internal System.Windows.Forms.SplitContainer splitContainer;
        internal System.Windows.Forms.MenuStrip menuStrip;
        internal System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem connectToToolStripMenuItem;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        internal System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private TableSearchPanel serachPanel;
        private ColumnDefView columnView;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private DBObjectTree dbObjectsTree;
        private System.Windows.Forms.ToolStripMenuItem manageConnectionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewDBConnectionToolStripMenuItem;
        internal System.Windows.Forms.StatusStrip statusStrip;
        internal System.Windows.Forms.ToolStripStatusLabel statusToolStripStatusLabe;
        private System.Windows.Forms.ToolStripStatusLabel messageToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel serverToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel databaseToolStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem tableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem columnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quickAnalysisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem analysisOnEntireTableviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem previewDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem copyTableviewNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FrequenciesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem copyColumnNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem anaysisOnSelectedColumnsToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolStripButton;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox dataSourcesToolStripComboBox;
        private System.Windows.Forms.ToolStripMenuItem excelFileAnalysisToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    }
}