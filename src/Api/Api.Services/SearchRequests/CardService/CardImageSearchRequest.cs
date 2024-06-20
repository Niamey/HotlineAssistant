using System.Web;
using Vostok.Hotline.Core.Common.Base.Abstractions.Requests;

namespace Vostok.Hotline.Api.Services.SearchRequests.CardService
{
	internal class CardImageSearchRequest : SearchRequestBaseModel
	{
		public CardImageSearchRequest(string cardProductCode)
		{
			CardProductCode = cardProductCode;
		}

		public string CardProductCode { get; set; }

		public override string GetUrlQuery()
		{
			var parameters = HttpUtility.ParseQueryString(string.Empty);

			parameters[nameof(CardProductCode)] = CardProductCode;

			return parameters.ToString();
		}
	}
}
