using Microsoft.EntityFrameworkCore.Migrations;

namespace CoachBot.Domain.Migrations
{
    public partial class Migration4_PlayerOfTheMatch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlayerOfTheMatchId",
                table: "Matches",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Matches_PlayerOfTheMatchId",
                table: "Matches",
                column: "PlayerOfTheMatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Players_PlayerOfTheMatchId",
                table: "Matches",
                column: "PlayerOfTheMatchId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Players_PlayerOfTheMatchId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_PlayerOfTheMatchId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "PlayerOfTheMatchId",
                table: "Matches");
        }
    }
}
