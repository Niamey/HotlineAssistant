using System;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Api.References.Helpers;
using Vostok.Hotline.Api.References.Managers;
using Vostok.Hotline.Api.References.Models;
using Vostok.Hotline.Api.References.Responses.Addresses;
using Vostok.Hotline.Core.Common.Enums;

namespace Vostok.Hotline.Api.References.Mapper
{
	internal class AddressMapper 
	{
		private readonly DistrictApiManager _districtManager;
		private readonly RegionApiManager _regionManager;
		public AddressMapper(IServiceProvider serviceProvider)
		{
			_districtManager = serviceProvider.GetRequiredService<DistrictApiManager>();
			_regionManager = serviceProvider.GetRequiredService<RegionApiManager>();
		}

		public AddressApiModel MapToAddressApiModel(PostalAddressResponseModel response)
		{
			if (response == null)
				return null;

			var result = new AddressApiModel
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
