using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Data.Repository.Core.Managers;

namespace Vostok.Hotline.Data.Repository.Core.Bootstrappers
{
	public static class TravelBootstrapperExtension
	{
		public static void AddTravelSettingRules(this IServiceCollection services)
		{
			services.AddTransient<TravelManager>();
		}
	}
}
