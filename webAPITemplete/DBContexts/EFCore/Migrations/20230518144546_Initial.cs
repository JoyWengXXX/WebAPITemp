using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPITemplete.DBContexts.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Page",
                columns: table => new
                {
                    PageID = table.Column<int>(type: "int", nullable: false, comment: "功能頁編號")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SystemID = table.Column<int>(type: "int", nullable: false, comment: "所屬系統編號"),
                    PageName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "功能頁名稱"),
                    ParentPageID = table.Column<int>(type: "int", nullable: false, comment: "上層功能頁編號"),
                    CreateBy = table.Column<int>(type: "int", nullable: false, comment: "建立者"),
                    CreateOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "建立時間"),
                    UpdateBy = table.Column<int>(type: "int", nullable: true, comment: "更新者"),
                    UpdateOn = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "更新時間")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Page", x => new { x.PageID, x.SystemID })
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false, comment: "角色編號"),
                    PageID = table.Column<int>(type: "int", nullable: false, comment: "功能頁編號"),
                    CreateBy = table.Column<int>(type: "int", nullable: false, comment: "建立者"),
                    CreateOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "建立時間")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => new { x.RoleID, x.PageID })
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false, comment: "角色編號")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "角色名稱"),
                    IsAdminRole = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "是否為最高權限"),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false, defaultValue: true, comment: "是否啟用"),
                    CreateBy = table.Column<int>(type: "int", nullable: false, comment: "建立者"),
                    CreateOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "建立時間"),
                    UpdateBy = table.Column<int>(type: "int", nullable: true, comment: "更新者"),
                    UpdateOn = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "更新時間")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "System",
                columns: table => new
                {
                    SystemID = table.Column<int>(type: "int", nullable: false, comment: "系統編號")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SystemName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "系統名稱"),
                    Memo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "備註"),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false, defaultValue: true, comment: "是否啟用"),
                    CreateBy = table.Column<int>(type: "int", nullable: false, comment: "建立者"),
                    CreateOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "建立時間"),
                    UpdateBy = table.Column<int>(type: "int", nullable: true, comment: "更新者"),
                    UpdateOn = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "更新時間")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_System", x => x.SystemID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    SerialNum = table.Column<int>(type: "int", nullable: false, comment: "流水編號")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "使用者帳號"),
                    RoleID = table.Column<int>(type: "int", nullable: false, comment: "角色編號"),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "使用者姓"),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "使用者名"),
                    Gender = table.Column<int>(type: "int", maxLength: 1, nullable: true, comment: "性別"),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "生日"),
                    Phone = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true, comment: "電話"),
                    Email = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: true, comment: "電子郵件"),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false, defaultValue: true, comment: "是否啟用"),
                    CreateBy = table.Column<int>(type: "int", nullable: false, comment: "建立者"),
                    CreateOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "建立時間"),
                    UpdateBy = table.Column<int>(type: "int", nullable: true, comment: "更新者"),
                    UpdateOn = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "更新時間")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.SerialNum);
                });

            migrationBuilder.CreateTable(
                name: "UserPasswordRecord",
                columns: table => new
                {
                    SerialNum = table.Column<int>(type: "int", nullable: false, comment: "流水編號")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserSerialNum = table.Column<int>(type: "int", nullable: false, comment: "使用者編號"),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "密碼"),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false, defaultValue: true, comment: "是否啟用"),
                    CreateBy = table.Column<int>(type: "int", nullable: false, comment: "建立者"),
                    CreateOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "建立時間")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPasswordRecord", x => x.SerialNum)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.InsertData(
                table: "Page",
                columns: new[] { "PageID", "SystemID", "CreateBy", "CreateOn", "PageName", "ParentPageID", "UpdateBy", "UpdateOn" },
                values: new object[,]
                {
                    { 1, 1, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "會員管理", 0, null, null },
                    { 2, 1, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "推播管理", 0, null, null },
                    { 3, 1, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "系統管理", 0, null, null },
                    { 4, 1, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "管理員列表", 1, null, null },
                    { 5, 1, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "會員列表", 1, null, null },
                    { 6, 1, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "推播公告管理", 2, null, null },
                    { 7, 1, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "推播裝置管理", 2, null, null },
                    { 8, 1, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "角色管理", 3, null, null },
                    { 9, 1, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "權限管理", 3, null, null },
                    { 10, 1, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "系統日誌", 3, null, null },
                    { 11, 1, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "進階會員", 5, null, null },
                    { 12, 1, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "一般會員", 5, null, null },
                    { 13, 1, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "系統角色", 8, null, null },
                    { 14, 1, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "會員角色", 8, null, null },
                    { 15, 1, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "系統角色權限", 9, null, null },
                    { 16, 1, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "會員角色權限", 9, null, null }
                });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "PageID", "RoleID", "CreateBy", "CreateOn" },
                values: new object[,]
                {
                    { 1, 1, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 1, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 1, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 1, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 1, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 1, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 1, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 1, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, 1, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, 1, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, 1, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, 1, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, 1, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, 1, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, 1, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 16, 1, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 1, 2, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 2, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 2, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 2, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, 2, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, 2, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 1, 3, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 3, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, 3, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleID", "CreateBy", "CreateOn", "IsAdminRole", "IsEnable", "RoleName", "UpdateBy", "UpdateOn" },
                values: new object[] { 1, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, "Admin", null, null });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleID", "CreateBy", "CreateOn", "IsEnable", "RoleName", "UpdateBy", "UpdateOn" },
                values: new object[,]
                {
                    { 2, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "AdvenceUser", null, null },
                    { 3, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "User", null, null }
                });

            migrationBuilder.InsertData(
                table: "System",
                columns: new[] { "SystemID", "CreateBy", "CreateOn", "Memo", "SystemName", "UpdateBy", "UpdateOn" },
                values: new object[] { 1, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "管理系統", "管理系統", null, null });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "SerialNum", "Birthday", "CreateBy", "CreateOn", "Email", "FirstName", "Gender", "IsEnable", "LastName", "Phone", "RoleID", "UpdateBy", "UpdateOn", "UserID" },
                values: new object[,]
                {
                    { 1, null, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "預設管理者", null, true, "預設管理者", null, 1, null, null, "Admin" },
                    { 2, null, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "預設進階使用者", null, true, "預設進階使用者", null, 2, null, null, "AdvenceUser" },
                    { 3, null, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "預設使用者", null, true, "預設使用者", null, 3, null, null, "User" }
                });

            migrationBuilder.InsertData(
                table: "UserPasswordRecord",
                columns: new[] { "SerialNum", "CreateBy", "CreateOn", "IsEnable", "Password", "UserSerialNum" },
                values: new object[,]
                {
                    { 1, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Admin", 1 },
                    { 2, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "AdvenceUser", 2 },
                    { 3, 0, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "User", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_CreateOn",
                table: "Role",
                column: "CreateOn")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_IsEnable",
                table: "Role",
                column: "IsEnable")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_RoleName",
                table: "Role",
                column: "RoleName")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_UpdateOn",
                table: "Role",
                column: "UpdateOn")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_CreateOn",
                table: "User",
                column: "CreateOn")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_FirstName",
                table: "User",
                column: "FirstName")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_IsEnable",
                table: "User",
                column: "IsEnable")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_LastName",
                table: "User",
                column: "LastName")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_RoleID",
                table: "User",
                column: "RoleID")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_UpdateOn",
                table: "User",
                column: "UpdateOn")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_UserID",
                table: "User",
                column: "UserID")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_CreateOn",
                table: "UserPasswordRecord",
                column: "CreateOn")
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_IsEnable",
                table: "UserPasswordRecord",
                column: "IsEnable")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_Password",
                table: "UserPasswordRecord",
                column: "Password")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_UserInfoSerialNum",
                table: "UserPasswordRecord",
                column: "UserSerialNum")
                .Annotation("SqlServer:Clustered", false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Page");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "System");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "UserPasswordRecord");
        }
    }
}
