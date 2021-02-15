using System;

namespace DBExpo
{
    class TimePeriod
    {
        public TimePeriod(int year, string period, DateTime startDate, DateTime endDate)
        {
            this.Year = year;
            this.Period = period;
            this.StartDate = startDate;
            this.EndDate = endDate;
        }

        public TimePeriod(TimePeriod value)
        {
            this.Year = value.Year;
            this.Period = value.Period;
            this.StartDate = value.StartDate;
            this.EndDate = value.EndDate;
        }

        public int Year { get; set; }
        public string Period { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public override string ToString()
        {
            if (Year == 0)
                return "(Blanks)";
            if (Period.Length == 0)
                return Year.ToString();
            return String.Format("{0}/{1}", Year, Period);
        }
    }

}
