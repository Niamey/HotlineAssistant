using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Core.Common.ConstantNames;
using Vostok.Hotline.Core.Common.Enums;
using Vostok.Hotline.Core.Common.Helpers;
using Vostok.Hotline.Data.Repository.Core.Managers;
using Vostok.Hotline.Storefront.ViewModels.Configurations;

namespace Vostok.Hotline.Storefront.Services.Configurations
{
	public class ConfigurationService
	{
		private readonly EnvironmentManager _environmentManager;
		public ConfigurationService(EnvironmentManager environmentManager)
		{
			_environmentManager = environmentManager;
		}

		public async Task<ConfigurationViewModel> GetConfigurationAsync(LocalizationEnum value, CancellationToken cancellationToken)
		{
			return new ConfigurationViewModel
			{
				Settings = new SettingViewModel
				{
					CounterpartApiSetting = new ApiSettingViewModel(await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.StorefrontApi.CounterpartUrl, cancellationToken)),
					AccountApiSetting = new ApiSettingViewModel(await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.StorefrontApi.AccountsUrl, cancellationToken)),
					AgreementApiSetting = new ApiSettingViewModel(await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.StorefrontApi.AgreementsUrl, cancellationToken)),
					CardApiSetting = new ApiSettingViewModel(await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.StorefrontApi.CardsUrl, cancellationToken)),
					UserInterfaceApiSetting = new ApiSettingViewModel(await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.StorefrontApi.UserInterfaceUrl, cancellationToken)),
					TransactionApiSetting = new ApiSettingViewModel(await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.StorefrontApi.TransactionsUrl, cancellationToken)),
					ModulesApiSetting = new ApiSettingViewModel(await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.StorefrontApi.ModulesUrl, cancellationToken)),
					TravelApiSetting = new ApiSettingViewModel(await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.StorefrontApi.TravelsUrl, cancellationToken)),
					CountryApiSetting = new ApiSettingViewModel(await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.StorefrontApi.CountriesUrl, cancellationToken)),
					StatementApiSetting = new ApiSettingViewModel(await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.StorefrontApi.StatementUrl, cancellationToken)),
				},
				VersionId = VersionHelper.SystemVersion
			};
		}
	}
}
