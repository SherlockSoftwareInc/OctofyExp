using System;

namespace OctofyLib
{
    public class MarkerDataItem
    {
        public MarkerDataItem()
        {
            ID = string.Empty;
            Caption = string.Empty;
            CaptionLine2 = string.Empty;
            StartDate = DateTime.MinValue;
            EndDate = DateTime.MaxValue;
            Value = string.Empty;
        }

        public string ID { get; set; }
        public string Caption { get; set; }
        public string CaptionLine2 { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            if (CaptionLine2.Length > 0)
            {
                return string.Format("{0} {1}", Caption, CaptionLine2);
            }
            else
            {
                return Caption;
            }
        }
    }
}