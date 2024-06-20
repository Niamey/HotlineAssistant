using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Vostok.Hotline.Core.Common.Models.Validations;

namespace Vostok.Hotline.Core.Common.ActionFilters
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public class GeneralValidateActionFilter : CoreActionFilterAttribute
	{
		public GeneralValidateActionFilter(IServiceProvider serviceProvider)
			: base(serviceProvider)
		{
		}

		public override void OnActionExecuting(ActionExecutingContext context)
		{
			if (!context.ModelState.IsValid)
			{
				context.Result = new ValidationFailedObjectResult(context.ModelState, sessionContext.Value.SessionId);
			}

			base.OnActionExecuting(context);
		}
	}
}