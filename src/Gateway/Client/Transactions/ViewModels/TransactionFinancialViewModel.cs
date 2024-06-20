using System;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Enums.Transactions;

namespace Vostok.Hotline.Gateway.Client.Transactions.ViewModels
{
	public class TransactionFinancialViewModel : ResponseBaseModel
	{	
		public long RowId { get; set; }
		public long TxnId { get; set; }

		public long CardId { get; set; }
		public string CardNum { get; set; }

		public long AgreementId { get; set; }
		public string AgreementNum { get; set; }

		//public EntryTypeEnum EntryType { get; set; }

		public string TransactionTypeName { get; set; }

		public DateTime TransactionDate { get; set; }
			
		public decimal TransactionAmount { get; set; }
		public string TransactionCurrency { get; set; }

		public decimal BillingAmount { get; set; }
		public string BillingCurrency { get; set; }

		public string MerchantName { get; set; }
		public string MerchantCity { get; set; }
		public string MerchantState { get; set; }
		public string MerchantCountry { get; set; }
		
		public TxnStatusEnum? TxnStatus { get; set; }
	}

}