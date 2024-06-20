using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Vostok.Hotline.Core.Common.Models.Validations
{
	public class ValidationFailedObjectResult : ObjectResult
	{
		public ValidationFailedObjectResult(ModelStateDictionary modelState, Guid sessionId)
			: base(new ValidationResultModel(modelState, sessionId))
		{
			StatusCode = StatusCodes.Status400BadRequest;
		}

		public ValidationFailedObjectResult(ValidationError[] errors, int statusCode, Guid sessionId)
			: base(new ValidationResultModel(errors, sessionId))
		{
			StatusCode = statusCode;
		}
	}
}