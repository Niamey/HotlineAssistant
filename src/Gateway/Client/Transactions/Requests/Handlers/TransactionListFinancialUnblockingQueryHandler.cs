using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Core.Common.Base.Abstractions.Requests;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Data.Repository.Solar.Models.Request;
using Vostok.Hotline.Gateway.Client.Transactions.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Transactions.Services;
using Vostok.Hotline.Gateway.Client.Transactions.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Transactions.Requests.Handlers
{
	public class TransactionListFinancialUnblockingQueryHandler : IRequestHandler<TransactionListFinancialUnblockingQuery, SearchPagedResponseRowModel<TransactionFinancialViewModel>>
	{
		private readonly TransactionService _transactionService;

		public TransactionListFinancialUnblockingQueryHandler(TransactionService transactionService)
		{
			_transactionService = transactionService;
		}

		public async Task<SearchPagedResponseRowModel<TransactionFinancialViewModel>> Handle(TransactionListFinancialUnblockingQuery searchRequest, CancellationToken cancellationToken)
		{
			var request = new TransactionSearchRequest
			{
				ClientId = searchRequest.SolarId,
				Paging = new SearchPagingModel
				{
					CurrentPage = 1,
					PageSize = searchRequest.PageSize.Value
				}
			};
			return await _transactionService.GetTransactionListFinancialAsync(request, cancellationToken);
		}
	}
}
