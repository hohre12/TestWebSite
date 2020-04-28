using Microsoft.EntityFrameworkCore.Migrations;

namespace TestWebSIte.Migrations
{
    public partial class _14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inquires",
                columns: table => new
                {
                    InquireNo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Questions = table.Column<string>(nullable: true),
                    CategoryChoice = table.Column<string>(nullable: true),
                    UserNo = table.Column<int>(nullable: false),
                    QuestionYear = table.Column<string>(nullable: true),
                    QuestionMonth = table.Column<string>(nullable: true),
                    QuestionDay = table.Column<string>(nullable: true),
                    QuestionTime = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inquires", x => x.InquireNo);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inquires");
        }
    }
}
