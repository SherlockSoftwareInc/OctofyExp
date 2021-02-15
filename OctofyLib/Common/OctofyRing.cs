using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace OctofyLib
{
    public partial class OctofyRing : UserControl
    {
        private int[] _values = new int[] { 48, 9, 21, 18, 16, 49 };
        private int[] _ringValues;
        private bool _calculated;
        private readonly ColorSchema _colors;
        private Color[] _ringColors;
        private string _saveFolderName = "";    //E:\\Octofy_Logo
        private int _imageIndex;
        private float _initialAngle;
        private float _da = 20.0F;
        private short _alpha;
        private readonly Point[] _pts = new Point[] { new Point(7, 71), new Point(9, 71), new Point(25, 73), new Point(36, 72), new Point(44, 71), new Point(49, 70), new Point(54, 69), new Point(57, 68), new Point(59, 67), new Point(62, 66), new Point(65, 65), new Point(68, 64), new Point(71, 63), new Point(73, 62), new Point(76, 61), new Point(79, 60), new Point(82, 59), new Point(84, 58), new Point(87, 57), new Point(89, 56), new Point(92, 55), new Point(95, 54), new Point(98, 53), new Point(101, 52), new Point(104, 51), new Point(108, 50), new Point(111, 49), new Point(115, 48), new Point(119, 47), new Point(124, 46), new Point(130, 45), new Point(138, 44), new Point(143, 43), new Point(171, 43), new Point(175, 44), new Point(180, 45), new Point(184, 46), new Point(188, 47), new Point(191, 48), new Point(194, 49), new Point(196, 50), new Point(198, 51), new Point(200, 52), new Point(202, 53), new Point(204, 54), new Point(206, 55), new Point(208, 56), new Point(209, 57), new Point(211, 58), new Point(212, 59), new Point(214, 60), new Point(215, 61), new Point(216, 62), new Point(218, 63), new Point(219, 64), new Point(220, 65), new Point(221, 66), new Point(222, 67), new Point(223, 68), new Point(224, 69), new Point(225, 70), new Point(226, 71), new Point(227, 72), new Point(228, 73), new Point(229, 74), new Point(230, 75), new Point(231, 76), new Point(231, 77), new Point(232, 78), new Point(233, 79), new Point(233, 80), new Point(234, 81), new Point(234, 82), new Point(235, 83), new Point(236, 84), new Point(236, 85), new Point(237, 86), new Point(237, 87), new Point(238, 88), new Point(238, 89), new Point(239, 90), new Point(239, 91), new Point(240, 92), new Point(240, 93), new Point(241, 94), new Point(241, 95), new Point(242, 96), new Point(242, 97), new Point(243, 98), new Point(243, 99), new Point(243, 100), new Point(244, 101), new Point(244, 102), new Point(244, 103), new Point(245, 104), new Point(245, 105), new Point(245, 106), new Point(246, 107), new Point(246, 108), new Point(246, 109), new Point(246, 110), new Point(247, 111), new Point(247, 112), new Point(247, 113), new Point(247, 114), new Point(247, 115), new Point(247, 116), new Point(248, 117), new Point(248, 118), new Point(248, 119), new Point(248, 120), new Point(248, 121), new Point(248, 122), new Point(248, 123), new Point(248, 124), new Point(248, 125), new Point(248, 126), new Point(248, 127), new Point(248, 128), new Point(248, 129), new Point(248, 130), new Point(248, 131), new Point(248, 132), new Point(248, 133), new Point(248, 134), new Point(248, 135), new Point(248, 136), new Point(248, 137), new Point(248, 138), new Point(248, 139), new Point(248, 140), new Point(248, 141), new Point(247, 142), new Point(247, 143), new Point(247, 144), new Point(247, 145), new Point(247, 146), new Point(247, 147), new Point(246, 148), new Point(246, 149), new Point(246, 149), new Point(246, 148), new Point(246, 147), new Point(246, 146), new Point(246, 145), new Point(246, 144), new Point(246, 143), new Point(246, 142), new Point(246, 141), new Point(246, 140), new Point(246, 139), new Point(246, 138), new Point(246, 137), new Point(246, 136), new Point(246, 135), new Point(245, 134), new Point(245, 133), new Point(245, 132), new Point(245, 131), new Point(245, 130), new Point(245, 129), new Point(244, 128), new Point(244, 127), new Point(244, 126), new Point(244, 125), new Point(243, 124), new Point(243, 123), new Point(243, 122), new Point(242, 121), new Point(242, 120), new Point(242, 119), new Point(241, 118), new Point(241, 117), new Point(240, 116), new Point(240, 115), new Point(239, 114), new Point(239, 113), new Point(238, 112), new Point(238, 111), new Point(237, 110), new Point(236, 109), new Point(236, 108), new Point(235, 107), new Point(234, 106), new Point(234, 105), new Point(233, 104), new Point(232, 103), new Point(231, 102), new Point(231, 101), new Point(230, 100), new Point(229, 99), new Point(228, 98), new Point(227, 97), new Point(226, 96), new Point(225, 95), new Point(223, 94), new Point(222, 93), new Point(221, 92), new Point(220, 91), new Point(219, 90), new Point(217, 89), new Point(216, 88), new Point(215, 87), new Point(213, 86), new Point(211, 85), new Point(210, 84), new Point(208, 83), new Point(206, 82), new Point(204, 81), new Point(202, 80), new Point(199, 79), new Point(197, 78), new Point(194, 77), new Point(190, 76), new Point(187, 75), new Point(183, 74), new Point(178, 73), new Point(173, 72), new Point(169, 71), new Point(162, 70), new Point(152, 71), new Point(146, 72), new Point(137, 73), new Point(131, 74), new Point(125, 75), new Point(119, 76), new Point(113, 77), new Point(107, 78), new Point(100, 79), new Point(94, 80), new Point(87, 81), new Point(78, 82), new Point(66, 83), new Point(54, 83), new Point(44, 82), new Point(37, 81), new Point(32, 80), new Point(27, 79), new Point(23, 78), new Point(20, 77), new Point(16, 76), new Point(14, 75), new Point(11, 74), new Point(8, 73), new Point(6, 72), new Point(7, 71) };
        private readonly string[] _schemas = new string[] { "#A9A57C, #9CBEBD, #D2CB6C, #95A39D, #C89F5D, #B1A089", "#797B7E, #F96A1B, #08A1D9, #7C984A, #C2AD8D, #506E94", "#CEB966, #9CB084, #6BB1C9, #6585CF, #7E6BC9, #A379BB", "#93A299, #CF543F, #B5AE53, #848058, #E8B54D, #786C71", "#F07F09, #9F2936, #1B587C, #4E8542, #604878, #C19859", "#94C600, #71685A, #FF6700, #909465, #956B43, #FEA022", "#6F6F74, #A7B789, #BEAE98, #92A9B9, #9C8265, #8D6974", "#D16349, #CCB400, #8CADAE, #8C7B70, #8FB08C, #D19049", "#93A299, #AD8F67, #726056, #4C5A6A, #808DA0, #79463D", "#98C723, #59B0B9, #DEAE00, #B77BB4, #E0773C, #A98D63", "#2DA2BF, #DA1F28, #EB641B, #39639D, #474B78, #7D3C4A", "#9E8E5C, #A09781, #85776D, #AEAFA9, #8D878B, #6B6149", "#629DD1, #297FD5, #7F8FA9, #4A66AC, #5AA2AE, #9D90A0", "#D34817, #9B2D1F, #A28E6A, #956251, #918485, #855D5D", "#7A7A7A, #F5C201, #526DB0, #989AAC, #DC5924, #B4B392", "#6076B4, #9C5252, #E68422, #846648, #63891F, #758085", "#0F6FC6, #009DD9, #0BD0D9, #10CF9B, #7CCA62, #A5C249", "#72A376, #B0CCB0, #A8CDD7, #C0BEAF, #CEC597, #E8B7B7", "#C66951, #BF974D, #928B70, #87706B, #94734E, #6F777D", "#873624, #D6862D, #D0BE40, #877F6C, #972109, #AEB795", "#7E97AD, #CC8E60, #7A6A60, #B4936D, #67787B, #9D936F", "#94B6D2, #DD8047, #A5AB81, #D8B25C, #7BA79D, #968C8C", "#7FD13B, #EA157A, #FEB80A, #00ADDC, #738AC8, #1AB39F", "#F0AD00, #60B5CC, #E66C7D, #6BB76D, #E88651, #C64847", "#AD0101, #726056, #AC956E, #808DA9, #424E5B, #730E00", "#B83D68, #AC66BB, #DE6C36, #F9B639, #CF6DA4, #FA8D3D", "#FE8637, #7598D9, #B32C16, #F5CD2D, #AEBAD5, #777C84", "#727CA3, #9FB8CD, #D2DA7A, #FADA7A, #B88472, #8E736A", "#A5B592, #F3A447, #E7BC29, #D092A7, #9C85C0, #809EC2", "#838D9B, #D2610C, #80716A, #94147C, #5D5AD2, #6F6C7D", "#FDA023, #AA2B1E, #71685C, #64A73B, #EB5605, #B9CA1A", "#4E67C8, #5ECCF3, #A7EA52, #5DCEAF, #FF8021, #F14124", "#3891A7, #FEB80A, #C32D2E, #84AA33, #964305, #475A8D", "#6EA0B0, #CCAF0A, #8D89A4, #748560, #9E9273, #7E848D", "#759AA5, #CFC60D, #99987F, #90AC97, #FFAD1C, #B9AB6F", "#F0A22E, #A5644E, #B58B80, #C3986D, #A19574, #C17529", "#53548A, #438086, #A04DA3, #C4652D, #8B5D3D, #5C92B5", "#FF388C, #E40059, #9C007F, #68007F, #005BD3, #00349E", "#31B6FD, #4584D3, #5BD078, #A5D028, #F5C040, #05E0DB" };
        private const int TR = 0;
        private const int TG = 128;
        private const int TB = 215;
        private short[,] _dColor;
        private readonly Color _ringEndColor;
        private Color _ringStartColor;
        private bool _pieSectionSameColor;

        public OctofyRing()
        {
            InitializeComponent();

            _colors = new ColorSchema("#94C600,#71685A,#FF6700,#909465,#956B43,#FEA022");
            InitRingColors();
            SaveFolderName = string.Empty;
            _ringEndColor = Color.FromArgb(255, TR, TG, TB);
        }

        /// <summary>
        /// Initialize colors for the ring
        /// </summary>
        private void InitRingColors()
        {
            if (_values is object)
            {
                if (_values.Count() > 0)
                {
                    _ringColors = new Color[_values.Count() + 1];
                    _dColor = new short[_values.Count() + 1, 3];
                    for (int i = 0, loopTo = _ringColors.Count() - 1; i <= loopTo; i++)
                    {
                        _ringColors[i] = _colors.GetColorAt((short)(i));
                        _dColor[i, 0] = (short)(Math.Floor((TR - _ringColors[i].R) / (double)30));
                        _dColor[i, 1] = (short)(Math.Floor((TG - _ringColors[i].G) / (double)30));
                        _dColor[i, 2] = (short)(Math.Floor((TB - _ringColors[i].B) / (double)30));
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Colors
        {
            get
            {
                return _colors.ToString();
            }

            set
            {
                _colors.Parse(value);
                InitRingColors();
                Invalidate();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string SaveFolderName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int[] Values
        {
            get
            {
                return _values;
            }

            set
            {
                _values = value;
                InitRingColors();
                _calculated = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public float InitialAngle
        {
            get
            {
                return _initialAngle;
            }

            set
            {
                _initialAngle = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Animation
        {
            get
            {
                return timer1.Enabled;
            }

            set
            {
                if (value)
                {
                    timer1.Start();
                }
                else
                {
                    timer1.Stop();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool CalculateRingValue()
        {
            var result = default(bool);
            if (_values is object)
            {
                if (_calculated == true)
                {
                    if (_ringValues == null)
                    {
                        _calculated = false;
                    }
                    else if (_values.Length != _ringValues.Length)
                    {
                        _calculated = false;
                    }
                }

                if (_calculated == false)
                {
                    _ringValues = new int[_values.Length + 1];
                    var total = default(int);
                    for (int i = 0, loopTo = _values.Length - 1; i <= loopTo; i++)
                        total += _values[i];
                    if (total == 0)
                    {
                        _ringValues = new int[1];
                        _ringValues[0] = 100;
                    }
                    else
                    {
                        float ratio = (float)(100 / (double)total);
                        total = 0;
                        for (int i = 0, loopTo1 = _values.Length - 2; i <= loopTo1; i++)
                        {
                            _ringValues[i] = (int)(_values[i] * ratio);
                            total += _ringValues[i];
                        }

                        _ringValues[_values.Length - 1] = 100 - total;
                    }

                    _calculated = true;
                }

                result = true;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="canvas"></param>
        public void DrawPieChart(Graphics canvas)
        {
            if (_ringColors is object)
            {
                var endColor = Color.FromArgb(255, TR, TG, TB);
                canvas.FillEllipse(new SolidBrush(endColor), new Rectangle(77, 44, 170, 170));
                if (_pieSectionSameColor == false)
                {
                    _pieSectionSameColor = IsRingSameColor();
                    if (_pieSectionSameColor)
                    {
                        _ringStartColor = _ringEndColor;
                    }
                }

                if (_pieSectionSameColor)
                {
                    // Timer1.Stop()

                    _ringStartColor = TowardColor(_ringStartColor, Color.FromArgb(255, 10, 176, 252));
                    var br = new LinearGradientBrush(new Point(6, 3), new Point(249, 149), _ringStartColor, endColor);
                    canvas.FillEllipse(br, new Rectangle(77, 44, 170, 170));
                }
                else if (CalculateRingValue())
                {
                    int piePercentTotal = 0;
                    var rect = new Rectangle(77, 44, 170, 170);
                    for (int i = 0, loopTo = _ringValues.Length - 1; i <= loopTo; i++)
                    {
                        using (var brush = new SolidBrush(_ringColors[i]))
                        {
                            float startAngle = (float)(piePercentTotal * 360 / (double)100) + _initialAngle;
                            float sweepAngle = (float)(_ringValues[i] * 360 / (double)100);
                            canvas.FillPie(brush, rect, startAngle, sweepAngle);
                        }

                        piePercentTotal += _ringValues[i];
                    }
                }

                if (BackColor == Color.Transparent)
                {
                    canvas.FillEllipse(new SolidBrush(Color.White), new RectangleF(104, 71, 116, 116));
                }
                else
                {
                    canvas.FillEllipse(new SolidBrush(BackColor), new RectangleF(104, 71, 116, 116));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromColor"></param>
        /// <param name="toColor"></param>
        /// <returns></returns>
        private Color TowardColor(Color fromColor, Color toColor)
        {
            float dr = (float)((Convert.ToInt16(toColor.R) - Convert.ToInt16(fromColor.R)) * 0.1);
            float dg = (float)((Convert.ToInt16(toColor.G) - Convert.ToInt16(fromColor.G)) * 0.1);
            float db = (float)((Convert.ToInt16(toColor.B) - Convert.ToInt16(fromColor.B)) * 0.1);
            short r, g, b;
            if (Math.Abs(dr) <= 1)
            {
                r = Convert.ToInt16(toColor.R);
            }
            else
            {
                r = (short)(Convert.ToInt16(fromColor.R) + dr);
            }

            if (Math.Abs(dg) <= 1)
            {
                g = Convert.ToInt16(toColor.G);
            }
            else
            {
                g = (short)(Convert.ToInt16(fromColor.G) + dg);
            }

            if (Math.Abs(db) <= 1)
            {
                b = Convert.ToInt16(toColor.B);
            }
            else
            {
                b = (short)(Convert.ToInt16(fromColor.B) + db);
            }

            return Color.FromArgb(r, g, b);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool IsRingSameColor()
        {
            bool result = true;
            if (_ringColors is object)
            {
                if (_ringColors.Count() > 1)
                {
                    var c1 = _ringColors[0];
                    for (int i = 1, loopTo = _ringColors.Count() - 1; i <= loopTo; i++)
                    {
                        if (IsSameColor(_ringColors[i], c1) == false)
                        {
                            result = false;
                            break;
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns></returns>
        private bool IsSameColor(Color c1, Color c2)
        {
            var result = default(bool);
            const int dr = 5;
            if (Math.Abs((short)(Convert.ToInt16(c1.R) - Convert.ToInt16(c2.R))) < dr)
            {
                if (Math.Abs((short)(Convert.ToInt16(c1.G) - Convert.ToInt16(c2.G))) < dr)
                {
                    if (Math.Abs((short)(Convert.ToInt16(c1.B) - Convert.ToInt16(c2.B))) < dr)
                    {
                        result = true;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OctofyRing_Paint(object sender, PaintEventArgs e)
        {
            var chartImage = new Bitmap(256, 256);
            using (var canvas = Graphics.FromImage(chartImage))
            {
                canvas.SmoothingMode = SmoothingMode.AntiAlias;
                canvas.CompositingQuality = CompositingQuality.HighQuality;
                canvas.InterpolationMode = InterpolationMode.High;
                if (BackColor.A > 1)
                {
                    canvas.Clear(BackColor);
                }

                DrawPieChart(canvas);
                DrawEyebrow(canvas);
            }

            if (BackColor == Color.Transparent)
            {
                for (int i = 0; i < chartImage.Width; i++)
                {
                    for (int j = 0; j < chartImage.Height; j++)
                    {
                        Color color = chartImage.GetPixel(i, j);
                        if (color.R == 255 && color.G == 255 && color.B == 255)
                        {
                            chartImage.SetPixel(i, j, Color.Transparent);
                        }
                    }
                }
            }

            e.Graphics.Clear(BackColor);
            e.Graphics.DrawImage(chartImage, new Rectangle(0, 0, Width, Height));
            if (_saveFolderName.Length > 0)
            {

                string fileName = System.IO.Path.Combine(_saveFolderName, string.Format("octofy{0}.png", _imageIndex));
                _imageIndex += 1;
                chartImage.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="canvas"></param>
        private void DrawEyebrow(Graphics canvas)
        {
            var path = new GraphicsPath();
            path.AddLines(_pts);
            if (_alpha > 255)
            {
                _alpha = 255;
            }

            var startColor = Color.FromArgb(_alpha, 255, 110, 40);
            var endColor = Color.FromArgb(_alpha, 255, 240, 60);
            var br = new LinearGradientBrush(new Point(6, 3), new Point(249, 149), startColor, endColor);
            canvas.SetClip(path, CombineMode.Replace);
            canvas.FillRectangle(br, 6, 3, 243, 146);
            canvas.ResetClip();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            InitialAngle += _da;
            _da *= 0.94F;
            if (_da < 1.0)
            {
                timer1.Stop();
                _ringStartColor = Color.FromArgb(255, 10, 176, 252);
            }

            _alpha += 10;
            if (_alpha > 255)
            {
                _alpha = 255;
            }

            for (int i = 0, loopTo = _ringColors.Count() - 1; i <= loopTo; i++)
            {
                short r1 = (short)(_ringColors[i].R + _dColor[i, 0]);
                if (_dColor[i, 0] > 0)
                {
                    if (r1 > TR)
                    {
                        r1 = TR;
                    }
                }
                else if (r1 < TR)
                {
                    r1 = TR;
                }

                short g1 = (short)(_ringColors[i].G + _dColor[i, 1]);
                if (_dColor[i, 1] > 0)
                {
                    if (g1 > TG)
                    {
                        g1 = TG;
                    }
                }
                else if (g1 < TG)
                {
                    g1 = TG;
                }

                short b1 = (short)(_ringColors[i].B + _dColor[i, 2]);
                if (_dColor[i, 2] > 0)
                {
                    if (b1 > TB)
                    {
                        b1 = TB;
                    }
                }
                else if (b1 < TB)
                {
                    b1 = TB;
                }

                if (r1 < 0 | r1 > 255)
                {
                    r1 = TR;
                }

                if (g1 < 0 | g1 > 255)
                {
                    g1 = TG;
                }

                if (b1 < 0 | b1 > 255)
                {
                    b1 = TB;
                }

                _ringColors[i] = Color.FromArgb(r1, g1, b1);
            }

            Invalidate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OctofyRing_DoubleClick(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                _da = (float)(_da * 2.5);
                if (_da < 20)
                {
                    _da = 20.0F;
                }
                else if (_da > 60)
                {
                    _da = 60.0F;
                }
            }
            else
            {
                Random rnd = new Random();
                short colorIndex = (short)(rnd.Next(1, _schemas.Length - 1));
                _colors.Parse(_schemas[colorIndex]);
                for (int i = 0, loopTo = _values.Length - 1; i <= loopTo; i++)
                    _values[i] = (int)(rnd.Next(50));
                InitRingColors();
                _calculated = false;
                _da = 20.0F;
            }

            _imageIndex = 1;
            timer1.Start();
        }
    }
}
