using System;
using System.Collections.Generic;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Enums.Agreements;

namespace Vostok.Hotline.Gateway.Client.Agreements.ViewModels
{
	public class AgreementHistoryStatusViewModel : ResponseBaseModel
	{
		public AgreementStatusEnum CurrentStatus { get; set; }
		/// <summary>
		/// Дата, с которой действует значение (включительно)
		/// </summary>
		public DateTime? DateChangeStatus { get; set; }
		/// <summary>
		/// Статус карты
		/// </summary>
		public List<AgreementHistoryStatusAuditViewModel> Histories { get; set; }
	}
}
