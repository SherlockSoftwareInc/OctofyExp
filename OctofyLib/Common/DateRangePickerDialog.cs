using System;
using System.Windows.Forms;

namespace OctofyLib
{
    public partial class DateRangePickerDialog
    {
        public DateRangePickerDialog()
        {
            InitializeComponent();
        }

        private readonly DateDuration _dateDuration = new DateDuration();

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
            if (tabControl1.SelectedIndex == 0)
            {
                if (thisYearRadioButton.Checked)
                {
                    StartDate = new DateTime(DateTime.Today.Year, 1, 1);
                    EndDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day - 1);
                    result = true;
                }
                else if (lastYearRadioButton.Checked)
                {
                    StartDate = new DateTime(DateTime.Today.Year - 1, 1, 1);
                    EndDate = new DateTime(DateTime.Today.Year - 1, 12, 31);
                    result = true;
                }
                else if (lastTwoYearsRadioButton.Checked)
                {
                    StartDate = new DateTime(DateTime.Today.Year - 2, 1, 1);
                    EndDate = new DateTime(DateTime.Today.Year - 1, 12, 31);
                    result = true;
                }
                else if (lastThreeYearsRadioButton.Checked)
                {
                    StartDate = new DateTime(DateTime.Today.Year - 3, 1, 1);
                    EndDate = new DateTime(DateTime.Today.Year - 1, 12, 31);
                    result = true;
                }
                else if (lastFiveYearsRadioButton.Checked)
                {
                    StartDate = new DateTime(DateTime.Today.Year - 5, 1, 1);
                    EndDate = new DateTime(DateTime.Today.Year - 1, 12, 31);
                    result = true;
                }
                else if (lastTenYearRadioButton.Checked)
                {
                    StartDate = new DateTime(DateTime.Today.Year - 10, 1, 1);
                    EndDate = new DateTime(DateTime.Today.Year - 1, 12, 31);
                    result = true;
                }
                else
                {
                    if (specifyDateRadioButton.Checked)
                    {
                        StartDate = startDateDateTimePicker.Value;
                        EndDate = endDateDateTimePicker.Value;
                        result = true;
                    }
                }
            }

            if (result)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show(Properties.Resources.B013); //B013: Please select reporting date range.
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void DateRangePickerDialog_Load(object sender, EventArgs e)
        {
        }

        private void SpecifyDate_CheckedChanged(object sender, EventArgs e)
        {
            specifyDateGroupBox.Enabled = specifyDateRadioButton.Checked;
        }

    }
}