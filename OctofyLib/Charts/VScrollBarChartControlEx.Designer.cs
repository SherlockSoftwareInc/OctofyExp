namespace OctofyLib
{
    partial class VScrollBarChartControlEx
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
            //OctofyLib.ColorSchema colorSchema2 = new OctofyLib.ColorSchema();
            this.chartControl = new OctofyLib.VScrollBarChartControl();
            this.xAxis = new OctofyLib.XAxisControl();
            this.legends = new OctofyLib.LegendPanel();
            this.subtitleLabel = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            this.totalLabel = new System.Windows.Forms.Label();
            this.ctlChart = new OctofyLib.VScrollBarChartControl();
            this.ctlXAxis = new OctofyLib.XAxisControl();
            this.SuspendLayout();
            // 
            // chartControl
            // 
            this.chartControl.Colors = "";
            this.chartControl.GridVisible = false;
            this.chartControl.Location = new System.Drawing.Point(0, 77);
            this.chartControl.MaxSeries = 64;
            this.chartControl.MinimumSize = new System.Drawing.Size(200, 20);
            this.chartControl.Name = "chartControl";
            this.chartControl.Size = new System.Drawing.Size(566, 261);
            this.chartControl.TabIndex = 21;
            this.chartControl.SelectedIndexChanged += new System.EventHandler(this.OnChart_SelectedIndexChanged);
            // 
            // xAxis
            // 
            this.xAxis.Location = new System.Drawing.Point(0, 456);
            this.xAxis.Name = "xAxis";
            this.xAxis.Size = new System.Drawing.Size(566, 33);
            this.xAxis.TabIndex = 20;
            this.xAxis.Visible = false;
            this.xAxis.DrawingRatioChanged += new System.EventHandler(this.XAxis_DrawingRatioChanged);
            // 
            // legends
            // 
            this.legends.Colors = "";
            this.legends.Location = new System.Drawing.Point(3, 425);
            this.legends.Margin = new System.Windows.Forms.Padding(2);
            this.legends.Name = "legends";
            this.legends.Size = new System.Drawing.Size(563, 0);
            this.legends.TabIndex = 22;
            this.legends.Visible = false;
            this.legends.SizeChanged += new System.EventHandler(this.Legends_SizeChanged);
            // 
            // subtitleLabel
            // 
            this.subtitleLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.subtitleLabel.Location = new System.Drawing.Point(0, 20);
            this.subtitleLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.subtitleLabel.Name = "subtitleLabel";
            this.subtitleLabel.Size = new System.Drawing.Size(569, 18);
            this.subtitleLabel.TabIndex = 19;
            this.subtitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.subtitleLabel.Visible = false;
            // 
            // titleLabel
            // 
            this.titleLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.Location = new System.Drawing.Point(0, 0);
            this.titleLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(569, 20);
            this.titleLabel.TabIndex = 18;
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.titleLabel.Visible = false;
            // 
            // totalLabel
            // 
            this.totalLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.totalLabel.Location = new System.Drawing.Point(0, 492);
            this.totalLabel.Name = "totalLabel";
            this.totalLabel.Size = new System.Drawing.Size(569, 20);
            this.totalLabel.TabIndex = 17;
            this.totalLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.totalLabel.Visible = false;
            this.totalLabel.Click += new System.EventHandler(this.OnTotalNumberClicked);
            // 
            // ctlChart
            // 
            this.ctlChart.Colors = "";
            this.ctlChart.GridVisible = false;
            this.ctlChart.Location = new System.Drawing.Point(0, 77);
            this.ctlChart.MaxSeries = 64;
            this.ctlChart.MinimumSize = new System.Drawing.Size(200, 20);
            this.ctlChart.Name = "ctlChart";
            this.ctlChart.Size = new System.Drawing.Size(552, 261);
            this.ctlChart.TabIndex = 21;
            this.ctlChart.SelectedIndexChanged += new System.EventHandler(this.OnChart_SelectedIndexChanged);
            // 
            // ctlXAxis
            // 
            this.ctlXAxis.Location = new System.Drawing.Point(0, 436);
            this.ctlXAxis.Name = "ctlXAxis";
            this.ctlXAxis.Size = new System.Drawing.Size(571, 33);
            this.ctlXAxis.TabIndex = 20;
            this.ctlXAxis.Visible = false;
            this.ctlXAxis.DrawingRatioChanged += new System.EventHandler(this.XAxis_DrawingRatioChanged);
            // 
            // VScrollBarChartControlEx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chartControl);
            this.Controls.Add(this.xAxis);
            this.Controls.Add(this.legends);
            this.Controls.Add(this.totalLabel);
            this.Controls.Add(this.subtitleLabel);
            this.Controls.Add(this.titleLabel);
            this.Name = "VScrollBarChartControlEx";
            this.Size = new System.Drawing.Size(569, 512);
            this.GotFocus += new System.EventHandler(this.VScrollBarChartControlEx_GotFocus);
            this.ResumeLayout(false);

        }

        #endregion

        private VScrollBarChartControl chartControl;
        private XAxisControl xAxis;
        private LegendPanel legends;
        private System.Windows.Forms.Label subtitleLabel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label totalLabel;
        private VScrollBarChartControl ctlChart;
        private XAxisControl ctlXAxis;
    }
}
