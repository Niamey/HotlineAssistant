using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Services.Models.CardService;

namespace Vostok.Hotline.Api.Services.Abstractions
{
	public interface ICardImageApiService
	{
		Task<CardImageApiModel> GetCardShirtAsync(string cardCode, CancellationToken cancellationToken);
	}
}
