
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OctofyLib
{
    public partial class StackedColumnControl
    {
        public event EventHandler SelectedIndexChange;

        private readonly StackedColumnChart _chart;

        public StackedColumnControl()
        {

            // This call is required by the designer.
            InitializeComponent();

            // Add any initialization after the InitializeComponent() call.
            _chart = new StackedColumnChart(Font)
            {
                Location = new Point(0, 0),
                Colors = "",
                BorderStyle = BorderStyle.None
            };
        }

        public int SelectedXIndex { get; set; }
        public int SelectedYIndex { get; set; }

        public string Title
        {
            set
            {
                if (_chart is object)
                {
                    _chart.TitleText = value;
                }
            }
        }

        public string Subtitle
        {
            set
            {
                if (_chart is object)
                {
                    _chart.SubtitleText = value;
                }
            }
        }

        public void Clear(string msg, Color color)
        {
            if (_chart is object)
            {
                _chart.Clear(msg, color);
                Invalidate();
            }
        }

        private void StackedBarControl_Load(object sender, EventArgs e)
        {
            PerformControlResize();
        }

        private void StackedBarControl_Paint(object sender, PaintEventArgs e)
        {
            Draw(e.Graphics);
        }

        private void StackedBarControl_ClientSizeChanged(object sender, EventArgs e)
        {
            PerformControlResize();
        }

        private void PerformControlResize()
        {
            if (_chart is object)
            {
                _chart.Location = new Point(Padding.Left, Padding.Top);
                _chart.Size = new Size(ClientSize.Width - Padding.Left - Padding.Right, ClientSize.Height - Padding.Top - Padding.Bottom);
                Invalidate();
            }
        }

        private void Draw(Graphics canvas)
        {
            canvas.Clear(BackColor);
            _chart.Draw(canvas);
        }

        public string Colors { get; set; } = "";

        public void Open(List<string> seriesNames, decimal?[,] values, List<TimePeriod> periods)
        {
            _chart.Colors = Colors;
            _chart.Open(seriesNames, values, periods);
            Invalidate();
        }

        public void Open(List<string> seriesNames, decimal?[,] values, List<string> categories)
        {
            _chart.Open(seriesNames,
                        values,
                        categories);
            Invalidate();
        }

        private void StackedBarControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (_chart is object)
            {
                var hitPeriodIndex = default(int);
                string hitInfo = string.Empty;
                if (_chart.HitTest(e.Location, ref hitPeriodIndex, ref hitInfo))
                {
                    toolTip1.SetToolTip(this, hitInfo);
                }
                else
                {
                    toolTip1.RemoveAll();
                }
            }
        }

        private void StackedBarControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (_chart is object)
            {
                var hitPeriodIndex = default(int);
                string hitInfo = string.Empty;
                if (_chart.HitTest(e.Location, ref hitPeriodIndex, ref hitInfo))
                {
                    SelectedYIndex = _chart.SelectedYIndex;
                    SelectedXIndex = _chart.SelectedXIndex;
                    SelectedIndexChange?.Invoke(this, new EventArgs());
                }
            }
        }

        public Bitmap ToImage()
        {
            if (_chart is object)
            {
                return _chart.ToImage();
            }
            else
            {
                return null;
            }
        }
    }
}