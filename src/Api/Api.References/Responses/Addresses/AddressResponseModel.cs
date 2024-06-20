using Newtonsoft.Json;

namespace Vostok.Hotline.Api.References.Responses.Addresses
{
    internal class AddressResponseModel
	{
        public int AddressId { get; set; }
        public int StreetId { get; set; }
        public string PostIndex { get; set; }
        public int? HouseNumber { get; set; }
        public string HouseNumberAdd { get; set; }
        public int Deleted { get; set; }

        [JsonIgnore]
        public virtual StreetResponseModel Street { get; set; }
    }
}
