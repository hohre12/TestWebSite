using Microsoft.EntityFrameworkCore.Migrations;

namespace TestWebSIte.Migrations
{
    public partial class _12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SignUpTime",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "SignUpDay",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SignUpMonth",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SignUpYear",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SignUpDay",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SignUpMonth",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SignUpYear",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "SignUpTime",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
