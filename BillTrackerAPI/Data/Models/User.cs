using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillTrackerAPI.Data.Models
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public double Income { get; set; }

        public double TotalSavings { get; set; }

    }
}
