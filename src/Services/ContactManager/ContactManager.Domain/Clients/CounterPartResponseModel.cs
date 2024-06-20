using System.Collections.Generic;

namespace Vostok.HotLineAssistant.ContactManager.Domain.Clients
{
	public class CounterPartResponseModel
	{
		public IEnumerable<Counterpart> Rows { get; set; }
		public bool IsNextPageAvailable { get; set; }
	}
}