using Vostok.HotLineAssistant.Common.Requests.Base;

namespace Vostok.HotLineAssistant.Common.Requests.Specific
{
	public class ByCardNumberRequest : BaseRequest
	{
		public string CardNumber { get; set; }
	}
}