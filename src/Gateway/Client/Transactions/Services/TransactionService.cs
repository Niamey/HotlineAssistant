using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Core.Common.Exeptions;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Gateway.Client.Transactions.ViewModels;
using Vostok.Hotline.Gateway.Client.Transactions.Mapper;
using Vostok.Hotline.Api.Retail.Managers;
using Vostok.Hotline.Gateway.Client.Transactions.Requests.Queries;
using Vostok.Hotline.Data.Repository.Solar.Managers;
using Vostok.Hotline.Data.Repository.Solar.Models.Request;
using Vostok.Hotline.Core.Common.Base.Abstractions.Requests;
using System.Linq;

namespace Vostok.Hotline.Gateway.Client.Transactions.Services
{
	public class TransactionService
	{
		private readonly TransactionApiManager _transactionApiManager;
		private readonly TransactionMapper _transactionMapper;
		private readonly TransactionFinancialMapper _transactionFinancialMapper;

		private readonly TransactionManager _transactionManager;

		public TransactionService(TransactionApiManager transactionApiManager, 
			TransactionMapper transactionMapper, 
			TransactionFinancialMapper transactionFinancialMapper,
			TransactionManager transactionManager)
		{
			_transactionApiManager = transactionApiManager;
			_transactionMapper = transactionMapper;
			_transactionFinancialMapper = transactionFinancialMapper;

			_transactionManager = transactionManager;
		}

		public async Task<SearchPagedResponseRowModel<TransactionViewModel>> GetTransactionListAsync(TransactionListQuery request, CancellationToken cancellationToken)
		{
			var result = await _transactionManager.SearchTransactionAsync(new TransactionSearchRequest
			{
				ClientId = request.SolarId,
				DateFrom = request.DateFrom,
				DateTo = request.DateTo,
				AmountFrom = request.AmountFrom,
				AmountTo = request.AmountTo,
				CardNumber = request.CardNumber,
				Paging = new SearchPagingModel
				{
					CurrentPage = request.PageIndex,
					PageSize = request.PageSize.Value
				}
			}, cancellationToken);
			
			if (result != null)
				return await _transactionMapper.MapToTransactionListViewModelAsync(result, cancellationToken);
			else
				throw new NotFoundRequestException();
		}

		public async Task<SearchPagedResponseRowModel<TransactionFinancialViewModel>> GetTransactionListFinancialAsync(TransactionSearchRequest request, CancellationToken cancellationToken)
		{
			var result = await _transactionManager.SearchTransactionAsync(request, cancellationToken);

			if (result != null)
				return await _transactionFinancialMapper.MapToTransactionFinancialListViewModelAsync(result, cancellationToken);
			else 
				throw new NotFoundRequestException();
		}

		public async Task<TransactionViewModel> GetTransactionReversalListAsync(TransactionReversalListQuery request, CancellationToken cancellationToken)
		{
			var result = await _transactionManager.SearchTransactionAsync(new TransactionSearchRequest { 
				ClientId = request.SolarId, 
				TxnId = request.TxnId 
			}, cancellationToken);

			if (result != null && result.Rows.Any())
				return await _transactionMapper.MapToTransactionViewModelAsync(result.Rows.FirstOrDefault(), cancellationToken);
			else
				throw new NotFoundRequestException();
		}
	}
}