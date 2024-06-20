using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Vostok.Hotline.Core.Common.Base.Abstractions;
using Vostok.Hotline.Core.Common.Loggers.Abstractions;

namespace Vostok.Hotline.Core.Common.ActionFilters
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public abstract class CoreActionFilterAttribute : ActionFilterAttribute
	{
		protected readonly Lazy<ILoggingService> logger;
		protected readonly Lazy<ISessionContext> sessionContext;

		public CoreActionFilterAttribute(IServiceProvider serviceProvider)
		{
			logger = new Lazy<ILoggingService>(() =>
			{
				var ctrlType = this.GetType();
				var loggerType = typeof(ILoggingService<>).MakeGenericType(ctrlType);

				return serviceProvider.GetService(loggerType) as ILoggingService;
			});
			sessionContext = new Lazy<ISessionContext>(() => serviceProvider.GetService(typeof(ISessionContext)) as ISessionContext);
		}

		public override void OnActionExecuting(ActionExecutingContext context)
		{
			if (!context.ModelState.IsValid)
			{
				logger.Value.LogWarning($"Error Validate", new
				{
					ValidateResult = context.Result,
					ControllerParams = context.ActionArguments
				});
			}
		}
	}
}
