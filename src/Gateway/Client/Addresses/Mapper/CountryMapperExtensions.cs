using Vostok.Hotline.Gateway.Client.Addresses.Services.Models.Responses;
using Vostok.Hotline.Gateway.Client.Addresses.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Addresses.Mapper
{
	public static class CountryMapperExtensions
	{
		public static CountryViewModel ToCountryViewModel(this CountryResponseModel response)
		{
			var result = new CountryViewModel
			{
				CountryId = response.Id,
				CountryCode = response.CountryA3,
				CountryName = response.Name,
			};

			return result;
		}
	}
}
