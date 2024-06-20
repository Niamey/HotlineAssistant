using System.Transactions;
using Vostok.Hotline.Core.Data.DbStorage;

namespace Vostok.Hotline.Data.EF.DbStorage
{
	internal class HotlineTransaction : TransactionBase
	{
		protected internal HotlineTransaction(TransactionScope transactionScope)
			: base(transactionScope)
		{
		}
	}
}