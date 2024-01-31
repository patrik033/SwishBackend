using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwishBackend.Carriers.Migrations
{
    /// <inheritdoc />
    public partial class update3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ServicePointId",
                table: "PostNordNearestServicePoints",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServicePointId",
                table: "PostNordNearestServicePoints");
        }
    }
}
