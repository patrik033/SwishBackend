using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwishBackend.Identity.Migrations
{
    /// <inheritdoc />
    public partial class AddedAddressToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StreetAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3a3e134a-2c3a-446f-86af-112d26fd2890",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "adc20af5-c0d6-4d5b-9a8b-a758bc12dd26", "AQAAAAIAAYagAAAAEErxijkRzAmW2TD3UuSwpyYJzP762gzLabQMGwwRR9P0jJCYskND0XCZdggCCl6X7A==", "93482514-b86f-41ec-9cb5-9613b1ed8c64" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9a3e139a-1c7a-446f-86af-112d26fd2899",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c680ba3a-a5d3-4334-acf3-88b0ff9f91e6", "AQAAAAIAAYagAAAAECuLM6mwrQuZZ/d+vxLa/tboLtU3tqhrc0k+kcXOfVv4s3HjOTdRG94WJlNWwX5Bpg==", "d2d356f4-57c7-4da4-8c43-5582eb8908f2" });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_UserId",
                table: "Addresses",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3a3e134a-2c3a-446f-86af-112d26fd2890",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c697d29e-9145-4179-9925-43516cb70819", "AQAAAAIAAYagAAAAELr4IKLjrOlomqeJ/YXVdj3TcI//MEDD8oqbbNZ6yEBmgxc/XqVVRjUpoVutxVbvHg==", "924fe6e8-ce10-4220-a74f-370072504954" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9a3e139a-1c7a-446f-86af-112d26fd2899",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b1789403-e368-4ded-b5dd-89cdd30c5d6d", "AQAAAAIAAYagAAAAEDbjBmbeoxBF0JG6NXH1/nPj7EC688avyID3lT5aguRunxXAb+Gz3NbRMCUgacGq6g==", "4ab48bf3-3836-45c6-86b4-b9569ddb7c25" });
        }
    }
}
