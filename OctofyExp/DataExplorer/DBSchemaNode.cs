using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace OctofyExp
{
    class DBSchemaNode : TreeNode
    {
        public DBSchemaNode(string schemaName, DataTable dataTable)
            : base(schemaName)
        {
            //var matches = dataTable.Select(string.Format("TABLE_SCHEMA = '{0}'", schemaName));
            var tableTypes = new List<string>();
            foreach (DataRow dr in dataTable.Rows)
            {
                string tableType = dr["TABLE_TYPE"].ToString();
                if (!tableTypes.Contains(tableType))
                    tableTypes.Add(tableType);
            }

            tableTypes.Sort();
            foreach (var item in tableTypes)
            {
                if (!IsExcludeType(item))
                {
                    var matches = dataTable.Select(string.Format("TABLE_SCHEMA = '{0}' AND TABLE_TYPE = '{1}'", schemaName, item), "TABLE_NAME");
                    if (matches.Length > 0)
                    {
                        this.Nodes.Add(new DBTableTypeNode(item, schemaName, item, matches.CopyToDataTable()));
                    }
                }
            }
            //this.Nodes.Add(new DBTableTypeNode("Table", schemaName, "BASE TABLE", dataTable));
            //this.Nodes.Add(new DBTableTypeNode("View", schemaName, "VIEW", dataTable));
        }

        public static bool IsExcludeType(string tableType)
        {
            switch (tableType.ToUpper())
            {
                //case "":
                case "SYSTEM TABLE":
                case "SYSTEM VIEW":
                    return true;
                default:
                    return false;
            }
        }
    }
}
