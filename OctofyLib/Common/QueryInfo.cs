using System;
using System.Collections.Generic;

namespace OctofyLib
{
    public class QueryInfo
    {
        public enum OpenResults
        {
            Failed,
            Success,
            InvalidCommand,
            InvalidParameters
        }

        public short DataSourceType { get; set; }
        public int ConnectionID { get; set; }
        public string Command { get; set; } = "";
        public bool HasDate { get; set; }
        public DateTime StartDate { get; set; } = DateTime.MinValue;
        public DateTime EndDate { get; set; } = DateTime.MinValue;
        public List<SPParameterItem> SPParameters { get; set; } = new List<SPParameterItem>();
        public OpenResults OpenResult { get; set; }

        public bool Success
        {
            get
            {
                return ConnectionID > 0 & Command.Length > 1;
            }
        }
    }
}