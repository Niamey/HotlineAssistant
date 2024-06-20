using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;

namespace Vostok.Hotline.Data.Repository.References.Models.Responses
{
	public class CountryResponseModel : ResponseBaseModel
	{
		public int CountryId { get; set; }
		public string CountryA2 { get; set; }
		public string CountryA3 { get; set; }
		public string CountryName { get; set; }
		public string CountryCode { get; set; }
		public bool IsCountryRisk { get; set; }
	}
}
