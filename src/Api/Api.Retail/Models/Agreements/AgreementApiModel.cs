using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Enums.Agreements;

namespace Vostok.Hotline.Api.Retail.Models.Agreements
{
	public class AgreementApiModel : ResponseBaseModel
	{
		/// <summary>
		/// ИД карты
		/// </summary>
		public long AgreementId { get; set; }
		/// <summary>
		/// ИД клиента в Солар
		/// </summary>
		public long SolarId { get; set; }

		/// <summary>
		/// Номер договора
		/// </summary>
		public string Number { get; set; }
		/// <summary>
		/// Клиент
		/// </summary>
		public string ClientName { get; set; }
		/// <summary>
		/// Проект
		/// </summary>
		public string ProductName { get; set; }
		/// <summary>
		/// Код валюты
		/// </summary>
		public string CurrencyCode { get; set; }		
		/// <summary>
		/// Бонусная программа
		/// </summary>
		public string Bonus { get; set; }
		/// <summary>
		/// Статус догвора
		/// </summary>
		public AgreementStatusEnum Status { get; set; }
		/// <summary>
		/// Вид договора
		/// </summary>
		public AgreementTypeEnum Type { get; set; }
		/// <summary>
		/// Вид продукта
		/// </summary>
		public AgreementViewTypeEnum ViewType { get; set; }
	}
}
