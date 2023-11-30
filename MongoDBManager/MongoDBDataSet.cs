using DataSetManager;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBManager
{
    public class MongoDBDataSet
    {
        public MongoDBDataSet(DataSet dataSet, DataSetMetaInfo metaInfo)
        {
            this.dataSet = dataSet;
            this.metaInfo = metaInfo;
        }

        public MongoDBDataSet(ObjectId id, DataSet dataSet, DataSetMetaInfo metaInfo)
        {
            this.id = id;
            this.dataSet = dataSet;
            this.metaInfo = metaInfo;
        }

        public ObjectId id { get; set; }
        public DataSet dataSet { get; set; }
        public DataSetMetaInfo metaInfo { get; set; }
    }
}
