using System.Windows.Forms;

namespace OctofyLib
{
    [System.Serializable]
    class DateColumnNode : TreeNode
    {
        public DateColumnNode(FilterColumnItem parameters) :
             base(parameters.Caption)
        {
            ColumnName = parameters.ColumnName;
        }

        public string ColumnName { get; set; }

        /// <summary>
        /// Build selections in a xml document
        /// </summary>
        /// <param name="writer"></param>
        public void WriteXML(System.Xml.XmlWriter writer)
        {
            if (Nodes.Count == 0) return;   //no setting
            if (!Checked) return;   // not selected

            DateRangeNode dateRangeNode = (DateRangeNode)Nodes[0];

            writer.WriteStartElement(ColumnName);

            writer.WriteStartElement("StartDate");
            writer.WriteValue(dateRangeNode.StartDate.ToString());
            writer.WriteEndElement();
            writer.WriteStartElement("EndDate");
            writer.WriteValue(dateRangeNode.EndDate.ToString());
            writer.WriteEndElement();

            writer.WriteEndElement();
        }
    }
}
