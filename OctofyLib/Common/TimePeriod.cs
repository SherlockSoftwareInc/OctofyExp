using System;

namespace OctofyLib
{
    /// <summary>
    /// Time period item
    /// </summary>
    public class TimePeriod
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <param name="periodIndex"></param>
        /// <param name="periodName"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        public TimePeriod(int year, int periodIndex, string periodName, DateTime startDate, DateTime endDate)
        {
            this.Year = year;
            this.Period = periodIndex;
            this.PeriodName = periodName;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Count = 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public TimePeriod(TimePeriod value)
        {
            this.Year = value.Year;
            this.Period = value.Period;
            this.PeriodName = value.PeriodName;
            this.StartDate = value.StartDate;
            this.EndDate = value.EndDate;
        }

        /// <summary>
        /// Year of time period
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// period name
        /// </summary>
        public int Period { get; set; }

        public string PeriodName { get; set; }

        /// <summary>
        /// Period start date
        /// </summary>
        public DateTime StartDate { get; set; }

        private DateTime _endDate;

        /// <summary>
        /// Period end date
        /// </summary>
        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                if (value.Hour != 23 || value.Second != 59 || value.Minute != 59)
                {
                    _endDate = new DateTime(value.Year, value.Month, value.Day, 23, 59, 59, 997);
                }
                else
                {
                    _endDate = value;
                }
            }
        }

        public int Count { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (Year == 0)
            {
                return Properties.Resources.B003;
            }
            if (PeriodName.Length == 0)
            {
                return Year.ToString("0000");
            }
            return String.Format("{0}/{1}", Year.ToString("0000"), PeriodName);
        }
    }

}
