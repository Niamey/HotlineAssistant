using System.Collections.Generic;

namespace Vostok.HotLineAssistant.ContactManager.Application.Client.Dtos
{
	public class ClientDto
	{
		public ClientDto()
		{
			Addresses = new HashSet<AddressDto>();
			Contacts = new HashSet<ContactDto>();
			Documents = new HashSet<DocumentDto>();
		}
		public long? Id { get; set; }

		public long CounterpartId { get; set; }
		public long? SolarId { get; set; }
		public string Inn { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string FullName { get; set; }
		public int TypeId { get; set; }
		public long? FinancialPhone { get; set; }
		public bool IsOpen { get; set; }
		public string OpenedOn { get; set; }
		public string ClosedOn { get; set; }
		public bool IsResident { get; set; }
		public int? ResidentCountryId { get; set; }
		public bool? IsVip { get; set; }
		public long? ManagerId { get; set; }
		public int? DivisionId { get; set; }
		public string SecretWord { get; set; }
		public TypeDto Type { get; set; }
		public ICollection<AddressDto> Addresses { get; set; }
		public AttributeDto Attributes { get; set; }
		public ICollection<ContactDto> Contacts { get; set; }
		public ICollection<DocumentDto> Documents { get; set; }
	}
}