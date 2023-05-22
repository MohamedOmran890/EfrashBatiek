using Microsoft.EntityFrameworkCore.Migrations;

namespace EfrashBatek.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Users_CustomerID",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Customs_Users_CustomerID",
                table: "Customs");

            migrationBuilder.DropForeignKey(
                name: "FK_Designs_Users_DesignerID",
                table: "Designs");

            migrationBuilder.DropForeignKey(
                name: "FK_feedbacks_Users_OrderItemID",
                table: "feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_CustomerID",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Shops_ShopID",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_WishLists_Users_CustomerID",
                table: "WishLists");

            migrationBuilder.DropIndex(
                name: "IX_Users_ShopID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "NationalCardImage",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ShopID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "WishListId",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    WishListId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Customer_Users_ID",
                        column: x => x.ID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Designer",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    NationalCardImage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designer", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Designer_Users_ID",
                        column: x => x.ID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    ShopID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Staff_Shops_ShopID",
                        column: x => x.ShopID,
                        principalTable: "Shops",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Staff_Users_ID",
                        column: x => x.ID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Staff_ShopID",
                table: "Staff",
                column: "ShopID");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Customer_CustomerID",
                table: "Carts",
                column: "CustomerID",
                principalTable: "Customer",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customs_Customer_CustomerID",
                table: "Customs",
                column: "CustomerID",
                principalTable: "Customer",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Designs_Designer_DesignerID",
                table: "Designs",
                column: "DesignerID",
                principalTable: "Designer",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_feedbacks_Customer_OrderItemID",
                table: "feedbacks",
                column: "OrderItemID",
                principalTable: "Customer",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customer_CustomerID",
                table: "Orders",
                column: "CustomerID",
                principalTable: "Customer",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WishLists_Customer_CustomerID",
                table: "WishLists",
                column: "CustomerID",
                principalTable: "Customer",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Customer_CustomerID",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Customs_Customer_CustomerID",
                table: "Customs");

            migrationBuilder.DropForeignKey(
                name: "FK_Designs_Designer_DesignerID",
                table: "Designs");

            migrationBuilder.DropForeignKey(
                name: "FK_feedbacks_Customer_OrderItemID",
                table: "feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customer_CustomerID",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_WishLists_Customer_CustomerID",
                table: "WishLists");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Designer");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NationalCardImage",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShopID",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WishListId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ShopID",
                table: "Users",
                column: "ShopID");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Users_CustomerID",
                table: "Carts",
                column: "CustomerID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customs_Users_CustomerID",
                table: "Customs",
                column: "CustomerID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Designs_Users_DesignerID",
                table: "Designs",
                column: "DesignerID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_feedbacks_Users_OrderItemID",
                table: "feedbacks",
                column: "OrderItemID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_CustomerID",
                table: "Orders",
                column: "CustomerID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Shops_ShopID",
                table: "Users",
                column: "ShopID",
                principalTable: "Shops",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WishLists_Users_CustomerID",
                table: "WishLists",
                column: "CustomerID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
