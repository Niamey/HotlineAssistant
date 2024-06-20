using Newtonsoft.Json;
using System.Collections.Generic;

namespace Vostok.Hotline.Gateway.Client.Addresses.Services.Models.Responses
{
    public partial class RegionResponseModel
	{
        public RegionResponseModel()
        {
            Districts = new HashSet<DistrictResponseModel>();
        }

        public int RegionId { get; set; }
        public string RegionNameUa { get; set; }
        public string RegionNameEn { get; set; }
        public string RegionCode { get; set; }
        public string RegionNameRu { get; set; }
        public int Deleted { get; set; }
        [JsonIgnore]
        public virtual ICollection<DistrictResponseModel> Districts { get; set; }
    }
}
