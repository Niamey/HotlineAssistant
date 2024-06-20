using System;
using System.Collections.Generic;
using System.Linq;
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
	public class TransactionFeeMapper
	{
		private readonly CurrencyApiManager _currencyManager;

		public TransactionFeeMapper(CurrencyApiManager currencyManager)
		{
			_currencyManager = currencyManager;
		}
		
		public async Task<TransactionFeeViewModel> MapToTransactionFeeViewModelAsync(TransactionFeeResponseModel response, CancellationToken cancellationToken)
		{
			var result = new TransactionFeeViewModel
			{
				FeeId = response.FeeId,
				TxnId = response.TxnId,
				FeeTypeName = $"{response.TxnTypeName} {response.TxnTypeDescription}",
				FeeAmount = Converter.ToDecimal(response.FeeAmount),
				FeeCurrency = await _currencyManager.GetCurrencyShortNameAsync(response.FeeCurrency, cancellationToken),
				BillingAmount = Converter.ToDecimal(response.BillingAmount),
				BillingCurrency = await _currencyManager.GetCurrencyShortNameAsync(response.BillingCurrency, cancellationToken),
			};

			return result;
		}

		public async Task<TransactionFeeCollectionViewModel> MapToTransactionFeeCollectionViewModelAsync(TransactionFeeCollectionResponseModel response, CancellationToken cancellationToken)
		{
			var result = new TransactionFeeCollectionViewModel();
			foreach (var row in response)
			{
				var fee = await MapToTransactionFeeViewModelAsync(row, cancellationToken);
				result.Add(fee);
			}

			return result;
		}

	}
}
