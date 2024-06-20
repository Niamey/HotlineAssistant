using System.Collections.Generic;
using Vostok.HotLineAssistant.Common.Requests.Base;

namespace Vostok.HotLineAssistant.Common.Requests.Common
{
	public class ByModelsRequest<T> : BaseRequest
	{
		public List<T> Models { get; set; }
	}
}