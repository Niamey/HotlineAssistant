using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Api.Services.Abstractions;
using Vostok.Hotline.Api.Services.ApiServices;
using Vostok.Hotline.Api.Services.Managers;
using Vostok.Hotline.Api.Services.ProviderClients;
using Vostok.Hotline.Api.Services.ProviderClients.Configurations;
using Vostok.Hotline.Api.Services.Providers;
using Vostok.HttpClient.DependencyInfection;

namespace Vostok.Hotline.Api.Services.Bootstrappers
{
	public static class ProviderClientExtension
	{
		public static void AddServiceProviderClientRules(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddIdentedHttpClient<RetailSystemCardShirtConfig>(configuration);
			services.AddSingleton<RetailSystemCardShirtProviderClient>();
		}
	}
}
