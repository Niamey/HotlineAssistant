using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web;
using Vostok.Hotline.Core.Common.Base.Abstractions.Requests;

namespace Vostok.Hotline.Api.References.SearchRequests
{
	internal class CurrencySearchRequest : SearchRequestBaseModel
	{
		public CurrencySearchRequest(string currencyCode)
		{
			CurrencyCode = currencyCode;
		}

		public string CurrencyCode { get; set; }

		public override string GetUrlQuery()
		{
			var parameters = HttpUtility.ParseQueryString(string.Empty);
			parameters[nameof(CurrencyCode)] = CurrencyCode;
			return parameters.ToString();
		}
	}
}