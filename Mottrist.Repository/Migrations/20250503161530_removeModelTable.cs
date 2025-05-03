using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mottrist.Repository.Migrations
{
    /// <inheritdoc />
    public partial class removeModelTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Models_ModelId",
                schema: "Vehicles",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "Models",
                schema: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Cars_ModelId",
                schema: "Vehicles",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "ModelId",
                schema: "Vehicles",
                table: "Cars");

            migrationBuilder.AddColumn<string>(
                name: "Model",
                schema: "Vehicles",
                table: "Cars",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1,
                column: "Model",
                value: "DTG");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2,
                column: "Model",
                value: "DTG");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3,
                column: "Model",
                value: "DTG");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Cars",
                keyColumn: "Id",
                keyValue: 4,
                column: "Model",
                value: "DTG");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Cars",
                keyColumn: "Id",
                keyValue: 5,
                column: "Model",
                value: "DTG");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Model",
                schema: "Vehicles",
                table: "Cars");

            migrationBuilder.AddColumn<int>(
                name: "ModelId",
                schema: "Vehicles",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Models",
                schema: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Models", x => x.Id);
                });

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1,
                column: "ModelId",
                value: 1);

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2,
                column: "ModelId",
                value: 2);

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3,
                column: "ModelId",
                value: 1);

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Cars",
                keyColumn: "Id",
                keyValue: 4,
                column: "ModelId",
                value: 2);

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Cars",
                keyColumn: "Id",
                keyValue: 5,
                column: "ModelId",
                value: 3);

            migrationBuilder.InsertData(
                schema: "Vehicles",
                table: "Models",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Corolla" },
                    { 2, "Mustang" },
                    { 3, "Civic" },
                    { 4, "Model S" },
                    { 5, "X5" },
                    { 6, "F-150" },
                    { 7, "Accord" },
                    { 8, "A4" },
                    { 9, "Camry" },
                    { 10, "Q5" },
                    { 11, "RX" },
                    { 12, "Tucson" },
                    { 13, "Explorer" },
                    { 14, "Kona" },
                    { 15, "911" },
                    { 16, "Grand Cherokee" },
                    { 17, "Range Rover" },
                    { 18, "Charger" },
                    { 19, "Cherokee" },
                    { 20, "X6" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ModelId",
                schema: "Vehicles",
                table: "Cars",
                column: "ModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Models_ModelId",
                schema: "Vehicles",
                table: "Cars",
                column: "ModelId",
                principalSchema: "Vehicles",
                principalTable: "Models",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
