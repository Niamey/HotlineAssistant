using Vostok.Hotline.Api.References.Models;
using Vostok.Hotline.Api.References.Responses;

namespace Vostok.Hotline.Api.References.Mapper
{
	internal static class CurrencyMapperExtension
	{
		internal static CurrencyApiModel ToCurrencyApiModel(this CurrencyResponseModel response)
		{
			var result = new CurrencyApiModel
			{
				Id = response.Id,
				CountryId = response.CountryId,
				CurrencyCode = response.CurrencyCode,
				CurrencyShortName = response.CurrencyShortName,
				CurrencyName = response.CurrencyName,
				CurrencyNameUa = response.CurrencyNameUa,
				CurrencyNameRu = response.CurrencyNameRu
			};

			return result;
		}
	}
}
