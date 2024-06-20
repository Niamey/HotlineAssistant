using Microsoft.Extensions.DependencyInjection;

namespace Vostok.Hotline.Api.References.Bootstrappers
{
	public static class ReferencesBootstrapperExtension
	{
		public static void AddApiReferencesRules(this IServiceCollection services)
		{
			services.AddApiCountryRules();
			services.AddApiAddressRules();
			services.AddApiCurrencyRules();
			services.AddApiDistrictRules();
			services.AddApiRegionRules();
		}
	}
}
