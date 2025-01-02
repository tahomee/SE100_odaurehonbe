using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace odaurehonbe.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PricePerSeat",
                table: "Buses");

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerSeat",
                table: "BusRoutes",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PricePerSeat",
                table: "BusRoutes");

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerSeat",
                table: "Buses",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
