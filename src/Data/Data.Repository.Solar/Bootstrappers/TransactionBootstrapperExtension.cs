using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Data.Repository.Solar.Managers;

namespace Vostok.Hotline.Data.Repository.Solar.Bootstrappers
{
	public static class TransactionBootstrapperExtension
	{
		public static void AddRepositoryTransactionsRules(this IServiceCollection services)
		{
			services.AddSingleton<TransactionManager>();
			services.AddSingleton<AgreementManager>();
			services.AddSingleton<TransactionFeeManager>();
		}
	}
}
