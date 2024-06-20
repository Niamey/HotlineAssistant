using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using Microsoft.Extensions.HealthChecks;
using Vostok.HotLineAssistant.HttpAggregator.Infrastructure.HttpHandlers;
using Vostok.HotLineAssistant.HttpAggregator.Services.Contact;
using Vostok.HotLineAssistant.WebApi.Extensions;

namespace Vostok.HotLineAssistant.HttpAggregator.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddHealthChecks(this IServiceCollection services,
			IConfiguration configuration)
		{
			services.AddHealthChecks(checks =>
			{
				var contactServiceUrl = configuration["Services:Contact:BaseUrl"].TrimEnd('/') + "/hc";

				checks.AddUrlCheck(contactServiceUrl);
			});

			return services;
		}

		public static IServiceCollection AddSwagger(this IServiceCollection services,
			Assembly assembly)
		{
			return services.AddSwaggerGen(assembly,
				new Dictionary<string, string>
				{
					{ "Vostok.HotLineAssistant.HttpAggregator", "The HotLineAssistant HTTP Aggregator API" },
					{ "Vostok.HotLineAssistant.ContactManager", "Управление контактами" }
				});
		}
		public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddTransient<HttpClientAuthorizationHandler>();
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			services.AddHttpClient<IContactService, HttpContactService>()
				.AddHttpMessageHandler<HttpClientAuthorizationHandler>()
				.AddPolicyHandler(GetRetryPolicy())
				.AddPolicyHandler(GetCircuitBreakerPolicy());

			services.AddTransient<IContactServiceUrlProvider>(s =>
				new ContactServiceUrlProvider(
					new Uri(configuration["Services:Contact:BaseUrl"]),
					new Uri(configuration["Services:ClientSelfService:BaseUrl"])));

			return services;
		}
		private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
		{
			return HttpPolicyExtensions
				.HandleTransientHttpError()
				.WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
		}

		private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
		{
			return HttpPolicyExtensions
				.HandleTransientHttpError()
				.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
		}
	}
}