using System;

namespace Vostok.Hotline.Api.Retail.Models.Classifiers
{
	/// <summary>
	/// классификатор
	/// </summary>
	public class ClassifierApiModel
	{
		/// <summary>
		/// Идентификатор классификатора в системе SOLAR
		/// </summary>
		public long Id { get; set; }
		/// <summary>
		/// Информация об объекте, для которого установлен классификатор
		/// </summary>
		public EntityApiModel Entity { get; set; }
		/// <summary>
		/// Идентификационная информация типа классификатора
		/// </summary>
		public ClassifierTypeApiModel Type { get; set; }
		/// <summary>
		/// Идентификационная информация значения классификатора
		/// </summary>
		public ClassifierValueApiModel Value { get; set; }
		/// <summary>
		/// Комментарий, который был введен оператором при назначении классификатора, если такая операция проводилась вручную
		/// </summary>
		public string Comment { get; set; }
		/// <summary>
		/// Дата начала действия классификатора
		/// </summary>
		public DateTime ValidFrom { get; set; }
		/// <summary>
		/// Дата окончания действия классификатора
		/// </summary>
		public DateTime? ValidTo { get; set; }
	}
}
