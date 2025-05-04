using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mottrist.Repository.Migrations
{
    /// <inheritdoc />
    public partial class addCommentsForEnumsInDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "Status",
                schema: "Drivers",
                table: "Drivers",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)2,
                comment: "Stores the status of the driver: Approved = 1, Pending = 2, or Rejected = 3.",
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldDefaultValue: (byte)2);

            migrationBuilder.AlterColumn<byte>(
                name: "WorkStatus",
                schema: "Drivers",
                table: "DriverCountries",
                type: "tinyint",
                nullable: false,
                comment: "Stores the work status of the driver in the country: WorkedOn = 1, CoverNow = 2.",
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AlterColumn<byte>(
                name: "WorkStatus",
                schema: "Drivers",
                table: "DriverCities",
                type: "tinyint",
                nullable: false,
                comment: "Stores the work status of the driver in the city: WorkedOn = 1, CoverNow = 2.",
                oldClrType: typeof(byte),
                oldType: "tinyint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "Status",
                schema: "Drivers",
                table: "Drivers",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)2,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldDefaultValue: (byte)2,
                oldComment: "Stores the status of the driver: Approved = 1, Pending = 2, or Rejected = 3.");

            migrationBuilder.AlterColumn<byte>(
                name: "WorkStatus",
                schema: "Drivers",
                table: "DriverCountries",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldComment: "Stores the work status of the driver in the country: WorkedOn = 1, CoverNow = 2.");

            migrationBuilder.AlterColumn<byte>(
                name: "WorkStatus",
                schema: "Drivers",
                table: "DriverCities",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldComment: "Stores the work status of the driver in the city: WorkedOn = 1, CoverNow = 2.");
        }
    }
}
