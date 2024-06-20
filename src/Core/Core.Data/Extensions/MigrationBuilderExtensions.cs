using Microsoft.EntityFrameworkCore.Migrations;
using Vostok.Hotline.Core.Data.Migrations;

namespace Vostok.Hotline.Core.Data.Extensions
{
	public static class MigrationBuilderExtensions
	{
		public static void AddSystemVersioning(this MigrationBuilder migrationBuilder, string tableName)
		{
			string sql = MigrationHelper.AddSystemVersioningSql(tableName);

			migrationBuilder.Sql(sql);
		}

		public static void AddSystemVersioning(this MigrationBuilder migrationBuilder, string schema, string tableName)
		{
			string sql = MigrationHelper.AddSystemVersioningSql(schema, tableName);

			migrationBuilder.Sql(sql);
		}
	}
}
