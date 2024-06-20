using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Api.Retail.Abstractions;
using Vostok.Hotline.Api.Retail.ApiServices;
using Vostok.Hotline.Api.Retail.Managers;

namespace Vostok.Hotline.Api.Retail.Bootstrappers
{
	public static class MoneyBoxBootstrapperExtension
	{
		public static void AddApiMoneyBoxRules(this IServiceCollection services)
		{
			services.AddSingleton<IMoneyBoxApiService, MoneyBoxApiService>(serviceProvider =>
			 {
				 return new MoneyBoxApiService(serviceProvider);
			 });
			services.AddSingleton<MoneyBoxApiManager>();
		}
	}
}
