using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace odaurehonbe.Migrations
{
    /// <inheritdoc />
    public partial class BusBusRoute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Buses_BusID",
                table: "Seats");

            migrationBuilder.DropIndex(
                name: "IX_Seats_BusID",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "BusID",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "SeatsAvailable",
                table: "Buses");

            migrationBuilder.RenameColumn(
                name: "BusID",
                table: "Tickets",
                newName: "BusBusRouteID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BusBusRouteID",
                table: "Tickets",
                newName: "BusID");

            migrationBuilder.AddColumn<int>(
                name: "BusID",
                table: "Seats",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SeatsAvailable",
                table: "Buses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Seats_BusID",
                table: "Seats",
                column: "BusID");

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Buses_BusID",
                table: "Seats",
                column: "BusID",
                principalTable: "Buses",
                principalColumn: "BusID");
        }
    }
}
