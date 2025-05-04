using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mottrist.Repository.Migrations
{
    /// <inheritdoc />
    public partial class ApplySoftDeleteablOnDriverAndTravelerTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateDeleted",
                schema: "Travellers",
                table: "Travellers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Travellers",
                table: "Travellers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateDeleted",
                schema: "Drivers",
                table: "Drivers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Drivers",
                table: "Drivers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateDeleted",
                schema: "Travellers",
                table: "Travellers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Travellers",
                table: "Travellers");

            migrationBuilder.DropColumn(
                name: "DateDeleted",
                schema: "Drivers",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Drivers",
                table: "Drivers");
        }
    }
}
