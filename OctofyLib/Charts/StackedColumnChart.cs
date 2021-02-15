
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace OctofyLib
{
    public class StackedColumnChart : ChartElementBase
    {

        private readonly bool _controlsReady;
        private readonly StackedColumnPlot _chart;
        private readonly XAxisMarkerPlot _xAxis;
        private readonly YAxisMarkers _yAxis;
        private readonly ChartLegends _legends;          // Chart legends for boxplot
        private Rectangle _chartRect;
        private int _maxValue = 100;
        private readonly int _maxScale;
        private ColorSchema _colors;
        private short _numOfPeriod;
        private readonly ChartLabel _title;                    // Chart title
        private readonly ChartLabel _subtitle;                 // Chart subtitle
        private int _chartHeight;
        private int _chartWidth;
        private int _chartLeft;
        private int _chartTop;
        private string _message;
        private bool _tooManyVariables;
        private Color _messageColor = Color.Black;

        public StackedColumnChart(Font font)
        {
            Font = font;
            Padding = new Padding(6);
            _colors = new ColorSchema();
            _title = new ChartLabel(new Font(Font.Name, 12.0F, FontStyle.Bold))
            {
                Padding = new Padding(3, 3, 3, 3),
                Text = ""
            };
            _subtitle = new ChartLabel(Font) { Text = "" };
            _xAxis = new XAxisMarkerPlot()
            {
                Font = Font,
                ForeColor = ForeColor,
                AxisColor = Color.DarkGray
            };
            _yAxis = new YAxisMarkers(Font)
            {
                Alignment = YAxisMarkers.Alignments.Right,
                AxisValueType = YAxisMarkers.AxisValueTypes.Values,
                AutoWidth = true,
                Minimum = 0,
                Maximum = 100,
                ScaleMax = _maxScale,
                Font = Font,
                ForeColor = ForeColor,
                MarkersVisible = true,
                TargetSteps = 10
            };
            _legends = new ChartLegends()
            {
                Font = font,
                Padding = new Padding(0, 3, 0, 3),
                AutoWidth = true,
                Orientation = ChartLegends.Orientations.Horizontal
            };
            _chart = new StackedColumnPlot()
            {
                Font = Font,
                BarDirection = StackedColumn.BarDirections.BottomToTop,
                CaptionType = CaptionTypes.Volume,
                BorderStyle = BorderStyle.None,
                BarColors = _colors
            };
            _controlsReady = true;
            _tooManyVariables = false;
            _message = string.Empty;
        }
        /* TODO ERROR: Skipped EndRegionDirectiveTrivia */
        /* TODO ERROR: Skipped RegionDirectiveTrivia */
        public int HitPeriodIndex { get; set; }
        public DateTime HitPeriodStartDate { get; set; }
        public DateTime HitPeriodEndDate { get; set; }
        public BorderStyle BorderStyle { get; set; }

        public string Colors
        {
            get
            {
                if (_colors == null)
                {
                    return string.Empty;
                }
                else
                {
                    return _colors.SchemaName;
                }
            }

            set
            {
                _colors = new ColorSchema(value);
                if (_chart != null)
                {
                    _chart.BarColors = _colors;
                }
            }
        }

        public string TitleText
        {
            set
            {
                _title.Text = value;
                LayoutCompleted = false;
            }
        }

        public string SubtitleText
        {
            set
            {
                _subtitle.Text = value;
                LayoutCompleted = false;
            }
        }

        /* TODO ERROR: Skipped EndRegionDirectiveTrivia */
        /* TODO ERROR: Skipped RegionDirectiveTrivia */    /// <summary>
                                                           /// Clear elements in the chart
                                                           /// </summary>
                                                           /// <remarks></remarks>
        public void Clear(string msg, Color color)
        {
            _chart.Clear();
            _messageColor = color;
            _maxValue = 100;
            _numOfPeriod = 0;
            _message = msg;
        }

        public void Open(List<string> seriesNames, decimal?[,] values, List<TimePeriod> periods)
        {
            if (values.GetLength(1) > 64)
            {
                _tooManyVariables = true;
            }
            else if (periods.Count > 64)
            {
                _tooManyVariables = true;
            }
            else
            {
                _tooManyVariables = false;
            }

            _numOfPeriod = (short)(periods.Count);
            if (_numOfPeriod > 0 & _tooManyVariables == false)
            {
                _xAxis.Open(periods);
                _maxValue = 1;
                for (int i = 0; i < _numOfPeriod; i++)
                {
                    int subtotal = 0;
                    for (int j = 0; j < values.GetLength(1); j++)
                        subtotal += (int)values[i, j];
                    if (subtotal > _maxValue)
                    {
                        _maxValue = subtotal;
                    }
                }

                _chart.Values = values;
                _chart.SectionNames = seriesNames;
                _chart.BarColors = _colors;
                _legends.Clear();
                for (int i = 0; i < seriesNames.Count(); i++)
                    _legends.AddItem(new LegendItem(seriesNames[i], _colors.GetColorAt((short)(i)), Font, new Size(20, 8)));
                if (_maxValue < 6)
                {
                    _maxValue = Convert.ToInt32(Math.Ceiling(_maxValue / (double)2));
                    _maxValue *= 2;
                }
                else
                {
                    _maxValue = Convert.ToInt32(Math.Ceiling(_maxValue / (double)5));
                    _maxValue *= 5;
                }

                if (_maxValue == 0)
                {
                    _maxValue = 1;
                }

                int scaleMax = _maxValue;
                _yAxis.Maximum = scaleMax;
                _chart.ScaleMax = scaleMax;
                LayoutCompleted = false;
            }
        }

        public void Open(List<string> seriesNames, decimal?[,] values, List<string> categories)
        {
            if (values.GetLength(1) > 64)
            {
                _tooManyVariables = true;
            }
            else if (categories.Count > 64)
            {
                _tooManyVariables = true;
            }
            else
            {
                _tooManyVariables = false;
            }

            _numOfPeriod = (short)(categories.Count);
            if (_numOfPeriod > 0 & _tooManyVariables == false)
            {
                _xAxis.Open(categories);
                _maxValue = 1;
                for (int i = 0; i < _numOfPeriod; i++)
                {
                    int subtotal = 0;
                    for (int j = 0; j < values.GetLength(1); j++)
                        subtotal += (int)values[i, j];
                    if (subtotal > _maxValue)
                    {
                        _maxValue = subtotal;
                    }
                }

                _chart.Values = values;
                _chart.SectionNames = seriesNames;
                _legends.Clear();
                for (int i = 0; i < seriesNames.Count(); i++)
                    _legends.AddItem(new LegendItem(seriesNames[i], _colors.GetColorAt((short)(i)), Font, new Size(20, 8)));

                if (_maxValue < 6)
                {
                    _maxValue = Convert.ToInt32(Math.Ceiling(_maxValue / (double)2));
                    _maxValue *= 2;
                }
                else
                {
                    _maxValue = Convert.ToInt32(Math.Ceiling(_maxValue / (double)5));
                    _maxValue *= 5;
                }

                if (_maxValue == 0)
                {
                    _maxValue = 1;
                }

                int scaleMax = _maxValue;
                _yAxis.Maximum = scaleMax;
                _chart.ScaleMax = scaleMax;
                LayoutCompleted = false;
            }
        }

        /// <summary>
        /// Build chart into a image
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
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

        public int SelectedXIndex { get; set; }
        public int SelectedYIndex { get; set; }


        public override bool HitTest(Point location, ref int hitPeriodIndex, ref string hitInfo)
        {
            hitPeriodIndex = -1;
            hitInfo = string.Empty;
            SelectedXIndex = -1;
            SelectedYIndex = -1;
            if (_chart != null)
            {
                if (_chart.HitTest(location, ref hitPeriodIndex, ref hitInfo))
                {
                    GetHitPeriodDates(hitPeriodIndex);
                    SelectedXIndex = _chart.SelectedXIndex;
                    SelectedYIndex = _chart.SelectedYIndex;
                    return true;
                }

                if (_xAxis.HitTest(location, ref hitPeriodIndex, ref hitInfo))
                {
                    hitInfo = string.Format(Properties.Resources.B008,
                        _xAxis.GetPeriodName(hitPeriodIndex),
                        _xAxis.GetStartDate(hitPeriodIndex).ToShortDateString(),
                        _xAxis.GetEndDate(hitPeriodIndex).ToShortDateString());
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        private void GetHitPeriodDates(int index)
        {
            if (index >= 0 & index < _xAxis.Count)
            {
                HitPeriodIndex = index;
                HitPeriodEndDate = _xAxis.GetEndDate(index);
                HitPeriodStartDate = _xAxis.GetStartDate(index);
            }
        }

        public override void Draw(Graphics canvas)
        {
            base.Draw(canvas);
            if (_numOfPeriod > 0)
            {
                if (_tooManyVariables == false)
                {
                    PerformLayout(canvas);
                    _title.Draw(canvas);
                    _subtitle.Draw(canvas);
                    _legends.Draw(canvas);
                    _yAxis.Draw(canvas);
                    _xAxis.Draw(canvas);
                    DrawGrids(canvas);
                    _chart.Draw(canvas);

                    // draw zero line
                    canvas.DrawLine(new Pen(Color.DarkGray), _chartLeft, _yAxis.ZeroPosition, _chartLeft + _chartWidth + 3, _yAxis.ZeroPosition);
                }
                else
                {
                    _message = Properties.Resources.B009;   // "There are too many data series or data points to fit in this chart.";
                    DrawMessage(canvas, _message, _messageColor);
                }
            }
            else if (_message.Length > 0)
            {
                DrawMessage(canvas, _message, _messageColor);
            }

            if (BorderStyle != BorderStyle.None)
            {
                DrawBoard(canvas);
            }
        }

        /// <summary>
        /// Draw grid lines
        /// </summary>
        /// <param name="canvas"></param>
        private void DrawGrids(Graphics canvas)
        {
            int x1 = _chartLeft;
            int x2 = _chartLeft + _chartWidth;
            int y;
            using (var p = new Pen(Color.LightGray) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash })
            {
                for (int i = 0; i < _yAxis.Coords.Count(); i++)
                {
                    y = _yAxis.Coords[i];
                    canvas.DrawLine(p, x1, y, x2, y);
                }
            }
        }

        /// <summary>
        /// Layout elements of the chart
        /// </summary>
        /// <param name="canvas"></param>
        public void PerformLayout(Graphics canvas)
        {
            if (LayoutCompleted == false & base.Width > 10 & base.Height > 10 & _numOfPeriod > 0)
            {
                _chartHeight = PlotHeight;
                _chartWidth = PlotWidth;
                _chartTop = base.Top + Padding.Top;
                _chartLeft = base.Left + Padding.Left;
                {
                    var withBlock = _title;
                    if (withBlock.Visible)
                    {
                        withBlock.Left = _chartLeft;
                        withBlock.Top = _chartTop;
                        withBlock.Width = PlotWidth;
                        withBlock.AutoSizeMode = ChartLabel.AutoSizeModes.byHeight;
                        withBlock.PerformLayout(canvas);
                        _chartHeight -= withBlock.Height;
                        _chartTop += withBlock.Height;
                    }
                }

                {
                    var withBlock1 = _subtitle;
                    if (withBlock1.Visible)
                    {
                        withBlock1.Left = _chartLeft;
                        withBlock1.Top = _chartTop;
                        withBlock1.Width = PlotWidth;
                        withBlock1.AutoSizeMode = ChartLabel.AutoSizeModes.byHeight;
                        withBlock1.PerformLayout(canvas);
                        _chartHeight -= withBlock1.Height;
                        _chartTop += withBlock1.Height;
                    }
                }

                {
                    var withBlock2 = _legends;
                    withBlock2.Width = PlotWidth;
                    withBlock2.PerformLayout(canvas);
                    withBlock2.Top = base.Top + base.Height - withBlock2.Height - Padding.Bottom;
                    withBlock2.Left = (int)(base.Left + (Width - withBlock2.Width) / (double)2 + Padding.Left);
                    withBlock2.PerformLayout(canvas);
                    _chartHeight -= withBlock2.Height;
                }

                {
                    var withBlock3 = _xAxis;
                    withBlock3.Left = _chartLeft;
                    withBlock3.Width = _chartWidth - 100;
                    withBlock3.PerformAutoSize(canvas);
                    _chartHeight -= withBlock3.Height;
                    _chartWidth -= (int)(withBlock3.Height / 2);
                }

                {
                    var withBlock4 = _yAxis;
                    withBlock4.Left = _chartLeft;
                    withBlock4.Top = _chartTop;
                    withBlock4.Height = _chartHeight;
                    withBlock4.PerformLayout(canvas);
                    _chartLeft += withBlock4.Width;
                    _chartWidth -= withBlock4.Width;
                }

                {
                    var withBlock5 = _xAxis;
                    withBlock5.Top = base.Top + base.Height - Padding.Bottom - withBlock5.Height - _legends.Height;
                    withBlock5.Left = _chartLeft;
                    withBlock5.Width = _chartWidth;
                    withBlock5.PerformLayout(canvas);
                }

                {
                    var withBlock6 = _yAxis;
                    withBlock6.Height = _chartHeight;
                    withBlock6.PerformLayout(canvas);
                }

                int cellHeight = _chartHeight;
                //int y1 = _chartRect.Top + cellHeight;
                {
                    var withBlock7 = _chart;
                    withBlock7.Left = _chartLeft;
                    withBlock7.Top = _chartTop;
                    withBlock7.Width = _chartWidth;
                    withBlock7.Height = cellHeight;
                    withBlock7.Coords = _xAxis.Coords;
                    withBlock7.DrawingRatio = _yAxis.DrawingRatio;
                    withBlock7.PerformLayout(canvas);
                }

                _chartRect = new Rectangle(_chartLeft, _chartTop, _chartWidth, _chartHeight);
                LayoutCompleted = true;
            }
        }
    }
}