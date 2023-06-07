using Microsoft.EntityFrameworkCore.Migrations;

namespace EfrashBatek.Migrations
{
    public partial class newAlyaa2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Orders_OrderId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Items_Warrantl_Requests_Warrantly_RequestID",
                table: "Order_Items");

            migrationBuilder.DropIndex(
                name: "IX_Order_Items_Warrantly_RequestID",
                table: "Order_Items");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_OrderId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Warrantly_RequestID",
                table: "Order_Items");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Addresses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Warrantly_RequestID",
                table: "Order_Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Addresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Order_Items_Warrantly_RequestID",
                table: "Order_Items",
                column: "Warrantly_RequestID");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_OrderId",
                table: "Addresses",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Orders_OrderId",
                table: "Addresses",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict );

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Items_Warrantl_Requests_Warrantly_RequestID",
                table: "Order_Items",
                column: "Warrantly_RequestID",
                principalTable: "Warrantl_Requests",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict );
        }
    }
}
