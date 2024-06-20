using System;
using System.Collections.Generic;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Enums.Cards;

namespace Vostok.Hotline.Gateway.Client.Cards.ViewModels
{
	public class CardHistoryStatusViewModel : ResponseBaseModel
	{
		public CardStatusEnum CurrentStatus { get; set; }
		public DateTime? DateChangeStatus { get; set; }
		public List<CardHistoryStatusAuditViewModel> Histories { get; set; }
	}
}
