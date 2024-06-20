using System;
using Vostok.Hotline.Api.Retail.Responses.Cards.Enums;

namespace Vostok.Hotline.Api.Retail.Responses.Cards
{
	internal class CardHistoryStatusResponseModel
	{
		/// <summary>
		/// Статус карты
		/// </summary>
		public CardStatusRetailEnum Status { get; set; }
		/// <summary>
		/// Комментарий о причинах установки
		/// </summary>
		public string Comment { get; set; }
		/// <summary>
		/// Дата, с которой действует значение (включительно)
		/// </summary>
		public DateTime ValidFrom { get; set; }
		public CardHistoryStatusAuditResponseModel AuditHistory { get; set; }
	}
}
