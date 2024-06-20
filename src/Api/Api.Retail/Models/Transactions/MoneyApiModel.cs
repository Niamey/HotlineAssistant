namespace Vostok.Hotline.Api.Retail.Models.Transactions
{
	public class MoneyApiModel
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
