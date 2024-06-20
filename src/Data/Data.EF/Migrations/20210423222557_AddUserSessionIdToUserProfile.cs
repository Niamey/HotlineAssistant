using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vostok.Hotline.Data.EF.Migrations
{
    public partial class AddUserSessionIdToUserProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "userSessionId",
                table: "UserProfiles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userSessionId",
                table: "UserProfiles");
        }
    }
}
