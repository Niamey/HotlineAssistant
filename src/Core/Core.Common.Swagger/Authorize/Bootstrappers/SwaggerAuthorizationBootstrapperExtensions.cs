using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Vostok.Hotline.Core.Common.Swagger.Authorize.Bootstrappers
{
	public static class SwaggerAuthorizationBootstrapperExtensions
	{
		public static void AddHotlineAuthorizationSwaggerRules(this IServiceCollection services)
		{
			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
					.AddCookie(options =>
					{
						options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
						options.ExpireTimeSpan = TimeSpan.FromHours(1);
					});
		}

		public static void UseHotlineAuthorizationSwaggerRules(this IApplicationBuilder applicationBuilder)
		{
			applicationBuilder.UseSwaggerAuthorization();
		}
	}
}
