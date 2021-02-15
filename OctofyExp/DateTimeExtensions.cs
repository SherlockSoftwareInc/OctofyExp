using System;

namespace OctofyExp
{
    public static class DateTimeExtensions
    {
        public static bool IsSameDay(this DateTime eventDate1, DateTime eventDate2)
        {
            if (eventDate1.Year == eventDate2.Year)
                if (eventDate1.Month == eventDate2.Month)
                    if (eventDate1.Day == eventDate2.Day)
                        return true;
            return false;
        }
    }
}
