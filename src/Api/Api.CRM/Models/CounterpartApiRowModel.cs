using System.Collections.Generic;


namespace Vostok.Hotline.Api.CRM.Models
{
	public class CounterpartApiRowModel
	{
		public bool IsNextPageAvailable { get; set; }
		public List<CounterpartApiModel> Rows { get; set; }
	}
}