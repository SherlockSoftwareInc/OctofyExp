using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace OctofyLib
{
    /// <summary>
    /// Base class of chart
    /// </summary>
    public class ChartElementBase : IDisposable
    {
        /// <summary>
        /// Enumeration to sepcify the brush types
        /// </summary>
        public enum BrushTypes
        {
            /// <summary>
            /// Solid brush
            /// </summary>
            Solid,
            /// <summary>
            /// Hatch brush
            /// </summary>
            Hatch
        }

        /// <summary>
        /// Enumeration to sepcify the location for legends on the chart
        /// </summary>
        public enum LegendPositions
        {
            /// <summary>
            /// at bottom
            /// </summary>
            Bottom,
            /// <summary>
            /// at top
            /// </summary>
            Top,
            /// <summary>
            /// at top of left side
            /// </summary>
            LeftTop,
            /// <summary>
            /// at middle of left side
            /// </summary>
            LeftMiddle,
            /// <summary>
            /// at bottom of left side
            /// </summary>
            LeftBottom,
            /// <summary>
            /// at top of right side
            /// </summary>
            RightTop,
            /// <summary>
            /// at middle of right side
            /// </summary>
            RightMiddle,
            /// <summary>
            /// at bottom of right side
            /// </summary>
            RightBottom
        }

        /// <summary>
        /// How to display the value caption on the chart
        /// </summary>
        public enum CaptionTypes
        {
            /// <summary>
            /// name only
            /// </summary>
            Name,
            /// <summary>
            /// name follow by percentage
            /// </summary>
            NamePercent,
            /// <summary>
            /// name follow by volumn
            /// </summary>
            NameVolume,
            /// <summary>
            /// name, volume and percentage
            /// </summary>
            NameVolumePercent,
            /// <summary>
            /// Diaplay none
            /// </summary>
            None,
            /// <summary>
            /// percentage only
            /// </summary>
            Percent,
            /// <summary>
            /// volumn only
            /// </summary>
            Volume,
            /// <summary>
            /// volumn and percentage
            /// </summary>
            VolumePercent
        }

        private int _x;
        private int _y;
        private int _width;
        private int _height;
        private Color _backColor = Color.Transparent;
        private Color _foreColor = Color.Black;
        private Font _font = System.Windows.Forms.Control.DefaultFont;
        private Padding _padding;

        /// <summary>
        /// Gets or set background color
        /// </summary>
        public virtual Color BackColor
        {
            get
            {
                return _backColor;
            }
            set
            {
                _backColor = value;
            }
        }

        /// <summary>
        /// Gets the y-coordinate of the bounding rectangle bottom edge.
        /// </summary>
        public int Bottom
        {
            get
            {
                return _y + _height;
            }
        }

        /// <summary>
        /// Gets or sets client area rectangle
        /// </summary>
        public virtual Rectangle ClientAreaRectangle
        {
            get
            {
                return new Rectangle(_x, _y, _width, _height);
            }
            set
            {
                _x = value.X;
                _y = value.Y;
                _width = value.Width;
                _height = value.Height;
            }
        }

        /// <summary>
        /// Gets or sets the font of the text displayed by the control.
        /// </summary>
        public virtual Font Font
        {
            get
            {
                return _font;
            }
            set
            {
                _font = value;
                LayoutCompleted = false;
            }
        }

        /// <summary>
        /// Gets or sets the foreground color of the control used to draw text.
        /// </summary>
        public virtual Color ForeColor
        {
            get
            {
                return _foreColor;
            }
            set
            {
                _foreColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the height of the bounding rectangle.
        /// </summary>
        public virtual int Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
                LayoutCompleted = false;
            }
        }

        /// <summary>
        /// Gets or sets a value indicates wether 
        /// </summary>
        internal bool LayoutCompleted { get; set; }

        /// <summary>
        /// Gets the x-coordinate of the bounding rectangle left edge.
        /// </summary>
        public virtual int Left
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
                LayoutCompleted = false;
            }
        }

        /// <summary>
        /// Gets or sets a point where top left corner of the element located
        /// </summary>
        public virtual Point Location
        {
            get
            {
                return new Point(_x, _y);
            }
            set
            {
                _x = value.X;
                _y = value.Y;
                LayoutCompleted = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Padding Padding
        {
            get
            {
                return _padding;
            }

            set
            {
                _padding = value;
                LayoutCompleted = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int PlotHeight
        {
            get
            {
                return _height - _padding.Top - _padding.Bottom;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int PlotLeft
        {
            get
            {
                return _x + _padding.Left;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int PlotRight
        {
            get
            {
                return _x + _width - _padding.Right;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int PlotWidth
        {
            get
            {
                return _width - _padding.Left - _padding.Right;
            }
        }

        /// <summary>
        /// Gets the x-coordinate of the bounding rectangle right edge.
        /// </summary>
        public int Right
        {
            get
            {
                return _x + _width;
            }
        }

        /// <summary>
        /// Gets or set size of the element
        /// </summary>
        public virtual Size Size
        {
            get
            {
                return new Size(_width, _height);
            }

            set
            {
                _width = value.Width;
                _height = value.Height;
                LayoutCompleted = false;
            }
        }

        /// <summary>
        /// Gets the y-coordinate of the bounding rectangle top edge.
        /// </summary>
        public virtual int Top
        {
            get
            {
                return _y;
            }

            set
            {
                _y = value;
                LayoutCompleted = false;
            }
        }

        /// <summary>
        /// Gets or sets the width of the bounding rectangle.
        /// </summary>
        public virtual int Width
        {
            get
            {
                return _width;
            }

            set
            {
                _width = value;
                LayoutCompleted = false;
            }
        }

        /// <summary>
        /// Gets or sets the x-coordinate of the upper-left corner of the
        /// bounding rectangle.
        /// </summary>
        public virtual int X
        {
            get
            {
                return _x;
            }

            set
            {
                _x = value;
                LayoutCompleted = false;
            }
        }

        /// <summary>
        /// Gets or sets the y-coordinate of the upper-left corner of the
        /// bounding rectangle.
        /// </summary>
        public virtual int Y
        {
            get
            {
                return _y;
            }

            set
            {
                _y = value;
                LayoutCompleted = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <returns></returns>
        public static SizeF GetTextSize(Graphics canvas, string text, Font font)
        {
            var flags = TextFormatFlags.NoPadding;
            var proposedSize = new Size(int.MaxValue, int.MaxValue);
            return TextRenderer.MeasureText(canvas, text, font, proposedSize, flags);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="canvas"></param>
        public virtual void Draw(Graphics canvas)
        {
            canvas.SmoothingMode = SmoothingMode.AntiAlias;
            if (BackColor != Color.Transparent)
            {
                canvas.FillRectangle(new SolidBrush(BackColor), ClientAreaRectangle);
            }
            // canvas.DrawRectangle(Pens.Black, Me.ClientAreaRectangle)
            var path = new GraphicsPath();
            path.AddRectangle(ClientAreaRectangle);
            canvas.SetClip(path);
        }

        /// <summary>
        /// Draw the border of the element
        /// </summary>
        /// <param name="canvas"></param>
        public void DrawBoard(Graphics canvas)
        {
            canvas.DrawRectangle(Pens.Black, Left, Top, Width - 1, Height - 1);
        }

        /// <summary>
        /// Draw message at the center of the element
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="msg"></param>
        public void DrawMessage(Graphics canvas, string msg, Color messageForeColor)
        {
            if (msg.Length > 0)
            {
                var oTextformat = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                canvas.DrawString(msg, Font, new SolidBrush(messageForeColor), ClientAreaRectangle, oTextformat);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="canvas"></param>
        public virtual void PerformAutoSize(Graphics canvas)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="location"></param>
        /// <param name="hitPeriodIndex"></param>
        /// <param name="hitInfo"></param>
        /// <returns></returns>
        public virtual bool HitTest(Point location, ref int hitPeriodIndex, ref string hitInfo)
        {
            hitPeriodIndex = -1;
            hitInfo = string.Empty;
            if (location.X >= Left & location.X <= Left + Width)
            {
                if (location.Y >= Top & location.Y <= Top + Height)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        public string WatermarkText { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public Font WatermarkFont { get; set; } = new Font("Times New Roman", 36);

        /// <summary>
        /// 
        /// </summary>
        public Color WatermarkColor { get; set; } = ColorTranslator.FromHtml("#8000CD32"); //Color.FromArgb(0x8000CD32);

        /// <summary>
        /// Draw water mark on the center of the element
        /// </summary>
        /// <param name="canvas"></param>
        public void DrawWatermark(Graphics canvas)
        {
            if (WatermarkText.Length > 0)
            {
                var textSize = canvas.MeasureString(WatermarkText, WatermarkFont);
                var textRect = new RectangleF((float)((Width - textSize.Width) / 2.6), (float)((Height - textSize.Height) / 2.7), textSize.Width + 4, textSize.Height + 4);
                using (var gp = new GraphicsPath())
                {
                    using (var outline = new Pen(Color.White, 2) { LineJoin = LineJoin.Round })
                    {
                        using (var sf = new StringFormat())
                        {
                            using (Brush foreBrush = new SolidBrush(WatermarkColor))
                            {
                                gp.AddString(WatermarkText, WatermarkFont.FontFamily, (int)WatermarkFont.Style, WatermarkFont.Size, textRect, sf);
                                canvas.ScaleTransform(1.3F, 1.35F);

                                // canvas.SmoothingMode = SmoothingMode.HighQuality
                                canvas.DrawPath(outline, gp);
                                canvas.FillPath(foreBrush, gp);
                                canvas.ResetTransform();
                            }
                        }
                    }
                }
            }
        }


        private bool disposedValue; // To detect redundant calls

        /// <summary>
        /// 
        /// </summary>
        public ChartElementBase()
        {
            _padding = new Padding();
        }

        /// <summary>
        /// IDisposable 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                // TODO: set large fields to null.
            }

            disposedValue = true;
        }

        // TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
        // Protected Overrides Sub Finalize()
        // ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        // Dispose(False)
        // MyBase.Finalize()
        // End Sub

        /// <summary>
        /// This code added by Visual Basic to correctly implement the disposable pattern. 
        /// </summary>
        public void Dispose()
        {
            // Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}