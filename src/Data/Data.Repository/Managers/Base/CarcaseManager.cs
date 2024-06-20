using System;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Core.Common.Base.Abstractions;
using Vostok.Hotline.Core.Common.Helpers;
using Vostok.Hotline.Core.Data.Abstractions;
using Vostok.Hotline.Core.Data.Contexts;

namespace Vostok.Hotline.Data.Repository.Managers.Base
{
	public abstract class CarcaseManager<TContext>
		where TContext : DbBaseContext
	{
		protected readonly TContext DbContext;
		protected readonly ISessionContext SessionContext;
		protected readonly ITransactionFactory TransactionFactory;
		protected readonly IServiceProvider ServiceProvider;

		public CarcaseManager(IServiceProvider serviceProvider)
		{
			ServiceProvider = Assure.ArgumentNotNull(serviceProvider, nameof(serviceProvider));
			DbContext = ServiceProvider.GetRequiredService<TContext>();
			TransactionFactory = ServiceProvider.GetRequiredService<ITransactionFactory>();
			SessionContext = ServiceProvider.GetRequiredService<ISessionContext>();
		}
	}
}