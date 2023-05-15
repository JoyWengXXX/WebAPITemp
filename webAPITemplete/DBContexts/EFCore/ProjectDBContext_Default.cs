using Microsoft.EntityFrameworkCore;
using WebAPITemplete.Models.Entities.DefaultDB;

namespace WebAPITemplete.DBContexts.EFCore
{
    public class ProjectDBContext_Default : DbContext
    {
        public DbSet<Student> Student { get; set; }

        public DbSet<Course> Course { get; set; }

        public DbSet<Enrollment> Enrollment { get; set; }

        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<UserPasswordRecord> UserPasswordRecord { get; set; }
        public DbSet<RoleInfo> RoleInfo { get; set; }

        public ProjectDBContext_Default(DbContextOptions<ProjectDBContext_Default> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.HasKey(e => new { e.SerialNum, e.UserID });
                entity.HasIndex(e => e.UserID).IsClustered(false).HasDatabaseName("IX_UserInfo_UserID");
                entity.HasIndex(e => e.RoleID).IsClustered(false).HasDatabaseName("IX_UserInfo_RoleID");
                entity.HasIndex(e => e.FirstName).IsClustered(false).HasDatabaseName("IX_UserInfo_FirstName");
                entity.HasIndex(e => e.LastName).IsClustered(false).HasDatabaseName("IX_UserInfo_LastName");
                entity.HasIndex(e => e.IsEnable).IsClustered(false).HasDatabaseName("IX_UserInfo_IsEnable");
                entity.HasIndex(e => e.CreateOn).IsClustered(false).HasDatabaseName("IX_UserInfo_CreateOn");
                entity.HasIndex(e => e.UpdateOn).IsClustered(false).HasDatabaseName("IX_UserInfo_UpdateOn");
                entity.Property(e => e.IsEnable).HasDefaultValue(true);

                //預設資料
                entity.HasData(new UserInfo { SerialNum = 1, UserID = "Admin", RoleID = 1, FirstName = "預設", LastName = "預設", IsEnable = true, CreateBy = 0, CreateOn = new DateTime(2023,5,14) });
            });

            modelBuilder.Entity<UserPasswordRecord>(entity => 
            {
                entity.HasKey(e => new { e.SerialNum }).IsClustered(false);
                entity.HasIndex(e => e.UserInfoSerialNum).IsClustered(false).HasDatabaseName("IX_UserInfo_UserInfoSerialNum");
                entity.HasIndex(e => e.Password).IsClustered(false).HasDatabaseName("IX_UserInfo_Password");
                entity.HasIndex(e => e.IsEnable).IsClustered(false).HasDatabaseName("IX_UserInfo_IsEnable");
                entity.HasIndex(e => e.CreateOn).IsClustered(true).HasDatabaseName("IX_UserInfo_CreateOn");
                entity.Property(e => e.IsEnable).HasDefaultValue(true);

                //預設資料
                entity.HasData(new UserPasswordRecord { SerialNum = 1, UserInfoSerialNum = 1, Password = "Admin", IsEnable = true, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });
            });

            modelBuilder.Entity<RoleInfo>(entity =>
            {
                entity.HasIndex(e => e.RoleName).IsClustered(false).HasDatabaseName("IX_UserInfo_RoleName");
                entity.HasIndex(e => e.IsEnable).IsClustered(false).HasDatabaseName("IX_UserInfo_IsEnable");
                entity.HasIndex(e => e.CreateOn).IsClustered(false).HasDatabaseName("IX_UserInfo_CreateOn");
                entity.HasIndex(e => e.UpdateOn).IsClustered(false).HasDatabaseName("IX_UserInfo_UpdateOn");
                entity.Property(e => e.IsEnable).HasDefaultValue(true);

                //預設資料
                entity.HasData(new RoleInfo { RoleID = 1, RoleName = "Admin", IsEnable = true, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });
                entity.HasData(new RoleInfo { RoleID = 2, RoleName = "User", IsEnable = true, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });
            });
        }
    }
}
