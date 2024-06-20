using System;

namespace Vostok.HotLineAssistant.CardManager.Domain.Models.Cards
{
	public class CardInfo
	{
		public int Id { get; set; }
		public string Number { get; set; }
		public DateTime ExpireDate { get; set; }
	}
}
