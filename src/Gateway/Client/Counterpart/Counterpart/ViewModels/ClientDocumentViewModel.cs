using System;

namespace Vostok.Hotline.Gateway.Client.Counterpart.ViewModels
{
	public class ClientDocumentViewModel
	{
		public long CounterpartDocumentId { get; set; }
		
		public string Number { get; set; }
		public string Series { get; set; }

		public string IssueDate { get; set; }
		public string IssueBy { get; set; }
		public string PhotoDate { get; set; }
		
		public int? CountryId { get; set; }
		public string ExpirationDate { get; set; }
		public string ClosingDate { get; set; }

		public ClientDocumentTypeViewModel DocumentType { get; set; }
	}
}