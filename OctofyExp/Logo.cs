using System;
using System.Windows.Forms;

namespace OctofyExp
{
    public partial class Logo : Form
    {
        public Logo()
        {
            InitializeComponent();
        }

        private void Logo_Load(object sender, EventArgs e)
        {
            octofyRing1.Animation = true;
        }
    }
}
