using System.Collections.Generic;
using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Vostok.HotLineAssistant.Application.Extensions;
using Vostok.HotLineAssistant.WebApi.Extensions;
using Vostok.HotLineAssistant.WebApi.Filters;

namespace Vostok.HotLineAssistant.WebApi
{
	public abstract class WebApiBaseStartup
	{
		protected abstract string ApplicationName { get; }
		protected abstract Assembly Api { get; }
		protected abstract Assembly Application { get; }
		protected abstract Assembly Infrastructure { get; }

		protected abstract Dictionary<string, string> Scopes { get; }

		protected IConfiguration Configuration { get; }

		protected WebApiBaseStartup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services
				.AddOptions()
				.AddEnvironment()
				.AddHealthChecks(Configuration, ApplicationName)
				.AddMediatR(Api)
				.AddMediatorHandlers(Infrastructure)
				.AddCustomMvc()
				.AddSwagger(Api, Scopes)
				.AddTransient<IConfigureOptions<MvcOptions>, MvcDefaultOptionsSetup>();

			services
				.AddApiVersionConfig()
				.AddApiVersioning()
				.AddApiExplorerServices();

			BeforeConfigureService(services);

			services.GetAutofacContainerBuilder(Api, Application, Infrastructure);

			services.BuildServiceProvider();

			services.AddMvc(options =>
				{
					options.EnableEndpointRouting = false;
					options.Filters.Add(typeof(ExceptionFilter));
				})
				.SetCompatibilityVersion(CompatibilityVersion.Latest)
				.AddControllersAsServices();

			services.AddMvcCore().AddApiExplorer();
		}

		protected abstract void BeforeConfigureService(IServiceCollection services);

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory,
			IApiVersionDescriptionProvider provider)
		{
			BeforeConfigureApp(app, env, loggerFactory, provider);
			app.ConfigLogger<WebApiBaseStartup>(Configuration, loggerFactory);
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseCors("CorsPolicy");

			app.UseMvcWithDefaultRoute();
			app.UseHttpsRedirection();
			app.AddSwagger(provider);
		}

		protected abstract void BeforeConfigureApp(IApplicationBuilder app, IWebHostEnvironment env,
			ILoggerFactory loggerFactory, IApiVersionDescriptionProvider provider);
	}
}