using System.Collections.Generic;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Vostok.Hotline.Core.Common.Base.Abstractions.Responses
{
	public class ResponseFailedRequestModel
	{
		public ResponseFailedRequestModel(ExceptionContext context)
		{
			Url = context.HttpContext.Request.GetDisplayUrl();
			HttpMethod = context.HttpContext.Request.Method;

			RequestParams = new Dictionary<string, object>();
			foreach (var key in context.ModelState.Keys)
			{
				RequestParams.Add(key, context.ModelState[key].RawValue);
			}
		}
		public string Url { get; set; }
		public string HttpMethod { get; set; }
		public Dictionary<string, object> RequestParams { get; set; }
	}
}
