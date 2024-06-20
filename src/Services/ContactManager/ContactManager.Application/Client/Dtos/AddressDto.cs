namespace Vostok.HotLineAssistant.ContactManager.Application.Client.Dtos
{
	public class AddressDto
	{
		public long Id { get; set; }
		public long CounterpartId { get; set; }
		public int TypeId { get; set; }
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

		public virtual AddressTypeDto Type { get; set; }
	}
}