using System.ComponentModel.DataAnnotations;
using System.Web;
using Vostok.Hotline.Core.Common.Base.Abstractions.Requests;

namespace Vostok.Hotline.Api.Retail.SearchRequests.Cards
{
	public class CardTariffSearchRequest : SearchRequestBaseModel
	{
		[Required]
		public long? ClientId { get; set; }
		[Required]
		public long? CardId { get; set; }

		public override string GetUrlQuery()
		{
			var parameters = HttpUtility.ParseQueryString(string.Empty);

			parameters[nameof(ClientId)] = ClientId.ToString();
			parameters[nameof(CardId)] = CardId.ToString();

			return parameters.ToString();
		}
	}
}
