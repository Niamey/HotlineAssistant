using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Core.Common.Configurations;
using Vostok.Hotline.Core.Common.Managers;
using Vostok.Hotline.Gateway.Client.Addresses.Services;
using Vostok.Hotline.Gateway.Client.Addresses.Services.SearchRequests;
using Vostok.Hotline.Gateway.Client.Addresses.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Addresses.Managers
{
	public class AddressManager
	{
		private readonly AddressService _service;

		public AddressManager(AddressService service)
		{
			_service = service;
		}

		public async Task<AddressCollectionViewModel> GetAdressesAsync(int[] addressIds, CancellationToken cancellationToken)
		{
			var collection = new AddressCollectionViewModel();
			foreach (var addressId in addressIds) {
				var request = new AddressDetailRequest
				{
					AddressId = addressId,
					NoExceptionForNotFound = true
				};

				var response = await _service.GetAddressDetailAsync(request, cancellationToken);
				if (response != null)
					collection.Add(addressId, response);
			}
			return collection;
		}
	}
}


