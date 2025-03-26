using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mottrist.Repository.Migrations
{
    /// <inheritdoc />
    public partial class seeduserroles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, null, "Admin", "ADMIN" },
                    { 2, null, "Driver", "DRIVER" },
                    { 3, null, "Traveler", "TRAVELER" }
                });

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1,
                column: "Continent",
                value: (byte)4);

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 2,
                column: "Continent",
                value: (byte)4);

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 3,
                column: "Continent",
                value: (byte)3);

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 4,
                column: "Continent",
                value: (byte)3);

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 5,
                column: "Continent",
                value: (byte)3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1,
                column: "Continent",
                value: (byte)0);

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 2,
                column: "Continent",
                value: (byte)0);

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 3,
                column: "Continent",
                value: (byte)0);

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 4,
                column: "Continent",
                value: (byte)0);

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 5,
                column: "Continent",
                value: (byte)0);
        }
    }
}
