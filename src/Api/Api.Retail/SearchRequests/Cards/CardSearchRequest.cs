using System.ComponentModel.DataAnnotations;
using System.Web;
using Vostok.Hotline.Core.Common.Base.Abstractions.Requests;

namespace Vostok.Hotline.Api.Retail.SearchRequests.Cards
{
	internal class CardSearchRequest : SearchRequestBaseModel
	{
		internal CardSearchRequest(long? clientId, long cardId)
		{
			ClientId = clientId;
			CardId = cardId;
		}

		public long? ClientId { get; set; }
		public long CardId { get; set; }

		public override string GetUrlQuery()
		{			
			var parameters = HttpUtility.ParseQueryString(string.Empty);
			
			if(ClientId.HasValue)
				parameters[nameof(ClientId)] = ClientId.ToString();

			parameters[nameof(CardId)] = CardId.ToString();

			return parameters.ToString();
		}
	}
}
