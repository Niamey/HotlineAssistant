using System;

namespace Vostok.HotLineAssistant.ContactManager.Application.Client.Dtos
{
	public class DocumentDto
	{
		public long Id { get; set; }
		public long CounterpartId { get; set; }
		public int TypeId { get; set; }
		public string Number { get; set; }
		public string Series { get; set; }
		public string IssueDate { get; set; }
		public string IssueBy { get; set; }
		public string PhotoDate { get; set; }
		public int? CountryId { get; set; }
		public bool? IsMain { get; set; }
		public string ExpirationDate { get; set; }
		public string ClosingDate { get; set; }

		public virtual DocumentTypeDto Type { get; set; }
	}
}