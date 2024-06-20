using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;

namespace Tools.Console.Sync.Transaction
{
	using Printer = System.Console;

	class Program
    {
		static string _fromDBHost = "spdv-oras001.proc.lan";
		static int _fromDBPort = 1521;
		static string _fromServiceName = "SOLAR";
		static string _fromDBUser = "Solar_bo";
		static string _fromDBPassword = "solar";

		static string _recipientDBHost = "SBDP-CENTER-I08.BANK.LAN";
		static string _recipientDB = "Solar";
		static string _recipientDBUser = "j-hotlineassistan";
		static string _recipientDBPassword = "H57M1MfaiOTonOxz7EwG";
		static string _recipientDBScheme = "SOLAR_BO";

		static List<string> _tableNameList = new List<string>()
		{
			"BO_AGREEMENT",
			"BO_FEE_TXN",
			"BO_STMT_ENTRY",
			"BO_STMT_ENTRY_AGMT",
			"BO_TXN",
			"BO_TXN_TYPE",
			"BO_ORIGINAL_TXN_DATA",
			"BO_ACCESSOR"

		};
		static void Main(string[] args)
        {
			string oracleConnectionString = $"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={_fromDBHost})(PORT={_fromDBPort}))(CONNECT_DATA=(SERVICE_NAME={_fromServiceName})));User Id={_fromDBUser};Password={_fromDBPassword};";
			
			foreach (var tableName in _tableNameList)
			{
				var startTime = DateTime.Now;
				Printer.WriteLine($"Start copy {_recipientDBScheme}.{tableName}");
				string sqlRequest = $"SELECT * FROM {tableName}";

				var dt = GetDataTable(oracleConnectionString, sqlRequest);
				Printer.WriteLine("Record Count: " + dt.Rows.Count.ToString());

				InsertData(dt, tableName);
				Printer.WriteLine($"Stop copy {_recipientDBScheme}.{tableName}");
				int timeSpan = (DateTime.Now - startTime).Seconds;
				Printer.WriteLine(timeSpan.ToString() + " Seconds");
				Printer.WriteLine("**********************************");
			}
			Printer.WriteLine("Finish");

			Printer.ReadLine();
		}

		private static DataTable GetDataTable(string connectionString, string sql)
		{
			var returnDataset = new DataSet();
			using (var connection = new OracleConnection(connectionString))
			{
				connection.Open();
				using (var command = new OracleCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;

					var dataAdapter = new OracleDataAdapter(command);
					dataAdapter.Fill(returnDataset);
				}
			}
			return returnDataset.Tables[0];
		}

		private static void InsertData(DataTable dt, string tableName)
		{
			string sqlTruncate = $"TRUNCATE TABLE {_recipientDBScheme}.{tableName};" + Environment.NewLine;
			string sqlExist = $"SELECT CASE WHEN EXISTS((SELECT * FROM information_schema.tables WHERE table_name = '{tableName}')) THEN 1 ELSE 0 END";

			string sqlConnectionString = $"Server={_recipientDBHost};Database={_recipientDB};User ID={_recipientDBUser};Password={_recipientDBPassword};";
			using (var connection = new SqlConnection(sqlConnectionString))
			{
				var exists = false;
				connection.Open();
				using (var cmd = new SqlCommand(sqlExist, connection))
				{
					exists = (int)cmd.ExecuteScalar() == 1;
				}

				if (exists)
				{
					using (var cmd = new SqlCommand(sqlTruncate, connection))
					{
						cmd.CommandType = CommandType.Text;
						cmd.ExecuteNonQuery();
					}
				}
				else 
				{
					Printer.WriteLine($"Table {_recipientDBScheme}.{tableName} not exist in {_recipientDBHost}");
					return;
				}

				using (var bulkCopy = new SqlBulkCopy(connection))
				{
					foreach (DataColumn c in dt.Columns)
						bulkCopy.ColumnMappings.Add(c.ColumnName, c.ColumnName);

					bulkCopy.DestinationTableName = $"[{_recipientDBScheme}].[{tableName}]";
					try
					{
						bulkCopy.WriteToServer(dt);
					}
					catch (Exception ex)
					{
						Printer.WriteLine(ex.Message);
					}
				}
			}
		}
	}
}
