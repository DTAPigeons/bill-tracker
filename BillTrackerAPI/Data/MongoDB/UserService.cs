using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using BillTrackerAPI.Data.Models;

namespace BillTrackerAPI.Data.MongoDB
{
    public class UserService : MongoService<User>
    {
        public UserService(IMongoDBSettings settings) : base(settings, settings.UserCollectionName)
        {
            
        }
    }
}
