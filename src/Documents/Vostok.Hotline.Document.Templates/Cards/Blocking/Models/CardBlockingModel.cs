using System;
using System.Collections.Generic;
using Vostok.Hotline.Core.Common.Enums.Cards;
using Vostok.Hotline.Core.Documents.Abstractions;
using Vostok.Hotline.Document.Templates.Cards.Blocking.Abstractions;
using Vostok.Hotline.Document.Templates.Cards.Blocking.Mappers;

namespace Vostok.Hotline.Document.Templates.Cards.Blocking.Models
{
	public class CardBlockingModel<T> : ICardBlockingModel, ITemplateViewModel
		where T : ICardBlockingReason
	{
		public T Reason { get; set; }
		ICardBlockingReason ICardBlockingModel.Reason => Reason;

		public long? SolarId { get; set; }
		public long? CardId { get; set; }
		public CardBlockingReasonTypeEnum? ReasonType { get; set; }
		public string ReasonComment { get; set; }
		public Dictionary<string, object> ViewData { get; set; }
		public string UserLogin { get; set; }
		public DateTime? ModifyDate { get; set; }

		public string GetCardBlockingReasonType() {
			return ReasonType.GetText();
		}
	}
}
