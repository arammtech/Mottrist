using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mottrist.Repository.Migrations
{
    /// <inheritdoc />
    public partial class addColumnIdDriverLanguageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DriverLanguages",
                schema: "Localization",
                table: "DriverLanguages");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                schema: "Localization",
                table: "DriverLanguages",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DriverLanguages",
                schema: "Localization",
                table: "DriverLanguages",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DriverLanguages_DriverId",
                schema: "Localization",
                table: "DriverLanguages",
                column: "DriverId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DriverLanguages",
                schema: "Localization",
                table: "DriverLanguages");

            migrationBuilder.DropIndex(
                name: "IX_DriverLanguages_DriverId",
                schema: "Localization",
                table: "DriverLanguages");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "Localization",
                table: "DriverLanguages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DriverLanguages",
                schema: "Localization",
                table: "DriverLanguages",
                columns: new[] { "DriverId", "LanguageId" });
        }
    }
}
