using System.ComponentModel.DataAnnotations;

namespace Vostok.Hotline.Api.CRM.SearchRequests
{
	public class CounterpartSearchByPhoneRequest
	{
		[Required]
		public string SearchFilter { get; set; }
	}
}
