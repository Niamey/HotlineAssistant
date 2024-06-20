using Microsoft.EntityFrameworkCore.Migrations;

namespace Vostok.Hotline.Data.EF.Migrations.HotlineReferences
{
    public partial class AddCountrySetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CountrySettings",
                columns: table => new
                {
                    countryCode = table.Column<string>(unicode: false, maxLength: 3, nullable: false),
                    isCountryRisk = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountrySettings", x => x.countryCode);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CountrySettings");
        }
    }
}
