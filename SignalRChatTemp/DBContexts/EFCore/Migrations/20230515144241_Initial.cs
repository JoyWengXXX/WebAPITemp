using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SignalRTemplete.DBContexts.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatRecord",
                columns: table => new
                {
                    SerialNum = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    CreateOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatRecord", x => x.SerialNum)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "GroupInfo",
                columns: table => new
                {
                    GroupID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    CreateOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<int>(type: "int", nullable: true),
                    UpdateOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupInfo", x => x.GroupID);
                });

            migrationBuilder.CreateTable(
                name: "UserGroupInfo",
                columns: table => new
                {
                    GroupID = table.Column<int>(type: "int", nullable: false),
                    UserInfoSerialNum = table.Column<int>(type: "int", nullable: false),
                    IsValid = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    CreateOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<int>(type: "int", nullable: true),
                    UpdateOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroupInfo", x => new { x.UserInfoSerialNum, x.GroupID });
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatRecord_CreateOn",
                table: "ChatRecord",
                column: "CreateOn")
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_ChatRecord_GroupID",
                table: "ChatRecord",
                column: "GroupID")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_ChatRecord_UserID",
                table: "ChatRecord",
                column: "UserID")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_GroupInfo_CreateOn",
                table: "GroupInfo",
                column: "CreateOn")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_GroupInfo_GroupName",
                table: "GroupInfo",
                column: "GroupName")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_GroupInfo_IsEnable",
                table: "GroupInfo",
                column: "IsEnable")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_GroupInfo_UpdateOn",
                table: "GroupInfo",
                column: "UpdateOn")
                .Annotation("SqlServer:Clustered", false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatRecord");

            migrationBuilder.DropTable(
                name: "GroupInfo");

            migrationBuilder.DropTable(
                name: "UserGroupInfo");
        }
    }
}
