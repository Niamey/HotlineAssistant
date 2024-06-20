using Vostok.Hotline.Core.Common.Helpers;
using Vostok.Hotline.Core.Data.Abstractions;
using Vostok.Hotline.Core.Data.Contexts;

namespace Vostok.Hotline.Data.EF.DbStorage
{
	public class HotlineBaseTransactionFactory<THotlineContext> : ITransactionFactory
		where THotlineContext : DbBaseContext
	{
		protected readonly THotlineContext _dbContext;

		public HotlineBaseTransactionFactory(THotlineContext dbContext)
		{
			_dbContext = Assure.ArgumentNotNull(dbContext, nameof(dbContext));
		}

		public virtual ITransaction Create()
		{
			return HotlineTransaction.Create();
		}

		public virtual ITransaction CreateReadUncommitted()
		{
			return HotlineTransaction.CreateReadUncommitted();
		}
	}
}