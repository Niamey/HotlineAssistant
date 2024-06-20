using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Vostok.HotLineAssistant.WebApi.ActionResults
{
	public class ExceptionObjectResult : ObjectResult
	{
		public ExceptionObjectResult(object value) : base(value)
		{
			StatusCode = StatusCodes.Status500InternalServerError;
		}
	}
}
