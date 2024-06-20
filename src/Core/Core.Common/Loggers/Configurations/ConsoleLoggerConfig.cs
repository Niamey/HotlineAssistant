using Vostok.Hotline.Core.Common.Configurations;
using Vostok.Hotline.Core.Common.Loggers.Configurations.Base;

namespace Vostok.Hotline.Core.Common.Loggers.Configurations
{
	public class ConsoleLoggerConfig : BaseLoggerConfig
	{
		public override string Key => "console";

		public override string EnvironmentKey => "Console";

		public ConsoleLoggerConfig(HotlineEnvironment environment)
			:base(environment)
		{
		}
	}
}