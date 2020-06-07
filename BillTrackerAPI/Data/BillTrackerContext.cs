using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BillTrackerAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BillTrackerAPI.Data
{
    public class BillTrackerContext : DbContext
    {
        public BillTrackerContext(DbContextOptions<BillTrackerContext> options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Bill> Bills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>();
            modelBuilder.Entity<Bill>();
        }

    }
}
