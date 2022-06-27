using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RLH.Plume.Migrations
{
    public partial class removeFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_Positions_PositionTimestamp",
                table: "Measurements");

            migrationBuilder.DropIndex(
                name: "IX_Measurements_PositionTimestamp",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "PositionTimestamp",
                table: "Measurements");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PositionTimestamp",
                table: "Measurements",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_PositionTimestamp",
                table: "Measurements",
                column: "PositionTimestamp");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_Positions_PositionTimestamp",
                table: "Measurements",
                column: "PositionTimestamp",
                principalTable: "Positions",
                principalColumn: "Timestamp",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
