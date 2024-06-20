using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;

namespace Vostok.Hotline.Core.Common.Models.Responses
{
	public class ResponseFailedObjectResult : ObjectResult
	{
		public ResponseFailedObjectResult(Exception exception, Guid sessionId, ExceptionContext context)
			: base(new ResponseFailedResultModel<ResponseErrorModel>(new ResponseErrorModel(exception, context), sessionId))
		{
			StatusCode = StatusCodes.Status500InternalServerError;
		}

		public ResponseFailedObjectResult(ResponseErrorModel errorModel, Guid sessionId)
			: base(new ResponseFailedResultModel<ResponseErrorModel>(errorModel, sessionId))
		{
			StatusCode = errorModel.StatusCode;
		}

		public ResponseFailedObjectResult(IResponseFailed errorModel, Guid sessionId)
			: base(new ResponseFailedResultModel<IResponseFailed>(errorModel, sessionId))
		{
			StatusCode = errorModel.StatusCode;
		}
	}
}
