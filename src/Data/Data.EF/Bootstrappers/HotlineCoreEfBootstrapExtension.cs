using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Vostok.Hotline.Core.Common.Data.Abstractions;
using Vostok.Hotline.Core.Data.Abstractions;
using Vostok.Hotline.Data.EF.Configurations;
using Vostok.Hotline.Data.EF.DbStorage.Core;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Vostok.Hotline.Data.EF.Bootstrappers
{
	public static class HotlineCoreEfBootstrapExtension
	{
		public static void AddHotlineCoreEfRules(this IServiceCollection services)
		{
			services.TryAddSingleton<IDbConnectionConfig, HotlineDbConnectionConfig>();
			var serviceProvider = services.BuildServiceProvider();

			services.AddDbContext<HotlineCoreContext>(options =>
			{
				options.UseSqlServer(serviceProvider.GetService<IDbConnectionConfig>().ConnectionString);
			}, ServiceLifetime.Transient);

			services.TryAddTransient<ITransactionFactory, HotlineCoreTransactionFactory>();			
		}
	}
}
