namespace Vostok.Hotline.Api.Retail.Responses.Transactions
{
	public class MoneyResponseModel
	{
		/// <summary>
		/// Сумма
		/// </summary>
		/// <example></example>
		public decimal Amount { get; set; }
		/// <summary>
		/// Числовой код валюты в соответствии со стандартом ISO 4217
		/// </summary>
		/// <example></example>
		public string Currency { get; set; }
	}
}
