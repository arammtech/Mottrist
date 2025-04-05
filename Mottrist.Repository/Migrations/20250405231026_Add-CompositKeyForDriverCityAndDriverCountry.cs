using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mottrist.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddCompositKeyForDriverCityAndDriverCountry : Migration
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DriverCountries",
                schema: "Drivers",
                table: "DriverCountries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DriverCities",
                schema: "Drivers",
                table: "DriverCities");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DriverCountries",
                schema: "Drivers",
                table: "DriverCountries",
                columns: new[] { "DriverId", "CountryId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_DriverCities",
                schema: "Drivers",
                table: "DriverCities",
                columns: new[] { "DriverId", "CityId" });
        }
    }
}
