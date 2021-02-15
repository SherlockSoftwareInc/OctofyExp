using System;

namespace OctofyLib
{
    partial class AnalysisChartPanel
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
            this.seriesAreaChart = new StackedAreaChartControl();
            this.seriesColumnChart = new StackedColumnControl();
            this.barChart = new VScrollBarChartControlEx();
            this.subtitleLabel = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // seriesAreaChart
            // 
            this.seriesAreaChart.Colors = "";
            this.seriesAreaChart.Location = new System.Drawing.Point(-31, -21);
            this.seriesAreaChart.Margin = new System.Windows.Forms.Padding(2);
            this.seriesAreaChart.Name = "seriesAreaChart";
            this.seriesAreaChart.SelectedXIndex = 0;
            this.seriesAreaChart.SelectedYIndex = 0;
            this.seriesAreaChart.Size = new System.Drawing.Size(567, 453);
            this.seriesAreaChart.TabIndex = 10;
            this.seriesAreaChart.Visible = false;
            this.seriesAreaChart.SelectedIndexChange += new EventHandler(SeriesAreaChart_SelectedIndexChange);
            // 
            // seriesColumnChart
            // 
            this.seriesColumnChart.Colors = "";
            this.seriesColumnChart.Location = new System.Drawing.Point(-31, -21);
            this.seriesColumnChart.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.seriesColumnChart.Name = "seriesColumnChart";
            this.seriesColumnChart.SelectedXIndex = 0;
            this.seriesColumnChart.SelectedYIndex = 0;
            this.seriesColumnChart.Size = new System.Drawing.Size(628, 494);
            this.seriesColumnChart.TabIndex = 9;
            this.seriesColumnChart.Visible = false;
            this.seriesColumnChart.SelectedIndexChange += new EventHandler(SeriesBarChart_SelectedIndexChange);
            // 
            // barChart
            // 
            this.barChart.AxisVisible = true;
            this.barChart.Colors = "";
            this.barChart.Location = new System.Drawing.Point(-31, -21);
            this.barChart.Name = "barChart";
            this.barChart.ShowTotal = true;
            this.barChart.Size = new System.Drawing.Size(283, 466);
            this.barChart.Subtitle = "";
            this.barChart.SubtitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.barChart.SubtitleVisible = false;
            this.barChart.TabIndex = 8;
            this.barChart.Title = "";
            this.barChart.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.barChart.TitleVisible = false;
            this.barChart.TotalVisible = true;
            this.barChart.Visible = false;
            this.barChart.SelectedIndexChanged += new EventHandler(BarChart_SelectedIndexChange);
            this.barChart.TotalNumberClicked += new EventHandler(BarChart_TotalNumberClicked);
            // 
            // subtitleLabel
            // 
            this.subtitleLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.subtitleLabel.Location = new System.Drawing.Point(0, 20);
            this.subtitleLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.subtitleLabel.Name = "subtitleLabel";
            this.subtitleLabel.Size = new System.Drawing.Size(696, 18);
            this.subtitleLabel.TabIndex = 12;
            this.subtitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // titleLabel
            // 
            this.titleLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.Location = new System.Drawing.Point(0, 0);
            this.titleLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(696, 20);
            this.titleLabel.TabIndex = 11;
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.seriesAreaChart);
            this.Controls.Add(this.seriesColumnChart);
            this.Controls.Add(this.barChart);
            this.Controls.Add(this.subtitleLabel);
            this.Controls.Add(this.titleLabel);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(722, 498);
            this.ResumeLayout(false);

        }

        #endregion

        private StackedAreaChartControl seriesAreaChart;
        private StackedColumnControl seriesColumnChart;
        private VScrollBarChartControlEx barChart;
        private System.Windows.Forms.Label subtitleLabel;
        private System.Windows.Forms.Label titleLabel;
    }
}
