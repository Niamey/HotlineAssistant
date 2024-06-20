using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Gateway.Client.Accounts.Bootstrappers;
using Vostok.Hotline.Gateway.Client.Agreements.Bootstrappers;
using Vostok.Hotline.Gateway.Client.Cards.Bootstrappers;
using Vostok.Hotline.Gateway.Client.Counterpart.Bootstrappers;
using Vostok.Hotline.Api.References.Bootstrappers;
using Vostok.Hotline.Api.Retail.Bootstrappers;
using Vostok.Hotline.Api.CRM.Bootstrappers;
using Vostok.Hotline.Authorization.Hosts;
using Vostok.Hotline.Gateway.Client.Transactions.Bootstrappers;
using Vostok.Hotline.Api.Services.Bootstrappers;
using Vostok.Hotline.Core.Documents.Bootstrappers;
using Vostok.Hotline.Data.EF.Bootstrappers;
using Vostok.Hotline.Data.Repository.Core.Bootstrappers;
using Vostok.Hotline.Api.Bvr.Bootstrappers;
using Vostok.Hotline.Data.Repository.Solar.Bootstrappers;
using Vostok.Hotline.Data.Repository.References.Bootstrappers;
using Vostok.Hotline.Gateway.Client.Classifiers.Bootstrappers;
using Vostok.Hotline.Gateway.Client.Travels.Bootstrappers;
using Vostok.HttpClient.DependencyInfection;
using Vostok.Hotline.Gateway.Client.Countries.Bootstrappers;
using Vostok.Hotline.Gateway.Client.Statements.Bootstrappers;

namespace Vostok.Hotline.Gateway.Client.WebApi
{
	public class Startup : WebStartupAuthorizationBase
	{
		public Startup(IConfiguration configuration)
			: base(configuration)
		{
		}

		public override string WebApplicationName => "Gateway.Client.WebApi";

		protected override void AfterConfigureService(IServiceCollection services)
		{
			services.AddHotlineCoreEfRules();
			services.AddHotlineSolarEfRules();
			services.AddHotlineReferencesEfRules();
			services.AddMemoryCache();

			services.AddEnvironmentSettingRules();
			
			services.AddRetailSystemProviderClientRules(Configuration);
			services.AddServiceProviderClientRules(Configuration);
			services.AddTravelSettingRules();

			services.AddRepositoryTransactionsRules();
			services.AddRepositoryReferencesRules();

			services.AddCounterpartRules();
			services.AddCardsRules();
			services.AddClassifiersRules();
			services.AddAgreementsRules();
			services.AddAccountsRules();
			services.AddTransactionsRules();
			services.AddTravelsRules();
			services.AddCountriesRules();
			services.AddStatementsRules();

			services.AddApiCounterpartRules();
			services.AddApiAgreementRules();
			services.AddApiAccountRules();
			services.AddApiClassifierRules();
			services.AddApiBvrRules();
			services.AddApiCardRules();
			services.AddApiReferencesRules();
			services.AddApiTransactionRules();
			services.AddApiTravelRules();
			services.AddApiServicesRules();
			services.AddApiMoneyBoxRules();
			services.AddApiServicesRules();

			services.AddLoggingRequestSettingRules();

			//after registering all the dependencies
			services.AddDocumentGenerationRules();			
		}
	}
}
