using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mottrist.Repository.Migrations
{
    /// <inheritdoc />
    public partial class seedcountriescities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "Geography",
                table: "Countries",
                columns: new[] { "Id", "Continent", "Name" },
                values: new object[,]
                {
                    { 1, (byte)0, "USA" },
                    { 2, (byte)0, "Canada" },
                    { 3, (byte)0, "UK" },
                    { 4, (byte)0, "Germany" },
                    { 5, (byte)0, "France" }
                });

            migrationBuilder.InsertData(
                schema: "Geography",
                table: "Cities",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "New York" },
                    { 2, 1, "Los Angeles" },
                    { 3, 1, "Chicago" },
                    { 4, 1, "Houston" },
                    { 5, 1, "Phoenix" },
                    { 6, 2, "Toronto" },
                    { 7, 2, "Montreal" },
                    { 8, 2, "Vancouver" },
                    { 9, 2, "Calgary" },
                    { 10, 2, "Ottawa" },
                    { 11, 3, "London" },
                    { 12, 3, "Manchester" },
                    { 13, 3, "Birmingham" },
                    { 14, 3, "Liverpool" },
                    { 15, 3, "Leeds" },
                    { 16, 4, "Berlin" },
                    { 17, 4, "Munich" },
                    { 18, 4, "Frankfurt" },
                    { 19, 4, "Hamburg" },
                    { 20, 4, "Cologne" },
                    { 21, 5, "Paris" },
                    { 22, 5, "Marseille" },
                    { 23, 5, "Lyon" },
                    { 24, 5, "Toulouse" },
                    { 25, 5, "Nice" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
