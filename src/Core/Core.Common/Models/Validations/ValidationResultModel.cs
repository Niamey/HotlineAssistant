using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Vostok.Hotline.Core.Common.Models.Validations
{
	public class ValidationResultModel
	{
		public Guid? SessionId { get; set; }
		public string Message { get; }
		public ValidationError[] Errors { get; }

		public ValidationResultModel(ModelStateDictionary modelState, Guid sessionId)
		{
			SessionId = sessionId;
			Message = "Validation Failed";
			Errors = modelState.Keys
					.SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
					.ToArray();
		}

		public ValidationResultModel(ValidationError[] errors, Guid sessionId)
		{
			SessionId = sessionId;
			Message = "Validation Failed";
			Errors = errors;
		}
	}
}
