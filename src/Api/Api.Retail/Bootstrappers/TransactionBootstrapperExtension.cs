using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Api.Retail.Abstractions;
using Vostok.Hotline.Api.Retail.ApiServices.Transactions;
using Vostok.Hotline.Api.Retail.Managers;

namespace Vostok.Hotline.Api.Retail.Bootstrappers
{
	public static class TransactionBootstrapperExtension
	{
		public static void AddApiTransactionRules(this IServiceCollection services)
		{
			services.AddSingleton<ITransactionApiService, TransactionApiService>(serviceProvider =>
			{
				return new TransactionApiService(serviceProvider);
			});
			services.AddSingleton<TransactionApiManager>();
		}
	}
}
