using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mottrist.Repository.Migrations
{
    /// <inheritdoc />
    public partial class seeddatalookupTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "Vehicles",
                table: "BodyTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 6, "Convertible" },
                    { 7, "Wagon" },
                    { 8, "Minivan" },
                    { 9, "Roadster" },
                    { 10, "Crossover" },
                    { 11, "Limousine" },
                    { 12, "Van" },
                    { 13, "Sports Car" },
                    { 14, "Luxury Sedan" },
                    { 15, "Coupe Convertible" },
                    { 16, "Station Wagon" },
                    { 17, "Supercar" },
                    { 18, "Hypercar" },
                    { 19, "Off-road" },
                    { 20, "Targa" }
                });

            migrationBuilder.InsertData(
                schema: "Vehicles",
                table: "Brands",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 6, "Chevrolet" },
                    { 7, "Mercedes-Benz" },
                    { 8, "Audi" },
                    { 9, "Nissan" },
                    { 10, "Volkswagen" },
                    { 11, "Hyundai" },
                    { 12, "Kia" },
                    { 13, "Subaru" },
                    { 14, "Mazda" },
                    { 15, "Lexus" },
                    { 16, "Jaguar" },
                    { 17, "Porsche" },
                    { 18, "Land Rover" },
                    { 19, "Ferrari" },
                    { 20, "Lamborghini" }
                });

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CountryId", "Name" },
                values: new object[] { 1, "Philadelphia" });

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CountryId", "Name" },
                values: new object[] { 1, "San Antonio" });

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CountryId", "Name" },
                values: new object[] { 1, "San Diego" });

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CountryId", "Name" },
                values: new object[] { 1, "Dallas" });

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CountryId", "Name" },
                values: new object[] { 1, "San Jose" });

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CountryId", "Name" },
                values: new object[] { 2, "Toronto" });

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CountryId", "Name" },
                values: new object[] { 2, "Montreal" });

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CountryId", "Name" },
                values: new object[] { 2, "Vancouver" });

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CountryId", "Name" },
                values: new object[] { 2, "Calgary" });

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CountryId", "Name" },
                values: new object[] { 2, "Ottawa" });

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CountryId", "Name" },
                values: new object[] { 2, "Edmonton" });

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CountryId", "Name" },
                values: new object[] { 2, "Winnipeg" });

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CountryId", "Name" },
                values: new object[] { 2, "Quebec City" });

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CountryId", "Name" },
                values: new object[] { 2, "Hamilton" });

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CountryId", "Name" },
                values: new object[] { 2, "Kitchener" });

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CountryId", "Name" },
                values: new object[] { 3, "London" });

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CountryId", "Name" },
                values: new object[] { 3, "Manchester" });

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CountryId", "Name" },
                values: new object[] { 3, "Birmingham" });

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CountryId", "Name" },
                values: new object[] { 3, "Liverpool" });

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "CountryId", "Name" },
                values: new object[] { 3, "Leeds" });

            migrationBuilder.InsertData(
                schema: "Geography",
                table: "Cities",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[,]
                {
                    { 26, 3, "Sheffield" },
                    { 27, 3, "Edinburgh" },
                    { 28, 3, "Glasgow" },
                    { 29, 3, "Bristol" },
                    { 30, 3, "Nottingham" },
                    { 31, 4, "Berlin" },
                    { 32, 4, "Munich" },
                    { 33, 4, "Frankfurt" },
                    { 34, 4, "Hamburg" },
                    { 35, 4, "Cologne" },
                    { 36, 4, "Stuttgart" },
                    { 37, 4, "Düsseldorf" },
                    { 38, 4, "Dortmund" },
                    { 39, 4, "Essen" },
                    { 40, 4, "Bremen" },
                    { 41, 5, "Paris" },
                    { 42, 5, "Marseille" },
                    { 43, 5, "Lyon" },
                    { 44, 5, "Toulouse" },
                    { 45, 5, "Nice" },
                    { 46, 5, "Nantes" },
                    { 47, 5, "Strasbourg" },
                    { 48, 5, "Montpellier" },
                    { 49, 5, "Bordeaux" },
                    { 50, 5, "Lille" }
                });

            migrationBuilder.InsertData(
                schema: "Vehicles",
                table: "Colors",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 6, "Yellow" },
                    { 7, "Orange" },
                    { 8, "Purple" },
                    { 9, "Silver" },
                    { 10, "Gray" },
                    { 11, "Brown" },
                    { 12, "Beige" },
                    { 13, "Pink" },
                    { 14, "Gold" },
                    { 15, "Turquoise" },
                    { 16, "Teal" },
                    { 17, "Magenta" },
                    { 18, "Copper" },
                    { 19, "Ivory" },
                    { 20, "Champagne" }
                });

            migrationBuilder.InsertData(
                schema: "Geography",
                table: "Countries",
                columns: new[] { "Id", "Continent", "Name" },
                values: new object[,]
                {
                    { 6, (byte)6, "Australia" },
                    { 7, (byte)7, "Brazil" },
                    { 8, (byte)7, "Argentina" },
                    { 9, (byte)3, "China" },
                    { 10, (byte)3, "Japan" },
                    { 11, (byte)3, "India" },
                    { 12, (byte)3, "South Korea" },
                    { 13, (byte)1, "South Africa" },
                    { 14, (byte)1, "Nigeria" },
                    { 15, (byte)1, "Egypt" },
                    { 16, (byte)5, "Mexico" },
                    { 17, (byte)4, "Italy" },
                    { 18, (byte)4, "Spain" },
                    { 19, (byte)4, "Russia" },
                    { 20, (byte)3, "Turkey" }
                });

            migrationBuilder.InsertData(
                schema: "Vehicles",
                table: "FuelTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 5, "CNG" },
                    { 6, "LPG" },
                    { 7, "Ethanol" },
                    { 8, "Biofuel" },
                    { 9, "Hydrogen" },
                    { 10, "Propane" },
                    { 11, "Methanol" },
                    { 12, "Butanol" },
                    { 13, "Natural Gas" },
                    { 14, "Biodiesel" },
                    { 15, "Alcohol" },
                    { 16, "Fischer-Tropsch" },
                    { 17, "Electric + Petrol" },
                    { 18, "Electric + Diesel" },
                    { 19, "Electric + Hydrogen" },
                    { 20, "Compressed Natural Gas (CNG)" }
                });

            migrationBuilder.InsertData(
                schema: "Vehicles",
                table: "Models",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
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

            migrationBuilder.InsertData(
                schema: "Geography",
                table: "Cities",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[,]
                {
                    { 51, 6, "Sydney" },
                    { 52, 6, "Melbourne" },
                    { 53, 6, "Brisbane" },
                    { 54, 6, "Perth" },
                    { 55, 6, "Adelaide" },
                    { 56, 6, "Gold Coast" },
                    { 57, 6, "Hobart" },
                    { 58, 6, "Canberra" },
                    { 59, 6, "Newcastle" },
                    { 60, 6, "Wollongong" },
                    { 61, 7, "São Paulo" },
                    { 62, 7, "Rio de Janeiro" },
                    { 63, 7, "Brasília" },
                    { 64, 7, "Salvador" },
                    { 65, 7, "Fortaleza" },
                    { 66, 7, "Belo Horizonte" },
                    { 67, 7, "Manaus" },
                    { 68, 7, "Curitiba" },
                    { 69, 7, "Recife" },
                    { 70, 7, "Porto Alegre" },
                    { 71, 8, "Buenos Aires" },
                    { 72, 8, "Córdoba" },
                    { 73, 8, "Rosario" },
                    { 74, 8, "Mendoza" },
                    { 75, 8, "La Plata" },
                    { 76, 8, "San Miguel de Tucumán" },
                    { 77, 8, "Mar del Plata" },
                    { 78, 8, "Salta" },
                    { 79, 8, "Santa Fe" },
                    { 80, 8, "Santiago del Estero" },
                    { 81, 9, "Beijing" },
                    { 82, 9, "Shanghai" },
                    { 83, 9, "Guangzhou" },
                    { 84, 9, "Shenzhen" },
                    { 85, 9, "Chengdu" },
                    { 86, 9, "Hangzhou" },
                    { 87, 9, "Xi'an" },
                    { 88, 9, "Wuhan" },
                    { 89, 9, "Chongqing" },
                    { 90, 9, "Tianjin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Models",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Models",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Models",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Models",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Models",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Models",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Models",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Models",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Models",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Models",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Models",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Models",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Models",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Models",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Models",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Countries",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CountryId", "Name" },
                values: new object[] { 2, "Toronto" });

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CountryId", "Name" },
                values: new object[] { 2, "Montreal" });

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CountryId", "Name" },
                values: new object[] { 2, "Vancouver" });

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CountryId", "Name" },
                values: new object[] { 2, "Calgary" });

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CountryId", "Name" },
                values: new object[] { 2, "Ottawa" });

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CountryId", "Name" },
                values: new object[] { 3, "London" });

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CountryId", "Name" },
                values: new object[] { 3, "Manchester" });

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CountryId", "Name" },
                values: new object[] { 3, "Birmingham" });

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CountryId", "Name" },
                values: new object[] { 3, "Liverpool" });

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CountryId", "Name" },
                values: new object[] { 3, "Leeds" });

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CountryId", "Name" },
                values: new object[] { 4, "Berlin" });

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CountryId", "Name" },
                values: new object[] { 4, "Munich" });

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CountryId", "Name" },
                values: new object[] { 4, "Frankfurt" });

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CountryId", "Name" },
                values: new object[] { 4, "Hamburg" });

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CountryId", "Name" },
                values: new object[] { 4, "Cologne" });

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CountryId", "Name" },
                values: new object[] { 5, "Paris" });

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CountryId", "Name" },
                values: new object[] { 5, "Marseille" });

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CountryId", "Name" },
                values: new object[] { 5, "Lyon" });

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CountryId", "Name" },
                values: new object[] { 5, "Toulouse" });

            migrationBuilder.UpdateData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "CountryId", "Name" },
                values: new object[] { 5, "Nice" });
        }
    }
}
