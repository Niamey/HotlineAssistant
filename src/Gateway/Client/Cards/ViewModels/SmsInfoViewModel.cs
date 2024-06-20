using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Enums.Cards;

namespace Vostok.Hotline.Gateway.Client.Cards.ViewModels
{
	public class SmsInfoViewModel : ResponseBaseModel
	{
		public SmsInfoStatusEnum Status { get; set; }
		public string Phone { get; set; }
		public string Tariff { get; set; }
		public bool IsFinancePhone { get; set; }
	}
}
