using System.Collections.Generic;

namespace Vostok.Hotline.Api.Retail.Models.Transactions
{
	public class TransactionCollectionApiModel
	{
		public List<TransactionApiModel> Transactions { get; set; }
		public PagingApiModel Paging { get; set; }
	}
}