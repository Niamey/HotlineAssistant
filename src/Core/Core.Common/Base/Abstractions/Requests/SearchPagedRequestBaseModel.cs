using System.Web;

namespace Vostok.Hotline.Core.Common.Base.Abstractions.Requests
{
	public abstract class SearchPagedRequestBaseModel : SearchRequestBaseModel, ISearchRequestPagedModel
	{
		public SearchPagingModel Paging { get; set; }
		public SearchSortingModel Sorting { get; set; }

		public override string GetUrlQuery()
		{
			var parameters = HttpUtility.ParseQueryString(string.Empty);

			if (Paging != null)
			{
				if (Paging.CurrentPage.HasValue)
					parameters[$"{nameof(Paging)}.{nameof(Paging.CurrentPage)}"] = (Paging.CurrentPage - 1).ToString();

				parameters[$"{nameof(Paging)}.{nameof(Paging.PageSize)}"] = Paging.PageSize.ToString();
			}

			if (Sorting != null)
			{
				parameters[$"{nameof(Sorting)}.{nameof(Sorting.Column)}"] = Sorting.Column;
				parameters[$"{nameof(Sorting)}.{nameof(Sorting.IsDescending)}"] = Sorting.IsDescending.ToString();
			}

			return parameters.ToString();
		}
	}
}
