using System;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Retail.Abstractions;
using Vostok.Hotline.Api.Retail.Models.Transactions;

namespace Vostok.Hotline.Api.Retail.Managers
{
	public class TransactionApiManager
	{
		private readonly ITransactionApiService _transactionApiService;
		public TransactionApiManager(ITransactionApiService transactionApiService)
		{
			_transactionApiService = transactionApiService;
		}

		public async Task<TransactionCollectionApiModel> GetTransactionCollectionAsync(long clientId, DateTime? dateFrom, DateTime? dateTo, int pageIndex, int pageSize, CancellationToken cancellationToken)
		{
			return await _transactionApiService.GetTransactionCollectionAsync(clientId, dateFrom, dateTo, pageIndex, pageSize, cancellationToken);
		}		
	}
}
