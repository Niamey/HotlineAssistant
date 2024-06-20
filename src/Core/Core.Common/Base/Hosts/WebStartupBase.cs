using System.Reflection;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Vostok.Hotline.Core.Common.Bootstrappers;
using Vostok.Hotline.Core.Common.Configurations;
using Vostok.Hotline.Core.Common.Managers;
using Vostok.Hotline.Core.Common.SessionContext.Bootstrappers;
using Vostok.Hotline.Core.Common.Loggers.Bootstrappers;
using Vostok.Hotline.Core.Common.Swagger.Authorize.Bootstrappers;
using Vostok.Hotline.Core.Common.Configurations.Bootstrappers;
using Vostok.Hotline.Core.Common.Helpers;

namespace Vostok.Hotline.Core.Common.Base.Hosts
{
	public abstract class WebStartupBase
	{
		public abstract string WebApplicationName { get; }

		public WebStartupBase(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			// If using Kestrel:
			services.Configure<KestrelServerOptions>(options =>
			{
				options.AllowSynchronousIO = true;
			});

			// If using IIS:
			services.Configure<IISServerOptions>(options =>
			{
				options.AllowSynchronousIO = true;
			});

			services.AddHotlineEnvironmentRules(Configuration);
			services.AddOptions();
			services.AddHotlineWebLoggerRules($"{WebApplicationName}.WebApi");

			AuthorizationConfigure(services);

			services.AddHotlineWebApiSessionContextRules();
			services.AddSingleton<HotlineHttpClient>();

			services
				.AddHotlineControllerRules()
				.AddFluentValidation(s =>
				{
					s.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
				});

			var serviceProvider = services.BuildServiceProvider();
			var env = serviceProvider.GetRequiredService<HotlineEnvironment>();

			services.AddSwaggerGen(options =>
			{
				//options.SchemaFilter<SwaggerExcludeSchemaFilter>();
				options.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = WebApplicationName,
					Description = $"ASP.NET Core web API <br />Environment: [{env.EnvironmentName?.ToUpper()}]",
					Version = "v1"
				});

				options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					In = ParameterLocation.Header,
					Description = "Please insert JWT with Bearer into field",
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey
				});

				options.AddSecurityRequirement(new OpenApiSecurityRequirement {
				   {
					 new OpenApiSecurityScheme
					 {
					   Reference = new OpenApiReference
					   {
						 Type = ReferenceType.SecurityScheme,
						 Id = "Bearer"
					   }
					  },
					  new string[] { }
					}
				});
				options.CustomSchemaIds(i => i.FullName);
			});

			services.AddMediatR(Assembly.GetExecutingAssembly());

			AfterConfigureService(services);

			services.AddHotlineCorsRules();
			services.AddRazorPages();
		}

		protected abstract void AfterConfigureService(IServiceCollection services);

		protected virtual void AuthorizationConfigure(IServiceCollection services)
		{ 
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, HotlineEnvironment env)
		{
			if (!env.IsLive)
			{
				app.UseDeveloperExceptionPage();
			}

			app.ConfigureController();
			app.UseWebApiSessionContextMiddleware();
			app.UseHotlineCors();
			app.UseHttpsRedirection();

			BeforeConfigure(app, env);

			app.UseHotlineAuthorizationSwaggerRules();
			app.UseSwagger();
			app.UseSwaggerUI(options =>
			{
				options.SwaggerEndpoint("/swagger/v1/swagger.json", $"{WebApplicationName} Rest Full API");
				options.RoutePrefix = string.Empty;
				options.InjectStylesheet("/swagger-ui/custom.css");
				options.DefaultModelsExpandDepth(-1);

				if (env.IsLive)
				{
					options.InjectStylesheet("/swagger-ui/live.css");
				}
			});

			app.UseStaticFiles();

			app.Use(async (context, next) =>
			{
				context.Response.Headers.Add("hotline-app-version", VersionHelper.SystemVersion.ToString());

				context.Request.EnableBuffering();
				await next();
			});

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapRazorPages();
				endpoints.MapControllers();
			});
		}

		protected virtual void BeforeConfigure(IApplicationBuilder app, HotlineEnvironment env)
		{
			app.UseRouting();
		}
	}
}
