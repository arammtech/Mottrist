using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mottrist.Repository.Migrations
{
    /// <inheritdoc />
    public partial class removeContinentColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Continent",
                schema: "Geography",
                table: "Countries");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Continent",
                schema: "Geography",
                table: "Countries",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1,
                column: "Continent",
                value: (byte)5);

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 2,
                column: "Continent",
                value: (byte)5);

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 3,
                column: "Continent",
                value: (byte)4);

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 4,
                column: "Continent",
                value: (byte)4);

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 5,
                column: "Continent",
                value: (byte)4);

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 6,
                column: "Continent",
                value: (byte)6);

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 7,
                column: "Continent",
                value: (byte)7);

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 8,
                column: "Continent",
                value: (byte)7);

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 9,
                column: "Continent",
                value: (byte)3);

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 10,
                column: "Continent",
                value: (byte)3);

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 11,
                column: "Continent",
                value: (byte)3);

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 12,
                column: "Continent",
                value: (byte)3);

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 13,
                column: "Continent",
                value: (byte)1);

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 14,
                column: "Continent",
                value: (byte)1);

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 15,
                column: "Continent",
                value: (byte)1);

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 16,
                column: "Continent",
                value: (byte)5);

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 17,
                column: "Continent",
                value: (byte)4);

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 18,
                column: "Continent",
                value: (byte)4);

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 19,
                column: "Continent",
                value: (byte)4);

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 20,
                column: "Continent",
                value: (byte)3);
        }
    }
}
