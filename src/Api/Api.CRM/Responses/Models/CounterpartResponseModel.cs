using System;
using System.Collections.Generic;

namespace Vostok.Hotline.Api.CRM.Responses.Models
{
	internal class CounterpartResponseModel 
	{
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
		
		public int? ResidentCountryId { get; set; }

		public bool? IsOpen { get; set; }
		public bool? IsResident { get; set; }
		public bool? IsVip { get; set; }

		public DateTime? OpenedOn { get; set; }
		public DateTime? ClosedOn { get; set; }
				
		public long? ManagerId { get; set; }
		public int? DivisionId { get; set; }

		public DateTime? IdentityDate { get; set; }
		public int? IdentityCode { get; set; }
		public int? RiskLevelCode { get; set; }

		public CounterpartAttributeResponseModel Attributes { get; set; }
		public CounterpartTypeResponseModel CounterpartType { get; set; }
		public SegmentationTypeResponseModel SegmentationType { get; set; }

		public IEnumerable<CounterpartContactResponseModel> Contacts { get; set; }		
		public IEnumerable<CounterpartAddressResponseModel> Addresses { get; set; }
		public IEnumerable<CounterpartDocumentResponseModel> Documents { get; set; }
	}
}
