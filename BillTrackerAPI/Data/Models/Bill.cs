using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BillTrackerAPI.Data.Models
{
    public class Bill : BaseEntity
    {
        public string Name { get; set; }
        public double Amount { get; set; }

        public bool Recuring { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        public override bool IsValid()
        {
            if (Name == null || Name == "") { return false; }
            if (UserId == null) { return false; }
            return true;
        }
    }
}
