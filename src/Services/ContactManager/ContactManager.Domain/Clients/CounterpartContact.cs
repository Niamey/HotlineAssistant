namespace Vostok.HotLineAssistant.ContactManager.Domain.Clients
{
	public class CounterpartContact
	{
		public long CounterpartContactId { get; set; }
		public long CounterpartId { get; set; }
		public int ContactTypeId { get; set; }
		public string Value { get; set; }

		public virtual ContactTypes ContactType { get; set; }
	}
}