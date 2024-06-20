using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Vostok.Hotline.Core.Common.ActionFilters;

namespace Vostok.Hotline.Core.Common.Security.Attributes
{
	public class HotlineAuthorizationFilter : CoreActionFilterAttribute, IAuthorizationFilter
	{
		public HotlineAuthorizationFilter(IServiceProvider serviceProvider)
			: base(serviceProvider)
		{
		}

		public void OnAuthorization(AuthorizationFilterContext context)
		{
			if (context.ActionDescriptor is Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)
			{
				var allowAnonymousAttribute = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).MethodInfo.CustomAttributes.Any(x => x.AttributeType == typeof(AllowAnonymousAttribute));
				if (!allowAnonymousAttribute)
				{
					var user = context.HttpContext.User;
					if (!user.Identity.IsAuthenticated)
					{
						logger.Value.LogWarning($"{context.ActionDescriptor.DisplayName} {nameof(OnAuthorization)}. Request is UnauthorizedResult.", new { context.HttpContext.Request.Path });

						context.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
						return;
					}
				}
			}
		}
	}
}