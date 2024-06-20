using System;

namespace Vostok.HotLineAssistant.AgreementManager.Application.Agreement.Dtos
{
	public abstract class ClassifierDto
	{
		public long Id { get; set; }
		public string Value { get; set; }
		public string Comment { get; set; }
		public string ValidFrom { get; set; }
		public string ValidTo { get; set; }
	}
}