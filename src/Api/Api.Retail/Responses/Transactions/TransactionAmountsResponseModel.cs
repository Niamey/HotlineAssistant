namespace Vostok.Hotline.Api.Retail.Responses.Transactions
{
	public class TransactionAmountsResponseModel
	{
		/// <summary>
		/// 
		/// </summary>
		public MoneyResponseModel TransactionAmount { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public MoneyResponseModel ReceiverBillingAmount { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public MoneyResponseModel TotalReceiverBillingAmount { get; set; }
	}
}
