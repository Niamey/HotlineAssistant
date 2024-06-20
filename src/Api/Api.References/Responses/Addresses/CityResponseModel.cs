using Newtonsoft.Json;
using System.Collections.Generic;

namespace Vostok.Hotline.Api.References.Responses.Addresses
{
    internal class CityResponseModel
	{
        public CityResponseModel()
        {
            Streets = new HashSet<StreetResponseModel>();
        }

        public int CityId { get; set; }
        public int CityTypeId { get; set; }
        public int? DistrictId { get; set; }
        public string CityNameUa { get; set; }
        public string CityNameEn { get; set; }
        public string CityOldNameUa { get; set; }
        public string CityOldNameEn { get; set; }
        public string CityCode { get; set; }
        public string CityNameRu { get; set; }
        public int Deleted { get; set; }
        public int Occupied { get; set; }

        public virtual ListCityTypeResponseModel CityType { get; set; }

        //[JsonIgnore]
        public virtual DistrictResponseModel District { get; set; }
        [JsonIgnore]
        public virtual ICollection<StreetResponseModel> Streets { get; set; }
    }
}
