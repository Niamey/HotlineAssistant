using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Api.References.Managers;

namespace Vostok.Hotline.Api.References.Bootstrappers
{
	public static class RegionBootstrapperExtension
	{
		public static void AddApiRegionRules(this IServiceCollection services)
		{
			services.AddSingleton<RegionApiManager>();
		}
	}
}
