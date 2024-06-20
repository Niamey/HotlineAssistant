using Vostok.HotLineAssistant.CardManager.Domain.Models.Cards;

namespace Vostok.HotLineAssistant.CardManager.Application.Cards.Dtos
{
	public class CardLimitDto
	{
		public CardLimitType Type { get; set; }
		public decimal Amount { get; set; }
	}
}