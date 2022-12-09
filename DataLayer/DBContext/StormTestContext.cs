using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.User;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.DBContext
{
    public class StormTestContext:DbContext
    {
        public StormTestContext(DbContextOptions<StormTestContext> options):base(options)
        {
            
        }

        #region DbSets

        public DbSet<User> Users { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasQueryFilter(u => !u.IsDelete);
        }

    }
}
