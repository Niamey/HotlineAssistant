using System;

namespace Vostok.Hotline.Api.CRM.Models
{
	public class CounterpartDocumentApiModel 
	{
		public long CounterpartDocumentId { get; set; }		
		public string Number { get; set; }

		public string Series { get; set; }
		public DateTime? IssueDate { get; set; }
		public string IssueBy { get; set; }
		public DateTime? PhotoDate { get; set; }
		public int? CountryId { get; set; }
		public bool? IsMain { get; set; }
		public DateTime? ExpirationDate { get; set; }
		public DateTime? ClosingDate { get; set; }

		public DocumentTypeApiModel DocumentType { get; set; }
	}
}
