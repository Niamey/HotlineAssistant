using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Api.Retail.Abstractions;
using Vostok.Hotline.Api.Retail.ApiServices.Transactions;
using Vostok.Hotline.Api.Retail.Managers;

namespace Vostok.Hotline.Api.Retail.Bootstrappers
{
	public static class TravelBootstrapperExtension
	{
		public static void AddApiTravelRules(this IServiceCollection services)
		{
			services.AddSingleton<TravelApiManager>();
		}
	}
}
