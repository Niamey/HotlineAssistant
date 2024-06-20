using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Gateway.Client.Addresses.Services.SearchRequests;
using Vostok.Hotline.Gateway.Client.References.Requests.Queries;
using Vostok.Hotline.Gateway.Client.References.Services;
using Vostok.Hotline.Gateway.Client.References.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Addresses.Requests.Handlers
{
	public class CurrencyDetailQueryHandler : IRequestHandler<CurrencyDetailQuery, CurrencyViewModel>
	{
		private readonly CurrencyService _currencyService;

		public CurrencyDetailQueryHandler(CurrencyService currencyService)
		{
			_currencyService = currencyService;
		}

		public async Task<CurrencyViewModel> Handle(CurrencyDetailQuery request, CancellationToken cancellationToken)
		{
			var searchRequest = new CurrencyDetailRequest
			{
				CurrencyCode = request.CurrencyCode
			};

			return await _currencyService.GetCurrencyDetailAsync(searchRequest, cancellationToken);
		}
	}
}
