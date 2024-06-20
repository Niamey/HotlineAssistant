using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using Vostok.Hotline.Core.Common.ActionFilters;
using Vostok.Hotline.Core.Common.Conversions;
using Vostok.Hotline.Core.Common.GlobalContext.Bootstrappers;
using Vostok.Hotline.Core.Common.Security.Attributes;

namespace Vostok.Hotline.Core.Common.Bootstrappers
{
	public static class ControllerBootstrapperExtension
	{
		public static IMvcBuilder AddHotlineControllerRules(this IServiceCollection services)
		{
			services.AddScoped<GeneralValidateActionFilter>();
			services.AddScoped<HotlineAuthorizationFilter>();

			var builder = services
				.AddControllers(options =>
				{
					options.Filters.Add(typeof(HotlineAuthorizationFilter));
					options.Filters.Add(typeof(GeneralValidateActionFilter));
					options.Filters.Add(typeof(GeneralExceptionFilter));
				});

			builder.AddNewtonsoftJson(options => {
				options.SerializerSettings.Converters.Add(new StringEnumConverter());
				options.SerializerSettings.Converters.Add(new HotlineStringEnumConverter());
			})
			.AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
			.AddXmlSerializerFormatters();

			services.Configure<ApiBehaviorOptions>(options =>
			{
				options.SuppressModelStateInvalidFilter = true;
			});

			return builder;
		}

		public static void ConfigureController(this IApplicationBuilder app)
		{
			app.ConfigureHttpGlobalContext();
		}
	}
}
