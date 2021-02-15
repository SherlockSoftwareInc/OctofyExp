namespace OctofyExp
{
    public class ExcludeColumn
    {
        public ExcludeColumn(string columnName, string reason)
        {
            this.ColumnName = columnName;
            this.Reason = reason;
        }

        public string ColumnName { get; set; }
        public string Reason { get; set; }
    }
}
