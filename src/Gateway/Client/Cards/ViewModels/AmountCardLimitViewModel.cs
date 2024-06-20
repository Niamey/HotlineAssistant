using Vostok.Hotline.Core.Common.Enums.Cards;

namespace Vostok.Hotline.Gateway.Client.Cards.ViewModels
{
	public class AmountCardLimitViewModel
	{
		public CardTypeLimitEnum? TypeLimit { get; set; }
		public CardPeriodTypeLimitEnum? PeriodTypeLimit { get; set; }
		public decimal? Limit { get; set; }
		public decimal? Used { get; set; }
		public string CurrencyCode { get; set; }
		public string CurrencyName { get; set; }
	}
}
