using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vostok.Hotline.Data.EF.Migrations
{
    public partial class AddLoggingRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoggingRequests",
                columns: table => new
                {
                    loggingRequestId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    version = table.Column<byte[]>(rowVersion: true, nullable: true),
                    createdOn = table.Column<DateTime>(nullable: false),
                    updatedOn = table.Column<DateTime>(nullable: false),
                    sessionId = table.Column<Guid>(nullable: true),
                    uniqueRequestId = table.Column<string>(unicode: false, maxLength: 32, nullable: true),
                    loggingSystemType = table.Column<int>(nullable: false),
                    loggingOperationType = table.Column<int>(nullable: true),
                    hasError = table.Column<bool>(nullable: false),
                    requestValue = table.Column<string>(unicode: false, nullable: true),
                    responseValue = table.Column<string>(unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoggingRequests", x => x.loggingRequestId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoggingRequests_sessionId",
                table: "LoggingRequests",
                column: "sessionId");

            migrationBuilder.CreateIndex(
                name: "IX_LoggingRequests_uniqueRequestId",
                table: "LoggingRequests",
                column: "uniqueRequestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoggingRequests");
        }
    }
}
