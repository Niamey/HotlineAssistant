using Vostok.Hotline.Core.Common.Enums.Cards;

namespace Vostok.Hotline.Api.Retail.Models.Cards
{
	public class SmsInfoApiModel
	{
		public SmsInfoStatusEnum Status { get; set; }
		public string Phone { get; set; }
		public string Tariff { get; set; }
		public bool IsFinancePhone { get; set; }
	}
}
