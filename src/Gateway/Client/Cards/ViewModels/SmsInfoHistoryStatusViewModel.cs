using System;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Enums.Cards;

namespace Vostok.Hotline.Gateway.Client.Cards.ViewModels
{
	public class SmsInfoHistoryStatusViewModel : ResponseBaseModel
	{
		/// <summary>
		/// Поточный статус
		/// </summary>
		public SmsInfoHistoryStatusEnum CurrentStatus { get; set; }
		/// <summary>
		/// Дата последнего изменения статуса
		/// </summary>
		public DateTime DateChangeStatus { get; set; }
		/// <summary>
		/// Комментарий кем изменен статус
		/// </summary>
		public string Comment { get; set; }
	}
}
