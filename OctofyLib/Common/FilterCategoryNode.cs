using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;

namespace OctofyLib
{
    /// <summary>
    /// 
    /// </summary>
    public class FilterCategoryNode : TreeNode
    {
        private readonly List<string> _items = new List<string>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tv"></param>
        /// <param name="options"></param>
        public FilterCategoryNode(TreeView tv, FilterColumnItem options) :
            base(options.Caption)
        {
            Virtualize();
            ColumnName = options.ColumnName;

            if (_items.Count > 0)
                _items.Clear();

            foreach (var item in options.DistinctValues)
            {
                string itemName = item;
                if (item.Length == 0)
                {
                    itemName = Properties.Resources.B003;   // "(Blanks)";
                }
                _items.Add(itemName);
            }

            Checked = true;
        }

        /// <summary>
        /// Gets or sets column name
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// Returns a value indicates whether the child 
        /// nodes (categories) have been loaded or not
        /// </summary>
        public bool Loaded
        {
            get
            {
                if (Nodes.Count != 0)
                {
                    if (Nodes[0].GetType() == typeof(FakeChildNode))
                        return false;
                }
                return true;
            }
        }

        /// <summary>
        /// Returns number of items to load in the category
        /// </summary>
        public int NumToLoad { get { return _items.Count; } }

        /// <summary>
        /// Load real child nodes
        /// </summary>
        public void LoadValues()
        {
            foreach (var item in _items)
            {
                string itemName = item;
                if (item.Length == 0)
                {
                    itemName = Properties.Resources.B003;   // "(Blanks)";
                }

                var itemNode = Nodes.Add(itemName);
                itemNode.Checked = true;
            }
            _items.Clear();
        }

        /// <summary>
        /// Add a fake child node instead loading a long category items
        /// to save the time. The real items will be load when expend the
        /// node
        /// </summary>
        private void Virtualize()
        {
            try
            {
                var node = new FakeChildNode(this);
            }
            catch (System.Exception)
            {
            }
        }

        /// <summary>
        /// Check a child node by node name (value)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool CheckNodeByName(string value)
        {
            if (value.Length == 0 | value == Properties.Resources.B003)
            {
                foreach (TreeNode node in Nodes)
                {
                    if (node.Name == Properties.Resources.B003)
                    {
                        node.Checked = true;
                        return true;
                    }
                }
            }
            else
            {
                foreach (TreeNode node in Nodes)
                {
                    if (string.Compare(node.Text.Trim(), value.Trim(), true) == 0)
                    {
                        node.Checked = true;
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if all children node have been selected in a specified node
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool IsAllChildrenSelected()
        {
            bool result = true;
            foreach (TreeNode child in Nodes)
            {
                if (!child.Checked)
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// Checks all children nodes
        /// </summary>
        public void SelectAll()
        {
            if (Nodes.Count > 0)
            {
                foreach (TreeNode child in Nodes)
                    child.Checked = true;
            }
        }

        /// <summary>
        /// Uncheck all children nodes
        /// </summary>
        public void UnselectAll()
        {
            if (Nodes.Count > 0)
            {
                foreach (TreeNode child in Nodes)
                    child.Checked = false;
            }
        }

        /// <summary>
        /// Build selections in a xml document
        /// </summary>
        /// <param name="writer"></param>
        public void WriteXML(System.Xml.XmlWriter writer)
        {
            var hasUnchecked = default(bool);
            if (Nodes.Count > 0 && Loaded)
            {
                foreach (TreeNode node in Nodes)
                {
                    if (!node.Checked)
                    {
                        hasUnchecked = true;
                        break;
                    }
                }
            }

            if (hasUnchecked)
            {
                writer.WriteStartElement("Column");
                //writer.WriteStartElement(ColumnName);
                //writer.WriteAttributeString("AllSelected", hasUnchecked ? "False" : "True");
                //writer.WriteValue(CategoryName);
                writer.WriteAttributeString("Name", ColumnName);

                foreach (TreeNode node in Nodes)
                {
                    if (node.Checked)
                    {
                        writer.WriteStartElement("item");
                        writer.WriteValue(node.Text);
                        writer.WriteEndElement();
                    }
                }

                writer.WriteEndElement();
            }
        }

        /// <summary>
        /// check/uncheck child nodes from the xml node
        /// </summary>
        /// <param name="xml"></param>
        public void ParseXML(XmlNode xml)
        {
            Expand();

            Checked = false;
            foreach (TreeNode node in Nodes)
                node.Checked = false;

            if (xml.HasChildNodes)
            {
                foreach (XmlNode xnode in xml.ChildNodes)
                {
                    CheckNodeByName(xnode.InnerText);
                }
            }

            if (IsAllChildrenSelected() == true)
            {
                Checked = true;
                //this.Collapse();
            }
        }
    }
}