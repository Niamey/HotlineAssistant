using Microsoft.Extensions.DependencyInjection;

namespace Vostok.Hotline.Api.Bvr.Bootstrappers
{
	public static class BvrBootstrapperExstension
	{
		public static void AddApiBvrRules(this IServiceCollection services)
		{
			services.AddApiDeviceRules();
		}
	}
}
