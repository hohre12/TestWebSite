using Microsoft.EntityFrameworkCore.Migrations;

namespace TestWebSIte.Migrations
{
    public partial class _11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderTime",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "OrderDay",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrderMonth",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrderYear",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderDay",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderMonth",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderYear",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "OrderTime",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
