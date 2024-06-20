using NLog.Web;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Core.Common.Loggers.Abstractions;
using Microsoft.Extensions.Hosting;
using NLog;
using Vostok.Hotline.Core.Common.Helpers;
using Vostok.Hotline.Core.Common.Loggers.Configurations;
using NLog.Targets;
using System;
using NLog.Targets.Wrappers;
using Nlog.RabbitMQ.Target;
using System.Linq;
using Vostok.Hotline.Core.Common.Configurations;
using Vostok.Hotline.Core.Common.Loggers.Configurations.Base;

namespace Vostok.Hotline.Core.Common.Loggers.Bootstrappers
{
	public static class HotlineLoggerBootstrapperExtension
	{
		public static void AddHotlineWebLoggerRules(this IServiceCollection services, string projectName)
		{
			Assure.ArgumentNotNullNotEmpty(projectName, nameof(projectName));

			NLog.LogManager.LoadConfiguration("web-nlog.config");
			AddLoggerRules(services, projectName);

			LogManager.Configuration.Variables["instance-name"] = $"{projectName}|";
		}

		private static void AddRabbitLoggerRules(IServiceCollection services, string projectName)
		{
			services.AddSingleton<RabbitMQLoggerConfig>();

			var serviceProvider = services.BuildServiceProvider();
			var logConfig = serviceProvider.GetRequiredService<RabbitMQLoggerConfig>();
			if (logConfig.Disable)
			{
				DisableTarget(logConfig.Key);
				return;
			}

			var hotlineEnvironment = serviceProvider.GetRequiredService<HotlineEnvironment>();

			RabbitMQTarget rabbitTarget;
			var target = LogManager.Configuration.FindTargetByName("rabbit");
			if (target is AsyncTargetWrapper)
				rabbitTarget = (target as AsyncTargetWrapper).WrappedTarget as RabbitMQTarget;
			else
				rabbitTarget = target as RabbitMQTarget;

			if (rabbitTarget == null)
				throw new NullReferenceException("Can't find RabbitMQTarget with name 'rabbit' ");

			rabbitTarget.Exchange = logConfig.Exchange;
			rabbitTarget.HostName = logConfig.Host;
			rabbitTarget.Password = logConfig.Password;
			rabbitTarget.Port = (ushort)logConfig.Port;
			rabbitTarget.UserName = logConfig.Usert;
			rabbitTarget.VHost = logConfig.Vhost;
			rabbitTarget.AppId = projectName;

			rabbitTarget.Fields.Where(x => x.Name == "project").First().Layout = projectName;
			rabbitTarget.Fields.Where(x => x.Name == "environment").First().Layout = hotlineEnvironment.EnvironmentName;

			ApplyBaseRule(logConfig);
		}

		private static void AddDatabaseLoggerRules(IServiceCollection services, string projectName)
		{
			services.AddSingleton<DbLoggerConfig>();

			var serviceProvider = services.BuildServiceProvider();
			var logConfig = serviceProvider.GetRequiredService<DbLoggerConfig>();
			if (logConfig.Disable)
			{
				DisableTarget(logConfig.Key);
				return;
			}

			DatabaseTarget databaseTarget;
			var target = LogManager.Configuration.FindTargetByName("database");
			if (target is AsyncTargetWrapper)
				databaseTarget = (target as AsyncTargetWrapper).WrappedTarget as DatabaseTarget;
			else
				databaseTarget = target as DatabaseTarget;

			if (databaseTarget == null)
				throw new NullReferenceException("Can't find DatabaseTarget with name 'database' ");

			databaseTarget.ConnectionString = logConfig.ConnectionString;

			ApplyBaseRule(logConfig);
		}

		private static void AddEmailLoggerRules(IServiceCollection services, string projectName)
		{
			services.AddSingleton<EmailLoggerConfig>();

			var serviceProvider = services.BuildServiceProvider();
			var logConfig = serviceProvider.GetRequiredService<EmailLoggerConfig>();
			if (logConfig.Disable)
			{
				DisableTarget(logConfig.Key);
				return;
			}

			MailTarget databaseTarget;
			var target = LogManager.Configuration.FindTargetByName("smtp");
			if (target is AsyncTargetWrapper)
				databaseTarget = (target as AsyncTargetWrapper).WrappedTarget as MailTarget;
			else
				databaseTarget = target as MailTarget;

			if (databaseTarget == null)
				throw new NullReferenceException("Can't find MailTarget with name 'smtp' ");

			if(logConfig.HasCC)
				databaseTarget.CC = logConfig.GetCC();

			if (logConfig.HasFrom)
				databaseTarget.From = logConfig.From;

			ApplyBaseRule(logConfig);
		}

		private static void AddConsoleLoggerRules(IServiceCollection services, string projectName)
		{
			services.AddSingleton<ConsoleLoggerConfig>();

			var serviceProvider = services.BuildServiceProvider();
			var logConfig = serviceProvider.GetRequiredService<ConsoleLoggerConfig>();

			ApplyBaseRule(logConfig);
		}

		private static void AddDebugLoggerRules(IServiceCollection services, string projectName)
		{
			services.AddSingleton<DebugLoggerConfig>();

			var serviceProvider = services.BuildServiceProvider();
			var logConfig = serviceProvider.GetRequiredService<DebugLoggerConfig>();

			ApplyBaseRule(logConfig);
		}

		private static void ApplyBaseRule(BaseLoggerConfig logConfig)
		{
			SetLogLevel(logConfig.Key, logConfig.Level);

			if (logConfig.Disable)
				DisableTarget(logConfig.Key);

			LogManager.ReconfigExistingLoggers();
		}

		private static void DisableTarget(string key)
		{
			foreach (var rule in LogManager.Configuration.LoggingRules)
			{
				foreach (var target in rule.Targets)
				{
					if (target.Name == key)
					{
						rule.DisableLoggingForLevels(LogLevel.Trace, LogLevel.Fatal);
					}
				}
			}

			LogManager.ReconfigExistingLoggers();
		}

		private static void SetLogLevel(string key, LogLevel newLogLevel)
		{
			foreach (var rule in LogManager.Configuration.LoggingRules)
			{
				foreach (var target in rule.Targets)
				{
					if (target.Name == key)
					{
						rule.SetLoggingLevels(newLogLevel, LogLevel.Fatal);
					}
				}
			}

			LogManager.ReconfigExistingLoggers();
		}

		private static void AddLoggerRules(IServiceCollection services, string projectName)
		{
			AddRabbitLoggerRules(services, projectName);
			AddDatabaseLoggerRules(services, projectName);
			AddConsoleLoggerRules(services, projectName);
			AddDebugLoggerRules(services, projectName);
			AddEmailLoggerRules(services, projectName);

			services.AddScoped(typeof(ILoggingService<>), typeof(HotlineLogger<>));
		}

		public static IHostBuilder UseHotlineLog(this IHostBuilder builder)
		{
			return builder.UseNLog();
		}
	}
}
