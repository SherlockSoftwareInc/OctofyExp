using System.Collections.Generic;

namespace OctofyLib
{
    /// <summary>
    /// 
    /// </summary>
    public class FilterColumns
    {
        private readonly List<FilterColumnItem> _columns;

        /// <summary>
        /// 
        /// </summary>
        public FilterColumns()
        {
            _columns = new List<FilterColumnItem>();
        }

        /// <summary>
        /// 
        /// </summary>
        public List<FilterColumnItem> Columns
        {
            get
            {
                return _columns;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ta"></param>
        public void BuildFilterColumns(TableAnalysis ta)
        {
            if (_columns.Count > 0)
            {
                _columns.Clear();
            }
            for (int i = 0; i < ta.Columns.Count; i++)
            {
                string columnName = ta.Columns[i].ColumnName;
                if (ta.Columns[i].ColumnType != TableColumn.ColumnTypes.Other)
                {
                    var item = new FilterColumnItem()
                    {
                        ColumnName = columnName,
                        ColumnType = ta.Columns[i].ColumnType,
                        DistinctValues = ta.Columns[i].GetUniqueValues(false)
                    };
                    _columns.Add(item);
                }
            }
        }

    }
}