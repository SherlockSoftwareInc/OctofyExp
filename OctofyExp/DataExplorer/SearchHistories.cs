using System.Windows.Forms;

namespace OctofyExp
{
    class SearchHistories
    {

        private AutoCompleteStringCollection _dataSource = new AutoCompleteStringCollection();

        public AutoCompleteStringCollection DataSource
        {
            get { return _dataSource; }
            set { _dataSource = value; }
        }

        public string Value
        {
            get { return ToString(); }
            set { ParseValue(value); }
        }

        private void ParseValue(string value)
        {
            if (_dataSource != null)
            {
                if (_dataSource.Count > 0)
                    _dataSource.Clear();

                string[] valueArray = value.Split('|');
                foreach (var item in valueArray)
                {
                    if (item.Trim().Length > 1)
                        _dataSource.Add(item);
                }
            }
        }

        public override string ToString()
        {
            string strResult = "";
            if (_dataSource != null)
            {
                short cnt = 0;
                for (int i = 0; i < _dataSource.Count; i++)
                {
                    string item = _dataSource[i];
                    if (strResult.Length == 0)
                        strResult = item;
                    else
                        strResult += "|" + item;
                    cnt++;
                    if (cnt > 500)
                        break;
                }
            }
            return strResult;
        }

        public void AddSearchItem(string searchString)
        {
            if (searchString.Trim().Length > 1)
                if (!IsItemExists(searchString))
                    _dataSource.Add(searchString.Trim());

        }

        private bool IsItemExists(string searchString)
        {
            foreach (var item in _dataSource)
            {
                if (string.Compare(searchString, item.ToString(), true) == 0)
                    return true;
            }
            return false;
        }
    }
}
