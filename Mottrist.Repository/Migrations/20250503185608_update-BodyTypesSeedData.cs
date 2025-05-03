using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mottrist.Repository.Migrations
{
    /// <inheritdoc />
    public partial class updateBodyTypesSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Vehicle");

            migrationBuilder.RenameTable(
                name: "BodyTypes",
                schema: "Vehicles",
                newName: "BodyTypes",
                newSchema: "Vehicle");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                schema: "Vehicle",
                table: "BodyTypes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 9,
                column: "Type",
                value: "Hybrid Car");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 36,
                column: "Type",
                value: "Compact MPV");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 37,
                column: "Type",
                value: "Large MPV");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 38,
                column: "Type",
                value: "Panel Van");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 39,
                column: "Type",
                value: "Pickup SUV");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 40,
                column: "Type",
                value: "Coupe SUV");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 41,
                column: "Type",
                value: "Off-Road SUV");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 42,
                column: "Type",
                value: "Luxury SUV");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 43,
                column: "Type",
                value: "Performance SUV");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 51,
                column: "Type",
                value: "Full-Size Crossover SUV");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 52,
                column: "Type",
                value: "Convertible SUV");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 53,
                column: "Type",
                value: "T-Top");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 54,
                column: "Type",
                value: "Phaeton");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 55,
                column: "Type",
                value: "Barchetta");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 56,
                column: "Type",
                value: "Spider");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 57,
                column: "Type",
                value: "Cabriolet");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 58,
                column: "Type",
                value: "Drophead Coupé");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 59,
                column: "Type",
                value: "Roadster Utility");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 60,
                column: "Type",
                value: "Club Coupe");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 61,
                column: "Type",
                value: "Opera Coupe");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 62,
                column: "Type",
                value: "Business Coupe");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 63,
                column: "Type",
                value: "Personal Luxury Coupe");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 64,
                column: "Type",
                value: "Four-Door Coupe");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 101,
                column: "Type",
                value: "Compact Hatchback");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 102,
                column: "Type",
                value: "Mid-Size Hatchback");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 103,
                column: "Type",
                value: "Luxury Hatchback");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 104,
                column: "Type",
                value: "Performance Hatchback");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 105,
                column: "Type",
                value: "Electric Hatchback");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 106,
                column: "Type",
                value: "Hybrid Hatchback");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 107,
                column: "Type",
                value: "Subcompact Coupe");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 108,
                column: "Type",
                value: "Compact Coupe");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 109,
                column: "Type",
                value: "Mid-Size Coupe");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 110,
                column: "Type",
                value: "Luxury Coupe");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 111,
                column: "Type",
                value: "Performance Coupe");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 112,
                column: "Type",
                value: "Electric Coupe");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 113,
                column: "Type",
                value: "Hybrid Coupe");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 114,
                column: "Type",
                value: "Subcompact Convertible");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 115,
                column: "Type",
                value: "Compact Convertible");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 116,
                column: "Type",
                value: "Mid-Size Convertible");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 117,
                column: "Type",
                value: "Luxury Convertible");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 118,
                column: "Type",
                value: "Performance Convertible");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 119,
                column: "Type",
                value: "Electric Convertible");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 120,
                column: "Type",
                value: "Hybrid Convertible");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 121,
                column: "Type",
                value: "Compact Wagon");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 122,
                column: "Type",
                value: "Mid-Size Wagon");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 123,
                column: "Type",
                value: "Full-Size Wagon");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 124,
                column: "Type",
                value: "Luxury Wagon");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 125,
                column: "Type",
                value: "Performance Wagon");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 126,
                column: "Type",
                value: "Electric Wagon");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 127,
                column: "Type",
                value: "Hybrid Wagon");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 128,
                column: "Type",
                value: "Subcompact Crossover");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 129,
                column: "Type",
                value: "Adventure SUV");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 130,
                column: "Type",
                value: "Retro SUV");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 131,
                column: "Type",
                value: "Urban SUV");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 132,
                column: "Type",
                value: "Compact Off-Road SUV");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 133,
                column: "Type",
                value: "Mid-Size Off-Road SUV");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 134,
                column: "Type",
                value: "Luxury Off-Road SUV");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 135,
                column: "Type",
                value: "Performance Off-Road SUV");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 136,
                column: "Type",
                value: "Electric Off-Road SUV");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 137,
                column: "Type",
                value: "Hybrid Off-Road SUV");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 138,
                column: "Type",
                value: "Micro Crossover");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 139,
                column: "Type",
                value: "Subcompact Performance SUV");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 140,
                column: "Type",
                value: "Mid-Size Performance SUV");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 141,
                column: "Type",
                value: "Full-Size Performance SUV");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 142,
                column: "Type",
                value: "Luxury Performance SUV");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 143,
                column: "Type",
                value: "Compact Hybrid SUV");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 144,
                column: "Type",
                value: "Full-Size Hybrid SUV");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 145,
                column: "Type",
                value: "Compact Electric SUV");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 146,
                column: "Type",
                value: "Mid-Size Electric SUV");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 147,
                column: "Type",
                value: "Full-Size Electric SUV");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 148,
                column: "Type",
                value: "Luxury Electric SUV");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 149,
                column: "Type",
                value: "Retro Hatchback");

            migrationBuilder.UpdateData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 150,
                column: "Type",
                value: "Concept Car");

            migrationBuilder.InsertData(
                schema: "Vehicle",
                table: "BodyTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 7, "Crossover" },
                    { 8, "Electric Car" },
                    { 10, "Sports Car" },
                    { 11, "Roadster" },
                    { 12, "Pony Car" },
                    { 13, "Muscle Car" },
                    { 14, "Supercar" },
                    { 15, "Hypercar" },
                    { 16, "Minicar" },
                    { 17, "Subcompact Car" },
                    { 18, "Compact Car" },
                    { 19, "Mid-Size Car" },
                    { 20, "Full-Size Car" },
                    { 21, "Luxury Car" },
                    { 22, "Grand Tourer" },
                    { 23, "Fastback" },
                    { 24, "Notchback" },
                    { 25, "Hardtop" },
                    { 26, "Targa Top" },
                    { 27, "Landau" },
                    { 28, "Shooting Brake" },
                    { 29, "Estate Car" },
                    { 30, "Liftback" },
                    { 31, "Kammback" },
                    { 32, "Bubble Car" },
                    { 33, "Microcar" },
                    { 34, "City Car" },
                    { 35, "Subcompact MPV" },
                    { 44, "Electric SUV" },
                    { 45, "Hybrid SUV" },
                    { 46, "Extended Length SUV" },
                    { 47, "Three-Row SUV" },
                    { 48, "Two-Row SUV" },
                    { 49, "Compact Crossover SUV" },
                    { 50, "Mid-Size Crossover SUV" },
                    { 65, "Compact Executive Car" },
                    { 66, "Executive Car" },
                    { 67, "Luxury Sedan" },
                    { 68, "Full-Size Luxury Sedan" },
                    { 69, "Performance Sedan" },
                    { 70, "Sports Sedan" },
                    { 71, "Touring Sedan" },
                    { 72, "Wagon Sedan" },
                    { 73, "Fastback Sedan" },
                    { 74, "Notchback Sedan" },
                    { 75, "Hardtop Sedan" },
                    { 76, "Pillarless Hardtop" },
                    { 77, "Long-Wheelbase Sedan" },
                    { 78, "Short-Wheelbase Sedan" },
                    { 79, "Hybrid Electric Sedan" },
                    { 80, "Compact Luxury Sedan" },
                    { 81, "Mid-Size Luxury Sedan" },
                    { 82, "Subcompact Luxury SUV" },
                    { 83, "Compact Luxury SUV" },
                    { 84, "Mid-Size Luxury SUV" },
                    { 85, "Full-Size Luxury SUV" },
                    { 86, "Retro Sedan" },
                    { 87, "Retro Coupe" },
                    { 88, "Soft-Top Convertible" },
                    { 89, "Hardtop Convertible" },
                    { 90, "Micro MPV" },
                    { 91, "Luxury Crossover" },
                    { 92, "Performance Crossover" },
                    { 93, "Electric Crossover" },
                    { 94, "Hybrid Crossover" },
                    { 95, "Compact Performance Car" },
                    { 96, "Mid-Size Performance Car" },
                    { 97, "Luxury Performance Car" },
                    { 98, "Electric Performance Car" },
                    { 99, "Hybrid Performance Car" },
                    { 100, "Subcompact Hatchback" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                schema: "Vehicle",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.RenameTable(
                name: "BodyTypes",
                schema: "Vehicle",
                newName: "BodyTypes",
                newSchema: "Vehicles");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                schema: "Vehicles",
                table: "BodyTypes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 9,
                column: "Type",
                value: "Crossover");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 36,
                column: "Type",
                value: "Electric Car");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 37,
                column: "Type",
                value: "Hybrid Car");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 38,
                column: "Type",
                value: "Sports Car");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 39,
                column: "Type",
                value: "Roadster");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 40,
                column: "Type",
                value: "Pony Car");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 41,
                column: "Type",
                value: "Muscle Car");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 42,
                column: "Type",
                value: "Supercar");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 43,
                column: "Type",
                value: "Hypercar");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 51,
                column: "Type",
                value: "Minicar");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 52,
                column: "Type",
                value: "Subcompact Car");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 53,
                column: "Type",
                value: "Compact Car");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 54,
                column: "Type",
                value: "Mid-Size Car");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 55,
                column: "Type",
                value: "Full-Size Car");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 56,
                column: "Type",
                value: "Luxury Car");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 57,
                column: "Type",
                value: "Grand Tourer");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 58,
                column: "Type",
                value: "Fastback");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 59,
                column: "Type",
                value: "Notchback");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 60,
                column: "Type",
                value: "Hardtop");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 61,
                column: "Type",
                value: "Targa Top");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 62,
                column: "Type",
                value: "Landau");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 63,
                column: "Type",
                value: "Shooting Brake");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 64,
                column: "Type",
                value: "Estate Car");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 101,
                column: "Type",
                value: "Liftback");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 102,
                column: "Type",
                value: "Kammback");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 103,
                column: "Type",
                value: "Bubble Car");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 104,
                column: "Type",
                value: "Microcar");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 105,
                column: "Type",
                value: "City Car");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 106,
                column: "Type",
                value: "Subcompact MPV");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 107,
                column: "Type",
                value: "Compact MPV");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 108,
                column: "Type",
                value: "Large MPV");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 109,
                column: "Type",
                value: "Panel Van");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 110,
                column: "Type",
                value: "Pickup SUV");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 111,
                column: "Type",
                value: "Coupe SUV");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 112,
                column: "Type",
                value: "Off-Road SUV");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 113,
                column: "Type",
                value: "Luxury SUV");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 114,
                column: "Type",
                value: "Performance SUV");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 115,
                column: "Type",
                value: "Electric SUV");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 116,
                column: "Type",
                value: "Hybrid SUV");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 117,
                column: "Type",
                value: "Extended Length SUV");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 118,
                column: "Type",
                value: "Three-Row SUV");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 119,
                column: "Type",
                value: "Two-Row SUV");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 120,
                column: "Type",
                value: "Compact Crossover SUV");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 121,
                column: "Type",
                value: "Mid-Size Crossover SUV");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 122,
                column: "Type",
                value: "Full-Size Crossover SUV");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 123,
                column: "Type",
                value: "Convertible SUV");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 124,
                column: "Type",
                value: "T-Top");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 125,
                column: "Type",
                value: "Phaeton");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 126,
                column: "Type",
                value: "Barchetta");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 127,
                column: "Type",
                value: "Spider");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 128,
                column: "Type",
                value: "Cabriolet");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 129,
                column: "Type",
                value: "Drophead Coupé");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 130,
                column: "Type",
                value: "Roadster Utility");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 131,
                column: "Type",
                value: "Club Coupe");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 132,
                column: "Type",
                value: "Opera Coupe");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 133,
                column: "Type",
                value: "Business Coupe");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 134,
                column: "Type",
                value: "Personal Luxury Coupe");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 135,
                column: "Type",
                value: "Four-Door Coupe");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 136,
                column: "Type",
                value: "Compact Executive Car");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 137,
                column: "Type",
                value: "Executive Car");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 138,
                column: "Type",
                value: "Luxury Sedan");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 139,
                column: "Type",
                value: "Full-Size Luxury Sedan");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 140,
                column: "Type",
                value: "Performance Sedan");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 141,
                column: "Type",
                value: "Sports Sedan");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 142,
                column: "Type",
                value: "Touring Sedan");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 143,
                column: "Type",
                value: "Wagon Sedan");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 144,
                column: "Type",
                value: "Fastback Sedan");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 145,
                column: "Type",
                value: "Notchback Sedan");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 146,
                column: "Type",
                value: "Hardtop Sedan");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 147,
                column: "Type",
                value: "Pillarless Hardtop");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 148,
                column: "Type",
                value: "Long-Wheelbase Sedan");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 149,
                column: "Type",
                value: "Short-Wheelbase Sedan");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 150,
                column: "Type",
                value: "Hybrid Electric Sedan");
        }
    }
}
