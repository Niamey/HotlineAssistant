using System.Threading.Tasks;
using Vostok.Hotline.Core.Common.Enums;
using Vostok.Hotline.Gateway.Client.Addresses.Helpers;
using Vostok.Hotline.Gateway.Client.Addresses.Managers;
using Vostok.Hotline.Gateway.Client.Addresses.Services.Models.Responses;
using Vostok.Hotline.Gateway.Client.Addresses.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Addresses.Mapper
{
	public class AddressMapper
	{
		private readonly DistrictManager _districtManager;
		private readonly RegionManager _regionManager;
		public AddressMapper(DistrictManager districtManager, RegionManager regionManager)
		{
			_districtManager = districtManager;
			_regionManager = regionManager;
		}

		public AddressViewModel MapToAddressViewModel(PostalAddressResponseModel response)
		{
			var result = new AddressViewModel
			{
				RegionId = response.Region.RegionId,
				RegionName = AddressHelper.GetFullTypeName(_regionManager.GetName(LocalizationEnum.Ukraine), response.Region.RegionNameUa, true),
				DistrictId = response.District.DistrictId,
				DistrictName = AddressHelper.GetFullTypeName(_districtManager.GetName(LocalizationEnum.Ukraine), response.District.DistrictNameUa, true),
				CityId = response.City.CityId,
				CityTypeId = response.City.CityType.CityTypeId,
				CityName = AddressHelper.GetFullTypeName(response.City.CityType.CityTypeShortNameUa, response.City.CityNameUa),
				CityTypeName = response.City.CityType.CityTypeShortNameUa,
				StreetId = response.Street.StreetId,
				StreetTypeId = response.Street.StreetType.StreetTypeId,
				StreetName = AddressHelper.GetFullTypeName(response.Street.StreetType.StreetTypeShortNameUa, response.Street.StreetNameUa),
				StreetTypeName = response.Street.StreetType.StreetTypeShortNameUa,
				PostIndex = response.Address.PostIndex,
				HouseNumber = response.Address.HouseNumber,
				HouseNumberAdd = response.Address.HouseNumberAdd
			};

			return result;
		}
	}
}
