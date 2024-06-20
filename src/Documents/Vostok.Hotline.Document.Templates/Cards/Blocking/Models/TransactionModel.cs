using System;
using Vostok.Hotline.Core.Common.Enums.Transactions;

namespace Vostok.Hotline.Document.Templates.Cards.Blocking.Models
{
	public class TransactionModel
	{
		public long? OperationId { get; set; }
		public DateTime? OperationDate { get; set; }
		public string MerchantName { get; set; }
		public string OperationDetails { get; set; }
		public decimal? Amount { get; set; }
		public string Currency { get; set; }
		public TxnStatusEnum? Status { get; set; }
	}
}
