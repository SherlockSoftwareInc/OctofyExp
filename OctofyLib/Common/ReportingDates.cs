using System;
using System.Collections.Generic;
using System.Globalization;

namespace OctofyLib
{
    /// <summary>
    /// 
    /// </summary>
    public class ReportingDates
    {
        /// <summary>
        /// 
        /// </summary>
        public enum PeriodTypes
        {
            None = -1,
            Month,
            Quarter,
            CalendarYear,
            Week
        }

        readonly List<TimePeriod> _periods;

        /// <summary>
        /// 
        /// </summary>
        public ReportingDates()
        {
            _periods = new List<TimePeriod>();
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="value"></param>
        //public ReportingDates(ReportingDates value)
        //    : base()
        //{
        //    List<TimePeriod> source = value.Periods;
        //    if (source != null)
        //    {
        //        for (int i = 0; i < source.Count; i++)
        //        {
        //            _periods.Add(new TimePeriod(source[i]));
        //        }
        //    }
        //}

        //public DateTime StartDate { get; set; }
        //public DateTime EndDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public PeriodTypes PeriodType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<TimePeriod> Periods
        {
            get
            {
                return _periods;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            _periods.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="periodType"></param>
        /// <returns></returns>
        public bool Open(DateTime startDate, DateTime endDate, PeriodTypes periodType)
        {
            bool result = false;
            this.PeriodType = periodType;
            Clear();

            int y, m, d;
            DateTime periodStart;
            DateTime periodEnd;
            string periodName;
            switch (periodType)
            {
                case PeriodTypes.Month:
                    y = startDate.Year;
                    m = startDate.Month;
                    periodStart = new DateTime(y, m, 1);
                    while (periodStart <= endDate)
                    {
                        d = DateTime.DaysInMonth(y, m);
                        periodEnd = new DateTime(y, m, d);
                        periodName = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(m);
                        //periodStart.ToString("MMM", CultureInfo.InvariantCulture)
                        _periods.Add(new TimePeriod(y, m, periodName, periodStart, periodEnd));
                        m++;
                        if (m > 12)
                        {
                            m = 1;
                            y++;
                        }
                        periodStart = new DateTime(y, m, 1);
                    }
                    result = true;
                    break;

                case PeriodTypes.Quarter:
                    y = startDate.Year;
                    m = startDate.Month;
                    int q = (int)Math.Ceiling(((double)m / 3.0f));
                    m = (q - 1) * 3 + 1;
                    periodStart = new DateTime(y, m, 1);
                    while (periodStart <= endDate)
                    {
                        m += 2;
                        d = DateTime.DaysInMonth(y, m);
                        periodEnd = new DateTime(y, m, d);
                        periodName = string.Format("Q{0}", q);
                        _periods.Add(new TimePeriod(y, q, periodName, periodStart, periodEnd));
                        q++;
                        if (q > 4)
                        {
                            q = 1;
                            y++;
                        }
                        m = (q - 1) * 3 + 1;
                        periodStart = new DateTime(y, m, 1);
                    }
                    result = true;
                    break;

                case PeriodTypes.CalendarYear:
                    y = startDate.Year;
                    periodStart = new DateTime(y, 1, 1);
                    while (periodStart <= endDate)
                    {
                        _periods.Add(new TimePeriod(y, 0, "", periodStart, new DateTime(y, 12, 31)));
                        y++;
                        periodStart = new DateTime(y, 1, 1);
                    }
                    result = true;
                    break;

                case PeriodTypes.Week:
                    int dayDIff = startDate.DayOfWeek - DayOfWeek.Monday;
                    if (dayDIff < 0)
                        dayDIff += 7;
                    periodStart = startDate.AddDays(-dayDIff);
                    while (periodStart <= endDate)
                    {
                        periodEnd = periodStart.AddDays(6);
                        int week = GetIso8601WeekOfYear(periodStart);
                        periodName = string.Format("W{0}", week);
                        _periods.Add(new TimePeriod(periodStart.Year, week, periodName, periodStart, periodEnd));
                        periodStart = periodStart.AddDays(7);
                    }
                    result = true;
                    break;

                default:
                    break;
            }

            return result;
        }

        /// <summary>
        /// https://stackoverflow.com/questions/11154673/get-the-correct-week-number-of-a-given-date
        /// This presumes that weeks start with Monday.
        /// Week 1 is the 1st week of the year with a Thursday in it.
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static int GetIso8601WeekOfYear(DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="periodType"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public bool Open(PeriodTypes periodType, List<string> values)
        {
            this.PeriodType = periodType;
            DateTime startDate = DateTime.MaxValue;
            DateTime endDate = DateTime.MinValue;
            bool hasBlanks = false;

            foreach (var value in values)
            {
                if (value.Length == 0 || value == Properties.Resources.B003)
                {
                    hasBlanks = true;
                }
                else
                {
                    if (DateTime.TryParse(value, out DateTime eventDate))
                    {
                        if (eventDate < startDate)
                            startDate = eventDate;
                        if (eventDate > endDate)
                            endDate = eventDate;
                    }
                }
            }

            if (startDate != DateTime.MaxValue && endDate != DateTime.MinValue)
            {
                //if (startDate.Year < 1753)
                //    startDate = new DateTime(1753, 1, 1);
                if (Open(startDate, endDate, periodType))
                {
                    if (hasBlanks)
                    {
                        _periods.Insert(0, new TimePeriod(0, 0, "", DateTime.MinValue, DateTime.MinValue));
                    }
                    return (_periods.Count > 0);
                }
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public int GetDatePeriodIndex(DateTime date)
        {
            for (int i = 0; i < _periods.Count; i++)
            {
                if (date >= _periods[i].StartDate && date <= _periods[i].EndDate)
                    return i;
            }
            return -1;
        }

    }
}
