using System;
using System.Collections.Generic;
using Vostok.Hotline.Core.Common.Enums.Transactions;
using Vostok.Hotline.Core.Data.Entities;

namespace Vostok.Hotline.Data.EF.Entities.Solar
{
	public class SolarTransactionQuery : EntityBase
	{
		public SolarTransactionQuery()
		{			
		}

		public long RowNumber { get; set; }
		public decimal ParentId { get; set; }
		public decimal? ChildId { get; set; }

		
		public string Status { get; set; }
		
		public decimal? TxnTypeId { get; set; }
		public string TxnTypeCode { get; set; }
		public string TxnTypeName { get; set; }
		public string TxnTypeDescription { get; set; }
		public string TxnTypeDirection { get; set; }
		public string TxnClass { get; set; }
		public string TxnCategory { get; set; }
		public string TxnDirection { get; set; }
		
		public DateTime? TxnDate { get; set; }
		
		public string AuthCode { get; set; }
		public string RetRefNumber { get; set; }
		public decimal? TxnAmount { get; set; }
		public string TxnCurrency { get; set; }
		//public string Attributes { get; set; }
		public string TxnDetails { get; set; }
		public string ResponseCode { get; set; }
		public string Mcc { get; set; }
		public string Merchant { get; set; }
		public string MerchantName { get; set; }
		public string MerchantCountry { get; set; }
		public string MerchantCity { get; set; }
		public string MerchantState { get; set; }

		public decimal? OrigCardId { get; set; }
		public string OrigCardNum { get; set; }
		public decimal? OrigAgreementId { get; set; }
		public string OrigAgreementNum { get; set; }
		public decimal? OrigClientId { get; set; }
		public decimal? OrigAmount { get; set; }
		public string OrigCurrency { get; set; }
		public decimal? OrigBillingAmount { get; set; }
		public string OrigBillingCurrency { get; set; }

		public decimal? RcvrCardId { get; set; }
		public string RcvrCardNum { get; set; }
		public decimal? RcvrAgreementId { get; set; }
		public string RcvrAgreementNum { get; set; }
		public decimal? RcvrClientId { get; set; }
		public decimal? RcvrAmount { get; set; }
		public string RcvrCurrency { get; set; }
		public decimal? RcvrBillingAmount { get; set; }
		public string RcvrBillingCurrency { get; set; }
			
		
		public string ChildStatus { get; set; }
		
		public decimal? ChildTxnTypeId { get; set; }
		public string ChildTxnTypeCode { get; set; }
		public string ChildTxnTypeName { get; set; }
		public string ChildTxnTypeDescription { get; set; }
		public string ChildTxnTypeDirection { get; set; }
		public string ChildTxnClass { get; set; }
		public string ChildTxnCategory { get; set; }
		public string ChildTxnDirection { get; set; }
		
		public DateTime? ChildTxnDate { get; set; }
		
		public decimal? ChildTxnAmount { get; set; }
		public string ChildTxnCurrency { get; set; }
		public string ChildTxnDetails { get; set; }
		public string ChildResponseCode { get; set; }

		public decimal? ChildOrigAgreementId { get; set; }
		public decimal? ChildOrigClientId { get; set; }
		public decimal? ChildRcvrAgreementId { get; set; }
		public decimal? ChildRcvrClientId { get; set; }

		// public string OriginalTxnData { get; set; }

		public string AcqInstitutionCode { get; set; }
		public string TerminalType { get; set; }
		public string CardDataInputMode { get; set; }
		public string OnlinePinVerificationResult { get; set; }
		public string Cvv2VerificationResult { get; set; }
		public string CavvValidationResult { get; set; }
		public string Stan { get; set; }
	}
}
