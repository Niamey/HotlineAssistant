using AgreementManager.Domain.Agreements;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Solar.SDK.Extensions;
using System.Collections.Generic;
using System.Reflection;
using AutoMapper;
using Vostok.HotLineAssistant.AgreementManager.Application.Agreements.Queries;
using Vostok.HotLineAssistant.AgreementManager.Infrastructure.QueryHandlers.Agreements;
using Vostok.HotLineAssistant.WebApi;
using Vostok.HotLineAssistant.WebApi.Extensions;

namespace Vostok.HotLineAssistant.AgreementManager.Api
{
	public class Startup : WebApiBaseStartup
	{
		public Startup(IConfiguration configuration) : base(configuration)
		{
		}

		protected override string ApplicationName => "HotLineAssistant.AgreementManager";
		protected override Assembly Api => typeof(Startup).Assembly;
		protected override Assembly Application => typeof(AgreementsByIdQuery).Assembly;
		protected override Assembly Infrastructure => typeof(AgreementsQueryHandler).Assembly;

		protected override Dictionary<string, string> Scopes => new Dictionary<string, string>
		{
			{"Vostok.HotLineAssistant.AgreementManager", "Agreement Service API"}
		};

		protected override void BeforeConfigureService(IServiceCollection services)
		{
			services
				.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>(), Infrastructure);
			services.AddSolar();
			services.AddScoped<IAgreementService, AgreementService>();
			services.AddScoped<IAgreementBalanceService, AgreementBalanceService>();
		}

		protected override void BeforeConfigureApp(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory,
			IApiVersionDescriptionProvider provider)
		{
			
		}
	}
}
