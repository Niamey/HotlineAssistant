using System;
using System.Collections.Generic;
using System.Text;
using NLog;
using Vostok.Hotline.Core.Common.Configurations;
using Vostok.Hotline.Core.Common.Data.Abstractions;
using Vostok.Hotline.Core.Common.Extensions;
using Vostok.Hotline.Core.Common.Helpers;
using Vostok.Hotline.Core.Common.Loggers.Configurations.Base;

namespace Vostok.Hotline.Core.Common.Loggers.Configurations
{
	public class DbLoggerConfig : BaseLoggerConfig, IDbConnectionConfig
	{
		public override string Key => "database";
		public override string EnvironmentKey => "Database";

		public DbLoggerConfig(HotlineEnvironment environment)
			: base(environment)
		{
			if (!Disable)
			{
				string hotlineDataSource = environment.GetEnvironmentVariable("Logger.Database.ConnectionString");
				string hotlinePassword = environment.GetEnvironmentPasswordVariable("Logger.Database.ConnectionString.Password");
				Assure.ArgumentNotNull(hotlinePassword, nameof(hotlinePassword));

				if (string.IsNullOrEmpty(hotlineDataSource))
					throw new Exception(nameof(hotlineDataSource), new Exception(environment.GetEnvironmentVariable("Logger.Database.ConnectionString")));

				ConnectionString = $"{hotlineDataSource}Password={hotlinePassword};";
			}
		}

		public string ConnectionString { get; protected set; }
		public int CommandTimeout { get; protected set; }
	}
}