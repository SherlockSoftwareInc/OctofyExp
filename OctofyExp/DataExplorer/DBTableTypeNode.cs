using System.Data;
using System.Windows.Forms;

namespace OctofyExp
{
    class DBTableTypeNode : TreeNode
    {
        public DBTableTypeNode(string name, string schemaName, string tableType, DataTable dataTable)
            : base(name)
        {
            //var matches = dataTable.Select(string.Format("TABLE_SCHEMA = '{0}' AND TABLE_TYPE = '{1}'", schemaName, tableType), "TABLE_NAME");
            this.SchemaName = schemaName;
            this.TableType = tableType;

            foreach (DataRow dr in dataTable.Rows)
            {
                this.Nodes.Add(new DBTableViewNode(tableType, schemaName, dr["TABLE_NAME"].ToString()));
            }
        }

        public string SchemaName { get; set; }
        public string TableType { get; set; }
    }
}
