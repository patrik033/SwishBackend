using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwishBackend.Orders.Migrations
{
    /// <inheritdoc />
    public partial class updatedNaming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_CartOrders_ShoppingCartOrderId",
                table: "OrderItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItems",
                table: "OrderItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartOrders",
                table: "CartOrders");

            migrationBuilder.RenameTable(
                name: "OrderItems",
                newName: "ShoppingCartItems");

            migrationBuilder.RenameTable(
                name: "CartOrders",
                newName: "ShoppingCartOrders");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_ShoppingCartOrderId",
                table: "ShoppingCartItems",
                newName: "IX_ShoppingCartItems_ShoppingCartOrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingCartItems",
                table: "ShoppingCartItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingCartOrders",
                table: "ShoppingCartOrders",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_ShoppingCartOrders_ShoppingCartOrderId",
                table: "ShoppingCartItems",
                column: "ShoppingCartOrderId",
                principalTable: "ShoppingCartOrders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_ShoppingCartOrders_ShoppingCartOrderId",
                table: "ShoppingCartItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingCartOrders",
                table: "ShoppingCartOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingCartItems",
                table: "ShoppingCartItems");

            migrationBuilder.RenameTable(
                name: "ShoppingCartOrders",
                newName: "CartOrders");

            migrationBuilder.RenameTable(
                name: "ShoppingCartItems",
                newName: "OrderItems");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCartItems_ShoppingCartOrderId",
                table: "OrderItems",
                newName: "IX_OrderItems_ShoppingCartOrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartOrders",
                table: "CartOrders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItems",
                table: "OrderItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_CartOrders_ShoppingCartOrderId",
                table: "OrderItems",
                column: "ShoppingCartOrderId",
                principalTable: "CartOrders",
                principalColumn: "Id");
        }
    }
}
