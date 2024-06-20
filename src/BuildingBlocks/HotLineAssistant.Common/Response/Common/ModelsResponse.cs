using System.Collections.Generic;
using Vostok.HotLineAssistant.Common.Response.Base;

namespace Vostok.HotLineAssistant.Common.Response.Common
{
	public class ModelsResponse<T> : BaseResponse
	{
		public List<T> Items { get; set; }
	}
}