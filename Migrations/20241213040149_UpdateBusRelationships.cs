using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace odaurehonbe.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBusRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArrivalTime",
                table: "Buses");

            migrationBuilder.DropColumn(
                name: "DepartTime",
                table: "Buses");

            migrationBuilder.DropColumn(
                name: "DriverID",
                table: "Buses");

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

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Buses",
                type: "text",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Buses");

            migrationBuilder.AddColumn<DateTime>(
                name: "ArrivalTime",
                table: "Buses",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DepartTime",
                table: "Buses",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DriverID",
                table: "Buses",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
