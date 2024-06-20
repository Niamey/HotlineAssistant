using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Vostok.Hotline.Data.EF.Configurations;
using Vostok.Hotline.Data.EF.DbStorage.Solar;

namespace Vostok.Hotline.Data.EF.Bootstrappers
{
	public static class HotlineSolarEfBootstrapExtension
	{
		public static void AddHotlineSolarEfRules(this IServiceCollection services)
		{
			services.TryAddSingleton<HotlineSolarDbConnectionConfig>();
			var serviceProvider = services.BuildServiceProvider();

			services.AddDbContext<HotlineSolarContext>(options =>
			{
				options.UseSqlServer(serviceProvider.GetService<HotlineSolarDbConnectionConfig>().ConnectionString);				
			}, ServiceLifetime.Transient);

			services.TryAddTransient<HotlineSolarTransactionFactory>();
		}
	}
}
