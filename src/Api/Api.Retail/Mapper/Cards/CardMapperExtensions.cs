using Vostok.Hotline.Api.Retail.Models.Cards;
using Vostok.Hotline.Api.Retail.Responses.Cards;

namespace Vostok.Hotline.Api.Retail.Mapper.Cards
{
	internal static class CardMapperExtensions
	{
		public static CardApiModel ToCardApiModel(this CardResponseModel response)
		{
			var result = new CardApiModel
			{
				CardId = response.Id,
				SolarId = response.ClientId,
				FinancePhone = response.FinancePhone,
				AgreementId = response.AgreementId,
				Image = response.Image,
				InStopList = response.InStopList,
				Number = response.Number,
				SecurePhone = response.SecurePhone,
				SmsInfoPhone = response.SmsInfoPhone,
				EmbossedName = response.EmbossedName,				 
				Expired = response.Expired,
				Barcode = response.Barcode,
				HasChip = response.HasChip,
				IsVirtual = response.IsVirtual,
				Status = response.Status.ToCardStatusEnum(),
				Category = response.Category.ToCardStatusEnum(),
				Kinde = response.Kind.ToCardStatusEnum(),
				Type = response.Type.ToCardStatusEnum()
			};

			if (response.Branch != null)
			{
				result.Branch = new BranchApiModel
				{
					BranchId = response.Branch.Id,
					Address = response.Branch.Address,
					Name = response.Branch.Name,
					Phone = response.Branch.Phone					 
				};
			}

			if (response.Tariff != null)
			{
				result.Tariff = new TariffApiModel
				{
					TariffId = response.Tariff.Id,
					Name = response.Tariff.Name,
					Date = response.Tariff.Date					 
				};
			}

			return result;
		}

		public static CardCollectionApiModel ToCardCollectionApiModel(this CardCollectionResponseModel respose)
		{
			var result = new CardCollectionApiModel();

			foreach (var row in respose)
			{
				result.Add(row.ToCardApiModel());
			}

			return result;
		}
	}
}