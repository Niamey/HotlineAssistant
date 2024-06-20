using Vostok.Hotline.Api.Retail.Responses.Cards.Enums;

namespace Vostok.Hotline.Api.Retail.Responses.Cards
{
	internal class CardLimitValuePeriodResponseModel
	{
		public int Value { get; set; }
		public CardPeriodTypeLimitRetailEnum PeriodType { get; set; }
	}
}
