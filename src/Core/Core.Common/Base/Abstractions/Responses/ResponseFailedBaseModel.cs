using Microsoft.AspNetCore.Mvc.Filters;

namespace Vostok.Hotline.Core.Common.Base.Abstractions.Responses
{
	public abstract class ResponseFailedBaseModel : IResponseFailed
	{
		public ResponseFailedBaseModel(ExceptionContext context)
		{
			Request = new ResponseFailedRequestModel(context);
		}

		public int StatusCode { get; set; }
		public ResponseFailedRequestModel Request { get; set; }


	}
}
