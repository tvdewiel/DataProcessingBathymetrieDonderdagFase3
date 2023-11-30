using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBManager
{
    public class MongoDBDataSetRepository
    {
        private IMongoClient dbClient;
        private IMongoDatabase database;
        private string connectionString;

        public MongoDBDataSetRepository(string connectionString)
        {
            this.connectionString = connectionString;
            dbClient = new MongoClient(connectionString);
            database = dbClient.GetDatabase("BelgicaWoensdag");
        }
        public void WriteDataSets(List<MongoDBDataSet> dataSets)
        {
            var collection = database.GetCollection<MongoDBDataSet>("datasets");
            collection.InsertMany(dataSets);
        }
    }
}
