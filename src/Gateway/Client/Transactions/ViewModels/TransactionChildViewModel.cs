using System;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Enums.Transactions;

namespace Vostok.Hotline.Gateway.Client.Transactions.ViewModels
{
	public class TransactionChildViewModel : ResponseBaseModel
	{
		public long RowId { get; set; }
		public long TxnId { get; set; }
		public TxnStatusEnum? TxnStatus { get; set; }
		public ClassEnum? Class { get; set; }
		public CategoryEnum? Category { get; set; }
		public DirectionEnum? Direction { get; set; }
		public DirectionClassEnum? DirectionClass { get; set; }
		public string TransactionTypeName { get; set; }
		public DateTime TransactionDate { get; set; }
		public decimal TransactionAmount { get; set; }
		public string TransactionCurrency { get; set; }
		public string ResponseCode { get; set; }
		public string TransactionDetails { get; set; }

		public decimal FeeAmount { get; set; }
		public string FeeCurrency { get; set; }
		public TransactionFeeCollectionViewModel Fees { get; set; }
	}
}