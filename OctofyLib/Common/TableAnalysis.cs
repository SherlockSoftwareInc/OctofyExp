
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace OctofyLib
{
    /// <summary>
    /// The class is used to analyze a given data table. 
    /// Find all categorical columns and their unique values (categories)
    /// </summary>
    public class TableAnalysis : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler DataBuildProgressChange;
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler TableParseProgressChange;

        private string BLANKS = Properties.Resources.B003;    // "(Blanks)";

        private DataTable _dataSource;
        private bool disposedValue;

        /// <summary>
        /// 
        /// </summary>
        public DataTable DataSource
        {
            get
            {
                return _dataSource;
            }
            //set
            //{
            //    if (_dataSource is object)
            //        _dataSource.Dispose();
            //    Columns.Clear();
            //    _dataSource = value;
            //    ParseDataTable();
            //}
        }

        /// <summary>
        /// Gets all column objects
        /// </summary>
        public List<TableColumn> Columns { get; private set; } = new List<TableColumn>();

        /// <summary>
        /// Gets table parsing process progress in percentage
        /// </summary>
        public float ProgressPercent { get; set; }

        ///// <summary>
        ///// Analyze table columns
        ///// </summary>
        //private void ParseDataTable()
        //{
        //    // remove binday column
        //    for (int index = _dataSource.Columns.Count - 1; index >= 0; index--)
        //    {
        //        if (_dataSource.Columns[index].DataType == typeof(byte[]))
        //        {
        //            RemoveColumnFromDataTable(index);
        //        }
        //    }

        //    int progressStep = (int)(_dataSource.Columns.Count / (double)10.0F);
        //    for (int i = 0; i < _dataSource.Columns.Count; i++)
        //    {
        //        var column = new TableColumn(_dataSource, i, MaxUniqueValues, CategoryNameMaxLen);
        //        //if (column.ColumnType == TableColumn.ColumnTypes.Category || column.ColumnType == TableColumn.ColumnTypes.Date)
        //        //{
        //        Columns.Add(column);
        //        //}

        //        if (progressStep > 1 & progressStep > 1)
        //        {
        //            if (i % progressStep == 0)
        //            {
        //                ProgressPercent = (float)(i * 100 / (double)_dataSource.Columns.Count);
        //                if (ProgressPercent > 1)
        //                {
        //                    TableParseProgressChange?.Invoke(this, new EventArgs());
        //                }
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// Parse data table async version
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public Task<bool> OpenAsync(DataTable dataTable, CancellationToken cancellationToken)
        {
            if (_dataSource is object)
                _dataSource.Dispose();
            Columns.Clear();
            _dataSource = dataTable;

            Task<bool> task = null;
            task = Task.Run(() =>
            {
                //bool result = false;

                // remove binday column
                for (int index = _dataSource.Columns.Count - 1; index >= 0; index--)
                {
                    if (_dataSource.Columns[index].DataType == typeof(byte[]))
                    {
                        RemoveColumnFromDataTable(index);
                    }
                }

                int progressStep = (int)(_dataSource.Columns.Count / (double)10.0F);
                for (int i = 0; i < _dataSource.Columns.Count; i++)
                {
                    // Check if a cancellation is requested, if yes,
                    // throw a TaskCanceledException.
                    if (cancellationToken.IsCancellationRequested)
                        throw new TaskCanceledException(task);

                    cancellationToken.ThrowIfCancellationRequested();

                    var column = new TableColumn(_dataSource, i, MaxUniqueValues, CategoryNameMaxLen);
                    Columns.Add(column);

                    if (progressStep > 1 & progressStep > 1)
                    {
                        if (i % progressStep == 0)
                        {
                            ProgressPercent = (float)(i * 100 / (double)_dataSource.Columns.Count);
                            if (ProgressPercent > 1)
                            {
                                TableParseProgressChange?.Invoke(this, new EventArgs());
                            }
                        }
                    }
                }

                return true;
            });
            return task;
        }

        public bool Open(DataTable dataTable)
        {
            if (_dataSource is object)
                _dataSource.Dispose();
            Columns.Clear();
            _dataSource = dataTable;

            // remove binday column
            for (int index = _dataSource.Columns.Count - 1; index >= 0; index--)
            {
                if (_dataSource.Columns[index].DataType == typeof(byte[]))
                {
                    RemoveColumnFromDataTable(index);
                }
            }

            int progressStep = (int)(_dataSource.Columns.Count / (double)10.0F);
            for (int i = 0; i < _dataSource.Columns.Count; i++)
            {
                var column = new TableColumn(_dataSource, i, MaxUniqueValues, CategoryNameMaxLen);
                Columns.Add(column);

                if (progressStep > 1 & progressStep > 1)
                {
                    if (i % progressStep == 0)
                    {
                        ProgressPercent = (float)(i * 100 / (double)_dataSource.Columns.Count);
                        if (ProgressPercent > 1)
                        {
                            TableParseProgressChange?.Invoke(this, new EventArgs());
                        }
                    }
                }
            }

            return true;
        }


        /// <summary>
        /// Remove a column from the data table
        /// </summary>
        /// <param name="index"></param>
        private void RemoveColumnFromDataTable(int index)
        {
            try
            {
                _dataSource.Columns.RemoveAt(index);
            }
            catch (SqlException)
            {
                // ignore error
            }
        }

        /// <summary>
        /// Get column index in the data table
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public int GetColumnIndex(string columnName)
        {
            int result = -1;
            for (int i = 0; i < _dataSource.Columns.Count; i++)
            {
                if (Equals(_dataSource.Columns[i].ColumnName, columnName))
                {
                    result = i;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Get column object by column name
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public TableColumn GetColumn(string columnName)
        {
            return Columns.Find(s => Equals(s.ColumnName, columnName));
        }

        /// <summary>
        /// Returns sorted unique values of the specified column
        /// </summary>
        /// <param name="columnName">Column name</param>
        /// <param name="excludeBlanks">Indicates if blanks exculed or not</param>
        /// <returns></returns>
        public List<string> GetSortedColumnUniqueValues(string columnName, bool excludeBlanks)
        {
            TableColumn column = GetColumn(columnName);
            if (column != null)
            {
                return column.GetUniqueValues(excludeBlanks);
            }

            return new List<string>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public bool IsDateColumn(string columnName)
        {
            TableColumn column = GetColumn(columnName);
            if (column != null)
                return (column.DataType == TableColumn.DataSubtypes.StringDate || column.DataType == TableColumn.DataSubtypes.Date);
            else
                return false;
        }

        /// <summary>
        /// Gets or sets length limit, column will be not calculate
        /// when length of value exceed the length limit
        /// </summary>
        public int CategoryNameMaxLen { get; set; } = 256;

        /// <summary>
        /// Gets or set maximum number of unique values.
        /// Colume will be not calculate when number of unique
        /// value exceed the limit. 
        /// (To save the calculate time on some column such as
        /// ID column)
        /// </summary>
        public long MaxUniqueValues { get; set; } = 32768;

        /// <summary>
        /// Build chart values for stacked bar chart (2D)
        /// </summary>
        /// <param name="yColumnName"></param>
        /// <param name="xColumnName"></param>
        /// <param name="excludeBlanks"></param>
        /// <returns></returns>
        public decimal?[,] BuildPivotTableValue(string yColumnName, string xColumnName, bool excludeBlanks)
        {
            TableColumn yColumn = GetColumn(yColumnName);
            TableColumn xColumn = GetColumn(xColumnName);
            int xBound = xColumn.GetCount(false);
            int yBound = yColumn.GetCount(excludeBlanks);

            var result = new decimal?[yBound, xBound + 1];
            for (int i = 0; i < yBound; i++)
            {
                for (int j = 0; j <= xBound; j++)
                    result[i, j] = 0;
            }

            int dy = yColumn.HasBlanks ? 1 : 0;
            if (excludeBlanks)
                dy = 0;
            int dx = xColumn.HasBlanks ? 1 : 0;
            long cnt = _dataSource.Rows.Count;
            long progressStep = (long)(cnt / 10);
            string strValue = "";

            for (int i = 0; i < cnt; i++)
            {
                var dr = _dataSource.Rows[i];
                int x = -1, y = -1;
                if (dr[yColumnName] != DBNull.Value)
                {
                    switch (yColumn.DataType)
                    {
                        case TableColumn.DataSubtypes.StringDate:
                        case TableColumn.DataSubtypes.Date:
                            DateTime dateValue = Convert.ToDateTime(dr[yColumnName]);
                            y = yColumn.ValueIndex(dateValue);
                            break;

                        case TableColumn.DataSubtypes.NativeInteger:
                            y = yColumn.ValueIndex(Convert.ToInt64(dr[yColumnName]));
                            break;

                        case TableColumn.DataSubtypes.StringInteger:
                            strValue = dr[yColumnName].ToString().TrimEnd();
                            if (strValue.Length > 0)
                                y = yColumn.ValueIndex(strValue);
                            else
                                y = (yColumn.HasBlanks ? -1 : 0);
                            break;

                        case TableColumn.DataSubtypes.NativeDecimal:
                            y = yColumn.ValueIndex(Convert.ToDouble(dr[yColumnName]));
                            break;

                        case TableColumn.DataSubtypes.StringDecimal:
                            strValue = dr[yColumnName].ToString().TrimEnd();
                            if (strValue.Length > 0)
                                y = yColumn.ValueIndex(strValue);
                            else
                                y = (yColumn.HasBlanks ? -1 : 0);
                            break;

                        case TableColumn.DataSubtypes.Text:
                            string valueY = dr[yColumnName].ToString().TrimEnd();
                            if (valueY.Length == 0)
                            {
                                y = (yColumn.HasBlanks ? -1 : 0);
                            }
                            else
                            {
                                y = yColumn.ValueIndex(valueY);
                            }
                            break;

                        case TableColumn.DataSubtypes.ByteArray:
                            var byteValue = (byte[])(dr[yColumnName]);
                            y = yColumn.ValueIndex(TimestampToString(byteValue));
                            break;

                        default:
                            y = -2;
                            break;
                    }
                }

                if (dr[xColumnName] != DBNull.Value)
                {
                    switch (xColumn.DataType)
                    {
                        case TableColumn.DataSubtypes.StringDate:
                        case TableColumn.DataSubtypes.Date:
                            DateTime dateValue = Convert.ToDateTime(dr[xColumnName]);
                            x = xColumn.ValueIndex(dateValue);
                            break;

                        case TableColumn.DataSubtypes.NativeInteger:
                            x = xColumn.ValueIndex(Convert.ToInt64(dr[xColumnName]));
                            break;

                        case TableColumn.DataSubtypes.StringInteger:
                            strValue = dr[xColumnName].ToString().TrimEnd();
                            Debug.Print(strValue);
                            if (long.TryParse(strValue, out long longValue))
                            {
                                x = xColumn.ValueIndex(longValue);
                            }


                            //if (strValue.Length > 0)
                            //{
                            //    x = xColumn.ValueIndex(long.Parse(strValue));
                            //}
                            break;

                        case TableColumn.DataSubtypes.NativeDecimal:
                            x = xColumn.ValueIndex(Convert.ToDouble(dr[xColumnName]));
                            break;

                        case TableColumn.DataSubtypes.StringDecimal:
                            strValue = dr[xColumnName].ToString().TrimEnd();
                            if (strValue.Length > 0)
                                x = xColumn.ValueIndex(strValue);
                            break;

                        case TableColumn.DataSubtypes.Text:
                            string valueX = dr[xColumnName].ToString().TrimEnd();
                            if (valueX.Length > 0)
                            {
                                x = xColumn.ValueIndex(valueX);
                            }
                            break;

                        case TableColumn.DataSubtypes.ByteArray:
                            var byteValue = (byte[])(dr[xColumnName]);
                            x = xColumn.ValueIndex(TimestampToString(byteValue));
                            break;

                        default:
                            x = -2;
                            break;
                    }
                }

                x += dx;
                y += dy;
                if (x >= 0 && y >= 0)
                {
                    result[y, x] += 1;
                }

                if (cnt > 20000 & progressStep > 1)
                {
                    if (i % progressStep == 0)
                    {
                        ProgressPercent = (float)(i * 100 / (double)_dataSource.Rows.Count);
                        if (ProgressPercent > 1)
                        {
                            DataBuildProgressChange?.Invoke(this, new EventArgs());
                        }
                    }
                }
            }

            for (int i = 0; i < yBound; i++)
            {
                decimal total = 0;
                for (int j = 0; j < xBound; j++)
                {
                    if (result[i, j].HasValue)
                        total += (decimal)result[i, j];
                }
                result[i, xBound] = total;
            }

            return result;
        }

        /// <summary>
        /// Build 2D stacked bar chart values async version
        /// </summary>
        /// <param name="yColumnName"></param>
        /// <param name="xColumnName"></param>
        /// <param name="excludeBlanks"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<decimal?[,]> BuildPivotTableValueAsync(string yColumnName,
            string xColumnName, bool excludeBlanks, CancellationToken cancellationToken)
        {
            Task<decimal?[,]> task = null;
            task = Task.Run(() =>
            {
                TableColumn yColumn = GetColumn(yColumnName);
                TableColumn xColumn = GetColumn(xColumnName);
                int xBound = xColumn.GetCount(false);
                int yBound = yColumn.GetCount(excludeBlanks);

                var result = new decimal?[yBound, xBound + 1];
                for (int i = 0; i < yBound; i++)
                {
                    for (int j = 0; j <= xBound; j++)
                        result[i, j] = 0;
                }

                int dy = yColumn.HasBlanks ? 1 : 0;
                if (excludeBlanks)
                    dy = 0;
                int dx = xColumn.HasBlanks ? 1 : 0;
                long cnt = _dataSource.Rows.Count;
                long progressStep = (long)(cnt / 10);
                string strValue = "";

                for (int i = 0; i < cnt; i++)
                {
                    // Check if a cancellation is requested, if yes,
                    // throw a TaskCanceledException.
                    if (cancellationToken.IsCancellationRequested)
                        throw new TaskCanceledException(task);

                    cancellationToken.ThrowIfCancellationRequested();

                    var dr = _dataSource.Rows[i];
                    int x = -1, y = -1;
                    if (dr[yColumnName] != DBNull.Value)
                    {
                        switch (yColumn.DataType)
                        {
                            case TableColumn.DataSubtypes.StringDate:
                            case TableColumn.DataSubtypes.Date:
                                DateTime dateValue = Convert.ToDateTime(dr[yColumnName]);
                                y = yColumn.ValueIndex(dateValue);
                                break;

                            case TableColumn.DataSubtypes.NativeInteger:
                                y = yColumn.ValueIndex(Convert.ToInt64(dr[yColumnName]));
                                break;

                            case TableColumn.DataSubtypes.StringInteger:
                                strValue = dr[yColumnName].ToString().TrimEnd();
                                if (strValue.Length > 0)
                                    y = yColumn.ValueIndex(strValue);
                                else
                                    y = (yColumn.HasBlanks ? -1 : 0);
                                break;

                            case TableColumn.DataSubtypes.NativeDecimal:
                                y = yColumn.ValueIndex(Convert.ToDouble(dr[yColumnName]));
                                break;

                            case TableColumn.DataSubtypes.StringDecimal:
                                strValue = dr[yColumnName].ToString().TrimEnd();
                                if (strValue.Length > 0)
                                    y = yColumn.ValueIndex(strValue);
                                else
                                    y = (yColumn.HasBlanks ? -1 : 0);
                                break;

                            case TableColumn.DataSubtypes.Text:
                                string valueY = dr[yColumnName].ToString().TrimEnd();
                                if (valueY.Length == 0)
                                {
                                    y = (yColumn.HasBlanks ? -1 : 0);
                                }
                                else
                                {
                                    y = yColumn.ValueIndex(valueY);
                                }
                                break;

                            case TableColumn.DataSubtypes.ByteArray:
                                var byteValue = (byte[])(dr[yColumnName]);
                                y = yColumn.ValueIndex(TimestampToString(byteValue));
                                break;

                            default:
                                y = -2;
                                break;
                        }
                    }

                    if (dr[xColumnName] != DBNull.Value)
                    {
                        switch (xColumn.DataType)
                        {
                            case TableColumn.DataSubtypes.StringDate:
                            case TableColumn.DataSubtypes.Date:
                                DateTime dateValue = Convert.ToDateTime(dr[xColumnName]);
                                x = xColumn.ValueIndex(dateValue);
                                break;

                            case TableColumn.DataSubtypes.NativeInteger:
                                x = xColumn.ValueIndex(Convert.ToInt64(dr[xColumnName]));
                                break;

                            case TableColumn.DataSubtypes.StringInteger:
                                strValue = dr[xColumnName].ToString().TrimEnd();
                                Debug.Print(strValue);
                                if (long.TryParse(strValue, out long longValue))
                                {
                                    x = xColumn.ValueIndex(longValue);
                                }
                                break;

                            case TableColumn.DataSubtypes.NativeDecimal:
                                x = xColumn.ValueIndex(Convert.ToDouble(dr[xColumnName]));
                                break;

                            case TableColumn.DataSubtypes.StringDecimal:
                                strValue = dr[xColumnName].ToString().TrimEnd();
                                if (strValue.Length > 0)
                                    x = xColumn.ValueIndex(strValue);
                                break;

                            case TableColumn.DataSubtypes.Text:
                                string valueX = dr[xColumnName].ToString().TrimEnd();
                                if (valueX.Length > 0)
                                {
                                    x = xColumn.ValueIndex(valueX);
                                }
                                break;

                            case TableColumn.DataSubtypes.ByteArray:
                                var byteValue = (byte[])(dr[xColumnName]);
                                x = xColumn.ValueIndex(TimestampToString(byteValue));
                                break;

                            default:
                                x = -2;
                                break;
                        }
                    }

                    x += dx;
                    y += dy;
                    if (x >= 0 && y >= 0)
                    {
                        result[y, x] += 1;
                    }

                    if (cnt > 20000 & progressStep > 1)
                    {
                        if (i % progressStep == 0)
                        {
                            ProgressPercent = (float)(i * 100 / (double)_dataSource.Rows.Count);
                            if (ProgressPercent > 1)
                            {
                                DataBuildProgressChange?.Invoke(this, new EventArgs());
                            }
                        }
                    }
                }

                for (int i = 0; i < yBound; i++)
                {
                    decimal total = 0;
                    for (int j = 0; j < xBound; j++)
                    {
                        if (result[i, j].HasValue)
                            total += (decimal)result[i, j];
                    }
                    result[i, xBound] = total;
                }

                return result;
            });

            return task;
        }


        /// <summary>
        /// Build pivottable with periods as y-axis
        /// </summary>
        /// <param name="yColumnName">y-axis column name</param>
        /// <param name="periods">periods for y-axis</param>
        /// <param name="xColumnName">x-axis column name</param>
        /// <param name="xMembers">members of x-axis</param>
        /// <returns></returns>
        public decimal?[,] BuildPivotTableValue(string yColumnName, List<TimePeriod> periods, string xColumnName)
        {
            TableColumn xColumn = GetColumn(xColumnName);
            int xBound = xColumn.GetCount(false);
            int yBound = periods.Count;

            var result = new decimal?[yBound, xBound + 1];
            for (int i = 0; i < yBound; i++)
            {
                for (int j = 0; j <= xBound; j++)
                    result[i, j] = 0;
            }

            int dx = xColumn.HasBlanks ? 1 : 0;
            long cnt = _dataSource.Rows.Count;
            long progressStep = (long)(cnt / 10);
            string strValue;
            for (int i = 0; i < cnt; i++)
            {
                var dr = _dataSource.Rows[i];
                int x = -1, y = -1;
                if (dr[xColumnName] != DBNull.Value)
                {
                    switch (xColumn.DataType)
                    {
                        case TableColumn.DataSubtypes.StringDate:
                        case TableColumn.DataSubtypes.Date:
                            DateTime dateValue = Convert.ToDateTime(dr[xColumnName]);
                            x = xColumn.ValueIndex(dateValue);
                            break;

                        case TableColumn.DataSubtypes.NativeInteger:
                            x = xColumn.ValueIndex(Convert.ToInt64(dr[xColumnName]));
                            break;

                        case TableColumn.DataSubtypes.StringInteger:
                            strValue = dr[xColumnName].ToString().TrimEnd();
                            if (strValue.Length > 0)
                                x = xColumn.ValueIndex(strValue);
                            break;

                        case TableColumn.DataSubtypes.NativeDecimal:
                            x = xColumn.ValueIndex(Convert.ToDouble(dr[xColumnName]));
                            break;

                        case TableColumn.DataSubtypes.StringDecimal:
                            strValue = dr[xColumnName].ToString().TrimEnd();
                            if (strValue.Length > 0)
                                x = xColumn.ValueIndex(strValue);
                            break;

                        case TableColumn.DataSubtypes.Text:
                            string valueX = dr[xColumnName].ToString().TrimEnd();
                            if (valueX.Length > 0)
                            {
                                x = xColumn.ValueIndex(valueX);
                            }
                            break;

                        case TableColumn.DataSubtypes.ByteArray:
                            var byteValue = (byte[])(dr[xColumnName]);
                            x = xColumn.ValueIndex(TimestampToString(byteValue));
                            break;

                        default:
                            x = -2;
                            break;
                    }
                }
                x += dx;

                if (dr[yColumnName] != DBNull.Value)
                {
                    y = IndexOfTimePeriod(periods, Convert.ToDateTime(dr[yColumnName]));
                }
                else
                {
                    if (periods[0].Year == 0)
                        y = 0;
                }

                if (x >= 0 && y >= 0)
                {
                    result[y, x] += 1;
                }

                if (cnt > 20000 & progressStep > 1)
                {
                    if (i % progressStep == 0)
                    {
                        ProgressPercent = (float)(i * 100 / (double)_dataSource.Rows.Count);
                        if (ProgressPercent > 1)
                        {
                            DataBuildProgressChange?.Invoke(this, new EventArgs());
                        }
                    }
                }
            }

            for (int i = 0; i < yBound; i++)
            {
                decimal total = 0;
                for (int j = 0; j < xBound; j++)
                {
                    if (result[i, j].HasValue)
                        total += (decimal)result[i, j];
                }
                result[i, xBound] = total;
            }

            return result;
        }

        public decimal?[,] BuildPivotTableValue(string yColumnName, DateGrouper dateGrouper, string xColumnName, bool excludeBlanks)
        {
            TableColumn xColumn = GetColumn(xColumnName);
            int xBound = xColumn.GetCount(false);
            int yBound = dateGrouper.Periods.Count;

            var result = new decimal?[yBound, xBound + 1];
            for (int i = 0; i < yBound; i++)
            {
                for (int j = 0; j <= xBound; j++)
                    result[i, j] = 0;
            }

            int dx = xColumn.HasBlanks ? 1 : 0;
            long cnt = _dataSource.Rows.Count;
            long progressStep = (long)(cnt / 10);
            string strValue;
            for (int i = 0; i < cnt; i++)
            {
                var dr = _dataSource.Rows[i];
                int x = -1, y = -1;
                if (dr[xColumnName] != DBNull.Value)
                {
                    switch (xColumn.DataType)
                    {
                        case TableColumn.DataSubtypes.StringDate:
                        case TableColumn.DataSubtypes.Date:
                            DateTime dateValue = Convert.ToDateTime(dr[xColumnName]);
                            x = xColumn.ValueIndex(dateValue);
                            break;

                        case TableColumn.DataSubtypes.NativeInteger:
                            x = xColumn.ValueIndex(Convert.ToInt64(dr[xColumnName]));
                            break;

                        case TableColumn.DataSubtypes.StringInteger:
                            strValue = dr[xColumnName].ToString().TrimEnd();
                            if (strValue.Length > 0)
                                x = xColumn.ValueIndex(strValue);
                            break;

                        case TableColumn.DataSubtypes.NativeDecimal:
                            x = xColumn.ValueIndex(Convert.ToDouble(dr[xColumnName]));
                            break;

                        case TableColumn.DataSubtypes.StringDecimal:
                            strValue = dr[xColumnName].ToString().TrimEnd();
                            if (strValue.Length > 0)
                                x = xColumn.ValueIndex(strValue);
                            break;

                        case TableColumn.DataSubtypes.Text:
                            string valueX = dr[xColumnName].ToString().TrimEnd();
                            if (valueX.Length > 0)
                            {
                                x = xColumn.ValueIndex(valueX);
                            }
                            break;

                        case TableColumn.DataSubtypes.ByteArray:
                            var byteValue = (byte[])(dr[xColumnName]);
                            x = xColumn.ValueIndex(TimestampToString(byteValue));
                            break;

                        default:
                            x = -2;
                            break;
                    }
                }
                x += dx;

                if (dr[yColumnName] != DBNull.Value)
                {
                    y = dateGrouper.IndexOf(Convert.ToDateTime(dr[yColumnName]));
                }
                else
                {
                    if (!excludeBlanks)
                    {
                        y = 0;
                    }
                    else
                    {
                        throw new InvalidDataException();
                    }
                }

                if (x >= 0 && y >= 0)
                {
                    result[y, x] += 1;
                }

                if (cnt > 20000 & progressStep > 1)
                {
                    if (i % progressStep == 0)
                    {
                        ProgressPercent = (float)(i * 100 / (double)_dataSource.Rows.Count);
                        if (ProgressPercent > 1)
                        {
                            DataBuildProgressChange?.Invoke(this, new EventArgs());
                        }
                    }
                }
            }

            for (int i = 0; i < yBound; i++)
            {
                decimal total = 0;
                for (int j = 0; j < xBound; j++)
                {
                    if (result[i, j].HasValue)
                        total += (decimal)result[i, j];
                }
                result[i, xBound] = total;
            }

            return result;
        }

        /// <summary>
        /// Build pivottable with periods as y-axis asynx version
        /// </summary>
        /// <param name="yColumnName">y-axis column name</param>
        /// <param name="periods">periods for y-axis</param>
        /// <param name="xColumnName">x-axis column name</param>
        /// <param name="xMembers">members of x-axis</param>
        /// <param name="cancellationToken">cancelation token</param>
        /// <returns></returns>
        public Task<decimal?[,]> BuildPivotTableValueAsync(string yColumnName, List<TimePeriod> periods,
            string xColumnName, CancellationToken cancellationToken)
        {
            Task<decimal?[,]> task = null;

            // Start a task a return it
            task = Task.Run(() =>
            {
                TableColumn xColumn = GetColumn(xColumnName);
                int xBound = xColumn.GetCount(false);
                int yBound = periods.Count;

                var result = new decimal?[yBound, xBound + 1];
                for (int i = 0; i < yBound; i++)
                {
                    for (int j = 0; j <= xBound; j++)
                        result[i, j] = 0;
                }

                int dx = xColumn.HasBlanks ? 1 : 0;
                long cnt = _dataSource.Rows.Count;
                long progressStep = (long)(cnt / 10);
                string strValue;
                for (int i = 0; i < cnt; i++)
                {
                    // Check if a cancellation is requested, if yes,
                    // throw a TaskCanceledException.
                    if (cancellationToken.IsCancellationRequested)
                        throw new TaskCanceledException(task);

                    cancellationToken.ThrowIfCancellationRequested();

                    var dr = _dataSource.Rows[i];
                    int x = -1, y = -1;
                    if (dr[xColumnName] != DBNull.Value)
                    {
                        switch (xColumn.DataType)
                        {
                            case TableColumn.DataSubtypes.StringDate:
                            case TableColumn.DataSubtypes.Date:
                                DateTime dateValue = Convert.ToDateTime(dr[xColumnName]);
                                x = xColumn.ValueIndex(dateValue);
                                break;

                            case TableColumn.DataSubtypes.NativeInteger:
                                x = xColumn.ValueIndex(Convert.ToInt64(dr[xColumnName]));
                                break;

                            case TableColumn.DataSubtypes.StringInteger:
                                strValue = dr[xColumnName].ToString().TrimEnd();
                                if (strValue.Length > 0)
                                    x = xColumn.ValueIndex(strValue);
                                break;

                            case TableColumn.DataSubtypes.NativeDecimal:
                                x = xColumn.ValueIndex(Convert.ToDouble(dr[xColumnName]));
                                break;

                            case TableColumn.DataSubtypes.StringDecimal:
                                strValue = dr[xColumnName].ToString().TrimEnd();
                                if (strValue.Length > 0)
                                    x = xColumn.ValueIndex(strValue);
                                break;

                            case TableColumn.DataSubtypes.Text:
                                string valueX = dr[xColumnName].ToString().TrimEnd();
                                if (valueX.Length > 0)
                                {
                                    x = xColumn.ValueIndex(valueX);
                                }
                                break;

                            case TableColumn.DataSubtypes.ByteArray:
                                var byteValue = (byte[])(dr[xColumnName]);
                                x = xColumn.ValueIndex(TimestampToString(byteValue));
                                break;

                            default:
                                x = -2;
                                break;
                        }
                    }
                    x += dx;

                    if (dr[yColumnName] != DBNull.Value)
                    {
                        y = IndexOfTimePeriod(periods, Convert.ToDateTime(dr[yColumnName]));
                    }
                    else
                    {
                        if (periods[0].Year == 0)
                            y = 0;
                    }

                    if (x >= 0 && y >= 0)
                    {
                        result[y, x] += 1;
                    }

                    if (cnt > 20000 & progressStep > 1)
                    {
                        if (i % progressStep == 0)
                        {
                            ProgressPercent = (float)(i * 100 / (double)_dataSource.Rows.Count);
                            if (ProgressPercent > 1)
                            {
                                DataBuildProgressChange?.Invoke(this, new EventArgs());
                            }
                        }
                    }
                }

                for (int i = 0; i < yBound; i++)
                {
                    decimal total = 0;
                    for (int j = 0; j < xBound; j++)
                    {
                        if (result[i, j].HasValue)
                            total += (decimal)result[i, j];
                    }
                    result[i, xBound] = total;
                }

                return result;
            });

            return task;
        }

        public Task<decimal?[,]> BuildPivotTableValueAsync(string yColumnName, DateGrouper dateGrouper,
            string xColumnName, bool excludeBlanks, CancellationToken cancellationToken)
        {
            Task<decimal?[,]> task = null;

            // Start a task a return it
            task = Task.Run(() =>
            {
                TableColumn xColumn = GetColumn(xColumnName);
                int xBound = xColumn.GetCount(false);
                int yBound = dateGrouper.Periods.Count;

                var result = new decimal?[yBound, xBound + 1];
                for (int i = 0; i < yBound; i++)
                {
                    for (int j = 0; j <= xBound; j++)
                        result[i, j] = 0;
                }

                int dx = xColumn.HasBlanks ? 1 : 0;
                long cnt = _dataSource.Rows.Count;
                long progressStep = (long)(cnt / 10);
                string strValue;
                for (int i = 0; i < cnt; i++)
                {
                    // Check if a cancellation is requested, if yes,
                    // throw a TaskCanceledException.
                    if (cancellationToken.IsCancellationRequested)
                        throw new TaskCanceledException(task);

                    cancellationToken.ThrowIfCancellationRequested();

                    var dr = _dataSource.Rows[i];
                    int x = -1, y = -1;
                    if (dr[xColumnName] != DBNull.Value)
                    {
                        switch (xColumn.DataType)
                        {
                            case TableColumn.DataSubtypes.StringDate:
                            case TableColumn.DataSubtypes.Date:
                                DateTime dateValue = Convert.ToDateTime(dr[xColumnName]);
                                x = xColumn.ValueIndex(dateValue);
                                break;

                            case TableColumn.DataSubtypes.NativeInteger:
                                x = xColumn.ValueIndex(Convert.ToInt64(dr[xColumnName]));
                                break;

                            case TableColumn.DataSubtypes.StringInteger:
                                strValue = dr[xColumnName].ToString().TrimEnd();
                                if (strValue.Length > 0)
                                    x = xColumn.ValueIndex(strValue);
                                break;

                            case TableColumn.DataSubtypes.NativeDecimal:
                                x = xColumn.ValueIndex(Convert.ToDouble(dr[xColumnName]));
                                break;

                            case TableColumn.DataSubtypes.StringDecimal:
                                strValue = dr[xColumnName].ToString().TrimEnd();
                                if (strValue.Length > 0)
                                    x = xColumn.ValueIndex(strValue);
                                break;

                            case TableColumn.DataSubtypes.Text:
                                string valueX = dr[xColumnName].ToString().TrimEnd();
                                if (valueX.Length > 0)
                                {
                                    x = xColumn.ValueIndex(valueX);
                                }
                                break;

                            case TableColumn.DataSubtypes.ByteArray:
                                var byteValue = (byte[])(dr[xColumnName]);
                                x = xColumn.ValueIndex(TimestampToString(byteValue));
                                break;

                            default:
                                x = -2;
                                break;
                        }
                    }
                    x += dx;

                    if (dr[yColumnName] != DBNull.Value)
                    {
                        y = dateGrouper.IndexOf(Convert.ToDateTime(dr[yColumnName]));
                    }
                    else
                    {
                        if (!excludeBlanks)
                        {
                            y = 0;
                        }
                        else
                        {
                            throw new InvalidDataException();
                        }
                    }

                    if (x >= 0 && y >= 0)
                    {
                        result[y, x] += 1;
                    }

                    if (cnt > 20000 & progressStep > 1)
                    {
                        if (i % progressStep == 0)
                        {
                            ProgressPercent = (float)(i * 100 / (double)_dataSource.Rows.Count);
                            if (ProgressPercent > 1)
                            {
                                DataBuildProgressChange?.Invoke(this, new EventArgs());
                            }
                        }
                    }
                }

                for (int i = 0; i < yBound; i++)
                {
                    decimal total = 0;
                    for (int j = 0; j < xBound; j++)
                    {
                        if (result[i, j].HasValue)
                            total += (decimal)result[i, j];
                    }
                    result[i, xBound] = total;
                }

                return result;
            });

            return task;
        }

        /// <summary>
        /// Comvert byte array to an Hex string
        /// </summary>
        /// <param name="byteArray"></param>
        /// <returns></returns>
        private string TimestampToString(byte[] byteArray)
        {
            return "0x" + string.Concat(Array.ConvertAll(byteArray, x => x.ToString("X2")));
        }

        /// <summary>
        /// Returns count of blanks
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public int GetNumOfBlanks(string columnName)
        {
            TableColumn column = GetColumn(columnName);
            if (column != null)
                return column.BlanksCount;
            else
                return 0;
        }

        /// <summary>
        /// Build frequencies value for a specified column
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="excludeBlanks"></param>
        /// <returns></returns>
        public decimal?[] BuildFrequencyTable(string columnName, bool excludeBlanks)
        {
            TableColumn column = GetColumn(columnName);
            if (column != null)
            {
                var values = column.GetUniqueValueCounts(excludeBlanks);
                var result = new decimal?[values.Count];
                for (int i = 0; i < values.Count; i++)
                {
                    result[i] = values[i];
                }
                return result;
            }
            else
            {
                return null;
            }
        }

        public Task<decimal?[]> BuildFrequencyTableAsync(string columnName, bool excludeBlanks, CancellationToken cancellationToken)
        {
            Task<decimal?[]> task = null;

            // Start a task a return it
            task = Task.Run(() =>
            {
                TableColumn column = GetColumn(columnName);
                if (column != null)
                {
                    var values = column.GetUniqueValueCounts(excludeBlanks);
                    var result = new decimal?[values.Count];
                    for (int i = 0; i < values.Count; i++)
                    {
                        // Check if a cancellation is requested, if yes,
                        // throw a TaskCanceledException.
                        if (cancellationToken.IsCancellationRequested)
                            throw new TaskCanceledException(task);

                        cancellationToken.ThrowIfCancellationRequested();

                        result[i] = values[i];
                    }
                    return result;
                }
                else
                {
                    return null;
                }
            });

            return task;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="periods"></param>
        /// <param name="excludeBlanks"></param>
        /// <param name="outPeriods"></param>
        /// <param name="outValues"></param>
        /// <returns></returns>
        public void BuildFrequencyTable(string columnName, List<TimePeriod> periods, bool excludeBlanks,
            out List<TimePeriod> outPeriods, out decimal?[] outValues)
        {
            var column = GetColumn(columnName);  // _columns.Find(s => Equals(s.ColumnName, columnName));

            int yBound = periods.Count;
            var periodValues = new long[yBound];
            var values = column.GetUniqueValueCounts(excludeBlanks);
            var dates = column.GetUniqueDates(excludeBlanks);
            for (int i = 0; i < dates.Count; i++)
            {
                int x = IndexOfTimePeriod(periods, dates[i]);
                periodValues[x] += values[i];
            }

            outPeriods = new List<TimePeriod>();
            var resultValues = new List<decimal?>();
            for (int i = 0; i < periodValues.Length; i++)
            {
                if (periodValues[i] != 0)
                {
                    if (periods[i].Year == 0)
                    {
                        if (!excludeBlanks)
                        {
                            outPeriods.Add(new TimePeriod(periods[i]));
                            resultValues.Add((decimal)periodValues[i]);
                        }
                    }
                    else
                    {
                        outPeriods.Add(new TimePeriod(periods[i]));
                        resultValues.Add((decimal)periodValues[i]);
                    }
                }
            }

            outValues = resultValues.ToArray();
        }

        public decimal?[] BuildFrequencyTable(string columnName, DateGrouper dateGrouper, bool excludeBlanks)
        {
            var column = GetColumn(columnName);
            int yBound = dateGrouper.Periods.Count;
            var periodValues = new decimal?[yBound];
            for (int i = 0; i < yBound; i++)
            {
                periodValues[i] = 0;
            }
            var values = column.GetUniqueValueCounts(excludeBlanks);
            var dates = column.GetUniqueDates(excludeBlanks);
            for (int i = 0; i < dates.Count; i++)
            {
                int x = dateGrouper.IndexOf(dates[i]);
                periodValues[x] += values[i];
            }

            //var resultValues = new List<decimal?>();
            //for (int i = 0; i < periodValues.Length; i++)
            //{
            //    if (periodValues[i] != 0)
            //    {
            //        if (periods[i].Year == 0)
            //        {
            //            if (!excludeBlanks)
            //            {
            //                resultValues.Add((decimal)periodValues[i]);
            //            }
            //        }
            //        else
            //        {
            //            resultValues.Add((decimal)periodValues[i]);
            //        }
            //    }
            //}

            return periodValues;
        }

        /// <summary>
        /// Find the index of the date in the period list
        /// </summary>
        /// <param name="periods"></param>
        /// <param name="eventDate"></param>
        /// <returns></returns>
        private int IndexOfTimePeriod(List<TimePeriod> periods, DateTime eventDate)
        {
            if (eventDate < periods[0].StartDate)
                return -1;  // before first period
            if (eventDate > periods[periods.Count - 1].EndDate)
                return -1;  // after last period

            return TimePeriodBinarySearch(periods, eventDate, 0, periods.Count - 1);
        }

        /// <summary>
        /// Do binary search to determine the index of a date in a period list
        /// </summary>
        /// <param name="periods">Time period list</param>
        /// <param name="eventDate">date to find the index</param>
        /// <param name="startIndex">Start index in the period list to search</param>
        /// <param name="endIndex">End index in the period list to search</param>
        /// <returns></returns>
        private int TimePeriodBinarySearch(List<TimePeriod> periods, DateTime eventDate, int startIndex, int endIndex)
        {
            if (startIndex > endIndex)
                return -1;

            if (eventDate >= periods[startIndex].StartDate && eventDate <= periods[startIndex].EndDate)
                return startIndex;

            if (startIndex == endIndex)
                return -1;

            if (eventDate >= periods[endIndex].StartDate && eventDate <= periods[endIndex].EndDate)
                return endIndex;

            if (startIndex + 2 == endIndex)
            {
                if (eventDate >= periods[startIndex + 1].StartDate && eventDate <= periods[startIndex + 1].EndDate)
                { return startIndex + 1; }
                else
                { return -1; }
            }

            int midIndex = startIndex + (endIndex - startIndex) / 2;
            if (eventDate >= periods[midIndex].StartDate)
                return TimePeriodBinarySearch(periods, eventDate, midIndex, endIndex);
            else
                return TimePeriodBinarySearch(periods, eventDate, startIndex, midIndex - 1);
        }


        /// <summary>
        /// Get data set on specific colume with specific value
        /// </summary>
        /// <param name="column"></param>
        /// <param name="selectedValue"></param>
        /// <returns></returns>
        public DataTable GetMatchedRows(string column, string selectedValue)
        {
            var result = _dataSource.Clone();
            int cIndex = GetColumnIndex(column);
            if (cIndex >= 0)
            {
                bool isDateColumn = IsDateColumn(column);
                if (Equals(selectedValue, BLANKS))
                    selectedValue = "";
                foreach (DataRow dr in _dataSource.Rows)
                {
                    string cValue = GetValueFromDataRow(dr, cIndex, isDateColumn);
                    if (selectedValue.Length == 0)
                    {
                        if (cValue.Length == 0)
                        {
                            result.ImportRow(dr);
                        }
                    }
                    else if (string.Compare(cValue, selectedValue, true) == 0)
                    {
                        result.ImportRow(dr);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Get data set on specific colume within specified time period
        /// </summary>
        /// <param name="column">Column name</param>
        /// <param name="startDate">Time period start point</param>
        /// <param name="endDate">Time period end point</param>
        /// <returns></returns>
        public DataTable GetMatchedRows(string column, DateTime startDate, DateTime endDate)
        {
            var result = _dataSource.Clone();
            if (IsDateColumn(column))
            {
                int cIndex = GetColumnIndex(column);
                if (cIndex >= 0)
                {
                    foreach (DataRow dr in _dataSource.Rows)
                    {
                        if (dr[cIndex] != DBNull.Value)
                        {
                            DateTime eventDate = Convert.ToDateTime(dr[cIndex]);
                            if (eventDate >= startDate && eventDate <= endDate)
                                result.ImportRow(dr);
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Returns a data set that value of columnX matches selectredValueX and
        /// value of columnY matches selectedValueY
        /// </summary>
        /// <param name="columnX">Column name X</param>
        /// <param name="selectedValueX">Value to match for columnX</param>
        /// <param name="columnY">Column name Y</param>
        /// <param name="selectedValueY">Value to match for columnY</param>
        /// <returns></returns>
        public DataTable GetMatchedRows(string columnX, string selectedValueX, string columnY, string selectedValueY)
        {
            var result = _dataSource.Clone();
            int colIndexX = GetColumnIndex(columnX);
            int colIndexY = GetColumnIndex(columnY);
            if (colIndexX >= 0 && colIndexY >= 0)
            {
                bool isDateColumnX = IsDateColumn(columnX);
                bool isDateColumnY = IsDateColumn(columnY);
                if (Equals(selectedValueX, BLANKS))
                    selectedValueX = "";
                if (Equals(selectedValueY, BLANKS))
                    selectedValueY = "";
                foreach (DataRow dr in _dataSource.Rows)
                {
                    string xValue = GetValueFromDataRow(dr, colIndexX, isDateColumnX);
                    bool xMatched = false;
                    if (selectedValueX.Length == 0)
                    {
                        if (xValue.Length == 0)
                        {
                            xMatched = true;
                        }
                    }
                    else if (string.Compare(xValue, selectedValueX, true) == 0)
                    {
                        xMatched = true;
                    }

                    if (xMatched)
                    {
                        if (Equals(columnX, columnY))
                        {
                            result.ImportRow(dr);
                        }
                        else
                        {
                            string yValue = GetValueFromDataRow(dr, colIndexY, isDateColumnY);
                            if (selectedValueY.Length == 0)
                            {
                                if (yValue.Length == 0)
                                {
                                    result.ImportRow(dr);
                                }
                            }
                            else if (string.Compare(yValue, selectedValueY, true) == 0)
                            {
                                result.ImportRow(dr);
                            }
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Returns a data set that value of columnX matches selectredValueX and
        /// value of dateColumn between startDate and endDate
        /// </summary>
        /// <param name="columnY"></param>
        /// <param name="selectedValueY"></param>
        /// <param name="dateColumn"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataTable GetMatchedRows(string columnY, string selectedValueY, string dateColumn, DateTime startDate, DateTime endDate)
        {
            var result = _dataSource.Clone();
            int colIndexX = GetColumnIndex(dateColumn);
            int colIndexY = GetColumnIndex(columnY);
            if (colIndexX >= 0 && colIndexY >= 0)
            {
                if (Equals(selectedValueY, BLANKS))
                    selectedValueY = "";
                foreach (DataRow dr in _dataSource.Rows)
                {
                    if (dr[colIndexX] != DBNull.Value)
                    {
                        DateTime eventDate = Convert.ToDateTime(dr[colIndexX]);
                        if (eventDate >= startDate && eventDate <= endDate)
                        {
                            string cValue = GetValueFromDataRow(dr, colIndexY, false);
                            if (selectedValueY.Length == 0)
                            {
                                if (cValue.Length == 0)
                                {
                                    result.ImportRow(dr);
                                }
                            }
                            else if (string.Compare(cValue, selectedValueY, true) == 0)
                            {
                                result.ImportRow(dr);
                            }
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="columnIndex"></param>
        /// <param name="isDateColumn"></param>
        /// <returns></returns>
        private string GetValueFromDataRow(DataRow dr, int columnIndex, bool isDateColumn)
        {
            string result = "";
            if (dr[columnIndex] != DBNull.Value)
            {
                if (isDateColumn)
                {
                    DateTime value = Convert.ToDateTime(dr[columnIndex]);
                    result = value.ToShortDateString();
                }
                else
                {
                    result = dr[columnIndex].ToString().TrimEnd();
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    _dataSource?.Dispose();
                    if (Columns != null)
                    {
                        foreach (var column in Columns)
                        {
                            column.Clear();
                        }
                        Columns.Clear();
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                // free native resources if there are any.
                //if (nativeResource != IntPtr.Zero)
                //{
                //    Marshal.FreeHGlobal(nativeResource);
                //    nativeResource = IntPtr.Zero;
                //}

                disposedValue = true;
            }
        }

        /// <summary>
        /// NOTE: Leave out the finalizer altogether if this class doesn't
        /// own unmanaged resources, but leave the other methods
        /// exactly as they are.
        /// </summary>
        ~TableAnalysis()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

    }
}
