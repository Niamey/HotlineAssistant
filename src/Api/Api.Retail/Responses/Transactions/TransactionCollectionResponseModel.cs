using System.Collections.Generic;
using Vostok.Hotline.Api.Retail.Models.Transactions;

namespace Vostok.Hotline.Api.Retail.Responses.Transactions
{
	internal class TransactionCollectionResponseModel
	{
		public List<TransactionResponseModel> Transactions { get; set; }

		public PagingResponseModel Paging { get; set; }
	}
}
