using System;

namespace Vostok.HotLineAssistant.ContactManager.Application.Client.Dtos
{
	public class AttributeDto
	{
		public string FirstNameLatin { get; set; }
		public string LastNameLatin { get; set; }
		public string DateOfBirth { get; set; }
		public string BirthPlace { get; set; }
		public int? StatusId { get; set; }
		public int? Gender { get; set; }
		public byte? DependentAmount { get; set; }
		public string CodeWord { get; set; }
		public byte? MaritalStatus { get; set; }
		public string WorkPlace { get; set; }
		public string WorkPosition { get; set; }
		public bool? IsTerrorist { get; set; }
		public bool? IsPep { get; set; }
		public bool? IsArrest { get; set; }

		public virtual SocialStatusDto Status { get; set; }
	}
}