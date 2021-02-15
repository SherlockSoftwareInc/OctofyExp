
using System;
using System.Collections.Generic;
using System.Drawing;

namespace OctofyLib
{
    public class StackedCategoryBarItem : ChartElementBase
    {
        private class Cell
        {
            public int? Value { get; set; }
            public RectangleF Rect { get; set; }
            public string TooltopText { get; set; }
            public Color Backcolor { get; set; }

            public bool HitTest(Point location)
            {
                var result = default(bool);
                if (Value.HasValue)
                {
                    if (location.X >= Rect.X)
                    {
                        if (location.Y >= Rect.Y)
                        {
                            if (location.X <= Rect.X + Rect.Width)
                            {
                                if (location.Y <= Rect.Y + Rect.Height)
                                {
                                    result = true;
                                }
                            }
                        }
                    }
                }

                return result;
            }
        }

        private readonly List<Cell> _cells = new List<Cell>();
        private bool _layoutPerformed;
        private float _drawingRatio;
        private ColorSchema _colors = new ColorSchema();

        internal bool SingleCellMode { get; set; }

        public List<int?> Values
        {
            get
            {
                if (_cells.Count > 0)
                {
                    var result = new List<int?>();
                    for (int i = 0; i < _cells.Count; i++)
                        result.Add(_cells[i].Value);
                    return result;
                }
                else
                {
                    return null;
                }
            }

            set
            {
                if (_cells.Count > 0)
                {
                    _cells.Clear();
                }

                for (int i = 0; i < value.Count; i++)
                    _cells.Add(new Cell() { Value = value[i] });

                SingleCellMode = _cells.Count == 1;
                if (SingleCellMode)
                {
                    _colors = new ColorSchema("#00BFFF");   //#6495ED
                }
                else
                {
                    _colors = new ColorSchema();
                }

                _layoutPerformed = false;
            }
        }

        public int ContentWidth { get; set; }
        public string Percentage { get; set; } = "";
        public int ZeroPosition { get; set; }
        public string Name { get; set; }
        public List<string> Series { get; set; }
        public bool Highlight { get; set; }
        public int HighlightIndex { get; set; }
        public int MaxNameLength { get; set; } = 40;

        public string DisplayName
        {
            get
            {
                if (Name.Length == 0)
                {
                    return Properties.Resources.B003;
                }
                else if (Name.Length > MaxNameLength && MaxNameLength > 10)
                {
                    return Name.Substring(0, MaxNameLength - 4) + "...";
                }
                else
                {
                    return Name;
                }
            }
        }

        public float DrawingRatio
        {
            get
            {
                return _drawingRatio;
            }

            set
            {
                _drawingRatio = value;
                _layoutPerformed = false;
            }
        }

        public int YOffset { get; set; }
        public bool Selected { get; set; }

        public ColorSchema BarColors
        {
            set
            {
                _colors = value;
                for (int i = 0; i < _cells.Count; i++)
                    _cells[i].Backcolor = value.GetColorAt((short)(i));
            }
        }

        public override void Draw(Graphics canvas)
        {
            if (_layoutPerformed == false)
            {
                if (_cells.Count > 0)
                {
                    float x = ZeroPosition;
                    for (int i = 0; i < _cells.Count; i++)
                    {
                        if (_cells[i].Value.HasValue)
                        {
                            float dx = (float)(_cells[i].Value * DrawingRatio);
                            _cells[i].Rect = new RectangleF(x, Top, dx, Height);
                            _cells[i].Backcolor = _colors.GetColorAt((short)(i));
                            x += dx;
                        }
                    }
                }

                _layoutPerformed = true;
            }

            // draw selected mark and background if selected
            if (Selected)
            {
                double y = base.Top - YOffset + base.Height / (double)2;
                var p0 = new PointF(2, (float)(y - 5));
                var p1 = new PointF(2, (float)(y + 5));
                var p2 = new PointF(7, (float)(y));
                canvas.FillPolygon(Brushes.Black, new PointF[] { p0, p1, p2 });
                canvas.FillRectangle(Brushes.CornflowerBlue, new RectangleF(9, base.Top - YOffset, ZeroPosition - 12, base.Height));
            }

            // draw category name
            var textRect = new Rectangle(Left, Top - YOffset, ZeroPosition - 3, Height);
            var oTextformat = new StringFormat()
            {
                Alignment = StringAlignment.Far,
                LineAlignment = StringAlignment.Center
            };
            oTextformat.FormatFlags = StringFormatFlags.NoWrap;
            string text = DisplayName.Replace("\r\n", " ");
            text = text.Replace("\r", " ");
            text = text.Replace("\n", " ");
            if (Selected)
            {
                canvas.DrawString(text, base.Font, new SolidBrush(Color.White), textRect, oTextformat);
            }
            else
            {
                canvas.DrawString(text, base.Font, new SolidBrush(ForeColor), textRect, oTextformat);
            }

            // draw value
            if (SingleCellMode)
            {
                var cell = _cells[0];
                int v = 0;
                if (cell.Value.HasValue)
                {
                    v = (int)cell.Value;
                }

                if (v != 0)
                {
                    var cellRect = new RectangleF(cell.Rect.X, cell.Rect.Y - YOffset, cell.Rect.Width, cell.Rect.Height);
                    var fillColor = cell.Backcolor;
                    if (Highlight)
                    {
                        fillColor = ColorUtil.CreateColorWithCorrectedLightness(cell.Backcolor, ColorUtil.BrightnessEnhancementFactor1);
                    }

                    using (Brush brush = new SolidBrush(fillColor))
                    {
                        canvas.FillRectangle(brush, cellRect);
                    }

                    oTextformat.Alignment = StringAlignment.Near;
                    string valueText;
                    if (v == 0)
                    {
                        valueText = "0";
                    }
                    else if (Percentage.Length > 0)
                    {
                        valueText = string.Format("{0} ({1}%)", v.ToString("N0"), Percentage);
                    }
                    else
                    {
                        valueText = string.Format("{0}", v.ToString("N0"));
                    }

                    textRect = new Rectangle(ZeroPosition + 2, base.Top - YOffset, base.Width - ZeroPosition + base.Left - 2, base.Height);
                    int textWidth = (int)(textRect.Left + canvas.MeasureString(valueText, base.Font).Width);
                    ContentWidth = (int)(_cells[0].Rect.Left + _cells[0].Rect.Width);
                    if (textWidth > ContentWidth)
                    {
                        ContentWidth = textWidth;
                    }

                    canvas.DrawString(valueText, base.Font, TextBrush(base.BackColor), textRect, oTextformat);
                }
            }
            else if (_cells.Count > 0)
            {
                for (int i = 0; i < _cells.Count; i++)
                {
                    var cell = _cells[i];
                    if (cell.Value.HasValue)
                    {
                        var cellRect = new RectangleF(cell.Rect.X, cell.Rect.Y - YOffset, cell.Rect.Width, cell.Rect.Height);
                        var fillColor = cell.Backcolor;
                        if (Highlight & HighlightIndex == i)
                        {
                            fillColor = ColorUtil.CreateColorWithCorrectedLightness(cell.Backcolor, ColorUtil.BrightnessEnhancementFactor1);
                        }

                        using (Brush brush = new SolidBrush(fillColor))
                        {
                            canvas.FillRectangle(brush, cellRect);
                        }

                        oTextformat.Alignment = StringAlignment.Near;
                        string valueText = cell.Value.ToString();
                        var textSize = canvas.MeasureString(valueText, base.Font);
                        if (textSize.Width + 5 < cell.Rect.Width)
                        {
                            textRect = new Rectangle((int)(cell.Rect.X + 2), (int)(cell.Rect.Y - YOffset), (int)(cell.Rect.Width - 4), Convert.ToInt32(cell.Rect.Height));
                            // textRect = New Rectangle(ZeroPosition + 2, Top - YOffset, Width - ZeroPosition + Left - 2, Height)
                            canvas.DrawString(valueText, base.Font, TextBrush(cell.Backcolor), textRect, oTextformat);
                        }
                    }
                }
            }
        }

        private SolidBrush TextBrush(Color backColor)
        {
            double luminance = 0.2126F * backColor.R + 0.7152F * backColor.G + 0.0722F * backColor.B;
            if (luminance < 140)
            {
                return new SolidBrush(Color.White);
            }
            else
            {
                return new SolidBrush(Color.Black);
            }
        }

        public override bool HitTest(Point location, ref int hitPeriodIndex, ref string hitInfo)
        {
            if (_cells != null)
            {
                if (location.Y >= base.Top)
                {
                    if (location.Y <= base.Top + base.Height)
                    {
                        for (int i = 0; i < _cells.Count; i++)
                        {
                            if (_cells[i].HitTest(location))
                            {
                                hitPeriodIndex = i;
                                string catName = Series[i];
                                if (catName.Length == 0)
                                {
                                    catName = Properties.Resources.B003;    // "(Blanks)";
                                }

                                hitInfo = catName + ":  " + _cells[i].Value.ToString();
                                return true;
                            }
                        }

                        if (location.X >= base.Left)
                        {
                            if (location.X <= base.Left + ZeroPosition)
                            {
                                long total = 0;
                                foreach (var item in _cells)
                                    total += (long)item.Value;
                                hitInfo = Name;
                                if (hitInfo.Length == 0)
                                {
                                    hitInfo = Properties.Resources.B003;    // "(Blanks)";
                                }

                                hitInfo = string.Format(Properties.Resources.B010, hitInfo, total.ToString("N0"));
                                //B010: Grand total of {0}:  {1}
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Hit cell test of a stacked bar
        /// </summary>
        /// <param name="location"></param>
        /// <param name="hitPeriodIndex"></param>
        /// <param name="cellValue"></param>
        /// <returns></returns>
        public bool CellHitTest(Point location, ref int hitPeriodIndex, ref decimal cellValue)
        {
            if (_cells != null)
            {
                for (int i = 0; i < _cells.Count; i++)
                {
                    if (location.X >= _cells[i].Rect.X & location.X <= _cells[i].Rect.X + _cells[i].Rect.Width)
                    {
                        hitPeriodIndex = i;
                        if (_cells[i].Value.HasValue)
                            cellValue = (decimal)_cells[i].Value;
                        return true;
                    }
                }
            }

            return false;
        }
    }
}