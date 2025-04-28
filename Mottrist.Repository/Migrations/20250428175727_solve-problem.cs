using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mottrist.Repository.Migrations
{
    /// <inheritdoc />
    public partial class solveproblem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Travellers_Languages_PreferredLanguageId",
                schema: "Travellers",
                table: "Travellers");

            migrationBuilder.AlterColumn<int>(
                name: "PreferredLanguageId",
                schema: "Travellers",
                table: "Travellers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                schema: "Travellers",
                table: "Travellers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Travellers_CityId",
                schema: "Travellers",
                table: "Travellers",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Travellers_Cities_CityId",
                schema: "Travellers",
                table: "Travellers",
                column: "CityId",
                principalSchema: "Geography",
                principalTable: "Cities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Travellers_Languages_PreferredLanguageId",
                schema: "Travellers",
                table: "Travellers",
                column: "PreferredLanguageId",
                principalSchema: "Localization",
                principalTable: "Languages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Travellers_Cities_CityId",
                schema: "Travellers",
                table: "Travellers");

            migrationBuilder.DropForeignKey(
                name: "FK_Travellers_Languages_PreferredLanguageId",
                schema: "Travellers",
                table: "Travellers");

            migrationBuilder.DropIndex(
                name: "IX_Travellers_CityId",
                schema: "Travellers",
                table: "Travellers");

            migrationBuilder.DropColumn(
                name: "CityId",
                schema: "Travellers",
                table: "Travellers");

            migrationBuilder.AlterColumn<int>(
                name: "PreferredLanguageId",
                schema: "Travellers",
                table: "Travellers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Travellers_Languages_PreferredLanguageId",
                schema: "Travellers",
                table: "Travellers",
                column: "PreferredLanguageId",
                principalSchema: "Localization",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
