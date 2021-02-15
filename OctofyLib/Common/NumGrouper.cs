using System;
using System.Collections.Generic;
using System.IO;
namespace OctofyLib
{
    public class NumGrouper
    {
        private readonly List<TimePeriod> _periods;
        private bool _hasBlanks = false;

        public NumGrouper()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<TimePeriod> Items
        {
            get
            {
                return _periods;
            }
        }

        /// <summary>
        /// Open date grouper from a list of values and period type
        /// </summary>
        /// <param name="groupBy"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public bool Open(double groupBy, List<string> values)
        {
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

                if (_periods.Count == 0)
                {
                    //_periods.Add(new TimePeriod(year, periodIndex, periodName, periodStart, periodEnd));
                }
                else if (_periods.Count == 1)
                {
                    if (_hasBlanks)
                    {
                        //_periods.Add(new TimePeriod(year, periodIndex, periodName, periodStart, periodEnd));
                    }
                    else
                    {
                        //if (year != _periods[0].Year || periodIndex != _periods[0].Period)
                        //{
                        //    if (year < _periods[0].Year || (year == _periods[0].Year && periodIndex < _periods[0].Period))
                        //    {
                        //        _periods.Insert(0, new TimePeriod(year, periodIndex, periodName, periodStart, periodEnd));
                        //    }
                        //    else
                        //    {
                        //        _periods.Add(new TimePeriod(year, periodIndex, periodName, periodStart, periodEnd));
                        //    }
                        //}
                        //else
                        //{
                        //    _periods[0].Count++;
                        //}
                    }
                }
                else
                {
                    //int startIndex = 0;
                    //if (_hasBlanks)
                    //{
                    //    startIndex = 1;
                    //}
                    //int index = FindPeriod(date, startIndex, _periods.Count - 1);

                    //if (index == int.MinValue)
                    //{
                    //    _periods.Insert(startIndex, new TimePeriod(year, periodIndex, periodName, periodStart, periodEnd));
                    //}
                    //else if (index == int.MaxValue)
                    //{
                    //    _periods.Add(new TimePeriod(year, periodIndex, periodName, periodStart, periodEnd));
                    //}
                    //else
                    //{
                    //    if (index < 0)
                    //    {
                    //        _periods.Insert(-index, new TimePeriod(year, periodIndex, periodName, periodStart, periodEnd));
                    //    }
                    //    else
                    //    {
                    //        _periods[index].Count++;
                    //    }
                    //}
                }
            }
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
