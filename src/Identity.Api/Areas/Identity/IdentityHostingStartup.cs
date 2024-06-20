using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Vostok.HotLineAssistant.Identity.Api.Areas.Identity.IdentityHostingStartup))]
namespace Vostok.HotLineAssistant.Identity.Api.Areas.Identity
{
	public class IdentityHostingStartup : IHostingStartup
	{
		public void Configure(IWebHostBuilder builder)
		{
			builder.ConfigureServices((context, services) => {
			});
		}
	}
}