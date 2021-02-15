using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;

namespace OctofyLib
{
    /// <summary>
    /// 
    /// </summary>
    public class DataRowFilter
    {
        private string _filterString = string.Empty;
        private readonly List<ColumnFilter> _conditions = new List<ColumnFilter>();

        private class ColumnFilter
        {
            private readonly List<string> _values = new List<string>();
            private readonly bool _hasBlanks;

            public string ColumnName { get; set; }
            public int ColumnIndex { get; set; }
            public List<string> FilterValues { get; set; }
            public bool IsDateColumn { get; set; } = false;
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }

            public ColumnFilter(string columnName, List<string> filterValues)
            {
                ColumnName = columnName;
                ColumnIndex = -1;
                FilterValues = filterValues;
                _values.Clear();
                _hasBlanks = false;
                foreach (var value in filterValues)
                {
                    if (value.Length == 0 | value == Properties.Resources.B003)
                    {
                        _hasBlanks = true;
                    }
                    else
                    {
                        _values.Add(value);
                    }
                }
            }

            public ColumnFilter(string columnName, DateTime startDate, DateTime endDate)
            {
                ColumnName = columnName;
                StartDate = startDate;
                EndDate = endDate;
                IsDateColumn = true;
            }

            public bool IsMeetCondition(DataRow dr)
            {
                bool result = false;

                if (ColumnIndex >= 0)
                {
                    if (IsDateColumn)
                    {
                        if (dr[ColumnIndex] != DBNull.Value)
                        {
                            DateTime dateValue = Convert.ToDateTime(dr[ColumnIndex]);
                            if (dateValue >= StartDate && dateValue <= EndDate)
                                result = true;
                        }
                    }
                    else
                    {
                        if (dr[ColumnIndex] == DBNull.Value)
                        {
                            if (_hasBlanks == true)
                            {
                                result = true;
                            }
                        }
                        else
                        {
                            string columnValue = dr[ColumnIndex].ToString().TrimEnd();
                            if (columnValue.Length == 0)
                            {
                                if (_hasBlanks == true)
                                {
                                    result = true;
                                }
                            }
                            else
                            {
                                if (_values.BinarySearch(columnValue) >= 0)
                                {
                                    result = true;
                                }
                            }
                        }
                    }
                }
                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterString"></param>
        /// <param name="dt"></param>
        public void SetFilter(string filterString, DataTable dt)
        {
            if ((filterString ?? "") != (_filterString ?? ""))
            {
                Clear();
                _filterString = filterString;
                if (filterString.Length > 0)
                {
                    var oDoc = new XmlDocument();
                    oDoc.LoadXml(filterString);

                    if (oDoc != null)
                    {
                        if (oDoc.DocumentElement.HasChildNodes)
                        {
                            foreach (XmlNode node in oDoc.DocumentElement.ChildNodes)
                            {
                                if (node is XmlElement)
                                {
                                    string columnName = node.Attributes["Name"].Value;
                                    if (columnName.Length > 0)
                                    {
                                        AddCondition(columnName, node);
                                    }
                                }
                            }
                        }
                    }

                    //var items = filterString.Split('|');
                    //foreach (var item in items)
                    //    AddCondition(item);

                    // set column index
                    for (int i = 0; i < _conditions.Count; i++)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            if ((_conditions[i].ColumnName ?? "") == (dt.Columns[j].ColumnName ?? ""))
                            {
                                _conditions[i].ColumnIndex = j;
                                break;
                            }
                        }
                    }
                }
            }
        }

        //public void SetFilter(string filterString)
        //{
        //    if ((filterString ?? "") != (_filterString ?? ""))
        //    {
        //        Clear();
        //        _filterString = filterString;
        //        if (filterString.Length > 0)
        //        {
        //            var items = filterString.Split('|');
        //            foreach (var item in items)
        //                AddCondition(item);
        //        }
        //    }
        //}

        //private void AddCondition(string value)
        //{
        //    int index = value.IndexOf("::");
        //    if (index > 0)
        //    {
        //        string columnName = value.Substring(0, index);
        //        string filterValues = value.Substring(index + 2, value.Length - index - 2);
        //        if (columnName.Length > 0 & filterValues.Length > 0)
        //        {
        //            _conditions.Add(new ColumnFilter(columnName, filterValues));
        //        }
        //    }
        //}
        private void AddCondition(string columnName, XmlNode xml)
        {
            List<string> filterValues = new List<string>();
            DateTime startDate = DateTime.MinValue;
            DateTime endDate = DateTime.MinValue;
            bool isDate = false;
            foreach (XmlNode xnode in xml.ChildNodes)
            {
                if (xnode.Name == "item")
                    filterValues.Add(xnode.InnerText);
                else if (xnode.Name == "StartDate")
                {
                    if (DateTime.TryParse(xnode.InnerText, out startDate))
                        isDate = true;
                }
                else if (xnode.Name == "EndDate")
                {
                    if (DateTime.TryParse(xnode.InnerText, out endDate))
                        isDate = true;
                }
            }
            if (isDate)
            {
                _conditions.Add(new ColumnFilter(columnName, startDate, endDate));
            }
            else
                _conditions.Add(new ColumnFilter(columnName, filterValues));
        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            _filterString = string.Empty;
            _conditions.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public bool CheckDataRow(DataRow dr)
        {
            if (_conditions.Count > 0)
            {
                for (int i = 0; i < _conditions.Count; i++)
                {
                    if (_conditions[i].IsMeetCondition(dr) == false)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public List<string> GetColumnFilterValue(string columnName)
        {
            for (int i = 0; i < _conditions.Count; i++)
            {
                if (string.Compare(_conditions[i].ColumnName, columnName, true) == 0)
                {
                    return _conditions[i].FilterValues;
                }
            }

            return new List<string>();
        }
    }
}