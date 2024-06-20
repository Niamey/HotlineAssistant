using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Retail.Managers;
using Vostok.Hotline.Core.Common.Exeptions;
using Vostok.Hotline.Gateway.Client.Cards.Requests.Commands;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;
using Vostok.Hotline.Gateway.Client.Cards.Mapper;
using Vostok.Hotline.Document.Templates.Cards.Blocking.Abstractions;

namespace Vostok.Hotline.Gateway.Client.Cards.Services
{
	public class CardBlockingService
	{
		private readonly CardBlockingApiManager _cardBlockingApiManager;

		public CardBlockingService(CardBlockingApiManager cardBlockingApiManager)
		{
			_cardBlockingApiManager = cardBlockingApiManager;
		}

		public async Task<CardBlockingResultViewModel> BlockCardAsync(ICardBlockingModel request, CancellationToken cancellationToken)
		{
			var result = await _cardBlockingApiManager.BlockCardAsync(request, cancellationToken);
			if (result != null)
				return result.ToCardBlockingResultViewModel();
			else
				throw new NotFoundRequestException();
		}

	}
}