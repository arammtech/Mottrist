using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mottrist.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddDriverAvailabilityAndPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AvailableAllTime",
                schema: "Drivers",
                table: "Drivers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateOnly>(
                name: "AvailableFrom",
                schema: "Drivers",
                table: "Drivers",
                type: "DATE",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "AvailableTo",
                schema: "Drivers",
                table: "Drivers",
                type: "DATE",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerHour",
                schema: "Drivers",
                table: "Drivers",
                type: "decimal(10,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableAllTime",
                schema: "Drivers",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "AvailableFrom",
                schema: "Drivers",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "AvailableTo",
                schema: "Drivers",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "PricePerHour",
                schema: "Drivers",
                table: "Drivers");
        }
    }
}
