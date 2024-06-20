using System.Collections.Generic;
using Vostok.Hotline.Api.Retail.Models.Transactions;
using Vostok.Hotline.Api.Retail.Responses.Transactions;

namespace Vostok.Hotline.Api.Retail.Mapper.Transactions
{
	internal static class TransactionMapperExtensions
	{
		public static TransactionApiModel ToTransactionApiModel(this TransactionResponseModel response)
		{
			var result = new TransactionApiModel
			{
				TxnId = response.TxnId,
				EntryType = response.EntryType.ToEntryTypeEnum(),
				Class = response.Class?.ToClassEnum() ?? null,
				Category = response.Category?.ToCategoryEnum() ?? null,
				Direction = response.Direction?.ToDirectionEnum() ?? null,
				DirectionClass = response.DirectionClass?.ToDirectionClassEnum() ?? null,
				TransactionDate = response.TransactionDate,
				SettlementDate = response.SettlementDate,
				TxnStatus = response.TxnStatus?.ToTxnStatusEnum() ?? null,
				Description = response.Description
			};

			if (response.TransactionType != null)
			{
				result.TransactionType = new TransactionTypeApiModel
				{
					Id = response.TransactionType.Id, 
					Code = response.TransactionType.Code,
					Name = response.TransactionType.Name
				};
			}

			if (response.Reference != null)
			{
				result.Reference = new ReferenceApiModel
				{
					OrigRefNumber = response.Reference.OrigRefNumber,
					AuthCode = response.Reference.AuthCode,
					RetrievalNumber = response.Reference.RetrievalNumber
				};
			}

			if (response.Amounts != null)
			{
				result.Amounts = new TransactionAmountsApiModel();

				if (response.Amounts.TransactionAmount != null) {
					result.Amounts.TransactionAmount = new MoneyApiModel
					{
						Amount = response.Amounts.TransactionAmount.Amount,
						Currency = response.Amounts.TransactionAmount.Currency
					};
				}
				if (response.Amounts.ReceiverBillingAmount != null)
				{
					result.Amounts.ReceiverBillingAmount = new MoneyApiModel
					{
						Amount = response.Amounts.ReceiverBillingAmount.Amount,
						Currency = response.Amounts.ReceiverBillingAmount.Currency
					};
				}
				if (response.Amounts.TotalReceiverBillingAmount != null)
				{
					result.Amounts.TotalReceiverBillingAmount = new MoneyApiModel
					{
						Amount = response.Amounts.TotalReceiverBillingAmount.Amount,
						Currency = response.Amounts.TotalReceiverBillingAmount.Currency
					};
				}			
			}

			if (response.FeeData != null)
			{
				result.FeeData = new FeeDataApiModel();

				if (response.FeeData.TotalAmount != null)
				{
					result.FeeData.TotalAmount = new MoneyApiModel
					{
						Amount = response.FeeData.TotalAmount.Amount,
						Currency = response.FeeData.TotalAmount.Currency
					};
				}
			}

			if (response.MerchantData != null)
			{
				result.MerchantData = new MerchantDataApiModel
				{
					MerchantName = response.MerchantData.MerchantName,
					MerchantCity = response.MerchantData.MerchantCity,
					MerchantState = response.MerchantData.MerchantState,
					MerchantCountry = response.MerchantData.MerchantCountry,
					Mсс = response.MerchantData.Mсс
				};
			}

			if (response.Response != null)
			{
				result.Response = new ResponseApiModel
				{
					Code = response.Response.Code,
					Message = response.Response.Message
				};
			}

			if (response.Agreement != null)
			{
				result.Agreement = new TransactionAgreementApiModel
				{
					Id = response.Agreement.Id,
					Number = response.Agreement.Number
				};

				if (response.Agreement.Card != null)
				{
					result.Agreement.Card = new TransactionCardApiModel
					{
						Id = response.Agreement.Card.Id,
						Number = response.Agreement.Card.Number
					};
				}
			}

			return result;
		}

		public static TransactionCollectionApiModel ToTransactionCollectionApiModel(this TransactionCollectionResponseModel response)
		{
			var result = new TransactionCollectionApiModel {
				Transactions = new List<TransactionApiModel>()
			};

			foreach (var row in response.Transactions)
			{
				result.Transactions.Add(row.ToTransactionApiModel());
			}

			if (response.Paging != null)
			{
				result.Paging = new PagingApiModel
				{
					Page = response.Paging.Page,
					PageSize = response.Paging.PageSize,
					Threshold = response.Paging.Threshold,
					TotalCount = response.Paging.TotalCount
				};
			}

			return result;
		}
	}
}
