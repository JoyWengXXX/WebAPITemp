using Microsoft.EntityFrameworkCore;
using WebAPITemplete.Models.Entities.DefaultDB;

namespace WebAPITemplete.DBContexts.EFCore
{
    public class ProjectDBContext_Default : DbContext
    {
        public DbSet<Student> Student { get; set; }

        public DbSet<Course> Course { get; set; }

        public DbSet<Enrollment> Enrollment { get; set; }

        public ProjectDBContext_Default(DbContextOptions<ProjectDBContext_Default> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
    }
}
