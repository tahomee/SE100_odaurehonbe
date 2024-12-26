using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace odaurehonbe.Migrations
{
    /// <inheritdoc />
    public partial class PromotionUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "PaymentId",
                table: "Tickets",
                newName: "PaymentID");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentID",
                table: "Tickets",
                type: "integer",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PromotionId",
                table: "Payments",
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
                name: "PromotionId",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "PaymentID",
                table: "Tickets",
                newName: "PaymentId");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentId",
                table: "Tickets",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

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
    }
}
