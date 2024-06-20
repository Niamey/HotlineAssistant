using System;
using System.Collections.Generic;

namespace Vostok.HotLineAssistant.ContactManager.Domain.Clients
{
	public class Counterpart
	{
		public Counterpart()
		{
			Addresses = new HashSet<CounterpartAddress>();
			Contacts = new HashSet<CounterpartContact>();
			Documents = new HashSet<CounterpartDocument>();
		}

		public long CounterpartId { get; set; }
		public long? PersonId { get; set; }
		public long? SolarId { get; set; }
		public long? SrBankId { get; set; }
		public long? SrBankCode { get; set; }
		public long? FinancialPhone { get; set; }
		public string Inn { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string FullName { get; set; }
		public int CounterpartTypeId { get; set; }
		public bool IsOpen { get; set; }
		public DateTime OpenedOn { get; set; }
		public DateTime? ClosedOn { get; set; }
		public bool IsResident { get; set; }
		public int? ResidentCountryId { get; set; }
		public bool? IsVip { get; set; }
		public long? ManagerId { get; set; }
		public int? DivisionId { get; set; }

		public CounterpartTypes CounterpartType { get; set; }
		public Person Person { get; set; }
		public ICollection<CounterpartAddress> Addresses { get; set; }
		public CounterpartAttribute Attributes { get; set; }
		public ICollection<CounterpartContact> Contacts { get; set; }
		public ICollection<CounterpartDocument> Documents { get; set; }
	}
}