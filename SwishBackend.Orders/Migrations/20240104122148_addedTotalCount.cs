using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwishBackend.Orders.Migrations
{
    /// <inheritdoc />
    public partial class addedTotalCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalCount",
                table: "ShoppingCartOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalCount",
                table: "ShoppingCartOrders");
        }
    }
}
