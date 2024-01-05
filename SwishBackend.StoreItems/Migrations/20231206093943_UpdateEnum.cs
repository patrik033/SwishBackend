using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SwishBackend.StoreItems.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("2404e29b-09e2-405a-a58b-2862ecb85e09"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("31662f6f-dbab-4ef3-9f6f-2a8cb7cac813"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("7861614d-4ce3-405c-9cc1-399158680d74"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("907703df-5699-44c9-9c9c-ec6e25b5c59e"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("936a4000-f78f-4095-82e3-300c7831a5d8"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("9d037afe-1993-44f6-b46e-1617e39ee50c"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a93a6587-04d8-4659-8971-5de664076b68"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c587af33-4262-4b28-bffe-b65ebd571b5b"));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Discriminator", "Image", "Name", "PainterName", "Price", "ProductCategoryId", "Stock", "Type" },
                values: new object[,]
                {
                    { new Guid("13720c78-0503-45f9-b72d-6d55a07af9ed"), "Painting", "https://images.pexels.com/photos/1266808/pexels-photo-1266808.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2", "Test Painging 5", "Test 5", 200m, new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), 12, 0 },
                    { new Guid("58e36234-0579-4d71-99d1-3d73810bf1b3"), "Painting", "https://images.pexels.com/photos/1266808/pexels-photo-1266808.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2", "Test Painging 2", "Test 2", 110m, new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), 16, 0 },
                    { new Guid("59c7b4e5-791b-46af-aba8-6cc682dbd26d"), "Painting", "https://images.pexels.com/photos/1266808/pexels-photo-1266808.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2", "Test Painging 1", "Test 1", 100m, new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), 10, 0 },
                    { new Guid("6c1f4831-4f3d-413a-b38a-0c9173d0892b"), "Painting", "https://images.pexels.com/photos/1266808/pexels-photo-1266808.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2", "Test Painging 3", "Test 3", 120m, new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), 12, 0 },
                    { new Guid("726fb132-c015-4e23-95ba-62d38c9941b8"), "Painting", "https://images.pexels.com/photos/1266808/pexels-photo-1266808.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2", "Test Painging 6", "Test 6", 100m, new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), 10, 0 },
                    { new Guid("7f4e4ab6-11ce-41d2-b178-2c768661e529"), "Painting", "https://images.pexels.com/photos/1266808/pexels-photo-1266808.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2", "Test Painging 4", "Test 4", 100m, new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), 5, 0 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Discriminator", "HorsePower", "Image", "Model", "Name", "Price", "ProductCategoryId", "Stock", "Type" },
                values: new object[,]
                {
                    { new Guid("958ff01c-8cd0-4872-897e-9cae8f9ea46b"), "Car", 200, "car_image_1.jpg", "Model X", "Car 1", 30000m, new Guid("bb2ea796-c92a-4a02-8857-0763f7f4e80a"), 5, 1 },
                    { new Guid("acc2ea2b-38a2-41fb-a779-85cad19508bf"), "Car", 150, "car_image_2.jpg", "Model Y", "Car 2", 25000m, new Guid("bb2ea796-c92a-4a02-8857-0763f7f4e80a"), 8, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("13720c78-0503-45f9-b72d-6d55a07af9ed"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("58e36234-0579-4d71-99d1-3d73810bf1b3"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("59c7b4e5-791b-46af-aba8-6cc682dbd26d"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("6c1f4831-4f3d-413a-b38a-0c9173d0892b"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("726fb132-c015-4e23-95ba-62d38c9941b8"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("7f4e4ab6-11ce-41d2-b178-2c768661e529"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("958ff01c-8cd0-4872-897e-9cae8f9ea46b"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("acc2ea2b-38a2-41fb-a779-85cad19508bf"));

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
        }
    }
}
