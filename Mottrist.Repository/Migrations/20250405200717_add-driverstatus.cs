using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mottrist.Repository.Migrations
{
    /// <inheritdoc />
    public partial class adddriverstatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Status",
                schema: "Drivers",
                table: "Drivers",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                schema: "Drivers",
                table: "Drivers");
        }
    }
}
