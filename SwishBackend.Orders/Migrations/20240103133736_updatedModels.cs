using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwishBackend.Orders.Migrations
{
    /// <inheritdoc />
    public partial class updatedModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_CartOrders_CartOrderId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_OrderProducts_OrderId",
                table: "OrderItems");

            migrationBuilder.DropTable(
                name: "OrderProducts");

            migrationBuilder.DropTable(
                name: "OrderProductCategories");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "OrderItems",
                newName: "Stock");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "OrderItems",
                newName: "ProductCategoryId");

            migrationBuilder.RenameColumn(
                name: "CartOrderId",
                table: "OrderItems",
                newName: "ShoppingCartOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_CartOrderId",
                table: "OrderItems",
                newName: "IX_OrderItems_ShoppingCartOrderId");

            migrationBuilder.AddColumn<string>(
                name: "CategoryName",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "OrderedQuantity",
                table: "OrderItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "OrderItems",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "CartOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "HasBeenCheckedOut",
                table: "CartOrders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderTime",
                table: "CartOrders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "CartOrders",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_CartOrders_ShoppingCartOrderId",
                table: "OrderItems",
                column: "ShoppingCartOrderId",
                principalTable: "CartOrders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_CartOrders_ShoppingCartOrderId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "CategoryName",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "OrderedQuantity",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "CartOrders");

            migrationBuilder.DropColumn(
                name: "HasBeenCheckedOut",
                table: "CartOrders");

            migrationBuilder.DropColumn(
                name: "OrderTime",
                table: "CartOrders");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "CartOrders");

            migrationBuilder.RenameColumn(
                name: "Stock",
                table: "OrderItems",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "ShoppingCartOrderId",
                table: "OrderItems",
                newName: "CartOrderId");

            migrationBuilder.RenameColumn(
                name: "ProductCategoryId",
                table: "OrderItems",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_ShoppingCartOrderId",
                table: "OrderItems",
                newName: "IX_OrderItems_CartOrderId");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "OrderItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "OrderProductCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProductCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderProductCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderProducts_OrderProductCategories_OrderProductCategoryId",
                        column: x => x.OrderProductCategoryId,
                        principalTable: "OrderProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_OrderProductCategoryId",
                table: "OrderProducts",
                column: "OrderProductCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_CartOrders_CartOrderId",
                table: "OrderItems",
                column: "CartOrderId",
                principalTable: "CartOrders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_OrderProducts_OrderId",
                table: "OrderItems",
                column: "OrderId",
                principalTable: "OrderProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
