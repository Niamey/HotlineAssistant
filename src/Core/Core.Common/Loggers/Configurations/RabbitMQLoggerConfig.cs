using NLog;
using Vostok.Hotline.Core.Common.Configurations;
using Vostok.Hotline.Core.Common.Conversions;
using Vostok.Hotline.Core.Common.Extensions;
using Vostok.Hotline.Core.Common.Loggers.Configurations.Base;

namespace Vostok.Hotline.Core.Common.Loggers.Configurations
{
	public class RabbitMQLoggerConfig : BaseLoggerConfig
	{
		public override string Key => "rabbit";
		public override string EnvironmentKey => "RabbitMq";

		public RabbitMQLoggerConfig(HotlineEnvironment environment)
			: base(environment)
		{
			if (!Disable)
			{
				Host = environment.GetEnvironmentVariable("Logger.RabbitMq.Host");
				Usert = environment.GetEnvironmentVariable("Logger.RabbitMq.Usert");
				Password = environment.GetEnvironmentPasswordVariable("Logger.RabbitMq.Password");
				Vhost = environment.GetEnvironmentVariable("Logger.RabbitMq.Vhost");
				Exchange = environment.GetEnvironmentVariable("Logger.RabbitMq.Exchange");
				Port = Converter.ToInt32(environment.GetEnvironmentVariable("Logger.RabbitMq.Port"));
			}
		}

		public string Host { get; protected set; }
		public string Usert { get; protected set; }
		public string Password { get; protected set; }
		public string Vhost { get; protected set; }
		public string Exchange { get; protected set; }
		public int Port { get; protected set; }
	}
}