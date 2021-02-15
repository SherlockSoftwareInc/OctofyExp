using System.Collections.Generic;

namespace OctofyExp.DataExplorer
{
    public class ExcludedColumns
    {
        public ExcludedColumns()
        {
        }

        private readonly List<ExcludeColumn> _columns = new List<ExcludeColumn>();

        public string TableName { get; set; }

        public List<ExcludeColumn> Columns
        {
            get { return _columns; }
        }

        public void Clear()
        {
            _columns.Clear();
        }

        public void Add(string columnName, string reason)
        {
            _columns.Add(new ExcludeColumn(columnName, reason));
        }
    }
}
