using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mottrist.Repository.Migrations
{
    /// <inheritdoc />
    public partial class addCarFieldsSeeData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "Type",
                value: "Convertible");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "Type",
                value: "Minivan");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 9,
                column: "Type",
                value: "Crossover");

            migrationBuilder.InsertData(
                schema: "Vehicles",
                table: "BodyTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 36, "Electric Car" },
                    { 37, "Hybrid Car" },
                    { 38, "Sports Car" },
                    { 39, "Roadster" },
                    { 40, "Pony Car" },
                    { 41, "Muscle Car" },
                    { 42, "Supercar" },
                    { 43, "Hypercar" },
                    { 51, "Minicar" },
                    { 52, "Subcompact Car" },
                    { 53, "Compact Car" },
                    { 54, "Mid-Size Car" },
                    { 55, "Full-Size Car" },
                    { 56, "Luxury Car" },
                    { 57, "Grand Tourer" },
                    { 58, "Fastback" },
                    { 59, "Notchback" },
                    { 60, "Hardtop" },
                    { 61, "Targa Top" },
                    { 62, "Landau" },
                    { 63, "Shooting Brake" },
                    { 64, "Estate Car" },
                    { 101, "Liftback" },
                    { 102, "Kammback" },
                    { 103, "Bubble Car" },
                    { 104, "Microcar" },
                    { 105, "City Car" },
                    { 106, "Subcompact MPV" },
                    { 107, "Compact MPV" },
                    { 108, "Large MPV" },
                    { 109, "Panel Van" },
                    { 110, "Pickup SUV" },
                    { 111, "Coupe SUV" },
                    { 112, "Off-Road SUV" },
                    { 113, "Luxury SUV" },
                    { 114, "Performance SUV" },
                    { 115, "Electric SUV" },
                    { 116, "Hybrid SUV" },
                    { 117, "Extended Length SUV" },
                    { 118, "Three-Row SUV" },
                    { 119, "Two-Row SUV" },
                    { 120, "Compact Crossover SUV" },
                    { 121, "Mid-Size Crossover SUV" },
                    { 122, "Full-Size Crossover SUV" },
                    { 123, "Convertible SUV" },
                    { 124, "T-Top" },
                    { 125, "Phaeton" },
                    { 126, "Barchetta" },
                    { 127, "Spider" },
                    { 128, "Cabriolet" },
                    { 129, "Drophead Coupé" },
                    { 130, "Roadster Utility" },
                    { 131, "Club Coupe" },
                    { 132, "Opera Coupe" },
                    { 133, "Business Coupe" },
                    { 134, "Personal Luxury Coupe" },
                    { 135, "Four-Door Coupe" },
                    { 136, "Compact Executive Car" },
                    { 137, "Executive Car" },
                    { 138, "Luxury Sedan" },
                    { 139, "Full-Size Luxury Sedan" },
                    { 140, "Performance Sedan" },
                    { 141, "Sports Sedan" },
                    { 142, "Touring Sedan" },
                    { 143, "Wagon Sedan" },
                    { 144, "Fastback Sedan" },
                    { 145, "Notchback Sedan" },
                    { 146, "Hardtop Sedan" },
                    { 147, "Pillarless Hardtop" },
                    { 148, "Long-Wheelbase Sedan" },
                    { 149, "Short-Wheelbase Sedan" },
                    { 150, "Hybrid Electric Sedan" }
                });

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Honda");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Ford");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Chevrolet");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Nissan");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Volkswagen");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "BMW");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "Mercedes-Benz");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 9,
                column: "Name",
                value: "Audi");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 10,
                column: "Name",
                value: "Hyundai");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 11,
                column: "Name",
                value: "Kia");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 12,
                column: "Name",
                value: "Renault");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 13,
                column: "Name",
                value: "Peugeot");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 14,
                column: "Name",
                value: "Fiat");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 15,
                column: "Name",
                value: "Mazda");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 16,
                column: "Name",
                value: "Subaru");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 17,
                column: "Name",
                value: "Suzuki");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 18,
                column: "Name",
                value: "Mitsubishi");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 19,
                column: "Name",
                value: "Volvo");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 20,
                column: "Name",
                value: "Tesla");

            migrationBuilder.InsertData(
                schema: "Vehicles",
                table: "Brands",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 21, "Jeep" },
                    { 22, "Land Rover" },
                    { 23, "Porsche" },
                    { 24, "Ferrari" },
                    { 25, "Lamborghini" },
                    { 26, "Aston Martin" },
                    { 27, "Bentley" },
                    { 28, "Rolls-Royce" },
                    { 29, "Bugatti" },
                    { 30, "Chrysler" },
                    { 31, "Dodge" },
                    { 32, "GMC" },
                    { 33, "Cadillac" },
                    { 34, "Lincoln" },
                    { 35, "Buick" },
                    { 36, "Opel" },
                    { 37, "Saab" },
                    { 38, "Skoda" },
                    { 39, "Seat" },
                    { 40, "Citroën" },
                    { 41, "Alfa Romeo" },
                    { 42, "Lancia" },
                    { 43, "Daewoo" },
                    { 44, "SsangYong" },
                    { 45, "Tata Motors" },
                    { 46, "Mahindra" },
                    { 47, "Hindustan Motors" },
                    { 48, "Maruti Suzuki" },
                    { 49, "Isuzu" },
                    { 50, "Daihatsu" },
                    { 51, "Hino Motors" },
                    { 52, "Lexus" },
                    { 53, "Infiniti" },
                    { 54, "Acura" },
                    { 55, "Scion" },
                    { 56, "Genesis" },
                    { 57, "Smart" },
                    { 58, "Mini" },
                    { 59, "Jaguar" },
                    { 60, "Lotus" },
                    { 61, "MG" },
                    { 62, "Rover" },
                    { 63, "Austin" },
                    { 64, "Morris" },
                    { 65, "Vauxhall" },
                    { 66, "Holden" },
                    { 67, "FPV" },
                    { 68, "HSV" },
                    { 69, "Proton" },
                    { 70, "Perodua" },
                    { 71, "Chery" },
                    { 72, "Geely" },
                    { 73, "BYD" },
                    { 74, "Great Wall Motors" },
                    { 75, "FAW" },
                    { 76, "Changan" },
                    { 77, "Dongfeng" },
                    { 78, "BAIC" },
                    { 79, "JAC" },
                    { 80, "Zotye" },
                    { 81, "Landwind" },
                    { 82, "Hawtai" },
                    { 83, "Soueast" },
                    { 84, "Brilliance" },
                    { 85, "Roewe" },
                    { 86, "MG Motor India" },
                    { 87, "Force Motors" },
                    { 88, "Premier Ltd." },
                    { 89, "Ram" },
                    { 90, "SRT" },
                    { 91, "Hummer" },
                    { 92, "SML Isuzu" },
                    { 93, "Ashok Leyland" },
                    { 94, "Eicher Motors" },
                    { 95, "Piaggio" },
                    { 96, "Bajaj Auto" },
                    { 97, "TVS Motors" },
                    { 98, "Hero MotoCorp" },
                    { 99, "Yamaha Motor Company" },
                    { 100, "Suzuki Motorcycles" },
                    { 101, "Kawasaki" },
                    { 102, "Ducati" },
                    { 103, "Harley-Davidson" },
                    { 104, "Triumph" },
                    { 105, "KTM" },
                    { 106, "Aprilia" },
                    { 107, "Vespa" },
                    { 108, "Royal Enfield" },
                    { 109, "Benelli" },
                    { 110, "Moto Guzzi" },
                    { 111, "Husqvarna" },
                    { 112, "Indian Motorcycle" },
                    { 113, "MV Agusta" },
                    { 114, "Bimota" },
                    { 115, "Norton" },
                    { 116, "Zero Motorcycles" },
                    { 117, "CFMoto" },
                    { 118, "Ural" },
                    { 119, "Victory Motorcycles" },
                    { 120, "Sym" },
                    { 121, "Kymco" },
                    { 122, "PGO Scooters" },
                    { 123, "Lifan" },
                    { 124, "Haojue" },
                    { 125, "QJMotor" },
                    { 126, "Zongshen" },
                    { 127, "Loncin" },
                    { 128, "Dayun" },
                    { 129, "Jincheng" },
                    { 130, "Shineray" },
                    { 131, "Daelim" },
                    { 132, "Hyosung" },
                    { 133, "Keeway" },
                    { 134, "Super Soco" },
                    { 135, "Energica" },
                    { 136, "Arcimoto" },
                    { 137, "Rivian" },
                    { 138, "Lucid Motors" },
                    { 139, "Fisker" },
                    { 140, "NIO" },
                    { 141, "XPeng" },
                    { 142, "Li Auto" },
                    { 143, "Polestar" },
                    { 144, "VinFast" },
                    { 145, "Sono Motors" },
                    { 146, "Lightyear" },
                    { 147, "Aptera Motors" },
                    { 148, "Faraday Future" },
                    { 149, "Lordstown Motors" },
                    { 150, "Canoo" },
                    { 151, "Bollinger Motors" },
                    { 152, "ElectraMeccanica" },
                    { 153, "Pininfarina" },
                    { 154, "Maserati" },
                    { 155, "McLaren" },
                    { 156, "Pagani" },
                    { 157, "Koenigsegg" },
                    { 158, "Rimac" },
                    { 159, "Spyker" },
                    { 160, "De Tomaso" },
                    { 161, "Hispano Suiza" },
                    { 162, "DS Automobiles" },
                    { 163, "Saleen" },
                    { 164, "Iveco" },
                    { 165, "MAN" },
                    { 166, "Scania" },
                    { 167, "DAF" },
                    { 168, "Navistar" },
                    { 169, "Paccar" },
                    { 170, "Kenworth" },
                    { 171, "Peterbilt" },
                    { 172, "Freightliner" },
                    { 173, "Mack Trucks" },
                    { 174, "Western Star" },
                    { 175, "Volvo Trucks" },
                    { 176, "Renault Trucks" },
                    { 177, "Tatra" },
                    { 178, "Kamaz" },
                    { 179, "UralAZ" },
                    { 180, "GAZ" },
                    { 181, "ZIL" },
                    { 182, "MAZ" },
                    { 183, "KrAZ" },
                    { 184, "BelAZ" },
                    { 185, "Sinotruk" },
                    { 186, "Foton" },
                    { 187, "Shacman" },
                    { 188, "Howo" },
                    { 189, "Yutong" },
                    { 190, "King Long" },
                    { 191, "Golden Dragon" },
                    { 192, "Higer" },
                    { 193, "Ankai" },
                    { 194, "Sollers" },
                    { 195, "Avtovaz" },
                    { 196, "Lada" },
                    { 197, "UAZ" },
                    { 198, "Moskvitch" },
                    { 199, "Dacia" },
                    { 200, "International Harvester" }
                });

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Green");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Black");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "White");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Silver");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "Gray");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "Yellow");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 9,
                column: "Name",
                value: "Brown");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 10,
                column: "Name",
                value: "Purple");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 11,
                column: "Name",
                value: "Candy Apple Red");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 12,
                column: "Name",
                value: "Midnight Blue");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 13,
                column: "Name",
                value: "Forest Green");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 14,
                column: "Name",
                value: "Jet Black");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 15,
                column: "Name",
                value: "Pearl White");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 16,
                column: "Name",
                value: "Metallic Silver");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 17,
                column: "Name",
                value: "Slate Gray");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 18,
                column: "Name",
                value: "Sunburst Yellow");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 19,
                column: "Name",
                value: "Mocha Brown");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 20,
                column: "Name",
                value: "Vivid Purple");

            migrationBuilder.InsertData(
                schema: "Vehicles",
                table: "Colors",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 21, "Ruby Red" },
                    { 22, "Sapphire Blue" },
                    { 23, "Emerald Green" },
                    { 24, "Obsidian Black" },
                    { 25, "Ivory White" },
                    { 26, "Titanium Silver" },
                    { 27, "Charcoal Gray" },
                    { 28, "Canary Yellow" },
                    { 29, "Chocolate Brown" },
                    { 30, "Amethyst Purple" },
                    { 31, "Crimson Red" },
                    { 32, "Navy Blue" },
                    { 33, "Olive Green" },
                    { 34, "Onyx Black" },
                    { 35, "Cream White" },
                    { 36, "Platinum Silver" },
                    { 37, "Gunmetal Gray" },
                    { 38, "Lemon Yellow" },
                    { 39, "Espresso Brown" },
                    { 40, "Plum Purple" },
                    { 41, "Fire Engine Red" },
                    { 42, "Sky Blue" },
                    { 43, "Lime Green" },
                    { 44, "Matte Black" },
                    { 45, "Snow White" },
                    { 46, "Brushed Silver" },
                    { 47, "Steel Gray" },
                    { 48, "Mustard Yellow" },
                    { 49, "Caramel Brown" },
                    { 50, "Violet Purple" },
                    { 51, "Cherry Red" },
                    { 52, "Cobalt Blue" },
                    { 53, "Mint Green" },
                    { 54, "Ebony Black" },
                    { 55, "Alabaster White" },
                    { 56, "Chrome Silver" },
                    { 57, "Pewter Gray" },
                    { 58, "Saffron Yellow" },
                    { 59, "Walnut Brown" },
                    { 60, "Lilac Purple" },
                    { 61, "Scarlet Red" },
                    { 62, "Aqua Blue" },
                    { 63, "Jade Green" },
                    { 64, "Satin Black" },
                    { 65, "Chalk White" },
                    { 66, "Liquid Silver" },
                    { 67, "Iron Gray" },
                    { 68, "Amber Yellow" },
                    { 69, "Hazelnut Brown" },
                    { 70, "Orchid Purple" },
                    { 71, "Burgundy Red" },
                    { 72, "Turquoise Blue" },
                    { 73, "Sage Green" },
                    { 74, "Gloss Black" },
                    { 75, "Eggshell White" },
                    { 76, "Polished Silver" },
                    { 77, "Ash Gray" },
                    { 78, "Golden Yellow" },
                    { 79, "Cocoa Brown" },
                    { 80, "Magenta Purple" },
                    { 81, "Maroon Red" },
                    { 82, "Cerulean Blue" },
                    { 83, "Pine Green" },
                    { 84, "Raven Black" },
                    { 85, "Porcelain White" },
                    { 86, "Sterling Silver" },
                    { 87, "Smoke Gray" },
                    { 88, "Honey Yellow" },
                    { 89, "Truffle Brown" },
                    { 90, "Fuchsia Purple" },
                    { 91, "Coral Red" },
                    { 92, "Teal Blue" },
                    { 93, "Moss Green" },
                    { 94, "Carbon Black" },
                    { 95, "Milky White" },
                    { 96, "Quicksilver" },
                    { 97, "Granite Gray" },
                    { 98, "Butter Yellow" },
                    { 99, "Sienna Brown" },
                    { 100, "Indigo Purple" },
                    { 101, "Vermilion Red" },
                    { 102, "Azure Blue" },
                    { 103, "Fern Green" },
                    { 104, "Midnight Black" },
                    { 105, "Opal White" },
                    { 106, "Iridescent Silver" },
                    { 107, "Basalt Gray" },
                    { 108, "Dandelion Yellow" },
                    { 109, "Mahogany Brown" },
                    { 110, "Eggplant Purple" },
                    { 111, "Garnet Red" },
                    { 112, "Ocean Blue" },
                    { 113, "Kelly Green" },
                    { 114, "Shadow Black" },
                    { 115, "Frost White" },
                    { 116, "Mercury Silver" },
                    { 117, "Flint Gray" },
                    { 118, "Mango Yellow" },
                    { 119, "Cedar Brown" },
                    { 120, "Lavender Purple" },
                    { 121, "Brick Red" },
                    { 122, "Denim Blue" },
                    { 123, "Shamrock Green" },
                    { 124, "Pitch Black" },
                    { 125, "Linen White" },
                    { 126, "Aluminum Silver" },
                    { 127, "Storm Gray" },
                    { 128, "Tangerine Yellow" },
                    { 129, "Teak Brown" },
                    { 130, "Mauve Purple" },
                    { 131, "Rose Red" },
                    { 132, "Glacier Blue" },
                    { 133, "Cypress Green" },
                    { 134, "Coal Black" },
                    { 135, "Marble White" },
                    { 136, "Nickel Silver" },
                    { 137, "Mist Gray" },
                    { 138, "Banana Yellow" },
                    { 139, "Pecan Brown" },
                    { 140, "Grape Purple" },
                    { 141, "Salmon Red" },
                    { 142, "Lagoon Blue" },
                    { 143, "Spruce Green" },
                    { 144, "Velvet Black" },
                    { 145, "Bone White" },
                    { 146, "Pewter Silver" },
                    { 147, "Fog Gray" },
                    { 148, "Pumpkin Yellow" },
                    { 149, "Oak Brown" },
                    { 150, "Berry Purple" },
                    { 151, "Tomato Red" },
                    { 152, "Powder Blue" },
                    { 153, "Avocado Green" },
                    { 154, "Smoky Black" },
                    { 155, "Cotton White" },
                    { 156, "Zinc Silver" },
                    { 157, "Cloud Gray" },
                    { 158, "Apricot Yellow" },
                    { 159, "Chestnut Brown" },
                    { 160, "Raspberry Purple" },
                    { 161, "Blood Red" },
                    { 162, "Periwinkle Blue" },
                    { 163, "Basil Green" },
                    { 164, "Graphite Black" },
                    { 165, "Seashell White" },
                    { 166, "Tin Silver" },
                    { 167, "Dusk Gray" },
                    { 168, "Peach Yellow" },
                    { 169, "Maple Brown" },
                    { 170, "Blackberry Purple" },
                    { 171, "Cardinal Red" },
                    { 172, "Cornflower Blue" },
                    { 173, "Thyme Green" },
                    { 174, "Anthracite Black" },
                    { 175, "Vanilla White" },
                    { 176, "Steel Silver" },
                    { 177, "Haze Gray" },
                    { 178, "Citron Yellow" },
                    { 179, "Acorn Brown" },
                    { 180, "Mulberry Purple" },
                    { 181, "Chili Red" },
                    { 182, "Robin Egg Blue" },
                    { 183, "Celery Green" },
                    { 184, "Soot Black" },
                    { 185, "Parchment White" },
                    { 186, "Cadmium Silver" },
                    { 187, "Shale Gray" },
                    { 188, "Marigold Yellow" },
                    { 189, "Bark Brown" },
                    { 190, "Boysenberry Purple" },
                    { 191, "Poppy Red" },
                    { 192, "Baby Blue" },
                    { 193, "Pistachio Green" },
                    { 194, "Ink Black" },
                    { 195, "Quartz White" },
                    { 196, "Mithril Silver" },
                    { 197, "Cinder Gray" },
                    { 198, "Ginger Yellow" },
                    { 199, "Saddle Brown" },
                    { 200, "Concord Purple" },
                    { 201, "Sangria Red" },
                    { 202, "Peacock Blue" },
                    { 203, "Parrot Green" },
                    { 204, "Caviar Black" },
                    { 205, "Lace White" },
                    { 206, "Bismuth Silver" },
                    { 207, "Driftwood Gray" },
                    { 208, "Papaya Yellow" },
                    { 209, "Rosewood Brown" },
                    { 210, "Huckleberry Purple" },
                    { 211, "Flamingo Pink" },
                    { 212, "Arctic Blue" },
                    { 213, "Seafoam Green" },
                    { 214, "Phantom Black" },
                    { 215, "Dove White" },
                    { 216, "Tungsten Silver" },
                    { 217, "Boulder Gray" },
                    { 218, "Nectarine Orange" },
                    { 219, "Ebony Brown" },
                    { 220, "Wisteria Purple" }
                });

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Type",
                value: "Petrol (Gasoline)");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Type",
                value: "Liquefied Petroleum Gas (LPG)");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "Type",
                value: "Compressed Natural Gas (CNG)");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "Type",
                value: "Natural Gas");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "Type",
                value: "Kerosene");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 7,
                column: "Type",
                value: "Heavy Fuel Oil (HFO)");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 8,
                column: "Type",
                value: "Ethanol (E85, etc.)");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 9,
                column: "Type",
                value: "Biodiesel (B20, etc.)");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 10,
                column: "Type",
                value: "Biobutanol");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 11,
                column: "Type",
                value: "Biogas");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 12,
                column: "Type",
                value: "Electricity");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 13,
                column: "Type",
                value: "Hydrogen");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 14,
                column: "Type",
                value: "Green Hydrogen");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 15,
                column: "Type",
                value: "Synthetic Diesel");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 16,
                column: "Type",
                value: "Synthetic Gasoline");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 17,
                column: "Type",
                value: "E-Fuel");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 18,
                column: "Type",
                value: "Jet Fuel (Jet A, Jet A-1)");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 19,
                column: "Type",
                value: "Avgas");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 20,
                column: "Type",
                value: "Propane");

            migrationBuilder.InsertData(
                schema: "Vehicles",
                table: "FuelTypes",
                columns: new[] { "Id", "Type" },
                values: new object[] { 21, "Methanol" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 120);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 121);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 122);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 123);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 124);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 125);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 126);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 127);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 128);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 129);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 130);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 131);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 132);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 133);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 134);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 135);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 136);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 137);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 138);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 139);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 140);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 141);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 142);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 143);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 144);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 145);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 146);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 147);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 148);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 149);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 150);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 120);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 121);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 122);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 123);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 124);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 125);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 126);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 127);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 128);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 129);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 130);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 131);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 132);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 133);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 134);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 135);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 136);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 137);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 138);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 139);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 140);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 141);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 142);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 143);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 144);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 145);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 146);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 147);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 148);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 149);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 150);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 151);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 152);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 153);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 154);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 155);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 156);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 157);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 158);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 159);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 160);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 161);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 162);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 163);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 164);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 165);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 166);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 167);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 168);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 169);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 170);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 171);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 172);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 173);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 174);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 175);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 176);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 177);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 178);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 179);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 180);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 181);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 182);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 183);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 184);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 185);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 186);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 187);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 188);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 189);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 190);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 191);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 192);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 193);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 194);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 195);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 196);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 197);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 198);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 199);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 200);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 120);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 121);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 122);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 123);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 124);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 125);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 126);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 127);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 128);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 129);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 130);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 131);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 132);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 133);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 134);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 135);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 136);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 137);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 138);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 139);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 140);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 141);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 142);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 143);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 144);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 145);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 146);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 147);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 148);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 149);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 150);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 151);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 152);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 153);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 154);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 155);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 156);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 157);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 158);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 159);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 160);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 161);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 162);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 163);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 164);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 165);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 166);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 167);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 168);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 169);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 170);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 171);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 172);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 173);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 174);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 175);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 176);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 177);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 178);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 179);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 180);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 181);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 182);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 183);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 184);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 185);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 186);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 187);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 188);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 189);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 190);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 191);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 192);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 193);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 194);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 195);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 196);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 197);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 198);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 199);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 200);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 201);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 202);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 203);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 204);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 205);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 206);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 207);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 208);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 209);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 210);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 211);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 212);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 213);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 214);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 215);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 216);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 217);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 218);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 219);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 220);

            migrationBuilder.DeleteData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "Type",
                value: "Pickup");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "Type",
                value: "Convertible");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "BodyTypes",
                keyColumn: "Id",
                keyValue: 9,
                column: "Type",
                value: "Roadster");

            migrationBuilder.InsertData(
                schema: "Vehicles",
                table: "BodyTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 7, "Wagon" },
                    { 8, "Minivan" },
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

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Ford");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Honda");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Tesla");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "BMW");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Chevrolet");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "Mercedes-Benz");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "Audi");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 9,
                column: "Name",
                value: "Nissan");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 10,
                column: "Name",
                value: "Volkswagen");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 11,
                column: "Name",
                value: "Hyundai");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 12,
                column: "Name",
                value: "Kia");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 13,
                column: "Name",
                value: "Subaru");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 14,
                column: "Name",
                value: "Mazda");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 15,
                column: "Name",
                value: "Lexus");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 16,
                column: "Name",
                value: "Jaguar");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 17,
                column: "Name",
                value: "Porsche");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 18,
                column: "Name",
                value: "Land Rover");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 19,
                column: "Name",
                value: "Ferrari");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Brands",
                keyColumn: "Id",
                keyValue: 20,
                column: "Name",
                value: "Lamborghini");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Black");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "White");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Green");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Yellow");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "Orange");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "Purple");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 9,
                column: "Name",
                value: "Silver");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 10,
                column: "Name",
                value: "Gray");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 11,
                column: "Name",
                value: "Brown");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 12,
                column: "Name",
                value: "Beige");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 13,
                column: "Name",
                value: "Pink");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 14,
                column: "Name",
                value: "Gold");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 15,
                column: "Name",
                value: "Turquoise");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 16,
                column: "Name",
                value: "Teal");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 17,
                column: "Name",
                value: "Magenta");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 18,
                column: "Name",
                value: "Copper");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 19,
                column: "Name",
                value: "Ivory");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "Colors",
                keyColumn: "Id",
                keyValue: 20,
                column: "Name",
                value: "Champagne");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Type",
                value: "Petrol");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Type",
                value: "Electric");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "Type",
                value: "Hybrid");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "Type",
                value: "CNG");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "Type",
                value: "LPG");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 7,
                column: "Type",
                value: "Ethanol");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 8,
                column: "Type",
                value: "Biofuel");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 9,
                column: "Type",
                value: "Hydrogen");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 10,
                column: "Type",
                value: "Propane");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 11,
                column: "Type",
                value: "Methanol");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 12,
                column: "Type",
                value: "Butanol");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 13,
                column: "Type",
                value: "Natural Gas");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 14,
                column: "Type",
                value: "Biodiesel");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 15,
                column: "Type",
                value: "Alcohol");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 16,
                column: "Type",
                value: "Fischer-Tropsch");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 17,
                column: "Type",
                value: "Electric + Petrol");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 18,
                column: "Type",
                value: "Electric + Diesel");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 19,
                column: "Type",
                value: "Electric + Hydrogen");

            migrationBuilder.UpdateData(
                schema: "Vehicles",
                table: "FuelTypes",
                keyColumn: "Id",
                keyValue: 20,
                column: "Type",
                value: "Compressed Natural Gas (CNG)");
        }
    }
}
