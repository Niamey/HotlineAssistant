namespace Vostok.HotLineAssistant.ContactManager.Application.Client.Dtos
{
	public class ContactDto
	{
		public long Id { get; set; }
		public long CounterpartId { get; set; }
		public int TypeId { get; set; }
		public string Value { get; set; }

		public virtual ContactTypeDto Type { get; set; }
	}
}