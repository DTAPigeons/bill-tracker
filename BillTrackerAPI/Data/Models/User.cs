using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillTrackerAPI.Data.Models
{
    public class User : BaseEntity
    {

        public string AccountName { get; set; }
        public string Name { get; set; }
        public double Income { get; set; }

        public double TotalSavings { get; set; }

        public override bool IsValid()
        {
            if(AccountName==null || AccountName == "") { return false; }
            if (Name == null || Name == "") { return false; }
            return true;
        }

    }
}
