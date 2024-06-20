using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using Microsoft.Extensions.HealthChecks;

namespace Vostok.HotLineAssistant.WebApi.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddJwtBearerAuthentication(this IServiceCollection services, string audience)
		{
			JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
			var identityUrl = services.BuildServiceProvider().GetRequiredService<HotlineEnvironment>()
				.GetEnvironmentVariable("IdentityUri");
			if (identityUrl == null)
				throw new InvalidOperationException("Failed to retrieve Identity Server internal url");

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(o =>
				{
					o.Authority = identityUrl;
					o.Audience = audience;
					o.RequireHttpsMetadata = false;
				});

			return services;
		}

		public static IServiceCollection AddSwaggerGen(this IServiceCollection services, Assembly assembly,
			IDictionary<string, string> scopes)
		{
			services.AddSwaggerGen(options =>
			{
				var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
				options.CustomSchemaIds(type => type.FullName);
				foreach (var description in provider.ApiVersionDescriptions)
				{
					options.SwaggerDoc(description.GroupName, new OpenApiInfo
					{
						Title =
							$"{assembly.GetCustomAttribute<AssemblyProductAttribute>()?.Product} {description.ApiVersion}",
						Version = description.ApiVersion.ToString(),
						Description = description.IsDeprecated
							? $"{assembly.GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description} - DEPRECATED"
							: assembly.GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description
					});
				}

				options.OperationFilter<SwaggerDefaultValues>();
				var identityUrl = services.BuildServiceProvider().GetRequiredService<HotlineEnvironment>()
					.GetEnvironmentVariable("IdentityExternalUrl");
				if (identityUrl == null)
					throw new InvalidOperationException("Failed to retrieve Identity Server external url");

				options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
				{
					Type = SecuritySchemeType.OAuth2,
					Flows = new OpenApiOAuthFlows
					{
						Implicit = new OpenApiOAuthFlow
						{
							AuthorizationUrl = new Uri($"{identityUrl}/connect/authorize"),
							TokenUrl = new Uri($"{identityUrl}/connect/token"),
							Scopes = scopes
						}
					}
				});

				options.OperationFilter<SecurityRequirementsOperationFilter>();
			});

			return services;
		}

		public static IServiceCollection AddSwagger(this IServiceCollection services, Assembly assembly, IDictionary<string, string> scopes)
		{
			return services.AddSwaggerGen(assembly, scopes);
		}

		public static IServiceCollection AddApiExplorerServices(this IServiceCollection services)
		{
			services.TryAddSingleton<IApiDescriptionGroupCollectionProvider, ApiDescriptionGroupCollectionProvider>();
			services.TryAddSingleton<IApiVersionDescriptionProvider>();
			services.TryAddEnumerable(
				ServiceDescriptor.Transient<IApiDescriptionProvider, DefaultApiDescriptionProvider>());

			return services;
		}

		public static IServiceCollection AddCustomMvc(this IServiceCollection services)
		{
			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy",
					builder => builder
						.SetIsOriginAllowed(host => true)
						.AllowAnyMethod()
						.AllowAnyHeader()
						.AllowCredentials());
			});

			return services;
		}

		public static IServiceCollection AddEnvironment(this IServiceCollection services)
		{
			services.AddSingleton<HotlineEnvironment>();
			return services;
		}

		public static IServiceCollection AddApiVersionConfig(this IServiceCollection services)
		{
			services.AddApiVersioning(cfg =>
			{
				cfg.ReportApiVersions = true;
				cfg.DefaultApiVersion = new ApiVersion(1, 0);
			});

			services.AddVersionedApiExplorer(options =>
			{
				options.GroupNameFormat = "'v'VVV";
				options.SubstituteApiVersionInUrl = true;
			});
			return services;
		}
		
		public static IServiceCollection AddHealthChecks(this IServiceCollection services,
			IConfiguration configuration, string checkName)
		{
			services.AddHealthChecks(checks =>
			{
				var cacheDurationInMinutes = configuration.GetValue("HealthCheck:CacheDurationInMinutes", 1);

				checks.AddSqlCheck(checkName,
					configuration["ConnectionString"],
					TimeSpan.FromMinutes(cacheDurationInMinutes));
			});

			return services;
		}
	}
}