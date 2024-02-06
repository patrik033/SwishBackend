using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwishBackend.Identity.Migrations
{
    /// <inheritdoc />
    public partial class updated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StreetNumber",
                table: "BillingAddress",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StreetNumber",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 1,
                column: "StreetNumber",
                value: "1");

            migrationBuilder.UpdateData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 2,
                column: "StreetNumber",
                value: "2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3a3e134a-2c3a-446f-86af-112d26fd2890",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "78ee5819-f8b0-4001-a28e-d8a5021b9e47", "AQAAAAIAAYagAAAAEKlPRHIGxuh0Y1h1FMFerP4XIqcKn/mKIXmWyCMrOl5NVmRXgZiKXsshwf9JNTdZWg==", "6bccab23-b7a4-4208-aa95-4753a0c3b9b3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9a3e139a-1c7a-446f-86af-112d26fd2899",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1338baf7-ca01-4ec3-9b82-663b623be218", "AQAAAAIAAYagAAAAEJn3mpm2yclUYALuEhsWmcM7Z/4zQOtpqEap1cKxWUAAWjp6x2xutfdDPWiaf9oVwQ==", "dcabf335-50b6-46d3-b70e-f64b237c1bff" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "StreetNumber",
                table: "BillingAddress",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StreetNumber",
                table: "Addresses",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 1,
                column: "StreetNumber",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 2,
                column: "StreetNumber",
                value: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3a3e134a-2c3a-446f-86af-112d26fd2890",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5dbb14a0-08e5-4ed0-8a4c-76a1c7c7b080", "AQAAAAIAAYagAAAAEBFQEFMQswQ2qI1UehFeAQVf19FseUDTp29UeNAu0BtHQWNyc0tMknOa7HgxkjTiqw==", "cd568269-bd00-47ce-86a0-438a861ee355" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9a3e139a-1c7a-446f-86af-112d26fd2899",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "050bae80-04aa-4e46-9457-f340c99e903c", "AQAAAAIAAYagAAAAEI3MGze+aJcAJ1CmTBouh7iJdqW6E2bFipc08TqnVfb5XoUKLcaVZivzm6yhVdCLlg==", "586be524-e463-49c8-bf7f-5302ff422e14" });
        }
    }
}
