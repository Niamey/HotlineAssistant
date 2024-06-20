using System.ComponentModel.DataAnnotations;
using System.Web;
using Vostok.Hotline.Core.Common.Base.Abstractions.Requests;

namespace Vostok.Hotline.Api.Retail.SearchRequests.Cards
{
	internal class CardCollectionSearchRequest : SearchRequestBaseModel
	{
		internal CardCollectionSearchRequest(long clientId)
		{
			ClientId = clientId;
		}

		public long ClientId { get; set; }

		public override string GetUrlQuery()
		{
			var parameters = HttpUtility.ParseQueryString(string.Empty);
			parameters[nameof(ClientId)] = ClientId.ToString();

			return parameters.ToString();
		}
	}
}
