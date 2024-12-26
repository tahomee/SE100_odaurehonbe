using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace odaurehonbe.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TicketID",
                table: "Payments");

            migrationBuilder.AddColumn<int>(
                name: "PaymentID",
                table: "Tickets",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_PaymentID",
                table: "Tickets",
                column: "PaymentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Payments_PaymentID",
                table: "Tickets",
                column: "PaymentID",
                principalTable: "Payments",
                principalColumn: "PaymentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Payments_PaymentID",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_PaymentID",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "PaymentID",
                table: "Tickets");

            migrationBuilder.AddColumn<int>(
                name: "TicketID",
                table: "Payments",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
