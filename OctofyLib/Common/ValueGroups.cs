using System.Collections.Generic;

namespace OctofyLib
{
    public class ValueGroups
    {
        private readonly List<ValueRange> _valueGroups;
        private int _minPrecision = 0;
        private int _maxPrecision = 0;
        private bool _intOnly = true;
        private readonly string BLANKS = Properties.Resources.B003;

        public ValueGroups()
        {
            _valueGroups = new List<ValueRange>();
        }

        /// <summary>
        /// Gets or sets the minimum value of the group
        /// </summary>
        public double StartValue { get; set; }

        /// <summary>
        /// Gets or sets the maximum value of the group
        /// </summary>
        public double EndValue { get; set; }

        /// <summary>
        /// Gets or set the precision for output group items
        /// </summary>
        public int Precision { get; set; }

        /// <summary>
        /// Clear the grouped items
        /// </summary>
        public void Clear()
        {
            _valueGroups.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public bool Open(List<string> values)
        {
            bool result = true;

            EndValue = double.MinValue;
            StartValue = double.MaxValue;

            _minPrecision = 0;
            _maxPrecision = 0;
            _intOnly = true;

            foreach (var item in values)
            {
                if (item != BLANKS)
                {
                    if (!item.IsNumeric())
                    {
                        result = false;
                        break;
                    }
                    else
                    {
                        if (item.IndexOf(".") > 0)
                        {
                            _intOnly = false;
                            int dotIndex = item.IndexOf(".");
                            string integerPart = item.Substring(0, dotIndex);
                            if (integerPart.StartsWith("-"))
                                integerPart = integerPart.Substring(1, integerPart.Length - 1);
                            string decimalPart = item.Substring(dotIndex + 1, item.Length - dotIndex - 1);

                            int integerLength = integerPart.Length;
                            if (integerLength - 1 > _maxPrecision)
                                _maxPrecision = integerLength - 1;

                            int decimalLength = decimalPart.Length;
                            if (decimalLength - 1 > _minPrecision)
                                _minPrecision = decimalLength - 1;

                            if (double.TryParse(item, out double doubleValue))
                            {
                                if (doubleValue > EndValue)
                                    EndValue = doubleValue;

                                if (doubleValue < StartValue)
                                    StartValue = doubleValue;
                            }
                            else
                            {
                                result = false;
                                break;
                            }
                        }
                        else
                        {
                            int intPrecision = item.Length - 1;
                            if (intPrecision > _maxPrecision)
                                _maxPrecision = intPrecision;

                            if (long.TryParse(item, out long longValue))
                            {
                                if ((double)longValue > EndValue)
                                    EndValue = (double)longValue;

                                if ((double)longValue < StartValue)
                                    StartValue = (double)longValue;
                            }
                            else
                            {
                                result = false;
                                break;
                            }
                        }
                    }
                }
            }

            return result;
        }

        public List<string> SelectablePrecisions()
        {
            var result = new List<string>();

            if (_minPrecision > 0)
            {
                for (int i = _minPrecision; i >= 0; i--)
                {
                    string zeros = new string('0', i);
                    result.Add(string.Format("0.{0}1", zeros));
                }
                result.Add("0");
                for (int i = 0; i <= _maxPrecision; i++)
                {
                    result.Add(string.Format("1{0}", new string('0', i)));
                }
            }
            return result;
        }

        /// <summary>
        /// Gets grouped items in specified precision
        /// </summary>
        /// <param name="precision"></param>
        /// <returns></returns>
        public List<ValueRange> ValueGroupItems(double precision)
        {
            var result = new List<ValueRange>();
            double dx = precision * 10;
            if (precision == 0)
                dx = 1;
            if (precision > 0)
            {

                for (double i = StartValue; i <= EndValue; i += dx)
                {
                    result.Add(new ValueRange(i, i + dx - precision));
                }
            }
            return result;
        }

        private double PrecisionValue(double value, double precision)
        {
            double result = 0;
            if (precision != 0)
            {
                long intValue = (long)(value / precision);
                result = intValue * precision;
            }

            return result;
        }
    }
}
