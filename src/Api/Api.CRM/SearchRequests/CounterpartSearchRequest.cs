using System.ComponentModel.DataAnnotations;
using Vostok.Hotline.Api.CRM.Enums;
using Vostok.Hotline.Core.Common.Enums;

namespace Vostok.Hotline.Api.CRM.SearchRequests
{
	public class CounterpartSearchRequest
	{
		[Required]
		public string SearchFilter { get; set; }

		[Required]
		public SearchTypeEnum SearchType { get; set; }

		public int? PageIndex { get; set; }

		public int PageSize { get; set; }

		public string SortColumn { get; set; }

		public SortDirectionEnum? SortDirection { get; set; }
	}
}
