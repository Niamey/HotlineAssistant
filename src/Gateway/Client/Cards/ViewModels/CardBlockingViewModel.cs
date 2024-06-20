using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;

namespace Vostok.Hotline.Gateway.Client.Cards.ViewModels
{
	public class CardBlockingViewModel : ResponseBaseModel
	{
		public string Message { get; set; }
		public bool? Success { get; set; }
	}
}
