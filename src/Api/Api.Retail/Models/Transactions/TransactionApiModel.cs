using System;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Enums.Transactions;

namespace Vostok.Hotline.Api.Retail.Models.Transactions
{
	public class TransactionApiModel : ResponseBaseModel { 
		/// <summary>
		/// 
		/// </summary>
		public long TxnId { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public EntryTypeEnum EntryType { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public TransactionTypeApiModel TransactionType { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public ClassEnum? Class { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public CategoryEnum? Category { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DirectionEnum? Direction { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DirectionClassEnum? DirectionClass { get; set; }
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
		public ReferenceApiModel Reference { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public TransactionAmountsApiModel Amounts { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public FeeDataApiModel FeeData { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public ResponseApiModel Response { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public StmtStatusEnum? TxnStatus { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Description { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public MerchantDataApiModel MerchantData { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public TransactionAgreementApiModel Agreement { get; set; }
	}
}
