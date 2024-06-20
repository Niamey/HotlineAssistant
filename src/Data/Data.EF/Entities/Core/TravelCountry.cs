using Vostok.Hotline.Core.Data.Entities;

namespace Vostok.Hotline.Data.EF.Entities.Core
{
	public class TravelCountry : BusinessEntityBase
	{
		public TravelCountry()
		{
		}

		public int TravelCountryId { get; set; }

		public int TravelId { get; set; }

		public string CountryCode { get; set; }

		public bool RiskCountry { get; set; }

		public virtual Travel Travel { get; set; }
	}
}
