using Vostok.Hotline.Api.Retail.Responses.Classifiers.Enums;

namespace Vostok.Hotline.Api.Retail.Responses.Classifiers
{
	public class EntityTypeResponseModel
	{
		/// <summary>
		/// Идентификатор типа объекта в системе SOLAR
		/// </summary>
		public long Id { get; set; }
		/// <summary>
		/// Код типа объекта.
		/// </summary>
		/// <remarks>
		/// Зависит от набора модулей системы SOLAR.
		/// Возможные значения:
		/// CLIENT — клиент,
		/// AGREEMENT — договор,
		/// ACCESSOR — способ доступа
		/// </remarks>
		public ClassifierRetailTypeEnum Code { get; set; }
		/// <summary>
		/// Наименование типа объекта
		/// </summary>
		public string Name { get; set; }
	}
}