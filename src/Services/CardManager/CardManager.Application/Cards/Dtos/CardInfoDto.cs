using System;

namespace Vostok.HotLineAssistant.CardManager.Application.Cards.Dtos
{
	public class CardInfoDto
	{
		public int Id { get; set; }
		public string Number { get; set; }
		public DateTime ExpireDate { get; set; }
	}
}
