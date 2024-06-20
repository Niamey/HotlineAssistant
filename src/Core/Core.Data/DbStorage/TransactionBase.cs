using System.Runtime.CompilerServices;
using System.Transactions;
using Vostok.Hotline.Core.Data.Abstractions;

[assembly: InternalsVisibleTo("Hotline.Data.EF")]

namespace Vostok.Hotline.Core.Data.DbStorage
{
	internal class TransactionBase : ITransaction
	{
		private TransactionScope _transactionScope;

		protected internal TransactionBase(TransactionScope transactionScope)
		{
			_transactionScope = transactionScope;
		}

		internal static ITransaction Create()
		{
			var option = new TransactionOptions
			{
				IsolationLevel = IsolationLevel.ReadCommitted
			};

			var scope = new TransactionScope(TransactionScopeOption.Required, option, TransactionScopeAsyncFlowOption.Enabled);

			return new TransactionBase(scope);
		}

		public static TransactionBase CreateReadUncommitted()
		{
			var option = new TransactionOptions
			{
				IsolationLevel = IsolationLevel.ReadUncommitted
			};

			var scope = new TransactionScope(TransactionScopeOption.Required, option, TransactionScopeAsyncFlowOption.Enabled);

			return new TransactionBase(scope);
		}

		public void Complete()
		{
			_transactionScope?.Complete();
		}

		public void Dispose()
		{
			_transactionScope?.Dispose();
			_transactionScope = null;
		}
	}
}
