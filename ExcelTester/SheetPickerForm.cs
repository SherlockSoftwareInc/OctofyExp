using System;
using System.Data;
using System.Windows.Forms;

namespace ExcelTester
{
    public partial class SheetPickerForm : Form
    {
        private DataTable _resultDataTable;

        public SheetPickerForm()
        {
            InitializeComponent();
        }

        public string FileName { get; set; }

        public DataTable ExcelSheets { get; set; }

        public string SelectedSheet { get; set; }

        private void sheetsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            okButton.Enabled = true;
        }

        private void SheetPickerForm_Load(object sender, EventArgs e)
        {
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            SelectedSheet = sheetsListBox.SelectedItem.ToString();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }




        //private string SelectASheet(DataTable dtSheets)
        //{
        //    string result = "";
        //    using (var dlg = new SheetPickerForm() { ExcelSheets = dtSheets })
        //    {
        //        if (dlg.ShowDialog() == DialogResult.OK)
        //        {
        //            result = dlg.SelectedSheet;
        //        }
        //    }

        //    return result;
        //}

    }
}