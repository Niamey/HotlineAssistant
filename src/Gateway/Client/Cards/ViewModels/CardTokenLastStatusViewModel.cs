using System;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Enums.Cards;
using Vostok.Hotline.Core.Common.Enums.MDES;

namespace Vostok.Hotline.Gateway.Client.Cards.ViewModels
{
	public class CardTokenLastStatusViewModel: ResponseBaseModel
	{
		public string CommentText { get; set; }
		public InitiatorMdesEnum Initiator { get; set; }
		public ReasonTypeMdesEnum ReasonCode { get; set; }
		public StatusCodeMdesEnum StatusCode { get; set; }
		public DateTime StatusDateTime { get; set; }
		public string StatusDescription { get; set; }
	}
}
