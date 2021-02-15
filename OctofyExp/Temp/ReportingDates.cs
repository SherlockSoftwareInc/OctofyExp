using System;
using System.Collections.Generic;
using System.Globalization;

namespace DBExpo
{
    class ReportingDates
    {
        public enum PeriodTypes
        {
            None = -1,
            Month,
            Quarter,
            CalendarYear,
            Week
        }

        readonly List<TimePeriod> _periods;

        public ReportingDates()
        {
            _periods = new List<TimePeriod>();
        }

        public ReportingDates(ReportingDates value)
            : base()
        {
            List<TimePeriod> source = value.Periods();
            if (source != null)
            {
                for (int i = 0; i < source.Count; i++)
                {
                    _periods.Add(new TimePeriod(source[i]));
                }
            }
        }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public PeriodTypes PeriodType { get; set; }

        public List<TimePeriod> Periods()
        {
            return _periods;
        }

        public void Clear()
        {
            _periods.Clear();
        }

        public bool Open(DateTime startDate, DateTime endDate, PeriodTypes periodType)
        {
            bool result = false;
            this.PeriodType = periodType;
            Clear();

            int y, m, d;
            //int y = endDate.Year;
            //int m = endDate.Month;
            //int d = DateTime.DaysInMonth(y, m);
            //if (periodType != PeriodTypes.Week)
            //{
            //    this.EndDate = new DateTime(y, m, d);

            //    this.StartDate = new DateTime(startDate.Year, startDate.Month, 1);
            //}
            //else
            //{
            //    int dayDIff = startDate.DayOfWeek - DayOfWeek.Monday;
            //    if (dayDIff < 0)
            //        dayDIff += 7;
            //    this.StartDate = startDate.AddDays(-dayDIff);

            //    //dayDIff = endDate.DayOfWeek - DayOfWeek.Sunday;
            //}

            DateTime periodStart;
            DateTime periodEnd;
            string periodName;
            switch (periodType)
            {
                case PeriodTypes.None:
                    break;
                case PeriodTypes.Month:
                    y = startDate.Year;
                    m = startDate.Month;
                    periodStart = new DateTime(y, m, 1);
                    while (periodStart < endDate)
                    {
                        d = DateTime.DaysInMonth(y, m);
                        periodEnd = new DateTime(y, m, d);
                        periodName = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(m);
                        //periodStart.ToString("MMM", CultureInfo.InvariantCulture)
                        _periods.Add(new TimePeriod(y, periodName, periodStart, periodEnd));
                        m++;
                        if (m > 12)
                        {
                            m = 1;
                            y++;
                        }
                        periodStart = new DateTime(y, m, 1);
                    }
                    break;

                case PeriodTypes.Quarter:
                    y = startDate.Year;
                    m = startDate.Month;
                    int q = (int)Math.Ceiling((double)(m / 3));
                    m = (q - 1) * 3 + 1;
                    periodStart = new DateTime(y, m, 1);
                    while (periodStart < endDate)
                    {
                        d = DateTime.DaysInMonth(y, m);
                        periodEnd = new DateTime(y, m, d);
                        periodName = string.Format("Q{0}", q);
                        _periods.Add(new TimePeriod(y, periodName, periodStart, periodEnd));
                        m += 3;
                        if (m > 12)
                        {
                            m = 1;
                            y++;
                        }
                        periodStart = new DateTime(y, m, 1);
                    }
                    break;
                case PeriodTypes.CalendarYear:
                    y = startDate.Year;
                    periodStart = new DateTime(y, 1, 1);
                    while (periodStart < endDate)
                    {
                        _periods.Add(new TimePeriod(y, "", periodStart, new DateTime(y, 12, 31)));
                        y++;
                        periodStart = new DateTime(y, 1, 1);
                    }
                    break;
                case PeriodTypes.Week:
                    int dayDIff = startDate.DayOfWeek - DayOfWeek.Monday;
                    if (dayDIff < 0)
                        dayDIff += 7;
                    periodStart = startDate.AddDays(-dayDIff);
                    while (periodStart < endDate)
                    {
                        periodEnd = periodStart.AddDays(6);
                        periodName = string.Format("W{0}", GetIso8601WeekOfYear(periodStart));
                        _periods.Add(new TimePeriod(periodStart.Year, periodName, periodStart, periodEnd));
                        periodStart = periodStart.AddDays(7);
                    }

                    break;
                default:
                    break;
            }

            return result;
        }

        //https://stackoverflow.com/questions/11154673/get-the-correct-week-number-of-a-given-date
        // This presumes that weeks start with Monday.
        // Week 1 is the 1st week of the year with a Thursday in it.
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

        public bool Open(PeriodTypes periodType, List<string> values)
        {
            this.PeriodType = periodType;
            DateTime startDate = DateTime.MaxValue;
            DateTime endDate = DateTime.MinValue;
            bool hasBlanks = false;

            foreach (var value in values)
            {
                if (value.Length == 0 || value == "(Blanks)")
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
                if (startDate.Year < 1753)
                    startDate = new DateTime(1753, 1, 1);
                if (Open(startDate, endDate, periodType))
                {
                    if (hasBlanks)
                    {
                        _periods.Insert(0, new TimePeriod(0, "", DateTime.MinValue, DateTime.MinValue));
                    }
                    return (_periods.Count > 0);
                }
            }

            return false;
        }

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
