using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mottrist.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnsCarWifiAndAirCondetions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasAirCondiations",
                schema: "Vehicles",
                table: "Cars",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasWiFi",
                schema: "Vehicles",
                table: "Cars",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "HasAirCondiations", "HasWiFi" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "HasAirCondiations", "HasWiFi" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "HasAirCondiations", "HasWiFi" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Cars",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "HasAirCondiations", "HasWiFi" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Cars",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "HasAirCondiations", "HasWiFi" },
                values: new object[] { false, false });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasAirCondiations",
                schema: "Vehicles",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "HasWiFi",
                schema: "Vehicles",
                table: "Cars");
        }
    }
}
