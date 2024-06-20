namespace Vostok.Hotline.Api.Retail.Models.Transactions
{
	public class TransactionAmountsApiModel
	{
		/// <summary>
		/// 
		/// </summary>
		public MoneyApiModel TransactionAmount { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public MoneyApiModel ReceiverBillingAmount { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public MoneyApiModel TotalReceiverBillingAmount { get; set; }
	}
}
