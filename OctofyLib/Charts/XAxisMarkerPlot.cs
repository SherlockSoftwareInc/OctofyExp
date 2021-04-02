

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace OctofyLib
{
    /// <summary>
    /// 
    /// </summary>
    public class XAxisMarkerPlot : ChartElementBase
    {
        /// <summary>
        /// 
        /// </summary>
        public enum DataSourceTypes
        {
            TimePeriods,
            Categories,
            Values
        }

        private List<MarkerDataItem> _values;
        private int[] _coords;
        private readonly LabelsRow _labelRow1;
        private readonly LabelsRow _labelRow2;
        private DataSourceTypes _dataSourceType;

        /// <summary>
        /// 
        /// </summary>
        public XAxisMarkerPlot()
        {
            _values = new List<MarkerDataItem>();
            _labelRow1 = new LabelsRow()
            {
                Padding = new Padding(0, 0, 0, 0),
                HideOverlap = true,
                RotateAngle = 315
            };
            _labelRow2 = new LabelsRow()
            {
                Padding = new Padding(0, 1, 0, 0),
                LineAlignment = StringAlignment.Near,
                RotateAngle = 315
            };
        }

        /// <summary>
        /// 
        /// </summary>
        public bool AreaMode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Color AxisColor { get; set; } = Control.DefaultForeColor;

        /// <summary>
        /// 
        /// </summary>
        public bool AxisLineVisible { get; set; } = true;

        /// <summary>
        /// 
        /// </summary>
        public int CellWidth
        {
            get
            {
                if (_coords != null)
                {
                    if (_coords.Count() > 1)
                    {
                        return _coords[1] - _coords[0];
                    }
                    else
                    {
                        return Width;
                    }
                }
                else
                {
                    return Width;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int[] Coords
        {
            get
            {
                return _coords;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Count
        {
            get
            {
                return _values.Count;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void Open(List<TimePeriod> value)
        {
            _dataSourceType = DataSourceTypes.TimePeriods;
            ClearItems();
            if (value != null)
            {
                for (int i = 0; i < value.Count; i++)
                {
                    var item = value[i];
                    Add(item.Year, item.PeriodName, item.StartDate, item.EndDate);
                }
            }

            LayoutCompleted = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void Open(List<string> value)
        {
            _dataSourceType = DataSourceTypes.Categories;
            ClearItems();
            if (value != null)
            {
                for (int i = 0; i < value.Count; i++)
                    Add(value[i]);
            }

            LayoutCompleted = false;
        }

        /// <summary>
        /// 
        /// </summary>
        public int TargetSteps { get; set; } = 10;

        /// <summary>
        /// 
        /// </summary>
        public long MaxValue { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public float DrawingRatio
        {
            get
            {
                if (MaxValue == 0)
                {
                    return 0;
                }
                else
                {
                    return (float)(Width / (double)MaxValue);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxValue"></param>
        public void Open(long maxValue)
        {
            _dataSourceType = DataSourceTypes.Values;
            ClearItems();
            int fh = Font.Height;
            int hfh = (int)(fh / (double)2);
            short targetSteps;
            if (TargetSteps > 1)
            {
                targetSteps = (short)(TargetSteps);
            }
            else
            {
                targetSteps = (short)(Math.Floor(PlotHeight / (double)fh));
            }

            int smin = 0;
            long smax = maxValue;
            if (smin == smax)
            {
                smin = 0;
                smax = 100;
            }

            var m = BuildSteps(smin, smax, targetSteps);
            if (m.Count > 1)
            {
                for (int i = 0; i < m.Count; i++)
                    Add(m[i]);

                MaxValue = long.Parse(m[m.Count - 1]);
            }
            else
            {
                MaxValue = 0;
            }

            LayoutCompleted = false;
        }

        private List<string> BuildSteps(double value1, double value2, short targetSteps)
        {
            var result = new List<string>();
            if (targetSteps > 21)
            {
                targetSteps = 21;
            }

            double vMax = value2;
            double vMin = value1;
            if (vMin > vMax)
            {
                vMax = value1;
                vMin = value2;
            }

            if (vMin == 0 | vMax == 0)
            {
                targetSteps -= 1;
            }

            double total = vMax - vMin;
            if (vMin < 0 & vMax < 0 | vMin > 0 & vMax > 0)
            {
                targetSteps -= 1;
            }

            double s = CalcStepSize(total, targetSteps);
            if (s != 0)
            {
                string fmt = "N0";
                if (s < 1)
                {
                    string tmp = s.ToString();
                    fmt = string.Format("N{0}", tmp.Length - tmp.LastIndexOf(".") - 1);
                }

                double startValue = 0;
                if (vMin < 0)
                {
                    while (startValue > vMin)
                        startValue -= s;
                }
                else if (vMin > 0)
                {
                    while (startValue + s <= vMin)
                        startValue += s;
                }

                double v = startValue;
                while (v < vMax + s)
                {
                    result.Add(v.ToString(fmt));
                    v += s;
                }
            }

            return result;
        }

        private double CalcStepSize(double range, int targetSteps)
        {
            if (range != 0 & targetSteps > 0)
            {
                if (targetSteps > range)
                {
                    targetSteps = Convert.ToInt32(range);
                }

                // calculate an initial guess at step size
                double tempStep = range / targetSteps;
                if (tempStep == 0)
                {
                    tempStep = 1;
                }
                // get the magnitude of the step size
                double mag = Math.Floor(Math.Log10(tempStep));
                double magPow = Math.Pow(10, mag);
                magPow = (long)(magPow * 1000);
                magPow /= 1000;

                // calculate most significant digit of the new step size
                double magMsd = (int)(tempStep / magPow + 0.5);

                // promote the MSD to either 1, 2, or 5
                if (magMsd > 5.0)
                {
                    magMsd = 10.0F;
                }
                else if (magMsd > 2.0)
                {
                    magMsd = 5.0F;
                }
                else if (magMsd > 1.0)
                {
                    magMsd = 2.0F;
                }

                return magMsd * magPow;
            }
            else
            {
                return 0;
            }
        }

        public DateTime EndDate
        {
            get
            {
                if (Count > 0)
                {
                    return _values[Count - 1].EndDate;
                }
                else
                {
                    return DateTime.MaxValue;
                }
            }
        }

        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }

            set
            {
                base.ForeColor = value;
                _labelRow1.ForeColor = value;
                _labelRow2.ForeColor = value;
            }
        }

        public DateTime StartDate
        {
            get
            {
                if (Count > 0)
                {
                    return _values[0].StartDate;
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        public List<MarkerDataItem> Values
        {
            get
            {
                return _values;
            }

            set
            {
                _values = value;
                _labelRow1.Clear();
                _labelRow2.Clear();
                int cnt = value.Count;
                if (cnt > 0)
                {
                    var line1 = new string[cnt];
                    var line2 = new string[cnt];
                    for (int i = 0; i < cnt; i++)
                    {
                        line1[i] = _values[i].Caption;
                        if (i == 0 & line1[0].Length == 0)
                        {
                            line1[0] = Properties.Resources.B003;   // "(Blanks)";
                        }

                        line2[i] = _values[i].CaptionLine2;
                    }

                    _labelRow1.Values = line1;
                    _labelRow2.Values = line2;
                }

                LayoutCompleted = false;
            }
        }

        public double XAxisRatio
        {
            get
            {
                double result;
                if (_coords.Count() > 1)
                {
                    result = _coords[1] - _coords[0];
                }
                else
                {
                    result = Width;
                }

                return result;
            }
        }

        public void Add(string category)
        {
            var item = new MarkerDataItem() { Caption = category };
            _values.Add(item);
            _labelRow1.Add(category);
        }

        public void Add(int year, string period, DateTime startDate, DateTime endDate)
        {
            string caption;
            if (period.StartsWith("FP") | period.StartsWith("FQ"))
            {
                caption = string.Format("{0}/{1} {2}", year - 2000, year - 1999, period);
            }
            else
            {
                caption = string.Format("{0} {1}", year, period);
            }

            var item = new MarkerDataItem()
            {
                StartDate = startDate,
                Caption = caption,
                EndDate = endDate
            };
            _values.Add(item);
            _labelRow1.Add(caption);
        }

        public void ClearItems()
        {
            if (_values != null)
            {
                _values.Clear();
            }

            _labelRow1.Clear();
            _labelRow2.Clear();
        }

        public override void PerformAutoSize(Graphics canvas)
        {
            Width = base.Width;
            Height = 0;
            if (Count > 0)
            {
                _labelRow1.Font = Font;
                _labelRow1.PerformAutosize(canvas, base.Width);
                _labelRow2.Font = Font;
                _labelRow2.PerformAutosize(canvas, base.Width);
                Height = _labelRow1.Height + _labelRow2.Height;
                if (Height > 0)
                {
                    Height += 3;
                }
            }
        }

        //public int TodayLocation
        //{
        //    get
        //    {
        //        int result = Left;
        //        if (_coords != null)
        //        {
        //            if (_coords.Count() > 0)
        //            {
        //                result = _coords[0];
        //                long totalDays = DateAndTime.DateDiff(DateInterval.Day, StartDate, _values[Count - 1].StartDate);
        //                int w = _coords[_coords.Count() - 1] - _coords[0];
        //                int startToToday = Convert.ToInt32(DateAndTime.DateDiff(DateInterval.Day, StartDate, DateTime.Today));
        //                double x = startToToday / (double)totalDays * w + _coords[0];
        //                for (int i = 0; i < _coords.Count() - 1; i++)
        //                {
        //                    if (x > _coords[i] & x <= _coords[i + 1])
        //                    {
        //                        result = _coords[i];
        //                        break;
        //                    }
        //                }
        //            }
        //        }

        //        return result;
        //    }
        //}

        public void PerformLayout(Graphics canvas)
        {
            try
            {
                int _count = Count;
                if (!LayoutCompleted & _count > 0)
                {
                    float cellWidth = (float)(ClientAreaRectangle.Width / (double)_count);
                    float haldWidth = cellWidth / 2;
                    if (_coords == null)
                    {
                        _coords = new int[_count];
                    }
                    else if (_coords.Length != _count)
                    {
                        _coords = new int[_count];
                    }

                    float x1 = Left;
                    for (int i = 0; i < _count; i++)
                    {
                        _coords[i] = (int)(x1 + haldWidth);
                        x1 += cellWidth;
                    }

                    int y1 = Top;
                    {
                        var withBlock = _labelRow1;
                        withBlock.AreaMode = AreaMode;
                        withBlock.Left = Left;
                        withBlock.Top = y1;
                        withBlock.Coords = _coords;
                        withBlock.PerformLayout(canvas);
                        y1 += withBlock.Height;
                    }

                    {
                        var withBlock1 = _labelRow2;
                        withBlock1.AreaMode = AreaMode;
                        withBlock1.Left = Left;
                        withBlock1.Top = y1;
                        withBlock1.Coords = _coords;
                        withBlock1.PerformLayout(canvas);
                    }

                    LayoutCompleted = true;
                }
            }
            catch (Exception)
            {
                //Interaction.MsgBox(ex.Message, MsgBoxStyle.Critical);
            }
        }

        public override void Draw(Graphics canvas)
        {
            if (Count > 0)
            {
                if (LayoutCompleted == false)
                {
                    PerformLayout(canvas);
                }

                int y1 = Top - 2;
                int y2 = Top + 2;

                using (var linePen = new Pen(AxisColor))
                {
                    if (Count > 1)
                    {
                        if (_coords[1] - _coords[0] > 2)
                        {
                            foreach (var item in _coords)
                                canvas.DrawLine(linePen, item, y1, item, y2);
                        }
                    }

                    var visibleMarksPositin = _labelRow1.GetVisibleMarkPosition();
                    y1 -= 2;
                    y2 += 2;
                    foreach (var item in visibleMarksPositin)
                        canvas.DrawLine(linePen, item, y1, item, y2);

                    canvas.ResetClip();
                    _labelRow1.Draw(canvas);
                    if (_labelRow2.HasValues)
                    {
                        _labelRow2.Draw(canvas);
                        visibleMarksPositin = _labelRow2.GetVisibleMarkPosition();
                        y1 = _labelRow1.Top;
                        y2 = y1 + Height;
                        int dx = _coords[0] - Left;
                        for (int i = 0; i < visibleMarksPositin.Count(); i++)
                        {
                            int x1 = visibleMarksPositin[i] - dx;
                            if (i == 0)
                            {
                                canvas.DrawLine(linePen, x1, Top, x1, y2);
                            }
                            else
                            {
                                canvas.DrawLine(linePen, x1, y1, x1, y2);
                            }
                        }
                    }
                }

                if (AxisLineVisible == true)
                {
                    using (var axisPen = new Pen(AxisColor))
                    {
                        axisPen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
                        canvas.DrawLine(axisPen, Left, Top, Left + Width, Top);
                    }
                }
            }
        }

        public override bool HitTest(Point location, ref int hitPeriodIndex, ref string hitInfo)
        {
            base.HitTest(location, ref hitPeriodIndex, ref hitInfo);
            hitPeriodIndex = -1;
            hitInfo = string.Empty;
            if (_coords != null)
            {
                if (location.Y >= Top & location.Y <= Top + Height)
                {
                    int hw = (int)(CellWidth / (double)2);
                    for (int i = 0; i < _coords.Count(); i++)
                    {
                        if (location.X >= _coords[i] - hw & location.X <= _coords[i] + hw)
                        {
                            hitPeriodIndex = i;
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private void Clear()
        {
            _labelRow1.Clear();
            _labelRow2.Clear();
        }

        public DateTime GetStartDate(int index)
        {
            if (index >= 0 & index < _values.Count)
            {
                return _values[index].StartDate;
            }
            else
            {
                return DateTime.MinValue;
            }
        }

        public DateTime GetEndDate(int index)
        {
            if (index >= 0 & index < _values.Count)
            {
                return _values[index].EndDate;
            }
            else
            {
                return DateTime.MinValue;
            }
        }

        public string GetPeriodName(int index)
        {
            return _labelRow1.GetTextAt(index);
        }

        public bool IsLastPeriodCurrentPeriod()
        {
            var result = default(bool);
            if (_values.Count > 0)
            {
                if (DateTime.Today >= _values[_values.Count - 1].StartDate)
                {
                    if (DateTime.Today <= _values[_values.Count - 1].EndDate)
                    {
                        result = true;
                    }
                }
            }

            return result;
        }
    }
}