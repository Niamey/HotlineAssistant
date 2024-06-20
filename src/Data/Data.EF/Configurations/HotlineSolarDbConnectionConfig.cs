using System;
using Vostok.Hotline.Core.Common.Configurations;
using Vostok.Hotline.Core.Common.Data.Abstractions;

namespace Vostok.Hotline.Data.EF.Configurations
{
	public class HotlineSolarDbConnectionConfig : IDbConnectionConfig
	{
		public HotlineSolarDbConnectionConfig(HotlineEnvironment environment)
		{
			string crmContext = environment.GetEnvironmentVariable("ConnectionStrings.HotlineContext.Solar");
			string crmPassword = environment.GetEnvironmentPasswordVariable("ConnectionStrings.HotlineContext.Solar.Password");

			if (string.IsNullOrEmpty(crmContext))
				throw new Exception(nameof(crmContext), new Exception(environment.GetEnvironmentVariable("ConnectionStrings.HotlineContext.Solar")));

			if (!string.IsNullOrEmpty(crmPassword))
				ConnectionString = $"{crmContext}Password={crmPassword};";
			else
				ConnectionString = $"{crmContext}{(!crmContext.Trim().EndsWith(';') ? ";" : "")}Persist security info=True;Integrated Security=SSPI;";
		}

		public string ConnectionString { get; protected set; }
		public int CommandTimeout { get; protected set; }
	}
}
