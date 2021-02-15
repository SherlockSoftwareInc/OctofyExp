using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace OctofyLib
{
    public class DateGrouper
    {
        /// <summary>
        /// Period types enum
        /// </summary>
        public enum PeriodTypes
        {
            None = -1,
            Month,
            Quarter,
            CalendarYear,
            Week
        }

        private readonly List<TimePeriod> _periods;
        private bool _hasBlanks = false;

        public DateGrouper(PeriodTypes periodType)
        {
            _periods = new List<TimePeriod>();
            this.PeriodType = periodType;
        }

        private PeriodTypes PeriodType { get; set; }

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

        //public bool Open(DateTime startDate, DateTime endDate, PeriodTypes periodType)
        //{
        //    bool result = false;
        //    this.PeriodType = periodType;
        //    _periods.Clear();

        //    int y, m, d;
        //    DateTime periodStart;
        //    DateTime periodEnd;
        //    string periodName;
        //    switch (periodType)
        //    {
        //        case PeriodTypes.Month:
        //            y = startDate.Year;
        //            m = startDate.Month;
        //            periodStart = new DateTime(y, m, 1);
        //            while (periodStart <= endDate)
        //            {
        //                d = DateTime.DaysInMonth(y, m);
        //                periodEnd = new DateTime(y, m, d);
        //                periodName = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(m);
        //                _periods.Add(new TimePeriod(y, m, periodName, periodStart, periodEnd));
        //                m++;
        //                if (m > 12)
        //                {
        //                    m = 1;
        //                    y++;
        //                }
        //                periodStart = new DateTime(y, m, 1);
        //            }
        //            result = true;
        //            break;

        //        case PeriodTypes.Quarter:
        //            y = startDate.Year;
        //            m = startDate.Month;
        //            int q = (int)Math.Ceiling(((double)m / 3.0f));
        //            m = (q - 1) * 3 + 1;
        //            periodStart = new DateTime(y, m, 1);
        //            while (periodStart <= endDate)
        //            {
        //                m += 2;
        //                d = DateTime.DaysInMonth(y, m);
        //                periodEnd = new DateTime(y, m, d);
        //                periodName = string.Format("Q{0}", q);
        //                _periods.Add(new TimePeriod(y, q, periodName, periodStart, periodEnd));
        //                q++;
        //                if (q > 4)
        //                {
        //                    q = 1;
        //                    y++;
        //                }
        //                m = (q - 1) * 3 + 1;
        //                periodStart = new DateTime(y, m, 1);
        //            }
        //            result = true;
        //            break;

        //        case PeriodTypes.CalendarYear:
        //            y = startDate.Year;
        //            periodStart = new DateTime(y, 1, 1);
        //            while (periodStart <= endDate)
        //            {
        //                _periods.Add(new TimePeriod(y, 0, "", periodStart, new DateTime(y, 12, 31)));
        //                y++;
        //                periodStart = new DateTime(y, 1, 1);
        //            }
        //            result = true;
        //            break;

        //        case PeriodTypes.Week:
        //            int dayDIff = startDate.DayOfWeek - DayOfWeek.Monday;
        //            if (dayDIff < 0)
        //            { dayDIff += 7; }
        //            periodStart = startDate.AddDays(-dayDIff);
        //            while (periodStart <= endDate)
        //            {
        //                periodEnd = periodStart.AddDays(6);
        //                int week = GetIso8601WeekOfYear(periodStart);
        //                periodName = string.Format("W{0}", week);
        //                _periods.Add(new TimePeriod(periodStart.Year, week, periodName, periodStart, periodEnd));
        //                periodStart = periodStart.AddDays(7);
        //            }
        //            result = true;
        //            break;

        //        default:
        //            break;
        //    }

        //    return result;
        //}

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

            // return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        /// <summary>
        /// Open date grouper from a list of values and period type
        /// </summary>
        /// <param name="periodType"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public bool Open(PeriodTypes periodType, List<string> values)
        {
            this.PeriodType = periodType;
            return Open(values);
        }


        /// <summary>
        /// Open date grouper from a list of values 
        /// </summary>
        /// <param name="periodType"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public bool Open(List<string> values)
        {
            _hasBlanks = false;

            foreach (var value in values)
            {
                if (value.Length == 0 || value == Properties.Resources.B003)
                {
                    AddBlankDate();
                }
                else
                {
                    if (DateTime.TryParse(value, out DateTime eventDate))
                    {
                        Add(eventDate);
                    }
                    else
                    {
                        throw new InvalidDataException();
                    }
                }
            }

            return (_periods.Count > 0);
        }

        /// <summary>
        /// Add a blank date item
        /// </summary>
        public void AddBlankDate()
        {
            if (!_hasBlanks)
            {
                _hasBlanks = true;
                _periods.Insert(0, new TimePeriod(0, 0, "", DateTime.MinValue, DateTime.MinValue));
            }
            else
            {
                _periods[0].Count++;
            }
        }

        /// <summary>
        /// Add a date item
        /// </summary>
        /// <param name="eventDate"></param>
        public void Add(DateTime? eventDate)
        {
            if (eventDate == null)
            {
                AddBlankDate();
            }
            else
            {
                DateTime date = (DateTime)eventDate;
                int year = date.Year;
                int periodIndex;
                DateTime periodStart;
                DateTime periodEnd;
                string periodName;

                switch (this.PeriodType)
                {
                    case PeriodTypes.Month:
                        periodIndex = date.Month;
                        periodStart = new DateTime(year, periodIndex, 1);
                        if (IsMaxDate(date))
                        {
                            periodEnd = DateTime.MaxValue;
                        }
                        else
                        {
                            periodEnd = new DateTime(year, periodIndex, DateTime.DaysInMonth(year, periodIndex));
                        }
                        periodName = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(periodIndex);
                        break;

                    case PeriodTypes.Quarter:
                        if (IsMaxDate(date))
                        {
                            periodStart = new DateTime(year, 10, 1);
                            periodEnd = DateTime.MaxValue;
                            periodIndex = 4;
                        }
                        else
                        {
                            int m = date.Month;
                            periodIndex = (int)Math.Ceiling(((double)m / 3.0f));
                            m = (periodIndex - 1) * 3 + 1;
                            periodStart = new DateTime(year, m, 1);

                            m += 2;
                            periodEnd = new DateTime(year, m, DateTime.DaysInMonth(year, m));
                        }
                        periodName = string.Format("Q{0}", periodIndex);
                        break;

                    case PeriodTypes.CalendarYear:
                        periodIndex = 0;
                        periodStart = new DateTime(year, 1, 1);
                        if (IsMaxDate(date))
                        {
                            periodEnd = DateTime.MaxValue;
                        }
                        else
                        {
                            periodEnd = new DateTime(year, 12, 31);
                        }
                        periodName = "";
                        break;

                    case PeriodTypes.Week:
                        periodIndex = GetIso8601WeekOfYear(date);
                        if (IsMaxDate(date))
                        {
                            periodStart = date;
                            periodEnd = DateTime.MaxValue;
                        }
                        else
                        {
                            long dayDIff = date.DayOfWeek - DayOfWeek.Monday;
                            if (dayDIff < 0)
                            {
                                dayDIff += 7;
                            }
                            periodStart = date.AddDays(-dayDIff);
                            periodEnd = periodStart.AddDays(6);
                        }
                        periodName = string.Format("W{0}", periodIndex);
                        break;

                    default:
                        return;
                }

                if (_periods.Count == 0)
                {
                    _periods.Add(new TimePeriod(year, periodIndex, periodName, periodStart, periodEnd));
                }
                else if (_periods.Count == 1)
                {
                    if (_hasBlanks)
                    {
                        _periods.Add(new TimePeriod(year, periodIndex, periodName, periodStart, periodEnd));
                    }
                    else
                    {
                        if (year != _periods[0].Year || periodIndex != _periods[0].Period)
                        {
                            if (year < _periods[0].Year || (year == _periods[0].Year && periodIndex < _periods[0].Period))
                            {
                                _periods.Insert(0, new TimePeriod(year, periodIndex, periodName, periodStart, periodEnd));
                            }
                            else
                            {
                                _periods.Add(new TimePeriod(year, periodIndex, periodName, periodStart, periodEnd));
                            }
                        }
                        else
                        {
                            _periods[0].Count++;
                        }
                    }
                }
                else
                {
                    int startIndex = 0;
                    if (_hasBlanks)
                    {
                        startIndex = 1;
                    }
                    int index = FindPeriod(date, startIndex, _periods.Count - 1);

                    if (index == int.MinValue)
                    {
                        _periods.Insert(startIndex, new TimePeriod(year, periodIndex, periodName, periodStart, periodEnd));
                    }
                    else if (index == int.MaxValue)
                    {
                        _periods.Add(new TimePeriod(year, periodIndex, periodName, periodStart, periodEnd));
                    }
                    else
                    {
                        if (index < 0)
                        {
                            _periods.Insert(-index, new TimePeriod(year, periodIndex, periodName, periodStart, periodEnd));
                        }
                        else
                        {
                            _periods[index].Count++;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Checks if given date is Max date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private bool IsMaxDate(DateTime date)
        {
            if (date == DateTime.MaxValue)
            {
                return true;
            }
            else if (date.Year == 9999 && date.Month == 12 && date.Day == 31)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Find index of period list for a given date
        /// </summary>
        /// <param name="date"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        private int FindPeriod(DateTime date, int startIndex, int endIndex)
        {
            if (date < _periods[startIndex].StartDate)
            {
                if (startIndex == 0)
                {
                    return int.MinValue;
                }
                else
                {
                    return -startIndex;
                }
            }
            else if (date > _periods[endIndex].EndDate)
            {
                if (endIndex == _periods.Count - 1)
                {
                    return int.MaxValue;
                }
                else
                {
                    return -(endIndex + 1);
                }
            }

            if (startIndex == endIndex)
            {
                if (date >= _periods[startIndex].StartDate && date <= _periods[startIndex].EndDate)
                {
                    return startIndex;
                }
                else
                {
                    return -startIndex;
                }

            }
            else if (startIndex + 1 == endIndex)
            {
                if (date >= _periods[startIndex].StartDate && date <= _periods[startIndex].EndDate)
                {
                    return startIndex;
                }
                else if (date >= _periods[endIndex].StartDate && date <= _periods[endIndex].EndDate)
                {
                    return endIndex;
                }
                else
                {
                    return -endIndex;
                }
            }
            else
            {
                int middleIndex = startIndex + (int)((endIndex - startIndex) / 2.0);

                if (date <= _periods[middleIndex - 1].EndDate)
                {
                    return FindPeriod(date, startIndex, middleIndex - 1);
                }
                else
                {
                    if (middleIndex > endIndex)
                    {
                        middleIndex = endIndex;
                    }

                    return FindPeriod(date, middleIndex, endIndex);
                }
            }
        }

        /// <summary>
        /// Gets index of the date in the period list
        /// </summary>
        /// <param name="eventDate"></param>
        /// <returns></returns>
        public int IndexOf(DateTime? eventDate)
        {
            if (eventDate == null)
            {
                if (_hasBlanks)
                { return 0; }
                else
                { return -1; }
            }
            else
            {
                var date = (DateTime)eventDate;
                return FindPeriod(date, 0, _periods.Count - 1);
            }
        }

    }
}
