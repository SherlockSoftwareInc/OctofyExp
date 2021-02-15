using System;
using System.Collections.Generic;
using System.Drawing;

namespace OctofyLib
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ColorSchema
    {

        private const string STR_DEF_colorSchemas = "#40699C,#9E413E,#7F9A48,#695185,#3C8DA3,#CC7B38,#4F81BD,#C0504D,#9BBB59,#8064A2,#4BACC6,#F79646,#AABAD7,#D9AAA9,#C6D6AC,#BAB0C9";
        private const string STR_GRAYSCALE = "#4F4F4F,#939393,#6A6A6A,#404040,#7A7A7A,#B4B4B4,#616161,#B3B3B3,#818181,#505050,#959595,#DADADA,#AFAFAF,#D2D2D2,#BABABA,#AAAAAA";
        private const string STR_Qualitative = "#E41A1C,#377EB8,#4DAF4A,#984EA3,#FF7F00,#FFFF33,#A65628,#F781BF,#999999,#8064A2,#4BACC6,#F79646,#AABAD7,#D9AAA9,#C6D6AC,#BAB0C9";
        private const string STR_Berry = "#8A2BE2,#BA55D3,#4169E1,#C71585,#0000FF,#8A2BE2,#DA70D6,#7B68EE,#C000C0,#0000CD,#4BACC6,#F79646,#AABAD7,#D9AAA9,#C6D6AC,#BAB0C9";
        private const string STR_Bright = "#008000,#0000FF,#800080,#00FF00,#FF00FF,#008080,#FFFF00,#808080,#00FFFF,#000080,#800000,#FF0000,#808000,#C0C0C0,#FF6347,#FFE4B5";
        private const string STR_BrightPastel = "#418CF0,#FCB441,#E0400A,#056492,#BFBFBF,#1A3B69,#FFE382,#129CDD,#CA6B4B,#005CDB,#F3D288,#506381,#F1B9A8,#E0830A,#7893BE";
        private const string STR_Chocolate = "#A0522D,#D2691E,#8B0000,#CD853F,#A52A2A,#F4A460,#8B4513,#C04000,#B22222,#B65C3A";
        private const string STR_EarthTones = "#FF8000,#B8860B,#C04000,#6B8E23,#CD853F,#C0C000,#228B22,#D2691E,#808000,#20B2AA,#F4A460,#00C000,#8FBC8B,#B22222,#8B4513,#C00000";
        private const string STR_Excel = "#9999FF,#993366,#FFFFCC,#CCFFFF,#660066,#FF8080,#0066CC,#CCCCFF,#000080,#FF00FF,#FFFF00,#00FFFF,#800080,#800000,#008080,#0000FF";
        private const string STR_Fire = "#FFD700,#FF0000,#FF1493,#DC143C,#FF8C00,#FF00FF,#FFFF00,#FF4500,#C71585,#DDE221";
        private const string STR_Light = "#E6E6FA,#FFF0F5,#FFDAB9,#FFFACD,#FFE4E1,#F0FFF0,#F0F8FF,#F5F5F5,#FAEBD7,#E0FFFF";
        private const string STR_Pastel = "#87CEEB,#32CD32,#BA55D3,#F08080,#4682B4,#9ACD32,#40E0D0,#FF69B4,#F0E68C,#D2B48C,#8FBC8B,#6495ED,#DDA0DD,#5F9EA0,#FFDAB9,#FFA07A";
        private const string STR_SeaGreen = "#2E8B57,#66CDAA,#4682B4,#008B8B,#5F9EA0,#3CB371,#48D1CC,#B0C4DE,#8FBC8B,#87CEEB";
        private const string STR_SemiTransparent = "#FF6969,#69FF69,#6969FF,#FFFF69,#69FFFF,#FF69FF,#CDB075,#FFAFAF,#AFFFAF,#AFAFFF,#FFFFAF,#AFFFFF,#FFAFFF,#E4D5B5,#A4B086,#819EC1";
        private const string STR_MY_COLORS = "#D77FB4,#CE7058,#9E67AB,#FAA75B,#5A9BD4,#7AC36A,#F15A60,#737373,#EBC0DA,#DDB9A9,#D5B2D4,#F3D1B0,#B8D2EC,#D9E4AA,#F2AFAD,#CCCCCC";

        private List<Color> _colors;

        /// <summary>
        /// class constructor
        /// </summary>
        public ColorSchema()
        {
            Parse("");
        }

        /// <summary>
        /// class constructor
        /// </summary>
        /// <param name="schema"></param>
        public ColorSchema(string schema)
        {
            Parse(schema);
        }

        /// <summary>
        /// Gets or sets color schema name
        /// </summary>
        public string SchemaName { get; private set; }

        /// <summary>
        /// Parse color schema
        /// </summary>
        /// <param name="schema"></param>
        public void Parse(string schema)
        {
            string strColor;
            var switchExpr = schema.ToLower();
            switch (switchExpr)
            {
                case "berry":
                    strColor = STR_Berry;
                    SchemaName = "Berry";
                    break;

                case "bright":
                    strColor = STR_Bright;
                    break;

                case "brightpastel":
                    strColor = STR_BrightPastel;
                    SchemaName = "BrightPastel";
                    break;

                case "chocolate":
                    strColor = STR_Chocolate;
                    SchemaName = "Chocolate";
                    break;

                case "earthtones":
                    strColor = STR_EarthTones;
                    SchemaName = "EarthTones";
                    break;

                case "excel":
                    strColor = STR_Excel;
                    SchemaName = "Excel";
                    break;

                case "fire":
                    strColor = STR_Fire;
                    SchemaName = "Fire";
                    break;

                case "grayscale":
                    strColor = STR_GRAYSCALE;
                    SchemaName = "GrayScale";
                    break;

                case "light":
                    strColor = STR_Light;
                    SchemaName = "Light";
                    break;

                case "pastel":
                    strColor = STR_Pastel;
                    SchemaName = "Pastel";
                    break;

                case "qualitative":
                    strColor = STR_Qualitative;
                    SchemaName = "Qualitative";
                    break;

                case "seagreen":
                    strColor = STR_SeaGreen;
                    SchemaName = "SeaGreen";
                    break;

                case "semitransparent":
                    strColor = STR_SemiTransparent;
                    SchemaName = "SemiTransparent";
                    break;

                case "mycolors":
                    strColor = STR_MY_COLORS;
                    SchemaName = "MyColors";
                    break;

                case "":
                case "default":
                case "[default]":
                    strColor = STR_DEF_colorSchemas;
                    SchemaName = "Default";
                    break;

                default:
                    {
                        if (schema.IndexOf(":") > 0)
                        {
                            var elements = schema.Split(':');
                            SchemaName = elements[0];
                            strColor = elements[1];
                        }
                        else if (schema.IndexOf(",") > 1 | schema.StartsWith("#"))
                        {
                            strColor = schema;
                            SchemaName = "Custom";
                        }
                        else
                        {
                            strColor = STR_DEF_colorSchemas;
                            SchemaName = "Default";
                        }

                        break;
                    }
            }

            if (strColor.Length == 0)
            {
                strColor = STR_DEF_colorSchemas;
            }

            var strColors = strColor.Split(',');
            if (_colors == null)
            {
                _colors = new List<Color>();
            }
            else
            {
                _colors.Clear();
            }
            for (int i = 0; i < strColors.Length; i++)
                _colors.Add(MyColorTranslator.FromColorString(strColors[i]));
        }

        /// <summary>
        /// Return a color at specified index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Color GetColorAt(short index)
        {
            if (_colors.Count == 0)
            {
                return Color.Transparent;
            }
            else
            {
                short x = (short)(index % _colors.Count);
                short dc = (short)(index / _colors.Count);
                float correctionFactor = 0;
                if (index >= _colors.Count)
                {
                    correctionFactor = (float)(0.3F * dc);
                }

                if (correctionFactor <= -1)
                {
                    correctionFactor = -0.9999F;
                }

                if (correctionFactor >= 1)
                {
                    correctionFactor = 0.9999F;
                }

                return CreateColorWithCorrectedLightness(_colors[x], correctionFactor);
            }
        }

        ///// <summary>
        ///// Return a hatch style at specified index
        ///// </summary>
        ///// <param name="index"></param>
        ///// <returns></returns>
        //public static HatchStyle GetHatchStyleAt(short index)
        //{
        //    if (index < 16)
        //    {
        //        return HatchStyle.DottedDiamond;
        //    }
        //    else
        //    {
        //        return ((short)(Math.Floor(index / (double)16) + 1)) switch
        //        {
        //            10 => HatchStyle.Percent05,
        //            1 => HatchStyle.Percent10,
        //            9 => HatchStyle.Percent20,
        //            8 => HatchStyle.Percent25,
        //            7 => HatchStyle.Percent30,
        //            6 => HatchStyle.Percent40,
        //            5 => HatchStyle.Percent50,
        //            4 => HatchStyle.Percent60,
        //            3 => HatchStyle.Percent70,
        //            2 => HatchStyle.Percent75,
        //            var @case when @case == 1 => HatchStyle.Percent80,
        //            _ => HatchStyle.Percent90,
        //        };
        //    }
        //}

        /// <summary>
        /// Replace a color at spceified index
        /// </summary>
        /// <param name="index"></param>
        /// <param name="color"></param>
        public void SetColorAt(short index, Color color)
        {
            if (index >= 0 & index < _colors.Count)
            {
                _colors[index] = color;
            }
        }

        /// <summary>
        /// Create a new color based on a given color with corrected lightness
        /// </summary>
        /// <param name="color"></param>
        /// <param name="correctionFactor"></param>
        /// <returns></returns>
        private Color CreateColorWithCorrectedLightness(Color color, float correctionFactor)
        {
            if (correctionFactor == 0)
            {
                return color;
            }
            else if (correctionFactor > -1 & correctionFactor < 1)
            {
                float red = color.R;
                float green = color.G;
                float blue = color.B;
                if (correctionFactor < 0)
                {
                    correctionFactor = 1 + correctionFactor;
                    red *= correctionFactor;
                    green *= correctionFactor;
                    blue *= correctionFactor;
                }
                else
                {
                    red = (255 - red) * correctionFactor + red;
                    green = (255 - green) * correctionFactor + green;
                    blue = (255 - blue) * correctionFactor + blue;
                }

                return Color.FromArgb(color.A, Convert.ToInt32(Math.Truncate(red)), Convert.ToInt32(Math.Truncate(green)), Convert.ToInt32(Math.Truncate(blue)));
            }
            else
            {
                return color;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (_colors.Count == 0)
            {
                return "";
            }
            else
            {
                string strColor = BuildColorString();
                var schemaType = GetColorSchemaType(strColor);
                if (schemaType == ColorSchemas.Custom)
                {
                    return strColor;
                }
                else if (schemaType == ColorSchemas.Default)
                {
                    return "";
                }
                else
                {
                    return schemaType.ToString();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string BuildColorString()
        {
            if (_colors.Count == 0)
            {
                return "";
            }
            else
            {
                string strColor = MyColorTranslator.ToColorString(_colors[0]);
                for (int i = 0; i < _colors.Count; i++)
                    strColor += "," + MyColorTranslator.ToColorString(_colors[i]);
                return strColor;
            }
        }

        private ColorSchemas GetColorSchemaType(string colorStr)
        {
            if (colorStr.Length == 0)
            {
                return ColorSchemas.Default;
            }
            else
            {
                switch (colorStr)
                {
                    case STR_Berry:
                        return ColorSchemas.Berry;
                    case STR_DEF_colorSchemas:
                        return ColorSchemas.Default;
                    case STR_GRAYSCALE:
                        return ColorSchemas.GrayScale;
                    case STR_Qualitative:
                        return ColorSchemas.Qualitative;
                    case STR_Bright:
                        return ColorSchemas.Bright;
                    case STR_BrightPastel:
                        return ColorSchemas.BrightPastel;
                    case STR_Chocolate:
                        return ColorSchemas.Chocolate;
                    case STR_EarthTones:
                        return ColorSchemas.EarthTones;
                    case STR_Excel:
                        return ColorSchemas.Excel;
                    case STR_Fire:
                        return ColorSchemas.Fire;
                    case STR_Light:
                        return ColorSchemas.Light;
                    case STR_Pastel:
                        return ColorSchemas.Pastel;
                    case STR_SeaGreen:
                        return ColorSchemas.SeaGreen;
                    case STR_SemiTransparent:
                        return ColorSchemas.SemiTransparent;
                    case STR_MY_COLORS:
                        return ColorSchemas.MyColors;
                    default:
                        return ColorSchemas.Custom;
                }
            }
        }
    }
}