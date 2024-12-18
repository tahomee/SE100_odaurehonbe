using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace odaurehonbe.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seat_Buses_BusID",
                table: "Seat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Seat",
                table: "Seat");

            migrationBuilder.RenameTable(
                name: "Seat",
                newName: "Seats");

            migrationBuilder.RenameIndex(
                name: "IX_Seat_BusID",
                table: "Seats",
                newName: "IX_Seats_BusID");

            migrationBuilder.AlterColumn<string>(
                name: "Price",
                table: "Seats",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Seats",
                table: "Seats",
                column: "SeatID");

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Buses_BusID",
                table: "Seats",
                column: "BusID",
                principalTable: "Buses",
                principalColumn: "BusID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Buses_BusID",
                table: "Seats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Seats",
                table: "Seats");

            migrationBuilder.RenameTable(
                name: "Seats",
                newName: "Seat");

            migrationBuilder.RenameIndex(
                name: "IX_Seats_BusID",
                table: "Seat",
                newName: "IX_Seat_BusID");

            migrationBuilder.AlterColumn<string>(
                name: "Price",
                table: "Seat",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Seat",
                table: "Seat",
                column: "SeatID");

            migrationBuilder.AddForeignKey(
                name: "FK_Seat_Buses_BusID",
                table: "Seat",
                column: "BusID",
                principalTable: "Buses",
                principalColumn: "BusID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
