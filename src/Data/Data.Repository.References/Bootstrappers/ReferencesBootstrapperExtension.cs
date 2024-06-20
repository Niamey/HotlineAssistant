using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Data.Repository.References.Managers;

namespace Vostok.Hotline.Data.Repository.References.Bootstrappers
{
	public static class ReferencesBootstrapperExtension
	{
		public static void AddRepositoryReferencesRules(this IServiceCollection services)
		{
			services.AddTransient<RefTransactionCodeManager>();
			services.AddTransient<CountryManager>();
		}
	}
}
