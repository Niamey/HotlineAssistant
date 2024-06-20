using System;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using Vostok.HotLineAssistant.Application.Extensions;
using Vostok.HotLineAssistant.ContactManager.Application.Client.Queries;
using Vostok.HotLineAssistant.ContactManager.Domain.Clients;
using Vostok.HotLineAssistant.ContactManager.Infrastructure.QueryHandlers.Clients;
using Vostok.HotLineAssistant.WebApi;
using Vostok.HotLineAssistant.WebApi.Extensions;

namespace Vostok.HotLineAssistant.ContactManager.Api
{
	public class Startup : WebApiBaseStartup
	{
		public Startup(IConfiguration configuration) : base(configuration)
		{
		}

		protected override string ApplicationName => "ContactManager";
		protected override Assembly Api => typeof(Startup).Assembly;
		protected override Assembly Application => typeof(ClientSearchQuery).Assembly;
		protected override Assembly Infrastructure => typeof(ClientSearchQueryHandler).Assembly;

		protected override Dictionary<string, string> Scopes => new Dictionary<string, string>
		{
			{"Vostok.HotLineAssistant.ContactManager", "Contact Manager Service API"}
		};

		protected override void BeforeConfigureService(IServiceCollection services)
		{
			services
				.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>(), Infrastructure);
				
			services.AddHttpClient("crm", (provider, client) =>
			{
				var env = provider.GetService<HotlineEnvironment>();
				client.BaseAddress = new Uri(env.GetEnvironmentVariable("CrmUrl"));
			}).ConfigureHttpMessageHandlerBuilder(builder =>
			{
				builder.PrimaryHandler = new HttpClientHandler
				{
					ServerCertificateCustomValidationCallback = (m, c, ch, e) => true
				};
			});

			services.AddScoped<IClientService, ClientService>();
		}

		protected override void BeforeConfigureApp(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory,
			IApiVersionDescriptionProvider provider)
		{
		}
	}
}
