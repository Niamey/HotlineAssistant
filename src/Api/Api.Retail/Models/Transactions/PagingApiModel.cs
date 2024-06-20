namespace Vostok.Hotline.Api.Retail.Models.Transactions
{
	public class PagingApiModel
	{
		public int Page { get; set; }
		public int PageSize { get; set; }
		public int? Threshold { get; set; }
		public int TotalCount { get; set; }
	}
}
