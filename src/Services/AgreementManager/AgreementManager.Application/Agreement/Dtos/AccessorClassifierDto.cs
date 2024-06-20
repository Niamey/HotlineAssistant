using Vostok.HotLineAssistant.AgreementManager.Application.Agreement.Enums;

namespace Vostok.HotLineAssistant.AgreementManager.Application.Agreement.Dtos
{
	public class AccessorClassifierDto : ClassifierDto
	{
		public AccessorClassifiers Type { get; set; }
		public string TypeComment { get; set; }
	}
}