using System;
using Vostok.Hotline.Api.Retail.Responses.Transactions;
using Vostok.Hotline.Api.Retail.Responses.Transactions.Enums;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;

namespace Vostok.Hotline.Api.Retail.Responses.Transactions
{
	public class TransactionResponseModel : ResponseBaseModel { 
		/// <summary>
		/// 
		/// </summary>
		public long TxnId { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public EntryTypeRetailEnum EntryType { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public TransactionTypeResponseModel TransactionType { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public ClassRetailEnum? Class { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public CategoryRetailEnum? Category { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DirectionRetailEnum? Direction { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DirectionClassRetailEnum? DirectionClass { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime TransactionDate { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime SettlementDate { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public ReferenceResponseModel Reference { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public TransactionAmountsResponseModel Amounts { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public FeeDataResponseModel FeeData { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public ResponseResponseModel Response { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public TxnStatusRetailEnum? TxnStatus { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Description { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public MerchantDataResponseModel MerchantData { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public TransactionAgreementResponseModel Agreement { get; set; }

	}
}
