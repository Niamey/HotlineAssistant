using System.Collections.Generic;


namespace Vostok.Hotline.Api.CRM.Responses.Models
{
	internal class CounterpartResponseRowModel
	{
		public bool IsNextPageAvailable { get; set; }
		public List<CounterpartResponseModel> Rows { get; set; }
	}
}