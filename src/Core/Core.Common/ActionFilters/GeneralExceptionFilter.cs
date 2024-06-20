using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Vostok.Hotline.Core.Common.Base.Abstractions;
using Vostok.Hotline.Core.Common.Exeptions;
using Vostok.Hotline.Core.Common.Loggers.Abstractions;
using Vostok.Hotline.Core.Common.Models.Responses;

namespace Vostok.Hotline.Core.Common.ActionFilters
{
	public class GeneralExceptionFilter : IExceptionFilter
	{
		protected readonly Lazy<ILoggingService> logger;
		protected readonly Lazy<ISessionContext> sessionContext;

		public GeneralExceptionFilter(IServiceProvider serviceProvider)
		{
			logger = new Lazy<ILoggingService>(() =>
			{
				var ctrlType = this.GetType();
				var loggerType = typeof(ILoggingService<>).MakeGenericType(ctrlType);

				return serviceProvider.GetService(loggerType) as ILoggingService;
			});

			sessionContext = new Lazy<ISessionContext>(() => serviceProvider.GetService(typeof(ISessionContext)) as ISessionContext);
		}

		public void OnException(ExceptionContext context)
		{
			var result = GetObjectResult(context);

			context.HttpContext.Response.StatusCode = result.StatusCode.Value;
			context.Result = result;

			context.ExceptionHandled = true;
		}

		private ObjectResult GetObjectResult(ExceptionContext context)
		{
			ObjectResult result;
			switch (context.Exception)
			{
				case AuthorizationRequestException authorizationFailed:
					result = new ResponseFailedObjectResult(new AuthorizationFailedModel(authorizationFailed, context), sessionContext.Value.SessionId);
					break;

				case SessionExpiredRequestException sessionExpiredFailed:
					result = new ResponseFailedObjectResult(new SessionExpiredModel(sessionExpiredFailed, context), sessionContext.Value.SessionId);
					break;

				case NotFoundRequestException notFound:
					result = new ResponseFailedObjectResult(new ResponseNotFoundModel(notFound, context), sessionContext.Value.SessionId);
					break;

				case Exception exception:
					result = new ResponseFailedObjectResult(new ResponseErrorModel(exception, context), sessionContext.Value.SessionId);
					break;

				default:
					result = new ResponseFailedObjectResult(new ResponseErrorModel(context.Exception, context), sessionContext.Value.SessionId);
					break;
			}

			logger.Value.LogError($"{context.ActionDescriptor.DisplayName} {nameof(GetObjectResult)}. Request is failed.", context.Exception);

			return result;
		}
	}
}
