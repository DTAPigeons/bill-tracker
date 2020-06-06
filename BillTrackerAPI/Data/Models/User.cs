using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillTrackerAPI.Models
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public double Income { get; set; }

        public double Savings { get; set; }

        public virtual List<Bill> Bills { get; set; }
    }
}
