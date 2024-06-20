using Vostok.Hotline.Core.Common.Base.Hosts.Bootstrappers;

namespace Vostok.Hotline.Gateway.Vicidial.WebApi
{
	public class Program
	{
		public static void Main(string[] args)
		{
			WebApplicationBootstrapper<Startup>.Run(args);
		}
	}
}
