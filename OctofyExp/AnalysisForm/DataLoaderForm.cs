﻿using OctofyLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace OctofyExp
{
    public partial class DataLoaderForm : Form
    {
        private CancellationTokenSource _cancellation;
        private readonly Stopwatch _stopWatch = new Stopwatch();
        private readonly CustomizedDataBuilder _customData = new CustomizedDataBuilder();

        public DataLoaderForm()
        {
            InitializeComponent();
        }

        public string ConnectionString { get; set; }

        public string TableName { get; set; }

        public string TableSelectSQL { get; set; }

        public string ErrorInfo { get; set; }

        public DataTable DataSource { 
            get {
                if (_customData != null)
                {
                    return _customData.GetData("");
                }
                else
                { return null; }
            } }

        public double LoadTime { get; set; }

        private void DataLoaderForm_Load(object sender, EventArgs e)
        {
            numRowsLabel.Text = "";
            ErrorInfo = "";
            loadingLabel.Text = OctofyExp.Properties.Resources.A012;
            Cursor = Cursors.WaitCursor;
            startTimer.Start();
            progressTimer.Start();
        }

        private void ProgressTimer_Tick(object sender, EventArgs e)
        {
            int progress = loadingProgressBar.Value;
            progress++;
            if (progress > loadingProgressBar.Maximum)
                progress = loadingProgressBar.Minimum;
            loadingProgressBar.Value = progress;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.Default ;
            progressTimer.Stop();
            Close();
        }

        /// <summary>
        /// Handle start timer tick event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void StartTimer_Tick(object sender, EventArgs e)
        {
            startTimer.Stop();
            long numOfRows = GetNumOfRows();
            if (numOfRows > 0)
            {
                numRowsLabel.Text = string.Format(Properties.Resources.A086, numOfRows.ToString ("N0") );

                using (var cancellationTokenSource = new CancellationTokenSource())
                {
                    _cancellation = cancellationTokenSource;

                    try
                    {
                        _stopWatch.Start();
                        ErrorInfo = await _customData.OpenTableAsync(ConnectionString, TableSelectSQL, cancellationTokenSource.Token,
                            OctofyExp.Properties.Settings.Default.ConnectionTimeout);
                        _stopWatch.Stop();
                        LoadTime = (double)_stopWatch.ElapsedMilliseconds / 1000F;
                        DialogResult = DialogResult.OK;
                        exitTimer.Start();
                        //Close();
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
        /// Get number of rows in the table
        /// </summary>
        /// <returns></returns>
        private long GetNumOfRows()
        {
            long numOfRows = 0;

            if (!string.IsNullOrWhiteSpace (TableName))
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
                                numOfRows =Convert.ToInt64 ( cmd.ExecuteScalar());
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

        private void ExitTimer_Tick(object sender, EventArgs e)
        {
            exitTimer.Stop();
            Close();
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