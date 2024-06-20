using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Solar.SDK.Extensions;
using System.Collections.Generic;
using System.Reflection;
using Vostok.HotLineAssistant.CardManager.Application.Cards.Queries;
using Vostok.HotLineAssistant.CardManager.Domain.Models.Cards;
using Vostok.HotLineAssistant.CardManager.Infrastructure.QueryHandlers;
using Vostok.HotLineAssistant.WebApi;
using Vostok.HotLineAssistant.WebApi.Extensions;

namespace Vostok.HotLineAssistant.CardManager.Api
{
	public class Startup : WebApiBaseStartup
	{
		public Startup(IConfiguration configuration) : base(configuration)
		{

		}

		protected override string ApplicationName => "HotLineAssistant.CardManager";
		protected override Assembly Api => typeof(Startup).Assembly;
		protected override Assembly Application => typeof(CardInfoByIdQuery).Assembly;
		protected override Assembly Infrastructure => typeof(CardInfoQueryHandler).Assembly;

		protected override Dictionary<string, string> Scopes => new Dictionary<string, string>
		{
			{"Vostok.HotLineAssistant.CardManager", "Card Service API"}
		};


		protected override void BeforeConfigureService(IServiceCollection services)
		{
			services.AddSolar();
			services.AddScoped<IClientBalanceService, ClientBalanceService>();
			services.AddScoped<IOperationLogService, OperationLogService>();
		}

		protected override void BeforeConfigureApp(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory,
			IApiVersionDescriptionProvider provider)
		{
		}
	}
}
