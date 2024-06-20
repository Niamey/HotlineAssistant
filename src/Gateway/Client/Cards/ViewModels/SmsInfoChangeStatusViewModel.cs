using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Enums.Cards;

namespace Vostok.Hotline.Gateway.Client.Cards.ViewModels
{
	public class SmsInfoChangeStatusViewModel : ResponseBaseModel
	{
		public SmsInfoChangeStatusEnum Status { get; set; }
		public string Message { get; set; }
	}
}
