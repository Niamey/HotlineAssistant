using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Gateway.Client.References.Managers;
using Vostok.Hotline.Gateway.Client.References.ViewModels;
using Vostok.Hotline.Gateway.Client.References.Mapper;

namespace Vostok.Hotline.Gateway.Client.References.Requests.Queries
{
	public class CurrencyListQueryHandler : IRequestHandler<CurrencyListQuery, SearchRowsResponseRowModel<CurrencyViewModel>>
	{
		private readonly CurrencyManager _currencyManager;

		public CurrencyListQueryHandler(CurrencyManager currencyManager)
		{
			_currencyManager = currencyManager;
		}

		public async Task<SearchRowsResponseRowModel<CurrencyViewModel>> Handle(CurrencyListQuery request, CancellationToken cancellationToken)
		{
			var searchRequest = new CurrencyListQuery
			{
				CurrencyCodes = request.CurrencyCodes
			};
			var result = await _currencyManager.GetCurrenciesAsync(searchRequest.CurrencyCodes, cancellationToken);
			return result.ToCurrencyListViewModel();
		}
	}
}
