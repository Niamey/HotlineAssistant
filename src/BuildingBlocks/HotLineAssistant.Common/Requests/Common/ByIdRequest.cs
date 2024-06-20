using Vostok.HotLineAssistant.Common.Requests.Base;

namespace Vostok.HotLineAssistant.Common.Requests.Common
{
	public class ByIdRequest : BaseRequest
	{
		public int Id { get; set; }
	}

	public class ByIdRequest<T> : BaseRequest
	{
		public T Id { get; set; }
	}
}