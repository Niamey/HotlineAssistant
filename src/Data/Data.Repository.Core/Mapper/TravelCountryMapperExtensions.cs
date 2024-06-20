using System.Collections.Generic;
using Vostok.Hotline.Data.EF.Entities.Core;
using Vostok.Hotline.Data.Repository.Core.Models.Responses;

namespace Vostok.Hotline.Data.Repository.Core.Mapper
{
	internal static class TravelCountryMapperExtensions
	{
		public static TravelCountryResponseModel ToTravelCountryResponseModel(this TravelCountry response)
		{
			var result = new TravelCountryResponseModel()
			{
				CountryCode = response.CountryCode,
				RiskCountry = response.RiskCountry
			};

			return result;
		}

		public static List<TravelCountryResponseModel> ToListTravelCountryResponseModel(this ICollection<TravelCountry> response)
		{
			var result = new List<TravelCountryResponseModel>();

			foreach (var item in response)
			{
				result.Add(item.ToTravelCountryResponseModel());
			}

			return result;
		}
	}
}
