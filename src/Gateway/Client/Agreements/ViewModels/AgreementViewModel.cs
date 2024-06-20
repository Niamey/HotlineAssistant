using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Enums.Agreements;

namespace Vostok.Hotline.Gateway.Client.Agreements.ViewModels
{
	public class AgreementViewModel : ResponseBaseModel
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
		public string AgreementNumber { get; set; }
		/// <summary>
		/// Клиент
		/// </summary>
		public string ClientName { get; set; }
		/// <summary>
		/// Проект
		/// </summary>
		public string ProductName { get; set; }
		/// <summary>
		/// Валюта
		/// </summary>
		public string Currency { get; set; }
		/// <summary>
		/// Бонусная программа
		/// </summary>
		public string Bonus { get; set; }
		/// <summary>ещ
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
