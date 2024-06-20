using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Api.Retail.ApiServices.Abstractions;
using Vostok.Hotline.Api.Retail.ApiServices.Agreements;
using Vostok.Hotline.Api.Retail.Managers;

namespace Vostok.Hotline.Api.Retail.Bootstrappers
{
	public static class AgreementBootstrapperExtension
	{
		public static void AddApiAgreementRules(this IServiceCollection services)
		{
			services.AddSingleton<IAgreementApiService, AgreementApiService>(serviceProvider => 
			{
				return new AgreementApiService(serviceProvider); 
			});
			services.AddSingleton<AgreementApiManager>();

			services.AddSingleton<IAgreementHistoryApiService, AgreementHistoryApiService>(serviceProvider =>
			{
				return new AgreementHistoryApiService(serviceProvider);
			});
			services.AddSingleton<AgreementHistoryApiManager>();

			services.AddSingleton<IAgreementTariffApiService, AgreementTariffApiService>(serviceProvider =>
			{
				return new AgreementTariffApiService(serviceProvider);
			});
			services.AddSingleton<AgreementTariffApiManager>();
		}
	}
}
