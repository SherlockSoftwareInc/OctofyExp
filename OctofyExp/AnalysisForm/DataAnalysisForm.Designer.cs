using System.Windows.Forms;

namespace OctofyExp
{
    public partial class DataAnalysisForm : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataAnalysisForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.measurementVariables = new OctofyExp.ChartVariableSelector();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chart = new OctofyLib.AnalysisChartPanel();
            this.summaryDataCollapsibleSplitter = new OctofyLib.CollapsibleSplitter();
            this.summaryDataGridView = new System.Windows.Forms.DataGridView();
            this.summaryContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.summaryExportToExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startTimer = new System.Windows.Forms.Timer(this.components);
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyChartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAggregateDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.exportAggregateDataToExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.barChartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stackedBarChartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.allCasesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.closeToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.barChartToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.stackedBarChartToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.excludeBlanksToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.sortToolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.sortToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.searchForToolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.searchToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.searchResultToolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.nextToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.previousToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.messageToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableNameToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.numOfRowsToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.loadTimeToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.analysisTimeToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.CalculateTimeToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.categoryToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.rowNumToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.collapsibleSplitter0 = new OctofyLib.CollapsibleSplitter();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.summaryDataGridView)).BeginInit();
            this.summaryContextMenuStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.measurementVariables);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            // 
            // measurementVariables
            // 
            this.measurementVariables.DateColumnName = "";
            resources.ApplyResources(this.measurementVariables, "measurementVariables");
            this.measurementVariables.MaxXMembers = 64;
            this.measurementVariables.MaxYMembers = 32768;
            this.measurementVariables.Name = "measurementVariables";
            this.measurementVariables.VariableType = OctofyExp.DataAnalysisForm.Views.BarChart;
            this.measurementVariables.SelectedDateGroupTypeChanged += new System.EventHandler(this.MeasurementVariables_SelectionChanged);
            this.measurementVariables.SelectionChanged += new System.EventHandler(this.MeasurementVariables_SelectionChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.chart);
            this.panel1.Controls.Add(this.summaryDataCollapsibleSplitter);
            this.panel1.Controls.Add(this.summaryDataGridView);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // chart
            // 
            this.chart.ChartType = OctofyLib.AnalysisChartPanel.ChartTypes.None;
            this.chart.Colors = "";
            resources.ApplyResources(this.chart, "chart");
            this.chart.MaxSeries = 0;
            this.chart.Name = "chart";
            this.chart.SelectedIndexX = 0;
            this.chart.SelectedIndexY = 0;
            this.chart.Subtitle = "";
            this.chart.SubtitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chart.Title = "";
            this.chart.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chart.SelectedIndexChanged += new System.EventHandler(this.OnChartView_SelectedIndexChanged);
            // 
            // summaryDataCollapsibleSplitter
            // 
            this.summaryDataCollapsibleSplitter.AnimationDelay = 20;
            this.summaryDataCollapsibleSplitter.AnimationStep = 20;
            this.summaryDataCollapsibleSplitter.BorderStyle3D = System.Windows.Forms.Border3DStyle.Flat;
            this.summaryDataCollapsibleSplitter.ControlToHide = this.summaryDataGridView;
            resources.ApplyResources(this.summaryDataCollapsibleSplitter, "summaryDataCollapsibleSplitter");
            this.summaryDataCollapsibleSplitter.ExpandParentForm = false;
            this.summaryDataCollapsibleSplitter.Name = "summaryDataCollapsibleSplitter";
            this.summaryDataCollapsibleSplitter.SplitterDistance = 330;
            this.summaryDataCollapsibleSplitter.TabStop = false;
            this.summaryDataCollapsibleSplitter.UseAnimations = false;
            this.summaryDataCollapsibleSplitter.VisualStyle = OctofyLib.VisualStyles.Mozilla;
            // 
            // summaryDataGridView
            // 
            this.summaryDataGridView.AllowUserToAddRows = false;
            this.summaryDataGridView.AllowUserToDeleteRows = false;
            this.summaryDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.summaryDataGridView.ContextMenuStrip = this.summaryContextMenuStrip;
            resources.ApplyResources(this.summaryDataGridView, "summaryDataGridView");
            this.summaryDataGridView.Name = "summaryDataGridView";
            this.summaryDataGridView.ReadOnly = true;
            this.summaryDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SummaryDataGridView_CellClick);
            this.summaryDataGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.SummaryDataGridView_ColumnHeaderMouseClick);
            this.summaryDataGridView.Sorted += new System.EventHandler(this.SummaryDataGridView_Sorted);
            // 
            // summaryContextMenuStrip
            // 
            this.summaryContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.summaryContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.summaryExportToExcelToolStripMenuItem});
            this.summaryContextMenuStrip.Name = "ContextMenuStrip1";
            resources.ApplyResources(this.summaryContextMenuStrip, "summaryContextMenuStrip");
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Image = global::OctofyExp.Properties.Resources.copy;
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            resources.ApplyResources(this.copyToolStripMenuItem, "copyToolStripMenuItem");
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItem_Click);
            // 
            // summaryExportToExcelToolStripMenuItem
            // 
            this.summaryExportToExcelToolStripMenuItem.Image = global::OctofyExp.Properties.Resources.to_excel;
            this.summaryExportToExcelToolStripMenuItem.Name = "summaryExportToExcelToolStripMenuItem";
            resources.ApplyResources(this.summaryExportToExcelToolStripMenuItem, "summaryExportToExcelToolStripMenuItem");
            this.summaryExportToExcelToolStripMenuItem.Click += new System.EventHandler(this.SummaryToExcelToolStripMenuItem_Click);
            // 
            // startTimer
            // 
            this.startTimer.Interval = 10;
            this.startTimer.Tick += new System.EventHandler(this.StartTimer_Tick);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Image = global::OctofyExp.Properties.Resources.icon_exit;
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            resources.ApplyResources(this.closeToolStripMenuItem, "closeToolStripMenuItem");
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem});
            resources.ApplyResources(this.menuStrip, "menuStrip");
            this.menuStrip.Name = "menuStrip";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyChartToolStripMenuItem,
            this.copyAggregateDataToolStripMenuItem,
            this.toolStripSeparator4,
            this.exportAggregateDataToExcelToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            resources.ApplyResources(this.editToolStripMenuItem, "editToolStripMenuItem");
            // 
            // copyChartToolStripMenuItem
            // 
            this.copyChartToolStripMenuItem.Image = global::OctofyExp.Properties.Resources.copy_chart;
            this.copyChartToolStripMenuItem.Name = "copyChartToolStripMenuItem";
            resources.ApplyResources(this.copyChartToolStripMenuItem, "copyChartToolStripMenuItem");
            this.copyChartToolStripMenuItem.Click += new System.EventHandler(this.CopyChartToolStripButton_Click);
            // 
            // copyAggregateDataToolStripMenuItem
            // 
            this.copyAggregateDataToolStripMenuItem.Image = global::OctofyExp.Properties.Resources.copy;
            this.copyAggregateDataToolStripMenuItem.Name = "copyAggregateDataToolStripMenuItem";
            resources.ApplyResources(this.copyAggregateDataToolStripMenuItem, "copyAggregateDataToolStripMenuItem");
            this.copyAggregateDataToolStripMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // exportAggregateDataToExcelToolStripMenuItem
            // 
            this.exportAggregateDataToExcelToolStripMenuItem.Image = global::OctofyExp.Properties.Resources.to_excel;
            this.exportAggregateDataToExcelToolStripMenuItem.Name = "exportAggregateDataToExcelToolStripMenuItem";
            resources.ApplyResources(this.exportAggregateDataToExcelToolStripMenuItem, "exportAggregateDataToExcelToolStripMenuItem");
            this.exportAggregateDataToExcelToolStripMenuItem.Click += new System.EventHandler(this.SummaryToExcelMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.barChartToolStripMenuItem,
            this.stackedBarChartToolStripMenuItem,
            this.toolStripSeparator5,
            this.allCasesToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            resources.ApplyResources(this.viewToolStripMenuItem, "viewToolStripMenuItem");
            // 
            // barChartToolStripMenuItem
            // 
            this.barChartToolStripMenuItem.Checked = true;
            this.barChartToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.barChartToolStripMenuItem.Image = global::OctofyExp.Properties.Resources.SimplBarChart;
            this.barChartToolStripMenuItem.Name = "barChartToolStripMenuItem";
            resources.ApplyResources(this.barChartToolStripMenuItem, "barChartToolStripMenuItem");
            this.barChartToolStripMenuItem.Click += new System.EventHandler(this.BarChartButton_Click);
            // 
            // stackedBarChartToolStripMenuItem
            // 
            this.stackedBarChartToolStripMenuItem.Image = global::OctofyExp.Properties.Resources.stacked_column;
            this.stackedBarChartToolStripMenuItem.Name = "stackedBarChartToolStripMenuItem";
            resources.ApplyResources(this.stackedBarChartToolStripMenuItem, "stackedBarChartToolStripMenuItem");
            this.stackedBarChartToolStripMenuItem.Click += new System.EventHandler(this.CategoryStackedBarChartMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            // 
            // allCasesToolStripMenuItem
            // 
            this.allCasesToolStripMenuItem.Name = "allCasesToolStripMenuItem";
            resources.ApplyResources(this.allCasesToolStripMenuItem, "allCasesToolStripMenuItem");
            // 
            // toolStrip
            // 
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripButton,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.barChartToolStripButton,
            this.stackedBarChartToolStripButton,
            this.toolStripSeparator2,
            this.excludeBlanksToolStripButton,
            this.sortToolStripDropDownButton,
            this.sortToolStripButton,
            this.toolStripSeparator3,
            this.searchForToolStripLabel,
            this.searchToolStripTextBox,
            this.searchResultToolStripLabel,
            this.nextToolStripButton,
            this.previousToolStripButton});
            resources.ApplyResources(this.toolStrip, "toolStrip");
            this.toolStrip.Name = "toolStrip";
            // 
            // closeToolStripButton
            // 
            this.closeToolStripButton.Image = global::OctofyExp.Properties.Resources.icon_exit;
            resources.ApplyResources(this.closeToolStripButton, "closeToolStripButton");
            this.closeToolStripButton.Name = "closeToolStripButton";
            this.closeToolStripButton.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            resources.ApplyResources(this.toolStripLabel1, "toolStripLabel1");
            // 
            // barChartToolStripButton
            // 
            this.barChartToolStripButton.Checked = true;
            this.barChartToolStripButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.barChartToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.barChartToolStripButton.Image = global::OctofyExp.Properties.Resources.SimplBarChart;
            resources.ApplyResources(this.barChartToolStripButton, "barChartToolStripButton");
            this.barChartToolStripButton.Name = "barChartToolStripButton";
            this.barChartToolStripButton.Click += new System.EventHandler(this.BarChartButton_Click);
            // 
            // stackedBarChartToolStripButton
            // 
            this.stackedBarChartToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.stackedBarChartToolStripButton.Image = global::OctofyExp.Properties.Resources.stacked_column;
            resources.ApplyResources(this.stackedBarChartToolStripButton, "stackedBarChartToolStripButton");
            this.stackedBarChartToolStripButton.Name = "stackedBarChartToolStripButton";
            this.stackedBarChartToolStripButton.Click += new System.EventHandler(this.CategoryStackedBarChartMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // excludeBlanksToolStripButton
            // 
            this.excludeBlanksToolStripButton.BackColor = System.Drawing.SystemColors.Control;
            this.excludeBlanksToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.excludeBlanksToolStripButton.Image = global::OctofyExp.Properties.Resources.no_blanks;
            resources.ApplyResources(this.excludeBlanksToolStripButton, "excludeBlanksToolStripButton");
            this.excludeBlanksToolStripButton.Name = "excludeBlanksToolStripButton";
            this.excludeBlanksToolStripButton.Click += new System.EventHandler(this.ExcludeBlanksToolStripButton_Click);
            // 
            // sortToolStripDropDownButton
            // 
            this.sortToolStripDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.sortToolStripDropDownButton.Image = global::OctofyExp.Properties.Resources.sort_icon_16;
            resources.ApplyResources(this.sortToolStripDropDownButton, "sortToolStripDropDownButton");
            this.sortToolStripDropDownButton.Name = "sortToolStripDropDownButton";
            // 
            // sortToolStripButton
            // 
            this.sortToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.sortToolStripButton.Image = global::OctofyExp.Properties.Resources.sort_icon_16;
            resources.ApplyResources(this.sortToolStripButton, "sortToolStripButton");
            this.sortToolStripButton.Name = "sortToolStripButton";
            this.sortToolStripButton.Click += new System.EventHandler(this.SortToolStripButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // searchForToolStripLabel
            // 
            this.searchForToolStripLabel.Name = "searchForToolStripLabel";
            resources.ApplyResources(this.searchForToolStripLabel, "searchForToolStripLabel");
            // 
            // searchToolStripTextBox
            // 
            this.searchToolStripTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.searchToolStripTextBox, "searchToolStripTextBox");
            this.searchToolStripTextBox.Name = "searchToolStripTextBox";
            this.searchToolStripTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchToolStripTextBox_KeyDown);
            this.searchToolStripTextBox.TextChanged += new System.EventHandler(this.SearchToolStripTextBox_TextChanged);
            // 
            // searchResultToolStripLabel
            // 
            this.searchResultToolStripLabel.Name = "searchResultToolStripLabel";
            resources.ApplyResources(this.searchResultToolStripLabel, "searchResultToolStripLabel");
            // 
            // nextToolStripButton
            // 
            this.nextToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.nextToolStripButton, "nextToolStripButton");
            this.nextToolStripButton.Name = "nextToolStripButton";
            this.nextToolStripButton.Click += new System.EventHandler(this.NextToolStripButton_Click);
            // 
            // previousToolStripButton
            // 
            this.previousToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.previousToolStripButton.Image = global::OctofyExp.Properties.Resources.up_arrow;
            resources.ApplyResources(this.previousToolStripButton, "previousToolStripButton");
            this.previousToolStripButton.Name = "previousToolStripButton";
            this.previousToolStripButton.Click += new System.EventHandler(this.PreviousToolStripButton_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.messageToolStripStatusLabel,
            this.tableNameToolStripStatusLabel,
            this.numOfRowsToolStripStatusLabel,
            this.loadTimeToolStripStatusLabel,
            this.analysisTimeToolStripStatusLabel,
            this.CalculateTimeToolStripStatusLabel,
            this.categoryToolStripStatusLabel,
            this.rowNumToolStripStatusLabel});
            resources.ApplyResources(this.statusStrip, "statusStrip");
            this.statusStrip.Name = "statusStrip";
            // 
            // messageToolStripStatusLabel
            // 
            this.messageToolStripStatusLabel.Name = "messageToolStripStatusLabel";
            resources.ApplyResources(this.messageToolStripStatusLabel, "messageToolStripStatusLabel");
            this.messageToolStripStatusLabel.Spring = true;
            // 
            // tableNameToolStripStatusLabel
            // 
            this.tableNameToolStripStatusLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tableNameToolStripStatusLabel.Name = "tableNameToolStripStatusLabel";
            resources.ApplyResources(this.tableNameToolStripStatusLabel, "tableNameToolStripStatusLabel");
            // 
            // numOfRowsToolStripStatusLabel
            // 
            this.numOfRowsToolStripStatusLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.numOfRowsToolStripStatusLabel.Name = "numOfRowsToolStripStatusLabel";
            resources.ApplyResources(this.numOfRowsToolStripStatusLabel, "numOfRowsToolStripStatusLabel");
            // 
            // loadTimeToolStripStatusLabel
            // 
            this.loadTimeToolStripStatusLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.loadTimeToolStripStatusLabel.Name = "loadTimeToolStripStatusLabel";
            resources.ApplyResources(this.loadTimeToolStripStatusLabel, "loadTimeToolStripStatusLabel");
            // 
            // analysisTimeToolStripStatusLabel
            // 
            this.analysisTimeToolStripStatusLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.analysisTimeToolStripStatusLabel.Name = "analysisTimeToolStripStatusLabel";
            resources.ApplyResources(this.analysisTimeToolStripStatusLabel, "analysisTimeToolStripStatusLabel");
            // 
            // CalculateTimeToolStripStatusLabel
            // 
            this.CalculateTimeToolStripStatusLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.CalculateTimeToolStripStatusLabel.Name = "CalculateTimeToolStripStatusLabel";
            resources.ApplyResources(this.CalculateTimeToolStripStatusLabel, "CalculateTimeToolStripStatusLabel");
            // 
            // categoryToolStripStatusLabel
            // 
            this.categoryToolStripStatusLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.categoryToolStripStatusLabel.Name = "categoryToolStripStatusLabel";
            resources.ApplyResources(this.categoryToolStripStatusLabel, "categoryToolStripStatusLabel");
            // 
            // rowNumToolStripStatusLabel
            // 
            this.rowNumToolStripStatusLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.rowNumToolStripStatusLabel.Name = "rowNumToolStripStatusLabel";
            resources.ApplyResources(this.rowNumToolStripStatusLabel, "rowNumToolStripStatusLabel");
            // 
            // collapsibleSplitter0
            // 
            this.collapsibleSplitter0.AnimationDelay = 20;
            this.collapsibleSplitter0.AnimationStep = 20;
            this.collapsibleSplitter0.BackColor = System.Drawing.Color.LightGray;
            this.collapsibleSplitter0.BorderStyle3D = System.Windows.Forms.Border3DStyle.Flat;
            this.collapsibleSplitter0.ControlToHide = null;
            this.collapsibleSplitter0.ExpandParentForm = false;
            resources.ApplyResources(this.collapsibleSplitter0, "collapsibleSplitter0");
            this.collapsibleSplitter0.Name = "collapsibleSplitter0";
            this.collapsibleSplitter0.SplitterDistance = 0;
            this.collapsibleSplitter0.TabStop = false;
            this.collapsibleSplitter0.UseAnimations = false;
            this.collapsibleSplitter0.VisualStyle = OctofyLib.VisualStyles.Mozilla;
            // 
            // DataAnalysisForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "DataAnalysisForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AnalysisFormEx_FormClosing);
            this.Load += new System.EventHandler(this.AnalysisFormEx_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DataAnalysisForm_KeyDown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.summaryDataGridView)).EndInit();
            this.summaryContextMenuStrip.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private SplitContainer splitContainer1;
        private Panel panel1;
        private OctofyLib.AnalysisChartPanel chart;
        private DataGridView summaryDataGridView;
        private ContextMenuStrip summaryContextMenuStrip;
        private ToolStripButton barChartToolStripButton;
        private OctofyLib.CollapsibleSplitter summaryDataCollapsibleSplitter;
        private Timer startTimer;
        private OctofyLib.CollapsibleSplitter collapsibleSplitter0;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem closeToolStripMenuItem;
        private ToolStripMenuItem summaryExportToExcelToolStripMenuItem;
        private MenuStrip menuStrip;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStrip toolStrip;
        private ToolStripButton closeToolStripButton;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripLabel toolStripLabel1;
        private ChartVariableSelector measurementVariables;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem copyChartToolStripMenuItem;
        private ToolStripMenuItem copyAggregateDataToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem exportAggregateDataToExcelToolStripMenuItem;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem barChartToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem allCasesToolStripMenuItem;
        private ToolStripButton stackedBarChartToolStripButton;
        #endregion

        private ToolStripMenuItem stackedBarChartToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton excludeBlanksToolStripButton;
        private ToolStripDropDownButton sortToolStripDropDownButton;
        private ToolStripButton sortToolStripButton;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel messageToolStripStatusLabel;
        private ToolStripStatusLabel tableNameToolStripStatusLabel;
        private ToolStripStatusLabel numOfRowsToolStripStatusLabel;
        private ToolStripStatusLabel loadTimeToolStripStatusLabel;
        private ToolStripStatusLabel analysisTimeToolStripStatusLabel;
        private ToolStripStatusLabel CalculateTimeToolStripStatusLabel;
        private ToolStripStatusLabel categoryToolStripStatusLabel;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripTextBox searchToolStripTextBox;
        private ToolStripLabel searchResultToolStripLabel;
        private ToolStripButton nextToolStripButton;
        private ToolStripButton previousToolStripButton;
        private ToolStripStatusLabel rowNumToolStripStatusLabel;
        private ToolStripLabel searchForToolStripLabel;
    }
}
