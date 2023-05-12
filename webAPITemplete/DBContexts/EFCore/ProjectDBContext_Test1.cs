using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using webAPITemplete.Models.Entities.TestDB1;

namespace webAPITemplete.DBContexts.EFCore
{
    public class ProjectDBContext_Test1 : DbContext
    {
        public DbSet<TestTable01> TestTable01 { get; set; }

        public ProjectDBContext_Test1(DbContextOptions<ProjectDBContext_Test1> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestTable01>()
                        .HasKey(sd => new { sd.ID_1, sd.ID_2 });
        }
    }
}