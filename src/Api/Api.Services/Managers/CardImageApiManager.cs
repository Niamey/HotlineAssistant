using System;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Services.Abstractions;

namespace Vostok.Hotline.Api.Services.Managers
{
	public class CardImageApiManager
	{
		private readonly ICardImageApiService _cardImageApiService;
		public CardImageApiManager(ICardImageApiService cardImageApiService)
		{
			_cardImageApiService = cardImageApiService;
		}

		public async Task<string> GetFrontUrlAsync(string cardCode, CancellationToken cancellationToken)
		{
			var result = await _cardImageApiService.GetCardShirtAsync(cardCode, cancellationToken);

			return result?.FrontUrl;
		}
	}
}
