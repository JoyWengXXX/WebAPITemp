using Microsoft.EntityFrameworkCore;
using webAPITemplete.Models.Entities;

namespace webAPITemplete.Repository.EFCore
{
    public class ProjectDBContext : DbContext
    {
        public DbSet<Student> Student { get; set; }

        public DbSet<Course> Course { get; set; }

        public DbSet<Enrollment> Enrollment { get; set; }

        public ProjectDBContext(DbContextOptions<ProjectDBContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
    }
}
