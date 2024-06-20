using Vostok.HotLineAssistant.Common.Requests.Base;

namespace Vostok.HotLineAssistant.Common.Requests.Specific
{
	public class ByNumberRequest : BaseRequest
	{
		public string Number { get; set; }
	}
}