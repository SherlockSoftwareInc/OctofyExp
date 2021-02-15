using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace OctofyLib
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomizedDataBuilder
    {
        private readonly DataTable _data = new DataTable();

        ///// <summary>
        ///// Open data table by sql query script
        ///// </summary>
        ///// <param name="connectionString"></param>
        ///// <param name="sql"></param>
        ///// <returns></returns>
        //public string OpenTable(string connectionString, string sql, int timeout = 30)
        //{
        //    string result = "";
        //    if (connectionString.Length > 0)
        //    {
        //        using (var conn = new SqlConnection(connectionString))
        //        {
        //            try
        //            {
        //                using (var cmd = new SqlCommand()
        //                {
        //                    CommandType = CommandType.Text,
        //                    Connection = conn,
        //                    CommandTimeout = timeout,
        //                    CommandText = sql
        //                })
        //                {
        //                    conn.Open();
        //                    var dr = cmd.ExecuteReader();
        //                    _data.Load(dr);
        //                }

        //            }
        //            catch (SqlException ex)
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
                                using (var reader = await cmd.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false)) /*.ConfigureAwait(false)*/
                                {
                                    _data.Load(reader);
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
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public DataTable GetData(string filter)
        {
            //_filterString = filter;
            if (filter.Length == 0)
            {
                return _data.Copy();
            }
            else
            {
                var result = _data.Clone();
                var rowFilter = new DataRowFilter();
                rowFilter.SetFilter(filter, _data);
                foreach (DataRow dr in _data.Rows)
                {
                    if (rowFilter.CheckDataRow(dr) == true)
                    {
                        result.ImportRow(dr);
                    }
                }

                return result;
            }
        }
    }
}