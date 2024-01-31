using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwishBackend.Carriers.Migrations
{
    /// <inheritdoc />
    public partial class updar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CordinateDTO_PostNordNearestServicePoints_NearestServicePointId",
                table: "CordinateDTO");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryAddressDTO_PostNordNearestServicePoints_PostNordNearestServicePointId",
                table: "DeliveryAddressDTO");

            migrationBuilder.DropForeignKey(
                name: "FK_DHLGeoDTO_PostNordNearestServicePoints_NearestServicePointId",
                table: "DHLGeoDTO");

            migrationBuilder.DropForeignKey(
                name: "FK_OpeningHoursDTO_PostNordNearestServicePoints_NearestServicePointId",
                table: "OpeningHoursDTO");

            migrationBuilder.DropIndex(
                name: "IX_CordinateDTO_NearestServicePointId",
                table: "CordinateDTO");

            migrationBuilder.DropColumn(
                name: "NearestServicePointId",
                table: "CordinateDTO");

            migrationBuilder.RenameColumn(
                name: "NearestServicePointId",
                table: "OpeningHoursDTO",
                newName: "ServicePointId");

            migrationBuilder.RenameIndex(
                name: "IX_OpeningHoursDTO_NearestServicePointId",
                table: "OpeningHoursDTO",
                newName: "IX_OpeningHoursDTO_ServicePointId");

            migrationBuilder.RenameColumn(
                name: "NearestServicePointId",
                table: "DHLGeoDTO",
                newName: "ServicePointId");

            migrationBuilder.RenameIndex(
                name: "IX_DHLGeoDTO_NearestServicePointId",
                table: "DHLGeoDTO",
                newName: "IX_DHLGeoDTO_ServicePointId");

            migrationBuilder.RenameColumn(
                name: "PostNordNearestServicePointId",
                table: "DeliveryAddressDTO",
                newName: "ServicePointId");

            migrationBuilder.RenameIndex(
                name: "IX_DeliveryAddressDTO_PostNordNearestServicePointId",
                table: "DeliveryAddressDTO",
                newName: "IX_DeliveryAddressDTO_ServicePointId");

            migrationBuilder.AddColumn<int>(
                name: "ServicePointId",
                table: "CordinateDTO",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CordinateDTO_ServicePointId",
                table: "CordinateDTO",
                column: "ServicePointId");

            migrationBuilder.AddForeignKey(
                name: "FK_CordinateDTO_PostNordNearestServicePoints_ServicePointId",
                table: "CordinateDTO",
                column: "ServicePointId",
                principalTable: "PostNordNearestServicePoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryAddressDTO_PostNordNearestServicePoints_ServicePointId",
                table: "DeliveryAddressDTO",
                column: "ServicePointId",
                principalTable: "PostNordNearestServicePoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DHLGeoDTO_PostNordNearestServicePoints_ServicePointId",
                table: "DHLGeoDTO",
                column: "ServicePointId",
                principalTable: "PostNordNearestServicePoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OpeningHoursDTO_PostNordNearestServicePoints_ServicePointId",
                table: "OpeningHoursDTO",
                column: "ServicePointId",
                principalTable: "PostNordNearestServicePoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CordinateDTO_PostNordNearestServicePoints_ServicePointId",
                table: "CordinateDTO");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryAddressDTO_PostNordNearestServicePoints_ServicePointId",
                table: "DeliveryAddressDTO");

            migrationBuilder.DropForeignKey(
                name: "FK_DHLGeoDTO_PostNordNearestServicePoints_ServicePointId",
                table: "DHLGeoDTO");

            migrationBuilder.DropForeignKey(
                name: "FK_OpeningHoursDTO_PostNordNearestServicePoints_ServicePointId",
                table: "OpeningHoursDTO");

            migrationBuilder.DropIndex(
                name: "IX_CordinateDTO_ServicePointId",
                table: "CordinateDTO");

            migrationBuilder.DropColumn(
                name: "ServicePointId",
                table: "CordinateDTO");

            migrationBuilder.RenameColumn(
                name: "ServicePointId",
                table: "OpeningHoursDTO",
                newName: "NearestServicePointId");

            migrationBuilder.RenameIndex(
                name: "IX_OpeningHoursDTO_ServicePointId",
                table: "OpeningHoursDTO",
                newName: "IX_OpeningHoursDTO_NearestServicePointId");

            migrationBuilder.RenameColumn(
                name: "ServicePointId",
                table: "DHLGeoDTO",
                newName: "NearestServicePointId");

            migrationBuilder.RenameIndex(
                name: "IX_DHLGeoDTO_ServicePointId",
                table: "DHLGeoDTO",
                newName: "IX_DHLGeoDTO_NearestServicePointId");

            migrationBuilder.RenameColumn(
                name: "ServicePointId",
                table: "DeliveryAddressDTO",
                newName: "PostNordNearestServicePointId");

            migrationBuilder.RenameIndex(
                name: "IX_DeliveryAddressDTO_ServicePointId",
                table: "DeliveryAddressDTO",
                newName: "IX_DeliveryAddressDTO_PostNordNearestServicePointId");

            migrationBuilder.AddColumn<int>(
                name: "NearestServicePointId",
                table: "CordinateDTO",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CordinateDTO_NearestServicePointId",
                table: "CordinateDTO",
                column: "NearestServicePointId");

            migrationBuilder.AddForeignKey(
                name: "FK_CordinateDTO_PostNordNearestServicePoints_NearestServicePointId",
                table: "CordinateDTO",
                column: "NearestServicePointId",
                principalTable: "PostNordNearestServicePoints",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryAddressDTO_PostNordNearestServicePoints_PostNordNearestServicePointId",
                table: "DeliveryAddressDTO",
                column: "PostNordNearestServicePointId",
                principalTable: "PostNordNearestServicePoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DHLGeoDTO_PostNordNearestServicePoints_NearestServicePointId",
                table: "DHLGeoDTO",
                column: "NearestServicePointId",
                principalTable: "PostNordNearestServicePoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OpeningHoursDTO_PostNordNearestServicePoints_NearestServicePointId",
                table: "OpeningHoursDTO",
                column: "NearestServicePointId",
                principalTable: "PostNordNearestServicePoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
