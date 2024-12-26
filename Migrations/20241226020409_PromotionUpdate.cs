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
            migrationBuilder.AddColumn<int>(
                name: "PromotionPromoID",
                table: "Promotions",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PromotionPromoID",
                table: "Payments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Promotions_PromotionPromoID",
                table: "Promotions",
                column: "PromotionPromoID");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PromotionPromoID",
                table: "Payments",
                column: "PromotionPromoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Promotions_PromotionPromoID",
                table: "Payments",
                column: "PromotionPromoID",
                principalTable: "Promotions",
                principalColumn: "PromoID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Promotions_Promotions_PromotionPromoID",
                table: "Promotions",
                column: "PromotionPromoID",
                principalTable: "Promotions",
                principalColumn: "PromoID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Promotions_PromotionPromoID",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Promotions_Promotions_PromotionPromoID",
                table: "Promotions");

            migrationBuilder.DropIndex(
                name: "IX_Promotions_PromotionPromoID",
                table: "Promotions");

            migrationBuilder.DropIndex(
                name: "IX_Payments_PromotionPromoID",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PromotionPromoID",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "PromotionPromoID",
                table: "Payments");
        }
    }
}
