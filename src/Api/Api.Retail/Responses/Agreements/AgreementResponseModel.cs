using Newtonsoft.Json;
using Vostok.Hotline.Api.Retail.Responses.Agreements.Enums;

namespace Vostok.Hotline.Api.Retail.Responses.Agreements
{
	/// <summary>
	/// Договор
	/// </summary>
	internal class AgreementResponseModel 
    {
		/// <summary>
		/// ИД договора
		/// </summary>
		public long Id { get; set; }
		/// <summary>
		/// ИД клиента в Солар
		/// </summary>
		public long ClientId { get; set; }
		/// <summary>
		/// Клиент
		/// </summary>
		public string ClientName { get; set; }
		/// <summary>
		/// Номер договора
		/// </summary>
		public string Number { get; set; }
		/// <summary>
		/// Бонусная программа
		/// </summary>
		public string Bonus { get; set; }
		/// <summary>
		/// Проект
		/// </summary>
		public ProductResponseModel Product { get; set; }
		/// <summary>
		/// Статус догвора
		/// </summary>
		public AgreementStatusRetailEnum Status { get; set; }
		/// <summary>
		/// Валюта
		/// </summary>
		[JsonProperty("Currency")]
		public string CurrencyCode { get; set; }
		/// <summary>
		/// Вид договора
		/// </summary>
		public AgreementTypeRetailEnum Type { get; set; }
		/// <summary>
		/// Вид продукта
		/// </summary>
		public AgreementViewTypeRetailEnum ViewType { get; set; }
	}
}
