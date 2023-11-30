namespace MongoDBManager
{
    public class DataSetMetaInfo
    {
        public DataSetMetaInfo(int nrOfDataPoints, string source, string campaignCode, string dataSeries, DataSetType dataSetType)
        {
            NrOfDataPoints = nrOfDataPoints;
            Source = source;
            CampaignCode = campaignCode;
            DataSeries = dataSeries;
            DataSetType = dataSetType;
        }

        public int NrOfDataPoints { get; set; }
        public string Source { get; set; }
        public string CampaignCode { get; set; }
        public string DataSeries { get; set; }
        public DataSetType DataSetType { get; set; }
    }
}