using Vostok.HotLineAssistant.Common.Response.Base;

namespace Vostok.HotLineAssistant.Common.Response.Common
{
	public class ModelResponse<T> : BaseResponse
	{
		public T Item { get; set; }
	}
}