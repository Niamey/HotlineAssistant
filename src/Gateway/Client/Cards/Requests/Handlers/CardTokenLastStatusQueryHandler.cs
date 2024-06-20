using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Gateway.Client.Cards.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Cards.Services;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Cards.Requests.Handlers
{
	public class CardTokenLastStatusQueryHandler : IRequestHandler<CardTokenLastStatusQuery, CardTokenLastStatusViewModel>
	{
		private readonly CardTokenService _cardTokenService;

		public CardTokenLastStatusQueryHandler(CardTokenService cardTokenService)
		{
			_cardTokenService = cardTokenService;
		}

		public async Task<CardTokenLastStatusViewModel> Handle(CardTokenLastStatusQuery request, CancellationToken cancellationToken)
		{
			return await _cardTokenService.GetLastStatusAsync(request, cancellationToken);			
		}
	}
}
