using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Api.References.Abstractions;
using Vostok.Hotline.Api.References.ApiServices;
using Vostok.Hotline.Api.References.Managers;

namespace Vostok.Hotline.Api.References.Bootstrappers
{
	public static class CountryBootstrapperExtension
	{
		public static void AddApiCountryRules(this IServiceCollection services)
		{
			services.AddSingleton<ICountryApiService, CountryApiService>(serviceProvider => 
			{
				return new CountryApiService(serviceProvider); 
			});
			services.AddSingleton<CountryApiManager>();
		}
	}
}
