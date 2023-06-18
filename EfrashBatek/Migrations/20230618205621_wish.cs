using Microsoft.EntityFrameworkCore.Migrations;

namespace EfrashBatek.Migrations
{
    public partial class wish : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WishLists_CustomerID",
                table: "WishLists");

            migrationBuilder.AddColumn<int>(
                name: "WishListId",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_WishLists_CustomerID",
                table: "WishLists",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_WishListId",
                table: "Customers",
                column: "WishListId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_WishLists_WishListId",
                table: "Customers",
                column: "WishListId",
                principalTable: "WishLists",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_WishLists_WishListId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_WishLists_CustomerID",
                table: "WishLists");

            migrationBuilder.DropIndex(
                name: "IX_Customers_WishListId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "WishListId",
                table: "Customers");

            migrationBuilder.CreateIndex(
                name: "IX_WishLists_CustomerID",
                table: "WishLists",
                column: "CustomerID",
                unique: true);
        }
    }
}
