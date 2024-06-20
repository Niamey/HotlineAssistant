﻿namespace Vostok.Hotline.Api.Retail.Responses.Classifiers
{
	public class ClassifierValueResponseModel
	{
		/// <summary>
		/// Идентификатор значения классификатора
		/// </summary>
		public long Id { get; set; }
		/// <summary>
		/// Код значения классификатора
		/// </summary>
		public string Code { get; set; }
		/// <summary>
		/// Наименование значения классификатора
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// Значение является значением по умолчанию для классификатора данного типа
		/// </summary>
		public bool DefaultValue { get; set; }
	}
}
