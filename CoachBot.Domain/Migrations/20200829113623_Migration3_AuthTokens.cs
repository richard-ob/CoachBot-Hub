using Microsoft.EntityFrameworkCore.Migrations;

namespace CoachBot.Domain.Migrations
{
    public partial class Migration3_AuthTokens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TeamCode",
                table: "Teams",
                maxLength: 8,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 6,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthorizationToken",
                table: "Regions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorizationToken",
                table: "Regions");

            migrationBuilder.AlterColumn<string>(
                name: "TeamCode",
                table: "Teams",
                maxLength: 6,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 8,
                oldNullable: true);
        }
    }
}
