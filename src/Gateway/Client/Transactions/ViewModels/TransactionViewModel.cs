using System;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Enums.Transactions;

namespace Vostok.Hotline.Gateway.Client.Transactions.ViewModels
{
	public class TransactionViewModel : ResponseBaseModel
	{
		public long RowId { get; set; }
		public long TxnId { get; set; }

		public TxnStatusEnum? TxnStatus { get; set; }

		//public EntryTypeEnum EntryType { get; set; }
		public ClassEnum? Class { get; set; }
		public CategoryEnum? Category { get; set; }
		public DirectionEnum? Direction { get; set; }
		public DirectionClassEnum? DirectionClass { get; set; }

		public string TransactionTypeName { get; set; }

		public DateTime TransactionDate { get; set; }

		public decimal TransactionAmount { get; set; }
		public string TransactionCurrency { get; set; }

		public string AuthCode { get; set; }
		public string Rrn { get; set; }
		public string ResponseCode { get; set; }

		public string TransactionDetails { get; set; }

		public decimal BillingAmount { get; set; }
		public string BillingCurrency { get; set; }

		public string Merchant { get; set; }
		public string MerchantName { get; set; }
		public string MerchantCity { get; set; }
		public string MerchantState { get; set; }
		public string MerchantCountry { get; set; }
		public string Mcc { get; set; }
		public string TerminalId { get; set; }
		
		public long CardId { get; set; }
		public string CardNum { get; set; }
		public long AgreementId { get; set; }
		public string AgreementNum { get; set; }

		public decimal FeeAmount { get; set; }
		public string FeeCurrency { get; set; }

		public decimal AvailableAmount { get; set; }
		public string AvailableCurrency { get; set; }

		public TransactionFeeCollectionViewModel Fees { get; set; }

		public TransactionChildCollectionViewModel Childs { get; set; }

		public string AcqInstitutionCode { get; set; }
		public string CardDataInputMode { get; set; }
		public string TerminalType { get; set; }
		public string PinVerification { get; set; }
		public string Cvv2Verification { get; set; }
		public string CavvValidation { get; set; }
		public string Stan { get; set; }
	}

}