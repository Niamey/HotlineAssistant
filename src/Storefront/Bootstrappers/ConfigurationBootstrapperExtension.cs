using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Storefront.Requests.Handlers.Configurations;
using Vostok.Hotline.Storefront.Requests.Queries.Configurations;
using Vostok.Hotline.Storefront.Services.Configurations;
using Vostok.Hotline.Storefront.ViewModels.Configurations;

namespace Vostok.Hotline.Storefront.Bootstrappers
{
	public static class ConfigurationBootstrapperExtension
	{
		public static void AddConfigurationRules(this IServiceCollection services)
		{
			services.AddTransient<ConfigurationService>();

			services.AddTransient<IRequestHandler<ConfigurationQuery, ConfigurationViewModel>, ConfigurationQueryHandler>();
		}
	}
}
