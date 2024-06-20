using Newtonsoft.Json;
using System.Collections.Generic;

namespace Vostok.Hotline.Gateway.Client.Addresses.Services.Models.Responses
{
    public partial class DistrictResponseModel
	{
        public DistrictResponseModel()
        {
            Cities = new HashSet<CityResponseModel>();
        }

        public int DistrictId { get; set; }
        public int RegionId { get; set; }
        public string DistrictNameUa { get; set; }
        public string DistrictNameEn { get; set; }
        public string DistrictOldNameUa { get; set; }
        public string DistrictOldNameEn { get; set; }
        public string DistrictCode { get; set; }
        public string DistrictNameRu { get; set; }
        public int Deleted { get; set; }
        public byte DistrictType { get; set; }

        [JsonIgnore]
        public virtual RegionResponseModel Region { get; set; }
        [JsonIgnore]
        public virtual ICollection<CityResponseModel> Cities { get; set; }
    }
}
