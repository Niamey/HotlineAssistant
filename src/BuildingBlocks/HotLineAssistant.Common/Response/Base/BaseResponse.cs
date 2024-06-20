using System.Collections.Generic;

namespace Vostok.HotLineAssistant.Common.Response.Base
{
	public abstract class BaseResponse
	{
		protected BaseResponse()
		{
			Errors = new List<string>();
		}
		public bool Success { get; set; } = true;
		public List<string> Errors { get; set; }
	}
}