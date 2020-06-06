using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillTrackerAPI.Models
{
    public class Bill : BaseEntity
    {
        public string Tittle { get; set; }
        public double Amount { get; set; }

        public bool Recuring { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
