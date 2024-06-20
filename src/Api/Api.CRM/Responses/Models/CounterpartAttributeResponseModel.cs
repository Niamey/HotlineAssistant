using System;
using Vostok.Hotline.Api.CRM.Responses.Enums;

namespace Vostok.Hotline.Api.CRM.Responses.Models
{
	internal class CounterpartAttributeResponseModel 
	{	
		public string FirstNameLatin { get; set; }
		public string LastNameLatin { get; set; }
		public DateTime? DateOfBirth { get; set; }
		public string BirthPlace { get; set; }

		public GenderCrmEnum? Gender { get; set; }		
		public string CodeWord { get; set; }

		public byte? DependentAmount { get; set; }
		public MaritalStatusCrmEnum? MaritalStatus { get; set; }
		public string WorkPlace { get; set; }
		public string WorkPosition { get; set; }
		
		public bool? IsTerrorist { get; set; }
		public bool? IsPep { get; set; }
		public bool? IsArrest { get; set; }

		public SocialStatusResponseModel SocialStatus { get; set; }
	}
}
