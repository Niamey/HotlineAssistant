using System;
using Vostok.Hotline.Core.Common.Conversions;
using Vostok.Hotline.Data.EF.Entities.Solar;
using Vostok.Hotline.Data.Repository.Solar.Models.Responses;

namespace Vostok.Hotline.Data.Repository.Solar.Mappers
{
	public static class TransactionMapperExtensions
	{
		public static TransactionResponseModel ToTransactionResponseModel(this SolarTransactionQuery response)
		{
			var result = new TransactionResponseModel
			{
				TxnId = Converter.ToInt64(response.ParentId),
				Status = response.Status.ToTxnStatusEnum(),
				TxnTypeCode = response.TxnTypeCode,
				TxnTypeName = response.TxnTypeName,
				TxnTypeDescription = response.TxnTypeDescription,
				TxnTypeDirection = response.TxnTypeDirection.ToDirectionClassEnum(),
				TxnClass = response.TxnClass.ToClassEnum(),
				TxnCategory = response.TxnCategory.ToCategoryEnum(),
				TxnDirection = response.TxnDirection.ToDirectionEnum(),
				TxnDate = response.TxnDate,
				AuthCode = response.AuthCode,
				RetRefNumber = response.RetRefNumber,
				TxnAmount = response.TxnAmount,
				TxnCurrency = response.TxnCurrency,
				TxnDetails = response.TxnDetails,
				ResponseCode = response.ResponseCode,
				Mcc = response.Mcc,
				Merchant = response.Merchant,
				TerminalId = !String.IsNullOrEmpty(response.Merchant) ? response.OrigCardNum : "",
				MerchantName = response.MerchantName,
				MerchantCountry = response.MerchantCountry,
				MerchantCity = response.MerchantCity,
				MerchantState = response.MerchantState,

				OrigCardId = Converter.ToInt64(response.OrigCardId),
				OrigCardNum = response.OrigCardNum,
				OrigAgreementId = Converter.ToInt64(response.OrigAgreementId),
				OrigAgreementNum = response.OrigAgreementNum,
				OrigClientId = Converter.ToInt64(response.OrigClientId),
				OrigAmount = response.OrigAmount,
				OrigCurrency = response.OrigCurrency,
				OrigBillingAmount = response.OrigBillingAmount,
				OrigBillingCurrency = response.OrigBillingCurrency,

				RcvrCardId = Converter.ToInt64(response.RcvrCardId),
				RcvrCardNum = response.RcvrCardNum,
				RcvrAgreementId = Converter.ToInt64(response.RcvrAgreementId),
				RcvrAgreementNum = response.RcvrAgreementNum,
				RcvrClientId = Converter.ToInt64(response.RcvrClientId),
				RcvrAmount = response.RcvrAmount,
				RcvrCurrency = response.RcvrCurrency,
				RcvrBillingAmount = response.RcvrBillingAmount,
				RcvrBillingCurrency = response.RcvrBillingCurrency,

				OriginalTxnData = new TransactionOriginalDataResponseModel
				{
					AcqInstitutionCode = response.AcqInstitutionCode,
					CardDataInputMode = response.CardDataInputMode,
					TerminalType = response.TerminalType,
					OnlinePinVerificationResult = response.OnlinePinVerificationResult,
					Cvv2VerificationResult = response.Cvv2VerificationResult,
					CavvValidationResult = response.CavvValidationResult,
					Stan = response.Stan
				}
			};			

			return result;
		}
	}
}
