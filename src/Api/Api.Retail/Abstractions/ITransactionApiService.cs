using System;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Retail.Models.Transactions;

namespace Vostok.Hotline.Api.Retail.Abstractions
{
	public interface ITransactionApiService
	{
		Task<TransactionCollectionApiModel> GetTransactionCollectionAsync(long clientId, DateTime? dateFrom, DateTime? dateTo, int pageIndex, int pageSize, CancellationToken cancellationToken);
	}
}