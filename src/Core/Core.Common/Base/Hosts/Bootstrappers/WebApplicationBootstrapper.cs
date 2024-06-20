using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Vostok.Hotline.Core.Common.Loggers.Bootstrappers;

namespace Vostok.Hotline.Core.Common.Base.Hosts.Bootstrappers
{
	public static class WebApplicationBootstrapper<TStartup>
		where TStartup : class
	{
		public static void Run(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args)
		{
			return Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<TStartup>();
				}).UseHotlineLog(); 
		}
	}
}
