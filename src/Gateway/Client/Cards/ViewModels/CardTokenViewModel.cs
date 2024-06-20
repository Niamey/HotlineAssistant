using System;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Enums.Cards;
using Vostok.Hotline.Core.Common.Enums.MDES;

namespace Vostok.Hotline.Gateway.Client.Cards.ViewModels
{
	public class CardTokenViewModel: ResponseBaseModel
	{
		public string TokenId { get; set; } 
		public TokenStatusNameMdesEnum TokenStatus { get; set; }
		public DateTime TokenTime { get; set; }
		public string DeviceName { get; set; }
		public string Wallet { get; set; }
		public string RequestorName { get; set; }
	}
}
