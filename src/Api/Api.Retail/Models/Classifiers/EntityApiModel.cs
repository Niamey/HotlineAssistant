namespace Vostok.Hotline.Api.Retail.Models.Classifiers
{
	public class EntityApiModel
	{
		/// <summary>
		/// Идентификатор объекта в системе SOLAR
		/// </summary>
		public long Id { get; set; }
		/// <summary>
		/// Информация о типе объекта
		/// </summary>
		public EntityTypeApiModel EntityType { get; set; }
	}
}
