using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.References.Managers;
using Vostok.Hotline.Api.Retail.ConstantNames;
using Vostok.Hotline.Api.Retail.Managers;
using Vostok.Hotline.Api.Services.Managers;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Enums.Cards;
using Vostok.Hotline.Core.Common.Exeptions;
using Vostok.Hotline.Gateway.Client.Cards.Mapper;
using Vostok.Hotline.Gateway.Client.Cards.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Cards.Services
{
	public class CardService
	{
		private readonly AgreementApiManager _agreementApiManager;
		private readonly CardApiManager _cardManager;
		private readonly CardMapper _cardMapper;
		private readonly CardLimitMapper _cardLimitMapper;
		private readonly CardBalanceMapper _cardBalanceMapper;
		private readonly CardLimitApiManager _cardLimitApiManager;
		private readonly CurrencyApiManager _currencyApiManager;
		private readonly CardImageApiManager _cardImageApiManager;


		public CardService(CardApiManager cardManager, CardMapper cardMapper, CardBalanceMapper cardBalanceMapper, CardLimitApiManager cardLimitApiManager, CurrencyApiManager currencyApiManager, CardLimitMapper cardLimitMapper, CardImageApiManager cardImageApiManager, AgreementApiManager agreementApiManager)
		{
			_agreementApiManager = agreementApiManager;
			_cardManager = cardManager;
			_cardMapper = cardMapper;
			_cardLimitMapper = cardLimitMapper;
			_cardBalanceMapper = cardBalanceMapper;
			_cardLimitApiManager = cardLimitApiManager;
			_currencyApiManager = currencyApiManager;
			_cardImageApiManager = cardImageApiManager;
		}

		public async Task<CardBalanceViewModel> GetCardBalanceAsync(CardBalanceQuery request, CancellationToken cancellationToken)
		{
			var result = await _cardManager.GetCardBalanceAsync(request.SolarId, request.CardId, cancellationToken);
			if (result != null)
				return await _cardBalanceMapper.MapToCardBalanceViewModelAsync(result, cancellationToken);
			else
				throw new NotFoundRequestException();
		}

		public async Task<CardExtendedBalanceViewModel> GetCardExtendedBalanceAsync(CardExtendedBalanceQuery request, CancellationToken cancellationToken)
		{
			var result = await _cardManager.GetCardBalanceAsync(request.SolarId, request.CardId, cancellationToken);
			if (result != null)
				return await _cardBalanceMapper.MapToCardExtendedBalanceViewModelAsync(result, cancellationToken);
			else
				throw new NotFoundRequestException();
		}

		public async Task<SearchRowsResponseRowModel<CardViewModel>> GetCardListAsync(CardListQuery request, CancellationToken cancellationToken)
		{
			var result = await _cardManager.GetCardCollectionAsync(request.SolarId.Value, cancellationToken);

			return await _cardMapper.MapToCardListViewModelAsync(result, cancellationToken);
		}

		public async Task<CardViewModel> GetCardDetailAsync(CardDetailQuery request, CancellationToken cancellationToken)
		{
			var result = await _cardManager.GetCardAsync(request.SolarId, request.CardId, cancellationToken);
			if (result != null)
				return await _cardMapper.MapToCardViewModelAsync(result, cancellationToken);
			else
				throw new NotFoundRequestException();
		}

		public async Task<CardFullBalanceViewModel> GetCardFullBalanceAsync(CardFullBalanceQuery request, CancellationToken cancellationToken)
		{
			var result = await _cardManager.GetCardBalanceAsync(request.SolarId, request.CardId.Value, cancellationToken);
			if (result != null)
				return await _cardBalanceMapper.MapToCardFullBalanceViewModelAsync(result, cancellationToken);
			else
				throw new NotFoundRequestException();
		}

		public async Task<CardLimitOfCashWithdrawalViewModel> GetCardLimitOfCashWithdrawalAsync(CardLimitOfCashWithdrawalQuery request, CancellationToken cancellationToken)
		{
			CardLimitOfCashWithdrawalViewModel result = new CardLimitOfCashWithdrawalViewModel();

			#region Moq
			var currencyUah = await _currencyApiManager.GetCurrencyShortNameAsync("980", cancellationToken);
			var limits = new List<AmountCardLimitViewModel>()
			{
				new AmountCardLimitViewModel()
				{
					CurrencyCode = null,
					Limit = 12,
					TypeLimit = CardTypeLimitEnum.Operation,
					CurrencyName = null,
					PeriodTypeLimit = CardPeriodTypeLimitEnum.Day,
					Used = 3
				},
				new AmountCardLimitViewModel()
				{
					CurrencyCode = null,
					Limit = 30,
					TypeLimit = CardTypeLimitEnum.Operation,
					CurrencyName = null,
					PeriodTypeLimit = CardPeriodTypeLimitEnum.Month,
					Used = 3
				}
			};
			result.InternalTransactions = new CardLimitViewModel() { Limits = limits };

			limits = new List<AmountCardLimitViewModel>()
			{
				new AmountCardLimitViewModel()
				{
					CurrencyCode = "980",
					Limit = 10000,
					TypeLimit = CardTypeLimitEnum.Money,
					CurrencyName = currencyUah,
					PeriodTypeLimit = CardPeriodTypeLimitEnum.Day,
					Used = 1000
				},
				new AmountCardLimitViewModel()
				{
					CurrencyCode = null,
					Limit = 12,
					TypeLimit = CardTypeLimitEnum.Operation,
					CurrencyName = null,
					PeriodTypeLimit = CardPeriodTypeLimitEnum.Day,
					Used = 2
				},
				new AmountCardLimitViewModel()
				{
					CurrencyCode = "980",
					Limit = 10000,
					TypeLimit = CardTypeLimitEnum.Money,
					CurrencyName = currencyUah,
					PeriodTypeLimit = CardPeriodTypeLimitEnum.Month,
					Used = 3000
				}
			};
			result.TransferTransactions = new CardLimitViewModel() { Limits = limits };

			result.SystemLimit = new CardLimitViewModel() 
			{ 
				Limits = new List<AmountCardLimitViewModel>()
				{
					new AmountCardLimitViewModel()
					{
						CurrencyCode = "980",
						Limit = 0,
						TypeLimit = CardTypeLimitEnum.Money,
						CurrencyName = currencyUah,
						PeriodTypeLimit = CardPeriodTypeLimitEnum.Day,
						Used = 0
					}
				}
			};
			#endregion

			var productsLimit = (await _cardLimitApiManager.GetLimitsAsync(request.CardId.Value, CardLimitConstant.ECommCard, cancellationToken)).FirstOrDefault();
			if (productsLimit != null)
			{
				result.Products = await _cardLimitMapper.MapToCardLimitViewModelAsync(productsLimit, cancellationToken);
			}
			//else
			//	throw new NotFoundRequestException();

			var isLimitOfCashWithdrawalActive = await _cardLimitApiManager.IsActiveLimitAsync(request.CardId.Value, CardLimitConstant.CashCard, cancellationToken);
			result.LimitOfCashWithdrawal = isLimitOfCashWithdrawalActive;

			return result;
		}

		public async Task<CardImageUrlViewModel> GetCardFrontImageUrlAsync(CardImageUrlQuery request, CancellationToken cancellationToken)
		{
			return new CardImageUrlViewModel()
			{
				CardCode = request.CardCode,
				FrontUrl = await _cardImageApiManager.GetFrontUrlAsync(request.CardCode, cancellationToken)
			};
		}
		public async Task<CardDebtToBankViewModel> GetDebtToBankAsync(CardDebtToBankQuery request, CancellationToken cancellationToken)
		{
			CardDebtToBankViewModel cardDebtToBankViewModel = new CardDebtToBankViewModel();

			var cardDetail = await _cardManager.GetCardAsync(request.SolarId, request.CardId.Value, cancellationToken);
			if (cardDetail == null)
				throw new NotFoundRequestException();

			var creditParams = await _agreementApiManager.GetCreditParamsAsync(cardDetail.AgreementId, cancellationToken);
			if (creditParams == null)
				throw new NotFoundRequestException();

			cardDebtToBankViewModel = creditParams.ToCardDebtToBankViewModel();

			var agreementDetail = await _agreementApiManager.GetAgreementAsync(request.SolarId.Value, cardDetail.AgreementId, cancellationToken);
			if (agreementDetail == null)
				throw new NotFoundRequestException();

			cardDebtToBankViewModel.CurrencyName = await _currencyApiManager.GetCurrencyShortNameAsync(agreementDetail.CurrencyCode, cancellationToken);

			return cardDebtToBankViewModel;
		}
	}}