using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;

namespace Vostok.Hotline.Gateway.Client.Countries.ViewModels
{
	public class CountryViewModel: ResponseBaseModel
	{
		public string CountryCode { get; set; }

		public string CountryName { get; set; }

		public string CountryA2 { get; set; }

		public bool IsRisky { get; set; }
	}
}
