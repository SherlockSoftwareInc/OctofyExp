using System;

public class OnAnalysisTableEventArgs : EventArgs
{
    public string TableName { get; set; }
    public int NumOfRows { get; set; }
    public string SQL { get; set; }
    public string ConnectionString { get; set; }
}

