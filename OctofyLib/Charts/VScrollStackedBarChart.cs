
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace OctofyLib
{
    public class VScrollStackedBarChart : ChartElementBase
    {
        public enum ChartTypes
        {
            BarChart,
            StackedBarChart
        }

        private readonly List<StackedCategoryBarItem> _bars;
        private double _maxValue;
        private int _selectedIndexX = -1;
        private int _selectedIndexY = -1;
        private string _message;
        private Color _messageColor = Color.Black;
        private List<string> _series;
        private readonly ColorSchema _colors = new ColorSchema("");
        private int _zeroPosition;
        private int _spacing = 5;
        private int _yOffset;
        private int _controlHeight;
        private float _drawingRatio = 0;
        private bool _zeroPositionCalculated;
        private int _seriesCount;
        private string _categoryColumn = "";
        private string _seriesColumn = "";
        private int _maxCategoryNameLength = 0;

        private ChartTypes _chartType = ChartTypes.BarChart;

        public VScrollStackedBarChart()
        {
            _bars = new List<StackedCategoryBarItem>();
            _message = string.Empty;
            LayoutCompleted = false;
        }

        /// <summary>
        /// Gets number of bars in the chart
        /// </summary>
        public int BarCount
        {
            get
            {
                if (_bars == null)
                {
                    return 0;
                }
                else
                {
                    return _bars.Count;
                }
            }
        }

        /// <summary>
        /// Gets or sets color schame name for the chart
        /// </summary>
        public string Colors
        {
            get
            {
                return _colors.ToString();
            }
            set
            {
                if (value != null)
                {
                    _colors.Parse(value);
                    if (_bars != null)
                    {
                        foreach (var bar in _bars)
                            bar.BarColors = _colors;
                    }
                }
            }
        }

        /// <summary>
        /// Sets selected bar
        /// </summary>
        public int SelectedBarIndex
        {
            set
            {
                if (_bars != null)
                {
                    for (int index = 0; index < _bars.Count; index++)
                    {
                        if (index == value)
                        {
                            _bars[index].Selected = true;
                        }
                        else
                        {
                            _bars[index].Selected = false;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the location for zero value
        /// </summary>
        public int ZeroPosition
        {
            get
            {
                return _zeroPosition;
            }

            set
            {
                if (_zeroPosition != value)
                {
                    _zeroPosition = value;
                    LayoutCompleted = false;
                }
            }
        }

        /// <summary>
        /// Gets or sets the distance between two bars
        /// </summary>
        public int Spacing
        {
            get
            {
                return _spacing;
            }

            set
            {
                if (_spacing != value)
                {
                    _spacing = value;
                    LayoutCompleted = false;
                }
            }
        }

        /// <summary>
        /// Sets bar index to highlight
        /// </summary>
        public Point HighlightIndex
        {
            set
            {
                if (_bars != null)
                {
                    for (int index = 0; index < _bars.Count; index++)
                    {
                        if (index == value.Y)
                        {
                            _bars[index].Highlight = true;
                            _bars[index].HighlightIndex = value.X;
                        }
                        else
                        {
                            _bars[index].Highlight = false;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets a distance between the top of the chart and the top of current chart control
        /// </summary>
        public int YOffset
        {
            get
            {
                return _yOffset;
            }

            set
            {
                if (value >= 0)
                {
                    _yOffset = value;
                    LayoutCompleted = false;
                }
            }
        }

        /// <summary>
        /// Gets or sets the height of the host control
        /// </summary>
        public int ControlHeight
        {
            get
            {
                return _controlHeight;
            }

            set
            {
                if (_controlHeight != value)
                {
                    _controlHeight = value;
                    LayoutCompleted = false;
                }
            }
        }

        /// <summary>
        /// Sets the value vs screen px ratio
        /// </summary>
        public float DrawingRatio
        {
            set
            {
                if (_drawingRatio != value)
                {
                    _drawingRatio = value;
                    LayoutCompleted = false;
                }
            }
        }

        /// <summary>
        /// Gets the maximum value of bars
        /// </summary>
        public int MaxValue
        {
            get
            {
                return Convert.ToInt32(_maxValue);
            }
        }

        /// <summary>
        /// Get selected category index
        /// </summary>
        public Point SelectedIndex
        {
            get
            {
                return new Point(_selectedIndexX, _selectedIndexY);
            }
        }

        /// <summary>
        /// The total of the values
        /// </summary>
        public decimal Total { get; private set; }

        /// <summary>
        /// Gets or sets maximum characters will be display for category name.
        /// When exceed the maximum length, ellipse will be used to end a category name.
        /// </summary>
        public int MaxCategoryLength { get; set; } = 40;

        /// <summary>
        /// Clear the chart and draw specified message on the screen
        /// </summary>
        /// <param name="msg"></param>
        public void Clear(string msg, Color color)
        {
            _bars.Clear();
            _messageColor = color;
            MessageForeColor = color;
            _message = msg;
        }

        public Color MessageForeColor { get; set; }

        /// <summary>
        /// Sets or gets a value of maximum categories allows for x-axis.
        /// </summary>
        public int MaximumSeries { get; set; } = 64;

        /// <summary>
        /// Open a stacked bar chart
        /// </summary>
        /// <param name="values">Values for the chart</param>
        /// <param name="categoryColumn">Column name for y-axis</param>
        /// <param name="categories">Categories for y-axis</param>
        /// <param name="seriesColumn">Column name for x-axis</param>
        /// <param name="series">Categories for x-axis</param>
        public void Open(decimal?[,] values, string categoryColumn, List<string> categories, string seriesColumn, List<string> series)
        {
            _chartType = ChartTypes.StackedBarChart;
            _categoryColumn = categoryColumn;
            _seriesColumn = seriesColumn;
            _series = series;
            _bars.Clear();
            if (BarHeight() * categories.Count() >= int.MaxValue)
            {
                _message = Properties.Resources.B001;   // "There are too many data points to fit into the chart.";
                return;
            }

            _maxValue = 0;
            Total = 0;
            if (values.GetLength(1) > MaximumSeries)
            {
                _message = Properties.Resources.B002;   // "There are too many data series to display.";
            }
            else
            {
                int barCount = values.GetLength(0);
                _seriesCount = values.GetLength(1);
                for (int i = 0; i < barCount; i++)
                {
                    int barMax = 0;
                    for (int j = 0; j < _seriesCount; j++)
                    {
                        if (values[i, j].HasValue)
                        {
                            Total += (int)values[i, j];
                            barMax += (int)values[i, j];
                        }
                    }

                    if (barMax > _maxValue)
                    {
                        _maxValue = barMax;
                    }
                }

                _maxCategoryNameLength = 0;
                for (int i = 0; i < barCount; i++)
                {
                    var barValues = new List<int?>();
                    for (int j = 0; j < _seriesCount; j++)
                        barValues.Add((int?)values[i, j]);

                    var bar = new StackedCategoryBarItem()
                    {
                        Font = Font,
                        Name = categories[i],
                        Series = series,
                        BarColors = _colors,
                        MaxNameLength = MaxCategoryLength,
                        Values = barValues
                    };
                    _bars.Add(bar);
                    if (categories[i].Length > _maxCategoryNameLength)
                    {
                        _maxCategoryNameLength = categories[i].Length;
                    }
                }

                if (_bars != null)
                {
                    if (_bars.Count == 1)
                    {
                        base.Height = _bars.Count * BarHeight() + 6 - Spacing;
                    }
                    else
                    {
                        base.Height = _bars.Count * BarHeight() + 6;
                    }
                }

                _message = "";
                _zeroPositionCalculated = false;
                LayoutCompleted = false;
            }
        }

        /// <summary>
        /// Returns a value indicates whether the length of categories exceed the space reserved for drawing category name
        /// </summary>
        public bool CategoryLengthOverflow { get; private set; } = false;

        /// <summary>
        /// Open a bar chart
        /// </summary>
        /// <param name="values">Values for the chart</param>
        /// <param name="categoryColumn">column name for y-axis</param>
        /// <param name="categories">categories for y-axis</param>
        /// <param name="seriesColumn">name of series</param>
        public void Open(decimal?[] values, string categoryColumn, List<string> categories, string seriesColumn)
        {
            _chartType = ChartTypes.BarChart;
            _categoryColumn = categoryColumn;
            _seriesColumn = seriesColumn;
            CategoryLengthOverflow = false;
            _series = new List<string>();
            _series.Add(seriesColumn);

            _bars.Clear();
            if (BarHeight() * categories.Count() >= int.MaxValue)
            {
                _message = Properties.Resources.B001;   // "There are too many data points to fit into the chart.";
                return;
            }

            _maxValue = 0;
            Total = 0;
            int barCount = values.Count();
            _seriesCount = 1;
            for (int i = 0; i < barCount; i++)
            {
                if (values[i].HasValue)
                {
                    Total += (int)values[i];
                    if ((double?)values[i] > _maxValue == true)
                    {
                        _maxValue = (double)values[i];
                    }
                }
            }

            _maxCategoryNameLength = 0;
            for (int i = 0; i < barCount; i++)
            {
                var barValues = new List<int?>() { (int)values[i] };
                var bar = new StackedCategoryBarItem()
                {
                    Font = Font,
                    Name = categories[i],
                    Series = _series,
                    BarColors = _colors,
                    Percentage = PercentText((int?)values[i]),
                    MaxNameLength = MaxCategoryLength,
                    Values = barValues
                };
                _bars.Add(bar);
                if (categories[i].Length > _maxCategoryNameLength)
                {
                    _maxCategoryNameLength = categories[i].Length;
                }
            }

            if (_bars != null)
            {
                if (_bars.Count == 1)
                {
                    base.Height = _bars.Count * BarHeight() + 6 - Spacing;
                }
                else
                {
                    base.Height = _bars.Count * BarHeight() + 6;
                }
            }

            _message = "";
            _zeroPositionCalculated = false;
            LayoutCompleted = false;
        }

        /// <summary>
        /// Returns percentage of the value as a string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string PercentText(int? value)
        {
            string result = "";
            if (Total > 0)
            {
                if (value.HasValue)
                {
                    double percent = (double)(value / Total * 100);
                    result = percent.ToString("N2");
                }
            }

            return result;
        }

        /// <summary>
        /// Get height of a bar
        /// </summary>
        /// <returns></returns>
        internal int BarHeight()
        {
            return base.Font.Height + 6 + Spacing;
        }

        /// <summary>
        /// Returs the current chart image as a picture
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

            Draw(canvas);
            return chartImage;
        }

        /// <summary>
        /// Calculate the space for category labels. 
        /// (Where is the zero value located on x-axis)
        /// (**There is no minus value for this chart)
        /// </summary>
        /// <param name="canvas"></param>
        /// <returns></returns>
        public int GetZeroPosition(Graphics canvas)
        {
            if (!_zeroPositionCalculated)
            {
                if (_bars.Count > 4096)
                {
                    int textLength = _maxCategoryNameLength;
                    if (_maxCategoryNameLength > MaxCategoryLength)
                        textLength = MaxCategoryLength;

                    string text = new string('M', textLength);
                    var textSize = canvas.MeasureString(text, base.Font);
                    _zeroPosition = (int)(Math.Ceiling(textSize.Width) + 12);
                }
                else
                {
                    float maxTextWidth = 60;
                    var Sorted = from p in _bars
                                 orderby p.DisplayName.Length descending
                                 select p.DisplayName;
                    for (int index = 0; index < Sorted.Count(); index++)
                    {
                        string text = Sorted.ElementAtOrDefault(index);
                        var textSize = canvas.MeasureString(text, base.Font);
                        if (textSize.Width > maxTextWidth)
                        {
                            maxTextWidth = textSize.Width;
                        }

                        if (index > 500)
                        {
                            break;
                        }
                    }
                    _zeroPosition = (int)maxTextWidth + 12;
                }

                _zeroPositionCalculated = true;
            }

            return _zeroPosition;
        }

        /// <summary>
        /// Layout elements of the chart
        /// </summary>
        /// <param name="canvas"></param>
        public void PerformLayout(Graphics canvas)
        {
            if (BarCount > 0)
            {
                float textHeight = base.Font.Height + 6;
                ZeroPosition = GetZeroPosition(canvas);
                if (_maxValue == 0)
                {
                    _maxValue = 1;
                }

                int w = Width;
                if (Width < ZeroPosition + 200)
                {
                    w = ZeroPosition + 200;
                    Width = w;
                }

                if (_drawingRatio == 0)
                {
                    _drawingRatio = (float)((w - 24 - ZeroPosition) / _maxValue);
                }

                int y = 3 + base.Top;
                for (int i = 0; i < _bars.Count; i++)
                {
                    _bars[i].Location = new Point(0, y);
                    _bars[i].Size = new Size(Width, Convert.ToInt32(textHeight));
                    _bars[i].ZeroPosition = ZeroPosition;
                    _bars[i].DrawingRatio = _drawingRatio;
                    y += Spacing + (int)textHeight;
                }

                LayoutCompleted = true;
            }
        }

        /// <summary>
        /// Draw the chart
        /// </summary>
        /// <param name="canvas"></param>
        public override void Draw(Graphics canvas)
        {
            if (BarCount > 0)
            {
                if (LayoutCompleted == false)
                {
                    PerformLayout(canvas);
                }

                canvas.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                int startIndex = Convert.ToInt32(Math.Floor(YOffset / (double)BarHeight()));
                if (startIndex < 0)
                {
                    startIndex = 0;
                }

                var endIndex = default(int);
                if (startIndex < BarCount)
                {
                    endIndex = Convert.ToInt32(Math.Ceiling((YOffset + ControlHeight) / (double)BarHeight()));
                    if (endIndex > BarCount - 1)
                    {
                        endIndex = BarCount - 1;
                    }
                }

                for (int i = startIndex; i <= endIndex; i++)
                {
                    if (i >= 0 & i < BarCount)
                    {
                        _bars[i].DrawingRatio = _drawingRatio;
                        _bars[i].YOffset = YOffset;
                        _bars[i].Draw(canvas);
                    }
                }

                int x = _bars[0].ZeroPosition;
                int y1 = 0;
                int y2 = 0;
                if (endIndex >= 0)
                {
                    y2 = _bars[endIndex].Top + _bars[endIndex].Height - YOffset + 6;
                }

                using (var p = new Pen(Color.Gray))
                {
                    canvas.DrawLine(p, x, y1, x, y2);
                }
            }
            else if (_message.Length > 0)
            {
                DrawMessage(canvas, _message, _messageColor);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="location"></param>
        /// <param name="hitIndex"></param>
        /// <param name="hitInfo"></param>
        /// <returns></returns>
        public override bool HitTest(Point location, ref int hitIndex, ref string hitInfo)
        {
            if (BarCount > 0)
            {
                int startIndex = Convert.ToInt32(Math.Floor(YOffset / (double)BarHeight()));
                if (startIndex < 0)
                {
                    startIndex = 0;
                }

                var endIndex = default(int);
                if (startIndex < BarCount)
                {
                    endIndex = Convert.ToInt32(Math.Ceiling((YOffset + ControlHeight) / (double)BarHeight()));
                    if (endIndex > BarCount - 1)
                    {
                        endIndex = BarCount - 1;
                    }
                }

                int y = location.Y + YOffset;
                _selectedIndexX = -1;
                _selectedIndexY = -1;
                for (int i = startIndex; i <= endIndex; i++)
                {
                    if (y >= _bars[i].Top)
                    {
                        if (y <= _bars[i].Top + _bars[i].Height)
                        {
                            int x = location.X;
                            if (x >= base.Left & x <= base.Left + base.Width)
                            {
                                hitIndex = i;
                                _selectedIndexY = i;
                                if (x < ZeroPosition)
                                {
                                    hitInfo = _bars[i].Name;
                                    return true;
                                }
                                else
                                {
                                    decimal cellValue = 0;
                                    if (_bars[i].CellHitTest(location, ref _selectedIndexX, ref cellValue))
                                    {
                                        if (_selectedIndexX >= 0)
                                        {
                                            string categoryValue = _bars[i].Name;
                                            if (categoryValue.Length == 0)
                                            {
                                                categoryValue = Properties.Resources.B003;  // "(Blanks)";
                                            }

                                            if (_chartType == ChartTypes.StackedBarChart)
                                            {
                                                hitInfo = string.Format("{0}:  {1}", _categoryColumn, categoryValue) + "\r\n" +
                                                    string.Format("{0}:  {1}", _seriesColumn, _series[_selectedIndexX]) + "\r\n" +
                                                    string.Format(Properties.Resources.B004, cellValue.ToString("N0"));    //Count:  {0}
                                            }
                                            else
                                            {
                                                hitInfo = string.Format("{0}:  {1}", _categoryColumn, categoryValue) + "\r\n" +
                                                    string.Format(Properties.Resources.B004, cellValue.ToString("N0"));
                                            }

                                            return true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }
    }
}