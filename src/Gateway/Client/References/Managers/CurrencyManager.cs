using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Gateway.Client.Addresses.Services.SearchRequests;
using Vostok.Hotline.Gateway.Client.References.Services;
using Vostok.Hotline.Gateway.Client.References.ViewModels;


namespace Vostok.Hotline.Gateway.Client.References.Managers
{
	public class CurrencyManager
	{
		private readonly CurrencyService _service;

		public CurrencyManager(CurrencyService service)
		{
			_service = service;
		}

		public async Task<CurrencyCollectionViewModel> GetCurrenciesAsync(string[] currencyCodes, CancellationToken cancellationToken)
		{
			var collection = new CurrencyCollectionViewModel();
			foreach (var currencyCode in currencyCodes)
			{
				var request = new CurrencyDetailRequest
				{
					CurrencyCode = currencyCode
				};

				var response = await _service.GetCurrencyDetailAsync(request, cancellationToken);
				if (response != null)
					collection.Add(currencyCode, response);
			}
			return collection;
		}
	}
}
