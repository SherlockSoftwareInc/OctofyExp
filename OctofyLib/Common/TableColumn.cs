using System;
using System.Collections.Generic;
using System.Data;

namespace OctofyLib
{
    /// <summary>
    /// Data table column object
    /// </summary>
    public class TableColumn : IDisposable
    {
        /// <summary>
        /// Column process results
        /// </summary>
        public enum TableColumnProcessResults
        {
            /// <summary>
            /// Analysis column succeed
            /// </summary>
            Success,
            /// <summary>
            /// Analysis failed due to number of categories exceed limitation
            /// </summary>
            CategoryOverflow,
            /// <summary>
            /// Analysis failed dure to length of category name exceed limitation
            /// </summary>
            NameLengthOverflow,
            /// <summary>
            /// Analysis failed dure to a unknow data type of the column
            /// </summary>
            UnknowDataType
        }

        /// <summary>
        /// Data type group
        /// </summary>
        public enum ColumnTypes
        {
            /// <summary>
            /// Column contains only date
            /// </summary>
            Date,
            /// <summary>
            /// Column contains categorical values
            /// </summary>
            Category,
            CategoryExceedMaxMembers,
            CategoryExceedMaxLength,
            /// <summary>
            /// Column contains non-categorical value or length exceed the limit
            /// </summary>
            Other
        }

        /// <summary>
        /// Data type for internal use
        /// </summary>
        public enum DataSubtypes
        {
            Date,
            StringDate,
            NativeInteger,
            NativeDecimal,
            Text,
            StringInteger,
            StringDecimal,
            ByteArray,
            MonthName,
            WeekName,
            Other
        }

        /// <summary>
        /// Create class by data table and column index
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="columnIndex"></param>
        /// <param name="maxUniqueValues"></param>
        /// <param name="valueLengthLimit"></param>
        public TableColumn(DataTable dataTable, int columnIndex, long maxUniqueValues, int valueLengthLimit)
        {
            this.ColumnIndex = columnIndex;
            _valueLengthLimit = valueLengthLimit;
            ColumnName = dataTable.Columns[columnIndex].ColumnName;

            DataType = GetDataType(dataTable.Columns[columnIndex].DataType);
            switch (DataType)
            {
                case DataSubtypes.Date:
                    ColumnType = ColumnTypes.Date;
                    break;
                case DataSubtypes.NativeInteger:
                case DataSubtypes.NativeDecimal:
                case DataSubtypes.Text:
                case DataSubtypes.ByteArray:
                    ColumnType = ColumnTypes.Category;
                    break;
                default:
                    ColumnType = ColumnTypes.Other;
                    break;
            }

            Result = TableColumnProcessResults.Success;
            foreach (DataRow dr in dataTable.Rows)
            {
                if (disposedValue)
                    return;

                if (_count < maxUniqueValues)
                {
                    AddValue(dr[columnIndex]);
                    if (_lengthOverflow)
                    {
                        Clear();
                        ColumnType = ColumnTypes.CategoryExceedMaxLength;
                        Result = TableColumnProcessResults.NameLengthOverflow;
                        break;
                    }
                }
                else
                {
                    Clear();
                    ColumnType = ColumnTypes.CategoryExceedMaxMembers;
                    Result = TableColumnProcessResults.CategoryOverflow;
                    break;
                }
            }

            Enhancement();

            //if (ColumnType == ColumnTypes.Category)
            //{
            //    var listType = CommonLib.SortList.ListValueType(_strValues);
            //    if (listType != CommonLib.SortList.ListValueTypes.String)
            //    {
            //        _strValues = CommonLib.SortList.SortTheList(_strValues, listType);
            //    }

            //    if (HasBlanks)
            //    {
            //        _strValues.Insert(0, BLANKS);
            //        Count++;
            //    }
            //}
        }

        /// <summary>
        /// Create the class from a frequency data table
        /// where first column is the category and second column is the counts
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="valueLengthLimit"></param>
        public TableColumn(DataTable dataTable, int valueLengthLimit)
        {
            this.ColumnIndex = 0;
            _valueLengthLimit = valueLengthLimit;
            ColumnName = dataTable.Columns[0].ColumnName;

            DataType = GetDataType(dataTable.Columns[0].DataType);
            switch (DataType)
            {
                case DataSubtypes.Date:
                    ColumnType = ColumnTypes.Date;
                    break;
                case DataSubtypes.NativeInteger:
                case DataSubtypes.NativeDecimal:
                case DataSubtypes.Text:
                case DataSubtypes.ByteArray:
                    ColumnType = ColumnTypes.Category;
                    break;
                default:
                    ColumnType = ColumnTypes.Other;
                    break;
            }

            //bool result = true;
            foreach (DataRow dr in dataTable.Rows)
            {
                if (disposedValue)
                    return;

                AddValue(dr[0], Convert.ToInt32(dr[1]));
                if (_lengthOverflow)
                {
                    Clear();
                    ColumnType = ColumnTypes.Other;
                    Result = TableColumnProcessResults.NameLengthOverflow;
                    break;
                }
            }
            Enhancement();
        }

        private string BLANKS = Properties.Resources.B003;  // "(Blanks)";
        private readonly List<string> _strValues = new List<string>();
        private readonly List<DateTime> _dateValues = new List<DateTime>();
        private readonly List<long> _intValues = new List<long>();
        private readonly List<double> _numValues = new List<double>();

        private readonly List<int> _valueCount = new List<int>();
        private readonly int _valueLengthLimit = 512;
        private bool _lengthOverflow = false;
        private bool disposedValue;

        public DataSubtypes DataType { get; private set; }

        /// <summary>
        /// Enhance the string category in case numbers, dates, month and week day recorded in a string column
        /// </summary>
        private void Enhancement()
        {
            if (DataType == DataSubtypes.Text && Result == TableColumnProcessResults.Success && _count > 1)
            {
                if (IsMonths())
                    // enhancement 1: if the category members are month names,
                    // sort it by month order
                    SortMonthList();
                else if (IsWeek())
                    // enhancement 2: if the category members are week names,
                    // sort it by week day order
                    SortWeekList();
                //else
                //{
                //    if (_strValues[0].IsNumeric())
                //    {
                //        if (_strValues[_strValues.Count - 1].IsNumeric())
                //        {
                //            // enhancement 3: if the column data type is string but all category members 
                //            // are actually numeric numbers, sort it in numeric value order
                //            //ChangeToNumeric();
                //        }
                //    }
                //    else if (IsSuspectedDateTime())
                //    {
                //        // enhancement 4: if the column data type is string but all category members 
                //        // are date time string, sort it in date time value order
                //        ChangeToDateTime();
                //    }
                //}
            }
        }

        /// <summary>
        /// Checks if the values in string list are all date time string (or empty)
        /// **Enhancement: for performance, check first 10 and last 10 items in the list
        /// </summary>
        /// <returns></returns>
        private bool IsSuspectedDateTime()
        {
            bool result = false;

            if (_strValues.Count < 20)
            {
                for (int i = 0; i < _strValues.Count; i++)
                {
                    if (_strValues[i].TrimEnd().Length > 0)
                    {
                        if (!_strValues[i].IsDateTime())
                            return false;
                        else
                        {
                            if (!result)
                                result = true;
                        }
                    }
                }
                return result;
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    {
                        if (_strValues[i].TrimEnd().Length > 0)
                        {
                            if (!_strValues[i].IsDateTime())
                                return false;
                            else
                            {
                                if (!result)
                                    result = true;
                            }
                        }
                    }
                }

                for (int i = _strValues.Count - 10; i < _strValues.Count; i++)
                {
                    {
                        if (_strValues[i].TrimEnd().Length > 0)
                        {
                            if (!_strValues[i].IsDateTime())
                                return false;
                            else
                            {
                                if (!result)
                                    result = true;
                            }
                        }
                    }
                }
                return result;
            }
        }

        /// <summary>
        /// Try to convert string categories to numeric categories
        /// </summary>
        private void ChangeToNumeric()
        {
            bool isDecimal = false;
            var sList = new List<SortItem>();
            int blanksCount = 0;
            for (int i = 0; i < _strValues.Count; i++)
            {
                string strValue = _strValues[i].TrimEnd();
                if (strValue.Length == 0)
                {
                    blanksCount++;
                }
                else
                {
                    if (strValue.StartsWith("0") && !strValue.StartsWith("0.")) // if a string with 0 but not 0.x
                    {
                        return;
                    }

                    if (double.TryParse(strValue, out double value))
                    {
                        sList.Add(new SortItem(value, _strValues[i], _valueCount[i]));
                        if (!isDecimal)
                            if (strValue.IndexOf(".") >= 0)
                                isDecimal = true;
                    }
                    else
                    {
                        return;
                    }
                }
            }
            if (sList.Count > 0)
            {
                sList.Sort((x, y) => x.SortValue.CompareTo(y.SortValue));

                _strValues.Clear();
                _valueCount.Clear();
                for (int i = 0; i < sList.Count; i++)
                {
                    _strValues.Add(sList[i].Value);
                    _numValues.Add(sList[i].SortValue);
                    _valueCount.Add(sList[i].Count);
                }
                if (blanksCount > 0)
                {
                    if (!HasBlanks)
                        HasBlanks = true;
                    BlanksCount += blanksCount;
                }

                if (isDecimal)
                    DataType = DataSubtypes.StringDecimal;
                else
                    DataType = DataSubtypes.StringInteger;
            }
        }

        /// <summary>
        /// Try to convert string categories to date time categories
        /// </summary>
        private void ChangeToDateTime()
        {
            var sList = new List<DateSortItem>();
            int blankCount = 0;
            for (int i = 0; i < _strValues.Count; i++)
            {
                string strValue = _strValues[i].TrimEnd();
                if (strValue.Length > 0)
                {
                    if (DateTime.TryParse(_strValues[i], out DateTime date))
                    {
                        sList.Add(new DateSortItem(date, _strValues[i], _valueCount[i]));
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    blankCount++;
                }
            }
            if (sList.Count > 0)
            {
                sList.Sort((x, y) => x.Date.CompareTo(y.Date));

                _strValues.Clear();
                _valueCount.Clear();
                for (int i = 0; i < sList.Count; i++)
                {
                    _strValues.Add(sList[i].Text);
                    _dateValues.Add(sList[i].Date);
                    _valueCount.Add(sList[i].Count);
                }
                if (blankCount > 0)
                {
                    if (!HasBlanks)
                        HasBlanks = true;
                    BlanksCount += blankCount;
                }
                DataType = DataSubtypes.StringDate;
                ColumnType = ColumnTypes.Date;
            }

        }

        /// <summary>
        /// Gets or set process result
        /// </summary>
        public TableColumnProcessResults Result { get; set; }

        /// <summary>
        /// Gets a value indicates wether the column has empty values
        /// </summary>
        public bool HasBlanks { get; private set; } = false;

        /// <summary>
        /// Gets count of blanks
        /// </summary>
        public int BlanksCount { get; private set; } = 0;

        /// <summary>
        /// Gets or sets column name
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// Gets or sets column index
        /// </summary>
        public int ColumnIndex { get; set; }

        /// <summary>
        /// Column type
        /// </summary>
        public ColumnTypes ColumnType { get; set; }

        ///// <summary>
        ///// Gets or sets number of unique values
        ///// </summary>
        //public int Count { get; set; }
        private int _count;

        /// <summary>
        /// Returns number of unique categorys (indlude or exclude blanks)
        /// </summary>
        /// <param name="excludeBlanks"></param>
        /// <returns></returns>
        public int GetCount(bool excludeBlanks)
        {
            int result = _count;
            if (!excludeBlanks)
            {
                if (HasBlanks || BlanksCount > 0)
                    result++;
            }
            return result;
        }

        /// <summary>
        /// Clear any unique values
        /// </summary>
        public void Clear()
        {
            HasBlanks = false;
            _strValues.Clear();
            _dateValues.Clear();
            _intValues.Clear();
            _numValues.Clear();
            _valueCount.Clear();
        }

        /// <summary>
        /// Column unique values
        /// </summary>
        public List<string> GetUniqueValues(bool excludeBlanks)
        {
            var result = new List<string>();
            if (HasBlanks && !excludeBlanks)
                result.Add(BLANKS);

            switch (DataType)
            {
                case DataSubtypes.StringDate:
                case DataSubtypes.Date:
                    foreach (var dateItem in _dateValues)
                    {
                        result.Add(dateItem.ToShortDateString());
                    }
                    break;

                case DataSubtypes.NativeInteger:
                //foreach (var value in _intValues)
                //{
                //    result.Add(value.ToString());
                //}
                //break;

                case DataSubtypes.NativeDecimal:
                //foreach (var value in _numValues)
                //{
                //    result.Add(value.ToString());
                //}
                //break;
                case DataSubtypes.StringInteger:
                case DataSubtypes.StringDecimal:
                case DataSubtypes.ByteArray:
                case DataSubtypes.Text:
                    //int startIndex = 0;
                    //if (!excludeBlanks && HasBlanks)
                    //    startIndex = 1;
                    for (int i = 0; i < _strValues.Count; i++)
                    {
                        result.Add(_strValues[i]);
                    }
                    break;

                //case DataSubtypes.MonthName:
                //    break;
                //case DataSubtypes.WeekName:
                //    break;
                //case DataSubtypes.Other:
                //    break;
                default:
                    break;
            }

            return result;
        }

        /// <summary>
        /// Returns all unique dates
        /// </summary>
        /// <param name="excludeBlanks"></param>
        /// <returns></returns>
        public List<DateTime> GetUniqueDates(bool excludeBlanks)
        {
            if (ColumnType == ColumnTypes.Date)
            {
                var result = new List<DateTime>();
                if (HasBlanks && !excludeBlanks)
                    result.Add(DateTime.MinValue);

                foreach (var dateItem in _dateValues)
                {
                    result.Add(dateItem);
                }
                return result;
            }
            return new List<DateTime>();
        }

        /// <summary>
        /// Returs count of each unique value
        /// </summary>
        public List<int> GetUniqueValueCounts(bool excludeBlanks)
        {
            var result = new List<int>();
            if (HasBlanks && !excludeBlanks)
                result.Add(BlanksCount);
            foreach (var count in _valueCount)
            {
                result.Add(count);
            }
            return result;
        }

        /// <summary>
        /// Add integer value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="counts"></param>
        public void AddValue(long value, int counts = 1)
        {
            int index = _intValues.BinarySearch(value);
            if (index < 0)
            {
                index = ~index;
                _intValues.Insert(index, value);
                _valueCount.Insert(index, counts);
                _strValues.Insert(index, value.ToString());
                _count++;
            }
            else
            {
                _valueCount[index] += counts;
            }
        }

        /// <summary>
        /// Add decimal value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="counts"></param>
        public void AddValue(double value, string text, int counts = 1)
        {
            int index = _numValues.BinarySearch(value);
            if (index < 0)
            {
                index = ~index;
                _numValues.Insert(index, value);
                _valueCount.Insert(index, counts);
                _strValues.Insert(index, text);
                _count++;
            }
            else
            {
                _valueCount[index] += counts;
            }
        }


        /// <summary>
        /// add a string value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="counts"></param>
        public void AddValue(string value, int counts = 1)
        {
            if (value.Length == 0)
            {
                HasBlanks = true;
                BlanksCount += counts;
            }
            else if (value.Length > _valueLengthLimit)
            {
                _lengthOverflow = true;
            }
            else
            {
                int index = _strValues.BinarySearch(value);
                if (index < 0)
                {
                    index = ~index;
                    _strValues.Insert(index, value);
                    _valueCount.Insert(index, counts);
                    _count++;
                }
                else
                {
                    _valueCount[index] += counts;
                }
            }
        }

        /// <summary>
        /// Add a datetime value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="counts"></param>
        public void AddValue(DateTime value, int counts = 1)
        {
            //ColumnType = ColumnTypes.Date;
            if (value == DateTime.MinValue || value == DateTime.MaxValue)
            {
                HasBlanks = true;
                BlanksCount += counts;
            }
            else
            {
                int index = _dateValues.BinarySearch(value);
                if (index < 0)
                {
                    index = ~index;
                    _dateValues.Insert(index, value);
                    _valueCount.Insert(index, counts);
                    _count++;
                }
                else
                {
                    _valueCount[index] += counts;
                }
            }
            //if (ColumnType == ColumnTypes.Category)
            //{
            //    if (HasBlanks)
            //        _strValues.Insert(0, BLANKS);
            //}
        }

        /// <summary>
        /// Add a value from DataRow
        /// </summary>
        /// <param name="value"></param>
        /// <param name="counts"></param>
        public void AddValue(object value, int counts = 1)
        {
            if (disposedValue)
                return;

            if (value == DBNull.Value)
            {
                HasBlanks = true;
                BlanksCount += counts;
            }
            else
            {
                switch (DataType)
                {
                    case DataSubtypes.Date:
                        DateTime dateValue = Convert.ToDateTime(value);
                        AddValue(dateValue.Date, counts);
                        break;

                    case DataSubtypes.NativeInteger:
                        long intValue = Convert.ToInt64(value);
                        AddValue(intValue, counts);
                        break;

                    case DataSubtypes.NativeDecimal:
                        double decValue = Convert.ToDouble(value);
                        AddValue(decValue, value.ToString(), counts);
                        break;

                    case DataSubtypes.ByteArray:
                        var baData = TimestampToString((byte[])value);
                        AddValue(baData, counts);
                        break;

                    case DataSubtypes.Text:
                        string strValue = value.ToString().TrimEnd();
                        AddValue(strValue, counts);
                        break;
                    default:
                        break;
                }
            }

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
        /// Get index of string value in unique values
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int ValueIndex(string value)
        {
            if (disposedValue)
                return -1;

            int result = -1;
            if (value.Length == 0 || value == BLANKS)
            {
                if (HasBlanks)
                    result = 0;
            }
            else
                result = _strValues.BinarySearch(value);
            //switch (DataType)
            //{
            //    case DataSubtypes.Date:
            //        result = _dateValues.BinarySearch(Convert.ToDateTime(value));
            //        break;

            //    case DataSubtypes.Integer:
            //        result = _intValues.BinarySearch(Convert.ToInt64(value));
            //        break;

            //    case DataSubtypes.Decimal:
            //        result = _numValues.BinarySearch(Convert.ToDecimal(value));
            //        break;

            //    case DataSubtypes.ByteArray:
            //    case DataSubtypes.Text:
            //        result = _strValues.BinarySearch(value);
            //        break;

            //    default:
            //        break;
            //}

            return result;
        }

        /// <summary>
        /// Get index of date value in unique values
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int ValueIndex(DateTime value)
        {
            return _dateValues.BinarySearch(value);
        }

        public int ValueIndex(long value)
        {
            return _intValues.BinarySearch(value);
        }

        public int ValueIndex(double value)
        {
            return _numValues.BinarySearch(value);
        }

        /// <summary>
        /// Get data type by System.Type
        /// </summary>
        /// <param name="dataType"></param>
        /// <returns></returns>
        private DataSubtypes GetDataType(System.Type dataType)
        {
            if (dataType == typeof(long) || dataType == typeof(int) || dataType == typeof(short) || dataType == typeof(byte))
            {
                return DataSubtypes.NativeInteger;
            }

            if (dataType == typeof(decimal) || dataType == typeof(double) || dataType == typeof(float))
            {
                return DataSubtypes.NativeDecimal;
            }

            if (dataType == typeof(DateTime))
            {
                return DataSubtypes.Date;
            }

            if (dataType == typeof(bool))
            {
                return DataSubtypes.Text;
            }

            if (dataType == typeof(string))
            {
                return DataSubtypes.Text;
            }

            if (dataType == typeof(byte[]))
            {
                return DataSubtypes.ByteArray;
            }

            return DataSubtypes.Other;
        }

        #region "Special sort for month or week name"
        private bool IsMonths()
        {
            if (DataType != DataSubtypes.Text || _strValues.Count == 0)
                return false;

            const string MONTH_NAMES = "||JANUARY|FEBRUARY|MARCH|APRIL|MAY|JUNE|JULY|AUGUST|SEPTEMBER|OCTOBER|NOVEMBER|DECEMBER|JAN|FEB|MAR|APR|JUN|JUL|AUG|SEP|OCT|NOV|DEC|";
            bool isMonthName = true;
            if (_strValues.Count <= 13)
            {
                for (int i = 0; i < _strValues.Count; i++)
                {
                    string value = _strValues[i];
                    if (value.Length > 0 && value != BLANKS)
                    {
                        value = "|" + value.ToUpper() + "|";
                        if (MONTH_NAMES.IndexOf(value) < 0)
                        {
                            isMonthName = false;
                            break;
                        }
                    }
                }
            }
            else
            {
                isMonthName = false;
            }
            return isMonthName;
        }

        private bool IsWeek()
        {
            if (DataType != DataSubtypes.Text || _strValues.Count == 0)
                return false;

            const string WEEKDAY_NAMES = "||MONDAY|TUESDAY|WEDNESDAY|THURSDAY|FRIDAY|SATURDAY|SUNDAY|MON|TUE|WED|THU|FRI|SAT|SUN|";
            bool isWeekName = true;
            if (_strValues.Count <= 8)
            {
                for (int i = 0; i < _strValues.Count; i++)
                {
                    string value = _strValues[i];
                    if (value.Length > 0 && value != BLANKS)
                    {
                        value = "|" + value.ToUpper() + "|";
                        if (WEEKDAY_NAMES.IndexOf(value) < 0)
                        {
                            isWeekName = false;
                            break;
                        }
                    }
                }
            }
            else
            {
                isWeekName = false;
            }
            return isWeekName;
        }


        private class SortItem
        {
            public SortItem(double sortValue, string value, int count)
            {
                this.SortValue = sortValue;
                this.Value = value;
                this.Count = count;
            }

            public double SortValue { get; set; }
            public string Value { get; set; }
            public int Count { get; set; }
        }

        private class DateSortItem
        {
            public DateSortItem(DateTime date, string text, int count)
            {
                this.Date = date;
                this.Text = text;
                this.Count = count;
            }

            public DateTime Date { get; set; }
            public string Text { get; set; }
            public int Count { get; set; }
        }


        /// <summary>
        /// Sort by month name
        /// </summary>
        private void SortMonthList()
        {
            var sList = new List<SortItem>();

            for (int i = 0; i < _strValues.Count; i++)
            {
                switch (_strValues[i].ToUpper())
                {
                    case "JANUARY":
                    case "JAN":
                        {
                            sList.Add(new SortItem(1, _strValues[i], _valueCount[i]));
                            break;
                        }

                    case "FEBRUARY":
                    case "FEB":
                        {
                            sList.Add(new SortItem(2, _strValues[i], _valueCount[i]));
                            break;
                        }

                    case "MARCH":
                    case "MAR":
                        {
                            sList.Add(new SortItem(3, _strValues[i], _valueCount[i]));
                            break;
                        }

                    case "APRIL":
                    case "APR":
                        {
                            sList.Add(new SortItem(4, _strValues[i], _valueCount[i]));
                            break;
                        }

                    case "MAY":
                        {
                            sList.Add(new SortItem(5, _strValues[i], _valueCount[i]));
                            break;
                        }

                    case "JUNE":
                    case "JUN":
                        {
                            sList.Add(new SortItem(6, _strValues[i], _valueCount[i]));
                            break;
                        }

                    case "JULY":
                    case "JUL":
                        {
                            sList.Add(new SortItem(7, _strValues[i], _valueCount[i]));
                            break;
                        }

                    case "AUGUST":
                    case "AUG":
                        {
                            sList.Add(new SortItem(8, _strValues[i], _valueCount[i]));
                            break;
                        }

                    case "SEPTEMBER":
                    case "SEP":
                        {
                            sList.Add(new SortItem(9, _strValues[i], _valueCount[i]));
                            break;
                        }

                    case "OCTOBER":
                    case "OCT":
                        {
                            sList.Add(new SortItem(10, _strValues[i], _valueCount[i]));
                            break;
                        }

                    case "NOVEMBER":
                    case "NOV":
                        {
                            sList.Add(new SortItem(11, _strValues[i], _valueCount[i]));
                            break;
                        }

                    case "DECEMBER":
                    case "DEC":
                        {
                            sList.Add(new SortItem(12, _strValues[i], _valueCount[i]));
                            break;
                        }
                }
            }

            sList.Sort((x, y) => x.SortValue.CompareTo(y.SortValue));

            _strValues.Clear();
            _valueCount.Clear();
            for (int i = 0; i < sList.Count; i++)
            {
                _strValues.Add(sList[i].Value);
                _valueCount.Add(sList[i].Count);
            }
        }

        /// <summary>
        /// Sort by week day name
        /// </summary>
        private void SortWeekList()
        {
            var sList = new List<SortItem>();
            for (int i = 0; i < _strValues.Count; i++)
            {
                switch (_strValues[i].ToUpper())
                {
                    case "MONDAY":
                    case "MON":
                        {
                            sList.Add(new SortItem(1, _strValues[i], _valueCount[i]));
                            break;
                        }

                    case "TUESDAY":
                    case "TUE":
                        {
                            sList.Add(new SortItem(2, _strValues[i], _valueCount[i]));
                            break;
                        }

                    case "WEDNESDAY":
                    case "WED":
                        {
                            sList.Add(new SortItem(3, _strValues[i], _valueCount[i]));
                            break;
                        }

                    case "THURSDAY":
                    case "THU":
                        {
                            sList.Add(new SortItem(4, _strValues[i], _valueCount[i]));
                            break;
                        }

                    case "FRIDAY":
                    case "FRI":
                        {
                            sList.Add(new SortItem(5, _strValues[i], _valueCount[i]));
                            break;
                        }

                    case "SATURDAY":
                    case "SAT":
                        {
                            sList.Add(new SortItem(6, _strValues[i], _valueCount[i]));
                            break;
                        }

                    case "SUNDAY":
                    case "SUN":
                        {
                            sList.Add(new SortItem(7, _strValues[i], _valueCount[i]));
                            break;
                        }
                }
            }

            sList.Sort((x, y) => x.SortValue.CompareTo(y.SortValue));

            _strValues.Clear();
            _valueCount.Clear();
            for (int i = 0; i < sList.Count; i++)
            {
                _strValues.Add(sList[i].Value);
                _valueCount.Add(sList[i].Count);
            }
        }
        #endregion

        /// <summary>
        /// Return column analysis result in a DataTable
        /// </summary>
        /// <param name="includeBlanks"></param>
        /// <returns></returns>
        public DataTable GetDataTable(bool includeBlanks)
        {
            DataTable result = new DataTable();
            switch (DataType)
            {
                case DataSubtypes.StringDate:
                case DataSubtypes.Date:
                    result.Columns.Add(new DataColumn(ColumnName, Type.GetType("System.DateTime")));
                    break;
                case DataSubtypes.StringInteger:
                case DataSubtypes.NativeInteger:
                    result.Columns.Add(new DataColumn(ColumnName, Type.GetType("System.Int64")));
                    break;
                case DataSubtypes.StringDecimal:
                case DataSubtypes.NativeDecimal:
                    result.Columns.Add(new DataColumn(ColumnName, Type.GetType("System.Double")));
                    break;
                case DataSubtypes.ByteArray:
                case DataSubtypes.Text:
                    result.Columns.Add(new DataColumn(ColumnName, Type.GetType("System.String")));
                    break;
                default:
                    break;
            }
            result.Columns.Add(new DataColumn("Count", Type.GetType("System.Int64")));

            if (includeBlanks && HasBlanks)
            {
                DataRow blankRow = result.NewRow();
                blankRow[0] = DBNull.Value;
                blankRow[1] = BlanksCount;
                result.Rows.Add(blankRow);
            }

            switch (DataType)
            {
                case DataSubtypes.StringDate:
                case DataSubtypes.Date:
                    for (int i = 0; i < _dateValues.Count; i++)
                    {
                        if (disposedValue)
                            break;

                        AddDataRow(result, _dateValues[i], _valueCount[i]);
                    }
                    break;

                case DataSubtypes.StringInteger:
                case DataSubtypes.NativeInteger:
                    for (int i = 0; i < _intValues.Count; i++)
                    {
                        if (disposedValue)
                            break;

                        AddDataRow(result, _intValues[i], _valueCount[i]);
                    }
                    break;

                case DataSubtypes.StringDecimal:
                case DataSubtypes.NativeDecimal:
                    for (int i = 0; i < _numValues.Count; i++)
                    {
                        if (disposedValue)
                            break;

                        AddDataRow(result, _numValues[i], _valueCount[i]);
                    }
                    break;

                case DataSubtypes.ByteArray:
                case DataSubtypes.Text:
                    for (int i = 0; i < _strValues.Count; i++)
                    {
                        if (disposedValue)
                            break;

                        AddDataRow(result, _strValues[i], _valueCount[i]);
                    }
                    break;

                default:
                    break;
            }

            return result;
        }

        /// <summary>
        /// Add a row into the data table
        /// </summary>
        /// <param name="dataTable">Data table where to insert the data row</param>
        /// <param name="category">category name</param>
        /// <param name="value">count of the category</param>
        private void AddDataRow(DataTable dataTable, object category, long? value)
        {
            var dr = dataTable.NewRow();
            dr[0] = category;
            dr[1] = value;
            dataTable.Rows.Add(dr);
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
                    Clear();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null

                disposedValue = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        //public static Type GetClrType(SqlDbType sqlType)
        //{
        //    switch (sqlType)
        //    {
        //        case SqlDbType.BigInt:
        //            return typeof(long?);

        //        case SqlDbType.Binary:
        //        case SqlDbType.Image:
        //        case SqlDbType.Timestamp:
        //        case SqlDbType.VarBinary:
        //            return typeof(byte[]);

        //        case SqlDbType.Bit:
        //            return typeof(bool?);

        //        case SqlDbType.Char:
        //        case SqlDbType.NChar:
        //        case SqlDbType.NText:
        //        case SqlDbType.NVarChar:
        //        case SqlDbType.Text:
        //        case SqlDbType.VarChar:
        //        case SqlDbType.Xml:
        //            return typeof(string);

        //        case SqlDbType.DateTime:
        //        case SqlDbType.SmallDateTime:
        //        case SqlDbType.Date:
        //        case SqlDbType.Time:
        //        case SqlDbType.DateTime2:
        //            return typeof(DateTime?);

        //        case SqlDbType.Decimal:
        //        case SqlDbType.Money:
        //        case SqlDbType.SmallMoney:
        //            return typeof(decimal?);

        //        case SqlDbType.Float:
        //            return typeof(double?);

        //        case SqlDbType.Int:
        //            return typeof(int?);

        //        case SqlDbType.Real:
        //            return typeof(float?);

        //        case SqlDbType.UniqueIdentifier:
        //            return typeof(Guid?);

        //        case SqlDbType.SmallInt:
        //            return typeof(short?);

        //        case SqlDbType.TinyInt:
        //            return typeof(byte?);

        //        case SqlDbType.Variant:
        //        case SqlDbType.Udt:
        //            return typeof(object);

        //        case SqlDbType.Structured:
        //            return typeof(DataTable);

        //        case SqlDbType.DateTimeOffset:
        //            return typeof(DateTimeOffset?);

        //        default:
        //            throw new ArgumentOutOfRangeException("sqlType");
        //    }
        //}

        ///// <summary>
        ///// Gets sorted unique values
        ///// </summary>
        //public List<string> SortedValues
        //{
        //    get
        //    {
        //        if (UniqueValues == null)
        //        {
        //            return new List<string>();
        //        }
        //        else
        //        {
        //            return CommonLib.SortList.SortTheList(UniqueValues);
        //        }
        //    }
        //}
    }
}