using System;
using System.Collections.Generic;
using System.Text;
using Vostok.Hotline.Api.References.Models;
using Vostok.Hotline.Api.Retail.Models;
using Vostok.Hotline.Api.Retail.Models.MoneyBox;
using Vostok.Hotline.Api.Retail.Responses.Accounts;
using Vostok.Hotline.Api.Retail.Responses.MoneyBoxes;

namespace Vostok.Hotline.Api.Retail.Mapper.MoneyBoxes
{
	internal static class MoneyBoxMapperExtensions
	{
		public static MoneyBoxApiModel ToMoneyBoxApiModel(this MoneyBoxResponseModel response)
		{
			var result = new MoneyBoxApiModel
			{
				Id = response.Id,
				SavingId = response.SavingId,
				Name = response.Name,
				Amount = new MoneyApiModel()
				{
					Amount = response.Amount.Amount,
					CurrencyCode = response.Amount.Currency
				},
				Status = response.Status.ToMoneyBoxStatusEnum(),
			};

			return result;
		}

		public static MoneyBoxCollectionApiModel ToMoneyBoxCollectionApiModel(this MoneyBoxCollectionResponseModel respose)
		{
			var result = new MoneyBoxCollectionApiModel();

			foreach (var row in respose)
			{
				result.Add(row.ToMoneyBoxApiModel());
			}

			return result;
		}
	}
}
