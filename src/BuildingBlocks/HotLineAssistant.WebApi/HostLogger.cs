using Microsoft.Extensions.Configuration;
using Serilog;

namespace Vostok.HotLineAssistant.WebApi
{
	public static class HostLogger<TProgram> where TProgram: class
	{
		private static readonly string Namespace = typeof(TProgram).Namespace;
		private static readonly string AppName = Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);
		public static Serilog.ILogger CreateSeriLogLogger(IConfiguration configuration)
		{
			var seqServerUrl = configuration["Serilog:SeqServerUrl"];
			var logStashUrl = configuration["Serilog:LogStashUrl"];
			return new LoggerConfiguration()
				.MinimumLevel.Verbose()
				.Enrich.WithProperty("ApplicationContext", AppName)
				.Enrich.FromLogContext()
				.WriteTo.Console()
				.WriteTo.Seq(string.IsNullOrWhiteSpace(seqServerUrl) ? "http://seq" : seqServerUrl)
				.WriteTo.Http(string.IsNullOrWhiteSpace(logStashUrl) ? "http://logstash:8080" : logStashUrl)
				.ReadFrom.Configuration(configuration)
				.CreateLogger();
		}
	}
}