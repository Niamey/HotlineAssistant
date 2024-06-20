using System.ComponentModel.DataAnnotations;
using System.Web;
using Vostok.Hotline.Core.Common.Base.Abstractions.Requests;

namespace Vostok.Hotline.Gateway.Client.Addresses.Services.SearchRequests
{
	public class CountryDetailRequest : SearchRequestBaseModel
	{
		[Required]
		public int? CountryId { get; set; }
		public bool? NoExceptionForNotFound { get; set; }

		public override string GetUrlQuery()
		{
			 var parameters = HttpUtility.ParseQueryString(string.Empty);
			 parameters[nameof(CountryId)] = CountryId.ToString();
			 return parameters.ToString();
		}
	}
}
