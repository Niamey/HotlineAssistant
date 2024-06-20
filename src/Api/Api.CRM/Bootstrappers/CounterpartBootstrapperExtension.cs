using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Api.CRM.Abstractions;
using Vostok.Hotline.Api.CRM.ApiServices;
using Vostok.Hotline.Api.CRM.Managers;

namespace Vostok.Hotline.Api.CRM.Bootstrappers
{
	public static class CounterpartBootstrapperExtension
	{
		public static void AddApiCounterpartRules(this IServiceCollection services)
		{
			services.AddSingleton<ICounterpartApiService, CounterpartApiService>(serviceProvider => 
			{
				return new CounterpartApiService(serviceProvider); 
			});
			services.AddSingleton<CounterpartApiManager>();
		}
	}
}
