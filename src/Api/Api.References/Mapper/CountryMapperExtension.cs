using Vostok.Hotline.Api.References.Models;
using Vostok.Hotline.Api.References.Responses;

namespace Vostok.Hotline.Api.References.Mapper
{
	internal static class CountryMapperExtension
	{
		internal static CountryApiModel ToCountryApiModel(this CountryResponseModel response)
		{
			var result = new CountryApiModel
			{
				CountryId = response.Id,
				CountryCode = response.CountryA3,
				CountryName = response.Name,
				CountryA2 = response.CountryA2
			};

			return result;
		}

		internal static CountryCollectionApiModel ToCountriesApiModel(this CountryCollectionResponseModel response)
		{
			var result = new CountryCollectionApiModel();
			foreach (var row in response.Rows)
			{
				result.Add(row.ToCountryApiModel());
			}

			return result;
		}
	}
}
