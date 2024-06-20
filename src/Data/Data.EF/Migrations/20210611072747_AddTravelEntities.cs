using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vostok.Hotline.Data.EF.Migrations
{
    public partial class AddTravelEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Travels",
                schema: "dbo",
                columns: table => new
                {
                    travelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    version = table.Column<byte[]>(rowVersion: true, nullable: true),
                    createdOn = table.Column<DateTime>(nullable: false),
                    updatedOn = table.Column<DateTime>(nullable: false),
                    sessionId = table.Column<Guid>(nullable: true),
                    solarId = table.Column<long>(nullable: false),
                    travelStatus = table.Column<int>(nullable: false),
                    startTravel = table.Column<DateTime>(nullable: false),
                    endTravel = table.Column<DateTime>(nullable: false),
                    contactPhone = table.Column<long>(nullable: false),
                    cashWithdrawalLimit = table.Column<decimal>(nullable: false),
                    limitCardTransfers = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Travels", x => x.travelId);
                });

            migrationBuilder.CreateTable(
                name: "TravelCards",
                schema: "dbo",
                columns: table => new
                {
                    travelCardId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    version = table.Column<byte[]>(rowVersion: true, nullable: true),
                    createdOn = table.Column<DateTime>(nullable: false),
                    updatedOn = table.Column<DateTime>(nullable: false),
                    sessionId = table.Column<Guid>(nullable: true),
                    travelId = table.Column<int>(nullable: false),
                    cardId = table.Column<long>(nullable: false),
                    cardMaskNumber = table.Column<string>(unicode: false, maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelCards", x => x.travelCardId);
                    table.ForeignKey(
                        name: "FK_TravelCards_Travels_travelId",
                        column: x => x.travelId,
                        principalSchema: "dbo",
                        principalTable: "Travels",
                        principalColumn: "travelId");
                });

            migrationBuilder.CreateTable(
                name: "TravelCountries",
                schema: "dbo",
                columns: table => new
                {
                    travelCountryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    version = table.Column<byte[]>(rowVersion: true, nullable: true),
                    createdOn = table.Column<DateTime>(nullable: false),
                    updatedOn = table.Column<DateTime>(nullable: false),
                    sessionId = table.Column<Guid>(nullable: true),
                    travelId = table.Column<int>(nullable: false),
                    countryCode = table.Column<string>(unicode: false, maxLength: 3, nullable: false),
                    riskCountry = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelCountries", x => x.travelCountryId);
                    table.ForeignKey(
                        name: "FK_TravelCountries_Travels_travelId",
                        column: x => x.travelId,
                        principalSchema: "dbo",
                        principalTable: "Travels",
                        principalColumn: "travelId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TravelCards_sessionId",
                schema: "dbo",
                table: "TravelCards",
                column: "sessionId");

            migrationBuilder.CreateIndex(
                name: "IX_TravelCards_travelId",
                schema: "dbo",
                table: "TravelCards",
                column: "travelId");

            migrationBuilder.CreateIndex(
                name: "IX_TravelCountries_sessionId",
                schema: "dbo",
                table: "TravelCountries",
                column: "sessionId");

            migrationBuilder.CreateIndex(
                name: "IX_TravelCountries_travelId",
                schema: "dbo",
                table: "TravelCountries",
                column: "travelId");

            migrationBuilder.CreateIndex(
                name: "IX_Travels_sessionId",
                schema: "dbo",
                table: "Travels",
                column: "sessionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TravelCards",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TravelCountries",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Travels",
                schema: "dbo");
        }
    }
}
