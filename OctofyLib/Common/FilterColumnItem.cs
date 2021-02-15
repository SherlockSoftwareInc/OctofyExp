using System.Collections.Generic;

namespace OctofyLib
{
    public class FilterColumnItem
    {
        private readonly List<string> _values = new List<string>();

        public string ColumnName { get; set; }

        public string Caption
        {
            get
            {
                return ColumnName;
            }
        }

        public TableColumn.ColumnTypes ColumnType { get; set; }

        //public DataTable DataSource
        //{
        //    set
        //    {
        //        if (_values.Count > 0)
        //        {
        //            _values.Clear();
        //        }

        //        foreach (DataRow dr in value.Rows)
        //            _values.Add(dr[0].ToString());
        //        _values.Sort();
        //    }
        //}

        public List<string> DistinctValues
        {
            get
            {
                return _values;
            }
            set
            {
                _values.Clear();
                foreach (var item in value)
                    _values.Add(item);
                _values.Sort();
            }
        }
    }
}