using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Vostok.HotLineAssistant.WebApi.Extensions
{
	public static class ApplicationConfigurationExtensions
	{
		public static IApplicationBuilder AddSwagger(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
		{
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				foreach (var description in provider.ApiVersionDescriptions)
				{
					c.RoutePrefix = String.Empty;
					c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
				}
			});

			return app;
		}

		public static IApplicationBuilder ConfigLogger<TStartup>(this IApplicationBuilder app, IConfiguration configuration, ILoggerFactory loggerFactory) where TStartup : class
		{
			var pathBase = configuration["PATH_BASE"];

			if (string.IsNullOrEmpty(pathBase)) return app;
			loggerFactory.CreateLogger<TStartup>().LogDebug("Using PATH BASE '{pathBase}'", pathBase);
			app.UsePathBase(pathBase);

			return app;
		}
	}
}