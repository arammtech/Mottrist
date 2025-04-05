using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mottrist.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddIdForDriverCountryAndDriverCity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DriverCountries",
                schema: "Drivers",
                table: "DriverCountries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DriverCities",
                schema: "Drivers",
                table: "DriverCities");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                schema: "Drivers",
                table: "DriverCountries",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                schema: "Drivers",
                table: "DriverCities",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DriverCountries",
                schema: "Drivers",
                table: "DriverCountries",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DriverCities",
                schema: "Drivers",
                table: "DriverCities",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DriverCountries_DriverId",
                schema: "Drivers",
                table: "DriverCountries",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverCities_DriverId",
                schema: "Drivers",
                table: "DriverCities",
                column: "DriverId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DriverCountries",
                schema: "Drivers",
                table: "DriverCountries");

            migrationBuilder.DropIndex(
                name: "IX_DriverCountries_DriverId",
                schema: "Drivers",
                table: "DriverCountries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DriverCities",
                schema: "Drivers",
                table: "DriverCities");

            migrationBuilder.DropIndex(
                name: "IX_DriverCities_DriverId",
                schema: "Drivers",
                table: "DriverCities");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "Drivers",
                table: "DriverCountries");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "Drivers",
                table: "DriverCities");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DriverCountries",
                schema: "Drivers",
                table: "DriverCountries",
                columns: new[] { "DriverId", "CountryId", "WorkStatus" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_DriverCities",
                schema: "Drivers",
                table: "DriverCities",
                columns: new[] { "DriverId", "CityId", "WorkStatus" });
        }
    }
}
