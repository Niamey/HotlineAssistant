using System;
using System.Web;
using Vostok.Hotline.Core.Common.Base.Abstractions.Requests;
using Vostok.Hotline.Core.Common.Helpers;

namespace Vostok.Hotline.Api.Retail.SearchRequests.Transactions
{
	internal class TransactionCollectionSearchRequest : SearchRequestBaseModel
	{
		internal TransactionCollectionSearchRequest(long clientId, DateTime? dateFrom, DateTime? dateTo, int pageIndex, int pageSize)
		{
			ClientId = clientId;
			DateFrom = dateFrom;
			DateTo = dateTo;
			Page = pageIndex;
			PageSize = pageSize;
		}

		public long ClientId { get; set; }
		public DateTime? DateFrom { get; set; }
		public DateTime? DateTo { get; set; }
		public int Page { get; set; }
		public int PageSize { get; set; }

		public override string GetUrlQuery()
		{
			var parameters = HttpUtility.ParseQueryString(string.Empty);
			parameters[nameof(ClientId)] = ClientId.ToString();
			parameters[nameof(DateFrom)] = QueryHelper.ToValue(DateFrom);
			parameters[nameof(DateTo)] = QueryHelper.ToValue(DateTo);
			parameters[nameof(Page)] = Page.ToString();
			parameters[nameof(PageSize)] = PageSize.ToString();

			return parameters.ToString();
		}
	}
}
