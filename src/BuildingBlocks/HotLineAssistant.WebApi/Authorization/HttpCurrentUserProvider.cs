using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using Vostok.HotLineAssistant.Common.Helpers;

namespace Vostok.HotLineAssistant.WebApi.Authorization
{
	public class HttpCurrentUserProvider : ICurrentUserProvider
	{
		private readonly IHttpContextAccessor _httpContext;

		public HttpCurrentUserProvider(IHttpContextAccessor httpContext)
		{
			_httpContext = Assure.ArgumentNotNull(httpContext, nameof(httpContext));
		}

		public ClaimsPrincipal GetUser()
		{
			return _httpContext.HttpContext.User;
		}
	}
}