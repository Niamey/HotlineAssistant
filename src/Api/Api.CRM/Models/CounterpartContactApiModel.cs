using Vostok.Hotline.Core.Common.Enums.Counterparts;

namespace Vostok.Hotline.Api.CRM.Models
{
	public class CounterpartContactApiModel 
	{
		public long CounterpartContactId { get; set; }
		public string Value { get; set; }
		public short? ContactAttributes { get; set; }
		public ContactTypeEnum? ContactType { get; set; }
	}
}