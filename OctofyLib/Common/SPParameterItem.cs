
namespace OctofyLib
{
    public class SPParameterItem
    {
        public SPParameterItem(string name, string dataType, string mode)
        {
            ParameterName = name;
            DataType = dataType;
            Mode = mode;
        }

        public string ParameterName { get; set; }
        public string DataType { get; set; }
        public string Mode { get; set; }
    }
}