#nullable enable
using System;

namespace Vostok.HotLineAssistant.ContactManager.Domain.Clients
{
	public class CounterpartAttribute
	{
		public long CounterpartAttributeId { get; set; }
		public long CounterpartId { get; set; }
		public string? FirstNameLatin { get; set; }
		public string? LastNameLatin { get; set; }
		public DateTime? DateOfBirth { get; set; }
		public string? BirthPlace { get; set; }
		public int? SocialStatusId { get; set; }
		public int? Gender { get; set; }
		public byte? DependentAmount { get; set; }
		public string? CodeWord { get; set; }
		public byte? MaritalStatus { get; set; }
		public string? WorkPlace { get; set; }
		public string? WorkPosition { get; set; }
		public bool? IsTerrorist { get; set; }
		public bool? IsPep { get; set; }
		public bool? IsArrest { get; set; }
		public virtual SocialStatuses? SocialStatus { get; set; }
	}
}