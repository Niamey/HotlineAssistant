namespace Vostok.Hotline.Api.Retail.Responses.Classifiers
{
	public class EntityResponseModel
	{
		/// <summary>
		/// Идентификатор объекта в системе SOLAR
		/// </summary>
		public long Id { get; set; }
		/// <summary>
		/// Информация о типе объекта
		/// </summary>
		public EntityTypeResponseModel EntityType { get; set; }
	}
}
