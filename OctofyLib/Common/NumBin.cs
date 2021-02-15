namespace OctofyLib
{
    public class NumBin
    {
        string _format = "N0";
        decimal _nextStartValue;
        int _decimalPlace = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MinValue"></param>
        /// <param name="groupBy"></param>
        public NumBin(decimal minValue, decimal groupBy)
        {
            Init(minValue, groupBy);
            this.Count = 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public NumBin(NumBin value)
        {
            Init(value.MinValue, value.BinSize);
            this.Count = value.Count;
        }

        public bool IsBlank { get; set; }

        public decimal MinValue { get; set; }

        public decimal MaxValue { get; set; }

        public decimal BinSize { get; set; }

        public int Count { get; set; }

        private void Init(decimal minValue, decimal groupBy)
        {
            this.MinValue = minValue;
            _nextStartValue = MinValue + groupBy;
            BinSize = groupBy;
            string strBinSize = groupBy.ToString();
            if (strBinSize.IndexOf(".") > 0)
            {
                //remove end zero
                while (strBinSize.EndsWith("0"))
                {
                    strBinSize = strBinSize.Substring(0, strBinSize.Length - 1);
                }
                _decimalPlace = strBinSize.Length - strBinSize.IndexOf(".");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (IsBlank)
            {
                return Properties.Resources.B003;
            }
            return string.Format("{0} - {1}", MinValue.ToString(_format), MaxValue.ToString(_format));
        }
    }

}
