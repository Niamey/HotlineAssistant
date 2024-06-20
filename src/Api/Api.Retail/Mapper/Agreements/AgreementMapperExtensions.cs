using Vostok.Hotline.Api.Retail.Models.Agreements;
using Vostok.Hotline.Api.Retail.Responses.Agreements;
using Vostok.Hotline.Core.Common.Enums.Agreements;

namespace Vostok.Hotline.Api.Retail.Mapper.Agreements
{
	internal static class AgreementMapperExtensions
	{
		public static AgreementApiModel ToAgreementViewModel(this AgreementResponseModel response)
		{
			var result = new AgreementApiModel
			{
				AgreementId = response.Id,
				SolarId = response.ClientId,
				ClientName = response.ClientName,
				CurrencyCode = response.CurrencyCode,
				Number = response.Number,
				ProductName = response.Product?.Name,
				Status = response.Status.ToAgreementStatusEnum(),
				Type = response.Type.ToAgreementTypeEnum(),
				Bonus = response.Bonus,
				ViewType = AgreementViewTypeEnum.Bvr
			};

			return result;
		}

		public static AgreementCollectionApiModel ToAgreementListViewModel(this AgreementCollectionResponseModel respose)
		{
			var result = new AgreementCollectionApiModel();

			foreach (var row in respose)
			{
				result.Add(row.ToAgreementViewModel());
			}

			return result;
		}
	}
}