using System;
using Vostok.Hotline.Api.Retail.Responses.Agreements.Enums;

namespace Vostok.Hotline.Api.Retail.Responses.Agreements
{
	internal class AgreementHistoryStatusResponseModel
	{

		/// <summary>
		/// Комментарий о причинах установки
		/// </summary>
		public string Comment { get; set; }
		/// <summary>
		/// Дата, с которой действует значение (включительно)
		/// </summary>
		public DateTime ValidFrom { get; set; }
		/// <summary>
		/// Статус карты
		/// </summary>
		public AgreementStatusRetailEnum Status { get; set; }
		public AgreementHistoryStatusAuditResponseModel AuditHistory { get; set; }
	}
}
