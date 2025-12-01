using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkApp.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParkingSpaces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpaceNumber = table.Column<int>(type: "int", nullable: false),
                    VehicleReg = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TimeIn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VehicleType = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingSpaces", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSpaces_SpaceNumber_VehicleReg",
                table: "ParkingSpaces",
                columns: new[] { "SpaceNumber", "VehicleReg" },
                unique: true,
                filter: "[VehicleReg] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParkingSpaces");
        }
    }
}
