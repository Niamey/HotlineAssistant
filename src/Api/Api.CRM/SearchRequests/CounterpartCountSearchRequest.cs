using System.ComponentModel.DataAnnotations;
using Vostok.Hotline.Api.CRM.Enums;

namespace Vostok.Hotline.Api.CRM.SearchRequests
{
	public class CounterpartCountSearchRequest 
	{
		[Required]
		public string SearchFilter { get; set; }

		[Required]
		public SearchTypeEnum SearchType { get; set; }
	}
}
