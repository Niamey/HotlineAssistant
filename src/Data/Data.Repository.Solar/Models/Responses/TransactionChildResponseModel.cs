using System;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Enums.Transactions;

namespace Vostok.Hotline.Data.Repository.Solar.Models.Responses
{
	public class TransactionChildResponseModel : ResponseBaseModel
	{
		public long TxnId { get; set; }
		public TxnStatusEnum Status { get; set; }
		public string TxnTypeCode { get; set; }
		public string TxnTypeName { get; set; }
		public string TxnTypeDescription { get; set; }
		public DirectionClassEnum TxnTypeDirection { get; set; }
		public ClassEnum TxnClass { get; set; }
		public CategoryEnum TxnCategory { get; set; }
		public DirectionEnum TxnDirection { get; set; }
		public DateTime? TxnDate { get; set; }
		public decimal? TxnAmount { get; set; }
		public string TxnCurrency { get; set; }
		public string TxnDetails { get; set; }
		public string ResponseCode { get; set; }

		public long? OrigAgreementId { get; set; }
		public long? OrigClientId { get; set; }
		public long? RcvrAgreementId { get; set; }
		public long? RcvrClientId { get; set; }

		public long? AgreementId { get; set; }
		public long? ClientId { get; set; }

		public decimal? FeeAmount { get; set; } // нет в SolarTransaction
		public string FeeCurrency { get; set; } // нет в SolarTransaction 
		public TransactionFeeCollectionResponseModel Fees { get; set; }
	}
}