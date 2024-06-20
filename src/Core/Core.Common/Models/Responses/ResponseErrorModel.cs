using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;

namespace Vostok.Hotline.Core.Common.Models.Responses
{
	public class ResponseErrorModel : ResponseFailedBaseModel
	{
		
		public string Message { get; }

		public Exception Exception { get; }

		public ResponseErrorModel(Exception exception, ExceptionContext context)
			:base(context)
		{
			StatusCode = StatusCodes.Status500InternalServerError;
			Exception = exception;

			switch (Exception)
			{
				case OperationCanceledException ex:
					Message = "Request was canceled";
					break;

				default:
					Message = "Error";
					break;
			}
		}
	}
}
