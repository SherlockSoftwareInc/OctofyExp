using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace OctofyLib
{
    public partial class FilterPanel
    {
        /// <summary>
        /// Filter data set by selected value in this tree view
        /// </summary>
        public FilterPanel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Tree value node selection changed event
        /// </summary>
        public event EventHandler SelectionChanged;

        private bool _ignoreCheckChange;    //do not fire change event when it's true

        ///// <summary>
        ///// Find a category node by category name
        ///// </summary>
        ///// <param name="catName"></param>
        ///// <returns></returns>
        //private FilterCategoryNode GetCategoryNodeByName(string catName)
        //{
        //    foreach (var node in filterTree.Nodes)
        //    {
        //        if (node is FilterCategoryNode)
        //        {
        //            if (string.Compare(((FilterCategoryNode)node).Text, catName, true) == 0)
        //            {
        //                return (FilterCategoryNode)node;
        //            }
        //        }
        //    }

        //    return null;
        //}

        /// <summary>
        /// Get selected values
        /// </summary>
        public string SelectedValues
        {
            get
            {
                var sb = new StringBuilder();
                var settings = new XmlWriterSettings() { OmitXmlDeclaration = true, Indent = true, NewLineOnAttributes = false };
                using (var writer = XmlWriter.Create(sb, settings))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("root");

                    foreach (var node in filterTree.Nodes)
                    {
                        if (node is FilterCategoryNode catNode)
                        {
                            catNode.WriteXML(writer);
                        }
                        else if (node is DateColumnNode dateNode)
                        {
                            dateNode.WriteXML(writer);
                        }
                    }

                    writer.WriteEndElement();    //end sf:root
                    writer.WriteEndDocument();

                    writer.Flush();
                    writer.Close();
                }
                return sb.ToString();
            }

            set
            {
                if (value != null)
                {
                    if (value.Length > 1)
                    {
                        var oDoc = new XmlDocument();
                        oDoc.LoadXml(value);

                        if (oDoc != null)
                        {
                            if (oDoc.DocumentElement.HasChildNodes)
                            {
                                foreach (XmlNode node in oDoc.DocumentElement.ChildNodes)
                                {
                                    if (node is XmlElement)
                                    {
                                        var catNode = FindCategoryNode(node.Name);
                                        if (catNode != null)
                                        {
                                            catNode.ParseXML(node);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private FilterCategoryNode FindCategoryNode(string name)
        {
            foreach (var node in filterTree.Nodes)
            {
                if (node is FilterCategoryNode node1)
                {
                    if (string.Compare(name, node1.ColumnName, true) == 0)
                        return node1;
                }
            }
            return null;
        }

        /// <summary>
        /// Open and populate a category tree from TableAnalysis
        /// </summary>
        /// <param name="value"></param>
        public void Open(TableAnalysis value)
        {
            var filterColumns = new FilterColumns();
            filterColumns.BuildFilterColumns(value);
            filterTree.Nodes.Clear();
            foreach (FilterColumnItem item in filterColumns.Columns)
            {
                if (item.ColumnType == TableColumn.ColumnTypes.Date)
                {
                    var dateNode = new DateColumnNode(item);
                    filterTree.Nodes.Add(dateNode);
                }
                else if (item.DistinctValues.Count > 1)
                {
                    var xNode = new FilterCategoryNode(filterTree, item);
                    filterTree.Nodes.Add(xNode);
                }

            }
            applyButton.BackColor = SystemColors.ButtonFace;
        }


        private void OnTree_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (_ignoreCheckChange == false)
            {
                if (e.Node is FilterCategoryNode)
                {
                    if (e.Node.IsExpanded == false)
                    {
                        e.Node.Expand();
                    }

                    _ignoreCheckChange = true;
                    if (e.Node.Checked)
                    {
                        ((FilterCategoryNode)e.Node).SelectAll();
                    }
                    else
                    {
                        ((FilterCategoryNode)e.Node).UnselectAll();
                    }

                    _ignoreCheckChange = false;
                }
                else if (e.Node is DateColumnNode)
                {
                    if (e.Node.Nodes.Count > 0)
                    {
                        _ignoreCheckChange = true;
                        e.Node.Nodes[0].Checked = e.Node.Checked;
                        _ignoreCheckChange = false;
                    }
                    else
                    {
                        if (e.Node.Checked)
                        {
                            using (var dlg = new DateRangePickerDialog())
                            {
                                if (dlg.ShowDialog() == DialogResult.OK)
                                {
                                    var dateRangeNode = new DateRangeNode(dlg.StartDate, dlg.EndDate);
                                    e.Node.Nodes.Add(dateRangeNode);
                                    e.Node.Checked = true;
                                    e.Node.Expand();
                                }
                                else
                                {
                                    _ignoreCheckChange = true;
                                    e.Node.Checked = false;
                                    _ignoreCheckChange = false;
                                }
                            }
                        }
                    }
                }
                else if (e.Node is DateRangeNode)
                {
                    _ignoreCheckChange = true;
                    e.Node.Parent.Checked = e.Node.Checked;
                    _ignoreCheckChange = false;
                }
                else if (e.Node.Checked == false)
                {
                    _ignoreCheckChange = true;
                    e.Node.Parent.Checked = false;
                    _ignoreCheckChange = false;
                }
                else if (((FilterCategoryNode)e.Node.Parent).IsAllChildrenSelected())
                {
                    _ignoreCheckChange = true;
                    e.Node.Parent.Checked = true;
                    _ignoreCheckChange = false;
                }
                applyButton.BackColor = Color.Aquamarine;
            }
        }

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            SelectionChanged?.Invoke(this, e);
            applyButton.BackColor = SystemColors.ButtonFace;
        }

        private void OnTreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.GetType() == typeof(FilterCategoryNode))
            {
                var node = (FilterCategoryNode)(e.Node);
                if (!node.Loaded)
                {
                    bool toLoad = true;
                    if (node.NumToLoad > 100)
                    {
                        toLoad = false;
                        string msg = string.Format(Properties.Resources.B014, node.NumToLoad.ToString("N0"));   //B014: There are {0} items in this category. Do you want to load them?
                        if (MessageBox.Show(msg, Properties.Resources.B015, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            toLoad = true;
                    }

                    if (toLoad)
                    {
                        Cursor = Cursors.WaitCursor;
                        _ignoreCheckChange = true;
                        //remove the fake child node used for virtualization
                        node.Nodes[0].Remove();
                        //load real categories and values
                        node.LoadValues();

                        _ignoreCheckChange = false;
                        Cursor = Cursors.Default;
                    }
                    else
                    { e.Cancel = true; }
                }
            }
        }

        /// <summary>
        /// Handler mouse up event:
        ///     Show popup menu if hit node is date column node or date range node.
        ///     This enables add, edit and remove date range filter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FilterTree_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;

            TreeNode hitNode = filterTree.GetNodeAt(e.X, e.Y);
            if (hitNode == null) return;

            filterTree.SelectedNode = hitNode;
            if (hitNode.GetType() == typeof(DateColumnNode))
            {
                if (hitNode.Nodes.Count == 0)
                {
                    _selectedNode = hitNode;
                    addDateRangecontextMenuStrip.Show(filterTree, e.Location);
                }
            }
            else if (hitNode.GetType() == typeof(DateRangeNode))
            {
                _selectedNode = hitNode;
                editDateRangecontextMenuStrip.Show(filterTree, e.Location);
            }
            else
            {
                _selectedNode = null;
            }
        }

        #region"Date range filter settings"
        private TreeNode _selectedNode;
        /// <summary>
        /// Add date range filter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddDateRangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedNode == null) return;
            if (_selectedNode.GetType() != typeof(DateColumnNode)) return;

            using (var dlg = new DateRangePickerDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    var dateRangeNode = new DateRangeNode(dlg.StartDate, dlg.EndDate);
                    _selectedNode.Nodes.Add(dateRangeNode);
                    _selectedNode.Checked = true;
                    _selectedNode.Expand();
                }
            }
        }

        /// <summary>
        /// Handle change date range menu item click event:
        ///     Show date range picker dialog and change the
        ///     date range upon dialog results is OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditDateRangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedNode == null) return;
            if (_selectedNode.GetType() != typeof(DateRangeNode)) return;
            DateRangeNode dateRangeNode = (DateRangeNode)_selectedNode;

            using (var dlg = new DateRangePickerDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    dateRangeNode.StartDate = dlg.StartDate;
                    dateRangeNode.EndDate = dlg.EndDate;
                    dateRangeNode.Text = dateRangeNode.ToString();
                    applyButton.BackColor = Color.Aquamarine;
                }
            }
        }

        /// <summary>
        /// Handle remove date range menu item event:
        ///     Remove selected date range
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedNode == null) return;
            if (_selectedNode.GetType() != typeof(DateRangeNode)) return;
            DateColumnNode parentNode = (DateColumnNode)_selectedNode.Parent;
            parentNode.Nodes.Clear();
            parentNode.Checked = false;
        }
        #endregion
    }
}