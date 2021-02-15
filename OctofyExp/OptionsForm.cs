using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace OctofyExp
{
    public partial class OptionsForm : Form
    {
        public OptionsForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handle form load event:
        ///     Load values from settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OptionsForm_Load(object sender, EventArgs e)
        {
            maxYTextBox.Text = Properties.Settings.Default.MaxYCategories.ToString();
            maxXTextBox.Text = Properties.Settings.Default.MaxXCategories.ToString();
            topRowsTextBox.Text = Properties.Settings.Default.NumOfRowOnDoubleClick.ToString();
            timeoutTextBox.Text = Properties.Settings.Default.ConnectionTimeout.ToString();
        }

        /// <summary>
        /// Handle maximum y-axis setting change event:
        ///     The input should be numeric only and value should be between 256 and 9,999,999
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MaxYTextBox_Validating(object sender, CancelEventArgs e)
        {
            string strValue = maxYTextBox.Text;
            if (!strValue.IsNumeric())
            {
                MessageBox.Show(Properties.Resources.A071, Properties.Resources.A072,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                //A071: Please enter numeric only.
                //A072: Invalid input
                e.Cancel = true;
                return;
            }

            long value = long.Parse(strValue);
            const long maxValue = Int32.MaxValue / 26;
            if (value < 256 || value > (maxValue))
            {
                MessageBox.Show(string.Format(Properties.Resources.A073, maxValue.ToString("N0")),
                    Properties.Resources.A072, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //A073: Please enter a number between 256 and {0}.
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Handle maximum x-axis setting change event:
        ///     The input should be numeric only and value should be between 16 and 128
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MaxXTextBox_Validating(object sender, CancelEventArgs e)
        {
            string strValue = maxXTextBox.Text;
            if (!strValue.IsNumeric())
            {
                MessageBox.Show(Properties.Resources.A071, Properties.Resources.A072,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }

            long value = long.Parse(strValue);
            if (value < 16 || value > 128)
            {
                MessageBox.Show(Properties.Resources.A074, Properties.Resources.A072,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                //A074: Please enter a number between 16 and 128.
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Handle numer of rows for quick analysis setting change event:
        ///     The input should be numeric only and value should be between 100 and 999,999
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TopRowsTextBox_Validating(object sender, CancelEventArgs e)
        {
            string strValue = topRowsTextBox.Text;
            if (!strValue.IsNumeric())
            {
                MessageBox.Show(Properties.Resources.A071, Properties.Resources.A072,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }

            long value = long.Parse(strValue);
            if (value < 100 || value > 999999)
            {
                MessageBox.Show(Properties.Resources.A075, Properties.Resources.A072,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                //A075: Please enter a number between 100 and 999999.
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Handle OK button click event:
        ///     Save the setting and close the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OkButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.MaxYCategories = int.Parse(maxYTextBox.Text);
            Properties.Settings.Default.MaxXCategories = int.Parse(maxXTextBox.Text);
            Properties.Settings.Default.NumOfRowOnDoubleClick = int.Parse(topRowsTextBox.Text);
            Properties.Settings.Default.ConnectionTimeout = int.Parse(timeoutTextBox.Text);
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Handle cancel button click event:
        ///     Close the form without save the settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimeoutTextBox_Validating(object sender, CancelEventArgs e)
        {
            string strValue = timeoutTextBox.Text;
            if (!strValue.IsNumeric())
            {
                MessageBox.Show(Properties.Resources.A071, Properties.Resources.A072,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }
        }
    }
}
