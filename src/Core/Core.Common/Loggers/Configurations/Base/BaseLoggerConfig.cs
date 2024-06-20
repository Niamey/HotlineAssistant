using NLog;
using Vostok.Hotline.Core.Common.Configurations;
using Vostok.Hotline.Core.Common.Conversions;

namespace Vostok.Hotline.Core.Common.Loggers.Configurations.Base
{
	public abstract class BaseLoggerConfig
	{
		public BaseLoggerConfig(HotlineEnvironment environment)
		{
			var level = environment.GetEnvironmentVariable($"Logger.{EnvironmentKey}.Level");
			if (string.IsNullOrEmpty(level))
				level = "Trace";

			Level = LogLevel.FromString(level);
			Disable = Converter.ToBoolean(environment.GetEnvironmentVariable($"Logger.{EnvironmentKey}.Disable"));
		}

		public abstract string Key { get; }
		public abstract string EnvironmentKey { get; }

		public LogLevel Level { get; protected set; }
		public bool Disable { get; protected set; }
	}
}
