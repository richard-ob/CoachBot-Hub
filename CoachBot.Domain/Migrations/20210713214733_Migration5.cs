using Microsoft.EntityFrameworkCore.Migrations;

namespace CoachBot.Domain.Migrations
{
    public partial class Migration5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MatchStatistics_AwayGoals",
                table: "MatchStatistics",
                column: "AwayGoals");

            migrationBuilder.CreateIndex(
                name: "IX_MatchStatistics_HomeGoals",
                table: "MatchStatistics",
                column: "HomeGoals");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MatchStatistics_AwayGoals",
                table: "MatchStatistics");

            migrationBuilder.DropIndex(
                name: "IX_MatchStatistics_HomeGoals",
                table: "MatchStatistics");
        }
    }
}
