using System;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Enums.Cards;

namespace Vostok.Hotline.Gateway.Client.Cards.ViewModels
{
	public class Secure3DStatusHistoryViewModel : ResponseBaseModel
	{
		public Secure3DStatusHistoryEnum CurrentStatus { get; set; }
		public DateTime DateChangeStatus { get; set; }
		public string Comment { get; set; }
	}
}
