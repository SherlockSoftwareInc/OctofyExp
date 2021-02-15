
using System;
using System.Collections.Generic;
using System.Drawing;

namespace OctofyLib
{
    public class AreaPlot : ChartElementBase
    {
        private decimal?[,] _values;
        private int _seriesCount;
        private Rectangle _clientRectangle;
        private CaptionTypes _captionType;
        private double _maximumScale;
        private int[] _coords;
        private bool _useExtenalCoords;
        private int _count;
        private float _drawingRatio;
        private bool _autoScale = true;
        private bool _percentMode;
        private double[] _seriesTotal;
        private List<PolygonItem> _areas = new List<PolygonItem>();

        private class PolygonItem
        {
            public int X1 { get; set; }
            public int X2 { get; set; }
            public int Y1 { get; set; }
            public int Y2 { get; set; }
            public int Y3 { get; set; }
            public int Y4 { get; set; }
            public Color FillColor { get; set; }
            public float DrawingRatio1 { get; set; }
            public float DrawingRatio2 { get; set; }
            public int BottomBound { get; set; }
            public int DataPointIndex { get; set; }
            public int seriesIndex { get; set; }

            public void Draw(Graphics canvas)
            {
                canvas.FillPolygon(new SolidBrush(FillColor), Points());
            }

            public Point[] Points()
            {
                var pts = new Point[5];
                pts[0] = new Point(X1, (int)(BottomBound - Y1 * DrawingRatio1));
                pts[1] = new Point(X1, (int)(BottomBound - Y2 * DrawingRatio1));
                pts[2] = new Point(X2, (int)(BottomBound - Y4 * DrawingRatio2));
                pts[3] = new Point(X2, (int)(BottomBound - Y3 * DrawingRatio2));
                pts[4] = new Point(X1, (int)(BottomBound - Y1 * DrawingRatio1));
                return pts;
            }

            public bool HitTest(Point location)
            {
                var result = default(bool);
                using (var gp = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    gp.AddPolygon(Points());
                    if (gp.IsVisible(location))
                    {
                        result = true;
                    }
                }

                return result;
            }
        }

        public double BaselineValue { get; set; }

        /* TODO ERROR: Skipped RegionDirectiveTrivia */
        public bool PercentMode
        {
            get
            {
                return _percentMode;
            }

            set
            {
                _percentMode = value;
            }
        }

        /// <summary>
        /// Gets or sets bar colors
        /// </summary>
        public ColorSchema Colors { get; set; }

        /// <summary>
        /// Gets number of bars
        /// </summary>
        public int Count
        {
            get
            {
                return _count;
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
                LayoutCompleted = false;
            }
        }

        /// <summary>
        /// Gets or sets maximum value
        /// </summary>
        public string MaximumScale
        {
            get
            {
                return _maximumScale.ToString();
            }

            set
            {
                _maximumScale = double.Parse(value);
            }
        }

        /// <summary>
        /// Gets number of sections of a bar
        /// </summary>
        public int seriesCount
        {
            get
            {
                return _seriesCount;
            }
        }

        /// <summary>
        /// Gets or set section names
        /// </summary>
        public List<string> SectionNames { get; set; }

        private int _maxValue;

        public int MaximumValue
        {
            get
            {
                return _maxValue;
            }
        }

        /// <summary>
        /// Gets or sets values for stacked bar chart
        /// </summary>
        public decimal?[,] Values
        {
            get
            {
                return _values;
            }

            set
            {
                _values = value;
                _count = Values.GetLength(0);
                _seriesCount = Values.GetLength(1);
                _seriesTotal = new double[_count];
                _maxValue = 0;
                if (_count > 0 & _seriesCount > 0)
                {
                    for (int i = 0; i < _count; i++)
                    {
                        _seriesTotal[i] = 0;
                        for (int j = 0; j < _seriesCount; j++)
                        {
                            if (value[i, j].HasValue)
                                _seriesTotal[i] += (double)value[i, j];

                        }
                        if (_seriesTotal[i] > _maxValue)
                        {
                            _maxValue = Convert.ToInt32(_seriesTotal[i]);
                        }
                    }
                }

                LayoutCompleted = false;
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
                LayoutCompleted = false;
            }
        }

        public void Clear()
        {
            if (_areas != null)
            {
                _areas.Clear();
            }
        }

        /// <summary>
        /// Draw area plot with specified vertical drawing ratio
        /// </summary>
        /// <param name="canvas"></param>
        /// <remarks></remarks>
        public override void Draw(Graphics canvas)
        {
            if (_count >= 0 & _seriesCount > 0)
            {
                if (LayoutCompleted == false)
                {
                    PerformLayout(canvas);
                }

                // draw polygons
                foreach (var item in _areas)
                    item.Draw(canvas);
            }
        }

        public int SelectedXIndex { get; set; }
        public int SelectedYIndex { get; set; }

        public override bool HitTest(Point location, ref int hitPeriodIndex, ref string hitInfo)
        {
            SelectedXIndex = -1;
            SelectedYIndex = -1;
            bool result = false;
            if (_areas.Count == 1)
            {
                int x1 = Left;
                int x2 = Left + Width;
                if (_areas[0].HitTest(location))
                {
                    int seriesIndex = _areas[0].seriesIndex;
                    string strName = SectionNameAt((short)(seriesIndex));
                    string strValue = _values[0, seriesIndex].ToString();
                    string strPercent = Percentage(0, seriesIndex);
                    if (strPercent.Length > 0)
                    {
                        if (strName.Length > 0)
                        {
                            hitInfo = string.Format("{0}: {1} ({2})", strName, strValue, strPercent);
                        }
                        else
                        {
                            hitInfo = string.Format("{0} ({1})", strValue, strPercent);
                        }
                    }
                    else if (strName.Length > 0)
                    {
                        hitInfo = string.Format("{0}: {1}", strName, strValue);
                    }
                    else
                    {
                        hitInfo = string.Format("{0}", strValue);
                    }

                    SelectedYIndex = seriesIndex;
                    SelectedXIndex = 0;
                    result = true;
                }
            }
            else
            {
                for (int i = 0; i < _areas.Count; i++)
                {
                    if (_areas[i].HitTest(location))
                    {
                        hitPeriodIndex = i;
                        int dataPointIndex = _areas[i].DataPointIndex;
                        if (dataPointIndex < _count)
                        {
                            int x1 = _coords[dataPointIndex];
                            int x2 = _coords[dataPointIndex + 1];
                            if (location.X > x1 + (x2 - x1) / (double)2)
                            {
                                dataPointIndex += 1;
                            }

                            int seriesIndex = _areas[i].seriesIndex;
                            string strName = SectionNameAt((short)(seriesIndex));
                            string strValue = _values[dataPointIndex, seriesIndex].ToString();
                            string strPercent = Percentage(dataPointIndex, seriesIndex);
                            if (strPercent.Length > 0)
                            {
                                if (strName.Length > 0)
                                {
                                    hitInfo = string.Format("{0}: {1} ({2})", strName, strValue, strPercent);
                                }
                                else
                                {
                                    hitInfo = string.Format("{0} ({1})", strValue, strPercent);
                                }
                            }
                            else if (strName.Length > 0)
                            {
                                hitInfo = string.Format("{0}: {1}", strName, strValue);
                            }
                            else
                            {
                                hitInfo = string.Format("{0}", strValue);
                            }

                            SelectedXIndex = dataPointIndex;
                            SelectedYIndex = seriesIndex;
                        }

                        result = true;
                        break;
                    }
                }
            }

            return result;
        }


        public string SectionNameAt(short index)
        {
            string result = string.Empty;
            if (SectionNames != null)
            {
                if (index < SectionNames.Count)
                {
                    result = SectionNames[index];
                    if (result.Length == 0)
                    {
                        if (SectionNames.Count > 1)
                        {
                            result = Properties.Resources.B003; // "(Blanks)";
                        }
                    }
                }
            }

            return result;
        }

        public int[] Coords
        {
            get
            {
                return _coords;
            }

            set
            {
                _coords = value;
                LayoutCompleted = false;
                _useExtenalCoords = true;
            }
        }


        public bool PerformLayout(Graphics canvas)
        {
            var result = default(bool);
            if (Colors == null)
            {
                Colors = new ColorSchema();
            }

            if (_count > 0 & ClientAreaRectangle.Width > 1 & ClientAreaRectangle.Height > 1)
            {
                if (_useExtenalCoords == false)
                {
                    _coords = new int[_count];
                    float dw = (float)(Width / (double)(_count - 1));
                    for (int i = 0; i < _count; i++)
                        _coords[i] = (int)(i * dw + Left);
                }

                int cellWidth = Width;
                if (_coords.Length > 1)
                {
                    cellWidth = _coords[1] - _coords[0];
                }

                int halfWidth = (int)(cellWidth / (double)2);

                // calculate polygons
                if (_areas.Count > 0)
                {
                    _areas.Clear();
                }

                int x1, x2, y1, y2, y3, y4;
                int bb = Top + Height;
                float dr1 = _drawingRatio;
                float dr2 = _drawingRatio;
                if (_count > 1)
                {
                    for (int i = 0; i < _count - 1; i++)
                    {
                        if (PercentMode)
                        {
                            if (_seriesTotal[i] > 0)
                            {
                                dr1 = (float)(Height / _seriesTotal[i]);
                            }
                            else
                            {
                                dr1 = 0.0F;
                            }

                            if (_seriesTotal[i + 1] > 0)
                            {
                                dr2 = (float)(Height / _seriesTotal[i + 1]);
                            }
                            else
                            {
                                dr2 = 0.0F;
                            }
                        }

                        x1 = _coords[i];
                        x2 = _coords[i + 1];
                        y1 = 0;
                        y2 = (int)_values[i, 0];
                        y3 = 0;
                        y4 = (int)_values[i + 1, 0];
                        for (int j = 0; j < _seriesCount; j++)
                        {
                            var areaItem = new PolygonItem();
                            areaItem.DataPointIndex = i;
                            areaItem.seriesIndex = j;
                            areaItem.X1 = x1;
                            areaItem.X2 = x2;
                            areaItem.Y1 = y1;
                            areaItem.Y2 = y2;
                            areaItem.Y3 = y3;
                            areaItem.Y4 = y4;
                            areaItem.DrawingRatio1 = dr1;
                            areaItem.DrawingRatio2 = dr2;
                            areaItem.BottomBound = bb;
                            areaItem.FillColor = Colors.GetColorAt((short)(j));
                            _areas.Add(areaItem);
                            if (j < _seriesCount - 1)
                            {
                                y1 = y2;
                                y2 += (int)_values[i, j + 1];
                                y3 = y4;
                                y4 += (int)_values[i + 1, j + 1];
                            }
                        }
                    }
                }
                else if (_count == 1)
                {
                    if (PercentMode)
                    {
                        if (_seriesTotal[0] > 0)
                        {
                            dr1 = (float)(Height / _seriesTotal[0]);
                        }
                        else
                        {
                            dr1 = 0.0F;
                        }

                        dr2 = dr1;
                    }

                    x1 = Left;
                    x2 = Left + Width;
                    y1 = 0;
                    y2 = (int)_values[0, 0];
                    y3 = 0;
                    y4 = (int)_values[0, 0];
                    for (int j = 0; j < _seriesCount; j++)
                    {
                        var areaItem = new PolygonItem();
                        areaItem.DataPointIndex = 0;
                        areaItem.seriesIndex = j;
                        areaItem.X1 = x1;
                        areaItem.X2 = x2;
                        areaItem.Y1 = y1;
                        areaItem.Y2 = y2;
                        areaItem.Y3 = y3;
                        areaItem.Y4 = y4;
                        areaItem.DrawingRatio1 = dr1;
                        areaItem.DrawingRatio2 = dr2;
                        areaItem.BottomBound = bb;
                        areaItem.FillColor = Colors.GetColorAt((short)(j));
                        _areas.Add(areaItem);
                        if (j < _seriesCount - 1)
                        {
                            y1 = y2;
                            y2 += (int)_values[0, j + 1];
                            y3 = y4;
                            y4 += (int)_values[0, j + 1];
                        }
                    }
                }

                result = true;
            }

            _drawingRatio = (float)(ClientAreaRectangle.Height / (double)100);
            LayoutCompleted = true;
            return result;
        }

        private string BuildSectionText(int dataPointIndex, short seriesIndex)
        {
            string strValue = string.Empty;
            string strName = string.Empty;
            string strPercent = string.Empty;
            string result = string.Empty;
            if (dataPointIndex >= 0 & dataPointIndex < _values.GetLength(0))
            {
                if (seriesIndex >= 0 & seriesIndex < _values.GetLength(1))
                {
                    if (_values[dataPointIndex, seriesIndex] > 0 == true)
                    {
                        strValue = _values[dataPointIndex, seriesIndex].ToString();
                        strPercent = Percentage(dataPointIndex, seriesIndex);
                        if (SectionNames != null)
                        {
                            if (seriesIndex < SectionNames.Count)
                            {
                                strName = SectionNames[seriesIndex];
                            }
                        }

                        var switchExpr = _captionType;
                        switch (switchExpr)
                        {
                            case CaptionTypes.Name:
                                {
                                    result = strName;
                                    break;
                                }

                            case CaptionTypes.NamePercent:
                                {
                                    if (strName.Length > 0)
                                    {
                                        result = string.Format("{0} ({1})", strName, strPercent);
                                    }
                                    else
                                    {
                                        result = strPercent;
                                    }

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

                            case CaptionTypes.NameVolumePercent:
                                {
                                    if (strName.Length > 0)
                                    {
                                        result = string.Format("{0}: {1} ({2})", strName, strValue, strPercent);
                                    }
                                    else
                                    {
                                        result = string.Format("{0} ({1})", strValue, strPercent);
                                    }

                                    break;
                                }

                            case CaptionTypes.None:
                                {
                                    result = string.Empty;
                                    break;
                                }

                            case CaptionTypes.Percent:
                                {
                                    result = strPercent;
                                    break;
                                }

                            case CaptionTypes.Volume:
                                {
                                    result = strValue;
                                    break;
                                }

                            case CaptionTypes.VolumePercent:
                                {
                                    result = string.Format("{0} ({1})", strValue, strPercent);
                                    break;
                                }
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Build display text in percentage
        /// </summary>
        /// <param name="seriesIndex"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private string Percentage(int dataPointIndex, int seriesIndex)
        {
            string result = string.Empty;
            if (dataPointIndex >= 0 & dataPointIndex < _count)
            {
                if (seriesIndex >= 0 & seriesIndex < _seriesCount)
                {
                    if (_seriesTotal[dataPointIndex] > 0)
                    {
                        var value = default(int);
                        if (_values[dataPointIndex, seriesIndex].HasValue)
                        {
                            value = (int)_values[dataPointIndex, seriesIndex];
                        }

                        result = string.Format("{0}%", Math.Round(value / _seriesTotal[dataPointIndex] * 100));
                    }
                }
            }

            return result;
        }
    }
}