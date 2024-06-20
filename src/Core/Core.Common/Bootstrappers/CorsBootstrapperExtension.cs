using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Vostok.Hotline.Core.Common.Bootstrappers
{
	public static class CorsBootstrapperExtension
	{
		public static void AddHotlineCorsRules(this IServiceCollection services)
		{
			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy",
					builder => builder
						.AllowAnyMethod()
						.AllowAnyHeader()
						.SetIsOriginAllowed((host) => true)
						.AllowCredentials()
						.WithExposedHeaders("hotline-app-version"));
			});
		}

		public static void UseHotlineCors(this IApplicationBuilder app)
		{
			app.UseCors("CorsPolicy");
		}
	}
}
