using Vostok.HotLineAssistant.Common.Requests.Base;

namespace Vostok.HotLineAssistant.Common.Requests.Common
{
	public class ByModelRequest<T> : BaseRequest
	{
		public T Model { get; set; }
	}
}