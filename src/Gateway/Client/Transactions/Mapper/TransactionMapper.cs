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
using Vostok.Hotline.Data.Repository.References.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace Vostok.Hotline.Gateway.Client.Transactions.Mapper
{
	public class TransactionMapper
	{
		private readonly CurrencyApiManager _currencyManager;
		private readonly TransactionFeeMapper _transactionFeeMapper;
		private readonly RefTransactionCodeManager _refTransactionCodeManager;
		private readonly TransactionChildMapper _transactionChildMapper;

		public TransactionMapper(IServiceProvider serviceProvider)
		{
			_currencyManager = serviceProvider.GetRequiredService<CurrencyApiManager>();
			_transactionFeeMapper = serviceProvider.GetRequiredService<TransactionFeeMapper>();
			_refTransactionCodeManager = serviceProvider.GetRequiredService<RefTransactionCodeManager>();
			_transactionChildMapper = serviceProvider.GetRequiredService<TransactionChildMapper>();
		}
		
		public async Task<TransactionViewModel> MapToTransactionViewModelAsync(TransactionResponseModel response, CancellationToken cancellationToken)
		{
			var result = new TransactionViewModel
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

				AuthCode = response.AuthCode,
				Rrn = response.RetRefNumber,
				TransactionDetails = response.TxnDetails,

				Merchant = response.Merchant,
				MerchantName = response.MerchantName,
				MerchantCity = response.MerchantCity,
				MerchantState = response.MerchantState,
				MerchantCountry = response.MerchantCountry,
				Mcc = response.Mcc,
				TerminalId = response.TerminalId,

				TransactionAmount = response.TxnAmount ?? 0,
				TransactionCurrency = await _currencyManager.GetCurrencyShortNameAsync(response.TxnCurrency, cancellationToken),

				AgreementId = Converter.ToInt64(response.AgreementId, true),
				AgreementNum = response.AgreementNum,
				CardId = Converter.ToInt64(response.CardId, true),
				CardNum = response.CardNum,

				BillingAmount = Converter.ToDecimal(response.BillingAmount),
				BillingCurrency = await _currencyManager.GetCurrencyShortNameAsync(response.BillingCurrency, cancellationToken),

				FeeAmount = Converter.ToDecimal(response.FeeAmount),
				FeeCurrency = await _currencyManager.GetCurrencyShortNameAsync(response.FeeCurrency, cancellationToken),

				AvailableAmount = Converter.ToDecimal(response.AvailableAmount),
				AvailableCurrency = await _currencyManager.GetCurrencyShortNameAsync(response.AvailableCurrency, cancellationToken),
			};		
						
			if (response.Fees != null)
			{
				result.Fees = await _transactionFeeMapper.MapToTransactionFeeCollectionViewModelAsync(response.Fees, cancellationToken);
			}
			
			
			var rc = await _refTransactionCodeManager.GetByCodeAsync(response.ResponseCode, cancellationToken);
			if (rc != null) {
				var rcDescription = String.IsNullOrEmpty(rc.Description) ? rc.Rc : rc.Description;
				result.ResponseCode = $"{rc.Code} - {rcDescription}";
			} else {
				result.ResponseCode = "Прочие отказы";
			}

			if (response.Childs != null)
			{
				result.Childs = await _transactionChildMapper.MapToTransactionChildCollectionViewModelAsync(response.Childs, cancellationToken);
			}

			if (response.OriginalTxnData != null)
			{
				result.AcqInstitutionCode = response.OriginalTxnData.AcqInstitutionCode;
				result.CardDataInputMode = response.OriginalTxnData.CardDataInputMode;
				result.TerminalType = response.OriginalTxnData.TerminalType;
				result.PinVerification = response.OriginalTxnData.OnlinePinVerificationResult;
				result.Cvv2Verification = response.OriginalTxnData.Cvv2VerificationResult;
				result.CavvValidation = response.OriginalTxnData.CavvValidationResult;
				result.Stan = response.OriginalTxnData.Stan;
			}

			return result;
		}

		public async Task<SearchPagedResponseRowModel<TransactionViewModel>> MapToTransactionListViewModelAsync(SearchPagedResponseRowModel<TransactionResponseModel> response, CancellationToken cancellationToken)
		{
			var result = new SearchPagedResponseRowModel<TransactionViewModel>
			{
				Rows = new List<TransactionViewModel>(),
				IsNextPageAvailable = response.IsNextPageAvailable
			};

			foreach (var row in response.Rows)
			{
				var transaction = await MapToTransactionViewModelAsync(row, cancellationToken);
				result.Rows.Add(transaction);
			}

			return result;
		}

	}
}
