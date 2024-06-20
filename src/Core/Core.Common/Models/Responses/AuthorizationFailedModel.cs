using Microsoft.AspNetCore.Mvc.Filters;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Exeptions;

namespace Vostok.Hotline.Core.Common.Models.Responses
{
	public class AuthorizationFailedModel : ResponseFailedBaseModel
	{
		public string Message { get; }

		public AuthorizationFailedModel(AuthorizationRequestException exception, ExceptionContext context)
			: base(context)
		{
			Message = exception.Message;
			StatusCode = exception.StatusCode;
		}
	}
}
