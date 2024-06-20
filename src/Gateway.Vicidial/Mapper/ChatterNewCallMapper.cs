using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Api.CRM.Models;
using Vostok.Hotline.Core.Common.ConstantNames;
using Vostok.Hotline.Data.Repository.Core.Managers;
using Vostok.Hotline.Gateway.Vicidial.ViewModels;

namespace Vostok.Hotline.Gateway.Vicidial.Mapper
{
	public class ChatterNewCallMapper
	{
		private readonly EnvironmentManager _environmentManager;


		public ChatterNewCallMapper(IServiceProvider serviceProvider)
		{
			_environmentManager = serviceProvider.GetRequiredService<EnvironmentManager>();
		}

		public async Task<ChatterNewCallResponseViewModel> MapToChatterNewCallResponseViewModelAsync (CounterpartSearchByPhoneApiModel response, CancellationToken cancellationToken)
		{
			if (response?.SolarId is null)
			{
				var host = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.UiEndpoint.Storefront.DetailUrl, cancellationToken);
				return new ChatterNewCallResponseViewModel
				{
					Url = $"{host}"
				};
			}
			else
			{
				var host = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.UiEndpoint.Storefront.ClientDetailUrl, cancellationToken);
				return new ChatterNewCallResponseViewModel
				{
					Url = $"{host}/client/{response.SolarId}"
				};
			}
		}
	}
}
