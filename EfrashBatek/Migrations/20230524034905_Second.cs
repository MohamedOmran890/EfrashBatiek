using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EfrashBatek.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishLists_Items_ItemID",
                table: "WishLists");

            migrationBuilder.DropIndex(
                name: "IX_WishLists_ItemID",
                table: "WishLists");

            migrationBuilder.DropColumn(
                name: "ItemID",
                table: "WishLists");

            migrationBuilder.AddColumn<int>(
                name: "WishListID",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LoginViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RememberMe = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginViewModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegisterViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConfirmPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Birthdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisterViewModel", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_WishListID",
                table: "Items",
                column: "WishListID");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_WishLists_WishListID",
                table: "Items",
                column: "WishListID",
                principalTable: "WishLists",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_WishLists_WishListID",
                table: "Items");

            migrationBuilder.DropTable(
                name: "LoginViewModel");

            migrationBuilder.DropTable(
                name: "RegisterViewModel");

            migrationBuilder.DropIndex(
                name: "IX_Items_WishListID",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "WishListID",
                table: "Items");

            migrationBuilder.AddColumn<int>(
                name: "ItemID",
                table: "WishLists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_WishLists_ItemID",
                table: "WishLists",
                column: "ItemID");

            migrationBuilder.AddForeignKey(
                name: "FK_WishLists_Items_ItemID",
                table: "WishLists",
                column: "ItemID",
                principalTable: "Items",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
