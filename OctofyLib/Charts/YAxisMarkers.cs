using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace OctofyLib
{
    public class YAxisMarkers : ChartElementBase
    {
        public enum Alignments
        {
            Left,
            Right
        }

        public enum AxisValueTypes
        {
            Categories,
            Values,
            Percent
        }

        private AxisValueTypes _axisValueType;
        private List<ChartLabel> _markers = new List<ChartLabel>();
        private double _scaleMax;
        private double _scaleMin;
        private float _drawingRatio;
        private Alignments _alignment;
        private int[] _coords;
        private bool _autoWidth;
        private int _zeroPosition;
        private double _mininum;
        private double _maximum;

        public YAxisMarkers()
        {
        }

        public YAxisMarkers(Font font)
        {
            Font = font;
        }

        public Alignments Alignment
        {
            get
            {
                return _alignment;
            }

            set
            {
                _alignment = value;
                LayoutCompleted = false;
            }
        }

        public bool AutoWidth
        {
            get
            {
                return _autoWidth;
            }

            set
            {
                _autoWidth = value;
                LayoutCompleted = false;
            }
        }

        public AxisValueTypes AxisValueType
        {
            get
            {
                return _axisValueType;
            }

            set
            {
                _axisValueType = value;
                LayoutCompleted = false;
            }
        }

        public bool AxisVisible { get; set; } = true;

        public int BaselinePosition
        {
            get
            {
                var result = default(int);
                if (_coords != null)
                {
                    if (_coords.Count() > 0)
                    {
                        result = _coords[0];
                    }
                }

                return result;
            }
        }

        public double BaselineValue
        {
            get
            {
                var result = default(double);
                if (_markers != null)
                {
                    if (_axisValueType == AxisValueTypes.Values)
                    {
                        if (_markers.Count > 0)
                        {
                            result = _scaleMin * _drawingRatio;
                        }
                    }
                }

                return result;
            }
        }

        public BorderStyle BorderStyle { get; set; }

        public string[] Categories
        {
            set
            {
                _markers.Clear();
                if (value != null)
                {
                    for (int i = 0; i < value.Count(); i++)
                    {
                        var marker = new ChartLabel(Font);
                        if (value[i].Length == 0)
                        {
                            marker.Text = Properties.Resources.B003;    // "(Blank)";
                        }
                        else
                        {
                            marker.Text = value[i];
                        }

                        _markers.Add(marker);
                    }
                }

                LayoutCompleted = false;
            }
        }

        public int[] Coords
        {
            get
            {
                return _coords;
            }
        }

        public short Count
        {
            get
            {
                var result = default(short);
                if (_markers != null)
                {
                    result = (short)(_markers.Count);
                }

                return result;
            }
        }

        public float DrawingRatio
        {
            get
            {
                return _drawingRatio;
            }
        }

        public bool MarkersVisible { get; set; } = true;

        public double Minimum
        {
            get
            {
                return _mininum;
            }

            set
            {
                _mininum = value;
                LayoutCompleted = false;
            }
        }

        public double Maximum
        {
            get
            {
                return _maximum;
            }

            set
            {
                _maximum = value;
                LayoutCompleted = false;
            }
        }

        public double ScaleMax
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

        public double ScaleMin
        {
            get
            {
                return _scaleMin;
            }

            set
            {
                _scaleMin = value;
                LayoutCompleted = false;
            }
        }

        public short TargetSteps { get; set; }

        public int ZeroPosition
        {
            get
            {
                return _zeroPosition;
            }
        }

        public override void PerformAutoSize(Graphics canvas)
        {
            if (AutoWidth == true)
            {
                int fh = Font.Height;
                var switchExpr = AxisValueType;
                switch (switchExpr)
                {
                    case AxisValueTypes.Categories:
                        {
                            _scaleMax = _markers.Count;
                            _scaleMin = 0;
                            break;
                        }

                    case AxisValueTypes.Percent:
                        {
                            _scaleMax = 100;
                            _scaleMin = 0;
                            short targetSteps;
                            if (TargetSteps > 1)
                            {
                                targetSteps = TargetSteps;
                            }
                            else
                            {
                                targetSteps = (short)(Math.Floor(PlotHeight / (double)fh));
                            }

                            if (targetSteps > 10)
                            {
                                targetSteps = 10;
                            }

                            var m = BuildSteps(0, 100, targetSteps);
                            _markers.Clear();
                            if (m.Count > 0)
                            {
                                for (int i = 0; i < m.Count; i++)
                                {
                                    var marker = new ChartLabel(Font)
                                    {
                                        Text = m[i] + "%"
                                    };
                                    _markers.Add(marker);
                                }
                            }

                            break;
                        }

                    case AxisValueTypes.Values:
                        {
                            short targetSteps;
                            if (TargetSteps > 1)
                            {
                                targetSteps = TargetSteps;
                            }
                            else
                            {
                                targetSteps = (short)(Math.Floor(PlotHeight / (double)fh));
                            }

                            double smin = Minimum;
                            double smax = Maximum;
                            if (smin == smax)
                            {
                                smin = 0;
                                smax = 100;
                            }

                            var m = BuildSteps(smin, smax, targetSteps);
                            _markers.Clear();
                            if (m.Count > 1)
                            {
                                for (int i = 0; i < m.Count; i++)
                                {
                                    var marker = new ChartLabel(Font);
                                    marker.Text = m[i];
                                    _markers.Add(marker);
                                }

                                _scaleMin = MarkerValue(m[0]);
                                _scaleMax = MarkerValue(m[m.Count - 1]);
                            }

                            break;
                        }
                }

                if (_markers.Count > 0 & _scaleMax != _scaleMin)
                {
                    int maxWidth = 0;
                    for (int i = 0; i < _markers.Count; i++)
                    {
                        _markers[i].Font = Font;
                        _markers[i].ForeColor = ForeColor;
                        _markers[i].AutoSizeMode = ChartLabel.AutoSizeModes.byBoth;
                        _markers[i].PerformAutoSize(canvas);
                        if (_markers[i].Width > maxWidth)
                        {
                            maxWidth = _markers[i].Width;
                        }
                    }

                    Width = maxWidth + Padding.Right + Padding.Left + 3;
                }
            }
        }

        public void PerformLayout(Graphics canvas)
        {
            if (LayoutCompleted == false)
            {
                _zeroPosition = -1;
                int fh = Font.Height;
                int hfh = (int)(fh / (double)2);
                var switchExpr = AxisValueType;
                switch (switchExpr)
                {
                    case AxisValueTypes.Categories:
                        {
                            _scaleMax = _markers.Count;
                            _scaleMin = 0;
                            break;
                        }

                    case AxisValueTypes.Percent:
                        {
                            _scaleMax = 100;
                            _scaleMin = 0;
                            short targetSteps;
                            if (TargetSteps > 1)
                            {
                                targetSteps = TargetSteps;
                            }
                            else
                            {
                                targetSteps = (short)(Math.Floor(PlotHeight / (double)fh));
                            }

                            if (targetSteps > 10)
                            {
                                targetSteps = 10;
                            }

                            var m = BuildSteps(0, 100, targetSteps);
                            _markers.Clear();
                            if (m.Count > 0)
                            {
                                for (int i = 0; i < m.Count; i++)
                                {
                                    var marker = new ChartLabel(Font)
                                    {
                                        Text = m[i] + "%"
                                    };
                                    _markers.Add(marker);
                                }
                            }

                            break;
                        }

                    case AxisValueTypes.Values:
                        {
                            short targetSteps;
                            if (TargetSteps > 1)
                            {
                                targetSteps = TargetSteps;
                            }
                            else
                            {
                                targetSteps = (short)(Math.Floor(PlotHeight / (double)fh));
                            }

                            double smin = Minimum;
                            double smax = Maximum;
                            if (smin == smax)
                            {
                                smin = 0;
                                smax = 100;
                            }

                            var m = BuildSteps(smin, smax, targetSteps);
                            _markers.Clear();
                            if (m.Count > 1)
                            {
                                for (int i = 0; i < m.Count; i++)
                                {
                                    var marker = new ChartLabel(Font);
                                    marker.Text = m[i];
                                    _markers.Add(marker);
                                }

                                _scaleMin = MarkerValue(m[0]);
                                _scaleMax = MarkerValue(m[m.Count - 1]);
                            }

                            break;
                        }
                }

                if (_markers.Count > 0 & _scaleMax != _scaleMin)
                {
                    int maxWidth = 0;
                    for (int i = 0; i < _markers.Count; i++)
                    {
                        _markers[i].Font = Font;
                        _markers[i].ForeColor = ForeColor;
                        _markers[i].AutoSizeMode = ChartLabel.AutoSizeModes.byBoth;
                        _markers[i].PerformAutoSize(canvas);
                        if (_markers[i].Width > maxWidth)
                        {
                            maxWidth = _markers[i].Width;
                        }
                    }

                    fh = _markers[0].Height;
                    hfh = (int)(fh / (double)2);
                    if (_autoWidth)
                    {
                        Width = maxWidth + Padding.Right + Padding.Left + 3;
                    }

                    _coords = new int[_markers.Count];
                    int bb = Top + Height - Padding.Bottom;
                    if (AxisValueType == AxisValueTypes.Categories)
                    {
                        if (_markers.Count > 1)
                        {
                            _drawingRatio = (float)(PlotHeight / (double)(_markers.Count + 1));
                            for (int i = 0; i < _markers.Count; i++)
                            {
                                _coords[i] = (int)(bb - _drawingRatio * (i + 1));
                                _markers[i].Top = _coords[i] - hfh;
                                if (Alignment == Alignments.Left)
                                {
                                    _markers[i].Left = Left + Padding.Left + 2;
                                }
                                else
                                {
                                    _markers[i].Left = Left + Width - Padding.Right - _markers[i].Width - 6;
                                }

                                _zeroPosition = bb;
                            }

                            int y = _markers[0].Top;
                            for (int i = 0; i < _markers.Count; i++)
                            {
                                if (_markers[i].Top + _markers[i].Height > y)
                                {
                                    _markers[i].Visible = false;
                                }
                                else
                                {
                                    _markers[i].Visible = true;
                                    y = _markers[i].Top;
                                }
                            }
                        }
                        else if (_markers.Count == 1)
                        {
                            _drawingRatio = 0;
                            _markers[0].Top = bb - hfh;
                            _coords[0] = bb;
                        }
                        else
                        {
                            _drawingRatio = 0;
                        }
                    }
                    else
                    {
                        if (_scaleMax == _scaleMin)
                        {
                            _drawingRatio = 0;
                        }
                        else
                        {
                            _drawingRatio = (float)(PlotHeight / (_scaleMax - _scaleMin));
                        }

                        double baseline = _drawingRatio * _scaleMin;
                        for (int i = 0; i < _markers.Count; i++)
                        {
                            if (i == 0)
                            {
                                hfh = (int)(_markers[0].Height / (double)2);
                            }

                            double v = MarkerValue(_markers[i].Text);
                            _coords[i] = (int)(bb - _drawingRatio * v + baseline);
                            _markers[i].Top = _coords[i] - hfh;
                            if (Alignment == Alignments.Left)
                            {
                                _markers[i].Left = Left + Padding.Left + 2;
                            }
                            else
                            {
                                _markers[i].Left = Left + Width - Padding.Right - _markers[i].Width - 6;
                            }

                            if (v == 0)
                            {
                                _zeroPosition = _coords[i];
                            }
                        }
                    }
                }

                LayoutCompleted = true;
            }
        }

        private double MarkerValue(string text)
        {
            if (text.IndexOf(",") >= 0)
            {
                return double.Parse((text.Replace(",", "")));
            }
            else
            {
                return double.Parse(text);
            }
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

        public override void Draw(Graphics canvas)
        {
            PerformLayout(canvas);
            if (_markers != null)
            {
                foreach (var marker in _markers)
                    marker.Draw(canvas);
            }

            if (_markers != null)
            {
                if (_markers.Count > 0)
                {
                    if (_coords != null)
                    {
                        int x1;
                        int x2;
                        if (Alignment == Alignments.Left)
                        {
                            x1 = Padding.Left + Left - 3;
                            x2 = x1 + 6;
                        }
                        else
                        {
                            x2 = Left + Width - Padding.Right + 3;
                            x1 = x2 - 6;
                        }

                        for (int i = 0; i < _coords.Count(); i++)
                        {
                            using (var p = new Pen(ForeColor))
                            {
                                canvas.DrawLine(p, x1, _coords[i], x2, _coords[i]);
                            }
                        }
                    }
                }
            }
            // If Me.BorderStyle <> BorderStyle.None Then
            int x;
            if (Alignment == Alignments.Left)
            {
                x = Left + Padding.Left;
            }
            else
            {
                x = Left + Width - Padding.Right;
            }

            using (var p = new Pen(ForeColor))
            {
                if (_axisValueType == AxisValueTypes.Values)
                {
                    p.StartCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
                    canvas.DrawLine(p, x, Top - 6, x, Top + Height);
                }
                else
                {
                    canvas.DrawLine(p, x, Top, x, Top + Height);
                }
            }
            // End If
        }

        public override bool HitTest(Point location, ref int hitPeriodIndex, ref string hitInfo)
        {
            base.HitTest(location, ref hitPeriodIndex, ref hitInfo);
            if (_coords != null)
            {
                if (_coords.Count() > 0)
                {
                    if (location.X >= Left & location.X <= Left + Width)
                    {
                        if (location.Y >= Top & location.Y <= Top + Height)
                        {
                            if (_coords.Count() == 1)
                            {
                                hitPeriodIndex = 0;
                                hitInfo = _markers[0].Text;
                                return true;
                            }
                            else
                            {
                                int dy = (int)(Math.Abs(_coords[1] - _coords[0]) / (double)2);
                                for (int i = 0; i < _coords.Count(); i++)
                                {
                                    if (location.Y <= _coords[i] + dy & location.Y >= _coords[i] - dy)
                                    {
                                        hitPeriodIndex = i;
                                        hitInfo = _markers[i].Text;
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }
    }
}