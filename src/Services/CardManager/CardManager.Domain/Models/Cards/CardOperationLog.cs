using System;

namespace Vostok.HotLineAssistant.CardManager.Domain.Models.Cards
{
	public class CardOperationLog
	{
		public int Id { get; set; }
		public DateTime OperationDate { get; set; }
		public string OperationMessage { get; set; }
		public int Status { get; set; }
	}
}