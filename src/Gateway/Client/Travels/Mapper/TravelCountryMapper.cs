using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Data.Repository.Core.Models.Responses;
using Vostok.Hotline.Data.Repository.References.Managers;
using Vostok.Hotline.Gateway.Client.Travels.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Travels.Mapper
{
	public class TravelCountryMapper
	{
		private readonly CountryManager _countryManager;

		public TravelCountryMapper(CountryManager countryManager)
		{
			_countryManager = countryManager;
		}
		public async Task<TravelCountryViewModel> MapToTravelCountryViewModelAsync(TravelCountryResponseModel response, CancellationToken cancellationToken)
		{
			var result = await _countryManager.GetCountryByCodeAsync(response.CountryCode, cancellationToken);

			return new TravelCountryViewModel()
			{
				CountryCode = response.CountryCode,
				IsRisky = result?.IsCountryRisk ?? false,
				CountryA2 = result?.CountryA2,
				CountryName = result?.CountryName
			};
		}

		public async Task<List<TravelCountryViewModel>> MapToListTravelCountryViewModelAsync(List<TravelCountryResponseModel> response, CancellationToken cancellationToken)
		{
			var result = new List<TravelCountryViewModel>();

			if (response == null)
				return result;

			foreach (var item in response)
			{
				result.Add(await MapToTravelCountryViewModelAsync(item, cancellationToken));
			}
			return result;
		}
	}
}
