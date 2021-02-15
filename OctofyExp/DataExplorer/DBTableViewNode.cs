using System.Windows.Forms;

namespace OctofyExp
{
    class DBTableViewNode : TreeNode
    {
        public DBTableViewNode(string tableType, string schemaName, string tableName)
            : base(tableName)
        {
            this.TableType = tableType;
            this.SchemaName = schemaName;
            this.TableName = tableName;
        }

        public string SchemaName { get; set; }

        public string TableName { get; set; }

        public string TableType { get; set; }

    }
}
