using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Api.Services.Bootstrappers;
using Vostok.Hotline.Authorization.Bootstrappers;
using Vostok.Hotline.Authorization.Hosts;
using Vostok.Hotline.Core.Documents.Bootstrappers;
using Vostok.Hotline.Data.EF.Bootstrappers;
using Vostok.Hotline.Data.Repository.Core.Bootstrappers;
using Vostok.Hotline.Storefront.Bootstrappers;
using Vostok.HttpClient.DependencyInfection;

namespace Vostok.Hotline.Storefront.WebApi
{
	public class Startup : WebStartupAuthorizationBase
	{
		public Startup(IConfiguration configuration)
			: base(configuration)
		{
		}

		public override string WebApplicationName => "Storefront.WebApi";

		protected override void AfterConfigureService(IServiceCollection services)
		{
			services.AddHotlineCoreEfRules();
			services.AddEnvironmentSettingRules();

			services.AddConfigurationRules();
			services.AddMenuRules();
			services.AddSearchLinksRules();
			services.AddModulesRules();
			services.AddFeedBackRules();

			services.AddApiServicesRules();

			//after registering all the dependencies
			services.AddDocumentGenerationRules();
		}
	}
}
