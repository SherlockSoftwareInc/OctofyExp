using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OctofyLib
{
    /// <summary>
    /// 
    /// </summary>
    public partial class AnalysisChartPanel : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler SelectedIndexChanged;
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler TotalNumberClicked;

        /// <summary>
        /// Enumeration to sepcify the type of chart
        /// </summary>
        public enum ChartTypes
        {
            None,
            BarChart,
            PieChart,
            StackedBarChart,
            TimeSeriesColumnChart,
            TimeSeriesAreaChart
        }

        private ChartTypes _chartType = ChartTypes.None;

        /// <summary>
        /// 
        /// </summary>
        public AnalysisChartPanel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        public int SelectedIndexX { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int SelectedIndexY { get; set; }

        public int MaxSeries { get; set; }

        /// <summary>
        /// Gets or sets chart title text
        /// </summary>
        /// <returns></returns>
        public string Title
        {
            get
            {
                return titleLabel.Text;
            }

            set
            {
                titleLabel.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets font of title label
        /// </summary>
        /// <returns></returns>
        public Font TitleFont
        {
            get
            {
                return titleLabel.Font;
            }

            set
            {
                titleLabel.Font = value;
            }
        }

        /// <summary>
        /// Gets or sets chart sub title text
        /// </summary>
        /// <returns></returns>
        public string Subtitle
        {
            get
            {
                return subtitleLabel.Text;
            }

            set
            {
                subtitleLabel.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets font of title label
        /// </summary>
        /// <returns></returns>
        public Font SubtitleFont
        {
            get
            {
                return subtitleLabel.Font;
            }

            set
            {
                subtitleLabel.Font = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Colors { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public ChartTypes ChartType
        {
            get
            {
                return _chartType;
            }

            set
            {
                if (_chartType != value)
                {
                    _chartType = value;
                    barChart.Visible = false;
                    seriesColumnChart.Visible = false;
                    seriesAreaChart.Visible = false;
                    barChart.Dock = DockStyle.None;
                    seriesColumnChart.Dock = DockStyle.None;
                    seriesAreaChart.Dock = DockStyle.None;
                    barChart.Dock = DockStyle.None;
                    seriesColumnChart.Dock = DockStyle.None;
                    seriesAreaChart.Dock = DockStyle.None;
                    var switchExpr = _chartType;
                    switch (switchExpr)
                    {
                        case ChartTypes.TimeSeriesAreaChart:
                            {
                                seriesAreaChart.Dock = DockStyle.Fill;
                                seriesAreaChart.Visible = true;
                                seriesAreaChart.BringToFront();
                                seriesAreaChart.Focus();
                                break;
                            }

                        case ChartTypes.BarChart:
                        case ChartTypes.StackedBarChart:
                            {
                                barChart.Dock = DockStyle.Fill;
                                barChart.Visible = true;
                                barChart.BringToFront();
                                barChart.Focus();
                                break;
                            }

                        case ChartTypes.TimeSeriesColumnChart:
                            {
                                seriesColumnChart.Dock = DockStyle.Fill;
                                seriesColumnChart.Visible = true;
                                seriesColumnChart.BringToFront();
                                seriesColumnChart.Focus();
                                break;
                            }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public void Clear(string msg, Color color)
        {
            seriesAreaChart.Visible = false;
            barChart.Visible = false;
            seriesColumnChart.Visible = false;
            DrawMessage(CreateGraphics(), msg, color);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="msg"></param>
        public void DrawMessage(Graphics canvas, string msg, Color color)
        {
            canvas.Clear(base.BackColor);
            if (msg.Length > 0)
            {
                var oTextformat = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                canvas.DrawString(msg, Font, new SolidBrush(color), ClientRectangle, oTextformat);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Bitmap ToImage()
        {
            Bitmap chartImage = null;
            var switchExpr = _chartType;
            switch (switchExpr)
            {
                case ChartTypes.TimeSeriesAreaChart:
                    {
                        chartImage = seriesAreaChart.ToImage();
                        break;
                    }

                case ChartTypes.BarChart:
                case ChartTypes.StackedBarChart:
                    {
                        chartImage = barChart.ToImage();
                        break;
                    }

                case ChartTypes.TimeSeriesColumnChart:
                    {
                        chartImage = seriesColumnChart.ToImage();
                        break;
                    }
            }

            if (chartImage is object)
            {
                if (titleLabel.Visible | subtitleLabel.Visible)
                {
                    int h = chartImage.Height;
                    int w = chartImage.Width;
                    int y = 0;
                    if (titleLabel.Visible)
                    {
                        y = titleLabel.Height;
                    }

                    if (subtitleLabel.Visible)
                    {
                        y += subtitleLabel.Height;
                    }

                    var img = new Bitmap(w, h + y);
                    var canvas = Graphics.FromImage(img);
                    if (BackColor == Color.Transparent)
                    {
                        canvas.Clear(Color.White);
                    }
                    else
                    {
                        canvas.Clear(BackColor);
                    }

                    canvas.DrawImage(chartImage, 0, y, chartImage.Width, chartImage.Height);
                    y = 0;
                    using (var br = new SolidBrush(base.ForeColor))
                    {
                        var oTextformat = new StringFormat() { FormatFlags = StringFormatFlags.NoWrap };
                        oTextformat.Alignment = StringAlignment.Center;
                        oTextformat.LineAlignment = StringAlignment.Center;
                        if (titleLabel.Visible)
                        {
                            canvas.DrawString(titleLabel.Text, titleLabel.Font, new SolidBrush(Color.Black), new RectangleF(0, y, Width, titleLabel.Height), oTextformat);
                            y += titleLabel.Height;
                        }

                        if (subtitleLabel.Visible)
                        {
                            canvas.DrawString(subtitleLabel.Text, subtitleLabel.Font, new SolidBrush(Color.Black), new RectangleF(0, y, Width, subtitleLabel.Height), oTextformat);
                        }
                    }

                    return img;
                }
                else
                {
                    return chartImage;
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <param name="categoryColumn"></param>
        /// <param name="categories"></param>
        /// <param name="seriesColumn"></param>
        public void OpenBarChart(decimal?[] values, string categoryColumn, List<string> categories, string seriesColumn)
        {
            ChartType = ChartTypes.BarChart;
            barChart.Open(values, categoryColumn, categories, seriesColumn);
            if (barChart.Visible == false)
                barChart.Visible = true;
            barChart.Focus();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <param name="categoryColumn"></param>
        /// <param name="categories"></param>
        /// <param name="seriesColumn"></param>
        /// <param name="series"></param>
        public void OpenStackedBarChart(decimal?[,] values, string categoryColumn, List<string> categories, string seriesColumn, List<string> series)
        {
            ChartType = ChartTypes.StackedBarChart;
            barChart.Colors = Colors;
            barChart.MaxSeries = MaxSeries;
            barChart.Open(values, categoryColumn, categories, seriesColumn, series);
            if (barChart.Visible == false)
                barChart.Visible = true;
            barChart.Focus();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <param name="categories"></param>
        /// <param name="series"></param>
        public void OpenSeriesColumnChart(decimal?[,] values, List<string> categories, List<string> series)
        {
            ChartType = ChartTypes.TimeSeriesColumnChart;
            seriesColumnChart.Open(series, values, categories);
            if (seriesColumnChart.Visible == false)
                seriesColumnChart.Visible = true;
            seriesColumnChart.Focus();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <param name="categories"></param>
        /// <param name="series"></param>
        public void OpenSeriesAreaChart(decimal?[,] values, List<string> categories, List<string> series)
        {
            ChartType = ChartTypes.TimeSeriesAreaChart;
            seriesAreaChart.Colors = Colors;
            seriesAreaChart.Open(series, values, categories);
            if (seriesAreaChart.Visible == false)
                seriesAreaChart.Visible = true;
            seriesAreaChart.Focus();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        public void SelectCategory(int index)
        {
            if (ChartType == ChartTypes.BarChart | ChartType == ChartTypes.StackedBarChart)
            {
                barChart.SelectCategory(index);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarChart_SelectedIndexChange(object sender, EventArgs e)
        {
            SelectedIndexX = barChart.SelectedIndex.X;
            SelectedIndexY = barChart.SelectedIndex.Y;
            SelectedIndexChanged?.Invoke(this, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SeriesAreaChart_SelectedIndexChange(object sender, EventArgs e)
        {
            SelectedIndexX = seriesAreaChart.SelectedXIndex;
            SelectedIndexY = seriesAreaChart.SelectedYIndex;
            SelectedIndexChanged?.Invoke(this, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SeriesBarChart_SelectedIndexChange(object sender, EventArgs e)
        {
            SelectedIndexX = seriesColumnChart.SelectedXIndex;
            SelectedIndexY = seriesColumnChart.SelectedYIndex;
            SelectedIndexChanged?.Invoke(this, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarChart_TotalNumberClicked(object sender, EventArgs e)
        {
            TotalNumberClicked?.Invoke(sender, e);
        }

    }
}