using Newtonsoft.Json;
using System.Collections.Generic;

namespace Vostok.Hotline.Api.References.Responses.Addresses
{
    internal class ListCityTypeResponseModel
	{
        public ListCityTypeResponseModel()
        {
            Cities = new HashSet<CityResponseModel>();
        }

        public int CityTypeId { get; set; }
        public string CityTypeNameUa { get; set; }
        public string CityTypeNameEn { get; set; }
        public string CityTypeShortNameUa { get; set; }
        public string CityTypeShortNameEn { get; set; }
        public string CityTypeNameRu { get; set; }
        public string CityTypeShortNameRu { get; set; }
        public int Deleted { get; set; }

        [JsonIgnore]
        public virtual ICollection<CityResponseModel> Cities { get; set; }
    }
}
