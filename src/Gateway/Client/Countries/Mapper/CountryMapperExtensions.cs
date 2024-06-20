using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Data.Repository.References.Models.Responses;
using Vostok.Hotline.Gateway.Client.Countries.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Countries.Mapper
{
	public static class CountryMapperExtensions
	{
		public static CountryViewModel ToCountryViewModel(this CountryResponseModel response)
		{
			return new CountryViewModel()
			{
				CountryCode = response.CountryCode,
				CountryName = response.CountryName,
				CountryA2 = response.CountryA2,
				IsRisky = response.IsCountryRisk
			};
		}

		public static SearchRowsResponseRowModel<CountryViewModel> ToCardListViewModel(this CountryCollectionResponseModel response)
		{
			var result = new SearchRowsResponseRowModel<CountryViewModel>();

			foreach (var item in response)
			{
				result.Rows.Add(item.ToCountryViewModel());
			}

			return result;
		}
	}
}
