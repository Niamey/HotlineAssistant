using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Vostok.HotLineAssistant.HttpAggregator.Extensions;
using Vostok.HotLineAssistant.HttpAggregator.Infrastructure.Filters;
using Vostok.HotLineAssistant.WebApi.Extensions;

namespace Vostok.HotLineAssistant.HttpAggregator
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}
		public IConfiguration Configuration { get; }
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddServices(Configuration).AddHealthChecks(Configuration)
				.AddSwagger(typeof(Startup).Assembly);
			
			services.AddVersionedApiExplorer(options =>
			{
				options.GroupNameFormat = "'v'VVV";
				options.SubstituteApiVersionInUrl = true;
			});

			services.AddApiVersioning(cfg => cfg.ReportApiVersions = true);

			services.AddMvc(options =>
				{
					options.EnableEndpointRouting = false;
					options.Filters.Add<ExceptionFilter>();
				});
			//ConfigureAuthentication(services);
		}

		protected virtual void ConfigureAuthentication(IServiceCollection services)
		{
			services.AddJwtBearerAuthentication("Vostok.HotLineAssistant.HttpAggregator");
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseAuthentication();

			app.UseSwagger();
			app.UseMvc();
			app.UseSwagger()
				.UseSwaggerUI(c =>
				{
					foreach (var description in provider.ApiVersionDescriptions)
					{
						c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
					}
				});
		}
	}
}
