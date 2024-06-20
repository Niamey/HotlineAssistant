using Vostok.Hotline.Api.CRM.Models;
using Vostok.Hotline.Api.CRM.Responses.Models;
using Vostok.Hotline.Core.Common.Conversions;

namespace Vostok.Hotline.Api.CRM.Mapper
{
	internal static class AddressMapperExtensions
	{
		public static void ToCounterpartAddressModel(this CounterpartAddressResponseModel target, CounterpartAddressApiModel response)
		{
			target.RegionName = response.RegionName;
			target.DistrictName = response.DistrictName;
			target.CityName = response.CityName;
			target.CityTypeId = response.CityTypeId;
			target.StreetName = response.StreetName;
			target.StreetTypeId = response.StreetTypeId;
			target.HouseNumber = Converter.ToString(response.HouseNumber);
			target.HouseNumberAdd = response.HouseNumberAdd;
			target.PostIndex = response.PostIndex;
		}
	}
}
