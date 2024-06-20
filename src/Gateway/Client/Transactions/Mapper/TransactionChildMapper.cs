using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.References.Managers;
using Vostok.Hotline.Api.Retail.Models.Transactions;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Conversions;
using Vostok.Hotline.Data.Repository.References.Managers;
using Vostok.Hotline.Data.Repository.Solar.Models.Responses;
using Vostok.Hotline.Gateway.Client.Transactions.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Transactions.Mapper
{
	public class TransactionChildMapper
	{
		private readonly CurrencyApiManager _currencyManager;
		private readonly RefTransactionCodeManager _refTransactionCodeManager;
		private readonly TransactionFeeMapper _transactionFeeMapper;

		public TransactionChildMapper(CurrencyApiManager currencyManager, RefTransactionCodeManager refTransactionCodeManager, TransactionFeeMapper transactionFeeMapper)
		{
			_currencyManager = currencyManager;
			_refTransactionCodeManager = refTransactionCodeManager;
			_transactionFeeMapper = transactionFeeMapper;
		}
		
		public async Task<TransactionChildViewModel> MapToTransactionChildViewModelAsync(TransactionChildResponseModel response, CancellationToken cancellationToken)
		{
			var result = new TransactionChildViewModel
			{
				RowId = response.TxnId,
				TxnId = response.TxnId,
				Class = response.TxnClass,
				Category = response.TxnCategory,
				Direction = response.TxnDirection,
				DirectionClass = response.TxnTypeDirection,
				TxnStatus = response.Status,
				TransactionDate = response.TxnDate.Value,
				TransactionTypeName = $"{response.TxnTypeName} {response.TxnTypeDescription}",
				TransactionDetails = response.TxnDetails,
				TransactionAmount = response.TxnAmount ?? 0,
				TransactionCurrency = await _currencyManager.GetCurrencyShortNameAsync(response.TxnCurrency, cancellationToken),
				FeeAmount = Converter.ToDecimal(response.FeeAmount),
				FeeCurrency = await _currencyManager.GetCurrencyShortNameAsync(response.FeeCurrency, cancellationToken),
				
			};

			if (response.Fees != null)
			{
				result.Fees = await _transactionFeeMapper.MapToTransactionFeeCollectionViewModelAsync(response.Fees, cancellationToken);
			}

			var rc = await _refTransactionCodeManager.GetByCodeAsync(response.ResponseCode, cancellationToken);
			if (rc != null)
			{
				var rcDescription = String.IsNullOrEmpty(rc.Description) ? rc.Rc : rc.Description;
				result.ResponseCode = $"{rc.Code} - {rcDescription}";
			}
			else
			{
				result.ResponseCode = "Прочие отказы";
			}

			return result;
		}

		public async Task<TransactionChildCollectionViewModel> MapToTransactionChildCollectionViewModelAsync(TransactionChildCollectionResponseModel response, CancellationToken cancellationToken)
		{
			var result = new TransactionChildCollectionViewModel();
			foreach (var row in response)
			{
				var fee = await MapToTransactionChildViewModelAsync(row, cancellationToken);
				result.Add(fee);
			}

			return result;
		}

	}
}
