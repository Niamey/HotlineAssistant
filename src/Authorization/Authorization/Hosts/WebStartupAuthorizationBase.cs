using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Core.Common.Base.Hosts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Vostok.Hotline.Authorization.Security;
using Vostok.Hotline.Core.Common.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Vostok.Hotline.Core.Common.Security;
using Vostok.Hotline.Authorization.Security.Helpers;
using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Linq;
using Vostok.Hotline.Authorization.Bootstrappers;

namespace Vostok.Hotline.Authorization.Hosts
{
	public abstract class WebStartupAuthorizationBase : WebStartupBase
	{
		public WebStartupAuthorizationBase(IConfiguration configuration)
			: base(configuration)
		{
		}

		protected override void AuthorizationConfigure(IServiceCollection services)
		{
			services.AddAuthorizationRules();

			services.AddScoped<UserSession>(serviceProvider =>
			{
				var userSessionService = serviceProvider.GetService<UserSessionService>();

				return userSessionService.GetCurrentSession();				
			});

			var serviceProvider = services.BuildServiceProvider();
			var env = serviceProvider.GetRequiredService<HotlineEnvironment>();

			var jwtTokenService = new JwtTokenService(env);

			services.AddSingleton<JwtTokenService>(jwtTokenService);

			services.AddAuthentication(config =>
			{
				config.DefaultScheme = "hotline_auth";
			})
			.AddPolicyScheme("hotline_auth", "Bearer or Jwt", options =>
			{
				options.ForwardDefaultSelector = context =>
				{
					var bearerAuth = context.Request.Headers["Authorization"].FirstOrDefault()?.StartsWith("Bearer ") ?? false;
					if (bearerAuth)
						return JwtBearerDefaults.AuthenticationScheme;
					else
						return CookieAuthenticationDefaults.AuthenticationScheme;
				};
			})
			.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
			{
				options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
				options.ExpireTimeSpan = TimeSpan.FromHours(1);
			}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
			{
				options.TokenValidationParameters = jwtTokenService.GetTokenValidationParameters();
			});
		}

		protected override void BeforeConfigure(IApplicationBuilder app, HotlineEnvironment env)
		{			
			app.UseAuthentication();
			app.UseCookiePolicy();
			app.UseRouting();
			app.UseAuthorization();
		}
	}
}
