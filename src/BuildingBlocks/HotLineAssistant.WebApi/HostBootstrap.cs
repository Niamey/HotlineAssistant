using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;

namespace Vostok.HotLineAssistant.WebApi
{
	public static class HostBootstrap<TProgram, TStartup> where TProgram: class where TStartup: class
	{
		private static readonly string Namespace = typeof(TProgram).Namespace;
		private static readonly string AppName = Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);
		public static int Run(string[] args)
		{
			var configuration = GetConfiguration();
			Log.Logger = HostLogger<TProgram>.CreateSeriLogLogger(configuration);

			try
			{
				Log.Information("Configuring web host ({ApplicationContext})...", AppName);
				var host = CreateHostBuilder(configuration, args);

				Log.Information("Starting web host ({ApplicationContext})...", AppName);
				host.Build().Run();

				return 0;
			}
			catch (Exception ex)
			{
				Log.Fatal(ex, "Program terminated unexpectedly ({ApplicationContext})!", AppName);
				return 1;
			}
			finally
			{
				Log.CloseAndFlush();
			}
		}

		public static IHostBuilder CreateHostBuilder(IConfiguration configuration, string[] args) =>
			Host.CreateDefaultBuilder(args)
				.UseServiceProviderFactory(new AutofacServiceProviderFactory())
				.ConfigureWebHostDefaults(builder =>
				{
					builder.UseStartup<TStartup>()
						.CaptureStartupErrors(false)
						.UseContentRoot(Directory.GetCurrentDirectory())
						.UseConfiguration(configuration)
						.UseSerilog()
						.UseHealthChecks("/hc");
				});

		private static IConfiguration GetConfiguration()
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddEnvironmentVariables();

			return builder.Build();
		}
	}
}