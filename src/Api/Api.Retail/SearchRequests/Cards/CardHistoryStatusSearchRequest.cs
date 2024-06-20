using System.ComponentModel.DataAnnotations;
using Vostok.Hotline.Core.Common.Base.Abstractions.Requests;

namespace Vostok.Hotline.Api.Retail.SearchRequests.Cards
{
	internal class CardHistoryStatusSearchRequest : SearchRequestBaseModel
	{
		internal CardHistoryStatusSearchRequest(long cardId)
		{
			CardId = cardId;
		}

		public long CardId { get; set; }

		public override string GetUrlQuery()
		{
			return CardId.ToString();
		}
	}
}
