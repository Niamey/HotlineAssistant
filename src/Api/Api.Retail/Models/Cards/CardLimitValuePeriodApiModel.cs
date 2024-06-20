using Vostok.Hotline.Core.Common.Enums.Cards;

namespace Vostok.Hotline.Api.Retail.Models.Cards
{
	public class CardLimitValuePeriodApiModel
	{
		public int Value { get; set; }
		public CardPeriodTypeLimitEnum PeriodType { get; set; }
	}
}
