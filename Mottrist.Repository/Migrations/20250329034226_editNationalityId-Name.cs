using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mottrist.Repository.Migrations
{
    /// <inheritdoc />
    public partial class editNationalityIdName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Countries_NationailtyId",
                schema: "Drivers",
                table: "Drivers");

            migrationBuilder.RenameColumn(
                name: "NationailtyId",
                schema: "Drivers",
                table: "Drivers",
                newName: "NationalityId");

            migrationBuilder.RenameIndex(
                name: "IX_Drivers_NationailtyId",
                schema: "Drivers",
                table: "Drivers",
                newName: "IX_Drivers_NationalityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Countries_NationalityId",
                schema: "Drivers",
                table: "Drivers",
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
                name: "FK_Drivers_Countries_NationalityId",
                schema: "Drivers",
                table: "Drivers");

            migrationBuilder.RenameColumn(
                name: "NationalityId",
                schema: "Drivers",
                table: "Drivers",
                newName: "NationailtyId");

            migrationBuilder.RenameIndex(
                name: "IX_Drivers_NationalityId",
                schema: "Drivers",
                table: "Drivers",
                newName: "IX_Drivers_NationailtyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Countries_NationailtyId",
                schema: "Drivers",
                table: "Drivers",
                column: "NationailtyId",
                principalSchema: "Geography",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
