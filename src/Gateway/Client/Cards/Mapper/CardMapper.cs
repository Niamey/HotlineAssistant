using Vostok.Hotline.Api.Retail.Models.Cards;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;
using System.Threading;
using Vostok.Hotline.Api.Retail.Managers;
using System.Threading.Tasks;
using System.Linq;
using Vostok.Hotline.Core.Common.Enums.Cards;
using Vostok.Hotline.Api.Bvr.Managers;
using System;

namespace Vostok.Hotline.Gateway.Client.Cards.Mapper
{
	public class CardMapper
	{
		private readonly AgreementApiManager _agreementManager;
		private readonly DeviceApiManger _deviceApiManger;
		private readonly ClassifierApiManager _classifierApiManager;

		public CardMapper(AgreementApiManager agreementManager, DeviceApiManger deviceApiManger, ClassifierApiManager classifierApiManager)
		{
			_agreementManager = agreementManager;
			_deviceApiManger = deviceApiManger;
			_classifierApiManager = classifierApiManager;
		}

		public async Task<CardViewModel> MapToCardViewModelAsync(CardApiModel response, CancellationToken cancellationToken)
		{
			var has3ds = await _classifierApiManager.Has3dsForCardAsync(response.CardId, cancellationToken);

			var result = new CardViewModel
			{
				CardId = response.CardId,
				SolarId = response.SolarId,
				FinancePhone = response.FinancePhone,
				AgreementId = response.AgreementId,
				CardCode = response.Image?.ToLower(),
				InStopList = response.InStopList,
				Number = response.Number,
				SecurePhone = has3ds ? response.SecurePhone : null,
				SmsInfoPhone = response.SmsInfoPhone,
				EmbossedName = response.EmbossedName,				 
				Expired = response.Expired,
				Barcode = response.Barcode,
				HasChip = response.HasChip,
				IsVirtual = response.IsVirtual,
				Status = response.Status,
				Category = response.Category,
				Kinde = response.Kinde,
				Type = response.Type
			};

			result.ViewType = result.Type.ToViewType();

			var pushStatus = await _deviceApiManger.GetPushInfoStatusAsync(response.FinancePhone, cancellationToken);
			result.PushInfo = pushStatus.ToPushInfoStatusEnum();

			result.AgreementNumber = await _agreementManager.GetAgreementNumberAsync(response.SolarId, response.AgreementId, cancellationToken);

			if (response.Branch != null)
			{
				result.Branch = new BranchViewModel
				{
					BranchId = response.Branch.BranchId,
					Address = response.Branch.Address,
					Name = response.Branch.Name,
					Phone = response.Branch.Phone					 
				};
			}

			if (response.Tariff != null)
			{
				result.Tariff = new Api.Retail.Models.TariffApiModel
				{
					TariffId = response.Tariff.TariffId,
					TariffName = response.Tariff.Name,
					TariffStart = response.Tariff.Date != null ? response.Tariff.Date.Value.ToString("dd.MM.yyyy") : null				 
				};
			}

			return result;
		}

		public async Task<SearchRowsResponseRowModel<CardViewModel>> MapToCardListViewModelAsync(CardCollectionApiModel respose, CancellationToken cancellationToken )
		{
			var result = new SearchRowsResponseRowModel<CardViewModel>();
			var orderedResponse = respose.OrderByDescending(s => s.Status == CardStatusEnum.Active);

			foreach (var row in orderedResponse)
			{
				result.Rows.Add(await MapToCardViewModelAsync(row, cancellationToken));
			}

			return result;
		}
	}
}