using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OctofyLib
{
    public partial class VScrollBarChartControlEx : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler SelectedIndexChanged;
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler TotalNumberClicked;

        private bool _legendsVisible = true;
        private string _colorSchema = "";
        private int _maxSeries = 64;

        /// <summary>
        /// 
        /// </summary>
        public VScrollBarChartControlEx()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets colors for the chart
        /// </summary>
        public string Colors
        {
            get
            {
                return _colorSchema;
            }
            set
            {
                if (value != null)
                    if (_colorSchema != value)
                    {
                        _colorSchema = value;
                        chartControl.Colors = value;
                        legends.Colors = value;
                    }
            }
        }

        /// <summary>
        /// 
        /// </summary>
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
        /// 
        /// </summary>
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
        /// 
        /// </summary>
        public bool TitleVisible
        {
            get
            {
                return titleLabel.Visible;
            }

            set
            {
                titleLabel.Visible = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
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
        /// 
        /// </summary>
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
        public bool SubtitleVisible
        {
            get
            {
                return subtitleLabel.Visible;
            }

            set
            {
                subtitleLabel.Visible = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool TotalVisible
        {
            get
            {
                return totalLabel.Visible;
            }

            set
            {
                totalLabel.Visible = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool AxisVisible { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool ShowTotal
        {
            get
            {
                if (totalLabel is object)
                {
                    return totalLabel.Visible;
                }
                else
                {
                    return false;
                }
            }

            set
            {
                if (totalLabel is object)
                {
                    totalLabel.Visible = value;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Point SelectedIndex
        {
            get
            {
                return chartControl.SelectedIndex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public void Clear(string msg, Color color)
        {
            chartControl.GridVisible = false;
            chartControl.Clear(msg, color);
            totalLabel.Text = "";
            xAxis.Visible = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        public void SelectCategory(int index)
        {
            chartControl.SelectCategory(index);
        }

        /// <summary>
        /// 
        /// </summary>
        public bool CategoryLengthOverflow
        {
            get
            {
                return chartControl.CategoryLengthOverflow;
            }
        }

        /// <summary>
        /// Open bar chart
        /// </summary>
        /// <param name="values"></param>
        /// <param name="categoryColumn"></param>
        /// <param name="categories"></param>
        /// <param name="seriesColumn"></param>
        public void Open(decimal?[] values, string categoryColumn, List<string> categories, string seriesColumn)
        {
            chartControl.Open(values, categoryColumn, categories, seriesColumn);
            xAxis.Visible = AxisVisible;
            _legendsVisible = false;     // no legends for bar chart
            legends.Visible = _legendsVisible;
            chartControl.GridVisible = AxisVisible;
            if (!AxisVisible)
            {
                chartControl.DrawingRatio = 0;
            }

            PerformLayout();
            using (var canvas = CreateGraphics())
            {
                xAxis.ZeroPosition = chartControl.GetZeroPosition(canvas);
                xAxis.Maximum = chartControl.MaxValue;
                if (xAxis.Visible)
                {
                    xAxis.DoPerformLayout(canvas);
                    chartControl.DrawingRatio = xAxis.DrawingRatio;
                }
                else
                {
                    chartControl.DrawingRatio = 0;
                }
            }

            chartControl.Invalidate();
            int total = chartControl.Total;
            if (total > 0)
            {
                totalLabel.Text = string.Format(Properties.Resources.B005, total.ToString("N0"));   //B005: Grand total:  {0}
            }
            else
            {
                totalLabel.Text = "";
            }
        }

        public int MaxSeries
        {
            get { return _maxSeries; }
            set
            {
                if (value < 1 || value > 256)
                {
                    throw new ArgumentOutOfRangeException(Properties.Resources.B006, Properties.Resources.B007);
                    //B006: Number of legend item
                    //B007: The maximum number of legend items is 1 to 256.
                }
                else
                    _maxSeries = value;
            }
        }

        /// <summary>
        /// Open stacked bar chart
        /// </summary>
        /// <param name="values"></param>
        /// <param name="categoryColumn"></param>
        /// <param name="categories"></param>
        /// <param name="seriesColumn"></param>
        /// <param name="series"></param>
        public void Open(decimal?[,] values, string categoryColumn, List<string> categories, string seriesColumn, List<string> series)
        {
            chartControl.MaxSeries = MaxSeries;
            chartControl.Open(values, categoryColumn, categories, seriesColumn, series);
            xAxis.Visible = AxisVisible;
            _legendsVisible = true;
            legends.Visible = _legendsVisible;
            if (_legendsVisible)
            {
                if (series.Count == 1)
                    legends.Colors = "#87CEEB";
                else
                    legends.Colors = "";
                legends.Open(series);
            }

            chartControl.GridVisible = AxisVisible;
            if (!AxisVisible)
            {
                chartControl.DrawingRatio = 0;
            }

            PerformLayout();
            using (var canvas = CreateGraphics())
            {
                xAxis.ZeroPosition = chartControl.GetZeroPosition(canvas);
                xAxis.Maximum = chartControl.MaxValue;
                if (xAxis.Visible)
                {
                    xAxis.DoPerformLayout(canvas);
                    chartControl.DrawingRatio = xAxis.DrawingRatio;
                }
                else
                {
                    chartControl.DrawingRatio = 0;
                }
            }

            chartControl.Invalidate();
            int total = chartControl.Total;
            if (total > 0)
            {
                totalLabel.Text = string.Format(Properties.Resources.B005, total.ToString("N0"));   //B005: Grand total:  {0}
            }
            else
            {
                totalLabel.Text = "";
            }

            Invalidate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Bitmap ToImage()
        {
            return chartControl.ToImage();
        }

        /// <summary>
        /// Handle total number label click event:
        ///     Tell the host that total number label has been clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTotalNumberClicked(object sender, EventArgs e)
        {
            if (totalLabel.Text.Length > 0 && totalLabel.Visible)
                TotalNumberClicked?.Invoke(sender, e);
        }

        /// <summary>
        /// Handle chart selected index change event:
        ///     Pass the event to the host
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnChart_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedIndexChanged?.Invoke(sender, e);
        }

        /// <summary>
        /// Handle x-axis control drawing ration change event:
        ///     Set the chart with same same drawing ratio with the x-axis control
        ///     and pass the grid mark positions to the chart so that the chart
        ///     can draw grid lines
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XAxis_DrawingRatioChanged(object sender, EventArgs e)
        {
            chartControl.GridPositions = xAxis.MarkPositions;
            chartControl.DrawingRatio = xAxis.DrawingRatio;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);
            SuspendLayout();
            int y = 0;
            int h = ClientRectangle.Height;
            int w = ClientRectangle.Width;
            int bl = ClientRectangle.Left;
            if (TitleVisible)
            {
                y += titleLabel.Height;
            }

            if (SubtitleVisible)
            {
                y = subtitleLabel.Top + subtitleLabel.Height;
            }

            h -= y;
            if (TotalVisible)
            {
                h -= totalLabel.Height;
            }

            using (var canvas = CreateGraphics())
            {
                if (_legendsVisible)
                {
                    legends.Left = bl;
                    if (legends.Width != w)
                    {
                        legends.Width = w;
                    }
                    //legends.PerformAutoSize(canvas);
                    h -= legends.Height;
                }

                if (AxisVisible)
                {
                    xAxis.Left = bl;
                    xAxis.Width = w;
                    xAxis.DoPerformLayout(canvas);
                    h -= xAxis.Height;
                }

                chartControl.Location = new Point(bl, y);
                chartControl.Width = w;
                if (h > 0)
                {
                    chartControl.PerformAutoHeight(h);
                    y += chartControl.Height;
                    if (AxisVisible)
                    {
                        xAxis.Top = y;
                        xAxis.DoPerformLayout(canvas);
                        chartControl.DrawingRatio = xAxis.DrawingRatio;
                    }
                    else
                    {
                        chartControl.DrawingRatio = 0;
                    }

                    if (_legendsVisible)
                    {
                        y += xAxis.Height;
                        legends.Top = y;
                    }
                }
            }

            chartControl.PerformLayout();
            ResumeLayout(false);
            chartControl.Invalidate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VScrollBarChartControlEx_GotFocus(object sender, EventArgs e)
        {
            chartControl.Focus();
        }

        /// <summary>
        /// Handle legends control size change event:
        ///     Perform layout again
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Legends_SizeChanged(object sender, EventArgs e)
        {
            if (_legendsVisible)
            {
                PerformLayout();
            }
        }
    }
}