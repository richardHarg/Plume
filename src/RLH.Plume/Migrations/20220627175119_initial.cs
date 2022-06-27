using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RLH.Plume.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Timestamp = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    latitude = table.Column<double>(type: "float", nullable: false),
                    longitude = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Timestamp);
                });

            migrationBuilder.CreateTable(
                name: "Measurements",
                columns: table => new
                {
                    Timestamp = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateTimeUTC = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    NO2 = table.Column<double>(type: "float", nullable: false),
                    VOC = table.Column<double>(type: "float", nullable: false),
                    PM01 = table.Column<double>(type: "float", nullable: false),
                    PM10 = table.Column<double>(type: "float", nullable: false),
                    PM25 = table.Column<double>(type: "float", nullable: false),
                    NO2_AQI = table.Column<double>(type: "float", nullable: false),
                    VOC_AQI = table.Column<double>(type: "float", nullable: false),
                    PM01_AQI = table.Column<double>(type: "float", nullable: false),
                    PM10_AQI = table.Column<double>(type: "float", nullable: false),
                    PM25_AQI = table.Column<double>(type: "float", nullable: false),
                    PositionTimestamp = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measurements", x => x.Timestamp);
                    table.ForeignKey(
                        name: "FK_Measurements_Positions_PositionTimestamp",
                        column: x => x.PositionTimestamp,
                        principalTable: "Positions",
                        principalColumn: "Timestamp",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_PositionTimestamp",
                table: "Measurements",
                column: "PositionTimestamp");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Measurements");

            migrationBuilder.DropTable(
                name: "Positions");
        }
    }
}
