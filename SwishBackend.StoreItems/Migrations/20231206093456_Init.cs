using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SwishBackend.StoreItems.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: true),
                    ProductCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    HorsePower = table.Column<int>(type: "int", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PainterName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_ProductCategories_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "Id", "CategoryName" },
                values: new object[,]
                {
                    { new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), "Painting" },
                    { new Guid("bb2ea796-c92a-4a02-8857-0763f7f4e80a"), "Cars" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Discriminator", "Image", "Name", "PainterName", "Price", "ProductCategoryId", "Stock", "Type" },
                values: new object[] { new Guid("2404e29b-09e2-405a-a58b-2862ecb85e09"), "Painting", "https://images.pexels.com/photos/1266808/pexels-photo-1266808.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2", "Test Painging 5", "Test 5", 200m, new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), 12, 0 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Discriminator", "HorsePower", "Image", "Model", "Name", "Price", "ProductCategoryId", "Stock", "Type" },
                values: new object[] { new Guid("31662f6f-dbab-4ef3-9f6f-2a8cb7cac813"), "Car", 150, "car_image_2.jpg", "Model Y", "Car 2", 25000m, new Guid("bb2ea796-c92a-4a02-8857-0763f7f4e80a"), 8, 1 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Discriminator", "Image", "Name", "PainterName", "Price", "ProductCategoryId", "Stock", "Type" },
                values: new object[,]
                {
                    { new Guid("7861614d-4ce3-405c-9cc1-399158680d74"), "Painting", "https://images.pexels.com/photos/1266808/pexels-photo-1266808.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2", "Test Painging 1", "Test 1", 100m, new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), 10, 0 },
                    { new Guid("907703df-5699-44c9-9c9c-ec6e25b5c59e"), "Painting", "https://images.pexels.com/photos/1266808/pexels-photo-1266808.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2", "Test Painging 3", "Test 3", 120m, new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), 12, 0 },
                    { new Guid("936a4000-f78f-4095-82e3-300c7831a5d8"), "Painting", "https://images.pexels.com/photos/1266808/pexels-photo-1266808.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2", "Test Painging 2", "Test 2", 110m, new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), 16, 0 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Discriminator", "HorsePower", "Image", "Model", "Name", "Price", "ProductCategoryId", "Stock", "Type" },
                values: new object[] { new Guid("9d037afe-1993-44f6-b46e-1617e39ee50c"), "Car", 200, "car_image_1.jpg", "Model X", "Car 1", 30000m, new Guid("bb2ea796-c92a-4a02-8857-0763f7f4e80a"), 5, 1 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Discriminator", "Image", "Name", "PainterName", "Price", "ProductCategoryId", "Stock", "Type" },
                values: new object[,]
                {
                    { new Guid("a93a6587-04d8-4659-8971-5de664076b68"), "Painting", "https://images.pexels.com/photos/1266808/pexels-photo-1266808.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2", "Test Painging 6", "Test 6", 100m, new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), 10, 0 },
                    { new Guid("c587af33-4262-4b28-bffe-b65ebd571b5b"), "Painting", "https://images.pexels.com/photos/1266808/pexels-photo-1266808.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2", "Test Painging 4", "Test 4", 100m, new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), 5, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductCategoryId",
                table: "Products",
                column: "ProductCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ProductCategories");
        }
    }
}
