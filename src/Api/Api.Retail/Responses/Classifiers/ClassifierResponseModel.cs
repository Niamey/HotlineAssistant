using System;
using System.Collections.Generic;
using System.Text;

namespace Vostok.Hotline.Api.Retail.Responses.Classifiers
{
	/// <summary>
	/// классификатор
	/// </summary>
	public class ClassifierResponseModel
	{
		/// <summary>
		/// Идентификатор классификатора в системе SOLAR
		/// </summary>
		public long Id { get; set; }
		/// <summary>
		/// Информация об объекте, для которого установлен классификатор
		/// </summary>
		public EntityResponseModel Entity { get; set; }
		/// <summary>
		/// Идентификационная информация типа классификатора
		/// </summary>
		public ClassifierTypeResponseModel Type { get; set; }
		/// <summary>
		/// Идентификационная информация значения классификатора
		/// </summary>
		public ClassifierValueResponseModel Value { get; set; }
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
