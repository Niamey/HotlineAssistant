using System;
using Vostok.Hotline.Core.Common.Enums.Cards;

namespace Vostok.Hotline.Gateway.Client.Cards.ViewModels
{
	public class CardHistoryStatusAuditViewModel
	{
		public CardStatusEnum Status { get; set; }
		public string UserLogin { get; set; }
		public string SystemName { get; set; }
		public string Comment { get; set; }
		public DateTime ModifyDate { get; set; }
		public bool? IsEmployee { get; set; }
		public bool IsFutureStatus => ModifyDate > DateTime.Now;
	}
}
