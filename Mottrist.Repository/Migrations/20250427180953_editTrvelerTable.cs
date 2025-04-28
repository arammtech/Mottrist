using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mottrist.Repository.Migrations
{
    /// <inheritdoc />
    public partial class editTrvelerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PreferredLanguageId",
                schema: "Travellers",
                table: "Travellers",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Travellers_PreferredLanguageId",
                schema: "Travellers",
                table: "Travellers",
                column: "PreferredLanguageId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Travellers_Languages_PreferredLanguageId",
                schema: "Travellers",
                table: "Travellers");

            migrationBuilder.DropIndex(
                name: "IX_Travellers_PreferredLanguageId",
                schema: "Travellers",
                table: "Travellers");

            migrationBuilder.DropColumn(
                name: "PreferredLanguageId",
                schema: "Travellers",
                table: "Travellers");
        }
    }
}
