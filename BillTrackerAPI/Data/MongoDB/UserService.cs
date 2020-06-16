using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using BillTrackerAPI.Data.Models;
using System.Security.Cryptography.X509Certificates;

namespace BillTrackerAPI.Data.MongoDB
{
    public class UserService : MongoService<User>
    {
        public UserService(IMongoDBSettings settings) : base(settings, settings.UserCollectionName)
        {

        }

        public async Task<User> GetUserByAccountName(string accountName)
        {
            User user = (await GetAll(us => us.AccountName == accountName))[0];
            return user;
        }
    }
}
