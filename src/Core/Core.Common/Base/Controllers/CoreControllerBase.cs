using System;
using Microsoft.AspNetCore.Mvc;
using Vostok.Hotline.Core.Common.Base.Abstractions;

namespace Vostok.Hotline.Core.Common.Base.Controllers
{
	public abstract class CoreControllerBase : ControllerBase
	{
		//protected readonly Lazy<ILoggingService> logger;
		protected readonly Lazy<ISessionContext> sessionContext;

		protected CoreControllerBase()
		{
			//logger = new Lazy<ILoggingService>(() =>
			//{
			//	var ctrlType = this.GetType();
			//	var loggerType = typeof(ILoggingService<>).MakeGenericType(ctrlType);

			//	return this.HttpContext.RequestServices.GetService(loggerType) as ILoggingService;
			//});

			sessionContext = new Lazy<ISessionContext>(() => this.HttpContext.RequestServices.GetService(typeof(ISessionContext)) as ISessionContext);
		}
	}
}
