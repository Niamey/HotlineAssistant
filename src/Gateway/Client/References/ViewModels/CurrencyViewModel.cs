using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;

namespace Vostok.Hotline.Gateway.Client.References.ViewModels
{
	public class CurrencyViewModel : ResponseBaseModel
	{
		public int Id { get; set; }
		public int CountryId { get; set; }
		public string CurrencyCode { get; set; }
		public string CurrencyShortName { get; set; }
		public string CurrencyName { get; set; }
		public string CurrencyNameUa { get; set; }
		public string CurrencyNameRu { get; set; }
	}
}
