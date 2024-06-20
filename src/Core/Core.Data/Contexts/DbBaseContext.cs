using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vostok.Hotline.Core.Common.Base.Abstractions;
using Vostok.Hotline.Core.Data.Exceptions;
using Vostok.Hotline.Core.Data.Helpers;

namespace Vostok.Hotline.Core.Data.Contexts
{
	public abstract class DbBaseContext : DbContext
	{
		protected ISessionContext SessionContext;
		private bool _isMigrationContext = false;
		private bool _saveSessionContextChanges = false;

		public DbBaseContext(DbContextOptions options)
			: base(options)
		{
		}

		internal static TContext CreateMigrationContext<TContext>(DbContextOptions<TContext> options)
			where TContext : DbBaseContext
		{
			var dbContext = Activator.CreateInstance(typeof(TContext), options) as TContext;
			dbContext._isMigrationContext = true;

			return dbContext;
		}

		public override int SaveChanges()
		{
			if (_isMigrationContext)
				return base.SaveChanges();

			throw new NotSupportedException("The application allows to use only async methods");
		}

		public override int SaveChanges(bool acceptAllChangesOnSuccess)
		{
			if (_isMigrationContext)
				return base.SaveChanges(acceptAllChangesOnSuccess);

			throw new NotSupportedException("The application allows to use only async methods");
		}

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			if (_isMigrationContext || _saveSessionContextChanges)
				return base.SaveChangesAsync(cancellationToken);

			throw new NotSupportedException($"The application allows to use only async methods {nameof(SaveChangesAsync)} with SessionContext");
		}

		public Task<int> SaveChangesAsync(ISessionContext sessionContext, CancellationToken cancellationToken = default)
		{
			_saveSessionContextChanges = true;
			if (!_isMigrationContext)
			{
				if (!EfTransactionHelper.IsTransactionExists())
				{
					throw new TransactionNotExistsException("The saving must be executed inside a transaction");
				}

				ApplyAudit(sessionContext, this);
			}

			return base.SaveChangesAsync(cancellationToken);
		}

		public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
		{
			if (_isMigrationContext || _saveSessionContextChanges)
				return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

			throw new NotSupportedException($"The application allows to use only async methods {nameof(SaveChangesAsync)} with SessionContext");
		}

		protected abstract void ApplyAudit(ISessionContext sessionContext, DbBaseContext dbBaseContext);
	}
}
