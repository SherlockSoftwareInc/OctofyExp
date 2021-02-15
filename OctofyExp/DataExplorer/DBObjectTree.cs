using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace OctofyExp
{
    public partial class DBObjectTree : UserControl
    {
        public event EventHandler AfterSelect;
        public event EventHandler<OnAnalysisTableEventArgs> OnAnalysisTable;
        public event EventHandler<OnAnalysisTableEventArgs> OnPreviewData;

        private string _connectionString = "";
        private int _defaultNumOfRows;

        public DBObjectTree()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or set selected table name
        /// </summary>
        public string SelectedTable { get; private set; }

        /// <summary>
        /// Sets or gets number of rows to quick analysis
        /// </summary>
        public int DefaultNumOfRows
        {
            get { return _defaultNumOfRows; }
            set
            {
                _defaultNumOfRows = value;
                tableAnalysisToolStripMenuItem.Text = string.Format(Properties.Resources.A002, value);
            }
        }

        /// <summary>
        /// Clear object tree
        /// </summary>
        public void Clear()
        {
            _connectionString = "";
            ctlTree.Nodes.Clear();
        }

        /// <summary>
        /// Open a connection
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        public string Open(string connectionString, string databaseName)
        {
            Clear();
            ctlTree.Nodes.Clear();

            Cursor = Cursors.WaitCursor;

            if (connectionString.Length > 0)
            {
                _connectionString = connectionString;

                TreeNode root = ctlTree.Nodes.Add(databaseName);

                try
                {
                    using (var conn = new SqlConnection(_connectionString))
                    {
                        conn.Open();
                        DataTable schemaTable = conn.GetSchema(System.Data.OleDb.OleDbMetaDataCollectionNames.Tables);

                        //populate nodes
                        var dtSchemas = schemaTable.DefaultView.ToTable(true, "TABLE_SCHEMA");
                        var schemas = new List<string>();
                        foreach (DataRow dr in dtSchemas.Rows)
                        {
                            schemas.Add(dr[0].ToString());
                        }
                        schemas.Sort();
                        foreach (var schema in schemas)
                        {
                            DataRow[] matches = schemaTable.Select(string.Format("TABLE_SCHEMA = '{0}'", schema));
                            int matchCount = 0;
                            foreach (var item in matches)
                            {
                                if (!DBSchemaNode.IsExcludeType(item["TABLE_TYPE"].ToString()))
                                { matchCount++; }
                            }

                            if (matchCount > 0)
                            {
                                root.Nodes.Add(new DBSchemaNode(schema, matches.CopyToDataTable()));
                            }
                        }
                        root.Expand();

                        conn.Close();
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(string.Format("{0}", ex.Message), Properties.Resources.A004, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            Cursor = Cursors.Default;
            return "";
        }

        private void CtlTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.GetType() == typeof(DBTableViewNode))
            {
                var node = (DBTableViewNode)e.Node;
                OpenTableAnalysis(string.Format("{0}.{1}", node.SchemaName, node.TableName), DefaultNumOfRows);
            }
        }

        private void OpenTableAnalysis(string tableName, int numOfRows)
        {
            if (tableName.Length > 0)
            {
                var args = new OnAnalysisTableEventArgs()
                {
                    TableName = tableName,
                    NumOfRows = numOfRows,
                    ConnectionString = _connectionString
                };

                OnAnalysisTable?.Invoke(this, args);
            }
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tableName = GetSelectedTable();
            if (tableName.Length > 0)
            {
                Clipboard.SetText(tableName);
            }
        }

        private void ScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tableName = GetSelectedTable();
            if (tableName.Length > 0)
            {
                var sb = new StringBuilder();

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand() { Connection = conn, CommandType = System.Data.CommandType.Text })
                        {
                            cmd.CommandText = string.Format("SELECT * FROM {0} WHERE 0=1", tableName);
                            conn.Open();

                            var dat = new SqlDataAdapter(cmd);
                            var ds = new DataSet();
                            dat.Fill(ds);

                            if (ds.Tables.Count > 0)
                            {
                                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                                {
                                    if (i == 0)
                                        sb.AppendLine("SELECT " + ds.Tables[0].Columns[i].ColumnName.Trim());
                                    else
                                        sb.AppendLine("      ," + ds.Tables[0].Columns[i].ColumnName.Trim());
                                }
                            }
                            sb.AppendLine(String.Format(" FROM {0}", tableName));

                            Clipboard.SetText(sb.ToString());

                            MessageBox.Show(Properties.Resources.A062); //A062: Script has been copied to the clipboard.
                        }
                    }
                    catch (SqlException ex)
                    {
                        Debug.Print(ex.Message);
                        throw;
                    }
                    finally
                    {
                        if (conn.State == ConnectionState.Open)
                            conn.Close();
                    }
                }
            }
        }

        private void TableAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tableName = GetSelectedTable();
            if (tableName.Length > 0)
            {
                OpenTableAnalysis(tableName, DefaultNumOfRows);
            }
        }


        private void AnalyzeEntireTableviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tableName = GetSelectedTable();
            if (tableName.Length > 0)
            {
                OpenTableAnalysis(tableName, -1);
            }
        }

        private string GetSelectedTable()
        {
            if (ctlTree.SelectedNode != null)
            {
                if (ctlTree.SelectedNode.GetType() == typeof(DBTableViewNode))
                {
                    var node = (DBTableViewNode)ctlTree.SelectedNode;
                    return string.Format("{0}.{1}", node.SchemaName, node.TableName);
                }
            }

            return "";
        }

        private void CtlTree_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var selectedNode = ctlTree.HitTest(e.Location).Node;
                if (selectedNode != null)
                {
                    ctlTree.SelectedNode = selectedNode;
                    if (selectedNode.GetType() == typeof(DBTableViewNode))
                    {
                        contextMenuStrip1.Show(Cursor.Position);
                    }
                }
            }
        }

        /// <summary>
        /// Handle tree AfterSelect event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CtlTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string tableName = GetSelectedTable();
            if (SelectedTable != tableName)
            {
                SelectedTable = tableName;
                AfterSelect?.Invoke(e.Node, new EventArgs());
            }
        }

        private void PreviewDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tableName = GetSelectedTable();
            if (tableName.Length > 0)
            {
                var args = new OnAnalysisTableEventArgs()
                {
                    TableName = tableName,
                    NumOfRows = -1,
                    ConnectionString = _connectionString
                };

                OnPreviewData?.Invoke(this, args);
            }
        }

    }

    //public partial class DBObjectTree : UserControl
    //{
    //    public event EventHandler AfterSelect;
    //    public event EventHandler<OnAnalysisTableEventArgs> OnAnalysisTable;
    //    public event EventHandler<OnAnalysisTableEventArgs> OnPreviewData;

    //    private string _connectionString = "";
    //    private int _defaultNumOfRows;

    //    public DBObjectTree()
    //    {
    //        InitializeComponent();
    //    }

    //    /// <summary>
    //    /// Gets or set selected table name
    //    /// </summary>
    //    public string SelectedTable { get; private set; }

    //    /// <summary>
    //    /// Sets or gets number of rows to quick analysis
    //    /// </summary>
    //    public int DefaultNumOfRows
    //    {
    //        get { return _defaultNumOfRows; }
    //        set
    //        {
    //            _defaultNumOfRows = value;
    //            tableAnalysisToolStripMenuItem.Text = string.Format(Properties.Resources.A002, value);
    //        }
    //    }

    //    /// <summary>
    //    /// Clear object tree
    //    /// </summary>
    //    public void Clear()
    //    {
    //        _connectionString = "";
    //        ctlTree.Nodes.Clear();
    //    }

    //    /// <summary>
    //    /// Open a connection
    //    /// </summary>
    //    /// <param name="connectionString"></param>
    //    /// <param name="databaseName"></param>
    //    /// <returns></returns>
    //    public string Open(string connectionString, string databaseName)
    //    {
    //        Clear();
    //        Cursor = Cursors.WaitCursor;
    //        //Application.DoEvents();

    //        if (connectionString.Length > 0)
    //        {
    //            _connectionString = connectionString;
    //            var dataTable = new DataTable();
    //            using (SqlConnection conn = new SqlConnection(connectionString))
    //            {
    //                try
    //                {
    //                    using (SqlCommand cmd = new SqlCommand() { Connection = conn, CommandType = CommandType.Text })
    //                    {
    //                        cmd.CommandText = "SELECT TABLE_SCHEMA, TABLE_NAME, TABLE_TYPE FROM information_schema.TABLES";
    //                        using (SqlDataAdapter dat = new SqlDataAdapter(cmd))
    //                        {
    //                            DataSet ds = new DataSet();
    //                            conn.Open();
    //                            dat.Fill(ds);

    //                            if (ds.Tables.Count > 0)
    //                                dataTable = ds.Tables[0];
    //                        }
    //                    }
    //                }
    //                catch (SqlException ex)
    //                {
    //                    _connectionString = "";
    //                    return ex.Message;
    //                }
    //                finally
    //                {
    //                    if (conn.State == ConnectionState.Open)
    //                        conn.Close();
    //                    Cursor = Cursors.Default;
    //                }

    //            }

    //            ctlTree.Nodes.Clear();
    //            TreeNode root = ctlTree.Nodes.Add(databaseName);
    //            //populate nodes
    //            var dtSchemas = dataTable.DefaultView.ToTable(true, "TABLE_SCHEMA");
    //            var schemas = new List<string>();
    //            foreach (DataRow dr in dtSchemas.Rows)
    //            {
    //                schemas.Add(dr[0].ToString());
    //            }
    //            schemas.Sort();
    //            foreach (var schema in schemas)
    //            {
    //                root.Nodes.Add(new DBSchemaNode(schema, dataTable));
    //            }
    //            root.Expand();
    //            //root.ExpandAll();
    //        }
    //        else
    //        {
    //            ctlTree.Nodes.Clear();
    //        }

    //        Cursor = Cursors.Default;
    //        return "";
    //    }


    //    private void CtlTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
    //    {
    //        if (e.Node.GetType() == typeof(DBTableViewNode))
    //        {
    //            var node = (DBTableViewNode)e.Node;
    //            OpenTableAnalysis(string.Format("{0}.{1}", node.SchemaName, node.TableName), DefaultNumOfRows);
    //        }
    //    }

    //    private void OpenTableAnalysis(string tableName, int numOfRows)
    //    {
    //        if (tableName.Length > 0)
    //        {
    //            var args = new OnAnalysisTableEventArgs()
    //            {
    //                TableName = tableName,
    //                NumOfRows = numOfRows,
    //                ConnectionString = _connectionString
    //            };

    //            OnAnalysisTable?.Invoke(this, args);
    //        }
    //    }

    //    private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
    //    {
    //        var tableName = GetSelectedTable();
    //        if (tableName.Length > 0)
    //        {
    //            Clipboard.SetText(tableName);
    //        }
    //    }

    //    private void ScriptToolStripMenuItem_Click(object sender, EventArgs e)
    //    {
    //        var tableName = GetSelectedTable();
    //        if (tableName.Length > 0)
    //        {
    //            var sb = new StringBuilder();

    //            using (SqlConnection conn = new SqlConnection(_connectionString))
    //            {
    //                try
    //                {
    //                    using (SqlCommand cmd = new SqlCommand() { Connection = conn, CommandType = System.Data.CommandType.Text })
    //                    {
    //                        cmd.CommandText = string.Format("SELECT * FROM {0} WHERE 0=1", tableName);
    //                        conn.Open();

    //                        var dat = new SqlDataAdapter(cmd);
    //                        var ds = new DataSet();
    //                        dat.Fill(ds);

    //                        if (ds.Tables.Count > 0)
    //                        {
    //                            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
    //                            {
    //                                if (i == 0)
    //                                    sb.AppendLine("SELECT " + ds.Tables[0].Columns[i].ColumnName.Trim());
    //                                else
    //                                    sb.AppendLine("      ," + ds.Tables[0].Columns[i].ColumnName.Trim());
    //                            }
    //                        }
    //                        sb.AppendLine(String.Format(" FROM {0}", tableName));

    //                        Clipboard.SetText(sb.ToString());

    //                        MessageBox.Show(Properties.Resources.A062); //A062: Script has been copied to the clipboard.
    //                    }
    //                }
    //                catch (SqlException)
    //                {
    //                    //Debug.Print(ex.Message);
    //                    throw;
    //                }
    //                finally
    //                {
    //                    if (conn.State == ConnectionState.Open)
    //                        conn.Close();
    //                }
    //            }
    //        }
    //    }

    //    private void TableAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
    //    {
    //        var tableName = GetSelectedTable();
    //        if (tableName.Length > 0)
    //        {
    //            OpenTableAnalysis(tableName, DefaultNumOfRows);
    //        }
    //    }


    //    private void AnalyzeEntireTableviewToolStripMenuItem_Click(object sender, EventArgs e)
    //    {
    //        var tableName = GetSelectedTable();
    //        if (tableName.Length > 0)
    //        {
    //            OpenTableAnalysis(tableName, -1);
    //        }
    //    }

    //    private string GetSelectedTable()
    //    {
    //        if (ctlTree.SelectedNode != null)
    //        {
    //            if (ctlTree.SelectedNode.GetType() == typeof(DBTableViewNode))
    //            {
    //                var node = (DBTableViewNode)ctlTree.SelectedNode;
    //                return string.Format("{0}.{1}", node.SchemaName, node.TableName);
    //            }
    //        }

    //        return "";
    //    }

    //    private void CtlTree_MouseClick(object sender, MouseEventArgs e)
    //    {
    //        if (e.Button == MouseButtons.Right)
    //        {
    //            var selectedNode = ctlTree.HitTest(e.Location).Node;
    //            if (selectedNode != null)
    //            {
    //                ctlTree.SelectedNode = selectedNode;
    //                if (selectedNode.GetType() == typeof(DBTableViewNode))
    //                {
    //                    contextMenuStrip1.Show(Cursor.Position);
    //                }
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// Handle tree AfterSelect event
    //    /// </summary>
    //    /// <param name="sender"></param>
    //    /// <param name="e"></param>
    //    private void CtlTree_AfterSelect(object sender, TreeViewEventArgs e)
    //    {
    //        string tableName = GetSelectedTable();
    //        if (SelectedTable != tableName)
    //        {
    //            SelectedTable = tableName;
    //            AfterSelect?.Invoke(e.Node, new EventArgs());
    //        }
    //    }


    //    private void PreviewDataToolStripMenuItem_Click(object sender, EventArgs e)
    //    {
    //        var tableName = GetSelectedTable();
    //        if (tableName.Length > 0)
    //        {
    //            var args = new OnAnalysisTableEventArgs()
    //            {
    //                TableName = tableName,
    //                NumOfRows = -1,
    //                ConnectionString = _connectionString
    //            };

    //            OnPreviewData?.Invoke(this, args);
    //        }
    //    }
    //}

}

