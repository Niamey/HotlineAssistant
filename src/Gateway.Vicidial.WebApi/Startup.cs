using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Api.CRM.Bootstrappers;
using Vostok.Hotline.Authorization.Hosts;
using Vostok.Hotline.Data.EF.Bootstrappers;
using Vostok.Hotline.Data.Repository.Core.Bootstrappers;
using Vostok.Hotline.Gateway.Vicidial.Bootstrappers;

namespace Vostok.Hotline.Gateway.Vicidial.WebApi
{
	public class Startup : WebStartupAuthorizationBase
	{
		public Startup(IConfiguration configuration)
			: base(configuration)
		{
		}

		public override string WebApplicationName => "Gateway.Vicidial.WebApi";

		protected override void AfterConfigureService(IServiceCollection services)
		{
			services.AddHotlineCoreEfRules();
			services.AddEnvironmentSettingRules();
			services.AddApiDivisionRules();
			services.AddApiCounterpartRules();
			services.AddLoggingRequestSettingRules();

			services.AddAreonRules();
			services.AddChatterRules();
		}
	}
}
