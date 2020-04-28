using Microsoft.EntityFrameworkCore.Migrations;

namespace TestWebSIte.Migrations
{
    public partial class APO5Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovedOrders_Orders_OrderNo",
                table: "ApprovedOrders");

            migrationBuilder.DropIndex(
                name: "IX_ApprovedOrders_OrderNo",
                table: "ApprovedOrders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ApprovedOrders_OrderNo",
                table: "ApprovedOrders",
                column: "OrderNo");

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovedOrders_Orders_OrderNo",
                table: "ApprovedOrders",
                column: "OrderNo",
                principalTable: "Orders",
                principalColumn: "OrderNo",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
