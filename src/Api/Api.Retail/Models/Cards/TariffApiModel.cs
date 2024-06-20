using System;

namespace Vostok.Hotline.Api.Retail.Models.Cards
{
	public class TariffApiModel
	{
		/// <summary>
		/// ИД тарифа
		/// </summary>
		public long TariffId { get; set; }
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
