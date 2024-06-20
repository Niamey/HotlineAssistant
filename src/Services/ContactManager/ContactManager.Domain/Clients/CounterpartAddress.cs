namespace Vostok.HotLineAssistant.ContactManager.Domain.Clients
{
	public class CounterpartAddress
	{
		public long CounterpartAddressId { get; set; }
		public long CounterpartId { get; set; }
		public int AddressTypeId { get; set; }
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

		public virtual AddressTypes AdressType { get; set; }
	}
}