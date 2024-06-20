using System;

namespace Vostok.HotLineAssistant.AgreementManager.Application.Agreement.Dtos
{
	public class CardDataDto
	{
		public int? SequenceNumber { get; set; }
		public string ExpiryDate { get; set; }
		public string ActivationStatus { get; set; }
	}
}