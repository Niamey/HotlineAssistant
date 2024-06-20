using System;

namespace Vostok.Hotline.Api.Retail.Responses.Cards
{
	internal class TariffResponseModel
	{
		/// <summary>
		/// ИД тарифа
		/// </summary>
		public long Id { get; set; }
		/// <summary>
		/// Наименование
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Дата тарифа
		/// </summary>
		public DateTime? Date { get; set; }
	}
}