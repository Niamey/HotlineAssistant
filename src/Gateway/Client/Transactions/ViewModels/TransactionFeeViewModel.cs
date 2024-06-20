namespace Vostok.Hotline.Gateway.Client.Transactions.ViewModels
{
	public class TransactionFeeViewModel
	{
		public long FeeId { get; set; }
		public long? TxnId { get; set; }
		public decimal? FeeAmount { get; set; }
		public string FeeCurrency { get; set; }
		public decimal? BillingAmount { get; set; }
		public string BillingCurrency { get; set; }

		public string FeeTypeName { get; set; }
	}
}
