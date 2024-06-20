using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.References.Abstractions;
using Vostok.Hotline.Api.References.Models;

namespace Vostok.Hotline.Api.References.Managers
{
	public class AddressApiManager
	{
		private readonly IAddressApiService _addressApiService;
		public AddressApiManager(IAddressApiService addressApiService)
		{
			_addressApiService = addressApiService;
		}

		public async Task<AddressApiModel> GetAddressAsync(int addressId, CancellationToken cancellationToken)
		{
			return await _addressApiService.GetAddressAsync(addressId, cancellationToken);
		}

		public async Task<AddressCollectionApiModel> GetAdresseCollectionAsync(int[] addressIds, CancellationToken cancellationToken)
		{
			var collection = new AddressCollectionApiModel();
			foreach (var addressId in addressIds)
			{
				var response = await _addressApiService.GetAddressAsync(addressId, cancellationToken);
				if (response != null)
					collection.Add(addressId, response);
			}
			return collection;
		}
	}
}
