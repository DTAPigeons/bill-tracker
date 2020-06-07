using BillTrackerAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillTrackerAPI.Data.MongoDB
{
    public class BillService : MongoService<Bill>
    {
        public BillService(IMongoDBSettings settings) : base(settings, settings.BillCollectionName)
        {
        }

        public async Task<List<Bill>> GetBillForUser(string userId)
        {
            return await GetAll(bill => bill.UserId == userId);
        }
    }
}
