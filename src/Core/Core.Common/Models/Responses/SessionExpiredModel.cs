using Microsoft.AspNetCore.Mvc.Filters;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Exeptions;

namespace Vostok.Hotline.Core.Common.Models.Responses
{
	public class SessionExpiredModel : ResponseFailedBaseModel
	{
		public string Message { get; }

		public SessionExpiredModel(SessionExpiredRequestException exception, ExceptionContext context)
			: base(context)
		{
			Message = exception.Message;
			StatusCode = exception.StatusCode;
		}
	}
}
