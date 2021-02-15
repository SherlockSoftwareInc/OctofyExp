
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace OctofyLib
{
    public partial class LegendPanel
    {
        private readonly ChartLegends _legends;          // Chart legends for boxplot
        private readonly List<string> _items;
        private bool _sizeChanged = true;
        private readonly ColorSchema _colors;
        private bool _useTooltip;
        private string _tooltipText = "";

        public LegendPanel()
        {

            // This call is required by the designer.
            InitializeComponent();

            // Add any initialization after the InitializeComponent() call.
            _items = new List<string>();
            _legends = new ChartLegends()
            {
                Font = Font,
                Padding = new Padding(3, 3, 3, 6),
                AutoWidth = false,
                Orientation = ChartLegends.Orientations.Horizontal
            };
            _colors = new ColorSchema("");
            _legends.AddItem(new LegendItem("Legend", _colors.GetColorAt(0), Font, new Size(20, 8)));
        }

        /// <summary>
        /// Clear the control
        /// </summary>
        public void Clear()
        {
            _legends.Clear();
            _items.Clear();
        }

        /// <summary>
        /// Sets or gets color schema
        /// </summary>
        /// <returns></returns>
        public string Colors
        {
            get
            {
                return _colors.ToString();
            }

            set
            {
                if (value is object)
                {
                    _colors.Parse(value);
                    Populate();
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Open legends
        /// </summary>
        /// <param name="series"></param>
        public void Open(List<string> series)
        {
            Clear();

            if (series.Count > 256)
                throw new ArgumentOutOfRangeException("Number of legend item", "The maximum number of legend items is 256.");
            else
            {
                for (int i = 0; i < series.Count(); i++)
                {
                    string legendName = series[i].Replace("\r\n", " ");
                    legendName = legendName.Replace("\r", " ");
                    legendName = legendName.Replace("\n", " ");
                    _items.Add(legendName);
                }
                Populate();
            }
        }

        /// <summary>
        /// Populate legend items
        /// </summary>
        private void Populate()
        {
            _useTooltip = false;
            _legends.Clear();

            for (int i = 0; i < _items.Count; i++)
            {
                string seriesName = _items[i];
                if (seriesName.Length > 32)
                {
                    _useTooltip = true;
                }

                _legends.AddItem(new LegendItem(seriesName, _colors.GetColorAt((short)(i)), Font, new Size(20, 8)));
            }
            _sizeChanged = true;
            PerformAutoSize(CreateGraphics());
            Invalidate();
        }

        /// <summary>
        /// Control resize event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LegendPanel_ClientSizeChanged(object sender, EventArgs e)
        {
            _sizeChanged = true;
            Invalidate();
        }

        /// <summary>
        /// Control paint event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LegendPanel_Paint(object sender, PaintEventArgs e)
        {
            Draw(e.Graphics);
        }

        public void PerformAutoSize(Graphics canvas)
        {
            if (_legends is object)
            {
                {
                    var withBlock = _legends;
                    withBlock.Width = Width;
                    withBlock.PerformLayout(canvas);
                    if (withBlock.Height != Height)
                    {
                        Height = withBlock.Height;
                    }
                }
            }
        }

        private void Draw(Graphics canvas)
        {
            canvas.Clear(base.BackColor);
            if (_legends is object)
            {
                if (_sizeChanged)
                {
                    {
                        var withBlock = _legends;
                        withBlock.Top = 0;
                        withBlock.Left = 0;
                        withBlock.Width = Width;
                        //withBlock.PerformAutoSize((Graphics)canvas);
                        withBlock.PerformLayout((Graphics)canvas);
                        if (withBlock.Height != Height)
                        {
                            Height = withBlock.Height;
                        }
                    }

                    _sizeChanged = false;
                }

                _legends.Draw((Graphics)canvas);
            }
        }

        public Bitmap ToImage()
        {
            var chartImage = new Bitmap(Width, Height);
            var canvas = Graphics.FromImage(chartImage);
            Draw(canvas);
            return chartImage;
        }


        /// <summary>
        /// Control mouse move event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LegendPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (_useTooltip)
            {
                var index = default(int);
                string info = string.Empty;
                if (_legends.HitTest(e.Location, ref index, ref info))
                {
                    if ((info ?? "") != (_tooltipText ?? ""))
                    {
                        _tooltipText = info;
                        toolTip1.SetToolTip(this, info);
                    }
                }
            }
        }
    }
}