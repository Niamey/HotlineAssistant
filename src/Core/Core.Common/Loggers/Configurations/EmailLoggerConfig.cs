using NLog;
using Vostok.Hotline.Core.Common.Configurations;
using Vostok.Hotline.Core.Common.Conversions;
using Vostok.Hotline.Core.Common.Loggers.Configurations.Base;

namespace Vostok.Hotline.Core.Common.Loggers.Configurations
{
	public class EmailLoggerConfig : BaseLoggerConfig
	{
		public override string Key => "smtp";
		public override string EnvironmentKey => "Email";

		public EmailLoggerConfig(HotlineEnvironment environment)
			:base(environment)
		{
			var level = environment.GetEnvironmentVariable($"Logger.{EnvironmentKey}.Level");
			if (string.IsNullOrEmpty(level))
				level = "Error";

			Level = LogLevel.FromString(level);
			From = environment.GetEnvironmentVariable($"Logger.{EnvironmentKey}.From");
			CC = Converter.ToArrayFromCommaSeparated(environment.GetEnvironmentVariable($"Logger.{EnvironmentKey}.CC"), ';');
		}

		public string From { get; protected set; }
		public string[] CC { get; protected set; }

		public bool HasCC => CC.Length > 0;
		public bool HasFrom => From?.Length > 0;

		public string GetCC()
		{
			return string.Join(';', CC);
		}
	}
}