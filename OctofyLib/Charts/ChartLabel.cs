using System;
using System.Drawing;
using System.Windows.Forms;

namespace OctofyLib
{
    /// <summary>
    /// Chart label class
    /// </summary>
    public class ChartLabel : ChartElementBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="font"></param>
        public ChartLabel(Font font)
        {
            BackColor = Color.Transparent;
            Font = font;
            ForeColor = Color.Black;
            Padding = new Padding(2, 2, 2, 2);
            _text = "";
            TextAlign = ContentAlignment.MiddleCenter;
            Visible = true;
            LayoutCompleted = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="font"></param>
        /// <param name="text"></param>
        public ChartLabel(Font font, string text)
        {
            BackColor = Color.Transparent;
            Font = font;
            ForeColor = Color.Black;
            Padding = new Padding(2, 2, 2, 2);
            _text = text;
            TextAlign = ContentAlignment.MiddleCenter;
            Visible = true;
            LayoutCompleted = false;
        }

        /// <summary>
        /// Lable directions
        /// </summary>
        public enum LabelDirections
        {
            /// <summary>
            /// Horizontal text
            /// </summary>
            Horizontal,
            /// <summary>
            /// Vertica text
            /// </summary>
            Vertical
        }

        /// <summary>
        /// Auto size modes
        /// </summary>
        public enum AutoSizeModes
        {
            /// <summary>
            /// No auto resize
            /// </summary>
            None,
            /// <summary>
            /// Auto resize element height
            /// </summary>
            byHeight,
            /// <summary>
            /// Auto resize element width
            /// </summary>
            byWidth,
            /// <summary>
            /// Resize both height and width
            /// </summary>
            byBoth
        }

        private AutoSizeModes _autoSizeMode = AutoSizeModes.byWidth;
        private string _text;
        private Rectangle _textRect;
        private LabelDirections _labelDirection;
        private short _rotateAngle;
        private float _angle;

        /// <summary>
        /// 
        /// </summary>
        public AutoSizeModes AutoSizeMode
        {
            get
            {
                return _autoSizeMode;
            }
            set
            {
                _autoSizeMode = value;
                LayoutCompleted = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool NoWrap { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Text
        {
            get
            {
                return _text;
            }

            set
            {
                _text = value;
                LayoutCompleted = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ContentAlignment TextAlign { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool MultiLine { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Visible { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public short RotateAngle
        {
            get
            {
                return _rotateAngle;
            }

            set
            {
                _rotateAngle = value;
                _angle = (float)(Math.PI * value / 180.0);
                if (value != 0)
                {
                    _labelDirection = LabelDirections.Vertical;
                }
                else
                {
                    _labelDirection = LabelDirections.Horizontal;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="canvas"></param>
        public override void Draw(Graphics canvas)
        {
            if (LayoutCompleted == false)
            {
                PerformLayout(canvas);
            }

            if (BackColor != Color.Transparent)
            {
                canvas.FillRectangle(new SolidBrush(BackColor), ClientAreaRectangle);
            }

            if (_text.Length > 0 & Width > 0 & Height > 0 & Visible)
            {
                var drawBrush = new SolidBrush(ForeColor);
                var drawFormat = new StringFormat();
                if (NoWrap)
                {
                    drawFormat.FormatFlags = StringFormatFlags.NoWrap;
                }

                if (_labelDirection == LabelDirections.Vertical)
                {
                    var txtSize = canvas.MeasureString(_text, Font);
                    var rotateSize = new SizeF(Convert.ToSingle(Math.Abs(txtSize.Width * Math.Cos(_angle) + txtSize.Height * Math.Sin(_angle))),
                        Convert.ToSingle(Math.Abs(txtSize.Height * Math.Cos(_angle) + txtSize.Width * Math.Sin(_angle))));
                    int x = _textRect.X;
                    int y = _textRect.Y;
                    var switchExpr = TextAlign;
                    switch (switchExpr)
                    {
                        case ContentAlignment.BottomCenter:
                            {
                                x += (int)((_textRect.Width - rotateSize.Width) / 2);
                                y += (int)(_textRect.Height - rotateSize.Height);
                                break;
                            }

                        case ContentAlignment.BottomLeft:
                            {
                                y += (int)(_textRect.Height - rotateSize.Height);
                                break;
                            }

                        case ContentAlignment.BottomRight:
                            {
                                x += (int)(_textRect.Width - rotateSize.Width);
                                y += (int)(_textRect.Height - rotateSize.Height);
                                break;
                            }

                        case ContentAlignment.MiddleCenter:
                            {
                                x += _textRect.Width;
                                break;
                            }

                        case ContentAlignment.MiddleLeft:
                            {
                                y += (int)(_textRect.Height - rotateSize.Height) / 2;
                                break;
                            }

                        case ContentAlignment.MiddleRight:
                            {
                                x += (int)(_textRect.Width - rotateSize.Width);
                                y += (int)((_textRect.Height - rotateSize.Height) / 2);
                                break;
                            }

                        case ContentAlignment.TopCenter:
                            {
                                x += (int)((_textRect.Width - rotateSize.Width) / 2);
                                break;
                            }

                        case ContentAlignment.TopLeft:
                            {
                                x = (int)(_textRect.X + _textRect.Width / (double)2);
                                break;
                            }

                        case ContentAlignment.TopRight:
                            {
                                x += (int)(_textRect.Width - rotateSize.Width);
                                break;
                            }
                    }

                    DrawRotatedTextAt(canvas, RotateAngle, _text, x, y, Font, drawBrush);
                }
                else
                {
                    var switchExpr1 = TextAlign;
                    switch (switchExpr1)
                    {
                        case ContentAlignment.BottomCenter:
                            {
                                drawFormat.Alignment = StringAlignment.Center;
                                drawFormat.LineAlignment = StringAlignment.Far;
                                break;
                            }

                        case ContentAlignment.BottomLeft:
                            {
                                drawFormat.Alignment = StringAlignment.Near;
                                drawFormat.LineAlignment = StringAlignment.Far;
                                break;
                            }

                        case ContentAlignment.BottomRight:
                            {
                                drawFormat.Alignment = StringAlignment.Far;
                                drawFormat.LineAlignment = StringAlignment.Far;
                                break;
                            }

                        case ContentAlignment.MiddleCenter:
                            {
                                drawFormat.Alignment = StringAlignment.Center;
                                drawFormat.LineAlignment = StringAlignment.Center;
                                break;
                            }

                        case ContentAlignment.MiddleLeft:
                            {
                                drawFormat.Alignment = StringAlignment.Near;
                                drawFormat.LineAlignment = StringAlignment.Center;
                                break;
                            }

                        case ContentAlignment.MiddleRight:
                            {
                                drawFormat.Alignment = StringAlignment.Far;
                                drawFormat.LineAlignment = StringAlignment.Center;
                                break;
                            }

                        case ContentAlignment.TopCenter:
                            {
                                drawFormat.Alignment = StringAlignment.Center;
                                drawFormat.LineAlignment = StringAlignment.Near;
                                break;
                            }

                        case ContentAlignment.TopLeft:
                            {
                                drawFormat.Alignment = StringAlignment.Near;
                                drawFormat.LineAlignment = StringAlignment.Near;
                                break;
                            }

                        case ContentAlignment.TopRight:
                            {
                                drawFormat.Alignment = StringAlignment.Far;
                                drawFormat.LineAlignment = StringAlignment.Near;
                                break;
                            }
                    }

                    canvas.DrawString(_text, Font, drawBrush, _textRect, drawFormat);
                }

                drawBrush.Dispose();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gr"></param>
        /// <param name="_angle"></param>
        /// <param name="txt"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="font"></param>
        /// <param name="brush"></param>
        private void DrawRotatedTextAt(Graphics gr, float _angle, string txt, int x, int y, Font font, Brush brush)
        {
            var state = gr.Save();
            gr.ResetTransform();
            gr.RotateTransform(_angle);
            gr.TranslateTransform(x, y, System.Drawing.Drawing2D.MatrixOrder.Append);
            gr.DrawString(txt, font, brush, 0, 0);
            gr.Restore(state);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="canvas"></param>
        public override void PerformAutoSize(Graphics canvas)
        {
            var minSize = default(SizeF);
            if (_autoSizeMode != AutoSizeModes.None)
            {
                if (_text.Length == 0)
                {
                    // Me.Width = 0
                    Height = 0;
                }
                else
                {
                    _textRect.X = X + Padding.Left;
                    _textRect.Y = Y + Padding.Top;
                    var switchExpr = _autoSizeMode;
                    switch (switchExpr)
                    {
                        case AutoSizeModes.byHeight:
                            {
                                if (Width > 0)
                                {
                                    minSize = canvas.MeasureString(_text, Font, Width);
                                }
                                else
                                {
                                    minSize = canvas.MeasureString(_text, Font);
                                }

                                if (_labelDirection == LabelDirections.Vertical)
                                {
                                    Height = Convert.ToInt32(Math.Ceiling(Math.Abs(minSize.Height * Math.Cos(_angle) + minSize.Width * Math.Sin(_angle))));
                                }
                                else
                                {
                                    Height = Convert.ToInt32(Math.Ceiling(minSize.Height));
                                }

                                Height += Padding.Top + Padding.Bottom;
                                break;
                            }

                        case AutoSizeModes.byWidth:
                            {
                                minSize = canvas.MeasureString(_text, Font);
                                if (_labelDirection == LabelDirections.Vertical)
                                {
                                    Width = Convert.ToInt32(Math.Ceiling(Math.Abs(minSize.Width * Math.Cos(_angle) + minSize.Height * Math.Sin(_angle))));
                                }
                                else
                                {
                                    Width = Convert.ToInt32(Math.Ceiling(minSize.Width));
                                }

                                Width += Padding.Left + Padding.Right;
                                break;
                            }

                        case AutoSizeModes.byBoth:
                            {
                                minSize = canvas.MeasureString(_text, Font);
                                if (_labelDirection == LabelDirections.Vertical)
                                {
                                    Height = Convert.ToInt32(Math.Ceiling(Math.Abs(minSize.Height * Math.Cos(_angle) + minSize.Width * Math.Sin(_angle))));
                                    Width = Convert.ToInt32(Math.Ceiling(Math.Abs(minSize.Width * Math.Cos(_angle) + minSize.Height * Math.Sin(_angle))));
                                }
                                else
                                {
                                    Width = Convert.ToInt32(Math.Ceiling(minSize.Width));  // + Me.Padding.Left + Me.Padding.Right
                                    Height = Convert.ToInt32(Math.Ceiling(minSize.Height));
                                } // + Me.Padding.Top + Me.Padding.Bottom

                                Width += Padding.Left + Padding.Right;
                                Height += Padding.Top + Padding.Bottom;
                                break;
                            }
                    }
                }
            }

            _textRect.Width = Width - Padding.Left - Padding.Right;
            _textRect.Height = Height - Padding.Top - Padding.Bottom;
            if (_textRect.Width < 0)
            {
                _textRect.Width = 0;
            }

            if (_textRect.Height < 0)
            {
                _textRect.Height = 0;
            }

            if (_rotateAngle != 0)
            {
                if (_textRect.Width != 0)
                {
                    _textRect.X -= (int)minSize.Height; 
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="canvas"></param>
        public void PerformLayout(Graphics canvas)
        {
            if (LayoutCompleted == false)
            {
                PerformAutoSize(canvas);
                if (_autoSizeMode == AutoSizeModes.None)
                {
                    _textRect = new Rectangle(X, Y, Width, Height);
                }

                LayoutCompleted = true;
            }
        }
    }
}