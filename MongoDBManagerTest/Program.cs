// See https://aka.ms/new-console-template for more information
using DataSetManager;
using MongoDBManager;

Console.WriteLine("Hello, World!");
string source = @"c:\data\belgica\belgica.txt";
string campaignCode = "Maart 2021 - Zeebrugge";
string dataSerieCode = "ModelTest IP 4";

string connString = "mongodb://localhost:27017";
MongoDBDataSetRepository repo=new MongoDBDataSetRepository(connString);

var data = FileDataSetManager.ReadData(source);
var dss = FileDataSetManager.MakeDataSetsWithTestSet(data.data, new List<int> { 500, 1000, 2500, 5000, 10000 });

List<MongoDBDataSet> mongoDBDataSets = new List<MongoDBDataSet>();

DataSetMetaInfo metaInfo = new DataSetMetaInfo(500, source, campaignCode, dataSerieCode, DataSetType.TestSet);
MongoDBDataSet mongoDBDataSet=new MongoDBDataSet(dss[0],metaInfo);
mongoDBDataSets.Add(mongoDBDataSet);
for(int i = 1; i < dss.Count; i++)
{
    metaInfo = new DataSetMetaInfo(dss[i].data.Count, source, campaignCode, dataSerieCode, DataSetType.DataSet);
    mongoDBDataSet=new MongoDBDataSet(dss[i],metaInfo);
    mongoDBDataSets.Add(mongoDBDataSet);
}

repo.WriteDataSets(mongoDBDataSets);
