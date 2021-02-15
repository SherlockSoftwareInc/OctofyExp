using OctofyLib;
using System;
using System.Windows.Forms;

namespace DBExpo
{
    public partial class DateRangePickerDialog
    {
        public DateRangePickerDialog()
        {
            InitializeComponent();
        }

        //private readonly WaitlistDataSource.FiscalPeriod _period;
        //private DateTime _startDate;
        //private DateTime _endDate;
        private DateDuration _dateDuration = new OctofyLib.DateDuration();

        public DateTime StartDate
        {
            get
            {
                return _dateDuration.StartDate;
            }

            set
            {
                _dateDuration.StartDate = value;
            }
        }

        public DateTime EndDate
        {
            get
            {
                return _dateDuration.EndDate;
            }

            set
            {
                _dateDuration.EndDate = value;
            }
        }

        //public string ConnectionString { get; set; } = string.Empty;

        private void OkButton_Click(object sender, EventArgs e)
        {
            var result = default(bool);
            if (rbtnThisYear.Checked)
            {
                StartDate = new DateTime(DateTime.Today.Year, 1, 1);
                EndDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day - 1);
                result = true;
            }
            else if (rbtnLastYear.Checked)
            {
                StartDate = new DateTime(DateTime.Today.Year - 1, 1, 1);
                EndDate = new DateTime(DateTime.Today.Year - 1, 12, 31);
                result = true;
            }
            else if (rbtnLastTwoYears.Checked)
            {
                StartDate = new DateTime(DateTime.Today.Year - 2, 1, 1);
                EndDate = new DateTime(DateTime.Today.Year - 1, 12, 31);
                result = true;
            }
            else if (rbtnLstThreeYears.Checked)
            {
                StartDate = new DateTime(DateTime.Today.Year - 3, 1, 1);
                EndDate = new DateTime(DateTime.Today.Year - 1, 12, 31);
                result = true;
            }
            else if (rbtnLastFiveYears.Checked)
            {
                StartDate = new DateTime(DateTime.Today.Year - 5, 1, 1);
                EndDate = new DateTime(DateTime.Today.Year - 1, 12, 31);
                result = true;
            }
            else
            {
                if (rbtnSpecifyDate.Checked)
                {
                    StartDate = dtpStart.Value;
                    EndDate = dtpEnd.Value;
                    result = true;
                }
            }

            if (result)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Please select reporting date range.");
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void DateRangePickerDialog_Load(object sender, EventArgs e)
        {
            //short fiscalYear = WaitlistDataSource.FiscalPeriod.ThisFiscalYear();
            //var dictFrom = new Dictionary<string, short>();
            //var dictTo = new Dictionary<string, short>();
            //for (int i = 2007, loopTo = fiscalYear; i <= loopTo; i++)
            //{
            //    string strFYear = string.Format("{0}-{1}", i, i + 1);
            //    dictFrom.Add(strFYear, (short)i);
            //    dictTo.Add(strFYear, (short)i);
            //}
        }

        private void SpecifyDate_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = rbtnSpecifyDate.Checked;
        }
    }
}