using System;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;

namespace Vostok.Hotline.Gateway.Client.Addresses.ViewModels
{
	public class AddressViewModel : ResponseBaseModel
	{
		public int RegionId { get; set; }
		public string RegionName { get; set; }
		public string RegionTypeName { get; set; }
		public int DistrictId { get; set; }
		public string DistrictName { get; set; }
		public string DistrictTypeName { get; set; }
		public int CityId { get; set; }
		public int CityTypeId { get; set; }
		public string CityName { get; set; }
		public string CityTypeName { get; set; }
		public int StreetId { get; set; }
		public int StreetTypeId { get; set; }
		public string StreetName { get; set; }
		public string StreetTypeName { get; set; }
		public string PostIndex { get; set; }
		public int? HouseNumber { get; set; }
		public string HouseNumberAdd { get; set; }
	}
}
