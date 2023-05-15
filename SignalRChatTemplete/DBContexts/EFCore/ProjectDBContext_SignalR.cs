using Microsoft.EntityFrameworkCore;
using SignalRChatTemplete.Models.Entities;
using SignalRTemplete.Models.Entities.DefaultDB;

namespace SignalRChatTemplete.DBContexts.EFCore
{
    public class ProjectDBContext_SignalR : DbContext
    {
        public DbSet<GroupInfo> GroupInfo { get; set; }

        public DbSet<UserGroupInfo> UserGroupInfo { get; set; }

        public DbSet<ChatRecord> ChatRecord { get; set; }

        public ProjectDBContext_SignalR(DbContextOptions<ProjectDBContext_SignalR> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 設定GroupInfo
            modelBuilder.Entity<GroupInfo>(entity =>
            {
                entity.HasIndex(e => e.GroupName).IsClustered(false).HasDatabaseName("IX_GroupInfo_GroupName");
                entity.HasIndex(e => e.IsEnable).IsClustered(false).HasDatabaseName("IX_GroupInfo_IsEnable");
                entity.HasIndex(e => e.CreateOn).IsClustered(false).HasDatabaseName("IX_GroupInfo_CreateOn");
                entity.HasIndex(e => e.UpdateOn).IsClustered(false).HasDatabaseName("IX_GroupInfo_UpdateOn");
                entity.Property(e => e.IsEnable).HasDefaultValue(true);
            });

            // 設定UserGroupInfo
            modelBuilder.Entity<UserGroupInfo>(entity =>
            {
                entity.HasKey(e => new { e.UserInfoSerialNum, e.GroupID });
                entity.Property(e => e.IsValid).HasDefaultValue(true);
            });

            // 設定ChatRecord
            modelBuilder.Entity<ChatRecord>(entity =>
            {
                entity.HasKey(e => new { e.SerialNum }).IsClustered(false);
                entity.HasIndex(e => e.GroupID).IsClustered(false).HasDatabaseName("IX_ChatRecord_GroupID");
                entity.HasIndex(e => e.UserID).IsClustered(false).HasDatabaseName("IX_ChatRecord_UserID");
                entity.HasIndex(e => e.CreateOn).IsClustered(true).HasDatabaseName("IX_ChatRecord_CreateOn");
            });
        }
    }
}
