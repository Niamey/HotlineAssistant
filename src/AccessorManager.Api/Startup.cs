using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Solar.SDK.Extensions;
using System.Collections.Generic;
using System.Reflection;
using Vostok.HotLineAssistant.AccessorManager.Application.Accessors.Queries;
using Vostok.HotLineAssistant.AccessorManager.Domain.Accessors;
using Vostok.HotLineAssistant.AccessorManager.Infrastructure.QueryHandlers;
using Vostok.HotLineAssistant.WebApi;
using Vostok.HotLineAssistant.WebApi.Extensions;

namespace Vostok.HotLineAssistant.AccessorManager.Api
{
	public class Startup : WebApiBaseStartup
	{
		public Startup(IConfiguration configuration) : base(configuration)
		{
		}

		protected override string ApplicationName => "HotLineAssistant.AccessorManager";
		protected override Assembly Api => typeof(Startup).Assembly;
		protected override Assembly Application => typeof(AccessorByIdQuery).Assembly;
		protected override Assembly Infrastructure => typeof(AccessorQueryHandler).Assembly;

		protected override Dictionary<string, string> Scopes => new Dictionary<string, string>
		{
			{"Vostok.HotLineAssistant.AccessorManager", "Accessor Service API"}
		};

		protected override void BeforeConfigureService(IServiceCollection services)
		{
			services.AddSolar();
			services.AddScoped<IAccessorService, AccessorService>();
		}

		protected override void BeforeConfigureApp(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory,
			IApiVersionDescriptionProvider provider)
		{
			
		}
	}
}
