namespace Vostok.Hotline.Api.CRM.Responses.Models
{
	internal class CounterpartAddressResponseModel 
	{
		public long CounterpartAddressId { get; set; }		
		public int? AddressId { get; set; }
		public string RegionName { get; set; }
		public string DistrictName { get; set; }
		public string CityName { get; set; }
		public int? CityTypeId { get; set; }
		public string StreetName { get; set; }
		public int? StreetTypeId { get; set; }
		public string HouseNumber { get; set; }
		public string HouseNumberAdd { get; set; }
		public string PostIndex { get; set; }
		public string Flat { get; set; }
		public string Korp { get; set; }

		public AddressTypeResponseModel AddressType { get; set; }
	}
}