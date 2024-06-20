using System.Web;
using Vostok.Hotline.Core.Common.Base.Abstractions.Requests;

namespace Vostok.Hotline.Api.Retail.SearchRequests.Agreements
{
	internal class AgreementBalanceSearchRequest : SearchRequestBaseModel
	{
		internal AgreementBalanceSearchRequest(long? clientId, long agreementId)
		{
			ClientId = clientId;
			AgreementId = agreementId;
		}
		public long? ClientId { get; set; }
		public long AgreementId { get; set; }
		public override string GetUrlQuery()
		{
			var parameters = HttpUtility.ParseQueryString(string.Empty);

			if (ClientId.HasValue)
				parameters[nameof(ClientId)] = ClientId.ToString();

			parameters[nameof(AgreementId)] = AgreementId.ToString();

			return parameters.ToString();
		}
	}
}
