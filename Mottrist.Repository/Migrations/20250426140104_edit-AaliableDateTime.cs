using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mottrist.Repository.Migrations
{
    /// <inheritdoc />
    public partial class editAaliableDateTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableAllTime",
                schema: "Drivers",
                table: "Drivers");

            migrationBuilder.AlterColumn<decimal>(
                name: "PricePerHour",
                schema: "Drivers",
                table: "Drivers",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AvailableTo",
                schema: "Drivers",
                table: "Drivers",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "DATE",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AvailableFrom",
                schema: "Drivers",
                table: "Drivers",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "DATE",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailableAllTime",
                schema: "Drivers",
                table: "Drivers",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailableAllTime",
                schema: "Drivers",
                table: "Drivers");

            migrationBuilder.AlterColumn<decimal>(
                name: "PricePerHour",
                schema: "Drivers",
                table: "Drivers",
                type: "decimal(10,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "AvailableTo",
                schema: "Drivers",
                table: "Drivers",
                type: "DATE",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "AvailableFrom",
                schema: "Drivers",
                table: "Drivers",
                type: "DATE",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AvailableAllTime",
                schema: "Drivers",
                table: "Drivers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
