using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Vostok.Hotline.Data.EF.Configurations;
using Vostok.Hotline.Data.EF.DbStorage.References;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Vostok.Hotline.Data.EF.Bootstrappers
{
	public static class HotlineReferencesEfBootstrapExtension
	{
		public static void AddHotlineReferencesEfRules(this IServiceCollection services)
		{
			services.TryAddSingleton<HotlineReferencesDbConnectionConfig>();
			var serviceProvider = services.BuildServiceProvider();

			services.AddDbContext<HotlineReferencesContext>(options =>
			{
				options.UseSqlServer(serviceProvider.GetService<HotlineReferencesDbConnectionConfig>().ConnectionString);
			}, ServiceLifetime.Transient);

			services.TryAddTransient<HotlineReferencesTransactionFactory>();
		}
	}
}
