using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillTrackerAPI.Data.MongoDB
{
    public class MongoDBSettings : IMongoDBSettings
    {
        public string UserCollectionName { get; set; }
        public string BillCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IMongoDBSettings
    {
        string UserCollectionName { get; set; }
        string BillCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
