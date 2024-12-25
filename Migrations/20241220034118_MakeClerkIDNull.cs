using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace odaurehonbe.Migrations
{
    /// <inheritdoc />
    public partial class MakeClerkIDNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_TicketClerks_ClerkID",
                table: "Notifications");

            migrationBuilder.AlterColumn<int>(
                name: "ClerkID",
                table: "Notifications",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_TicketClerks_ClerkID",
                table: "Notifications",
                column: "ClerkID",
                principalTable: "TicketClerks",
                principalColumn: "AccountID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_TicketClerks_ClerkID",
                table: "Notifications");

            migrationBuilder.AlterColumn<int>(
                name: "ClerkID",
                table: "Notifications",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_TicketClerks_ClerkID",
                table: "Notifications",
                column: "ClerkID",
                principalTable: "TicketClerks",
                principalColumn: "AccountID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
