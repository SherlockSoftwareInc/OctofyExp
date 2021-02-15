
using System.Drawing;

namespace OctofyLib
{
    internal class ColumnCellItem : ChartElementBase
    {
        public Color BarColor { get; set; }
        public string SectionName { get; set; }
        public string Text { get; set; }
        public int? Value { get; set; }
        public short Depth3D { get; set; }
        public bool ShowNumber { get; set; }
        public bool Selected { get; set; }

        public override void Draw(Graphics canvas)
        {
            // MyBase.Draw(canvas)

            if (base.Width > 0 & base.Height > 0)
            {
                var rect = new Rectangle(base.X, base.Y, base.Width, base.Height);

                // If Me.Depth3D > 1 Then
                DrawRightSideShadow(canvas, rect.X + rect.Width, rect.Top, rect.Height);
                DrawBottomSideShadow(canvas, rect.Top + rect.Height, rect.X, rect.Width);
                // End If

                canvas.FillRectangle(SurfaceBrush(), rect);
                if (ShowNumber & Text.Length > 0)
                {
                    var oTextformat = new StringFormat();
                    oTextformat.Alignment = StringAlignment.Center;
                    oTextformat.LineAlignment = StringAlignment.Center;
                    canvas.DrawString(Text, base.Font, TextBrush(), rect, oTextformat);
                }
            }
        }

        private SolidBrush SurfaceBrush()
        {
            if (Selected)
            {
                return new SolidBrush(ColorUtil.CreateColorWithCorrectedLightness(BarColor, 0.15F));
            }
            else
            {
                return new SolidBrush(BarColor);
            }
        }

        private void DrawRightSideShadow(Graphics canvas, int lb, int top, int height)
        {
            var shadowColorR = ColorUtil.CreateColorWithCorrectedLightness(BarColor, -0.092F);
            var shadowPtR = new Point[5];
            int bb = top + height;
            shadowPtR[0] = new Point(lb, top);
            shadowPtR[1] = new Point(lb + Depth3D, top + Depth3D);
            shadowPtR[2] = new Point(lb + Depth3D, bb + Depth3D);
            shadowPtR[3] = new Point(lb, bb);
            shadowPtR[4] = new Point(lb, top);
            canvas.FillPolygon(new SolidBrush(shadowColorR), shadowPtR);
        }

        private void DrawBottomSideShadow(Graphics canvas, int tb, int left, int width)
        {
            var shadowColorR = ColorUtil.CreateColorWithCorrectedLightness(BarColor, -0.154F);
            var shadowPtR = new Point[5];
            int rb = left + width;
            shadowPtR[0] = new Point(left, tb);
            shadowPtR[1] = new Point(rb, tb);
            shadowPtR[2] = new Point(rb + Depth3D, tb + Depth3D);
            shadowPtR[3] = new Point(left + Depth3D, tb + Depth3D);
            shadowPtR[4] = new Point(left, tb);
            canvas.FillPolygon(new SolidBrush(shadowColorR), shadowPtR);
        }

        private SolidBrush TextBrush()
        {
            int r = (BarColor.R + 128) % 256;
            int g = (BarColor.G + 128) % 256;
            int b = (BarColor.B + 128) % 256;
            return new SolidBrush(Color.FromArgb(255, r, g, b));
        }

        public bool IsHit(Point location)
        {
            if (location.X > X & location.Y > Y & location.X < X + Width & location.Y < Y + Height)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}