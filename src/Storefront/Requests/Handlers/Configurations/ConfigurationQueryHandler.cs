using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Storefront.Requests.Queries.Configurations;
using Vostok.Hotline.Storefront.Services.Configurations;
using Vostok.Hotline.Storefront.ViewModels.Configurations;

namespace Vostok.Hotline.Storefront.Requests.Handlers.Configurations
{
	public class ConfigurationQueryHandler : IRequestHandler<ConfigurationQuery, ConfigurationViewModel>
	{
		private readonly ConfigurationService _configurationService;

		public ConfigurationQueryHandler(ConfigurationService configurationService)
		{
			_configurationService = configurationService;
		}

		public async Task<ConfigurationViewModel> Handle(ConfigurationQuery request, CancellationToken cancellationToken)
		{
			return await _configurationService.GetConfigurationAsync(request.Localization.Value, cancellationToken);
		}
	}
}
