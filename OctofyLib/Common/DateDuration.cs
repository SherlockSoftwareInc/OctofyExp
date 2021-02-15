using System;

namespace OctofyLib
{
    public class DateDuration
    {
        private DateTime _startDate;
        private DateTime _endDate;

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                if (value != null)
                    _startDate = new DateTime(value.Year, value.Month, value.Day, 0, 0, 0);
            }
        }


        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                if (value != null)
                    _endDate = new DateTime(value.Year, value.Month, value.Day, 23, 59, 59, 997);
            }
        }
    }
}
