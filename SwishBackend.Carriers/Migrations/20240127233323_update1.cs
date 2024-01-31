using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwishBackend.Carriers.Migrations
{
    /// <inheritdoc />
    public partial class update1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CordinateDTO_PostNordNearestServicePoints_NearestServicePointId",
                table: "CordinateDTO");

            migrationBuilder.AlterColumn<int>(
                name: "NearestServicePointId",
                table: "CordinateDTO",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_CordinateDTO_PostNordNearestServicePoints_NearestServicePointId",
                table: "CordinateDTO",
                column: "NearestServicePointId",
                principalTable: "PostNordNearestServicePoints",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CordinateDTO_PostNordNearestServicePoints_NearestServicePointId",
                table: "CordinateDTO");

            migrationBuilder.AlterColumn<int>(
                name: "NearestServicePointId",
                table: "CordinateDTO",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CordinateDTO_PostNordNearestServicePoints_NearestServicePointId",
                table: "CordinateDTO",
                column: "NearestServicePointId",
                principalTable: "PostNordNearestServicePoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
