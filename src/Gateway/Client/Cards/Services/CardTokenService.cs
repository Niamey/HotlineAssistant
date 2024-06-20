using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Retail.Managers;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Exeptions;
using Vostok.Hotline.Gateway.Client.Cards.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;
using Vostok.Hotline.Gateway.Client.Cards.Mapper;
using Vostok.Hotline.Api.Services.Managers;
using Vostok.Hotline.Core.Common.Enums.MDES;
using System;
using Vostok.Hotline.Core.Common.Models.Responses.Commands;
using Vostok.Hotline.Gateway.Client.Cards.Requests.Commands;
using Vostok.Hotline.Data.Repository.Core.Managers;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Core.Common.ConstantNames;
using System.Collections.Generic;
using System.Linq;
using Vostok.Hotline.Api.CRM.Managers;

namespace Vostok.Hotline.Gateway.Client.Cards.Services
{
	public class CardTokenService
	{
		private readonly MDESApiManager _mdesApiManager;
		private readonly CardApiManager _cardApiManager;
		private readonly EnvironmentManager _environmentManager;
		private readonly CounterpartApiManager _counterpartApiManager;
		private readonly BVRApiManager _bvrApiManager;


		private readonly List<ReasonTypeMdesEnum> _reasonBlock = new List<ReasonTypeMdesEnum> { 
			ReasonTypeMdesEnum.Lost, ReasonTypeMdesEnum.Stolen, ReasonTypeMdesEnum.Closed, ReasonTypeMdesEnum.Transaction
		};

		public CardTokenService(IServiceProvider serviceProvider)
		{
			_mdesApiManager = serviceProvider.GetRequiredService<MDESApiManager>();
			_cardApiManager = serviceProvider.GetRequiredService<CardApiManager>();
			_environmentManager = serviceProvider.GetRequiredService<EnvironmentManager>();
			_counterpartApiManager = serviceProvider.GetRequiredService<CounterpartApiManager>();
			_bvrApiManager = serviceProvider.GetRequiredService<BVRApiManager>();
		}

		private PaymentSystemTypeMdesEnum GetPaymentSystem(string cardNum)
			=> cardNum.Substring(0, 1) switch
			{
				"4" => PaymentSystemTypeMdesEnum.Visa,
				"5" => PaymentSystemTypeMdesEnum.MasterCard,
				// "6" => MAESTRO
				_ => PaymentSystemTypeMdesEnum.Undefined
			};

		private async Task<string> GetCardNumberAsync(long solarId, long cardId, CancellationToken cancellationToken) {
			var testCardNum = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.ApiServices.MDES.TestCardNumber, cancellationToken);

			if (!String.IsNullOrEmpty(testCardNum))
			{
				return testCardNum;
			}

			var cardNum = await _cardApiManager.GetCardNumberAsync(solarId, cardId, cancellationToken);
			if (String.IsNullOrEmpty(cardNum))
			{
				throw new NotFoundRequestException();
			}

			return cardNum;
		}

		public async Task<SearchRowsResponseRowModel<CardTokenViewModel>> GetCardTokenListAsync(CardTokenListQuery request, CancellationToken cancellationToken)
		{
			var cardNum = await GetCardNumberAsync(request.SolarId.Value, request.CardId.Value, cancellationToken);

			var result = await _mdesApiManager.GetTokensByPanAsync(cardNum, GetPaymentSystem(cardNum), cancellationToken);
			if (result != null)
				return result.ToCardTokenListViewModel();
			else
				throw new NotFoundRequestException();

		}

		public async Task<CardTokenLastStatusViewModel> GetLastStatusAsync(CardTokenLastStatusQuery request, CancellationToken cancellationToken)
		{
			var cardNum = await GetCardNumberAsync(request.SolarId.Value, request.CardId.Value, cancellationToken);
			
			var tokenId = request.TokenUniqueReference;
			if (String.IsNullOrEmpty(tokenId))
			{
				throw new NotFoundRequestException();
			}

			var result = await _mdesApiManager.GetLastStatusAsync(tokenId, GetPaymentSystem(cardNum), cancellationToken);
			if (result != null)
				return result.ToCardTokenLastStatusViewModel();
			else
				throw new NotFoundRequestException();

		}

		public async Task<StatusCommandViewModel> TokenDeleteAsync(CardTokenDeleteCommand request, CancellationToken cancellationToken)
		{
			var cardNum = await GetCardNumberAsync(request.SolarId.Value, request.CardId.Value, cancellationToken);

			var tokenId = request.TokenUniqueReference;
			if (String.IsNullOrEmpty(tokenId))
			{
				throw new NotFoundRequestException();
			}

			var result = await _mdesApiManager.TokenDeleteAsync(tokenId, request.CommentText, request.ReasonCode.Value, GetPaymentSystem(cardNum), cancellationToken);
			if (result == null)
			{
				throw new NotFoundRequestException();
			}

			if (_reasonBlock.Any(x => x == request.ReasonCode))
			{
				if (request.BlockMobApp.Value)
				{
					var client = await _counterpartApiManager.GetBySolarIdAsync(request.SolarId.Value, cancellationToken);
					var phone = client.FinancialPhone.ToString();
					if (String.IsNullOrEmpty(phone))
					{
						throw new NotFoundRequestException();
					}

					var resultBlock = await _bvrApiManager.AddPermanentBlockingAsync(phone, "token delete", cancellationToken);
				}
			}

			return result.ToStatusCommandViewModel();
		}

		public async Task<StatusCommandViewModel> TokenActivateAsync(CardTokenActivateCommand request, CancellationToken cancellationToken)
		{
			var cardNum = await GetCardNumberAsync(request.SolarId.Value, request.CardId.Value, cancellationToken);

			var tokenId = request.TokenUniqueReference;
			if (String.IsNullOrEmpty(tokenId))
			{
				throw new NotFoundRequestException();
			}

			var result = await _mdesApiManager.TokenActivateAsync(tokenId, GetPaymentSystem(cardNum), cancellationToken);
			if (result != null)
			{
				return result.ToStatusCommandViewModel();
			} else
			{
				throw new NotFoundRequestException();
			}			
		}
	}
}
