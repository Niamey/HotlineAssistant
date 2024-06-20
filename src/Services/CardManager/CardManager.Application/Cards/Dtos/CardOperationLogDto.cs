using System;

namespace Vostok.HotLineAssistant.CardManager.Application.Cards.Dtos
{
	public class CardOperationLogDto
	{
		public int Id { get; set; }
		public DateTime OperationDate { get; set; }
		public string OperationMessage { get; set; }
		public int Status { get; set; }
	}
}