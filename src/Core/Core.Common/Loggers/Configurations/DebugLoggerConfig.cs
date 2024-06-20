using Vostok.Hotline.Core.Common.Configurations;
using Vostok.Hotline.Core.Common.Loggers.Configurations.Base;

namespace Vostok.Hotline.Core.Common.Loggers.Configurations
{
	public class DebugLoggerConfig : BaseLoggerConfig
	{
		public override string Key => "debug";
		public override string EnvironmentKey => "Debug";

		public DebugLoggerConfig(HotlineEnvironment environment)
			:base(environment)
		{			
		}
	}
}