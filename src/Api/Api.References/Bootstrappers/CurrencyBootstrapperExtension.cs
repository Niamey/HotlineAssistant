using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Api.References.Abstractions;
using Vostok.Hotline.Api.References.ApiServices;
using Vostok.Hotline.Api.References.Managers;

namespace Vostok.Hotline.Api.References.Bootstrappers
{
	public static class CurrencyBootstrapperExtension
	{
		public static void AddApiCurrencyRules(this IServiceCollection services)
		{
			services.AddSingleton<ICurrencyApiService, CurrencyApiService>(serviceProvider => 
			{
				return new CurrencyApiService(serviceProvider); 
			});
			services.AddSingleton<CurrencyApiManager>();
		}
	}
}
