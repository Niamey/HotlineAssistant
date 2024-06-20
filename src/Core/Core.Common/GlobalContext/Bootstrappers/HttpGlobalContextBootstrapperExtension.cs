using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Vostok.Hotline.Core.Common.GlobalContext.Bootstrappers
{
	public static class HttpGlobalContextBootstrapperExtension
	{
		public static void ConfigureHttpGlobalContext(this IApplicationBuilder app)
		{
			GlobalHttpContextAccessor.Configure(app.ApplicationServices.GetRequiredService<Microsoft.AspNetCore.Http.IHttpContextAccessor>());
		}
	}
}
