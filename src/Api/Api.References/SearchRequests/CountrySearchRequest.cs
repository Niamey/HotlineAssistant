using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Vostok.Hotline.Core.Common.Base.Abstractions.Requests;

namespace Vostok.Hotline.Api.References.SearchRequests
{
	internal class CountrySearchRequest : SearchRequestBaseModel
	{
		public CountrySearchRequest(int countryId)
		{
			CountryId = countryId;
		}

		public int CountryId { get; set; }

		public override string GetUrlQuery()
		{
			var parameters = HttpUtility.ParseQueryString(string.Empty);
			parameters[nameof(CountryId)] = CountryId.ToString();
			return parameters.ToString();
		}
	}
}