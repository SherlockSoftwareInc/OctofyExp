using System;
using System.Windows.Forms;

namespace OctofyLib
{
    class DateRangeNode : TreeNode
    {
        public DateRangeNode(DateTime startDate, DateTime endDate)
        {
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Text = ToString();
            this.Checked = true;
        }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public override string ToString()
        {
            return string.Format(Properties.Resources.B012, StartDate.ToShortDateString(), EndDate.ToShortDateString());
        }
    }
}
