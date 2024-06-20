using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Api.Retail.ProviderClients;
using Vostok.Hotline.Api.Retail.ProviderClients.Configurations;
using Vostok.HttpClient.DependencyInfection;

namespace Vostok.Hotline.Api.Retail.Bootstrappers
{
	public static class ProviderClientExtension
	{
		public static void AddRetailSystemProviderClientRules(this IServiceCollection services, IConfiguration configuration)
		{			
			services.AddIdentedHttpClient<RetailSystemInformationConfig>(configuration);
			services.AddIdentedHttpClient<RetailSystemManagementConfig>(configuration);
			
			services.AddSingleton<RetailSystemInformationProviderClient>();
			services.AddSingleton<RetailSystemManagementProviderClient>();
		}
	}
}
