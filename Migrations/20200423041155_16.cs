using Microsoft.EntityFrameworkCore.Migrations;

namespace TestWebSIte.Migrations
{
    public partial class _16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    AnswerNo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InquireNo = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Questions = table.Column<string>(nullable: true),
                    CategoryChoice = table.Column<string>(nullable: true),
                    UserNo = table.Column<int>(nullable: false),
                    QuestionYear = table.Column<string>(nullable: true),
                    QuestionMonth = table.Column<string>(nullable: true),
                    QuestionDay = table.Column<string>(nullable: true),
                    QuestionTime = table.Column<string>(nullable: true),
                    AnswerContent = table.Column<string>(nullable: true),
                    AnswerDay = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.AnswerNo);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");
        }
    }
}
