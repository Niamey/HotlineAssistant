using System.ComponentModel.DataAnnotations;
using System.Web;
using Vostok.Hotline.Core.Common.Base.Abstractions.Requests;

namespace Vostok.Hotline.Gateway.Client.Addresses.Services.SearchRequests
{
	public class CurrencyDetailRequest : SearchRequestBaseModel
	{
		[Required]
		public string CurrencyCode { get; set; }

		public override string GetUrlQuery()
		{
			var parameters = HttpUtility.ParseQueryString(string.Empty);
			parameters[nameof(CurrencyCode)] = CurrencyCode;
			return parameters.ToString();
		}
	}
}
