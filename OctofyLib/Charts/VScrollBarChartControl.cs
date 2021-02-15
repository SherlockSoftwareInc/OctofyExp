using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OctofyLib
{
    /// <summary>
    /// 
    /// </summary>
    public partial class VScrollBarChartControl : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler SelectedIndexChanged;

        private readonly VScrollStackedBarChart _stackedBarChart;
        private string _tooltipText = "";
        private Point _selectedIndex;
        private bool _scrollBarVisible;
        private float _drawingRatio = 0;
        private int _maxHeight;
        private readonly List<int> _gridPositions = new List<int>();


        /// <summary>
        /// 
        /// </summary>
        public VScrollBarChartControl()
        {
            InitializeComponent();

            _stackedBarChart = new VScrollStackedBarChart()
            {
                Left = 0,
                Top = 3,
                Width = Width,
                Height = 100,
                Font = Font,
                ForeColor = ForeColor
            };
            _maxHeight = Height;
            scrollBar.Name = "scrollBar";
        }

        /// <summary>
        /// 
        /// </summary>
        public string Colors
        {
            get
            {
                if (_stackedBarChart is object)
                {
                    return _stackedBarChart.Colors;
                }
                else
                {
                    return "";
                }
            }

            set
            {
                if (value is object & _stackedBarChart is object)
                {
                    _stackedBarChart.Colors = value;
                }
            }
        }

        /// <summary>
        /// Gets selected bar index
        /// </summary>
        /// <returns></returns>
        public Point SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
        }

        /// <summary>
        /// Gets grand total of the values
        /// </summary>
        /// <returns></returns>
        public int Total
        {
            get
            {
                return Convert.ToInt32(_stackedBarChart.Total);
            }
        }

        /// <summary>
        /// Gets grand total of the values
        /// </summary>
        /// <returns></returns>
        public int ZeroPosition
        {
            get
            {
                return _stackedBarChart.ZeroPosition;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int MaxValue
        {
            get
            {
                return _stackedBarChart.MaxValue;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public float DrawingRatio
        {
            set
            {
                if (_stackedBarChart is object)
                {
                    _drawingRatio = value;
                    _stackedBarChart.DrawingRatio = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or set grid mark positions
        /// </summary>
        public List<int> GridPositions
        {
            get
            {
                return _gridPositions;
            }
            set
            {
                _gridPositions.Clear();
                if (value != null)
                {
                    for (int i = 0; i < value.Count; i++)
                    {
                        _gridPositions.Add(value[i]);
                    }
                }
                else
                {
                    _gridPositions.Add(0);
                }
                GridVisible = value.Count > 1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool GridVisible { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="canvas"></param>
        /// <returns></returns>
        public int GetZeroPosition(Graphics canvas)
        {
            return _stackedBarChart.GetZeroPosition(canvas);
        }

        /// <summary>
        /// Clear the graph with a message
        /// </summary>
        /// <param name="msg"></param>
        public void Clear(string msg, Color color)
        {
            _stackedBarChart.Clear(msg, color);
            Invalidate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        public void SelectCategory(int index)
        {
            _stackedBarChart.SelectedBarIndex = index;

            // ensure the selected bar is visible is scroll bar visible
            if (scrollBar.Visible)
            {
                int barTop = index * _stackedBarChart.BarHeight();
                if (barTop < scrollBar.Value)
                {
                    scrollBar.Value = barTop;
                }
                else if (barTop - scrollBar.Height + _stackedBarChart.BarHeight() > scrollBar.Value)
                {
                    if (barTop > scrollBar.Maximum)
                    {
                        scrollBar.Value = scrollBar.Maximum;
                    }
                    else
                    {
                        scrollBar.Value = barTop;
                    }
                }
            }

            Invalidate();
        }

        /// <summary>
        /// 
        /// </summary>
        public bool CategoryLengthOverflow
        {
            get
            {
                return _stackedBarChart.CategoryLengthOverflow;
            }
        }

        /// <summary>
        /// Open a bar chart with given values and categories
        /// </summary>
        /// <param name="values"></param>
        /// <param name="categoryColumn"></param>
        /// <param name="categories"></param>
        /// <param name="seriesColumn"></param>
        public void Open(decimal?[] values, string categoryColumn, List<string> categories, string seriesColumn)
        {
            // _chartType = ChartTypes.StackedBarChart

            _stackedBarChart.Open(values, categoryColumn, categories, seriesColumn);
            PerformAutoHeight(_maxHeight);

            // Control the visibility of the vertical scroll bar
            _scrollBarVisible = _stackedBarChart.Height > PlotHeight();
            scrollBar.Visible = _scrollBarVisible;

            // populate scroll bar by setting the maximum, large change and small change value
            PopulateScrollBar();

            // If scroll bar is visible, scroll to top and set focus on it
            if (_scrollBarVisible)
            {
                scrollBar.Value = 0;
            }

            Invalidate();
        }

        public int MaxSeries { get; set; } = 64;

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
            // _chartType = ChartTypes.StackedBarChart
            _stackedBarChart.MaximumSeries = MaxSeries;
            _stackedBarChart.Open(values, categoryColumn, categories, seriesColumn, series);
            PerformAutoHeight(_maxHeight);

            // Control the visibility of the vertical scroll bar
            _scrollBarVisible = _stackedBarChart.Height > PlotHeight();
            scrollBar.Visible = _scrollBarVisible;

            // populate scroll bar by setting the maximum, large change and small change value
            PopulateScrollBar();

            // If scroll bar is visible, scroll to top and set focus on it
            if (_scrollBarVisible)
            {
                scrollBar.Value = 0;
            }

            Invalidate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxHeight"></param>
        public void PerformAutoHeight(int maxHeight)
        {
            _maxHeight = maxHeight;

            if (_stackedBarChart is object)
            {
                if (_stackedBarChart.Height + 3 > maxHeight)
                {
                    Height = maxHeight;
                }
                else
                {
                    Height = _stackedBarChart.Height + 3;
                }
            }
        }

        /// <summary>
        /// Build current chart view as an image and return it
        /// </summary>
        /// <returns></returns>
        public Bitmap ToImage()
        {
            var chartImage = new Bitmap(Width, Height);
            var canvas = Graphics.FromImage(chartImage);
            if (BackColor == Color.Transparent)
            {
                canvas.Clear(Color.White);
            }
            else
            {
                canvas.Clear(BackColor);
            }

            if (_scrollBarVisible)
            {
                _stackedBarChart.YOffset = scrollBar.Value;
            }
            else
            {
                _stackedBarChart.YOffset = 0;
            }

            _stackedBarChart.Draw(canvas);

            return chartImage;
        }

        /// <summary>
        /// Handle paint event: draw current view of the bar chart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VScrollBarChartControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(base.BackColor);
            int y1;
            int y2;
            int yOffset;
            if (_scrollBarVisible)
            {
                yOffset = scrollBar.Value;
            }
            else
            {
                yOffset = 0;
            }

            if (GridVisible && _stackedBarChart.BarCount > 0)
            {
                if (_gridPositions.Count > 1)
                {
                    y1 = 0;
                    y2 = PlotHeight();
                    int chartHeight;

                    chartHeight = _stackedBarChart.Height;

                    if (yOffset - chartHeight > y2)
                    {
                        y2 = yOffset - chartHeight;
                    }

                    using (var p = new Pen(Color.LightGray) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dot })
                    {
                        for (int i = 0; i < _gridPositions.Count; i++)
                            e.Graphics.DrawLine(p, _gridPositions[i], y1, _gridPositions[i], y2);
                    }
                }
            }

            if (_stackedBarChart is object)
            {
                _stackedBarChart.YOffset = yOffset;
                if (_drawingRatio == 0)
                {
                    _stackedBarChart.DrawingRatio = 0;
                }

                _stackedBarChart.Draw(e.Graphics);
            }
        }

        /// <summary>
        /// Handle mouse wheel event: scroll up or down based on the scroll direction
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VScrollBarChartControl_MouseWheel(object sender, MouseEventArgs e)
        {
            if (_scrollBarVisible)
            {
                int y;
                if (e.Delta > 0)
                {
                    y = scrollBar.Value - scrollBar.SmallChange;
                }
                else
                {
                    y = scrollBar.Value + scrollBar.SmallChange;
                }

                if (y < 0)
                {
                    y = 0;
                }
                else if (y > scrollBar.Maximum)
                {
                    y = scrollBar.Maximum;
                }

                if (y >= 0 & y <= scrollBar.Maximum)
                {
                    scrollBar.Value = y;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Handle control client size change:
        /// 1. Determine whether to show vertical scroll bar or not
        /// 2. Set chart width and tell chart the height of its host
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VScrollBarChartControl_ClientSizeChanged(object sender, EventArgs e)
        {
            if (_stackedBarChart is object)
            {
                int h = PlotHeight();
                if (_stackedBarChart.Height > h)
                {
                    _scrollBarVisible = true;
                }
                else
                {
                    _scrollBarVisible = false;
                }

                if (scrollBar.Visible != _scrollBarVisible)
                {
                    scrollBar.Visible = _scrollBarVisible;
                }

                int w = ClientRectangle.Width - 6;
                if (_scrollBarVisible)
                {
                    w -= scrollBar.Width;
                }

                _stackedBarChart.Width = w;

                // _barChart.ControlHeight = h
                _stackedBarChart.ControlHeight = h;
                if (_scrollBarVisible)
                {
                    PopulateScrollBar();
                }

                Invalidate();
            }
            // End If
        }

        /// <summary>
        /// Handle arrow key, page up/down and home/end key press
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VScrollBarChartControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (scrollBar.Visible)
            {
                if (e.KeyCode == Keys.Home)
                {
                    scrollBar.Value = 0;
                    Invalidate();
                }
                else if (e.KeyCode == Keys.End)
                {
                    scrollBar.Value = scrollBar.Maximum;
                    Invalidate();
                }
                else
                {
                    int delta = 0;
                    if (e.KeyCode == Keys.PageUp)
                    {
                        delta = -scrollBar.LargeChange;
                    }
                    else if (e.KeyCode == Keys.PageDown)
                    {
                        delta = scrollBar.LargeChange;
                    }
                    else if (e.KeyCode == Keys.Up)
                    {
                        delta = -scrollBar.SmallChange;
                    }
                    else if (e.KeyCode == Keys.Down)
                    {
                        delta = scrollBar.SmallChange;
                    }

                    if (delta != 0)
                    {
                        int y = scrollBar.Value + delta;
                        if (y < 0)
                        {
                            y = 0;
                        }
                        else if (y > scrollBar.Maximum)
                        {
                            y = scrollBar.Maximum;
                        }

                        scrollBar.Value = y;
                        Invalidate();
                    }
                }
            }
        }

        /// <summary>
        /// Handle mouse click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VScrollBarChartControl_MouseClick(object sender, MouseEventArgs e)
        {
            var index = default(int);
            if (_stackedBarChart is object)
            {
                string info = "";
                if (_stackedBarChart.HitTest(e.Location, ref index, ref info))
                {
                    _selectedIndex = _stackedBarChart.SelectedIndex;
                    SelectBar(_selectedIndex.Y);
                    SelectedIndexChanged?.Invoke(this, new EventArgs());
                }
                else
                {
                    SelectBar(-1);
                    if (_selectedIndex.X >= 0 | _selectedIndex.Y >= 0)
                    {
                        _selectedIndex = new Point(-1, -1);
                        SelectedIndexChanged?.Invoke(this, new EventArgs());
                    }
                }
            }
            // End If
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="barIndex"></param>
        public void SelectBar(int barIndex)
        {
            _stackedBarChart.SelectedBarIndex = barIndex;
            // TODO: ensure bar visible
            Invalidate();
        }

        /// <summary>
        /// Handle mouse move event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VScrollBarChartControl_MouseMove(object sender, MouseEventArgs e)
        {
            var index = default(int);
            if (_stackedBarChart is object)
            {
                string info = "";
                if (_stackedBarChart.HitTest(e.Location, ref index, ref info))
                {
                    if (_hightlightIndexY != _stackedBarChart.SelectedIndex.Y | _hightlightIndexX != _stackedBarChart.SelectedIndex.X)
                    {
                        _hightlightIndexY = _stackedBarChart.SelectedIndex.Y;
                        _hightlightIndexX = _stackedBarChart.SelectedIndex.X;
                        _stackedBarChart.HighlightIndex = _stackedBarChart.SelectedIndex;
                        Invalidate();
                    }

                    if ((info ?? "") != (_tooltipText ?? ""))
                    {
                        _tooltipText = info;
                        toolTip.SetToolTip(this, info);
                    }
                }
                else
                {
                    if (_tooltipText.Length > 0)
                    {
                        _tooltipText = "";
                        toolTip.RemoveAll();
                    }

                    if (_hightlightIndexX >= 0 | _hightlightIndexY >= 0)
                    {
                        _hightlightIndexX = -1;
                        _hightlightIndexY = -1;
                        _stackedBarChart.HighlightIndex = new Point(-1, -1);
                        Invalidate();
                    }
                }
            }
        }

        private int _hightlightIndexY = -1;
        private int _hightlightIndexX = -1;

        /// <summary>
        /// Handle control load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VScrollBarChartControl_Load(object sender, EventArgs e)
        {
            _stackedBarChart.Location = new Point(3, 3);
            int w = ClientRectangle.Width;
            if (scrollBar.Visible)
            {
                w -= scrollBar.Width;
            }
            _stackedBarChart.Width = w;
        }

        /// <summary>
        /// Handle scroll bar scroll event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            Invalidate();
        }

        /// <summary>
        /// 
        /// </summary>
        private void PopulateScrollBar()
        {
            if (_stackedBarChart is object)
            {
                if (_scrollBarVisible)
                {
                    int h = PlotHeight();   // scrollBar.Height
                    int scrollMax = _stackedBarChart.Height - h;
                    int largeChange = (int)(h * 0.85);
                    int smallChange = (int)(h / (double)10);
                    if (largeChange > scrollMax)
                    {
                        largeChange = (int)(scrollMax / (double)2);
                        smallChange = (int)(largeChange / (double)5);
                        if (smallChange < 1)
                        {
                            smallChange = 1;
                        }
                    }

                    if (scrollMax > 0 & largeChange > 0)
                    {
                        scrollBar.Maximum = scrollMax;
                        scrollBar.LargeChange = largeChange;
                        scrollBar.SmallChange = smallChange;
                    }
                }
            }
        }

        private int PlotHeight()
        {
            return ClientRectangle.Height - 3;
        }

        private void ScrollBar_GotFocus(object sender, EventArgs e)
        {
            Focus();
        }
    }
}