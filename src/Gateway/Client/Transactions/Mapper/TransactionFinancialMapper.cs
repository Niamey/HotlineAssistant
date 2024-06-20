using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.References.Managers;
using Vostok.Hotline.Api.Retail.Models.Transactions;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Conversions;
using Vostok.Hotline.Data.Repository.Solar.Models.Responses;
using Vostok.Hotline.Gateway.Client.Transactions.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Transactions.Mapper
{
	public class TransactionFinancialMapper
	{
		private readonly CurrencyApiManager _currencyManager;

		public TransactionFinancialMapper(CurrencyApiManager currencyManager)
		{
			_currencyManager = currencyManager;
		}

		public async Task<TransactionFinancialViewModel> MapToTransactionFinancialViewModelAsync(TransactionResponseModel response, CancellationToken cancellationToken)
		{
			var result = new TransactionFinancialViewModel
			{
				RowId = response.TxnId,
				TxnId = response.TxnId,

				TransactionDate = response.TxnDate.Value,
				TxnStatus = response.Status,

				TransactionTypeName = $"{response.TxnTypeName} {response.TxnTypeDescription}",
				TransactionAmount = response.TxnAmount ?? 0,
				TransactionCurrency = await _currencyManager.GetCurrencyShortNameAsync(response.TxnCurrency, cancellationToken),

				MerchantName = response.MerchantName,
				MerchantCity = response.MerchantCity,
				MerchantState = response.MerchantState,
				MerchantCountry = response.MerchantCountry,

				AgreementId = Converter.ToInt64(response.AgreementId, true),
				AgreementNum = response.AgreementNum,
				CardId = Converter.ToInt64(response.CardId, true),
				CardNum = response.CardNum,

				BillingAmount = Converter.ToDecimal(response.BillingAmount),
				BillingCurrency = await _currencyManager.GetCurrencyShortNameAsync(response.BillingCurrency, cancellationToken),
			};		
			
			return result;
		}

		public async Task<SearchPagedResponseRowModel<TransactionFinancialViewModel>> MapToTransactionFinancialListViewModelAsync(SearchPagedResponseRowModel<TransactionResponseModel> response, CancellationToken cancellationToken)
		{
			var result = new SearchPagedResponseRowModel<TransactionFinancialViewModel>
			{
				Rows = new List<TransactionFinancialViewModel>(),
				IsNextPageAvailable = response.IsNextPageAvailable
			};

			foreach (var row in response.Rows)
			{
				var transaction = await MapToTransactionFinancialViewModelAsync(row, cancellationToken);
				result.Rows.Add(transaction);
			}

			return result;
		}

	}
}
