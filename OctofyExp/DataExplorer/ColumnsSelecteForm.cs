using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OctofyExp
{
    public partial class ColumnsSelecteForm : Form
    {
        readonly private List<string> _selectedColumn = new List<string>();
        public ColumnsSelecteForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Selectable columns of a table or view
        /// </summary>
        public List<string> Columns { get; set; }

        /// <summary>
        /// Selected columns
        /// </summary>
        public List<string> SelectedColumns { get { return _selectedColumn; } }

        /// <summary>
        /// Form load event handle: populate selectable columns
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ColumnsSelecteForm_Load(object sender, EventArgs e)
        {
            foreach (var item in Columns)
            {
                columnsCheckedListBox.Items.Add(item);
            }
        }

        /// <summary>
        /// "Selecte All" tool button click event handle:
        /// Set all item state to true
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectAllToolStripButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < columnsCheckedListBox.Items.Count; i++)
            {
                columnsCheckedListBox.SetItemChecked(i, true);
            }
        }

        /// <summary>
        /// "Clear" tool button click event handle:
        /// Set all item state to false
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearToolStripButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < columnsCheckedListBox.Items.Count; i++)
            {
                columnsCheckedListBox.SetItemChecked(i, false);
            }
        }

        /// <summary>
        /// OK button click event handle:
        ///     Colse dialog and return all selected items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OKToolStripButton_Click(object sender, EventArgs e)
        {
            foreach (string item in columnsCheckedListBox.CheckedItems)
            {
                _selectedColumn.Add(item);
            }
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Cancel button click event handle:
        ///     Close dialog and return nothing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelToolStripButton_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// Enable or disable OK button when checkedListBox item check
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ColumnsCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            okToolStripButton.Enabled = (columnsCheckedListBox.CheckedItems.Count > 0);
        }
    }
}
