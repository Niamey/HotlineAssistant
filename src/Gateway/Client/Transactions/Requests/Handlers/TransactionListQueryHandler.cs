using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Gateway.Client.Transactions.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Transactions.Services;
using Vostok.Hotline.Gateway.Client.Transactions.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Transactions.Requests.Handlers
{
	public class TransactionListQueryHandler : IRequestHandler<TransactionListQuery, SearchPagedResponseRowModel<TransactionViewModel>>
	{
		private readonly TransactionService _transactionService;

		public TransactionListQueryHandler(TransactionService transactionService)
		{
			_transactionService = transactionService;
		}

		public async Task<SearchPagedResponseRowModel<TransactionViewModel>> Handle(TransactionListQuery searchRequest, CancellationToken cancellationToken)
		{
			return await _transactionService.GetTransactionListAsync(searchRequest, cancellationToken);
		}
	}
}
