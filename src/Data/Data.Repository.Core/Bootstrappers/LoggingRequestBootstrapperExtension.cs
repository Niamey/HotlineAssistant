using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Data.Repository.Core.Managers;

namespace Vostok.Hotline.Data.Repository.Core.Bootstrappers
{
	public static class LoggingRequestBootstrapperExtension
	{
		public static void AddLoggingRequestSettingRules(this IServiceCollection services)
		{
			services.AddScoped<LoggingRequestManager>();
		}
	}
}
