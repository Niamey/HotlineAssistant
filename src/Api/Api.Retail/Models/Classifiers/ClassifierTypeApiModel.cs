namespace Vostok.Hotline.Api.Retail.Models.Classifiers
{
	public class ClassifierTypeApiModel
	{
		/// <summary>
		/// Идентификатор типа классификатора
		/// </summary>
		public long Id { get; set; }
		/// <summary>
		/// Код типа классификатора
		/// </summary>
		public string Code { get; set; }
		/// <summary>
		/// Наименование типа классификатора
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// Описание типа классификатора
		/// </summary>
		public string Description { get; set; }
	}
}
