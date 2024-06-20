using System;
using System.Collections.Generic;
using System.Text;
using Vostok.Hotline.Core.Common.Conversions;
using Vostok.Hotline.Data.EF.Entities.Solar;
using Vostok.Hotline.Data.Repository.Solar.Models.Responses;

namespace Vostok.Hotline.Data.Repository.Solar.Mappers
{
	public static class TransactionChildMapperExtensions
	{
		public static TransactionChildResponseModel ToTransactionChildResponseModel(this SolarTransactionQuery response)
		{
			return new TransactionChildResponseModel
			{
				TxnId = Converter.ToInt64(response.ChildId),
				Status = response.ChildStatus.ToTxnStatusEnum(),
				TxnTypeCode = response.ChildTxnTypeCode,
				TxnTypeName = response.ChildTxnTypeName,
				TxnTypeDescription = response.ChildTxnTypeDescription,
				TxnTypeDirection = response.ChildTxnTypeDirection.ToDirectionClassEnum(),
				TxnClass = response.ChildTxnClass.ToClassEnum(),
				TxnCategory = response.ChildTxnCategory.ToCategoryEnum(),
				TxnDirection = response.ChildTxnDirection.ToDirectionEnum(),
				TxnDate = response.ChildTxnDate,
				TxnAmount = response.ChildTxnAmount,
				TxnCurrency = response.ChildTxnCurrency,
				TxnDetails = response.ChildTxnDetails,
				ResponseCode = response.ChildResponseCode,
				OrigAgreementId = Converter.ToInt64(response.OrigAgreementId),
				OrigClientId = Converter.ToInt64(response.OrigClientId),
				RcvrAgreementId = Converter.ToInt64(response.RcvrAgreementId),
				RcvrClientId = Converter.ToInt64(response.RcvrClientId)
			};
		}
	}
}
