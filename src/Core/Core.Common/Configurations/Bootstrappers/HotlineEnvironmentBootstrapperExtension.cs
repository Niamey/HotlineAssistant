using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Vostok.Hotline.Core.Common.Configurations.Bootstrappers
{
	public static class HotlineEnvironmentBootstrapperExtension
	{
		public static void AddHotlineEnvironmentRules(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSingleton<IConfiguration>(configuration);
			services.AddSingleton<HotlineEnvironment>();
		}
	}
}
