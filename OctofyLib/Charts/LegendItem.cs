using System;
using System.Drawing;


namespace OctofyLib
{

    /// <summary>
    /// Legend item
    /// </summary>
    internal class LegendItem : ChartElementBase
    {
        public enum LegendStyles
        {
            Line,
            DashLine,
            DotLine,
            Rectangle,
            Circle,
            BoxPlotTop,
            BoxPlotQ1,
            BoxPlotMedian,
            BoxPlotMiddle,
            BoxPlotQ2,
            BoxPlotBottom,
            BoxPlotQ3
        }

        public enum DashStyles
        {
            Solid,
            Dash,
            Dot
        }

        public enum MarkerStyles
        {
            None,
            Circle,
            Square,
            Diamond,
            Triangle,
            Cross,
            Plus
        }

        private int _fontHeight;
        private int _iconWidth;
        private Rectangle _iconRect;

        //public LegendItem(Font font)
        //{
        //    Font = font;
        //}

        public LegendItem(string text, Color color, Font font, Size markSize)
        {
            Text = text;
            LegendColor = color;
            Font = font;
            LegendMarkSize = markSize;
        }

        //public LegendItem(string text, Color color, Font font, Size markSize, LegendStyles style)
        //{
        //    Text = text;
        //    LegendColor = color;
        //    Font = font;
        //    LegendMarkSize = markSize;
        //    LegendStyle = style;
        //}

        //public LegendItem(string text, Color color, Font font, LegendStyles style)
        //{
        //    Text = text;
        //    LegendColor = color;
        //    Font = font;
        //    LegendStyle = style;
        //}


        public BrushTypes BrushType { get; set; }
        //public DashStyles DashStyle { get; set; } = DashStyles.Solid;
        public System.Drawing.Drawing2D.HatchStyle HatchStyle { get; set; }
        public int Indent { get; set; } = 24;
        public Color LegendColor { get; set; }
        public Color LegendColor2 { get; set; }
        public Color LegendColor3 { get; set; }
        public Size LegendMarkSize { get; set; } = new Size(24, 4);
        public LegendStyles LegendStyle { get; set; } = LegendStyles.Rectangle;
        public short LineWidth { get; set; } = 3;
        public MarkerStyles MarkerStyle { get; set; } = MarkerStyles.None;
        public Point Offset { get; set; } = new Point(0, 0);
        public string Text { get; set; } = "";
        public int MaxTextLength { get; set; } = 32;


        public bool IsHit(Point location)
        {
            var result = default(bool);
            if (location.X > Left)
            {
                if (location.X < Left + Width)
                {
                    if (location.Y > Top)
                    {
                        if (location.Y < Top + Height)
                        {
                            result = true;
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Draw the legend item
        /// </summary>
        /// <param name="canvas"></param>
        public void DrawLegend(Graphics canvas)
        {
            // PerformAutoResize(canvas)
            CalIconRect(canvas);
            _iconRect.Location = new Point(base.Left + Offset.X, base.Top + Offset.Y + 1);

            // draw legend mark
            int dy = (int)((Size.Height - LegendMarkSize.Height) / (double)2);
            var switchExpr = LegendStyle;
            switch (switchExpr)
            {
                case LegendStyles.Rectangle:
                    {
                        var markRect = new Rectangle(Location.X + Offset.X, Location.Y + Offset.Y + dy, LegendMarkSize.Width, LegendMarkSize.Height);
                        var switchExpr1 = BrushType;
                        switch (switchExpr1)
                        {
                            case BrushTypes.Solid:
                                {
                                    var oBrush = new SolidBrush(LegendColor);
                                    canvas.FillRectangle(oBrush, markRect);
                                    oBrush.Dispose();
                                    break;
                                }

                            case BrushTypes.Hatch:
                                {
                                    var oBrush = new System.Drawing.Drawing2D.HatchBrush(HatchStyle, LegendColor, BackColor);
                                    canvas.FillRectangle(oBrush, markRect);
                                    oBrush.Dispose();
                                    break;
                                }
                        }

                        break;
                    }

                case LegendStyles.Line:
                    {
                        _iconRect.Width = LegendMarkSize.Width;
                        var oPen = new Pen(LegendColor, LegendMarkSize.Height);
                        int y = (int)(Location.Y + Offset.Y + (Size.Height - LegendMarkSize.Height) / (double)2 + 1);
                        canvas.DrawLine(oPen, Location.X + Offset.X, y, Location.X + Offset.X + LegendMarkSize.Width, y);
                        oPen.Dispose();
                        break;
                    }

                case LegendStyles.DashLine:
                    {
                        _iconRect.Width = LegendMarkSize.Width;
                        var oPen = new Pen(LegendColor, LegendMarkSize.Height) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash };
                        int y = (int)(Location.Y + Offset.Y + (Size.Height - LegendMarkSize.Height) / (double)2 + 1);
                        canvas.DrawLine(oPen, Location.X + Offset.X, y, Location.X + Offset.X + LegendMarkSize.Width, y);
                        oPen.Dispose();
                        break;
                    }

                case LegendStyles.DotLine:
                    {
                        _iconRect.Width = LegendMarkSize.Width;
                        var oPen = new Pen(LegendColor, LegendMarkSize.Height) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dot };
                        int y = (int)(Location.Y + Offset.Y + (Size.Height - LegendMarkSize.Height) / (double)2 + 1);
                        canvas.DrawLine(oPen, Location.X + Offset.X, y, Location.X + Offset.X + LegendMarkSize.Width, y);
                        oPen.Dispose();
                        break;
                    }

                case LegendStyles.BoxPlotBottom:
                    {
                        var markRect = GetIconRectangle();
                        dy = (int)(markRect.Height / (double)2);
                        using (var oPen = new Pen(LegendColor, 1))
                        {
                            canvas.DrawLine(oPen, markRect.X, markRect.Top + dy, markRect.X + markRect.Width, markRect.Top + dy);
                            int x1 = (int)(markRect.Left + markRect.Width / (double)2);
                            canvas.DrawLine(oPen, x1, markRect.Top, x1, markRect.Top + dy);
                        }

                        break;
                    }

                case LegendStyles.BoxPlotMedian:
                    {
                        var markRect = GetIconRectangle();
                        dy = (int)(markRect.Height / (double)2);
                        using (var oPen = new Pen(LegendColor, 1))
                        {
                            // canvas.DrawLine(oPen, x1, markRect.Top, x1, markRect.Top + dy)
                            canvas.FillRectangle(new SolidBrush(LegendColor), new Rectangle(markRect.X, markRect.Top, markRect.Width, dy));
                            canvas.FillRectangle(new SolidBrush(LegendColor2), new Rectangle(markRect.X, markRect.Top + dy, markRect.Width, dy));
                        }

                        using (var oPen = new Pen(LegendColor3))
                        {
                            canvas.DrawLine(oPen, markRect.X, markRect.Top + dy, markRect.X + markRect.Width, markRect.Top + dy);
                        }

                        break;
                    }

                case LegendStyles.BoxPlotQ1:
                    {
                        var markRect = GetIconRectangle();
                        dy = (int)(markRect.Height / (double)2);
                        using (var oPen = new Pen(LegendColor, 1))
                        {
                            int x1 = (int)(markRect.Left + markRect.Width / (double)2);
                            canvas.DrawLine(oPen, x1, markRect.Top, x1, markRect.Top + dy);
                            canvas.FillRectangle(new SolidBrush(LegendColor), new Rectangle(markRect.X, markRect.Top + dy, markRect.Width, dy));
                        }

                        break;
                    }

                case LegendStyles.BoxPlotQ2:
                    {
                        var markRect = GetIconRectangle();
                        dy = (int)(markRect.Height / (double)2);
                        using (var oPen = new Pen(LegendColor, 1))
                        {
                            int x1 = (int)(markRect.Left + markRect.Width / (double)2);
                            canvas.DrawLine(oPen, x1, markRect.Top + dy, x1, markRect.Top + dy + dy);
                            canvas.FillRectangle(new SolidBrush(LegendColor), new Rectangle(markRect.X, markRect.Top, markRect.Width, dy));
                        }

                        break;
                    }

                case LegendStyles.BoxPlotQ3:
                    {
                        var markRect = GetIconRectangle();
                        using (var oPen = new Pen(LegendColor, 1))
                        {
                            canvas.FillRectangle(new SolidBrush(LegendColor), new Rectangle(markRect.X, markRect.Top, markRect.Width, (int)(markRect.Height / (double)2)));
                        }

                        break;
                    }

                case LegendStyles.BoxPlotTop:
                    {
                        var markRect = GetIconRectangle();
                        dy = (int)(markRect.Height / (double)2);
                        using (var oPen = new Pen(LegendColor, 1))
                        {
                            canvas.DrawLine(oPen, markRect.X, markRect.Top + dy, markRect.X + markRect.Width, markRect.Top + dy);
                            int dx = (int)(markRect.Width / (double)2);
                            int x1 = markRect.Left + dx;
                            canvas.DrawLine(oPen, x1, markRect.Top + dy, x1, markRect.Top + markRect.Height);
                        }

                        break;
                    }

                case LegendStyles.BoxPlotMiddle:
                    {
                        var markRect = GetIconRectangle();
                        dy = (int)(markRect.Height / (double)2);
                        using (var oPen = new Pen(LegendColor, 1))
                        {
                            canvas.DrawLine(oPen, markRect.X, markRect.Top + dy, markRect.X + markRect.Width, markRect.Top + dy);
                            int dx = (int)(markRect.Width / (double)2);
                            int x1 = markRect.Left + dx;
                            canvas.DrawLine(new Pen(Color.DarkGray), x1, markRect.Top, x1, markRect.Top + markRect.Height);
                        }

                        break;
                    }
            }

            if (MarkerStyle != MarkerStyles.None)
            {
                DrawMarker(canvas, MarkerStyle, (short)(LegendMarkSize.Height + 1), LegendColor);
            }

            // draw legend text
            var oTextformat = new StringFormat
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Center
            };
            var rect = new Rectangle(Location.X + Offset.X + Indent, Location.Y + Offset.Y, Size.Width - Indent, Size.Height);
            canvas.DrawString(DisplayText(), base.Font, new SolidBrush(Color.Black), rect, oTextformat);
        }

        /// <summary>
        /// Draw legned mark
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="style"></param>
        /// <param name="dx"></param>
        /// <param name="color"></param>
        private void DrawMarker(Graphics canvas, MarkerStyles style, short dx, Color color)
        {
            // _iconRect = GetIconRectangle()

            using (var p = new Pen(color))
            {
                short dx2 = (short)(dx + dx);
                short penWidth = GetLineWidth();
                int x1 = (int)(_iconRect.X + _iconRect.Width / (double)2);
                int y1 = (int)(_iconRect.Top + _iconRect.Height / (double)2);
                switch (style)
                {
                    case MarkerStyles.Circle:
                        {
                            var rect = new Rectangle(x1 - dx, y1 - dx, dx2, dx2);
                            canvas.FillEllipse(new SolidBrush(color), rect);
                            break;
                        }
                    // canvas.DrawEllipse(p, rect)

                    case MarkerStyles.Cross:
                        {
                            int x = x1 - dx;
                            int y = y1 - dx;
                            using (var p1 = new Pen(color, 2))
                            {
                                canvas.DrawLine(p1, x, y, x + dx2, y + dx2);
                                canvas.DrawLine(p1, x, y + dx2, x + dx2, y);
                            }

                            break;
                        }

                    case MarkerStyles.Diamond:
                        {
                            dx += 1;
                            dx2 += 2;
                            int x = x1;
                            int y = y1 - dx;
                            var pts = new Point[5];
                            pts[0] = new Point(x, y);
                            pts[1] = new Point(x - dx, y + dx);
                            pts[2] = new Point(x, y + dx2);
                            pts[3] = new Point(x + dx, y + dx);
                            pts[4] = new Point(x, y);
                            canvas.FillPolygon(new SolidBrush(Color.White), pts);
                            canvas.DrawPolygon(p, pts);
                            break;
                        }

                    case MarkerStyles.Plus:
                        {
                            int x = x1 - dx;
                            int y = (int)(y1 - Math.Floor(LineWidth / (double)2) + 1);
                            using (var p1 = new Pen(color, 2))
                            {
                                canvas.DrawLine(p1, x, y, x + dx2, y);
                                x += dx;
                                canvas.DrawLine(p1, x, y - dx, x, y + dx);
                            }

                            break;
                        }

                    case MarkerStyles.Square:
                        {
                            var rect = new Rectangle(x1 - dx, y1 - dx, dx2, dx2);
                            canvas.FillRectangle(new SolidBrush(Color.White), rect);
                            canvas.DrawRectangle(p, rect);
                            break;
                        }

                    case MarkerStyles.Triangle:
                        {
                            int x = x1;
                            int y = y1 - dx;
                            var pts = new Point[4];
                            pts[0] = new Point(x, y);
                            pts[1] = new Point(x - dx, y + dx2);
                            pts[2] = new Point(x + dx, y + dx2);
                            pts[3] = new Point(x, y);
                            canvas.FillPolygon(new SolidBrush(Color.White), pts);
                            canvas.DrawPolygon(p, pts);
                            break;
                        }
                }
            }
        }

        /// <summary>
        /// Calculate the location and size for the icon
        /// </summary>
        /// <returns></returns>
        private Rectangle GetIconRectangle()
        {
            short dy = (short)(Math.Floor(Height * 0.2));
            return new Rectangle(Left + Offset.X, Top + Offset.Y + dy, _iconRect.Width, Height - dy - dy);
        }

        /// <summary>
        /// Calculate required size to fit 
        /// </summary>
        /// <param name="canvas"></param>
        /// <returns></returns>
        public Size RequiredSize(Graphics canvas)
        {
            var textSize = canvas.MeasureString(DisplayText(), Font);
            return new Size((int)(Math.Ceiling(textSize.Width) + Indent + 3),
                Convert.ToInt32(Math.Max(Math.Ceiling(textSize.Height), LegendMarkSize.Height + 2)));
        }

        /// <summary>
        /// Build display text from original text in case 
        /// the length of original text is too long
        /// </summary>
        /// <returns></returns>
        private string DisplayText()
        {
            if (Text.Length <= MaxTextLength)
            {
                return Text;
            }
            else
            {
                int index = MaxTextLength - 8;
                if (index <= 0)
                {
                    return Text;
                }
                else
                {
                    return Text.Substring(0, index) + "..." + Text.Substring(Text.Length - 5, 5);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private short GetLineWidth()
        {
            int penWidth = LineWidth;
            if (penWidth > _iconRect.Height)
            {
                penWidth = _iconRect.Height;
            }

            if (penWidth <= 0)
            {
                penWidth = 1;
            }

            return (short)(penWidth);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="canvas"></param>
        private void CalIconRect(Graphics canvas)
        {
            var textSize = GetTextSize(canvas, DisplayText(), Font);
            _fontHeight = Convert.ToInt32(Math.Ceiling(textSize.Height));
            _iconWidth = _fontHeight + 4;
            if (_iconWidth < 24)
            {
                _iconWidth = 24;
            }

            _iconRect.Width = _iconWidth - 4;
            _iconRect.Height = _fontHeight - 2;
        }

    }
}