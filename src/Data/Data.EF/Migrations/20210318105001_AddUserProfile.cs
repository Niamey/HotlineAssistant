using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vostok.Hotline.Data.EF.Migrations
{
    public partial class AddUserProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    userId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    version = table.Column<byte[]>(rowVersion: true, nullable: true),
                    createdOn = table.Column<DateTime>(nullable: false),
                    updatedOn = table.Column<DateTime>(nullable: false),
                    sessionId = table.Column<Guid>(nullable: true),
                    login = table.Column<string>(unicode: false, maxLength: 32, nullable: false),
                    email = table.Column<string>(unicode: false, maxLength: 48, nullable: false),
                    fullName = table.Column<string>(unicode: false, maxLength: 96, nullable: true),
                    position = table.Column<string>(unicode: false, maxLength: 48, nullable: true),
                    avatar = table.Column<byte[]>(nullable: true),
                    successfulDateLogin = table.Column<DateTime>(nullable: true),
                    lastDateLogin = table.Column<DateTime>(nullable: true),
                    unSuccessfulLogin = table.Column<byte>(nullable: false, defaultValue: (byte)0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.userId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_login",
                table: "UserProfiles",
                column: "login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_sessionId",
                table: "UserProfiles",
                column: "sessionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserProfiles");
        }
    }
}
