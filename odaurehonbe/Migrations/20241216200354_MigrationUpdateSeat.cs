using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace odaurehonbe.Migrations
{
    /// <inheritdoc />
    public partial class MigrationUpdateSeat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Price",
                table: "Seat",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Seat");
        }
    }
}
