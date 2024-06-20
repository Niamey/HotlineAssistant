using System;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Enums.Cards;

namespace Vostok.Hotline.Api.Retail.Models.Cards
{
	public class CardHistoryStatusApiModel : ResponseBaseModel
	{
		public CardStatusEnum Status { get; set; }
		public DateTime ModifyDate { get; set; }
		public string Comment { get; set; }
		public string SystemName { get; set; }
		public string UserLogin { get; set; }
		public bool? IsEmployee { get; set; }
	}
}
