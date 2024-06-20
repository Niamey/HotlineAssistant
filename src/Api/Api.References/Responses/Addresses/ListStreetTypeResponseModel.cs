using Newtonsoft.Json;
using System.Collections.Generic;

namespace Vostok.Hotline.Api.References.Responses.Addresses
{
    internal class ListStreetTypeResponseModel
	{
        public ListStreetTypeResponseModel()
        {
            Streets = new HashSet<StreetResponseModel>();
        }

        public int StreetTypeId { get; set; }
        public string StreetTypeNameUa { get; set; }
        public string StreetTypeNameEn { get; set; }
        public string StreetTypeShortNameUa { get; set; }
        public string StreetTypeShortNameEn { get; set; }
        public string StreetTypeNameRu { get; set; }
        public string StreetTypeShortNameRu { get; set; }
        public int Deleted { get; set; }

        [JsonIgnore]
        public virtual ICollection<StreetResponseModel> Streets { get; set; }
    }
}
