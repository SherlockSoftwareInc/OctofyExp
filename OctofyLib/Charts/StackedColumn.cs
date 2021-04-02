
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OctofyLib
{
    public class StackedColumn : ChartElementBase
    {
        public event EventHandler Overflow;

        public enum BarDirections
        {
            LeftToRight,
            RightToLeft,
            TopToBottom,
            BottomToTop
        }

        private bool _calculated;
        private ColorSchema _barColor;
        private int _total;           // Total
        private short _numSection;        // number of sections
        private int _maximum = 100;
        private BarDirections _drawDirection = BarDirections.BottomToTop;
        private CaptionTypes _captionType = CaptionTypes.None;
        //private readonly short _selectedSection = -1;
        private Point _location = new Point();
        private Size _size = new Size();
        private int?[] _values;
        private Rectangle _clientAreaRect;
        private bool _isOverflow=false;
        private readonly List<ColumnCellItem> _barSections = new List<ColumnCellItem>();
        private float _drawingRatio;
        private float _barWidthPercent = 0.5F;
        private string _toolTipText = "";
        private short _depth3D;

        public StackedColumn(Font font)
        {
            Font = font;
            _barColor = new ColorSchema();
        }

        /* TODO ERROR: Skipped RegionDirectiveTrivia */
        public float BarWidthPercent
        {
            get
            {
                return _barWidthPercent;
            }

            set
            {
                if (value != _barWidthPercent)
                {
                    if (value > 0 & value <= 1)
                    {
                        _barWidthPercent = value;
                        _calculated = false;
                    }
                    else if (value > 1 & value <= 100)
                    {
                        _barWidthPercent = value / 100;
                        _calculated = false;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets border style
        /// </summary>
        public BorderStyle BorderStyle { get; set; }

        /// <summary>
        /// Gets or set client area rectangle
        /// </summary>
        public Rectangle ClientAreaRect
        {
            get
            {
                return _clientAreaRect;
            }

            set
            {
                _clientAreaRect = value;
                _calculated = false;
            }
        }

        /// <summary>
        /// Gets or sets driection to draw
        /// </summary>
        public BarDirections DrawDirection
        {
            get
            {
                return _drawDirection;
            }

            set
            {
                _drawDirection = value;
                _calculated = false;
            }
        }

        /// <summary>
        /// Gets or sets drawing ratio (valume vs maximum)
        /// </summary>
        public float DrawingRatio
        {
            get
            {
                return _drawingRatio;
            }

            set
            {
                _drawingRatio = value;
                _calculated = false;
            }
        }

        /// <summary>
        /// Gets or sets hit section index
        /// </summary>
        public short HitSection { get; set; }

        /// <summary>
        /// Gets or sets maximum value of the bar
        /// </summary>
        public int Maximum
        {
            get
            {
                return _maximum;
            }

            set
            {
                if (value <= 0)
                {
                    // Throw (New Exception("Invalid value. Maximum value should great than 0."))
                    _maximum = 1;
                }
                else
                {
                    _maximum = value;
                    _calculated = false;
                }
            }
        }

        /// <summary>
        /// Gets or sets contents express type
        /// </summary>
        public CaptionTypes CaptionType
        {
            get
            {
                return _captionType;
            }

            set
            {
                _captionType = value;
                _calculated = false;
                PerformLayout();
            }
        }

        /// <summary>
        /// Gets or sets section name
        /// </summary>
        public List<string> SectionName { get; set; } = new List<string>();

        /// <summary>
        /// Gets tool tip text
        /// </summary>
        public string ToolTipText
        {
            get
            {
                return _toolTipText;
            }
        }

        /// <summary>
        /// Gets total value
        /// </summary>
        public int Total
        {
            get
            {
                return _total;
            }
        }

        /// <summary>
        /// Gets or sets values to draw
        /// </summary>
        public int?[] Values
        {
            get
            {
                return _values;
            }

            set
            {
                _values = value;
                _total = 0;
                if (value.Length > 0)
                {
                    for (int i = 0; i < value.Length; i++)
                    {
                        if (value[i].HasValue)
                        {
                            _total += (int)value[i];
                        }
                    }
                }

                _calculated = false;
            }
        }

        public short Depth3D
        {
            get
            {
                return _depth3D;
            }

            set
            {
                if (value >= 0)
                {
                    if (value < 500)
                    {
                        _depth3D = value;
                    }
                    else
                    {
                        _depth3D = 500;
                    }
                }
                else
                {
                    _depth3D = 0;
                }
            }
        }

        public bool HighlightHoverSection { get; set; }

        private readonly ColorSchema _colors = new ColorSchema();

        /// <summary>
        /// Calculate drawing parameters
        /// </summary>
        /// <remarks></remarks>
        private void PerformLayout()
        {
            if (_values == null)
            {
                return;
            }

            if (!_calculated)
            {
                _numSection = (short)(_values.Length);
                if (SectionName != null)
                {
                    if (SectionName.Count > _numSection)
                    {
                        _numSection = (short)(SectionName.Count);
                    }
                }

                if (_numSection != _barSections.Count)
                {
                    if (_barSections.Count > 0)
                    {
                        _barSections.Clear();
                    }

                    for (int i = 0; i <= _numSection; i++)
                    {
                        var oSection = new ColumnCellItem()
                        {
                            Font = Font,
                            SectionName = "",
                            Value = 0,
                            Text = "",
                            ForeColor = _colors.GetColorAt((short)(i - 1)),
                            Depth3D = _depth3D
                        };
                        _barSections.Add(oSection);
                    }
                }

                bool showNumber = _captionType != CaptionTypes.None;
                for (int i = 0; i < _numSection; i++)
                {
                    _barSections[i].Value = 0;
                    _barSections[i].Text = "";
                    _barSections[i].ShowNumber = showNumber;
                }

                for (int i = 0; i < _values.Length; i++)
                {
                    _barSections[i].Value = _values[i];
                    _barSections[i].Text = BuildSectionText(i);
                }

                if (SectionName != null)
                {
                    for (int i = 0; i < SectionName.Count; i++)
                        _barSections[i].SectionName = SectionName[i];
                }

                for (short i = 0; i < _numSection; i++)
                    _barSections[i].BarColor = _barColor.GetColorAt(i);

                if (Total > Maximum)
                {
                    Overflow?.Invoke(this, new EventArgs());
                    _isOverflow = true;
                }
                else
                {
                    _isOverflow = false;
                }

                if (DrawDirection == BarDirections.LeftToRight | DrawDirection == BarDirections.RightToLeft)
                {
                    int barDy = (int)(ClientAreaRect.Height * (1 - _barWidthPercent) / 2);
                    if (barDy < 0)
                    {
                        barDy = 0;
                    }

                    int barHeight = ClientAreaRect.Height - barDy;
                    if (barHeight < 0)
                    {
                        barHeight = 0;
                    }

                    _location = new Point(ClientAreaRect.Left, ClientAreaRect.Top + barDy);
                    _size = new Size(ClientAreaRect.Width, ClientAreaRect.Height - barDy * 2);
                }
                else
                {
                    int barDx = (int)(ClientAreaRect.Width * (1 - _barWidthPercent) / 2);
                    if (barDx < 0)
                    {
                        barDx = 0;
                    }

                    int barWidth = ClientAreaRect.Width - barDx;
                    if (barWidth < 0)
                    {
                        barWidth = 0;
                    }

                    _location = new Point(ClientAreaRect.Left + barDx, ClientAreaRect.Top);
                    _size = new Size(ClientAreaRect.Width - barDx * 2, ClientAreaRect.Height);
                }

                if (_drawingRatio == 0)
                {
                    int sectionTotal = 0;
                    int totalDrawSpace;
                    if (DrawDirection == BarDirections.LeftToRight | DrawDirection == BarDirections.RightToLeft)
                    {
                        totalDrawSpace = _size.Width;
                    }
                    else
                    {
                        totalDrawSpace = _size.Height;
                    }

                    int totalValue = Total;
                    int z;
                    int x1 = default, x2 = default, y1 = default, y2 = default;
                    if (DrawDirection == BarDirections.RightToLeft)
                    {
                        x2 = _size.Width;
                    }

                    if (DrawDirection == BarDirections.BottomToTop)
                    {
                        y2 = _size.Height;
                    }

                    for (int i = 0; i < _values.Length; i++)
                    {
                        if (_values[i].HasValue)
                        {
                            sectionTotal += (int)_values[i];
                        }

                        if (DrawDirection == BarDirections.BottomToTop | DrawDirection == BarDirections.RightToLeft)
                        {
                            z = (int)((1 - sectionTotal / (double)Maximum) * totalDrawSpace);
                        }
                        else
                        {
                            z = (int)(sectionTotal / (double)Maximum * totalDrawSpace);
                        }

                        var switchExpr = DrawDirection;
                        switch (switchExpr)
                        {
                            case BarDirections.LeftToRight:
                                {
                                    y1 = _location.Y;
                                    x2 = z;
                                    {
                                        var withBlock = _barSections[i];
                                        withBlock.X = x1 + _location.X;
                                        withBlock.Y = y1;
                                        withBlock.Width = x2 - x1;
                                        withBlock.Height = _size.Height;
                                    }

                                    x1 = x2;
                                    break;
                                }

                            case BarDirections.RightToLeft:
                                {
                                    y1 = _location.Y;
                                    x1 = z;
                                    {
                                        var withBlock1 = _barSections[i];
                                        withBlock1.X = x1 + _location.X;
                                        withBlock1.Y = y1;
                                        withBlock1.Width = x2 - x1;
                                        withBlock1.Height = _size.Height;
                                    }

                                    x2 = x1;
                                    break;
                                }

                            case BarDirections.TopToBottom:
                                {
                                    x1 = _location.X;
                                    y2 = z;
                                    {
                                        var withBlock2 = _barSections[i];
                                        withBlock2.X = x1;
                                        withBlock2.Y = y1 + _location.Y;
                                        withBlock2.Width = _size.Width;
                                        withBlock2.Height = y2 - y1;
                                    }

                                    y1 = y2;
                                    break;
                                }

                            case BarDirections.BottomToTop:
                                {
                                    x1 = _location.X;
                                    y1 = z;
                                    {
                                        var withBlock3 = _barSections[i];
                                        withBlock3.X = x1;
                                        withBlock3.Y = y1 + _location.Y;
                                        withBlock3.Width = _size.Width;
                                        withBlock3.Height = y2 - y1;
                                    }

                                    y2 = y1;
                                    break;
                                }
                        }
                    }
                }
                else
                {
                    int x = default, y = default;
                    var switchExpr1 = DrawDirection;
                    switch (switchExpr1)
                    {
                        case BarDirections.LeftToRight:
                            {
                                x = _location.X;
                                y = _location.Y;
                                break;
                            }

                        case BarDirections.RightToLeft:
                            {
                                x = _location.X + _size.Width;
                                y = _location.Y;
                                break;
                            }

                        case BarDirections.BottomToTop:
                            {
                                x = _location.X;
                                y = _location.Y + _size.Height;
                                break;
                            }

                        case BarDirections.TopToBottom:
                            {
                                x = _location.X;
                                y = _location.Y;
                                break;
                            }
                    }

                    for (int i = 0; i < _numSection; i++)
                    {
                        int dv = 0;
                        if (Values[i].HasValue)
                        {
                            dv = (int)(Values[i] * _drawingRatio);
                        }

                        var switchExpr2 = DrawDirection;
                        switch (switchExpr2)
                        {
                            case BarDirections.LeftToRight:
                                {
                                    {
                                        var withBlock4 = _barSections[i];
                                        withBlock4.X = x;
                                        withBlock4.Y = y;
                                        withBlock4.Width = dv;
                                        withBlock4.Height = _size.Height;
                                    }

                                    x += dv;
                                    break;
                                }

                            case BarDirections.RightToLeft:
                                {
                                    {
                                        var withBlock5 = _barSections[i];
                                        withBlock5.X = x - dv;
                                        withBlock5.Y = y;
                                        withBlock5.Width = dv;
                                        withBlock5.Height = _size.Height;
                                    }

                                    x -= dv;
                                    break;
                                }

                            case BarDirections.TopToBottom:
                                {
                                    {
                                        var withBlock6 = _barSections[i];
                                        withBlock6.X = x;
                                        withBlock6.Y = y;
                                        withBlock6.Width = _size.Width;
                                        withBlock6.Height = dv;
                                    }

                                    y += dv;
                                    break;
                                }

                            case BarDirections.BottomToTop:
                                {
                                    {
                                        var withBlock7 = _barSections[i];
                                        withBlock7.X = x;
                                        withBlock7.Y = y - dv;
                                        withBlock7.Width = _size.Width;
                                        withBlock7.Height = dv;
                                    }

                                    y -= dv;
                                    break;
                                }
                        }
                    }
                }

                _calculated = true;
            }
        }

        private string BuildSectionText(int index)
        {
            string strValue = "";
            string strName = "";
            string strPercent = "";
            string result = "";
            if (index >= 0 & index < _values.Length)
            {
                if (_values[index].HasValue)
                {
                    strValue = _values[index].ToString();
                    strPercent = Percentage((int)_values[index]);
                }

                if (index < SectionName.Count)
                {
                    strName = SectionName[index];
                }

                var switchExpr = _captionType;
                switch (switchExpr)
                {
                    case CaptionTypes.Volume:
                        {
                            result = strValue;
                            break;
                        }

                    case CaptionTypes.Percent:
                        {
                            result = strPercent;
                            break;
                        }

                    case CaptionTypes.Name:
                        {
                            result = strName;
                            break;
                        }

                    case CaptionTypes.NameVolume:
                        {
                            if (strName.Length > 0)
                            {
                                result = string.Format("{0}: {1}", strName, strValue);
                            }
                            else
                            {
                                result = strValue;
                            }

                            break;
                        }

                    case CaptionTypes.NamePercent:
                        {
                            if (strName.Length > 0)
                            {
                                result = string.Format("{0}: {1}", strName, strPercent);
                            }
                            else
                            {
                                result = strPercent;
                            }

                            break;
                        }
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="canvas"></param>
        /// <remarks></remarks>
        public void DrawBar(Graphics canvas)
        {
            try
            {
                PerformLayout();
                if (_numSection > 0)
                {
                    var switchExpr = _drawDirection;
                    switch (switchExpr)
                    {
                        case BarDirections.BottomToTop:
                        case BarDirections.RightToLeft:
                            {
                                for (short i = (short)(_numSection - 1); i >= 0; i -= 1)
                                {
                                    if (_barSections[i].Value.HasValue)
                                    {
                                        if (_barSections[i].Value > 0 == true)
                                        {
                                            _barSections[i].Draw(canvas);
                                        }
                                    }
                                }

                                break;
                            }

                        case BarDirections.LeftToRight:
                        case BarDirections.TopToBottom:
                            {
                                for (int i = 0; i < _numSection; i++)
                                {
                                    if (_barSections[i].Value.HasValue)
                                    {
                                        if (_barSections[i].Value > 0 == true)
                                        {
                                            _barSections[i].Draw(canvas);
                                        }
                                    }
                                }

                                break;
                            }
                    }

                    if (BorderStyle == BorderStyle.FixedSingle)
                    {
                        canvas.DrawRectangle(Pens.Black, new Rectangle(_location.X, _location.Y, _size.Width, _size.Height));
                    }
                }
            }
            catch (Exception )
            {
                //Interaction.MsgBox(ex.Message, MsgBoxStyle.Critical);
            }
        }

        /// <summary>
        /// Build display text in percentage
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private string Percentage(int numbers)
        {
            if (Total <= 0)
            {
                return "";
            }
            else
            {
                return string.Format("{0}%", Math.Round(numbers / (double)Total * 100));
            }
        }

        public bool IsHit(Point location)
        {
            if (_numSection > 0)
            {
                for (int i = 0; i < _numSection; i++)
                {
                    if (_barSections[i].IsHit(location))
                    {
                        ChangeHitSection(i);
                        return true;
                    }
                }
            }

            return false;
        }

        private void ChangeHitSection(int index)
        {
            HitSection = (short)(index);
            if (_barSections[index].Value.HasValue)
            {
                _toolTipText = string.Format("{0} {1}", _barSections[index].Value, _barSections[index].SectionName);
                if (HighlightHoverSection)
                {
                    for (int i = 0; i < _numSection; i++)
                    {
                        if (i == index)
                        {
                            _barSections[i].Selected = true;
                        }
                        else
                        {
                            _barSections[i].Selected = false;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ColorSchema Colors
        {
            set
            {
                _barColor = value;
                for (int i = 0; i < _numSection; i++)
                {
                    _barSections[i].BarColor = _barColor.GetColorAt((short)i);
                }
            }
        }
    }
}