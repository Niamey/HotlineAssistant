using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;

namespace Vostok.Hotline.Gateway.Client.Cards.ViewModels
{
	public class CardTariffViewModel : ResponseBaseModel
	{
		/// <summary>
		/// ИД тарифа
		/// </summary>
		public long TariffId { get; set; }
		/// <summary>
		/// Наименование
		/// </summary>
		public string TariffName { get; set; }

		/// <summary>
		/// Дата начала действия тарифа
		/// </summary>
		public string TariffStart { get; set; }

		/// <summary>
		/// Дата окончания действия тарифа
		/// </summary>
		public string TariffEnd { get; set; }

		/// <summary>
		/// Ссылка на PDF
		/// </summary>
		public string TariffUrl { get; set; }
	}
}
