using System;
using System.Windows.Forms;

namespace OctofyExp
{
    public partial class SearchOptionsDialog : Form
    {
        public SearchOptionsDialog()
        {
            InitializeComponent();
        }

        private void SearchOptionsDialog_Load(object sender, EventArgs e)
        {
            switch (Properties.Settings.Default.SearchPattern)
            {
                case 0:
                    startWithRadioButton.Checked = true;
                    break;

                case 1:
                    endWithRadioButton.Checked = true;
                    break;

                case 3:
                    matchRadioButton.Checked = true;
                    break;

                default:
                    containsRadioButton.Checked = true;
                    break;
            }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            if (startWithRadioButton.Checked)
                Properties.Settings.Default.SearchPattern = 0;
            else if (endWithRadioButton.Checked)
                Properties.Settings.Default.SearchPattern = 1;
            else if (matchRadioButton.Checked)
                Properties.Settings.Default.SearchPattern = 3;
            else
                Properties.Settings.Default.SearchPattern = 2;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
