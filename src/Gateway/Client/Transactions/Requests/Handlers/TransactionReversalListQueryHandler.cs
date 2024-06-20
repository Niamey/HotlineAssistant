using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Gateway.Client.Transactions.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Transactions.Services;
using Vostok.Hotline.Gateway.Client.Transactions.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Transactions.Requests.Handlers
{
	public class TransactionReversalListQueryHandler : IRequestHandler<TransactionReversalListQuery, TransactionViewModel>
	{
		private readonly TransactionService _transactionService;

		public TransactionReversalListQueryHandler(TransactionService transactionService)
		{
			_transactionService = transactionService;
		}

		public async Task<TransactionViewModel> Handle(TransactionReversalListQuery searchRequest, CancellationToken cancellationToken)
		{
			return await _transactionService.GetTransactionReversalListAsync(searchRequest, cancellationToken);
		}
	}
}
