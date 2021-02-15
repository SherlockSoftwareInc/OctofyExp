using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OctofyLib
{
    public class StackedColumnPlot : ChartElementBase
    {
        private readonly List<StackedColumn> _bars = new List<StackedColumn>();
        private decimal?[,] _values;
        private int _numOfseries;
        private StackedColumn.BarDirections _barDirection;
        private CaptionTypes _captionType = CaptionTypes.Volume;
        private List<string> _sectionNames = new List<string>();
        private BorderStyle _borderStyle;
        private short _depth3D;
        private int _scaleMax;
        private int _maxValue;
        private ColorSchema _colors;


        /// <summary>
        /// Gets or sets 3D depth
        /// </summary>
        public short Depth3D
        {
            get
            {
                return _depth3D;
            }

            set
            {
                _depth3D = value;
                foreach (var item in _bars)
                    item.Depth3D = value;
            }
        }

        /// <summary>
        /// Gets or set border style
        /// </summary>
        public BorderStyle BorderStyle
        {
            get
            {
                return _borderStyle;
            }

            set
            {
                _borderStyle = value;
                foreach (var item in _bars)
                    item.BorderStyle = value;
            }
        }

        public int HitBarSectionIndex { get; set; }

        public ColorSchema BarColors
        {
            get
            {
                return _colors;
            }

            set
            {
                _colors = value;
            }
        }

        public StackedColumn.BarDirections BarDirection
        {
            get
            {
                return _barDirection;
            }

            set
            {
                _barDirection = value;
                foreach (var item in _bars)
                    item.DrawDirection = value;
            }
        }

        /// <summary>
        /// Gets or sets bar caption type
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
                foreach (var item in _bars)
                    item.CaptionType = value;
            }
        }

        public int ScaleMax
        {
            get
            {
                return _scaleMax;
            }

            set
            {
                _scaleMax = value;
                LayoutCompleted = false;
            }
        }

        public int MaxValue
        {
            get
            {
                return _maxValue;
            }
        }

        public int[] Coords { get; set; }
        public float DrawingRatio { get; set; }

        public decimal?[,] Values
        {
            get
            {
                return _values;
            }

            set
            {
                _values = value;
                PopulateBars();
            }
        }

        public int BarCount
        {
            get
            {
                return _bars.Count;
            }
        }

        /// <summary>
        /// Gets or set section names
        /// </summary>
        public List<string> SectionNames
        {
            get
            {
                return _sectionNames;
            }

            set
            {
                _sectionNames = value;
                foreach (var item in _bars)
                    item.SectionName = value;
            }
        }

        public void Clear()
        {
            _bars.Clear();
        }

        public override void Draw(Graphics canvas)
        {
            PerformLayout(canvas);
            foreach (StackedColumn item in _bars)
                item.DrawBar(canvas);
        }

        public float BarWidthPercent { get; set; } = 0.5F;

        public void PerformLayout(Graphics canvas)
        {
            try
            {
                if (LayoutCompleted == false)
                {
                    if (BarCount > 0)
                    {
                        int cellWidth = (int)(base.Width / (double)BarCount);
                        int x = base.Left;
                        int y = base.Top;
                        int y1 = Bottom;
                        int px = (int)(cellWidth / (double)2);
                        if (Coords == null)
                        {
                            var coordX = new int[BarCount];
                            for (int i = 0; i < BarCount; i++)
                            {
                                coordX[i] = px;
                                _bars[i].Maximum = ScaleMax;
                                _bars[i].BarWidthPercent = BarWidthPercent;
                                _bars[i].ClientAreaRect = new Rectangle(x, y, cellWidth, base.Height);
                                px += cellWidth;
                                x += cellWidth;
                            }
                        }
                        else
                        {
                            for (int i = 0; i < BarCount; i++)
                            {
                                _bars[i].Maximum = ScaleMax;
                                _bars[i].DrawingRatio = DrawingRatio;
                                x = Coords[i] - px;
                                _bars[i].BarWidthPercent = BarWidthPercent;
                                _bars[i].ClientAreaRect = new Rectangle(x, y, cellWidth, base.Height);
                            }
                        }
                    }

                    LayoutCompleted = true;
                }
            }
            catch (Exception)
            {
                // Debug.Print(ex.Message)
                throw;
            }
        }

        public int SelectedXIndex { get; set; }
        public int SelectedYIndex { get; set; }

        public override bool HitTest(Point location, ref int hitPeriodIndex, ref string hitText)
        {
            var result = default(bool);
            hitPeriodIndex = 0;
            hitText = string.Empty;
            SelectedXIndex = -1;
            SelectedYIndex = -1;
            for (int i = 0; i < _bars.Count; i++)
            {
                var item = _bars[i];
                if (item.IsHit(location))
                {
                    hitText = item.ToolTipText;
                    hitPeriodIndex = i;
                    HitBarSectionIndex = _bars[i].HitSection;
                    result = true;
                    SelectedXIndex = i;
                    SelectedYIndex = HitBarSectionIndex;
                    break;
                }
            }

            return result;
        }

        private void PopulateBars()
        {
            int numOfBars;
            _maxValue = 0;
            try
            {
                numOfBars = Values.GetLength(0);
                _numOfseries = Values.GetLength(1);
                if (_bars.Count > 0)
                {
                    _bars.Clear();
                }

                if (_colors == null)
                {
                    _colors = new ColorSchema();
                }

                if (numOfBars > 0 & _numOfseries > 0)
                {
                    for (int i = 0; i < numOfBars; i++)
                    {
                        var barValues = GetBarValues(i);
                        int mv = 0;
                        foreach (var value in barValues)
                        {
                            if (value.HasValue)
                                mv += (int)value;
                        }
                        if (mv > _maxValue)
                        {
                            _maxValue = mv;
                        }

                        var oBar = new StackedColumn(Font);
                        oBar.Colors = _colors;
                        oBar.DrawDirection = _barDirection;
                        oBar.CaptionType = _captionType;
                        oBar.SectionName = _sectionNames;
                        oBar.BorderStyle = _borderStyle;
                        oBar.Depth3D = _depth3D;
                        oBar.Values = barValues;
                        _bars.Add(oBar);
                    }
                }

                LayoutCompleted = false;
            }
            catch (Exception ex)
            {
            }
        }

        private int?[] GetBarValues(int index)
        {
            var barValues = new int?[_numOfseries];
            // For i = 0 To _numOfseries - 1
            // barValues(i) = 0
            // Next

            if (index >= 0 & index < _values.GetLength(0))
            {
                for (int i = 0; i < _numOfseries; i++)
                    barValues[i] = (int?)_values[index, i];
            }

            return barValues;
        }
    }
}