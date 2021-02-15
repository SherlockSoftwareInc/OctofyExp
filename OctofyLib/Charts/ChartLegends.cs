using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OctofyLib
{
    /// <summary>
    /// Chart legends 
    /// </summary>
    internal class ChartLegends : ChartElementBase
    {
        /// <summary>
        /// Enumeration to sepcify the orientation for layout the legends on the chart
        /// </summary>
        public enum Orientations
        {
            Horizontal,
            Vertical
        }

        private readonly List<LegendItem> _legendItems = new List<LegendItem>();

        private int _minItemWidth;
        private int _minItemHeight;
        private int _rows;

        public bool AutoWidth { get; set; }

        public int Spacing { get; set; } = 4;

        public Orientations Orientation { get; set; } = Orientations.Horizontal;

        public bool Visible { get; set; } = true;

        public int HitSectionIndex { get; private set; }

        public void AddItem(LegendItem item)
        {
            _legendItems.Add(item);
        }

        public void Clear()
        {
            _legendItems.Clear();
        }

        public override bool HitTest(Point location, ref int hitPeriodIndex, ref string hitInfo)
        {
            hitPeriodIndex = -1;
            hitInfo = string.Empty;
            if (_legendItems.Count > 0)
            {
                for (int i = 0; i < _legendItems.Count; i++)
                {
                    if (_legendItems[i].IsHit(location))
                    {
                        HitSectionIndex = i;
                        hitPeriodIndex = i;
                        hitInfo = _legendItems[i].Text;
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Layout items
        /// </summary>
        /// <param name="canvas"></param>
        /// <remarks>
        /// Before layout items, Width parameter is required
        /// </remarks>
        public void PerformLayout(Graphics canvas)
        {
            _rows = 1;
            _minItemHeight = 0;
            _minItemWidth = 0;

            if (_legendItems.Count == 0)
            {
                return;
            }

            foreach (LegendItem item in _legendItems)
                item.Font = base.Font;

            // Calculate minimum size required for legend item
            for (int i = 0; i < _legendItems.Count; i++)
            {
                var s = _legendItems[i].RequiredSize(canvas);
                if (s.Width > _minItemWidth)
                {
                    _minItemWidth = s.Width;
                }

                if (s.Height > _minItemHeight)
                {
                    _minItemHeight = s.Height;
                }
            }

            if (Orientation == Orientations.Horizontal)
            {
                if (base.Width > 0)
                {
                    // Calculate items per row
                    int itemPerRow = 1;
                    int w = _minItemWidth;
                    int dx = w + Spacing;
                    while (true)
                    {
                        if (w + dx > base.Width)
                        {
                            break;
                        }
                        else
                        {
                            itemPerRow += 1;
                            w += dx;
                        }
                    }

                    // Calculate total rows required
                    _rows = Convert.ToInt32(Math.Ceiling(_legendItems.Count / (double)itemPerRow));
                    if (itemPerRow > _legendItems.Count)
                    {
                        itemPerRow = _legendItems.Count;
                    }
                    else
                    {
                        itemPerRow = Convert.ToInt32(Math.Ceiling(_legendItems.Count / (double)_rows));
                    }

                    if (AutoWidth)
                    {
                        Width = itemPerRow * (_minItemWidth + Spacing) + Padding.Left + Padding.Right + 4;
                    }

                    int y = Padding.Top;
                    int dy = _minItemHeight + 3;
                    if (itemPerRow == 1)  // one item per row
                    {
                        int x = (int)((base.Width - _minItemWidth) / 2.0F);
                        for (int i = 0; i < _legendItems.Count; i++)
                        {
                            _legendItems[i].Location = new Point(x, y);
                            y += dy;
                        }

                        base.Height = _legendItems.Count * dy;
                    }
                    else
                    {
                        // layout items from middle to two sides
                        var itemLeft = new int[itemPerRow];
                        if (itemPerRow / 2.0F == Math.Ceiling(itemPerRow / 2.0F))   // even number of item per row
                        {
                            int x = (int)(base.Width / 2 - _minItemWidth - Spacing / 2);
                            for (int i = itemPerRow / 2 - 1; i >= 0; i -= 1)
                            {
                                itemLeft[i] = x;
                                x -= dx;
                            }

                            x = (int)(base.Width / 2.0F + Spacing / 2.0F);

                            for (int i = itemPerRow / 2, loopTo2 = itemPerRow - 1; i <= loopTo2; i++)
                            {
                                itemLeft[i] = x;
                                x += dx;
                            }
                        }
                        else    // odd
                        {
                            int middleItemIdx = Convert.ToInt32(Math.Floor(itemPerRow / (double)2));
                            itemLeft[middleItemIdx] = (int)((base.Width - _minItemWidth) / (double)2);
                            int x = itemLeft[middleItemIdx] - dx;
                            for (int i = (int)Math.Floor(itemPerRow / 2.0F) - 1; i >= 0; i -= 1)
                            {
                                itemLeft[i] = x;
                                x -= dx;
                            }

                            x = itemLeft[middleItemIdx] + dx;
                            for (int i = (int)Math.Ceiling(itemPerRow / (double)2), loopTo3 = itemPerRow - 1; i <= loopTo3; i++)
                            {
                                itemLeft[i] = x;
                                x += dx;
                            }
                        }

                        // set location for each legend item
                        int j = 0;
                        for (int i = 0; i < _legendItems.Count; i++)
                        {
                            _legendItems[i].Location = new Point(itemLeft[j], y);
                            j += 1;
                            if (j >= itemPerRow)
                            {
                                j = 0;
                                y += dy;
                            }
                        }

                        base.Height = _rows * dy;
                    }
                }
            }
            else
            {
                int y = Padding.Top;
                int dy = _minItemHeight + 3;
                for (int i = 0; i < _legendItems.Count; i++)
                {
                    _legendItems[i].Location = new Point(3, y);
                    y += dy;
                }

                base.Width = _minItemWidth + 6;
                base.Height = _legendItems.Count * dy;
            }

            base.Height += Padding.Top + Padding.Bottom;
            for (int i = 0; i < _legendItems.Count; i++)
                _legendItems[i].Size = new Size(_minItemWidth, _minItemHeight);
        }

        public override void Draw(Graphics canvas)
        {
            if (Visible)
            {
                for (int i = 0; i < _legendItems.Count; i++)
                {
                    _legendItems[i].Offset = Location;
                    _legendItems[i].DrawLegend(canvas);
                }
            }
        }
    }
}