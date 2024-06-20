using System;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Enums.Cards;

namespace Vostok.Hotline.Gateway.Client.Cards.ViewModels
{
	public class CardCurrentStatusViewModel: ResponseBaseModel
	{
		public CardStatusEnum Status { get; set; }
		public DateTime DateChangeStatus { get; set; }
		public string Reason { get; set; }
		public string SystemName { get; set; }
		public string UserLogin { get; set; }
		public bool? IsEmployee { get; set; }
	}
}
