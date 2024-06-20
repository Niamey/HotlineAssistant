using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Enums.Counterparts;

namespace Vostok.Hotline.Gateway.Client.Counterpart.ViewModels
{
	public class ClientViewModel : ResponseBaseModel
	{
		public long CounterpartId { get; set; }
		public long? PersonId { get; set; }
		public long? SolarId { get; set; }
		public long? SrBankId { get; set; }

		public long? FinancialPhone { get; set; }
		public string Email { get; set; }

		public string Inn { get; set; }
		public string DateOfBirth { get; set; }
		public string BirthPlace { get; set; }

		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string FullName { get; set; }

		public GenderEnum? Gender { get; set; }
		public string CodeWord { get; set; }
		public string ResidentCountry { get; set; }

		public string WorkPlace { get; set; }
		public string WorkPosition { get; set; }

		public bool? IsOpen { get; set; }
		public bool? IsResident { get; set; }		

		public string OpenedOn { get; set; }
		public string ClosedOn { get; set; }

		public string ResidentialAddress { get; set; }
		public string RegistrationAddress  { get; set; }

		public ClientSegmentViewModel SegmentationType { get; set; }
		public ClientDocumentViewModel MainDocument { get; set; }
	}
}
