using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwishBackend.Identity.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StreetNumber",
                table: "BillingAddress",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StreetNumber",
                table: "Addresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StreetNumber",
                table: "BillingAddress");

            migrationBuilder.DropColumn(
                name: "StreetNumber",
                table: "Addresses");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3a3e134a-2c3a-446f-86af-112d26fd2890",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d6fdbe10-bf74-44b1-9857-519dd9f470f7", "AQAAAAIAAYagAAAAEGxpY9j+izAnrNrUIlxsIz0IVYKi+hSG2H2D+Sye5Xqd22gaE5w7GIwouLE1JEiBSw==", "643b7195-90c7-496c-b8b6-d62f224186e2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9a3e139a-1c7a-446f-86af-112d26fd2899",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "595ec6d3-a350-4efc-8c97-a8b4bc9a16b2", "AQAAAAIAAYagAAAAEEwwf2RlHiGkd5G0wzWtc3IATi0TPsu2NVxkihWQx5pWxVVbbX/7xbbE/3UaLBx9+w==", "88206eb3-3b8a-4276-ab1d-bd4a336680a2" });
        }
    }
}
