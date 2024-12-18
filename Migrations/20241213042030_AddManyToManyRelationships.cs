using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace odaurehonbe.Migrations
{
    /// <inheritdoc />
    public partial class AddManyToManyRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusRoutes_Buses_BusID",
                table: "BusRoutes");

            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Buses_BusID",
                table: "Drivers");

            migrationBuilder.DropIndex(
                name: "IX_Drivers_BusID",
                table: "Drivers");

            migrationBuilder.DropIndex(
                name: "IX_BusRoutes_BusID",
                table: "BusRoutes");

            migrationBuilder.DropColumn(
                name: "BusID",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "BusID",
                table: "BusRoutes");

            migrationBuilder.CreateTable(
                name: "BusBusRoutes",
                columns: table => new
                {
                    BusBusRouteID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BusID = table.Column<int>(type: "integer", nullable: false),
                    BusRouteID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusBusRoutes", x => x.BusBusRouteID);
                    table.ForeignKey(
                        name: "FK_BusBusRoutes_BusRoutes_BusRouteID",
                        column: x => x.BusRouteID,
                        principalTable: "BusRoutes",
                        principalColumn: "BusRouteID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusBusRoutes_Buses_BusID",
                        column: x => x.BusID,
                        principalTable: "Buses",
                        principalColumn: "BusID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusDrivers",
                columns: table => new
                {
                    BusDriverID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BusID = table.Column<int>(type: "integer", nullable: false),
                    DriverID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusDrivers", x => x.BusDriverID);
                    table.ForeignKey(
                        name: "FK_BusDrivers_Buses_BusID",
                        column: x => x.BusID,
                        principalTable: "Buses",
                        principalColumn: "BusID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusDrivers_Drivers_DriverID",
                        column: x => x.DriverID,
                        principalTable: "Drivers",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusBusRoutes_BusID",
                table: "BusBusRoutes",
                column: "BusID");

            migrationBuilder.CreateIndex(
                name: "IX_BusBusRoutes_BusRouteID",
                table: "BusBusRoutes",
                column: "BusRouteID");

            migrationBuilder.CreateIndex(
                name: "IX_BusDrivers_BusID",
                table: "BusDrivers",
                column: "BusID");

            migrationBuilder.CreateIndex(
                name: "IX_BusDrivers_DriverID",
                table: "BusDrivers",
                column: "DriverID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusBusRoutes");

            migrationBuilder.DropTable(
                name: "BusDrivers");

            migrationBuilder.AddColumn<int>(
                name: "BusID",
                table: "Drivers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BusID",
                table: "BusRoutes",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_BusID",
                table: "Drivers",
                column: "BusID");

            migrationBuilder.CreateIndex(
                name: "IX_BusRoutes_BusID",
                table: "BusRoutes",
                column: "BusID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusRoutes_Buses_BusID",
                table: "BusRoutes",
                column: "BusID",
                principalTable: "Buses",
                principalColumn: "BusID");

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Buses_BusID",
                table: "Drivers",
                column: "BusID",
                principalTable: "Buses",
                principalColumn: "BusID");
        }
    }
}
