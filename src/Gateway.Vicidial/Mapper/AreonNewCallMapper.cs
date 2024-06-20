using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Api.CRM.Managers;
using Vostok.Hotline.Api.CRM.Models;
using Vostok.Hotline.Api.References.Managers;
using Vostok.Hotline.Core.Common.ConstantNames;
using Vostok.Hotline.Core.Common.Enums.Counterparts;
using Vostok.Hotline.Data.Repository.Core.Managers;
using Vostok.Hotline.Gateway.Vicidial.ViewModels;

namespace Vostok.Hotline.Gateway.Vicidial.Mapper
{
	public class AreonNewCallMapper
	{
		private readonly EnvironmentManager _environmentManager;
		private readonly CounterpartApiManager _counterpartManager;
		private readonly DivisionApiManager _divisionManager;



		public AreonNewCallMapper(IServiceProvider serviceProvider)
		{
			_environmentManager = serviceProvider.GetRequiredService<EnvironmentManager>();
			_counterpartManager = serviceProvider.GetRequiredService<CounterpartApiManager>();
			_divisionManager = serviceProvider.GetRequiredService<DivisionApiManager>();
		}

		public async Task<AreonNewCallResponseViewModel> MapToAreonNewCallResponseViewModelAsync(CounterpartSearchByPhoneApiModel response, CancellationToken cancellationToken)
		{
			if (response?.SolarId is null)
			{
				var host = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.UiEndpoint.Storefront.DetailUrl, cancellationToken);
				return new AreonNewCallResponseViewModel
				{
					Url = $"{host}",
					Guid = Guid.NewGuid()
				};
			}
			else
			{
				var host = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.UiEndpoint.Storefront.ClientDetailUrl, cancellationToken);
				return new AreonNewCallResponseViewModel
				{
					Url = $"{host}/client/{response.SolarId}",
					Guid = Guid.NewGuid()
				};
			}
		}

		public async Task<AreonNewDetailCallResponseViewModel> MapToAreonNewDetailCallResponseViewModelAsync(CounterpartSearchByPhoneApiModel response, CancellationToken cancellationToken)
		{
			if (response?.SolarId is null)
			{
				var host = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.UiEndpoint.Storefront.DetailUrl, cancellationToken);
				return new AreonNewDetailCallResponseViewModel
				{
					Url = $"{host}"
				};
			}
			else
			{
				var host = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.UiEndpoint.Storefront.ClientDetailUrl, cancellationToken);
				var counterpart = await _counterpartManager.GetBySolarIdAsync(response.SolarId.Value, cancellationToken);
				return new AreonNewDetailCallResponseViewModel
				{
					Url = $"{host}/client/{response.SolarId}",
					FullName = response.FullName,
					IsVip = (byte)(response.SegmentationType == SegmentTypeEnum.Vip ? 1 : 0),
					Office = counterpart?.DivisionId != null ? await _divisionManager.GetDivisionNameAsync(counterpart.DivisionId.Value, cancellationToken) : null
				};
			}
		}
	}
}
