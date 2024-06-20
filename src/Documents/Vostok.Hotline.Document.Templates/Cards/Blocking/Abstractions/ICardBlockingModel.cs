using System;
using System.Collections.Generic;
using Vostok.Hotline.Core.Common.Enums.Cards;

namespace Vostok.Hotline.Document.Templates.Cards.Blocking.Abstractions
{
	public interface ICardBlockingModel
	{
		long? SolarId { get; set; }
		long? CardId { get; set; }
		CardBlockingReasonTypeEnum? ReasonType { get; set; } 
		ICardBlockingReason Reason { get; }
		string ReasonComment { get; set; }
		public Dictionary<string, object> ViewData { get; set; }
		string UserLogin { get; set; }
		DateTime? ModifyDate { get; set; }
	}
}
