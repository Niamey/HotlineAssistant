using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Vostok.Hotline.Core.Common.Base.Abstractions;
using Vostok.Hotline.Core.Common.SessionContext.Middlewares;

namespace Vostok.Hotline.Core.Common.SessionContext.Bootstrappers
{
	public static class SessionContextBootstrapper
	{
		public static void AddHotlineWebApiSessionContextRules(this IServiceCollection services)
		{
			services.TryAddTransient<IHttpContextAccessor, HttpContextAccessor>();
			services.AddScoped<ISessionContext, RequestSessionContextAdapter>();
		}

		public static void UseWebApiSessionContextMiddleware(this IApplicationBuilder app)
		{
			app.UseMiddleware<AspNetSessionContextMiddleware>();
		}
	}
}
