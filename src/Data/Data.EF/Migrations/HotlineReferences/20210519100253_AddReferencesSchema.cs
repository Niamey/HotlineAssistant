using Microsoft.EntityFrameworkCore.Migrations;

namespace Vostok.Hotline.Data.EF.Migrations.HotlineReferences
{
    public partial class AddReferencesSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "references");

            migrationBuilder.CreateTable(
                name: "RefTransactionCode",
                schema: "references",
                columns: table => new
                {
                    code = table.Column<string>(unicode: false, maxLength: 2, nullable: false),
                    rc = table.Column<string>(unicode: false, maxLength: 64, nullable: true),
                    description = table.Column<string>(unicode: false, maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefTransactionCode", x => x.code);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefTransactionCode",
                schema: "references");
        }
    }
}
