using Vostok.HotLineAssistant.AgreementManager.Application.Agreement.Enums;

namespace Vostok.HotLineAssistant.AgreementManager.Application.Agreement.Dtos
{
	public class AgreementClassifierDto : ClassifierDto
	{
		public AgreementClassifiers Type { get; set; }
	}
}