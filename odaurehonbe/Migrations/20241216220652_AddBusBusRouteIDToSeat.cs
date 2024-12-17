using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace odaurehonbe.Migrations
{
    /// <inheritdoc />
    public partial class AddBusBusRouteIDToSeat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Buses_BusID",
                table: "Seats");

            migrationBuilder.AlterColumn<int>(
                name: "BusID",
                table: "Seats",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "BusBusRouteID",
                table: "Seats",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Seats_BusBusRouteID",
                table: "Seats",
                column: "BusBusRouteID");

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_BusBusRoutes_BusBusRouteID",
                table: "Seats",
                column: "BusBusRouteID",
                principalTable: "BusBusRoutes",
                principalColumn: "BusBusRouteID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Buses_BusID",
                table: "Seats",
                column: "BusID",
                principalTable: "Buses",
                principalColumn: "BusID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seats_BusBusRoutes_BusBusRouteID",
                table: "Seats");

            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Buses_BusID",
                table: "Seats");

            migrationBuilder.DropIndex(
                name: "IX_Seats_BusBusRouteID",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "BusBusRouteID",
                table: "Seats");

            migrationBuilder.AlterColumn<int>(
                name: "BusID",
                table: "Seats",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Buses_BusID",
                table: "Seats",
                column: "BusID",
                principalTable: "Buses",
                principalColumn: "BusID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
