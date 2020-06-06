using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BillTrackerAPI.Models
{
    public class BillTrackerContext : DbContext
    {
        public BillTrackerContext(DbContextOptions<BillTrackerContext> options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Bill> Bills { get; set; }



    }
}
