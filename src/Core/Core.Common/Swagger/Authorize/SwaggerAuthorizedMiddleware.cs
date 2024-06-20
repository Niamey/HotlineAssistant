using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Vostok.Hotline.Core.Common.Swagger.Authorize
{
	public static class SwaggerAuthorizedMiddleware
	{
		public static void UseSwaggerAuthorization(this IApplicationBuilder app)
		{
			app.UseWhen(IsSwagger, HandelAsync);
		}

		private static void HandelAsync(IApplicationBuilder app)
		{
			app.Use(async (context, next) =>
			{
				var authorization = await context.AuthenticateAsync();
				if (authorization.Succeeded)
					await next();
				else
					context.Response.Redirect("/auth/login");

			});
		}

		private static bool IsSwagger(HttpContext context)
		{
			return context.Request.Path.Value.Contains("swagger")
					|| context.Request.Path.Value.Contains("index.html");
		}
	}
}
