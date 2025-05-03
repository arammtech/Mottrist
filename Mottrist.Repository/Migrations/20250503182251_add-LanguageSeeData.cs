using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mottrist.Repository.Migrations
{
    /// <inheritdoc />
    public partial class addLanguageSeeData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Spanish");

            migrationBuilder.UpdateData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "German");

            migrationBuilder.InsertData(
                schema: "Localization",
                table: "Languages",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 5, "Chinese" },
                    { 6, "Japanese" },
                    { 7, "Arabic" },
                    { 8, "Russian" },
                    { 9, "Portuguese" },
                    { 10, "Hindi" },
                    { 11, "Bengali" },
                    { 12, "Urdu" },
                    { 13, "Italian" },
                    { 14, "Dutch" },
                    { 15, "Greek" },
                    { 16, "Turkish" },
                    { 17, "Korean" },
                    { 18, "Vietnamese" },
                    { 19, "Swedish" },
                    { 20, "Polish" },
                    { 21, "Finnish" },
                    { 22, "Hebrew" },
                    { 23, "Malay" },
                    { 24, "Indonesian" },
                    { 25, "Thai" },
                    { 26, "Hungarian" },
                    { 27, "Czech" },
                    { 28, "Romanian" },
                    { 29, "Bulgarian" },
                    { 30, "Persian" },
                    { 31, "Swahili" },
                    { 32, "Filipino" },
                    { 33, "Tamil" },
                    { 34, "Telugu" },
                    { 35, "Marathi" },
                    { 36, "Serbian" },
                    { 37, "Croatian" },
                    { 38, "Slovak" },
                    { 39, "Danish" },
                    { 40, "Norwegian" },
                    { 41, "Ukrainian" },
                    { 42, "Lithuanian" },
                    { 43, "Latvian" },
                    { 44, "Estonian" },
                    { 45, "Macedonian" },
                    { 46, "Armenian" },
                    { 47, "Georgian" },
                    { 48, "Pashto" },
                    { 49, "Sinhala" },
                    { 50, "Mongolian" },
                    { 51, "Basque" },
                    { 52, "Catalan" },
                    { 53, "Malagasy" },
                    { 54, "Azerbaijani" },
                    { 55, "Kurdish" },
                    { 56, "Tatar" },
                    { 57, "Belarusian" },
                    { 58, "Welsh" },
                    { 59, "Irish" },
                    { 60, "Yiddish" },
                    { 61, "Nepali" },
                    { 62, "Javanese" },
                    { 63, "Sundanese" },
                    { 64, "Gujarati" },
                    { 65, "Haitian Creole" },
                    { 66, "Zulu" },
                    { 67, "Xhosa" },
                    { 68, "Hausa" },
                    { 69, "Igbo" },
                    { 70, "Samoan" },
                    { 71, "Māori" },
                    { 72, "Tibetan" },
                    { 73, "Lao" },
                    { 74, "Burmese" },
                    { 75, "Khmer" },
                    { 76, "Twi" },
                    { 77, "Amharic" },
                    { 78, "Tigrinya" },
                    { 79, "Maldivian" },
                    { 80, "Oromo" },
                    { 81, "Fula" },
                    { 82, "Chichewa" },
                    { 83, "Bambara" },
                    { 84, "Tswana" },
                    { 85, "Shona" },
                    { 86, "Sesotho" },
                    { 87, "Wolof" },
                    { 88, "Dzongkha" },
                    { 89, "Kanuri" },
                    { 90, "Ga" },
                    { 91, "Acholi" },
                    { 92, "Ewe" },
                    { 93, "Bislama" },
                    { 94, "Tok Pisin" },
                    { 95, "Nauruan" },
                    { 96, "Chamorro" },
                    { 97, "Palauan" },
                    { 98, "Tuvaluan" },
                    { 99, "Marshallese" },
                    { 100, "Tetum" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.UpdateData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Arabic");

            migrationBuilder.UpdateData(
                schema: "Localization",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Spanish");
        }
    }
}
