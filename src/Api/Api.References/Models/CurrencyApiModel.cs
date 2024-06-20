namespace Vostok.Hotline.Api.References.Models
{
	public class CurrencyApiModel
	{
		public int Id { get; set; }
		public int? CountryId { get; set; }
		public string CurrencyCode { get; set; }
		public string CurrencyShortName { get; set; }
		public string CurrencyName { get; set; }
		public string CurrencyNameUa { get; set; }
		public string CurrencyNameRu { get; set; }
	}
}
