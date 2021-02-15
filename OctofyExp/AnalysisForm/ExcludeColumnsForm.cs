using OctofyExp.DataExplorer;
using System;
using System.Windows.Forms;

namespace OctofyExp
{
    public partial class ExcludeColumnsForm : Form
    {
        public ExcludeColumnsForm()
        {
            InitializeComponent();
        }

        private void ExcludeColumnsForm_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = DataSource.Columns;
            dataGridView1.AutoResizeColumns();
        }

        public ExcludedColumns DataSource { get; set; }

        private void CloseToolStripButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
