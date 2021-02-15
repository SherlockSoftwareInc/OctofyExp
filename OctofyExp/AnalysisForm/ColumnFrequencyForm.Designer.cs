namespace OctofyExp
{
    partial class ColumnFrequencyForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColumnFrequencyForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.closeToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.chartViewToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.gridViewToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.sortToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.excludeBlanksToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.searchForToolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.searchToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.searchResultToolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.nextToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.previousToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.messageToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableNameToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.columnToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.rowsToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.rowNumToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.frequencyDataGridView = new System.Windows.Forms.DataGridView();
            this.ctlChart = new OctofyLib.VScrollBarChartControlEx();
            this.openFormTimer = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.frequencyDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripButton,
            this.toolStripSeparator1,
            this.chartViewToolStripButton,
            this.gridViewToolStripButton,
            this.toolStripSeparator2,
            this.sortToolStripButton,
            this.excludeBlanksToolStripButton,
            this.toolStripSeparator4,
            this.searchForToolStripLabel,
            this.searchToolStripTextBox,
            this.searchResultToolStripLabel,
            this.nextToolStripButton,
            this.previousToolStripButton});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // closeToolStripButton
            // 
            this.closeToolStripButton.Image = global::OctofyExp.Properties.Resources.icon_exit;
            resources.ApplyResources(this.closeToolStripButton, "closeToolStripButton");
            this.closeToolStripButton.Name = "closeToolStripButton";
            this.closeToolStripButton.Click += new System.EventHandler(this.CloseToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // chartViewToolStripButton
            // 
            this.chartViewToolStripButton.Image = global::OctofyExp.Properties.Resources.SimplBarChart;
            resources.ApplyResources(this.chartViewToolStripButton, "chartViewToolStripButton");
            this.chartViewToolStripButton.Name = "chartViewToolStripButton";
            this.chartViewToolStripButton.Click += new System.EventHandler(this.ChartViewToolStripButton_Click);
            // 
            // gridViewToolStripButton
            // 
            this.gridViewToolStripButton.Image = global::OctofyExp.Properties.Resources.data_grid;
            resources.ApplyResources(this.gridViewToolStripButton, "gridViewToolStripButton");
            this.gridViewToolStripButton.Name = "gridViewToolStripButton";
            this.gridViewToolStripButton.Click += new System.EventHandler(this.GridViewToolStripButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // sortToolStripButton
            // 
            this.sortToolStripButton.Image = global::OctofyExp.Properties.Resources.sort_icon_16;
            resources.ApplyResources(this.sortToolStripButton, "sortToolStripButton");
            this.sortToolStripButton.Name = "sortToolStripButton";
            this.sortToolStripButton.Click += new System.EventHandler(this.SortToolStripButton_Click);
            // 
            // excludeBlanksToolStripButton
            // 
            this.excludeBlanksToolStripButton.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.excludeBlanksToolStripButton, "excludeBlanksToolStripButton");
            this.excludeBlanksToolStripButton.Image = global::OctofyExp.Properties.Resources.no_blanks;
            this.excludeBlanksToolStripButton.Name = "excludeBlanksToolStripButton";
            this.excludeBlanksToolStripButton.Click += new System.EventHandler(this.ExcludeBlanksToolStripButton_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
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
            this.columnToolStripStatusLabel,
            this.rowsToolStripStatusLabel,
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
            // columnToolStripStatusLabel
            // 
            this.columnToolStripStatusLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.columnToolStripStatusLabel.Name = "columnToolStripStatusLabel";
            resources.ApplyResources(this.columnToolStripStatusLabel, "columnToolStripStatusLabel");
            // 
            // rowsToolStripStatusLabel
            // 
            this.rowsToolStripStatusLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.rowsToolStripStatusLabel.Name = "rowsToolStripStatusLabel";
            resources.ApplyResources(this.rowsToolStripStatusLabel, "rowsToolStripStatusLabel");
            // 
            // rowNumToolStripStatusLabel
            // 
            this.rowNumToolStripStatusLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.rowNumToolStripStatusLabel.Name = "rowNumToolStripStatusLabel";
            resources.ApplyResources(this.rowNumToolStripStatusLabel, "rowNumToolStripStatusLabel");
            // 
            // frequencyDataGridView
            // 
            this.frequencyDataGridView.AllowUserToAddRows = false;
            this.frequencyDataGridView.AllowUserToDeleteRows = false;
            this.frequencyDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.frequencyDataGridView, "frequencyDataGridView");
            this.frequencyDataGridView.Name = "frequencyDataGridView";
            this.frequencyDataGridView.ReadOnly = true;
            this.frequencyDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.FrequencyDataGridView_CellClick);
            this.frequencyDataGridView.Sorted += new System.EventHandler(this.FrequencyDataGridView_Sorted);
            // 
            // ctlChart
            // 
            this.ctlChart.AxisVisible = true;
            this.ctlChart.BackColor = System.Drawing.Color.White;
            this.ctlChart.Colors = "";
            resources.ApplyResources(this.ctlChart, "ctlChart");
            this.ctlChart.MaxSeries = 64;
            this.ctlChart.Name = "ctlChart";
            this.ctlChart.ShowTotal = true;
            this.ctlChart.Subtitle = "";
            this.ctlChart.SubtitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlChart.SubtitleVisible = false;
            this.ctlChart.Title = "";
            this.ctlChart.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlChart.TitleVisible = true;
            this.ctlChart.TotalVisible = true;
            this.ctlChart.SelectedIndexChanged += new System.EventHandler(this.OnChart_SelectedIndexChanged);
            // 
            // openFormTimer
            // 
            this.openFormTimer.Interval = 10;
            this.openFormTimer.Tick += new System.EventHandler(this.OpenFormTimer_Tick);
            // 
            // ColumnFrequencyForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ctlChart);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.frequencyDataGridView);
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "ColumnFrequencyForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ColumnFrequencyForm_FormClosing);
            this.Load += new System.EventHandler(this.TableColumnFrequencyForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ColumnFrequencyForm_KeyDown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.frequencyDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OctofyLib.VScrollBarChartControlEx ctlChart;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton chartViewToolStripButton;
        private System.Windows.Forms.ToolStripButton gridViewToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton closeToolStripButton;
        private System.Windows.Forms.ToolStripButton sortToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton excludeBlanksToolStripButton;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel messageToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel tableNameToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel columnToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel rowsToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel rowNumToolStripStatusLabel;
        private System.Windows.Forms.ToolStripLabel searchForToolStripLabel;
        private System.Windows.Forms.ToolStripTextBox searchToolStripTextBox;
        private System.Windows.Forms.ToolStripLabel searchResultToolStripLabel;
        private System.Windows.Forms.ToolStripButton nextToolStripButton;
        private System.Windows.Forms.ToolStripButton previousToolStripButton;
        private System.Windows.Forms.DataGridView frequencyDataGridView;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.Timer openFormTimer;
    }
}