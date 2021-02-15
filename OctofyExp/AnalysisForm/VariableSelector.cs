using OctofyExp.DataExplorer;
using OctofyLib;
using System;
using System.Windows.Forms;

namespace OctofyExp
{
    public partial class VariableSelector : UserControl
    {
        public event EventHandler SelectionChanged;
        public event EventHandler SelectedDateGroupTypeChanged;

        private class ColumnNameListBoxItem
        {
            public ColumnNameListBoxItem(string columnName)
            {
                ColumnName = columnName;
            }

            public string ColumnName { get; set; }

            public bool IsDateColumn { get; set; }

            public override string ToString()
            {
                return ColumnName.ConvertPascalName();
            }
        }

        public VariableSelector()
        {
            InitializeComponent();
        }

        private DataAnalysisForm.Views _variableType;
        private bool _init;
        private ReportingDates.PeriodTypes _dateGroupType = ReportingDates.PeriodTypes.None;
        private readonly ExcludedColumns _excludedColumns = new ExcludedColumns();
        private TableAnalysis _dataSource;
        string _selectedColumn = "";

        #region"Properties"
        public bool AllowDateColumn { get; set; }

        /// <summary>
        /// Gets caption of selected variable
        /// </summary>
        public string Caption
        {
            get
            {
                if (columnListBox.SelectedItem != null)
                {
                    return ((ColumnNameListBoxItem)columnListBox.SelectedItem).ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Gets or set date column name
        /// </summary>
        public string DateColumnName { get; set; } = "";

        /// <summary>
        /// Gets or set maximum members for a category
        /// </summary>
        public int MaxMembers { get; set; } = 32768;

        /// <summary>
        /// Gets selected column
        /// </summary>
        public string SelectedVariable
        {
            get
            {
                if (columnListBox.SelectedItem != null)
                {
                    return ((ColumnNameListBoxItem)columnListBox.SelectedItem).ColumnName;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Gets or set title
        /// </summary>
        public string MeasurementColumn
        {
            get { return measurementColumnLabel.Text; }
            set { measurementColumnLabel.Text = value; }
        }

        /// <summary>
        /// Gets or sets variable type (chart type)
        /// </summary>
        public DataAnalysisForm.Views VariableType
        {
            get
            {
                return _variableType;
            }
            set
            {
                if (_variableType != value)
                {
                    _variableType = value;
                    Populate();
                    PerformLayout();
                }
            }
        }
        #endregion

        public void Open(TableAnalysis dataSource, DataAnalysisForm.Views variableType)
        {
            _dataSource = dataSource;
            _variableType = variableType;
            Populate();
            PerformLayout();
        }

        public void Populate()
        {
            string currentSelection = _selectedColumn;
            columnListBox.Items.Clear();
            infoButton.Visible = false;
            _excludedColumns.Clear();

            bool hasColumn = false;
            int categoryCount;
            foreach (var item in _dataSource.Columns)
            {
                if (item.ColumnType == TableColumn.ColumnTypes.CategoryExceedMaxMembers)
                {
                    _excludedColumns.Add(item.ColumnName, Properties.Resources.A049);   //A049: Number of categories exceed the maximum setting
                }
                else if (item.ColumnType == TableColumn.ColumnTypes.CategoryExceedMaxLength)
                {
                    _excludedColumns.Add(item.ColumnName, Properties.Resources.A050);   //A050: Category text length exceed the maximum setting
                }
                else if (item.ColumnType == TableColumn.ColumnTypes.Other)
                {
                    _excludedColumns.Add(item.ColumnName, Properties.Resources.A051);   //A051: Non-categorical column
                }
                else if (!AllowDateColumn && item.ColumnType == TableColumn.ColumnTypes.Date)
                {
                    _excludedColumns.Add(item.ColumnName, Properties.Resources.A052);   //A052: Date time column
                }
                else
                {
                    categoryCount = item.GetCount(true);
                    if (categoryCount == 0)
                    {
                        _excludedColumns.Add(item.ColumnName, Properties.Resources.A053);   //A053: Empty column
                    }
                    else if (categoryCount > MaxMembers && item.ColumnType != TableColumn.ColumnTypes.Date)
                    {
                        _excludedColumns.Add(item.ColumnName, Properties.Resources.A049);
                    }
                    else
                    {
                        if (item.ColumnType == TableColumn.ColumnTypes.Category)
                        {
                            columnListBox.Items.Add(new ColumnNameListBoxItem(item.ColumnName) { IsDateColumn = false });
                            hasColumn = true;
                        }
                        else if (item.ColumnType == TableColumn.ColumnTypes.Date)
                        {
                            columnListBox.Items.Add(new ColumnNameListBoxItem(item.ColumnName) { IsDateColumn = true });
                            hasColumn = true;
                        }
                    }
                }
            }

            if (_excludedColumns.Columns.Count > 0)
            {
                infoButton.Visible = true;
            }

            if (hasColumn)
            {
                _init = true;

                int indexY = 0;
                if (currentSelection.Length > 0)
                {
                    indexY = GetListBoxIndex(columnListBox, currentSelection);
                }

                columnListBox.SelectedIndex = indexY;
                _selectedColumn = SelectedVariable;

                _init = false;
            }
        }

        private int GetListBoxIndex(ListBox listBox, string value)
        {
            int result = 0;
            for (int i = 0; i < listBox.Items.Count; i++)
            {
                ColumnNameListBoxItem item = (ColumnNameListBoxItem)listBox.Items[i];
                if (item.ColumnName == value)
                {
                    result = i;
                    break;
                }
            }
            return result;
        }


        private void Variable_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItem = (ColumnNameListBoxItem)columnListBox.SelectedItem;

            if (!_init)
            {
                _selectedColumn = selectedItem.ColumnName;
                SelectionChanged?.Invoke(this, e);
            }
        }


        private void TitlePanel_Resize(object sender, EventArgs e)
        {
            if (titlePanel.Height != infoButton.Height)
                titlePanel.Height = infoButton.Height;
        }

        private void InfoButton_Click(object sender, EventArgs e)
        {
            using (var form = new ExcludeColumnsForm() { DataSource = _excludedColumns })
            {
                form.ShowDialog();
            }
        }
    }
}
