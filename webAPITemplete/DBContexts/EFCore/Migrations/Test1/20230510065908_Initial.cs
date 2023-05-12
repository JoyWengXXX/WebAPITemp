using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPITemplete.DBContexts.EFCore.Migrations.Test1
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestTable01",
                columns: table => new
                {
                    ID_1 = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_2 = table.Column<int>(type: "int", nullable: false),
                    TestCol01 = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    TestCol02 = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestTable01", x => new { x.ID_1, x.ID_2 });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestTable01");
        }
    }
}
