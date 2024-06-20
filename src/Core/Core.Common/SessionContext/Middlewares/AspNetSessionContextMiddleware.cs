using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Vostok.Hotline.Core.Common.Base.Abstractions;

namespace Vostok.Hotline.Core.Common.SessionContext.Middlewares
{
	public class AspNetSessionContextMiddleware
	{
		private readonly RequestDelegate _next;

		public AspNetSessionContextMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext httpContext, ISessionContext sessionContext)
		{
			httpContext.Response.OnStarting((Func<Task>)(() =>
			{
				httpContext.Response.Headers.Add(ISessionContext.SessionKey, sessionContext.SessionId.ToString());

				return Task.CompletedTask;
			}));

			await this._next(httpContext);
		}
	}
}
