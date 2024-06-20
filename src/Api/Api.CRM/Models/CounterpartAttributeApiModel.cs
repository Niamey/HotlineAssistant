using System;
using Vostok.Hotline.Core.Common.Enums.Counterparts;

namespace Vostok.Hotline.Api.CRM.Models
{
	public class CounterpartAttributeApiModel 
	{	
		public string FirstNameLatin { get; set; }
		public string LastNameLatin { get; set; }
		public DateTime? DateOfBirth { get; set; }
		public string BirthPlace { get; set; }

		public GenderEnum? Gender { get; set; }		
		public string CodeWord { get; set; }

		public byte? DependentAmount { get; set; }
		public MaritalStatusEnum? MaritalStatus { get; set; }
		public string WorkPlace { get; set; }
		public string WorkPosition { get; set; }
		
		public bool? IsTerrorist { get; set; }
		public bool? IsPep { get; set; }
		public bool? IsArrest { get; set; }

		public SocialStatusApiModel SocialStatus { get; set; }
	}
}
