using Vostok.Hotline.Core.Common.Enums.Cards;

namespace Vostok.Hotline.Api.Retail.Models.Cards
{
	public class SmsInfoChangeStatusApiModel
	{
		public SmsInfoChangeStatusEnum Status { get; set; }
		public string Message { get; set; }
	}
}
