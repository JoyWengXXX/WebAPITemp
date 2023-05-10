using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using webAPITemplete.Models.Entities.TestDB1;

namespace webAPITemplete.Repository.EFCore
{
    public class ProjectDBContext_Test1 : DbContext
    {
        public DbSet<SalesOrderDetail> SalesOrderDetail { get; set; }

        public ProjectDBContext_Test1(DbContextOptions<ProjectDBContext_Test1> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SalesOrderDetail>()
                        .HasKey(sd => new { sd.SalesOrderID, sd.SalesOrderDetailID });
        }
    }
}