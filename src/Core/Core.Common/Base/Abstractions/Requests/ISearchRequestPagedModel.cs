using System;
using System.Collections.Generic;
using System.Text;

namespace Vostok.Hotline.Core.Common.Base.Abstractions.Requests
{
	public interface ISearchRequestPagedModel : ISearchRequestModel
	{
		SearchPagingModel Paging { get; set; }
		SearchSortingModel Sorting { get; set; }
	}
}
