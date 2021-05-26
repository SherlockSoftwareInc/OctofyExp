﻿using OctofyLib;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OctofyExp
{
    public partial class DataLoaderForm : Form
    {
        private readonly CustomizedDataBuilder _customData = new CustomizedDataBuilder();

        private readonly Stopwatch _stopWatch = new Stopwatch();

        private CancellationTokenSource _cancellation;

        private DataSourceTypesEnum _dataSourceType = DataSourceTypesEnum.SQLServerDataTable;

        private string _excelFileName = "";

        private DataTable _resultDataTable;

        private string _tableName = "";

        public DataLoaderForm()
        {
            InitializeComponent();
        }

        public enum DataSourceTypesEnum
        {
            SQLServerDataTable,
            ExcelFile
        }

        /// <summary>
        /// target database connection string (required)
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// The result data table read from the database
        /// </summary>
        public DataTable DataSource
        {
            get
            {
                return _resultDataTable;
            }
        }

        public DataSourceTypesEnum DataSourceType { set { _dataSourceType = value; } }

        /// <summary>
        /// Error message for reading the data.
        /// Empty if reading data successfully.
        /// </summary>
        public string ErrorInfo { get; set; }

        /// <summary>
        /// Excel file name
        /// </summary>
        public string ExcelFileName
        {
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _excelFileName = value;
                    _dataSourceType = DataSourceTypesEnum.ExcelFile;
                }
            }
        }

        /// <summary>
        /// The time it takes to read in the data (in second)
        /// </summary>
        public double LoadTime { get; set; }

        /// <summary>
        /// The target table name with schema (required)
        /// </summary>
        public string TableName
        {
            get { return _tableName; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _tableName = value;
                }
            }
        }

        /// <summary>
        /// The SELECT SQL statement
        /// </summary>
        public string TableSelectSQL { get; set; }

        public async Task<string> OpenExcelFileAsync(string connectionString, CancellationToken cancellationToken)
        {
            var result = "";
            if (connectionString.Length > 0)
            {
                using (var conn = new System.Data.OleDb.OleDbConnection(connectionString))
                {
                    try
                    {
                        using (var cmd = new System.Data.OleDb.OleDbCommand(TableSelectSQL, conn)
                        {
                            CommandType = CommandType.Text
                        })
                        {
                            await conn.OpenAsync().ConfigureAwait(false);
                            using (CancellationTokenRegistration crt = cancellationToken.Register(() => cmd.Cancel()))
                            {
                                using (var reader = await cmd.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false)) /*.ConfigureAwait(false)*/
                                {
                                    _resultDataTable = new DataTable();
                                    _resultDataTable.Load(reader);
                                    reader.Close();
                                }
                            }
                        }
                    }
                    catch (TaskCanceledException)
                    {
                        result = "Cancelled";
                    }
                    catch (System.Exception ex)
                    {
                        result = ex.Message;
                    }
                    finally
                    {
                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Open data table/view from the database
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<string> OpenTableAsync(string connectionString, string sql, CancellationToken cancellationToken, int timeout = 30)
        {
            var result = "";
            if (connectionString.Length > 0)
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        using (var cmd = new SqlCommand(sql, conn) { CommandType = CommandType.Text, CommandTimeout = timeout })
                        {
                            await conn.OpenAsync();
                            using (CancellationTokenRegistration crt = cancellationToken.Register(() => cmd.Cancel()))
                            {
                                using (var reader = await cmd.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false))
                                {
                                    _resultDataTable = new DataTable();
                                    _resultDataTable.Load(reader);
                                    reader.Close();
                                }
                            }
                        }
                    }
                    catch (TaskCanceledException)
                    {
                        result = "Cancelled";
                    }
                    catch (System.Exception ex)
                    {
                        result = ex.Message;
                    }
                    finally
                    {
                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Cancel button click event handle: cancel the reading process
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            progressTimer.Stop();
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// Form load handle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataLoaderForm_Load(object sender, EventArgs e)
        {
            numRowsLabel.Text = "";
            ErrorInfo = "";
            loadingLabel.Text = OctofyExp.Properties.Resources.A012;
            Cursor = Cursors.WaitCursor;
            startTimer.Start();
            progressTimer.Start();
        }

        private void ExitTimer_Tick(object sender, EventArgs e)
        {
            exitTimer.Stop();
            Close();
        }

        /// <summary>
        /// Get number of rows in the table
        /// </summary>
        /// <returns></returns>
        private long GetNumOfRows()
        {
            long numOfRows = 0;

            if (!string.IsNullOrWhiteSpace(TableName))
            {
                string sql = string.Format("SELECT COUNT(*) FROM {0}", TableName);

                if (ConnectionString.Length > 0)
                {
                    using (var conn = new SqlConnection(ConnectionString))
                    {
                        try
                        {
                            using (var cmd = new SqlCommand(sql, conn)
                            {
                                CommandType = CommandType.Text
                            })
                            {
                                conn.Open();
                                numOfRows = Convert.ToInt64(cmd.ExecuteScalar());
                            }
                        }
                        catch (System.Exception)
                        {
                            numOfRows = -1;
                        }
                        finally
                        {
                            if (conn.State == ConnectionState.Open)
                            {
                                conn.Close();
                            }
                        }
                    }
                }
            }
            return numOfRows;
        }

        private async void OpenExcelDataSource()
        {
            if (!string.IsNullOrWhiteSpace(ConnectionString))
            {
                using (var cancellationTokenSource = new CancellationTokenSource())
                {
                    ErrorInfo = await OpenExcelFileAsync(ConnectionString, cancellationTokenSource.Token).ConfigureAwait(false);
                    _stopWatch.Stop();
                    LoadTime = (double)_stopWatch.ElapsedMilliseconds / 1000F;
                    DialogResult = DialogResult.OK;
                    exitTimer.Start();

                    //using (var conn = new System.Data.OleDb.OleDbConnection(ConnectionString))
                    //{
                    //    using (var cmd = new System.Data.OleDb.OleDbCommand(TableSelectSQL, conn)
                    //    {
                    //        CommandType = CommandType.Text
                    //    })
                    //    {
                    //        using (CancellationTokenRegistration crt = cancellationTokenSource.Token.Register(() => cmd.Cancel()))
                    //        {
                    //            await conn.OpenAsync().ConfigureAwait(false);
                    //            //await conn.OpenAsync();

                    //            using (var reader = await cmd.ExecuteReaderAsync(cancellationTokenSource.Token).ConfigureAwait(false))
                    //            {
                    //                _resultDataTable = new DataTable();
                    //                _resultDataTable.Load(reader);
                    //                reader.Close();

                    //                _stopWatch.Stop();
                    //                LoadTime = (double)_stopWatch.ElapsedMilliseconds / 1000F;

                    //                DialogResult = DialogResult.OK;
                    //                ErrorInfo = "";
                    //            }
                    //            //cmd.TableMappings.Add("Table", TableName);
                    //            //DataSet dtSet = new System.Data.DataSet();
                    //            //cmd.Fill(dtSet);

                    //            //if (dtSet != null)
                    //            //{
                    //            //    _resultDataTable = dtSet.Tables[0];
                    //            //    DialogResult = DialogResult.OK;
                    //            //    ErrorInfo = "";
                    //            //}
                    //            //else
                    //            //{
                    //            //    ErrorInfo = "No data available";
                    //            //}
                    //        }
                    //    }
                    //}

                    //try
                    //{
                    //}
                    //catch (TaskCanceledException)
                    //{
                    //    ErrorInfo = "Cancelled";
                    //}
                    //catch (Exception ex)
                    //{
                    //    ErrorInfo = ex.Message;
                    //    //throw;
                    //}
                    //finally
                    //{
                    //    exitTimer.Start();
                    //}
                }
            }
        }
        /// <summary>
        /// Loading data from the SQL server
        /// </summary>
        private async void OpenSQLDataSource()
        {
            long numOfRows = GetNumOfRows();
            if (numOfRows > 0)
            {
                numRowsLabel.Text = string.Format(Properties.Resources.A086, numOfRows.ToString("N0"));

                using (var cancellationTokenSource = new CancellationTokenSource())
                {
                    _cancellation = cancellationTokenSource;

                    try
                    {
                        ErrorInfo = await OpenTableAsync(ConnectionString, TableSelectSQL, cancellationTokenSource.Token,
                            OctofyExp.Properties.Settings.Default.ConnectionTimeout).ConfigureAwait(false);
                        _stopWatch.Stop();
                        LoadTime = (double)_stopWatch.ElapsedMilliseconds / 1000F;
                        DialogResult = DialogResult.OK;
                        //if (ErrorInfo.Length == 0)
                        //{
                        //    _resultDataTable = _customData.GetData("");
                        //}
                        exitTimer.Start();
                    }
                    catch (OutOfMemoryException)
                    {
                        ErrorInfo = OctofyExp.Properties.Resources.A084;
                    }
                    catch (TaskCanceledException)
                    {
                        ErrorInfo = OctofyExp.Properties.Resources.A085;
                    }
                    catch (ObjectDisposedException)
                    {
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            else
            {
                MessageBox.Show(Properties.Resources.A087, Properties.Resources.A009,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ErrorInfo = Properties.Resources.A087;
                Close();
            }
        }
        /// <summary>
        /// The timer to keep progress bar moving
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressTimer_Tick(object sender, EventArgs e)
        {
            int progress = loadingProgressBar.Value;
            progress++;
            if (progress > loadingProgressBar.Maximum)
                progress = loadingProgressBar.Minimum;
            loadingProgressBar.Value = progress;
        }

        private string SelectASheet(DataTable dtSheets)
        {
            return "";
        }

        /// <summary>
        /// Handle start timer tick event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartTimer_Tick(object sender, EventArgs e)
        {
            startTimer.Stop();

            _stopWatch.Start();
            if (_dataSourceType == DataSourceTypesEnum.SQLServerDataTable)
            {
                OpenSQLDataSource();
            }
            else if (_dataSourceType == DataSourceTypesEnum.ExcelFile)
            {
                OpenExcelDataSource();
            }
        }

        ///// <summary>
        ///// Open data table/view from the database
        ///// </summary>
        ///// <param name="connectionString"></param>
        ///// <param name="sql"></param>
        ///// <returns></returns>
        //public async Task<string> OpenTableAsync(string connectionString, string sql, CancellationToken cancellationToken, int timeout = 30)
        //{
        //    var result = "";
        //    if (connectionString.Length > 0)
        //    {
        //        using (var conn = new SqlConnection(connectionString))
        //        {
        //            try
        //            {
        //                using (var cmd = new SqlCommand(sql, conn) { CommandType = CommandType.Text, CommandTimeout = timeout })
        //                {
        //                    await conn.OpenAsync();
        //                    using (CancellationTokenRegistration crt = cancellationToken.Register(() => cmd.Cancel()))
        //                    {
        //                        using (var reader = await cmd.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false)) /*.ConfigureAwait(false)*/
        //                        {
        //                            _data.Load(reader);
        //                            reader.Close();
        //                        }
        //                    }
        //                }
        //            }
        //            catch (TaskCanceledException)
        //            {
        //                result = "Cancelled";
        //            }
        //            catch (System.Exception ex)
        //            {
        //                result = ex.Message;
        //            }
        //            finally
        //            {
        //                if (conn.State == ConnectionState.Open)
        //                {
        //                    conn.Close();
        //                }
        //            }
        //        }
        //    }
        //    return result;
        //}
    }
}