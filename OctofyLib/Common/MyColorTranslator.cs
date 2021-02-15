
using System;
using System.Drawing;

namespace OctofyLib
{
    public class MyColorTranslator
    {
        public static Color FromColorString(string strColor)
        {
            try
            {
                string colorString = strColor.Trim();
                if (colorString.StartsWith("#"))
                {
                    if (colorString.Length == 9)
                    {
                        int alpha = Convert.ToInt32(colorString.Substring(1, 2), 16);
                        int red = Convert.ToInt32(colorString.Substring(3, 2), 16);
                        int green = Convert.ToInt32(colorString.Substring(5, 2), 16);
                        int blue = Convert.ToInt32(colorString.Substring(7, 2), 16);
                        return Color.FromArgb(alpha, red, green, blue);
                    }
                    else
                    {
                        return ColorTranslator.FromHtml(colorString);
                    }
                }
                else
                {
                    return ColorTranslator.FromHtml(colorString);
                }
            }
            catch (Exception)
            {
                return Color.Transparent;
            }
        }

        public static string ToColorString(Color colour)
        {
            if (colour.A == 255)
            {
                return ColorTranslator.ToHtml(colour);
            }
            else if (colour == Color.Transparent)
            {
                return "Transparent";
            }
            else
            {
                string alpha = "00" + colour.A.ToString("X");
                if (alpha.Length > 2)
                {
                    alpha = alpha.Substring(alpha.Length - 2, 2);
                }

                return string.Format("#{0}{1}", alpha, ColorTranslator.ToHtml(colour).Substring(1, 6));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="backColor"></param>
        /// <returns></returns>
        public static Color ReverseColorBW(Color backColor)
        {
            short darkRed = (short)(backColor.R <= 128 ? 1 : 0);
            short darkGreen = (short)(backColor.G <= 128 ? 1 : 0);
            short darkBlue = (short)(backColor.B <= 128 ? 1 : 0);
            int totalVolue = backColor.R;
            totalVolue += backColor.G;
            totalVolue += backColor.B;
            bool useBlack;
            if (totalVolue < 383 | (short)(darkBlue + darkGreen) + darkRed > 1)
            {
                useBlack = false;
            }
            else
            {
                useBlack = true;
            }

            if (!useBlack & backColor.A <= 128)
            {
                useBlack = true;
            }

            if (useBlack)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }
    }
}