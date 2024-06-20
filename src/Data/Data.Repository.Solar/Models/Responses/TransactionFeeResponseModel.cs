using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;

namespace Vostok.Hotline.Data.Repository.Solar.Models.Responses
{
	public class TransactionFeeResponseModel : ResponseBaseModel
	{
		public long FeeId { get; set; }
		public long? TxnId { get; set; }
		public long? AgreementId { get; set; }

		public decimal? FeeAmount { get; set; }
		public string FeeCurrency { get; set; }
		public decimal? BillingAmount { get; set; }
		public string BillingCurrency { get; set; }

		public string TxnTypeName { get; set; }
		public string TxnTypeDescription { get; set; }
	}
}