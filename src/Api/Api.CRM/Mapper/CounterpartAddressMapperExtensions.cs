using Vostok.Hotline.Api.CRM.Models;
using Vostok.Hotline.Api.CRM.Responses.Models;
using System.Collections.Generic;
using Vostok.Hotline.Api.CRM.Mapper;

namespace Vostok.Hotline.Api.CRM.Mapper
{
	internal static class CounterpartAddressMapperExtensions
	{
		public static CounterpartAddressApiModel MapToCounterpartAddressApiModel(this CounterpartAddressResponseModel response)
		{
			var result = new CounterpartAddressApiModel
			{
				AddressId = response.AddressId,
				AddressType = response.AddressType?.ToAddressTypeEnum(),
				CityName = response.CityName,
				CityTypeId = response.CityTypeId,
				CounterpartAddressId = response.CounterpartAddressId,
				DistrictName = response.DistrictName,
				Flat = response.Flat,
				HouseNumber = response.HouseNumber,
				HouseNumberAdd = response.HouseNumberAdd,
				Korp = response.Korp,
				PostIndex = response.PostIndex,
				RegionName = response.RegionName,
				StreetName = response.StreetName,
				StreetTypeId = response.StreetTypeId
			};

			return result;
		}

		public static List<CounterpartAddressApiModel> MapToCounterpartAddressApiModel(this IEnumerable<CounterpartAddressResponseModel> response)
		{
			var result = new List<CounterpartAddressApiModel>();
			foreach (var row in response)
			{
				result.Add(row.MapToCounterpartAddressApiModel());
			}

			return result;
		} 
	}
}
