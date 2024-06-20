namespace Vostok.Hotline.Gateway.Client.Addresses.Services.Models.Responses
{
    public class PostalAddressResponseModel
	{
        public RegionResponseModel Region { get; set; }

        public DistrictResponseModel District { get; set; }

        public CityResponseModel City { get; set; }

        public StreetResponseModel Street { get; set; }

        public AddressResponseModel Address { get; set; }

    }
}
