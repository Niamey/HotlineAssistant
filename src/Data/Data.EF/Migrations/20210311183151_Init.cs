using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vostok.Hotline.Data.EF.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EnvironmentSettings",
                columns: table => new
                {
                    environmentKey = table.Column<string>(unicode: false, maxLength: 128, nullable: false),
                    version = table.Column<byte[]>(rowVersion: true, nullable: true),
                    createdOn = table.Column<DateTime>(nullable: false),
                    updatedOn = table.Column<DateTime>(nullable: false),
                    sessionId = table.Column<Guid>(nullable: true),
                    environmentValue = table.Column<string>(unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnvironmentSettings", x => x.environmentKey);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnvironmentSettings");
        }
    }
}
