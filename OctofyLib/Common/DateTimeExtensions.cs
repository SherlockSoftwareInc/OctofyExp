using System;

namespace OctofyLib
{
    /// <summary>
    /// 
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventDate"></param>
        /// <param name="eventDate2"></param>
        /// <returns></returns>
        public static bool IsSameDay(this DateTime eventDate, DateTime eventDate2)
        {
            if (eventDate.Year == eventDate2.Year)
                if (eventDate.Month == eventDate2.Month)
                    if (eventDate.Day == eventDate2.Day)
                        return true;
            return false;
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="dt"></param>
        ///// <param name="birthday"></param>
        ///// <returns></returns>
        //public static int DateTimeMonth(this DateTime dt, DateTime birthday)
        //{
        //    int difference = birthday.Month - dt.Month;
        //    if (difference < 0)
        //    {
        //        difference += 12;
        //    }
        //    return difference;
        //}
    }
}
