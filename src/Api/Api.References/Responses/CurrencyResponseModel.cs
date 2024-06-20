using System;
using System.Collections.Generic;
using System.Text;

namespace Vostok.Hotline.Api.References.Responses
{
	internal class CurrencyResponseModel
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
