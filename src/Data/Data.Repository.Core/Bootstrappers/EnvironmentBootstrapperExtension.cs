using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Data.Repository.Core.Managers;

namespace Vostok.Hotline.Data.Repository.Core.Bootstrappers
{
	public static class EnvironmentBootstrapperExtension
	{
		public static void AddEnvironmentSettingRules(this IServiceCollection services)
		{
			services.AddSingleton<EnvironmentManager>();
		}
	}
}
