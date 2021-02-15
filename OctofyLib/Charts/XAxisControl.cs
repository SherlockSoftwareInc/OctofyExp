using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace OctofyLib
{
    public partial class XAxisControl : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler DrawingRatioChanged;

        private long _scaleMax;
        private float _drawingRatio;
        private int[] _coords;
        private long _maximum = 100;
        private readonly LabelsRow _labelRow1;
        private int _count;
        private int _rightSpace = 12;
        private int _zeroPosition;
        private bool _layoutCompleted;

        /// <summary>
        /// 
        /// </summary>
        public XAxisControl()
        {
            InitializeComponent();

            _labelRow1 = new LabelsRow()
            {
                Padding = new Padding(0, 0, 0, 0),
                HideOverlap = true,
                RotateAngle = 315,
                ForeColor = ForeColor
            };
        }

        /// <summary>
        /// Gets mark positions
        /// </summary>
        /// <returns></returns>
        public List<int> MarkPositions
        {
            get
            {
                if (_coords is object)
                {
                    return _coords.ToList();
                }
                else
                {
                    return new List<int>();
                }
            }
        }

        /// <summary>
        /// Gets drawing ratio
        /// </summary>
        /// <returns></returns>
        public float DrawingRatio
        {
            get
            {
                return _drawingRatio;
            }
        }

        /// <summary>
        /// Sets maximum value for axis.
        /// </summary>
        public long Maximum
        {
            set
            {
                if (_maximum != value)
                {
                    _maximum = value;
                    if (_labelRow1 is object)
                    {
                        _layoutCompleted = false;
                        DoPerformLayout(CreateGraphics());
                        Invalidate();
                    }
                }
            }
        }

        /// <summary>
        /// Sets a position where the zero value located
        /// </summary>
        public int ZeroPosition
        {
            set
            {
                if (_zeroPosition != value)
                {
                    _zeroPosition = value;
                    _layoutCompleted = false;
                    if (_labelRow1 is object)
                    {
                        Invalidate();
                    }
                }
            }
        }

        /// <summary>
        /// Perform step calculation and set the height
        /// of the control automatically
        /// </summary>
        /// <param name="canvas"></param>
        public void PerformAutoSize(Graphics canvas)
        {
            if (_labelRow1 == null)
            {
                return;
            }

            _labelRow1.Clear();
            var txtSize = canvas.MeasureString(MarkText(_scaleMax), base.Font);
            _rightSpace = (int)(txtSize.Width + 24);
            int targetSteps = (int)((Width - _zeroPosition) / (txtSize.Width * 2));
            int smin = 0;
            long smax = _maximum;
            if (smin == smax)
            {
                smin = 0;
                smax = 100;
            }

            var m = BuildSteps(smin, smax, (short)(targetSteps));
            _count = m.Count;
            if (m.Count > 1)
            {
                var line1 = new string[m.Count];
                for (int i = 0; i < m.Count; i++)
                    line1[i] = m[i].ToString();

                _labelRow1.Values = line1;
                _scaleMax = long.Parse(m[m.Count - 1].Replace(",", ""));
                _labelRow1.Font = Font;
                _labelRow1.Left = _zeroPosition;
                _labelRow1.Width = AxisWidth();
                _labelRow1.PerformAutosize(canvas, AxisWidth());
            }
            else
            {
                var line1 = new string[1];
                line1[0] = "0";
                _labelRow1.Values = line1;
                _scaleMax = 0;
                _labelRow1.Font = Font;
                _labelRow1.Left = _zeroPosition;
                _labelRow1.Width = AxisWidth();
                _labelRow1.PerformAutosize(canvas, AxisWidth());
            }

            if (_labelRow1.Height > 1)
            {
                if (Height != _labelRow1.Height + 6)
                {
                    Height = _labelRow1.Height + 6;
                }
            }
        }

        /// <summary>
        /// Gets width for axis
        /// </summary>
        /// <returns></returns>
        private int AxisWidth()
        {
            return Width - _zeroPosition - _rightSpace;
        }

        /// <summary>
        /// Perform layout for each elements in the control
        /// </summary>
        /// <param name="canvas"></param>
        public void DoPerformLayout(Graphics canvas)
        {
            if (_labelRow1 == null)
            {
                return;
            }

            if (_layoutCompleted == false)
            {
                PerformAutoSize(canvas);
                if (_count == 0)
                {
                    return;
                }

                float cellWidth;
                if (_count == 1)
                {
                    cellWidth = AxisWidth();
                }
                else
                {
                    cellWidth = (float)(AxisWidth() / (double)(_count - 1));
                }

                float dr;
                if (_scaleMax > 0)
                {
                    dr = (float)(AxisWidth() / (double)_scaleMax);
                }
                else
                {
                    dr = 0;
                }

                if (_coords == null)
                {
                    _coords = new int[_count];
                }
                else if (_coords.Length != _count)
                {
                    _coords = new int[_count];
                }

                float x1 = _zeroPosition;
                for (int i = 0; i < _count; i++)
                {
                    _coords[i] = Convert.ToInt32(x1); // + haldWidth
                    x1 += cellWidth;
                }

                {
                    var withBlock = _labelRow1;
                    withBlock.AreaMode = false;
                    withBlock.Left = _zeroPosition;
                    withBlock.Top = 6;
                    withBlock.Coords = _coords;
                    withBlock.PerformLayout(canvas);
                }

                _layoutCompleted = true;
                if (dr != _drawingRatio)
                {
                    _drawingRatio = dr;
                    DrawingRatioChanged?.Invoke(this, new EventArgs());
                }
            }
        }

        /// <summary>
        /// Calculate the steps for marks of axis
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <param name="targetSteps"></param>
        /// <returns></returns>
        private List<string> BuildSteps(long value1, long value2, short targetSteps)
        {
            var result = new List<string>();
            if (targetSteps > 21)
            {
                targetSteps = 21;
            }

            long vMax = value2;
            long vMin = value1;
            if (vMin > vMax)
            {
                vMax = value1;
                vMin = value2;
            }

            if (vMin == 0 | vMax == 0)
            {
                targetSteps -= 1;
            }

            long total = vMax - vMin;
            if (vMin < 0 & vMax < 0 | vMin > 0 & vMax > 0)
            {
                targetSteps -= 1;
            }

            long s = (long)(CalcStepSize(total, targetSteps));
            if (s != 0)
            {
                string fmt = "N0";
                if (s < 1)
                {
                    string tmp = s.ToString();
                    fmt = string.Format("N{0}", tmp.Length - tmp.LastIndexOf(".") - 1);
                }

                long startValue = 0;
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

                long v = startValue;
                while (v < vMax + s)
                {
                    result.Add(v.ToString(fmt));
                    v += s;
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="range"></param>
        /// <param name="targetSteps"></param>
        /// <returns></returns>
        private double CalcStepSize(double range, int targetSteps)
        {
            if (range != 0 & targetSteps > 0)
            {
                // calculate an initial guess at step size
                if (targetSteps > range)
                {
                    targetSteps = Convert.ToInt32(range);
                }

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

        /// <summary>
        /// Draw control
        /// </summary>
        /// <param name="canvas"></param>
        public void Draw(Graphics canvas)
        {
            canvas.Clear(base.BackColor);
            if (_labelRow1 is object)
            {
                DoPerformLayout(canvas);
                using (var linePen = new Pen(ForeColor))
                {
                    var visibleMarksPositin = _labelRow1.GetVisibleMarkPosition();
                    int y1 = 3;
                    int y2 = 9;
                    foreach (var item in visibleMarksPositin)
                        canvas.DrawLine(linePen, item, y1, item, y2);
                }

                _labelRow1.Draw(canvas);
                using (var p = new Pen(ForeColor))
                {
                    p.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
                    canvas.DrawLine(p, _zeroPosition, 6, _zeroPosition + AxisWidth() + 12, 6);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Bitmap ToImage()
        {
            var chartImage = new Bitmap(Width, Height);
            var canvas = Graphics.FromImage(chartImage);
            Draw(canvas);
            return chartImage;
        }

        /// <summary>
        /// Format the value to be displyed of the mark
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string MarkText(long value)
        {
            return value.ToString("N0");
        }

        /// <summary>
        /// Handler control paint event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XAxisControl_Paint(object sender, PaintEventArgs e)
        {
            Draw(e.Graphics);
        }

        /// <summary>
        /// Handler control resize event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XAxisControl_Resize(object sender, EventArgs e)
        {
            _layoutCompleted = false;
            Invalidate();
        }
    }
}