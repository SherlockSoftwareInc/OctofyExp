
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
namespace OctofyLib
{
    /// <summary>
    /// 
    /// </summary>
    public partial class StackedAreaChartControl : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler SelectedIndexChange;

        private AreaChart _chart;                   // area chart plot

        /// <summary>
        /// 
        /// </summary>
        public StackedAreaChartControl()
        {
            InitializeComponent();
            _chart = new AreaChart(Font) { PercentMode = true };
        }

        /// <summary>
        /// 
        /// </summary>
        public int SelectedXIndex { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int SelectedYIndex { get; set; }

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        public string Colors { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public void Clear(string msg, Color color)
        {
            if (_chart is object)
            {
                _chart.Clear(msg, color);
                Invalidate();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StackedBarControl_Load(object sender, EventArgs e)
        {
            PerformControlResize();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StackedBarControl_Paint(object sender, PaintEventArgs e)
        {
            Draw(e.Graphics);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StackedBarControl_ClientSizeChanged(object sender, EventArgs e)
        {
            PerformControlResize();
        }

        /// <summary>
        /// 
        /// </summary>
        private void PerformControlResize()
        {
            if (_chart is object)
            {
                _chart.Location = new Point(Padding.Left, Padding.Top);
                _chart.Size = new Size(ClientSize.Width - Padding.Left - Padding.Right, ClientSize.Height - Padding.Top - Padding.Bottom);
                Invalidate();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="canvas"></param>
        private void Draw(Graphics canvas)
        {
            canvas.Clear(BackColor);
            _chart.Draw(canvas);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="seriesNames"></param>
        /// <param name="values"></param>
        /// <param name="periods"></param>
        public void Open(List<string> seriesNames, decimal?[,] values, List<TimePeriod> periods)
        {
            _chart.Colors = Colors;
            _chart.Open(seriesNames, values, periods);
            Invalidate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="seriesNames"></param>
        /// <param name="values"></param>
        /// <param name="categories"></param>
        public void Open(List<string> seriesNames, decimal?[,] values, List<string> categories)
        {
            _chart.Colors = Colors;
            _chart.Open(seriesNames, values, categories);
            Invalidate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StackedBarControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (_chart is object)
            {
                var hitPeriodIndex = default(int);
                string hitInfo = string.Empty;
                if (_chart.HitTest(e.Location, ref hitPeriodIndex, ref hitInfo))
                {
                    toolTip.SetToolTip(this, hitInfo);
                }
                else
                {
                    toolTip.RemoveAll();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StackedAreaChartControl_MouseClick(object sender, MouseEventArgs e)
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
    }
}