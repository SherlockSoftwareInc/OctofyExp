
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace OctofyLib
{
    public class LabelsRow
    {
        private readonly List<ChartLabel> _labels = new List<ChartLabel>();
        private short _count;
        private bool _hasValue;
        private bool _layoutComplete;
        private int _height;
        private Color _foreColor = Color.Black;
        private Font _font = Control.DefaultFont;
        private int _width;
        private int[] _coords;
        private int _x;
        private int _y;
        private ChartLabel.LabelDirections _labelDirection = ChartLabel.LabelDirections.Horizontal;

        public bool AreaMode { get; set; }
        public StringAlignment LineAlignment { get; set; } = StringAlignment.Center;

        public int[] Coords
        {
            set
            {
                _coords = value;
                _layoutComplete = false;
            }
        }

        public short Count
        {
            get
            {
                return _count;
            }
        }

        public Font Font
        {
            get
            {
                return _font;
            }

            set
            {
                _font = value;
                foreach (var item in _labels)
                    item.Font = value;
                _layoutComplete = false;
            }
        }

        public Color ForeColor
        {
            get
            {
                return _foreColor;
            }

            set
            {
                _foreColor = value;
                foreach (var item in _labels)
                    item.ForeColor = value;
            }
        }

        public bool HasValues
        {
            get
            {
                return _hasValue;
            }

            set
            {
                _hasValue = value;
            }
        }

        public int Height
        {
            get
            {
                return _height;
            }
        }

        public bool HideOverlap { get; set; }

        public int Left
        {
            get
            {
                return _x;
            }

            set
            {
                _x = value;
                _layoutComplete = false;
            }
        }

        public Point Location
        {
            get
            {
                return new Point(_x, _y);
            }

            set
            {
                _x = value.X;
                _y = value.Y;
                _layoutComplete = false;
            }
        }

        public int Top
        {
            get
            {
                return _y;
            }

            set
            {
                _y = value;
                _layoutComplete = false;
            }
        }

        public string[] Values
        {
            set
            {
                _count = (short)(value.Count());
                _hasValue = false;
                foreach (string item in value)
                {
                    var labelItem = new ChartLabel(_font, item)
                    {
                        AutoSizeMode = ChartLabel.AutoSizeModes.byBoth,
                        ForeColor = _foreColor,
                        NoWrap = true,
                        Padding = new Padding(0, 0, 0, 0),
                        RotateAngle = RotateAngle
                    };
                    // labelItem.LabelDirection = ChartLabel.LabelDirections.Horizontal
                    _labels.Add(labelItem);
                    if (_hasValue == false)
                    {
                        if (item.Length > 0)
                        {
                            _hasValue = true;
                        }
                    }
                }

                _layoutComplete = false;
            }
        }

        public void Add(string item)
        {
            var labelItem = new ChartLabel(_font, item)
            {
                AutoSizeMode = ChartLabel.AutoSizeModes.byBoth,
                ForeColor = _foreColor,
                NoWrap = true,
                Padding = new Padding(0, 0, 0, 0),
                RotateAngle = RotateAngle
            };
            // labelItem.LabelDirection = ChartLabel.LabelDirections.Horizontal
            _labels.Add(labelItem);
            if (_hasValue == false)
            {
                if (item.Length > 0)
                {
                    _hasValue = true;
                }
            }

            _count = (short)(_labels.Count);
            _layoutComplete = false;
        }

        public int Width
        {
            get
            {
                return _width;
            }

            set
            {
                _width = value;
                _layoutComplete = false;
            }
        }

        public short RotateAngle { get; set; } = 90;

        public void Clear()
        {
            _labels.Clear();
            _count = 0;
        }

        public void PerformAutosize(Graphics canvas, int width)
        {
            _height = 0;
            _width = width;
            if (_count > 0)
            {
                float cellWidth = (float)(width / (double)_count);
                _labelDirection = ChartLabel.LabelDirections.Horizontal;
                if (CanDrawHorizontal(canvas, cellWidth) == false)
                {
                    _labelDirection = ChartLabel.LabelDirections.Vertical;
                    RotateAngle = 45;
                }
                else
                {
                    RotateAngle = 0;
                }

                int maxHeight = 0;
                foreach (ChartLabel item in _labels)
                {
                    if (item.Text.Length > 0)
                    {
                        item.Font = _font;
                        // item.LabelDirection = _labelDirection
                        item.RotateAngle = RotateAngle;
                        item.TextAlign = ContentAlignment.TopLeft;
                        item.AutoSizeMode = ChartLabel.AutoSizeModes.byBoth;
                        item.PerformAutoSize(canvas);
                        if (item.Height > maxHeight)
                        {
                            maxHeight = item.Height;
                        }
                    }
                }

                if (maxHeight > 0)
                {
                    _height = maxHeight + Padding.Top + Padding.Bottom;
                    if (RotateAngle == 0)
                    {
                        _height += Font.Height;
                    }
                }
            }
        }

        public Padding Padding { get; set; }

        public void PerformLayout(Graphics canvas)
        {
            if (_hasValue)
            {
                if (_layoutComplete == false)
                {
                    int y = _y + 3;
                    int cellWidth;
                    if (_count > 1)
                    {
                        cellWidth = _coords[1] - _coords[0];
                    }
                    else
                    {
                        cellWidth = Width;
                    }

                    if (_labelDirection == ChartLabel.LabelDirections.Vertical)
                    {
                        float angle = (float)(Math.PI * RotateAngle / 180.0);
                        int dx = Convert.ToInt32(Math.Abs(_font.Height * Math.Sin(angle)));
                        for (int i = 0; i < _count; i++)
                        {
                            _labels[i].Top = y;
                            _labels[i].Left = _coords[i] - dx;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < _count; i++)
                        {
                            _labels[i].Top = y;
                            var switchExpr = LineAlignment;
                            switch (switchExpr)
                            {
                                case StringAlignment.Center:
                                    {
                                        _labels[i].Left = (int)(_coords[i] - _labels[i].Width / (double)2);
                                        break;
                                    }

                                case StringAlignment.Near:
                                    {
                                        _labels[i].Left = (int)(_coords[i] - cellWidth / (double)2);
                                        break;
                                    }

                                case StringAlignment.Far:
                                    {
                                        _labels[i].Left = _coords[i] + cellWidth - _labels[i].Width;
                                        break;
                                    }
                            }
                        }
                    }

                    if (_count > 1 & HideOverlap & _labelDirection == ChartLabel.LabelDirections.Vertical)
                    {
                        int displayRatio = 1;
                        if (cellWidth < _labels[0].Width & cellWidth > 0)
                        {
                            displayRatio = Convert.ToInt32(Math.Ceiling(canvas.MeasureString("W", _font).Width / cellWidth));
                        }

                        if (AreaMode)
                        {
                            _labels[0].Visible = true;
                            for (int j = 0; j < _count; j++)
                            {
                                if (j % displayRatio == 0)
                                {
                                    _labels[j].Visible = true;
                                }
                                else
                                {
                                    _labels[j].Visible = false;
                                }
                            }
                        }
                        else
                        {
                            for (int j = 0; j < _count; j++)
                            {
                                if (j % displayRatio == 0)
                                {
                                    _labels[j].Visible = true;
                                }
                                else
                                {
                                    _labels[j].Visible = false;
                                }
                            }
                        }
                    }

                    _layoutComplete = true;
                }
            }
        }

        private bool CanDrawHorizontal(Graphics canvas, float width)
        {
            bool result = true;
            for (int i = 0; i < _count - 1; i++)
            {
                var item = _labels[i];
                string strText = item.Text;
                if (strText.Length > 0)
                {
                    if (ChartElementBase.GetTextSize(canvas, strText, _font).Width + 9 > width * GetEmptySpots((short)(i)))
                    {
                        result = false;
                        break;
                    }
                }
            }

            return result;
        }

        private short GetEmptySpots(short index)
        {
            short result = 1;
            for (int i = 0; i < _count; i++)
            {
                if (_labels[i].Text.Length > 0)
                {
                    break;
                }
                else
                {
                    result += 1;
                }
            }

            return result;
        }

        public void Draw(Graphics canvas)
        {
            if (_hasValue)
            {
                PerformLayout(canvas);
                foreach (ChartLabel item in _labels)
                    item.Draw(canvas);
            }
        }

        public int[] GetVisibleMarkPosition()
        {
            var values = new List<int>();
            for (int i = 0; i < _count; i++)
            {
                if (_labels[i].Visible & _labels[i].Text.Length > 0)
                {
                    values.Add(_coords[i]);
                }
            }

            return values.ToArray();
        }

        public string GetTextAt(int index)
        {
            string result = string.Empty;
            if (index >= 0 & index < _labels.Count)
            {
                result = _labels[index].Text;
            }

            return result;
        }
    }
}