using System;

namespace OctofyLib
{
    public class ValueRange
    {
        public ValueRange()
        {
        }

        public ValueRange(long startValue, long endValue)
        {
            this.StartValue = (double)startValue;
            this.EndValue = (double)endValue;
        }

        public ValueRange(double startValue, double endValue)
        {
            this.StartValue = startValue;
            this.EndValue = endValue;
        }

        public ValueRange(ValueRange value)
        {
            this.StartValue = value.StartValue;
            this.EndValue = value.EndValue;
        }

        public double StartValue { get; set; }

        public double EndValue { get; set; }

        public int NumericScale { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("{0}<= value <{1}", StartValue, EndValue);
        }

    }
}
