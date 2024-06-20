using System;
using System.Collections.Generic;
using System.Text;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Enums.Transactions;
using Vostok.Hotline.Data.EF.Entities.Solar;

namespace Vostok.Hotline.Data.Repository.Solar.Models.Responses
{
	public class TransactionResponseModel : ResponseBaseModel
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
		
		public string AuthCode { get; set; }
		public string RetRefNumber { get; set; }
		public decimal? TxnAmount { get; set; }
		public string TxnCurrency { get; set; }
		//public string Attributes { get; set; }

		public string TxnDetails { get; set; }

		public string ResponseCode { get; set; }

		public string Mcc { get; set; }
		public string TerminalId { get; set; }
		public string Merchant { get; set; }
		public string MerchantName { get; set; }
		public string MerchantCountry { get; set; }
		public string MerchantCity { get; set; }
		public string MerchantState { get; set; }
		
		public long? OrigCardId { get; set; }
		public string OrigCardNum { get; set; }
		public long? OrigAgreementId { get; set; }
		public string OrigAgreementNum { get; set; }
		public long? OrigClientId { get; set; }
		public decimal? OrigAmount { get; set; }
		public string OrigCurrency { get; set; }
		public decimal? OrigBillingAmount { get; set; }
		public string OrigBillingCurrency { get; set; }

		public long? RcvrCardId { get; set; }
		public string RcvrCardNum { get; set; }
		public long? RcvrAgreementId { get; set; }
		public string RcvrAgreementNum { get; set; }
		public long? RcvrClientId { get; set; }
		public decimal? RcvrAmount { get; set; }
		public string RcvrCurrency { get; set; }
		public decimal? RcvrBillingAmount { get; set; }
		public string RcvrBillingCurrency { get; set; }

		public long? CardId { get; set; }
		public string CardNum { get; set; }
		public long? AgreementId { get; set; }
		public string AgreementNum { get; set; }
		public long? ClientId { get; set; }
		public decimal? Amount { get; set; }
		public string Currency { get; set; }
		public decimal? BillingAmount { get; set; }
		public string BillingCurrency { get; set; }

		public decimal? FeeAmount { get; set; } // нет в SolarTransaction
		public string FeeCurrency { get; set; } // нет в SolarTransaction 

		public decimal? AvailableAmount { get; set; } // нет в SolarTransaction
		public string AvailableCurrency { get; set; } // нет в SolarTransaction

		public TransactionFeeCollectionResponseModel Fees { get; set; }
		public TransactionChildCollectionResponseModel Childs { get; set; }

		public TransactionOriginalDataResponseModel OriginalTxnData { get; set; }		
	}
}
