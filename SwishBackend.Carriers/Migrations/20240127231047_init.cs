using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwishBackend.Carriers.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostNordNearestServicePoints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostNordNearestServicePoints", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CordinateDTO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    countryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    northing = table.Column<double>(type: "float", nullable: false),
                    easting = table.Column<double>(type: "float", nullable: false),
                    srId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NearestServicePointId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CordinateDTO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CordinateDTO_PostNordNearestServicePoints_NearestServicePointId",
                        column: x => x.NearestServicePointId,
                        principalTable: "PostNordNearestServicePoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryAddressDTO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StreetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StreetNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostNordNearestServicePointId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryAddressDTO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryAddressDTO_PostNordNearestServicePoints_PostNordNearestServicePointId",
                        column: x => x.PostNordNearestServicePointId,
                        principalTable: "PostNordNearestServicePoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DHLGeoDTO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    NearestServicePointId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DHLGeoDTO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DHLGeoDTO_PostNordNearestServicePoints_NearestServicePointId",
                        column: x => x.NearestServicePointId,
                        principalTable: "PostNordNearestServicePoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OpeningHoursDTO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NearestServicePointId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpeningHoursDTO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpeningHoursDTO_PostNordNearestServicePoints_NearestServicePointId",
                        column: x => x.NearestServicePointId,
                        principalTable: "PostNordNearestServicePoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostalServiceDTO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OpenDay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CloseDay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpenTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CloseTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpeningHoursId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostalServiceDTO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostalServiceDTO_OpeningHoursDTO_OpeningHoursId",
                        column: x => x.OpeningHoursId,
                        principalTable: "OpeningHoursDTO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CordinateDTO_NearestServicePointId",
                table: "CordinateDTO",
                column: "NearestServicePointId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryAddressDTO_PostNordNearestServicePointId",
                table: "DeliveryAddressDTO",
                column: "PostNordNearestServicePointId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DHLGeoDTO_NearestServicePointId",
                table: "DHLGeoDTO",
                column: "NearestServicePointId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OpeningHoursDTO_NearestServicePointId",
                table: "OpeningHoursDTO",
                column: "NearestServicePointId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostalServiceDTO_OpeningHoursId",
                table: "PostalServiceDTO",
                column: "OpeningHoursId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CordinateDTO");

            migrationBuilder.DropTable(
                name: "DeliveryAddressDTO");

            migrationBuilder.DropTable(
                name: "DHLGeoDTO");

            migrationBuilder.DropTable(
                name: "PostalServiceDTO");

            migrationBuilder.DropTable(
                name: "OpeningHoursDTO");

            migrationBuilder.DropTable(
                name: "PostNordNearestServicePoints");
        }
    }
}
