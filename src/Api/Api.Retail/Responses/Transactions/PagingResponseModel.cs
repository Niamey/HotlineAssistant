namespace Vostok.Hotline.Api.Retail.Responses.Transactions
{
	public class PagingResponseModel
	{
		public int Page { get; set; }
		public int PageSize { get; set; }
		public int? Threshold { get; set; }
		public int TotalCount { get; set; }
    }
}
