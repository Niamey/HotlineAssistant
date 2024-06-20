using Vostok.Hotline.Core.Data.Entities;

namespace Vostok.Hotline.Data.EF.Entities.References
{
	public class CountrySetting : EntityBase
	{
		public string CountryCode { get; set; }
		public bool IsCountryRisk { get; set; }
	}
}
