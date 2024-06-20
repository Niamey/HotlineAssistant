using System;
using Vostok.Hotline.Core.Common.Configurations;
using Vostok.Hotline.Core.Common.Data.Abstractions;

namespace Vostok.Hotline.Data.EF.Configurations
{
	public class HotlineReferencesDbConnectionConfig : IDbConnectionConfig
	{
		public HotlineReferencesDbConnectionConfig(HotlineEnvironment environment)
		{
			string context = environment.GetEnvironmentVariable("ConnectionStrings.HotlineContext.References");
			string password = environment.GetEnvironmentPasswordVariable("ConnectionStrings.HotlineContext.References.Password");

			if (string.IsNullOrEmpty(context))
				throw new Exception(nameof(context), new Exception(environment.GetEnvironmentVariable("ConnectionStrings.HotlineContext.References")));

			if (!string.IsNullOrEmpty(password))
				ConnectionString = $"{context}Password={password};";
			else
				ConnectionString = $"{context}{(!context.Trim().EndsWith(';') ? ";" : "")}Persist security info=True;Integrated Security=SSPI;";
		}

		public string ConnectionString { get; protected set; }
		public int CommandTimeout { get; protected set; }
	}
}
