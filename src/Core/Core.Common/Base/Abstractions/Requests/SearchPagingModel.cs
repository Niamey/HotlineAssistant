namespace Vostok.Hotline.Core.Common.Base.Abstractions.Requests
{
	public class SearchPagingModel
	{
		public const int DefaultPageSize = 10;

		public int? CurrentPage { get; set; }

		public int PageSize { get; set; }
	}
}
