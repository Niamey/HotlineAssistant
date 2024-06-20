using Vostok.Hotline.Api.CRM.Models;
using Vostok.Hotline.Api.References.Models;
using Vostok.Hotline.Core.Common.Conversions;

namespace Vostok.Hotline.Gateway.Client.Counterpart.Mapper
{
	public static class CounterpartAddressMapperExtensions
	{
		public static void ToCounterpartAddressModel(this CounterpartAddressApiModel target, AddressApiModel response)
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
