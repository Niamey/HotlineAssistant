using Vostok.HotLineAssistant.WebApi;

namespace Vostok.HotLineAssistant.AgreementManager.Api
{
	public class Program
	{
		public static int Main(string[] args)
		{
			return HostBootstrap<Program, Startup>.Run(args);
		}
	}
}
