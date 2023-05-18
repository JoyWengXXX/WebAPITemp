using Microsoft.EntityFrameworkCore;
using WebAPITemplete.Models.Entities.DefaultDB;
using static Dapper.SqlMapper;

namespace WebAPITemplete.DBContexts.EFCore
{
    public class ProjectDBContext_Default : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<UserPasswordRecord> UserPasswordRecord { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Permission> Permission { get; set; }
        public DbSet<Page> Page { get; set; }
        public DbSet<Models.Entities.DefaultDB.System> System { get; set; }

        public ProjectDBContext_Default(DbContextOptions<ProjectDBContext_Default> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.UserID).IsClustered(false).HasDatabaseName("IX_UserInfo_UserID");
                entity.HasIndex(e => e.RoleID).IsClustered(false).HasDatabaseName("IX_UserInfo_RoleID");
                entity.HasIndex(e => e.FirstName).IsClustered(false).HasDatabaseName("IX_UserInfo_FirstName");
                entity.HasIndex(e => e.LastName).IsClustered(false).HasDatabaseName("IX_UserInfo_LastName");
                entity.HasIndex(e => e.IsEnable).IsClustered(false).HasDatabaseName("IX_UserInfo_IsEnable");
                entity.HasIndex(e => e.CreateOn).IsClustered(false).HasDatabaseName("IX_UserInfo_CreateOn");
                entity.HasIndex(e => e.UpdateOn).IsClustered(false).HasDatabaseName("IX_UserInfo_UpdateOn");
                entity.Property(e => e.IsEnable).HasDefaultValue(true);

                //新增欄位備註
                entity.Property(e => e.SerialNum).HasComment("流水編號");
                entity.Property(e => e.UserID).HasComment("使用者帳號");
                entity.Property(e => e.RoleID).HasComment("角色編號");
                entity.Property(e => e.FirstName).HasComment("使用者姓");
                entity.Property(e => e.LastName).HasComment("使用者名");
                entity.Property(e => e.Gender).HasComment("性別");
                entity.Property(e => e.Birthday).HasComment("生日");
                entity.Property(e => e.Phone).HasComment("電話");
                entity.Property(e => e.Email).HasComment("電子郵件");
                entity.Property(e => e.IsEnable).HasComment("是否啟用");
                entity.Property(e => e.CreateBy).HasComment("建立者");
                entity.Property(e => e.CreateOn).HasComment("建立時間");
                entity.Property(e => e.UpdateBy).HasComment("更新者");
                entity.Property(e => e.UpdateOn).HasComment("更新時間");

                //預設資料
                entity.HasData(new User { SerialNum = 1, UserID = "Admin", RoleID = 1, FirstName = "預設管理者", LastName = "預設管理者", IsEnable = true, CreateBy = 0, CreateOn = new DateTime(2023,5,14) });
                entity.HasData(new User { SerialNum = 2, UserID = "AdvenceUser", RoleID = 2, FirstName = "預設進階使用者", LastName = "預設進階使用者", IsEnable = true, CreateBy = 0, CreateOn = new DateTime(2023,5,14) });
                entity.HasData(new User { SerialNum = 3, UserID = "User", RoleID = 3, FirstName = "預設使用者", LastName = "預設使用者", IsEnable = true, CreateBy = 0, CreateOn = new DateTime(2023,5,14) });
            });

            modelBuilder.Entity<UserPasswordRecord>(entity => 
            {
                entity.HasKey(e => new { e.SerialNum }).IsClustered(false);
                entity.HasIndex(e => e.UserSerialNum).IsClustered(false).HasDatabaseName("IX_UserInfo_UserInfoSerialNum");
                entity.HasIndex(e => e.Password).IsClustered(false).HasDatabaseName("IX_UserInfo_Password");
                entity.HasIndex(e => e.IsEnable).IsClustered(false).HasDatabaseName("IX_UserInfo_IsEnable");
                entity.HasIndex(e => e.CreateOn).IsClustered(true).HasDatabaseName("IX_UserInfo_CreateOn");
                entity.Property(e => e.IsEnable).HasDefaultValue(true);

                //新增欄位備註
                entity.Property(e => e.SerialNum).HasComment("流水編號");
                entity.Property(e => e.UserSerialNum).HasComment("使用者編號");
                entity.Property(e => e.Password).HasComment("密碼");
                entity.Property(e => e.IsEnable).HasComment("是否啟用");
                entity.Property(e => e.CreateBy).HasComment("建立者");
                entity.Property(e => e.CreateOn).HasComment("建立時間");

                //預設資料
                entity.HasData(new UserPasswordRecord { SerialNum = 1, UserSerialNum = 1, Password = "Admin", IsEnable = true, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });
                entity.HasData(new UserPasswordRecord { SerialNum = 2, UserSerialNum = 2, Password = "AdvenceUser", IsEnable = true, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });
                entity.HasData(new UserPasswordRecord { SerialNum = 3, UserSerialNum = 3, Password = "User", IsEnable = true, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasIndex(e => e.RoleName).IsClustered(false).HasDatabaseName("IX_UserInfo_RoleName");
                entity.HasIndex(e => e.IsEnable).IsClustered(false).HasDatabaseName("IX_UserInfo_IsEnable");
                entity.HasIndex(e => e.CreateOn).IsClustered(false).HasDatabaseName("IX_UserInfo_CreateOn");
                entity.HasIndex(e => e.UpdateOn).IsClustered(false).HasDatabaseName("IX_UserInfo_UpdateOn");
                entity.Property(e => e.IsEnable).HasDefaultValue(true);

                //新增欄位備註
                entity.Property(e => e.RoleID).HasComment("角色編號");
                entity.Property(e => e.RoleName).HasComment("角色名稱");
                entity.Property(e => e.IsAdminRole).HasComment("是否為最高權限");
                entity.Property(e => e.IsEnable).HasComment("是否啟用");
                entity.Property(e => e.CreateBy).HasComment("建立者");
                entity.Property(e => e.CreateOn).HasComment("建立時間");
                entity.Property(e => e.UpdateBy).HasComment("更新者");
                entity.Property(e => e.UpdateOn).HasComment("更新時間");

                //預設資料
                entity.Property(e => e.IsEnable).HasDefaultValue(true);
                entity.Property(e => e.IsAdminRole).HasDefaultValue(false);
                entity.HasData(new Role { RoleID = 1, RoleName = "Admin", IsAdminRole = true, IsEnable = true, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });
                entity.HasData(new Role { RoleID = 2, RoleName = "AdvenceUser", IsAdminRole = false, IsEnable = true, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });
                entity.HasData(new Role { RoleID = 3, RoleName = "User", IsAdminRole = false, IsEnable = true, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.HasKey(e => new { e.RoleID, e.PageID }).IsClustered(false);

                //新增欄位備註
                entity.Property(e => e.RoleID).HasComment("角色編號");
                entity.Property(e => e.PageID).HasComment("功能頁編號");
                entity.Property(e => e.CreateBy).HasComment("建立者");
                entity.Property(e => e.CreateOn).HasComment("建立時間");

                //預設資料
                entity.HasData(new Permission { RoleID = 1, PageID = 1, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });
                entity.HasData(new Permission { RoleID = 1, PageID = 2, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });
                entity.HasData(new Permission { RoleID = 1, PageID = 3, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });
                entity.HasData(new Permission { RoleID = 1, PageID = 4, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });
                entity.HasData(new Permission { RoleID = 1, PageID = 5, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });
                entity.HasData(new Permission { RoleID = 1, PageID = 6, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });
                entity.HasData(new Permission { RoleID = 1, PageID = 7, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });
                entity.HasData(new Permission { RoleID = 1, PageID = 8, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });
                entity.HasData(new Permission { RoleID = 1, PageID = 9, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });
                entity.HasData(new Permission { RoleID = 1, PageID = 10, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });
                entity.HasData(new Permission { RoleID = 1, PageID = 11, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });
                entity.HasData(new Permission { RoleID = 1, PageID = 12, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });
                entity.HasData(new Permission { RoleID = 1, PageID = 13, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });
                entity.HasData(new Permission { RoleID = 1, PageID = 14, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });
                entity.HasData(new Permission { RoleID = 1, PageID = 15, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });
                entity.HasData(new Permission { RoleID = 1, PageID = 16, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });
                
                entity.HasData(new Permission { RoleID = 2, PageID = 1, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });
                entity.HasData(new Permission { RoleID = 2, PageID = 2, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });
                entity.HasData(new Permission { RoleID = 2, PageID = 5, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });
                entity.HasData(new Permission { RoleID = 2, PageID = 6, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });
                entity.HasData(new Permission { RoleID = 2, PageID = 7, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });
                entity.HasData(new Permission { RoleID = 2, PageID = 11, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });
                entity.HasData(new Permission { RoleID = 2, PageID = 12, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });

                entity.HasData(new Permission { RoleID = 3, PageID = 1, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });
                entity.HasData(new Permission { RoleID = 3, PageID = 5, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });
                entity.HasData(new Permission { RoleID = 3, PageID = 12, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });
            });

            modelBuilder.Entity<Page>(entity =>
            {
                entity.HasKey(e => new { e.PageID, e.SystemID }).IsClustered(false);

                //新增欄位備註
                entity.Property(e => e.PageID).HasComment("功能頁編號");
                entity.Property(e => e.SystemID).HasComment("所屬系統編號");
                entity.Property(e => e.PageName).HasComment("功能頁名稱");
                entity.Property(e => e.ParentPageID).HasComment("上層功能頁編號");
                entity.Property(e => e.CreateBy).HasComment("建立者");
                entity.Property(e => e.CreateOn).HasComment("建立時間");
                entity.Property(e => e.UpdateBy).HasComment("更新者");
                entity.Property(e => e.UpdateOn).HasComment("更新時間");

                //預設資料
                entity.HasData(new Page { PageID = 1, SystemID = 1, PageName = "會員管理", ParentPageID = 0, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });
                entity.HasData(new Page { PageID = 2, SystemID = 1, PageName = "推播管理", ParentPageID = 0, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });
                entity.HasData(new Page { PageID = 3, SystemID = 1, PageName = "系統管理", ParentPageID = 0, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });

                entity.HasData(new Page { PageID = 4, SystemID = 1, PageName = "管理員列表", ParentPageID = 1, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });
                entity.HasData(new Page { PageID = 5, SystemID = 1, PageName = "會員列表", ParentPageID = 1, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });

                entity.HasData(new Page { PageID = 6, SystemID = 1, PageName = "推播公告管理", ParentPageID = 2, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });
                entity.HasData(new Page { PageID = 7, SystemID = 1, PageName = "推播裝置管理", ParentPageID = 2, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });

                entity.HasData(new Page { PageID = 8, SystemID = 1, PageName = "角色管理", ParentPageID = 3, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });
                entity.HasData(new Page { PageID = 9, SystemID = 1, PageName = "權限管理", ParentPageID = 3, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });
                entity.HasData(new Page { PageID = 10, SystemID = 1, PageName = "系統日誌", ParentPageID = 3, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });

                entity.HasData(new Page { PageID = 11, SystemID = 1, PageName = "進階會員", ParentPageID = 5, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });
                entity.HasData(new Page { PageID = 12, SystemID = 1, PageName = "一般會員", ParentPageID = 5, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });

                entity.HasData(new Page { PageID = 13, SystemID = 1, PageName = "系統角色", ParentPageID = 8, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });
                entity.HasData(new Page { PageID = 14, SystemID = 1, PageName = "會員角色", ParentPageID = 8, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });

                entity.HasData(new Page { PageID = 15, SystemID = 1, PageName = "系統角色權限", ParentPageID = 9, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });
                entity.HasData(new Page { PageID = 16, SystemID = 1, PageName = "會員角色權限", ParentPageID = 9, CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });
            });

            modelBuilder.Entity<Models.Entities.DefaultDB.System>(entity =>
            {
                //新增欄位備註
                entity.Property(e => e.SystemID).HasComment("系統編號");
                entity.Property(e => e.SystemName).HasComment("系統名稱");
                entity.Property(e => e.Memo).HasComment("備註");
                entity.Property(e => e.IsEnable).HasComment("是否啟用");
                entity.Property(e => e.CreateBy).HasComment("建立者");
                entity.Property(e => e.CreateOn).HasComment("建立時間");
                entity.Property(e => e.UpdateBy).HasComment("更新者");
                entity.Property(e => e.UpdateOn).HasComment("更新時間");

                //預設資料
                entity.Property(e => e.IsEnable).HasDefaultValue(true);
                entity.HasData(new Models.Entities.DefaultDB.System { SystemID = 1, SystemName = "管理系統", Memo = "管理系統", CreateBy = 0, CreateOn = new DateTime(2023, 5, 14) });
            });
        }
    }
}
