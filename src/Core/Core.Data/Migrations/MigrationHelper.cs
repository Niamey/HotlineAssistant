using System.Text;

namespace Vostok.Hotline.Core.Data.Migrations
{
	public static class MigrationHelper
	{
		public static string AddSystemVersioningSql(string tableName)
		{
			return AddSystemVersioningSql("dbo", tableName);
		}

		public static string AddSystemVersioningSql(string schema, string tableName)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine(AddFieldsForSystemVersioningSql(schema, tableName));
			sb.AppendLine(AddSystemVersionTableSql(schema, tableName));

			return sb.ToString();
		}

		public static string AddSystemVersionTableSql(string schema, string tableName)
		{
			string sql = $@"IF NOT EXISTS (SELECT * FROM  sys.tables WHERE  object_id = OBJECT_ID('{schema}.{tableName}')  AND temporal_type > 0) 
                BEGIN 
                    ALTER TABLE [{schema}].[{tableName}] SET(SYSTEM_VERSIONING = ON (HISTORY_TABLE = [{schema}].[{tableName}_SysHistory]))
                END
				";
			return sql;
		}

		public static string AddFieldsForSystemVersioningSql(string schema, string tableName)
		{
			return $@"ALTER TABLE [{schema}].[{tableName}] ADD
						sysStartTime datetime2(0)  GENERATED ALWAYS AS ROW START HIDDEN CONSTRAINT DF_{tableName}_SysStartTime DEFAULT CONVERT(datetime2 (0), DATEADD(s, -1, SYSUTCDATETIME())),
						sysEndTime datetime2(0)    GENERATED ALWAYS AS ROW END   HIDDEN CONSTRAINT DF_{tableName}_SysEndTime   DEFAULT CONVERT(datetime2 (0), '99991231 23:59:59'),
                    PERIOD FOR SYSTEM_TIME(sysStartTime, sysEndTime)
					";
		}
	}
}
