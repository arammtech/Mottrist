using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mottrist.Repository.Migrations
{
    /// <inheritdoc />
    public partial class fixspellingMistaketraveler : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Travellers_Countries_NationailtyId",
                schema: "Travellers",
                table: "Travellers");

            migrationBuilder.RenameColumn(
                name: "NationailtyId",
                schema: "Travellers",
                table: "Travellers",
                newName: "NationalityId");

            migrationBuilder.RenameIndex(
                name: "IX_Travellers_NationailtyId",
                schema: "Travellers",
                table: "Travellers",
                newName: "IX_Travellers_NationalityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Travellers_Countries_NationalityId",
                schema: "Travellers",
                table: "Travellers",
                column: "NationalityId",
                principalSchema: "Geography",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Travellers_Countries_NationalityId",
                schema: "Travellers",
                table: "Travellers");

            migrationBuilder.RenameColumn(
                name: "NationalityId",
                schema: "Travellers",
                table: "Travellers",
                newName: "NationailtyId");

            migrationBuilder.RenameIndex(
                name: "IX_Travellers_NationalityId",
                schema: "Travellers",
                table: "Travellers",
                newName: "IX_Travellers_NationailtyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Travellers_Countries_NationailtyId",
                schema: "Travellers",
                table: "Travellers",
                column: "NationailtyId",
                principalSchema: "Geography",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
