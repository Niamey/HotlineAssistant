using Newtonsoft.Json;
using System.Collections.Generic;

namespace Vostok.Hotline.Gateway.Client.Addresses.Services.Models.Responses
{
    public partial class StreetResponseModel
	{
        public StreetResponseModel()
        {
            Addresses = new HashSet<AddressResponseModel>();
        }

        public int StreetId { get; set; }
        public int StreetTypeId { get; set; }
        public int CityId { get; set; }
        public string StreetNameUa { get; set; }
        public string StreetNameEn { get; set; }
        public string StreetOldNameUa { get; set; }
        public string StreetOldNameEn { get; set; }
        public string StreetNameRu { get; set; }
        public int Deleted { get; set; }

        [JsonIgnore]
        public virtual CityResponseModel City { get; set; }

        public virtual ListStreetTypeResponseModel StreetType { get; set; }
        [JsonIgnore]
        public virtual ICollection<AddressResponseModel> Addresses { get; set; }
    }
}
