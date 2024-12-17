using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace odaurehonbe.Migrations
{
    /// <inheritdoc />
    public partial class MigrationUpdtated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Seats");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Price",
                table: "Seats",
                type: "text",
                nullable: true);
        }
    }
}
