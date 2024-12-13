using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace odaurehonbe.Migrations
{
    /// <inheritdoc />
    public partial class AddSeatsAndPriceToBus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PricePerSeat",
                table: "Buses",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "SeatsAvailable",
                table: "Buses",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PricePerSeat",
                table: "Buses");

            migrationBuilder.DropColumn(
                name: "SeatsAvailable",
                table: "Buses");
        }
    }
}
