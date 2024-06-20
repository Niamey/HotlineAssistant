using System.ComponentModel.DataAnnotations;

namespace Vostok.HotLineAssistant.ContactManager.Domain.Clients
{
	public class ClientSearchCriteria
	{
		[Required] 
		public CriteriaTypes Code { get; set; }

		[Required] public string Criteria { get; set; }
		public IdentifierTypes Type { get; set; }
		public int? CurrentPage { get; set; }
		public int? PageSize { get; set; }
		public string SortColumn { get; set; }
		public bool? IsDescending { get; set; }
	}

	public enum CriteriaTypes
	{
		NONE,
		P_FINANCIALPHONE,
		IDENTCODE,
		P_PNUMBER,
		FULLNAME,
		AGREEMENT_NUMBER,
		IBAN,
		CARD
	}

	public enum IdentifierTypes
	{
		None,
		Opened,
		Closed,
		Dummy
	}

	public enum SortDirection
	{
		Ascending = 0,
		Descending = 1
	}
}